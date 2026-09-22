# Thiết kế Kiến trúc & Giải pháp Kỹ thuật (Solution Design) — Phân tích và tối ưu CV

| Thuộc tính | Giá trị |
|---|---|
| Artifact ID | DESIGN-CV-001 |
| Revision | Draft 1; ngày 2026-09-22 |
| Chế độ | Cổng G3 — Thiết kế Kiến trúc, Database Schema & API Contracts |
| Công nghệ lựa chọn (Tech Stack) | **ASP.NET Core Web API (.NET 9/8) + React (Vite) + Microsoft SQL Server** |
| Nguồn nghiệp vụ G1 | `docs/workflow/specs/cv-analysis-brief.md` (Artifact ID: BRIEF-CV-001, Revision Draft 6, Approved 2026-09-22) |
| Nguồn User Stories G2 | `docs/workflow/specs/cv-analysis-stories.md` (Artifact ID: STORIES-CV-001, Revision Draft 2, Approved 2026-09-22) |
| Prototype Mockup G2 | `docs/workflow/prototypes/cv-analysis-mockup.html` (Đã kiểm thử Browser Native) |
| Sơ đồ ERD kiểm thử | `docs/workflow/diagrams/cv-analysis-erd.html` (Đã kiểm thử Browser Native) |
| Trạng thái G3 | `approved`: Người dùng đã phê duyệt chính thức ngày 2026-09-22 sau khi xem sơ đồ ERD trực quan; sẵn sàng bàn giao cho Cổng G4 (delivery-planning) |

---

## 1. Quyết định Công nghệ & ADR-001

