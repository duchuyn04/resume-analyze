# TASK-03: Trích xuất Văn bản PDF/DOCX, Upload Service & Quản lý Hạn 90 Ngày

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-03` |
| Folder & Phân bổ | `backend/` &bull; Phân công: **Dev 1 (Backend & Database)** |
| User Stories liên quan | `US04`, `US05`, `US06`, `US16` |
| Bảng cơ sở dữ liệu | `dbo.CVFiles`, `dbo.JobDescriptions` |
| Hard Prerequisites | `TASK-01` (Cần bảng CVFiles và JobDescriptions) |
| Chặn các tasks | `TASK-04`, `TASK-05` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng module xử lý và trích xuất văn bản từ file CV (PDF và Word DOCX), kiểm tra nghiêm ngặt các điều kiện biên kỹ thuật (dung lượng $\le 10$MB, số trang $\le 5$, từ chối file có mật khẩu, dừng trước PDF scan thuần ảnh theo BR03/OQ06), phân tách cấu trúc thông tin JSON sơ bộ và gán thời hạn lưu trữ độc lập 90 ngày (BR21).

---

## 2. Checklist Hành động

- [ ] Cài đặt NuGet: `PdfPig` (trích xuất PDF), `DocumentFormat.OpenXml` (trích xuất DOCX).
- [ ] Xây dựng interface `IPdfTextExtractor` và `IDocxTextExtractor`:
  - Kiểm tra mật khẩu bảo vệ (password protection): nếu có $\rightarrow$ ném ngoại lệ `EncryptedFileException`.
  - Đếm số trang: nếu $> 5$ trang $\rightarrow$ ném ngoại lệ `PageLimitExceededException`.
  - Kiểm tra lớp văn bản: nếu không có text nào trích xuất được (PDF scan ảnh) $\rightarrow$ ném ngoại lệ `UnparseableFileException` (BR03).
- [ ] Xây dựng Command `UploadCvFileCommand`:
  - Kiểm tra dung lượng file $\le 10$MB.
  - Lưu file vật lý vào thư mục lưu trữ cục bộ có bảo mật (hoặc S3/MinIO abstraction).
  - Trích xuất text thô (`raw_text`) và chuẩn hóa cấu trúc JSON (`extracted_data`).
  - Đặt `expires_at = SYSDATETIMEOFFSET() + 90 days`.
  - Lưu bản ghi vào bảng `dbo.CVFiles`.
- [ ] Xây dựng Command `ConfirmCvExtractionCommand` (US05):
  - Cho phép người dùng chỉnh sửa nội dung trích xuất và lưu bản ghi `Nội dung đã xác nhận` làm căn cứ chấm điểm (BR09, BR23).
- [ ] Xây dựng Command `ValidateJobDescriptionCommand` (US06):
  - Đếm số từ: kiểm tra $50 \le \text{word\_count} \le 2500$ (hoặc $\ge 300$ ký tự).
  - Kiểm tra điều kiện căn cứ: chức danh, $\ge 3$ kỹ năng hoặc $\ge 2$ trách nhiệm (BR35, Mục 5.1).

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given file PDF dung lượng 2MB, dài 3 trang, có lớp text chọn được
When ứng viên tải file lên qua API POST /api/v1/cv/upload
Then hệ thống trả về mã 200 OK kèm cấu trúc JSON thông tin trích xuất
And bản ghi dbo.CVFiles có expires_at bằng đúng ngày tạo cộng 90 ngày

Given file PDF có mật khẩu bảo vệ hoặc file scan thuần ảnh
When ứng viên tải file lên
Then hệ thống từ chối xử lý, trả về mã lỗi 400 Bad Request kèm thông báo hướng dẫn
And không tạo bản ghi phân tích, không trừ lượt hoặc credit của ứng viên

Given nội dung JD nhập vào chỉ có 35 từ
When gọi API POST /api/v1/jd/validate
Then hệ thống trả về minRequirementsMet = false kèm cảnh báo thiếu căn cứ đối chiếu
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
dotnet test tests/CVAnalysis.UnitTests --filter FullyQualifiedName~CvParserTests
```
