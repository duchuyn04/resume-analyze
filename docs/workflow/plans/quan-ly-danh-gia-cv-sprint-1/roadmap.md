# Lộ trình Thực thi Quản lý đánh giá CV + sprint 1 (Delivery Planning)

| Thuộc tính | Giá trị |
|---|---|
| Artifact ID | ROADMAP-CV-001 |
| Revision | Draft 1; ngày 2026-09-22 |
| Chế độ | Cổng G4 — Lập kế hoạch thực thi, phân rã tasks & phân phối luồng song song |
| Sprint Scope | **Quản lý đánh giá CV + sprint 1** (Core Walking Skeleton & 5 Roles MVP) |
| Quy mô nhóm khảo sát | **2 người** (Dev 1: Backend & Database, Dev 2: Frontend & UI/UX) |
| Nguồn kiến trúc G3 | `docs/workflow/architecture/cv-analysis-design.md` (Approved 2026-09-22) |
| Nguồn User Stories G2 | `docs/workflow/specs/cv-analysis-stories.md` (Approved 2026-09-22, 20 User Stories, 61 ACs) |
| Nguồn Nghiệp vụ G1 | `docs/workflow/specs/cv-analysis-brief.md` (Approved 2026-09-22, Draft 6) |
| Trạng thái G4 | `approved`: Người dùng đã phê duyệt chính thức ngày 2026-09-22; sẵn sàng chuyển giao cho Cổng task-execution |

---

## 1. Mục tiêu & Phạm vi Sprint 1 (Sprint Goal & Walking Skeleton)

### Sprint Goal
> Xây dựng và nghiệm thu thành công luồng giá trị cốt lõi (Walking Skeleton): Ứng viên đăng ký/nhận 3 lượt $\rightarrow$ Tải CV (PDF/DOCX) $\rightarrow$ Đối chiếu JD theo Rubric 4 Trụ cột $\rightarrow$ Tối ưu bản viết lại với Diff view $\rightarrow$ Xuất file PDF/DOCX; kết hợp quản lý giao dịch nạp credit bằng khóa nguyên tử và phân quyền 5 roles theo nguyên tắc Zero-Trust.

### Phạm vi bao phủ
- Toàn bộ 20 User Stories (`US01` – `US20`) và 61 Acceptance Criteria đã được chuẩn hóa tại Cổng G2.
- 15 Bảng cơ sở dữ liệu SQL Server và hệ thống khóa nguyên tử `UPDLOCK, ROWLOCK` cho bất biến credit (ADR-002).
- Circuit Breaker 90s và lưu trữ kết quả dở dang 24h (BR04, BR17).
- Phân hệ vận hành 4 vai trò nội bộ (Hỗ trợ viên, Lead hỗ trợ, Kế toán, Quản trị viên).

---

## 2. Cấu trúc Thư mục Tasks phân bổ theo 2 Thành viên

```text
docs/workflow/plans/quan-ly-danh-gia-cv-sprint-1/
├── roadmap.md                                      # Lộ trình tổng thể, ma trận ràng buộc & điểm hội tụ
├── backend/                                        # Phân công: Dev 1 (ASP.NET Core Web API, EF Core, SQL Server)
│   ├── task-01-database-efcore.md                  # Khởi tạo Solution, 15 bảng DDL & Migrations
│   ├── task-02-auth-rbac-service.md                # JWT Bearer, 5 Roles, Zero-Trust Handler & Email Hash
│   ├── task-03-cv-parsers-storage.md               # PdfPig / OpenXml parsers & CV Upload Service
│   ├── task-04-analysis-rubric-engine.md           # Lõi tính điểm Rubric 4 Trụ cột (mẫu số 85, sàn 0)
│   ├── task-05-ai-integration-timeout.md           # External AI Client (PII reduction) & Worker 90s
│   ├── task-06-billing-credit-concurrency.md       # Khóa nguyên tử UPDLOCK, Webhook VietQR & Đối soát 2 ngày
│   └── task-07-support-operations-api.md           # Phân hệ API 4 vai trò: Hỗ trợ, Lead, Kế toán, Admin
├── frontend/                                       # Phân công: Dev 2 (React Vite, TypeScript, Tailwind CSS)
│   ├── task-08-react-scaffold-layout.md            # Khởi tạo React Vite, App Shell, 5-Role Switcher & Nav
│   ├── task-09-cv-upload-extraction-ui.md          # Dropzone Upload, Validate 10MB/5 trang, Form sửa trích xuất
│   ├── task-10-jd-input-validation-ui.md           # Textarea đếm từ 50-2500, Modal đồng ý xử lý AI (US02)
│   ├── task-11-ats-report-breakdown-ui.md          # Bảng điểm ATS, Accordion 4 trụ cột, Cảnh báo bắt buộc BR18
│   ├── task-12-optimization-diff-view-ui.md        # Side-by-side Diff view (Action-Context-Metric), Export triggers
│   ├── task-13-billing-qr-checkout-ui.md           # Bảng gói credit cố định, Modal VietQR đếm ngược 15 phút
│   └── task-14-operations-portal-views.md          # 4 Màn hình quản trị nội bộ (SCR11, SCR12, SCR13, SCR14)
└── qa/                                             # Phân công: QA & Tích hợp (Cả 2 thành viên)
    ├── task-15-api-client-mock-switch.md           # Axios API Client, TanStack Query hooks & Mock switch
    └── task-16-e2e-verification-deploy.md          # Tích hợp toàn diện, kiểm thử E2E Walking Skeleton
```