### ADR-001: Lựa chọn Tech Stack Doanh nghiệp (Enterprise-Grade)
- **Bối cảnh:** Hệ thống phân tích và tối ưu CV vận hành theo mô hình Freemium & gói credit VND tại thị trường Việt Nam. Đòi hỏi độ tin cậy giao dịch tuyệt đối (ACID, chống trừ trùng/âm credit), xử lý văn bản tài liệu đa định dạng (PDF, Word DOCX), điều phối tác vụ AI bất đồng bộ với Circuit Breaker 90 giây, và ma trận phân quyền 5 roles chặt chẽ theo nguyên tắc Zero-Trust.
- **Quyết định:** Áp dụng phương án công nghệ đã được người dùng phê duyệt:
  1. **Backend:** **ASP.NET Core Web API (.NET 9 / C#)** theo mô hình Clean Architecture (Domain, Application, Infrastructure, WebApi).
     - *ORM:* Entity Framework Core (EF Core) 9 với SQL Server Provider.
     - *Xử lý file:* `PdfPig` / `iTextSharp` cho PDF; `DocumentFormat.OpenXml` cho Word DOCX.
     - *Bất đồng bộ & Background Worker:* `IHostedService` / `BackgroundService` của .NET kết hợp Channel / Hangfire để điều phối tác vụ AI và giám sát timeout 90 giây.
     - *Bảo mật & Auth:* ASP.NET Core Identity + JWT Bearer Authentication, Role-based Claims cho 5 roles và Custom Authorization Handler cho quyền xem CV tạm thời (tối đa 72h).
  2. **Frontend:** **React 18/19 (Vite + TypeScript + Tailwind CSS)**.
     - *Quản lý trạng thái & Data Fetching:* TanStack Query (React Query) v5 + Zustand.
     - *Diff view:* Thư viện `diff` kết hợp visual styling Action-Context-Metric.
     - *Icon & UI:* Lucide React + Tailwind CSS.
  3. **Cơ sở dữ liệu:** **Microsoft SQL Server (2022 / Azure SQL Database)**.
     - Cung cấp đảm bảo giao dịch mạnh mẽ (ACID), hỗ trợ `UPDLOCK, ROWLOCK` cho việc khóa giữ credit, hỗ trợ lưu trữ dữ liệu JSON có cấu trúc (`NVARCHAR(MAX)` với hàm `ISJSON`), và Temporal Tables / Audit Triggers cho nhật ký bảo mật.

---

## 2. Sơ đồ ERD & Database Schema (SQL Server)

Sơ đồ ERD vật lý của hệ thống đã được thiết kế và kiểm thử thành công bằng Engine Browser Native tại:
👉 **[Sơ đồ ERD trực quan (docs/workflow/diagrams/cv-analysis-erd.html)](../diagrams/cv-analysis-erd.html)**

### Danh mục 15 Bảng Cơ sở Dữ liệu

```text
[dbo.Users] 1 ─────── 1 [dbo.UserBalances] (PK/FK: user_id, RowVersion)
     │ 1                     │ 1
     │                       └─── N [dbo.CreditLedger]
     │ 1
     ├─── N [dbo.CVFiles] 1 ─── N [dbo.AnalysisRequests] 1 ─── 1 [dbo.Reports]
     │                                     │                         │ 1
     ├─── N [dbo.JobDescriptions]          │                         └─── N [dbo.OptimizedBulletPoints]
     │                                     │
     ├─── N [dbo.PaymentOrders]            │
     │                                     │
     ├─── N [dbo.SupportTickets] 1 ────────┼─── 1..* [dbo.SupportAccessGrants] (72h TTL)
     │                                     │
     ├─── N [dbo.AuditLogs]                └─── 0..1 [dbo.CompensationRequests]
     │
     └─── [dbo.EmailHashRegistry] (12 months abuse prevention)
```

### Chi tiết DDL (SQL Server Schema)

#### 1. Bảng `dbo.Users` (Quản lý người dùng & 5 Roles)
```sql
CREATE TABLE dbo.Users (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_Users_id DEFAULT NEWSEQUENTIALID(),
    email NVARCHAR(256) NOT NULL,
    password_hash NVARCHAR(512) NOT NULL,
    role NVARCHAR(32) NOT NULL, -- 'Candidate', 'SupportSpecialist', 'SupportLead', 'Finance', 'SystemAdmin'
    is_email_verified BIT NOT NULL CONSTRAINT DF_Users_is_email_verified DEFAULT 0,
    email_verified_at DATETIMEOFFSET NULL,
    is_active BIT NOT NULL CONSTRAINT DF_Users_is_active DEFAULT 1,
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_Users_created_at DEFAULT SYSDATETIMEOFFSET(),
    updated_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_Users_updated_at DEFAULT SYSDATETIMEOFFSET(),
    CONSTRAINT PK_Users PRIMARY KEY CLUSTERED (id),
    CONSTRAINT UQ_Users_email UNIQUE NONCLUSTERED (email),
    CONSTRAINT CK_Users_role CHECK (role IN ('Candidate', 'SupportSpecialist', 'SupportLead', 'Finance', 'SystemAdmin'))
);
CREATE NONCLUSTERED INDEX IX_Users_role ON dbo.Users (role);
```

#### 2. Bảng `dbo.EmailHashRegistry` (Chống lạm dụng lượt dùng thử 12 tháng — BR34)
```sql
CREATE TABLE dbo.EmailHashRegistry (
    email_hash NVARCHAR(128) NOT NULL, -- SHA-256 hash của normalized email
    deleted_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_EmailHashRegistry_deleted_at DEFAULT SYSDATETIMEOFFSET(),
    trial_blocked_until DATETIMEOFFSET NOT NULL, -- deleted_at + 12 tháng
    CONSTRAINT PK_EmailHashRegistry PRIMARY KEY CLUSTERED (email_hash)
);
CREATE NONCLUSTERED INDEX IX_EmailHashRegistry_blocked_until ON dbo.EmailHashRegistry (trial_blocked_until);
```

#### 3. Bảng `dbo.UserBalances` (Bất biến số dư & Concurrency RowVersion — BR04, BR13)
```sql
CREATE TABLE dbo.UserBalances (
    user_id UNIQUEIDENTIFIER NOT NULL,
    free_trial_credits INT NOT NULL CONSTRAINT DF_UserBalances_free_trial DEFAULT 3,
    paid_credits INT NOT NULL CONSTRAINT DF_UserBalances_paid_credits DEFAULT 0,
    held_credits INT NOT NULL CONSTRAINT DF_UserBalances_held_credits DEFAULT 0,
    row_version ROWVERSION NOT NULL, -- Optimistic concurrency token
    updated_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_UserBalances_updated_at DEFAULT SYSDATETIMEOFFSET(),
    CONSTRAINT PK_UserBalances PRIMARY KEY CLUSTERED (user_id),
    CONSTRAINT FK_UserBalances_Users FOREIGN KEY (user_id) REFERENCES dbo.Users(id) ON DELETE CASCADE,
    CONSTRAINT CK_UserBalances_free_trial CHECK (free_trial_credits >= 0),
    CONSTRAINT CK_UserBalances_paid_credits CHECK (paid_credits >= 0),
    CONSTRAINT CK_UserBalances_held_credits CHECK (held_credits >= 0)
);
```

#### 4. Bảng `dbo.CVFiles` (Quản lý file CV & trích xuất — BR03, BR21, OQ06)
```sql
CREATE TABLE dbo.CVFiles (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_CVFiles_id DEFAULT NEWSEQUENTIALID(),
    user_id UNIQUEIDENTIFIER NOT NULL,
    file_name NVARCHAR(256) NOT NULL,
    file_size_bytes BIGINT NOT NULL,
    page_count INT NOT NULL,
    file_path NVARCHAR(512) NOT NULL,
    raw_text NVARCHAR(MAX) NOT NULL,
    extracted_data NVARCHAR(MAX) NOT NULL, -- JSON structure (Contact, Experience, Skills, Education)
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_CVFiles_created_at DEFAULT SYSDATETIMEOFFSET(),
    expires_at DATETIMEOFFSET NOT NULL, -- created_at + 90 ngày
    is_deleted BIT NOT NULL CONSTRAINT DF_CVFiles_is_deleted DEFAULT 0,
    CONSTRAINT PK_CVFiles PRIMARY KEY CLUSTERED (id),
    CONSTRAINT FK_CVFiles_Users FOREIGN KEY (user_id) REFERENCES dbo.Users(id) ON DELETE NO ACTION,
    CONSTRAINT CK_CVFiles_file_size CHECK (file_size_bytes <= 10485760), -- <= 10MB
    CONSTRAINT CK_CVFiles_page_count CHECK (page_count <= 5 AND page_count >= 1)
);
CREATE NONCLUSTERED INDEX IX_CVFiles_user_expires ON dbo.CVFiles (user_id, expires_at) WHERE is_deleted = 0;
```

#### 5. Bảng `dbo.JobDescriptions` (Mô tả tuyển dụng & kiểm tra căn cứ — BR35, Mục 5.1)
```sql
CREATE TABLE dbo.JobDescriptions (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_JobDescriptions_id DEFAULT NEWSEQUENTIALID(),
    user_id UNIQUEIDENTIFIER NOT NULL,
    title NVARCHAR(256) NOT NULL,
    raw_text NVARCHAR(MAX) NOT NULL,
    word_count INT NOT NULL,
    min_requirements_met BIT NOT NULL, -- >= 50 từ, có chức danh, >= 3 kỹ năng/2 trách nhiệm
    extracted_skills_json NVARCHAR(MAX) NULL, -- Phân nhóm bắt buộc vs ưu tiên
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_JobDescriptions_created_at DEFAULT SYSDATETIMEOFFSET(),
    CONSTRAINT PK_JobDescriptions PRIMARY KEY CLUSTERED (id),
    CONSTRAINT FK_JobDescriptions_Users FOREIGN KEY (user_id) REFERENCES dbo.Users(id) ON DELETE NO ACTION,
    CONSTRAINT CK_JobDescriptions_word_count CHECK (word_count >= 50 AND word_count <= 2500)
);
CREATE NONCLUSTERED INDEX IX_JobDescriptions_user_created ON dbo.JobDescriptions (user_id, created_at);
```

#### 6. Bảng `dbo.AnalysisRequests` (Vòng đời phân tích & Circuit Breaker 90s — BR04, BR17)
```sql
CREATE TABLE dbo.AnalysisRequests (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_AnalysisRequests_id DEFAULT NEWSEQUENTIALID(),
    user_id UNIQUEIDENTIFIER NOT NULL,
    cv_file_id UNIQUEIDENTIFIER NOT NULL,
    job_description_id UNIQUEIDENTIFIER NULL, -- NULL nếu phân tích tổng quát (BR12)
    analysis_type NVARCHAR(32) NOT NULL, -- 'BasicGeneral', 'BasicJdMatch', 'PremiumBundle'
    status NVARCHAR(32) NOT NULL, -- 'Queued', 'Processing', 'Completed', 'Failed', 'TimedOut', 'Cancelled'
    content_hash NVARCHAR(128) NOT NULL, -- Phục vụ Deduplication cache (BR36)
    timeout_at DATETIMEOFFSET NOT NULL, -- started_at + 90 giây
    partial_result_expires_at DATETIMEOFFSET NULL, -- failed_at + 24 giờ (BR17)
    error_message NVARCHAR(1024) NULL,
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_AnalysisRequests_created_at DEFAULT SYSDATETIMEOFFSET(),
    completed_at DATETIMEOFFSET NULL,
    CONSTRAINT PK_AnalysisRequests PRIMARY KEY CLUSTERED (id),
    CONSTRAINT FK_AnalysisRequests_Users FOREIGN KEY (user_id) REFERENCES dbo.Users(id) ON DELETE NO ACTION,
    CONSTRAINT FK_AnalysisRequests_CVFiles FOREIGN KEY (cv_file_id) REFERENCES dbo.CVFiles(id) ON DELETE CASCADE,
    CONSTRAINT FK_AnalysisRequests_JobDescriptions FOREIGN KEY (job_description_id) REFERENCES dbo.JobDescriptions(id) ON DELETE NO ACTION
);
CREATE NONCLUSTERED INDEX IX_AnalysisRequests_status_timeout ON dbo.AnalysisRequests (status, timeout_at);
CREATE NONCLUSTERED INDEX IX_AnalysisRequests_dedup ON dbo.AnalysisRequests (user_id, content_hash, status);
```

#### 7. Bảng `dbo.Reports` (Báo cáo điểm ATS & Rubric 4 Trụ cột — BR01, BR05, BR18)
```sql
CREATE TABLE dbo.Reports (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_Reports_id DEFAULT NEWSEQUENTIALID(),
    analysis_request_id UNIQUEIDENTIFIER NOT NULL,
    overall_score INT NOT NULL, -- 0-100 (Round Half Up)
    score_label NVARCHAR(64) NOT NULL, -- 'Xuất sắc', 'Khá', 'Trung bình', 'Kém'
    skills_score DECIMAL(5,2) NOT NULL,
    experience_score DECIMAL(5,2) NOT NULL,
    format_score DECIMAL(5,2) NOT NULL,
    education_score DECIMAL(5,2) NULL, -- NULL nếu NOT_APPLICABLE
    normalized_denominator INT NOT NULL, -- 85 hoặc 100
    mandatory_warnings_json NVARCHAR(MAX) NULL, -- Cảnh báo bắt buộc BR18
    report_details_json NVARCHAR(MAX) NOT NULL,
    is_system_error BIT NOT NULL CONSTRAINT DF_Reports_is_system_error DEFAULT 0, -- BR37
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_Reports_created_at DEFAULT SYSDATETIMEOFFSET(),
    expires_at DATETIMEOFFSET NOT NULL, -- created_at + 90 ngày
    CONSTRAINT PK_Reports PRIMARY KEY CLUSTERED (id),
    CONSTRAINT UQ_Reports_analysis_request UNIQUE NONCLUSTERED (analysis_request_id),
    CONSTRAINT FK_Reports_AnalysisRequests FOREIGN KEY (analysis_request_id) REFERENCES dbo.AnalysisRequests(id) ON DELETE CASCADE,
    CONSTRAINT CK_Reports_overall_score CHECK (overall_score >= 0 AND overall_score <= 100)
);
CREATE NONCLUSTERED INDEX IX_Reports_expires ON dbo.Reports (expires_at) WHERE is_system_error = 0;
```

#### 8. Bảng `dbo.OptimizedBulletPoints` (Bản viết lại Action-Context-Metric & Diff view — BR06, BR14)
```sql
CREATE TABLE dbo.OptimizedBulletPoints (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_OptimizedBulletPoints_id DEFAULT NEWSEQUENTIALID(),
    report_id UNIQUEIDENTIFIER NOT NULL,
    section_name NVARCHAR(128) NOT NULL,
    original_text NVARCHAR(MAX) NOT NULL,
    suggested_text NVARCHAR(MAX) NOT NULL,
    customized_text NVARCHAR(MAX) NULL,
    status NVARCHAR(32) NOT NULL CONSTRAINT DF_OptimizedBulletPoints_status DEFAULT 'Pending', -- 'Pending', 'Accepted', 'Rejected'
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_OptimizedBulletPoints_created_at DEFAULT SYSDATETIMEOFFSET(),
    CONSTRAINT PK_OptimizedBulletPoints PRIMARY KEY CLUSTERED (id),
    CONSTRAINT FK_OptimizedBulletPoints_Reports FOREIGN KEY (report_id) REFERENCES dbo.Reports(id) ON DELETE CASCADE
);
CREATE NONCLUSTERED INDEX IX_OptimizedBulletPoints_report ON dbo.OptimizedBulletPoints (report_id);
```

#### 9. Bảng `dbo.CreditPackages` (Cấu hình bảng giá credit VND động — Kế toán quản lý, BR28, OQ08)
```sql
CREATE TABLE dbo.CreditPackages (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_CreditPackages_id DEFAULT NEWSEQUENTIALID(),
    package_name NVARCHAR(128) NOT NULL,
    credits_amount INT NOT NULL,
    price_vnd DECIMAL(18,2) NOT NULL,
    is_active BIT NOT NULL CONSTRAINT DF_CreditPackages_is_active DEFAULT 1,
    created_by_user_id UNIQUEIDENTIFIER NOT NULL, -- Phải là role Finance
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_CreditPackages_created_at DEFAULT SYSDATETIMEOFFSET(),
    CONSTRAINT PK_CreditPackages PRIMARY KEY CLUSTERED (id),
    CONSTRAINT FK_CreditPackages_Users FOREIGN KEY (created_by_user_id) REFERENCES dbo.Users(id) ON DELETE NO ACTION,
    CONSTRAINT CK_CreditPackages_credits CHECK (credits_amount > 0),
    CONSTRAINT CK_CreditPackages_price CHECK (price_vnd > 0)
);
```

#### 10. Bảng `dbo.PaymentOrders` (Đơn hàng 15p & Chờ đối soát — BR11, OQ05)
```sql
CREATE TABLE dbo.PaymentOrders (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_PaymentOrders_id DEFAULT NEWSEQUENTIALID(),
    order_code NVARCHAR(64) NOT NULL, -- ORD-YYYYMMDD-XXXX
    user_id UNIQUEIDENTIFIER NOT NULL,
    package_id UNIQUEIDENTIFIER NOT NULL,
    credits_amount INT NOT NULL,
    amount_vnd DECIMAL(18,2) NOT NULL,
    status NVARCHAR(32) NOT NULL, -- 'Pending', 'Completed', 'Expired', 'AwaitingReconciliation'
    gateway_transaction_id NVARCHAR(128) NULL,
    reconciliation_notes NVARCHAR(512) NULL,
    reconciled_by_user_id UNIQUEIDENTIFIER NULL, -- Role Finance
    reconciled_at DATETIMEOFFSET NULL,
    expires_at DATETIMEOFFSET NOT NULL, -- created_at + 15 phút
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_PaymentOrders_created_at DEFAULT SYSDATETIMEOFFSET(),
    completed_at DATETIMEOFFSET NULL,
    CONSTRAINT PK_PaymentOrders PRIMARY KEY CLUSTERED (id),
    CONSTRAINT UQ_PaymentOrders_code UNIQUE NONCLUSTERED (order_code),
    CONSTRAINT FK_PaymentOrders_Users FOREIGN KEY (user_id) REFERENCES dbo.Users(id) ON DELETE NO ACTION,
    CONSTRAINT FK_PaymentOrders_CreditPackages FOREIGN KEY (package_id) REFERENCES dbo.CreditPackages(id) ON DELETE NO ACTION
);
CREATE NONCLUSTERED INDEX IX_PaymentOrders_reconciliation ON dbo.PaymentOrders (status, expires_at);
```

#### 11. Bảng `dbo.CreditLedger` (Sổ cái bất biến số dư — BR04, BR13, BR37)
```sql
CREATE TABLE dbo.CreditLedger (
    id BIGINT IDENTITY(1,1) NOT NULL,
    user_id UNIQUEIDENTIFIER NOT NULL,
    analysis_request_id UNIQUEIDENTIFIER NULL,
    payment_order_id UNIQUEIDENTIFIER NULL,
    entry_type NVARCHAR(32) NOT NULL, -- 'FreeTrialGranted', 'Hold', 'ReleaseHold', 'ConsumePaid', 'ConsumeFree', 'ManualCreditAdd', 'Compensation'
    credit_type NVARCHAR(16) NOT NULL, -- 'Free', 'Paid'
    amount INT NOT NULL,
    balance_before INT NOT NULL,
    balance_after INT NOT NULL,
    description NVARCHAR(512) NOT NULL,
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_CreditLedger_created_at DEFAULT SYSDATETIMEOFFSET(),
    CONSTRAINT PK_CreditLedger PRIMARY KEY CLUSTERED (id),
    CONSTRAINT FK_CreditLedger_Users FOREIGN KEY (user_id) REFERENCES dbo.Users(id) ON DELETE NO ACTION
);
CREATE NONCLUSTERED INDEX IX_CreditLedger_user_created ON dbo.CreditLedger (user_id, created_at);
```

#### 12. Bảng `dbo.SupportTickets` (Khiếu nại kỹ thuật & giám sát SLA 2 ngày)
```sql
CREATE TABLE dbo.SupportTickets (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_SupportTickets_id DEFAULT NEWSEQUENTIALID(),
    ticket_code NVARCHAR(64) NOT NULL, -- TCK-YYYYMMDD-XXXX
    user_id UNIQUEIDENTIFIER NOT NULL,
    report_id UNIQUEIDENTIFIER NOT NULL,
    assigned_to_user_id UNIQUEIDENTIFIER NULL, -- Role SupportSpecialist (phân công bởi Lead)
    status NVARCHAR(32) NOT NULL, -- 'Open', 'Assigned', 'PendingLeadApproval', 'Resolved', 'Rejected'
    issue_description NVARCHAR(MAX) NOT NULL,
    sla_due_at DATETIMEOFFSET NOT NULL, -- created_at + 2 ngày làm việc
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_SupportTickets_created_at DEFAULT SYSDATETIMEOFFSET(),
    resolved_at DATETIMEOFFSET NULL,
    CONSTRAINT PK_SupportTickets PRIMARY KEY CLUSTERED (id),
    CONSTRAINT UQ_SupportTickets_code UNIQUE NONCLUSTERED (ticket_code),
    CONSTRAINT FK_SupportTickets_Users FOREIGN KEY (user_id) REFERENCES dbo.Users(id) ON DELETE NO ACTION,
    CONSTRAINT FK_SupportTickets_Reports FOREIGN KEY (report_id) REFERENCES dbo.Reports(id) ON DELETE NO ACTION
);
CREATE NONCLUSTERED INDEX IX_SupportTickets_status_sla ON dbo.SupportTickets (status, sla_due_at);
```

#### 13. Bảng `dbo.SupportAccessGrants` (Quyền xem CV tạm thời tối đa 72h — BR07, BR22)
```sql
CREATE TABLE dbo.SupportAccessGrants (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_SupportAccessGrants_id DEFAULT NEWSEQUENTIALID(),
    ticket_id UNIQUEIDENTIFIER NOT NULL,
    cv_file_id UNIQUEIDENTIFIER NOT NULL,
    granted_by_user_id UNIQUEIDENTIFIER NOT NULL, -- Ứng viên
    granted_to_user_id UNIQUEIDENTIFIER NOT NULL, -- Hỗ trợ viên hoặc Lead được phân công
    granted_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_SupportAccessGrants_granted_at DEFAULT SYSDATETIMEOFFSET(),
    expires_at DATETIMEOFFSET NOT NULL, -- granted_at + tối đa 72 giờ
    revoked_at DATETIMEOFFSET NULL, -- Ứng viên chủ động thu hồi sớm
    CONSTRAINT PK_SupportAccessGrants PRIMARY KEY CLUSTERED (id),
    CONSTRAINT FK_SupportAccessGrants_Tickets FOREIGN KEY (ticket_id) REFERENCES dbo.SupportTickets(id) ON DELETE CASCADE,
    CONSTRAINT FK_SupportAccessGrants_CVFiles FOREIGN KEY (cv_file_id) REFERENCES dbo.CVFiles(id) ON DELETE CASCADE
);
CREATE NONCLUSTERED INDEX IX_SupportAccessGrants_lookup ON dbo.SupportAccessGrants (ticket_id, granted_to_user_id, expires_at) WHERE revoked_at IS NULL;
```

#### 14. Bảng `dbo.CompensationRequests` (Phiếu đề nghị bồi thường lỗi kỹ thuật — BR37)
```sql
CREATE TABLE dbo.CompensationRequests (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_CompensationRequests_id DEFAULT NEWSEQUENTIALID(),
    ticket_id UNIQUEIDENTIFIER NOT NULL,
    proposed_by_user_id UNIQUEIDENTIFIER NOT NULL, -- Role SupportSpecialist
    approved_by_user_id UNIQUEIDENTIFIER NULL, -- Role SupportLead
    compensated_credit_type NVARCHAR(16) NOT NULL, -- 'Free', 'Paid'
    credit_amount INT NOT NULL CONSTRAINT DF_CompensationRequests_amount DEFAULT 1,
    technical_root_cause NVARCHAR(MAX) NOT NULL,
    status NVARCHAR(32) NOT NULL, -- 'PendingApproval', 'Approved', 'Rejected'
    decision_notes NVARCHAR(512) NULL,
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_CompensationRequests_created_at DEFAULT SYSDATETIMEOFFSET(),
    decided_at DATETIMEOFFSET NULL,
    CONSTRAINT PK_CompensationRequests PRIMARY KEY CLUSTERED (id),
    CONSTRAINT UQ_CompensationRequests_ticket UNIQUE NONCLUSTERED (ticket_id),
    CONSTRAINT FK_CompensationRequests_Tickets FOREIGN KEY (ticket_id) REFERENCES dbo.SupportTickets(id) ON DELETE CASCADE
);
```

#### 15. Bảng `dbo.AuditLogs` (Nhật ký bảo mật bất biến — US20)
```sql
CREATE TABLE dbo.AuditLogs (
    id BIGINT IDENTITY(1,1) NOT NULL,
    actor_user_id UNIQUEIDENTIFIER NOT NULL,
    actor_role NVARCHAR(32) NOT NULL,
    action NVARCHAR(64) NOT NULL, -- 'ViewCVContent', 'GrantRole', 'ApproveCompensation', 'ReconcilePayment', 'CloseAccount'
    target_entity NVARCHAR(64) NOT NULL,
    target_entity_id NVARCHAR(64) NOT NULL,
    ip_address NVARCHAR(45) NOT NULL,
    details_json NVARCHAR(MAX) NULL,
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_AuditLogs_created_at DEFAULT SYSDATETIMEOFFSET(),
    CONSTRAINT PK_AuditLogs PRIMARY KEY CLUSTERED (id)
);
CREATE NONCLUSTERED INDEX IX_AuditLogs_actor_created ON dbo.AuditLogs (actor_user_id, created_at);
```

---

## 3. Kiến trúc Phần mềm & Cấu trúc Dự án (Clean Architecture)

```text
src/
├── CVAnalysis.Domain/                    # Lõi Domain, Bất biến & Quy tắc nghiệp vụ
│   ├── Entities/                         # User, CVFile, AnalysisRequest, Report, UserBalance...
│   ├── ValueObjects/                     # ScoreBreakdown, TimeWindow, CreditAmount...
│   ├── Enums/                            # UserRole, RequestStatus, OrderStatus...
│   ├── Exceptions/                       # DomainValidationException, InsufficientCreditsException...
│   └── Invariants/                       # RubricScoreCalculator, WeightNormalizer (Mục 5 brief)
│
├── CVAnalysis.Application/               # CQRS Commands & Queries, DTOs & Use Cases
│   ├── Common/Interfaces/                # IDbContext, IAiClient, IPaymentGateway, IPdfParser
│   ├── Common/Behaviors/                 # ValidationBehavior, TransactionBehavior, AuditLogBehavior
│   ├── Features/Auth/                    # Register, VerifyEmail, Login
│   ├── Features/CVFiles/                 # UploadCV, ExtractCVText, ConfirmExtraction
│   ├── Features/Analysis/                # StartAnalysisCommand (Atomic Hold), ProcessAiAnalysis
│   ├── Features/Optimization/            # GetBulletPointsQuery, CustomizeBulletPointCommand
│   ├── Features/Billing/                 # CreateOrderCommand, ProcessPaymentWebhookCommand
│   └── Features/Operations/              # ReconcileOrder (Finance), ApproveCompensation (Lead)
│
├── CVAnalysis.Infrastructure/            # Tầng tích hợp kỹ thuật & Third-party
│   ├── Persistence/                      # ApplicationDbContext, EF Core Configurations, Migrations
│   ├── Parsers/                          # PdfPigExtractor, OpenXmlDocxExtractor
│   ├── AiIntegration/                    # CommercialAiClient (OpenAI/Anthropic API, PII Sanitizer)
│   ├── PaymentGateway/                   # VietQrPaymentGateway, WebhookSignatureVerifier
│   └── BackgroundServices/               # AnalysisTimeoutWorker (90s Circuit Breaker), PurgeWorker
│
├── CVAnalysis.WebApi/                    # REST API Controllers & Middleware
│   ├── Controllers/                      # AuthController, AnalysisController, BillingController...
│   ├── Middleware/                       # ExceptionHandlingMiddleware, IdempotencyMiddleware
│   ├── Authorization/                    # ZeroTrustCvAccessHandler (kiểm tra 72h grant)
│   └── Program.cs                        # DI Registration, Health Checks, Swagger/OpenAPI
│
└── cvanalysis-client/                    # React (Vite + TypeScript + Tailwind CSS)
    ├── src/components/                   # Dropzone, ScoreCard, DiffViewer, QrModal
    ├── src/features/candidate/           # Upload, ReportView, OptimizationView, HistoryView
    ├── src/features/operations/          # SupportView (SCR11), LeadView (SCR12), FinanceView (SCR13), AdminView (SCR14)
    └── src/services/                     # Axios API clients, AuthContext, React Query Hooks
```

---

## 4. Đặc tả Hợp đồng API (API Contracts)

### Nhóm 1: Xác thực & Quản trị tài khoản (`/api/v1/auth`, `/api/v1/account`)
- `POST /api/v1/auth/register` — Đăng ký tài khoản (US01)
  - *Request:* `{ "email": "string", "password": "string" }`
  - *Response (200):* `{ "message": "Email xác minh đã được gửi." }`
- `POST /api/v1/auth/verify-email` — Kích hoạt tài khoản và nhận 3 lượt cơ bản (US01)
  - *Request:* `{ "token": "string" }`
  - *Response (200):* `{ "token": "jwt_string", "freeCredits": 3 }`
- `POST /api/v1/account/close` — Đóng tài khoản vĩnh viễn (US03)
  - *Headers:* `Authorization: Bearer <token>`
  - *Request:* `{ "password": "string", "confirmForfeitBalance": true }`
  - *Response (200):* `{ "message": "Tài khoản và toàn bộ dữ liệu CV/báo cáo đã được xóa vĩnh viễn." }`

### Nhóm 2: CV & Trích xuất nội dung (`/api/v1/cv`)
- `POST /api/v1/cv/upload` — Tải lên file CV (US04)
  - *Request:* `multipart/form-data` (file $\le 10$MB, $\le 5$ trang)
  - *Response (200):* `{ "cvFileId": "guid", "fileName": "string", "extractedData": { ... } }`
- `PUT /api/v1/cv/{cvFileId}/confirm` — Chỉnh sửa & Xác nhận nội dung trích xuất (US05)
  - *Request:* `{ "confirmedData": { "contact": {...}, "skills": [...], "experience": [...] } }`
  - *Response (200):* `{ "message": "Đã lưu nội dung xác nhận làm căn cứ phân tích." }`

### Nhóm 3: Phân tích & Báo cáo kết quả (`/api/v1/analysis`, `/api/v1/reports`)
- `POST /api/v1/analysis/start` — Khởi động phân tích (US07, US08, US14)
  - *Request:* `{ "cvFileId": "guid", "jobDescriptionText": "string" (optional), "consentExternalAi": true }`
  - *Processing:* Khóa nguyên tử `UPDLOCK` trên `dbo.UserBalances`, giữ 1 credit, đặt hạn 90s.
  - *Response (202 Accepted):* `{ "requestId": "guid", "status": "Queued", "timeoutSeconds": 90 }`
- `GET /api/v1/analysis/{requestId}/status` — Kiểm tra tiến trình xử lý (Polling / SSE)
  - *Response (200):* `{ "status": "Processing" | "Completed" | "TimedOut", "reportId": "guid" (khi xong) }`
- `GET /api/v1/reports/{reportId}` — Mở báo cáo điểm ATS (US09)
  - *Response (200):* `{ "overallScore": 82, "scoreLabel": "Khá", "normalizedDenominator": 85, "pillars": { ... }, "mandatoryWarnings": [ ... ] }`

### Nhóm 4: Tối ưu bản viết lại & Xuất file (`/api/v1/optimization`)
- `GET /api/v1/reports/{reportId}/bullets` — Lấy danh sách gạch đầu dòng tối ưu (US10, US11)
  - *Response (200):* `[ { "id": "guid", "originalText": "...", "suggestedText": "...", "status": "Pending" } ]`
- `PUT /api/v1/optimization/bullets/{id}` — Cập nhật trạng thái hoặc sửa thủ công (US11)
  - *Request:* `{ "status": "Accepted" | "Rejected", "customizedText": "..." }`
- `POST /api/v1/reports/{reportId}/export` — Xuất file CV tối ưu (US12)
  - *Request:* `{ "format": "PDF" | "DOCX" }`
  - *Response (200):* Stream file binary (`application/pdf` hoặc `application/vnd.openxmlformats-officedocument.wordprocessingml.document`)

### Nhóm 5: Thanh toán & Quản lý Credit (`/api/v1/billing`)
- `GET /api/v1/billing/packages` — Lấy danh mục gói credit đang bán (US13)
- `POST /api/v1/billing/orders` — Tạo đơn mua gói (US13)
  - *Request:* `{ "packageId": "guid" }`
  - *Response (200):* `{ "orderCode": "ORD-...", "amountVnd": 100000, "qrUrl": "...", "expiresAt": "datetime" (15m) }`
- `POST /api/v1/webhooks/payment` — Webhook cổng thanh toán (Xác thực Single Source of Truth)
  - *Headers:* `X-Signature: <HMAC-SHA256>`
  - *Processing:* Kiểm tra chữ ký, kiểm tra Idempotency, cộng credit đúng gói.

### Nhóm 6: Phân hệ Quản trị 4 Vai trò (`/api/v1/operations`)
- `POST /api/v1/operations/finance/reconcile-order` — Kế toán duyệt cộng credit đơn muộn (US15, Role Finance)
  - *Request:* `{ "orderId": "guid", "notes": "Đã khớp giao dịch ngân hàng" }`
- `POST /api/v1/operations/support/create-compensation-proposal` — Hỗ trợ viên lập đề xuất bù (US19, Role SupportSpecialist)
  - *Request:* `{ "ticketId": "guid", "rootCause": "Lỗi font bảng PDF trích xuất thiếu từ khóa" }`
- `POST /api/v1/operations/support/approve-compensation` — Lead hỗ trợ duyệt bồi thường (US19, Role SupportLead)
  - *Request:* `{ "compensationRequestId": "guid", "decision": "Approved" }`
- `POST /api/v1/operations/admin/assign-role` — Admin gán vai trò người dùng nội bộ (US20, Role SystemAdmin)
  - *Request:* `{ "userId": "guid", "assignedRole": "SupportSpecialist" | "SupportLead" | "Finance" | "SystemAdmin" }`

---

## 5. Xử lý Tranh chấp Đồng thời & Bất biến Domain (Concurrency Strategy)

### 1. Khóa Giữ/Thu Credit Nguyên tử (Atomic Balance Hold)
Để ngăn chặn race condition khi người dùng mở 2 tab trình duyệt và bấm chạy cùng lúc khi chỉ còn 1 credit:
```csharp
// Trong Application Service / Command Handler:
using var transaction = await _dbContext.Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);

// Sử dụng câu lệnh RAW SQL với gợi ý khóa UPDLOCK, ROWLOCK để chặn truy vấn cạnh tranh
var balance = await _dbContext.UserBalances
    .FromSqlInterpolated($"SELECT * FROM dbo.UserBalances WITH (UPDLOCK, ROWLOCK) WHERE user_id = {userId}")
    .SingleOrDefaultAsync();

int totalAvailable = balance.FreeTrialCredits + balance.PaidCredits;
if (totalAvailable <= 0)
{
    throw new InsufficientCreditsException("Số dư khả dụng không đủ để bắt đầu lượt phân tích.");
}

// Giữ 1 credit
if (balance.FreeTrialCredits > 0)
{
    balance.FreeTrialCredits -= 1;
}
else
{
    balance.PaidCredits -= 1;
}
balance.HeldCredits += 1;

await _dbContext.SaveChangesAsync();
await transaction.CommitAsync();
```

### 2. Idempotency Middleware cho Webhook Cổng thanh toán
- Khóa phân tán hoặc bản ghi kiểm tra trùng lặp trên bảng `dbo.PaymentOrders`.
- Nếu webhook cổng thanh toán gửi lại (retry) giao dịch đã ở trạng thái `Completed` hoặc `AwaitingReconciliation`: Trả về HTTP 200 ngay lập tức, không thực thi lại logic cộng credit.

### 3. Circuit Breaker & Worker kiểm tra Timeout 90 giây
- Một `BackgroundService` chạy chu kỳ 5 giây quét các `dbo.AnalysisRequests` có trạng thái `Queued` hoặc `Processing` mà `SYSDATETIMEOFFSET() > timeout_at`:
  1. Cập nhật trạng thái tác vụ thành `TimedOut`.
  2. Giải phóng `held_credits` trong `dbo.UserBalances` về lại nguồn credit tương ứng (hoàn trả quyền sử dụng).
  3. Ghi log kiểm toán vào `dbo.CreditLedger`.

---

## 6. Các Quyết định Kiến trúc Bổ sung (ADRs)

### ADR-002: Kiểm soát Tranh chấp Credit bằng SQL Server Transactional Locking (UPDLOCK) thay vì Distributed Lock
- **Bối cảnh:** Ở quy mô hệ thống vừa (SME / Startup), việc bổ sung hạ tầng Redis Distributed Lock (Redlock) làm tăng chi phí vận hành và rủi ro phân mảnh trạng thái khi node Redis mất kết nối.
- **Quyết định:** Sử dụng trực tiếp tính năng Transactional Locking với `UPDLOCK, ROWLOCK` của SQL Server trên bảng `dbo.UserBalances`.
- **Hệ quả:** Đảm bảo tính nhất quán tuyệt đối (Strict Serializability) cho số dư credit, đơn giản hóa hạ tầng triển khai mà vẫn chịu tải tốt ở quy mô hàng nghìn giao dịch mỗi ngày.

### ADR-003: Phân quyền Zero-Trust đối với Nội dung CV thông qua SupportAccessGrants
- **Bối cảnh:** Để bảo vệ quyền riêng tư cá nhân của ứng viên theo BR07/BR22, không có bất kỳ nhân viên nội bộ nào (kể cả System Administrator) được cấp quyền đọc mặc định nội dung CV hoặc báo cáo của ứng viên.
- **Quyết định:** Áp dụng Custom Authorization Requirement trong ASP.NET Core (`CvAccessAuthorizationHandler`):
  - Chỉ cho phép truy cập file CV / Báo cáo khi có bản ghi hợp lệ trong bảng `dbo.SupportAccessGrants` khớp với `UserId` của nhân viên, `TicketId` đang mở, và thời gian hiện tại nằm trong hạn 72 giờ (`expires_at > SYSDATETIMEOFFSET()` và `revoked_at IS NULL`).
  - Mọi lượt truy cập đều tự động ghi một bản ghi bất biến vào `dbo.AuditLogs`.

---

## 7. Bàn giao sang Cổng G4 (Delivery Planning)

Bản thiết kế kỹ thuật này đã giải quyết toàn bộ các yêu cầu kỹ thuật và bàn giao sẵn sàng cho Cổng G4:
1. **Kiến trúc:** ASP.NET Core Web API + React (Vite) + SQL Server.
2. **Database:** 15 bảng DDL chi tiết, chuẩn hóa kiểu dữ liệu và ràng buộc toàn vẹn.
3. **Sơ đồ ERD:** Đã kiểm thử và render trực quan tại `docs/workflow/diagrams/cv-analysis-erd.html`.
4. **Hợp đồng API:** 20 User Stories đều có endpoint, DTO request/response và cơ chế bảo mật tương ứng.
5. **Kế hoạch G4:** Phân rã cấu trúc thư mục tasks theo Clean Architecture và lập lộ trình thực thi song song.
