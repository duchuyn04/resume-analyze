# TASK-01: Khởi tạo Solution ASP.NET Core, EF Core DbContext & SQL Server Migrations

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-01` |
| Folder & Phân bổ | `backend/` &bull; Phân công: **Dev 1 (Backend & Database)** |
| User Stories liên quan | Nền tảng kỹ thuật cho toàn bộ `US01` – `US20` |
| Bảng cơ sở dữ liệu | Cả 15 bảng DDL tại Mục 2 `cv-analysis-design.md` |
| Hard Prerequisites | Không (Bắt đầu ngay tại thời điểm T0) |
| Chặn các tasks | `TASK-02`, `TASK-03`, `TASK-04`, `TASK-06` |
| Trạng thái | `Done` |

---

## 1. Mục tiêu & Phạm vi

Khởi tạo cấu trúc dự án ASP.NET Core (.NET 9/8) theo mô hình Clean Architecture, cài đặt Entity Framework Core với SQL Server Provider, định nghĩa đầy đủ 15 Entities/Configurations theo đúng DDL và quan hệ khóa ngoại (CASCADE / NO ACTION), và sinh bản migration đầu tiên (`InitialCreate`).

---

## 2. Checklist Hành động

- [x] Tạo dotnet solution `CVAnalysis.sln` với 4 projects:
  - `CVAnalysis.Domain` (Class library)
  - `CVAnalysis.Application` (Class library)
  - `CVAnalysis.Infrastructure` (Class library)
  - `CVAnalysis.WebApi` (Web API project)
- [x] Cài đặt NuGet packages cần thiết:
  - `Microsoft.EntityFrameworkCore.SqlServer` (8.0.14)
  - `Microsoft.EntityFrameworkCore.Design` (8.0.14)
  - `Microsoft.EntityFrameworkCore.Tools` (8.0.14)
- [x] Khai báo 15 Entity classes trong `CVAnalysis.Domain/Entities/`:
  - `User`, `EmailHashRegistry`, `UserBalance`, `CVFile`, `JobDescription`
  - `AnalysisRequest`, `Report`, `OptimizedBulletPoint`, `CreditPackage`, `PaymentOrder`
  - `CreditLedger`, `SupportTicket`, `SupportAccessGrant`, `CompensationRequest`, `AuditLog`
- [x] Thiết lập `IEntityTypeConfiguration<T>` cho từng entity trong `CVAnalysis.Infrastructure/Persistence/Configurations/`:
  - Ràng buộc khóa chính (PK), khóa duy nhất (UQ), khóa ngoại (FK) với hành vi `ON DELETE`.
  - Cấu hình `RowVersion` cho `UserBalance` (`IsRowVersion()`).
  - Đánh chỉ mục lọc (Filtered indexes) cho `expires_at` và `timeout_at`.
- [x] Cấu hình `ApplicationDbContext` và chuỗi kết nối SQL Server trong `appsettings.Development.json`.
- [x] Chạy lệnh tạo bản migration đầu tiên: `dotnet ef migrations add InitialCreate --project backend/src/CVAnalysis.Infrastructure --startup-project backend/src/CVAnalysis.WebApi`.
---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given dự án ASP.NET Core được khởi tạo và chuỗi kết nối SQL Server hợp lệ
When lập trình viên thực thi lệnh `dotnet ef database update`
Then toàn bộ 15 bảng cơ sở dữ liệu được tạo thành công trên SQL Server
And bảng dbo.UserBalances có cột row_version kiểu rowversion
And quan hệ từ dbo.AnalysisRequests sang dbo.CVFiles có hành vi ON DELETE CASCADE
And quan hệ từ dbo.CVFiles sang dbo.Users có hành vi ON DELETE NO ACTION
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
dotnet build CVAnalysis.sln
dotnet ef migrations list --project src/CVAnalysis.Infrastructure --startup-project src/CVAnalysis.WebApi
```

---

## 5. Bằng chứng Thực thi (Execution Evidence)

- **Lệnh build solution:**
  - `dotnet build CVAnalysis.sln` $\rightarrow$ Exit Code: 0, 0 Warnings, 0 Errors.
  - `dotnet build backend/CVAnalysis.sln` $\rightarrow$ Exit Code: 0, 0 Warnings, 0 Errors.
- **Lệnh kiểm tra migration:**
  - `dotnet ef migrations list --project backend/src/CVAnalysis.Infrastructure --startup-project backend/src/CVAnalysis.WebApi` $\rightarrow$ `20260922114446_InitialCreate (Pending)`.
- **Kiểm chứng tiêu chí nghiệm thu schema:**
  - 15 bảng DDL được tạo đầy đủ trong `20260922114446_InitialCreate.cs`.
  - Cột `row_version` trong `dbo.UserBalances`: kiểu `rowversion` (`rowVersion: true`).
  - Quan hệ `AnalysisRequests` $\rightarrow$ `CVFiles`: `onDelete: ReferentialAction.Cascade`.
  - Quan hệ `CVFiles` $\rightarrow$ `Users`: `onDelete: ReferentialAction.NoAction`.