---

## 3. Ma trận Ràng buộc giữa các Tasks (Task Dependency Matrix)

| Task ID | Tiêu đề Task | Phân bổ / Role | Hard Prerequisites (Phải xong trước) | Blocking (Chặn các tasks sau) | Shared-Write Areas (Vùng code chung) |
|:---:|---|---|---|---|---|
| `TASK-01` | Khởi tạo Solution & EF Core Migrations | `backend/` (Dev 1) | Không (Bắt đầu tại T0) | Chặn `TASK-02`, `TASK-03`, `TASK-04` | `src/CVAnalysis.Domain/`, `src/CVAnalysis.Infrastructure/Persistence/` |
| `TASK-08` | Scaffold React Vite & App Shell Layout | `frontend/` (Dev 2) | Không (Bắt đầu tại T0) | Chặn `TASK-09`, `TASK-10` | `cvanalysis-client/src/` |
| `TASK-15` | Axios Client & Mock Contracts Layer | `qa/` (Dev 2) | Không (Kế thừa API Contract G3) | Chặn `TASK-09` đến `TASK-14` | `cvanalysis-client/src/services/api/` |
| `TASK-02` | Auth, RBAC 5 Roles & Zero-Trust Handler | `backend/` (Dev 1) | Cần `TASK-01` | Chặn `TASK-06`, `TASK-07` | `src/CVAnalysis.WebApi/Controllers/Auth/` |
| `TASK-03` | Parser PDF/DOCX & Upload Service | `backend/` (Dev 1) | Cần `TASK-01` | Chặn `TASK-04`, `TASK-05` | `src/CVAnalysis.Infrastructure/Parsers/` |
| `TASK-09` | Dropzone Upload & Extraction Form UI | `frontend/` (Dev 2) | Cần `TASK-08`, `TASK-15` | Chặn `TASK-16` | `cvanalysis-client/src/features/upload/` |
| `TASK-10` | JD Validation UI & Modal Consent AI | `frontend/` (Dev 2) | Cần `TASK-08`, `TASK-15` | Chặn `TASK-16` | `cvanalysis-client/src/features/jd/` |
| `TASK-04` | Lõi Tính điểm Rubric 4 Trụ cột Engine | `backend/` (Dev 1) | Cần `TASK-01`, `TASK-03` | Chặn `TASK-05` | `src/CVAnalysis.Domain/Invariants/` |
| `TASK-05` | AI Integration & Worker Timeout 90s | `backend/` (Dev 1) | Cần `TASK-03`, `TASK-04` | Chặn `TASK-06`, `TASK-16` | `src/CVAnalysis.Infrastructure/Background/` |
| `TASK-11` | ATS Report Banner & Accordion Breakdown | `frontend/` (Dev 2) | Cần `TASK-08`, `TASK-15` | Chặn `TASK-12` | `cvanalysis-client/src/features/report/` |
| `TASK-12` | Diff View Action-Context-Metric & Export | `frontend/` (Dev 2) | Cần `TASK-11` | Chặn `TASK-16` | `cvanalysis-client/src/features/diff/` |
| `TASK-06` | Atomic Balance Hold & Billing Webhook | `backend/` (Dev 1) | Cần `TASK-01`, `TASK-02`, `TASK-05` | Chặn `TASK-07`, `TASK-16` | `src/CVAnalysis.Application/Features/Billing/` |
| `TASK-13` | Credit Packages & VietQR Checkout Modal | `frontend/` (Dev 2) | Cần `TASK-08`, `TASK-15` | Chặn `TASK-16` | `cvanalysis-client/src/features/billing/` |
| `TASK-07` | Support, Reconciliation & Admin APIs | `backend/` (Dev 1) | Cần `TASK-02`, `TASK-06` | Chặn `TASK-16` | `src/CVAnalysis.WebApi/Controllers/Ops/` |
| `TASK-14` | 4 Giao diện Quản trị Nội bộ (SCR11–14) | `frontend/` (Dev 2) | Cần `TASK-08`, `TASK-15` | Chặn `TASK-16` | `cvanalysis-client/src/features/operations/` |
| `TASK-16` | E2E Integration & Handoff Inspection | `qa/` (Cả 2) | Cần tất cả tasks 01–15 xong | Không (Hoàn tất Sprint 1) | Toàn bộ repo & CI/CD |

