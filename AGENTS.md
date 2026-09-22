# AGENTS.md — Quy Chuẩn Kỹ Thuật Dự Án (Engineering Standards & Coding Guidelines)

> **Tệp giao ước kỹ thuật dành cho AI Agents (Oh My Pi / Cline / Cursor) và Lập trình viên dự án CVAnalysis.**  
> **Phiên bản:** 2.0.0 (Context-Optimized) &bull; **Ngày ban hành:** 2026-09-22  
> **Nguyên tắc tối thượng:** Mật độ thông tin cao, chỉ dẫn có thể thực thi ngay (Actionable), cấm vi phạm các bất biến kỹ thuật.

---

## 1. Tổng quan Dự án & Tech Stack

| Thành phần | Công nghệ & Thư viện | Phiên bản & Môi trường |
|---|---|---|
| **Backend** | ASP.NET Core Web API, EF Core, SQL Server | .NET 9 / 8 (C# 12+) |
| **Kiến trúc BE** | Clean Architecture (Domain $\rightarrow$ Application $\rightarrow$ Infrastructure $\rightarrow$ WebApi) | MediatR, FluentValidation |
| **Frontend** | React, TypeScript, Vite, Tailwind CSS | React 19, TS 5.x |
| **Kiến trúc FE** | Feature-First Architecture | TanStack Query v5, Zustand, Axios |
| **Quản lý Tài liệu** | Docs-First trong `docs/workflow/` | Specs (G1/G2), Architecture (G3), Plans (G4) |

---

## 2. Bản đồ Thư mục & Đường dẫn Trọng yếu

```text
CVAnalysis/
├── docs/workflow/                     # [Chân lý tài liệu] Specs, Architecture, Backlog, Plans
│   ├── specs/                         # G1 Business Brief & G2 User Stories
│   ├── architecture/                  # G3 Architecture Design, 15 bảng DDL, ADRs, Contracts
│   ├── diagrams/                      # Sơ đồ HTML/SVG (ERD, Architecture, Flows)
│   └── plans/                         # G4 Lộ trình Sprint, task cards phân theo vai trò
├── backend/                           # Solution Backend ASP.NET Core
│   ├── CVAnalysis.sln                 # Solution file chính
│   ├── src/CVAnalysis.Domain/         # [Lõi] Entities, ValueObjects, Invariants, Enums (KHÔNG phụ thuộc ngoài)
│   ├── src/CVAnalysis.Application/    # [Use Cases] CQRS Commands/Queries, Interfaces, DTOs, Behaviors
│   ├── src/CVAnalysis.Infrastructure/ # [Kỹ thuật] EF Core DbContext, Configurations, Migrations, Parsers, Workers
│   ├── src/CVAnalysis.WebApi/         # [Entry Point] Controllers, Middlewares, Auth Handlers, Program.cs
│   └── tests/                         # UnitTests (Domain/App), IntegrationTests (Infra), FunctionalTests (API)
└── frontend/cvanalysis-client/        # Ứng dụng Frontend React Vite
    └── src/
        ├── components/common/         # UI nguyên tử dùng chung (Button, Modal, Input, Badge, Dropdown)
        ├── components/layout/         # AppHeader, CandidateNav, OperationsSidebar, AppShell
        ├── features/<feature-name>/   # Module nghiệp vụ độc lập: components, hooks, services, types
        │   ├── auth/ | cv-upload/ | analysis/ | optimization/ | billing/ | operations/
        ├── hooks/ | services/ | store/ # Hooks dùng chung, Axios API Client, Zustand stores toàn cục
        └── utils/ | types/            # Pure utilities (format-currency, cn), Ambient TypeScript types
```

---

## 3. Lệnh Thực thi Thiết yếu (Essential CLI Commands)

Mọi thay đổi mã nguồn **bắt buộc** phải được kiểm chứng bằng các lệnh CLI sau trước khi báo cáo hoàn thành:

```bash
# === BACKEND (.NET) ===
dotnet build backend/CVAnalysis.sln                       # Biên dịch toàn bộ Solution (phải exit code 0, 0 warning nghiêm trọng)
dotnet test backend/CVAnalysis.sln                        # Chạy toàn bộ Unit & Integration test suites

# EF Core Migrations (Chạy từ thư mục gốc repo hoặc backend/):
dotnet ef migrations add <MigrationName> --project backend/src/CVAnalysis.Infrastructure --startup-project backend/src/CVAnalysis.WebApi
dotnet ef database update --project backend/src/CVAnalysis.Infrastructure --startup-project backend/src/CVAnalysis.WebApi
dotnet ef migrations list --project backend/src/CVAnalysis.Infrastructure --startup-project backend/src/CVAnalysis.WebApi

# === FRONTEND (React) ===
cd frontend/cvanalysis-client
npm install                                              # Cài đặt dependencies khi package.json thay đổi
npm run build                                            # Kiểm tra TypeScript type-check (tsc) & đóng gói Vite (0 type errors)
npm run lint                                             # Kiểm tra quy chuẩn linter
npm run dev                                              # Chạy development server cục bộ (mặc định port 5173)
```

---

## 4. Điều Cấm Tuyệt Đối (Non-Negotiable Invariants)

AI Agents và Lập trình viên **tuyệt đối không được vi phạm** các quy tắc sau:

1. **CẤM sửa đổi Migration cũ:** Các tệp migration đã commit vào nhánh chính không được sửa tay. Muốn đổi schema bắt buộc tạo migration mới.
2. **CẤM phá vỡ Dependency Inversion:** `CVAnalysis.Domain` tuyệt đối không được tham chiếu bất kỳ thư viện ngoài, EF Core packages, hay project nào khác. Phụ thuộc luôn hướng vào trong.
3. **CẤM nới lỏng kiểu dữ liệu (Strict Typing):**
   - C#: Bắt buộc bật `<Nullable>enable</Nullable>`. Cấm dùng `dynamic`, `object` lỏng lẻo hoặc dùng toán tử `!` để dập tắt cảnh báo null.
   - TypeScript: Bắt buộc cấu hình `strict: true`. Cấm dùng kiểu `any` (thay bằng `unknown` hoặc generic có ràng buộc).
4. **CẤM lưu trữ Secret trong mã nguồn:** Cấm hardcode JWT secret, API keys, Connection strings trong code hoặc git repo. Sử dụng Environment Variables hoặc `appsettings.Development.json` (được ignore).
5. **CẤM thao tác số dư ngoài giao dịch khóa nguyên tử (Atomic Balance Hold):** Trừ hoặc hoàn trả credit bắt buộc dùng khóa `UPDLOCK, ROWLOCK` tại tầng Database để bảo vệ bất biến `UserBalance >= 0`, triệt tiêu hoàn toàn race condition (ADR-002).
6. **CẤM commit mã nguồn chết (Dead Code) hoặc bình luận rác:** Không để lại code cũ bị comment, file tạm debug, hoặc các comment không mang giá trị kỹ thuật.

---

## 5. Quy chuẩn Đặt tên & Cú pháp (Naming & Conventions)

### 5.1. Bảng quy chuẩn Đặt tên C# / .NET & TypeScript / React

| Đối tượng | Quy ước | Ví dụ chuẩn (Do) | Ví dụ sai phạm (Don't) |
|---|---|---|---|
| **C# Class / Record / Struct** | `PascalCase` | `UserBalance`, `ScoreBreakdown` | `user_balance`, `TblUser` |
| **C# Interface** | `IPascalCase` | `IApplicationDbContext`, `IAiClient` | `ApplicationDbContextInterface` |
| **C# Async Method** | `...Async` | `GetReportByIdAsync(...)` | `GetReportById(...)` |
| **C# Private Field** | `_camelCase` | `private readonly ILogger _logger;` | `m_logger`, `logger` |
| **C# Static Private Field** | `s_camelCase`| `private static readonly Lock s_lock;` | `s_Lock`, `lockObj` |
| **C# Boolean Property** | `Is/Has/Can...` | `IsEmailVerified`, `HasCredits` | `EmailVerified`, `Flag` |
| **C# Enum & Enum Member** | `PascalCase` | `UserRole.Candidate` (không suffix Enum)| `UserRoleEnum`, `ROLE_CANDIDATE` |
| **TS/React Component** | `PascalCase` | `ScoreBreakdownCard.tsx` | `scoreBreakdownCard.tsx` |
| **TS Custom Hook** | `use...` | `useCvUpload()`, `useAnalysisPolling()` | `getCvUpload()`, `cvHook()` |
| **TS Constant** | `UPPER_SNAKE`| `MAX_FILE_SIZE_BYTES = 10 * 1024 * 1024;`| `maxFileSize`, `Max_Size` |
| **TS Event Prop vs Handler** | `on...` / `handle...`| `<Dropzone onDrop={handleDrop} />` | Đảo lộn `on...` cho hàm xử lý |
| **FE Directory & File Name** | `kebab-case` | `cv-upload/`, `score-card.tsx`, `use-debounce.ts` | `CvUpload/`, `scoreCard.tsx` |

*Lưu ý:* Thư mục và file Frontend luôn dùng `kebab-case` để tránh lỗi Case-Sensitivity giữa Windows và Linux CI/CD.

### 5.2. Ma trận Ánh xạ Database $\leftrightarrow$ Backend $\leftrightarrow$ Frontend

```text
[Database: SQL Server]          [Backend: C# Entity / DTO]      [REST API: JSON]           [Frontend: TS Type]
dbo.UserBalances (PascalCase)   class UserBalance (PascalCase)  { "userBalance": ... }     interface UserBalance
user_id (snake_case)            public Guid UserId              "userId" (camelCase)       userId: string
free_trial_credits              public int FreeTrialCredits     "freeTrialCredits"         freeTrialCredits: number
row_version (timestamp)         public byte[] RowVersion        "rowVersion" (Base64)      rowVersion: string
```

- **REST API Routing:** Dùng `kebab-case`, danh từ số nhiều, tiền tố `/api/v1/`:
  - `POST /api/v1/cv-files`, `PUT /api/v1/cv-files/{id}/confirm`, `POST /api/v1/analysis-requests`, `GET /api/v1/reports/{id}`.
- **EF Core Mapping:** Cấu hình Fluent API trong `Infrastructure/Persistence/Configurations/` để map C# thuộc tính sang cột DB `snake_case`. Không dùng Data Annotations bừa bãi trong Domain Entities (Persistence Ignorance).

---

## 6. Quy chuẩn Chú thích & Tài liệu hóa Mã nguồn (Commenting Protocol)

### 6.1. Triết lý cốt lõi: "Why, Not What" & Domain Invariants
- Code tự giải thích *What* qua định danh rõ ràng.
- Chú thích chỉ dành để giải thích *Why* (lý do chọn giải pháp, đánh đổi kỹ thuật) và các *Bất biến nghiệp vụ* (Domain Invariants).

### 6.2. Cú pháp Chú thích Chuẩn
- **C# Public API & Domain Services:** Dùng thẻ XML Documentation chuẩn của Microsoft:
  - `/// <summary>`: Mục đích hàm / class.
  - `/// <param name="...">`: Ý nghĩa và ràng buộc của tham số.
  - `/// <returns>`: Giá trị trả về và các trường hợp đặc biệt.
  - `/// <exception cref="...">`: Ngoại lệ có thể ném ra và điều kiện phát sinh.
  - `/// <remarks>`: Diễn giải bối cảnh nghiệp vụ, mã quy tắc (BR), hoặc cơ chế concurrency.
- **TypeScript Reusable Hooks & Utils:** Dùng chuẩn TSDoc (`tsdoc.org`):
  - `/** ... */`, `@param`, `@returns`, `@throws`, `@remarks`, `@example`.

### 6.3. 5 Thẻ Annotation Nội bộ Bắt buộc (Internal Annotation Tags)
Bắt buộc phải ghi rõ tên người phụ trách hoặc mã task/ticket, kèm nội dung hành động cụ thể:
- `// TODO(dev/ticket): [Hành động] [Lý do kỹ thuật]`
- `// FIXME(dev/ticket): [Mô tả lỗi] [Hướng xử lý triệt để]`
- `// NOTE(topic): [Bối cảnh] [Lý do chọn giải pháp kỹ thuật đặc biệt/đánh đổi]`
- `// INVARIANT(Mã-BR): [Mô tả quy tắc nghiệp vụ bất khả xâm phạm]` (Ví dụ: `// INVARIANT(BR04): Số dư UserBalance không được âm`)
- `// SECURITY(topic): [Rủi ro an ninh] [Biện pháp phòng ngừa/Sanitize]`

### 6.4. Danh mục Bình luận Rác CẤM Xuất Hiện (Noise Comments)
- Cấm bình luận lặp lại đúng tên hàm/biến (Ví dụ: `// Lấy user theo id` trước hàm `GetUserById`).
- Cấm mã nguồn cũ bị comment lại ("để dành tham khảo"). Hãy xóa thẳng tay, Git đã lưu lịch sử.
- Cấm nhật ký thay đổi trong file (changelog/commit history comments).
- Cấm bình luận than phiền, cảm xúc hoặc đổ lỗi.
- Cấm chú thích đóng ngoặc thừa thãi (Ví dụ: `} // end if`).

---

## 7. Quy trình Thực thi của Agent (Execution Protocol)

1. **Đọc trước khi sửa (Research First):** Luôn đọc task card trong `docs/workflow/plans/...` và contracts liên quan trong `docs/workflow/architecture/...` trước khi viết mã.
2. **Bảo toàn Hợp đồng Dùng chung (Shared Contracts):** Không tự ý đổi tên field, kiểu dữ liệu của API Contract đã được phê duyệt tại G3 mà không có sự đồng ý của cả nhóm.
3. **Tự kiểm thử trước khi bàn giao (Self-Verification Gate):**
   - Backend: Bắt buộc chạy `dotnet build backend/CVAnalysis.sln` và kiểm tra 0 lỗi.
   - Frontend: Bắt buộc chạy `npm run build` trong `frontend/cvanalysis-client` và kiểm tra 0 type error.
4. **Báo cáo Xúc tích & Bằng chứng Thật:** Nêu cụ thể file, symbol đã thay đổi, lệnh kiểm chứng và output thực tế. Không tường thuật chung chung.

---

## 8. Chỉ mục Tài liệu Tra cứu Sâu (Documentation Index)

Khi cần thông tin chi tiết về nghiệp vụ, kiến trúc hoặc kế hoạch triển khai, Agent đọc các tệp sau:

| Mục tiêu tra cứu | Đường dẫn tệp trong repository |
|---|---|
| **Nghiệp vụ cốt lõi & Quy tắc BR** | `docs/workflow/specs/cv-analysis-brief.md` |
| **20 User Stories & 61 Acceptance Criteria** | `docs/workflow/specs/cv-analysis-stories.md` |
| **Kiến trúc, 15 bảng DDL, ADRs & API Contracts** | `docs/workflow/architecture/cv-analysis-design.md` |
| **Sơ đồ ERD trực quan (HTML/SVG)** | `docs/workflow/diagrams/cv-analysis-erd.html` |
| **Lộ trình Sprint 1 & Ma trận Dependencies** | `docs/workflow/plans/quan-ly-danh-gia-cv-sprint-1/roadmap.md` |
| **Chi tiết Task Backend (`TASK-01` $\rightarrow$ `TASK-07`)** | `docs/workflow/plans/quan-ly-danh-gia-cv-sprint-1/backend/` |
| **Chi tiết Task Frontend (`TASK-08` $\rightarrow$ `TASK-14`)** | `docs/workflow/plans/quan-ly-danh-gia-cv-sprint-1/frontend/` |
| **Mock API Layer & QA (`TASK-15`, `TASK-16`)** | `docs/workflow/plans/quan-ly-danh-gia-cv-sprint-1/qa/` |
| **Tiến độ tổng thể & Story Points (Product Backlog)** | `docs/workflow/product-backlog.md` |

---

## 9. Danh mục Tài liệu Sơ cấp Tham chiếu (Primary Sources)

1. [Microsoft C# Coding Conventions](https://learn.microsoft.com/dotnet/csharp/fundamentals/coding-style/coding-conventions)
2. [Microsoft Framework Design Guidelines: Naming Guidelines](https://learn.microsoft.com/dotnet/standard/design-guidelines/naming-guidelines)
3. [Microsoft Framework Design Guidelines: Member Design](https://learn.microsoft.com/dotnet/standard/design-guidelines/member-design-guidelines)
4. [Microsoft Clean Architecture Guide](https://learn.microsoft.com/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures)
5. [Microsoft XML Documentation Comments Guide](https://learn.microsoft.com/dotnet/csharp/language-reference/xmldoc/recommended-tags)
6. [TypeScript Handbook: Declaration Files Do's and Don'ts](https://www.typescriptlang.org/docs/handbook/declaration-files/do-s-and-don-ts.html)
7. [React.dev: Reusing Logic with Custom Hooks](https://react.dev/learn/reusing-logic-with-custom-hooks)
8. [TSDoc Official Specification](https://tsdoc.org/)
9. [Rob Pike: Notes on Programming in C](https://www.lysator.liu.se/c/pikestyle.html)
10. [Robert C. Martin: Clean Code (Prentice Hall, 2008)](https://www.oreilly.com/library/view/clean-code-a/9780136083238/)
