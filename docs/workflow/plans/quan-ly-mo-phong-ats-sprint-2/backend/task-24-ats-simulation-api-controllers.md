# TASK-24: REST API Controllers cho Phân hệ Mô phỏng ATS & Xuất File Auto-fix

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-24` |
| Folder & Phân bổ | `backend/` &bull; Phân công: **Dev 1 (Backend & Database)** |
| User Stories liên quan | `US21`, `US22`, `US24`, `US25` |
| WebApi Controllers | `CVAnalysis.WebApi/Controllers/AtsSimulationController.cs` |
| Hard Prerequisites | `TASK-21`, `TASK-22`, `TASK-23` |
| Chặn các tasks | `TASK-28` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng các REST API Controllers cung cấp dữ liệu mô phỏng 4 dòng ATS cho giao diện người dùng, hỗ trợ tải kết quả bóc tách văn bản thô, tính toán ma trận điểm tương thích, hỗ trợ quét tương thích độc lập không cần JD (US25), và cung cấp endpoint xuất file CV đã chuẩn hóa 1 cột an toàn (US24), áp dụng đầy đủ cơ chế bảo vệ Zero-Trust và kiểm tra thời hạn cấp quyền hỗ trợ 72h.

---

## 2. Checklist Hành động

- [ ] Tạo `AtsSimulationController` kế thừa `ApiControllerBase`:
  - `[Authorize]` trên toàn bộ Controller.
- [ ] Endpoint 1: `GET /api/v1/reports/{reportId}/ats-simulation` (US21, US22, US23):
  - Kiểm tra quyền sở hữu: chỉ chủ sở hữu report (`UserId == CurrentUser`) hoặc nhân viên hỗ trợ có quyền hợp lệ trong 72h mới được xem (Zero-Trust — BR22).
  - Trả về DTO `AtsSimulationResponse`: 4 điểm số tương thích, raw text của từng hệ thống, các tọa độ cảnh báo lỗi bôi màu, và danh sách hướng dẫn tự sửa chi tiết.
- [ ] Endpoint 2: `POST /api/v1/cv/scan-ats-compatibility` (US25):
  - Nhận file upload `multipart/form-data`.
  - Thực hiện trích xuất và chạy 4 simulator ở chế độ không có JD (tiêu chí từ khóa Greenhouse gán `NOT_APPLICABLE`).
  - Trả về `AtsQuickScanResponse` với 4 điểm số và trạng thái an toàn bố cục.
- [ ] Endpoint 3: `POST /api/v1/reports/{reportId}/export-autofix` (US24):
  - Nhận request body `{ "format": "PDF" | "DOCX", "autoFixLayout": true }`.
  - Gọi `AutoFixLayoutService` để sinh stream dữ liệu nhị phân.
  - Trả về `FileStreamResult` với Content-Type chuẩn (`application/pdf` hoặc `application/vnd.openxmlformats-officedocument.wordprocessingml.document`).
- [ ] Ghi nhận nhật ký kiểm toán vào `dbo.AuditLogs` khi nhân viên hỗ trợ gọi API xem dữ liệu bóc tách ATS của khách hàng.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given ứng viên đã hoàn tất lượt phân tích nâng cao
When gọi API GET /api/v1/reports/{id}/ats-simulation
Then API trả về 200 OK chứa điểm số của 4 hệ thống và văn bản bóc tách của Workday, Taleo, Greenhouse, Lever

Given ứng viên tải file CV kiểm tra không có JD qua API POST /api/v1/cv/scan-ats-compatibility
When phân tích hoàn tất
Then API trả về ma trận điểm mà không trừ lượt hay đòi hỏi phải dán JD

Given ứng viên yêu cầu xuất file với autoFixLayout = true
When gọi API POST /api/v1/reports/{id}/export-autofix
Then nhận về stream file tải về chuẩn 1 cột an toàn tuyệt đối
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
dotnet test tests/CVAnalysis.IntegrationTests --filter FullyQualifiedName~AtsApiControllerTests
```
