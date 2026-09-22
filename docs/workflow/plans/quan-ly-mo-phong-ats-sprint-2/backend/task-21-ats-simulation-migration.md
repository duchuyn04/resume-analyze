# TASK-21: Bảng dbo.AtsSimulations, EF Core Entity & Migration (Sprint 2)

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-21` |
| Folder & Phân bổ | `backend/` &bull; Phân công: **Dev 1 (Backend & Database)** |
| User Stories liên quan | Nền tảng dữ liệu cho `US21` – `US25` |
| Bảng cơ sở dữ liệu | `dbo.AtsSimulations` |
| Hard Prerequisites | Kế thừa Sprint 1 (`dbo.Reports`) |
| Chặn các tasks | `TASK-22`, `TASK-24` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Định nghĩa Entity `AtsSimulation` trong `CVAnalysis.Domain`, cấu hình `IEntityTypeConfiguration<AtsSimulation>` với khóa ngoại `report_id` tham chiếu `dbo.Reports(id)` có hành vi `ON DELETE CASCADE` (ADR-004), tạo và thực thi bản Migration `AddAtsSimulationsTable` trên cơ sở dữ liệu SQL Server.

---

## 2. Checklist Hành động

- [ ] Tạo class `AtsSimulation` trong `CVAnalysis.Domain/Entities/`:
  - `Id`: `Guid` (PK)
  - `ReportId`: `Guid` (FK, UQ)
  - `WorkdayScore`: `int` (0–100)
  - `TaleoScore`: `int` (0–100)
  - `GreenhouseScore`: `int` (0–100)
  - `LeverScore`: `int` (0–100)
  - `SimulationDetails`: `string` (`NVARCHAR(MAX)` JSON)
  - `CreatedAt`: `DateTimeOffset`
  - `ExpiresAt`: `DateTimeOffset` (90 ngày)
  - Navigation property: `public Report Report { get; set; }`
- [ ] Cấu hình `AtsSimulationConfiguration` trong `CVAnalysis.Infrastructure/Persistence/Configurations/`:
  - Ràng buộc khóa chính, chỉ mục duy nhất trên `ReportId`.
  - Khóa ngoại `HasOne(a => a.Report).WithOne(r => r.AtsSimulation).HasForeignKey<AtsSimulation>(a => a.ReportId).OnDelete(DeleteBehavior.Cascade)`.
  - Check constraints: $0 \le \text{Score} \le 100$ cho cả 4 cột điểm.
  - Non-clustered index trên `ExpiresAt`.
- [ ] Cập nhật `ApplicationDbContext` đăng ký `DbSet<AtsSimulation> AtsSimulations`.
- [ ] Chạy lệnh tạo migration: `dotnet ef migrations add AddAtsSimulationsTable --project src/CVAnalysis.Infrastructure --startup-project src/CVAnalysis.WebApi`.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given bản migration AddAtsSimulationsTable được áp dụng
When kiểm tra bảng dbo.AtsSimulations trên SQL Server
Then bảng tồn tại với đầy đủ 4 cột điểm int, cột simulation_details kiểu nvarchar(max)
And khóa ngoại report_id có ràng buộc ON DELETE CASCADE từ dbo.Reports
And khi một bản ghi dbo.Reports bị xóa, bản ghi AtsSimulations tương ứng tự động bị xóa theo
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
dotnet ef migrations list --project src/CVAnalysis.Infrastructure --startup-project src/CVAnalysis.WebApi
```
