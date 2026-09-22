# TASK-23: Dịch vụ Dàn phẳng Bố cục Auto-fix Layout khi Xuất File (PDF/DOCX)

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-23` |
| Folder & Phân bổ | `backend/` &bull; Phân công: **Dev 1 (Backend & Database)** |
| User Stories liên quan | `US23`, `US24` |
| Domain & Infra | `CVAnalysis.Infrastructure/Export/AutoFixLayoutService.cs` |
| Hard Prerequisites | Kế thừa `IPdfParser` & `OpenXml` từ Sprint 1 |
| Chặn các tasks | `TASK-24`, `TASK-28` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng dịch vụ `AutoFixLayoutService` có khả năng tự động phân tích và dàn phẳng (flattening) các cấu trúc CV phức tạp (bảng ẩn, cột đôi, text box trôi nổi) thành bố cục 1 cột tuần tự từ trên xuống dưới theo chuẩn ATS khi người dùng kích hoạt tùy chọn "Tự động chuẩn hóa bố cục khi xuất file" (US24, SR06), đảm bảo 100% file PDF/Word sinh ra vượt qua các bài kiểm tra bóc tách của Taleo và Workday.

---

## 2. Checklist Hành động

- [ ] Xây dựng interface `IAutoFixLayoutService`:
  - `Task<Stream> ExportSafePdfAsync(ConfirmedCvData data, ReportScoreResult score, CancellationToken ct)`.
  - `Task<Stream> ExportSafeDocxAsync(ConfirmedCvData data, ReportScoreResult score, CancellationToken ct)`.
- [ ] Triển khai template 1 cột văn bản tuần tự (Single-column layout template):
  - Khối Header: Họ tên (cỡ chữ 18pt), Email, Số điện thoại, Địa chỉ, Link LinkedIn (đặt ở body dòng 1, tuyệt đối không đưa vào `<header>` hay `<w:headerReference>`).
  - Khối Phân mục chuẩn convention:
    - `Tóm tắt chuyên môn` (Professional Summary)
    - `Kỹ năng` (Skills — phân cách bằng bullet `•`, không dùng table hay multicolumn)
    - `Kinh nghiệm làm việc` (Work Experience — Tên vị trí, Tên công ty, Ngày tháng, Gạch đầu dòng ACM)
    - `Học vấn` (Education)
    - `Chứng chỉ` (Certifications)
- [ ] Xử lý loại bỏ hoàn toàn các cấu trúc phá vỡ ATS:
  - Loại bỏ các thẻ `<w:tbl>` (table layout), `<w:txbx>` (text box thả nổi), `<w:cols>` (column breaks).
  - Đảm bảo font chữ chuẩn (Arial, Calibri, Times New Roman) không bị lỗi font encoding.
- [ ] Viết hàm tự kiểm thử (Self-verification): Cho chạy file vừa xuất qua `WorkdayParserSimulator` và `TaleoParserSimulator`, khẳng định điểm số đạt 100% an toàn định dạng.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given CV gốc có thiết kế 2 cột phức tạp và số điện thoại nằm ở Header
When người dùng yêu cầu xuất file với cờ autoFixLayout = true
Then file PDF và DOCX sinh ra có định dạng 1 cột tuần tự sạch sẽ
And thông tin liên hệ được đặt an toàn ở phần thân trên cùng của trang giấy
And khi đưa file này qua 4 bộ ATS simulator, điểm định dạng đạt 100% tuyệt đối
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
dotnet test tests/CVAnalysis.UnitTests --filter FullyQualifiedName~AutoFixLayoutTests
```