---

## 4. Phân phối 2 Luồng Thực thi Song song (Parallel Tracks)

```text
THỜI GIAN       TRACK 1: DEV 1 (BACKEND & DB)                  TRACK 2: DEV 2 (FRONTEND & CLIENT)
─────────       ──────────────────────────────                  ──────────────────────────────────
T0 (Bắt đầu)    TASK-01: Solution & EF Core Migrations         TASK-08: React Vite Shell & Navigation
                (15 tables DDL, Constraints, Indexes)          TASK-15: Mock API Layer từ G3 Contracts
                                  │                                              │
T1 (Xong T0)    TASK-02: Auth & 5 Roles RBAC                   TASK-09: Upload & Extraction Form UI
                TASK-03: PDF/DOCX Parser Service               TASK-10: JD Validation & Consent UI
                                  │                                              │
               ┌──────────────────┴──────────────────────────────────────────────┴─────────────────┐
               │ SYNC CHECKPOINT 1: Ghép nối API Auth, Upload file CV & Trích xuất dữ liệu thật    │
               └──────────────────┬──────────────────────────────────────────────┬─────────────────┘
                                  │                                              │
T2              TASK-04: Rubric Engine 4 Trụ cột               TASK-11: Báo cáo điểm ATS & Accordion
                TASK-05: AI Client & Worker 90s                TASK-12: Diff View & Export File UI
                                  │                                              │
T3              TASK-06: Atomic Hold & VietQR Webhook          TASK-13: Gói Credit & VietQR Modal
                TASK-07: Operations APIs (Support/Finance/Admin)TASK-14: 4 Màn hình Vận trị (SCR11–14)
                                  │                                              │
               ┌──────────────────┴──────────────────────────────────────────────┴─────────────────┐
               │ SYNC CHECKPOINT 2: Ghép nối luồng Chạy AI 90s, Nạp credit & Cổng Vận hành 5 roles  │
               └──────────────────┬──────────────────────────────────────────────┬─────────────────┘
                                  │                                              │
T4 (Hội tụ)     ──────────────────┴────── TASK-16: E2E VERIFICATION ──────────────┴───────────────────
                Chạy toàn diện Walking Skeleton: Đăng ký -> Upload -> Chạy AI 90s -> Diff -> Xuất file.
```

---

## 5. Quy tắc Kiểm chứng & Định nghĩa Hoàn thành (DoD)

Mỗi task card chỉ được đánh dấu hoàn thành khi đáp ứng trọn vẹn Definition of Done:
1. **Mã nguồn sạch (Clean Code):** Tuân thủ quy chuẩn Clean Architecture trong ASP.NET Core và React TypeScript.
2. **Kiểm thử độc lập:** Mỗi task card có lệnh kiểm chứng tương ứng (Unit test cho Rubric Calculator, Integration test cho Atomic Hold, Browser test cho UI).
3. **Bảo toàn bất biến:**
   - Số dư credit không bao giờ âm; giữ credit độc lập, chỉ thu khi đủ bộ 3 thành phần.
   - Timeout 90s tự động ngắt và trả credit đang giữ.
   - Quyền xem CV của nhân viên hỗ trợ tự động hết hiệu lực sau 72h.
4. **Hội tụ Checkpoint an toàn:** Không đẩy mã nguồn làm vỡ build của thành viên còn lại tại các điểm Sync Checkpoints.
