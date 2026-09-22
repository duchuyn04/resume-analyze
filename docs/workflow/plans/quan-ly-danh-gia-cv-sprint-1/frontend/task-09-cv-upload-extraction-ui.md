# TASK-09: Giao diện Tải CV, Client Validation & Form Xác nhận Trích xuất

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-09` |
| Folder & Phân bổ | `frontend/` &bull; Phân công: **Dev 2 (Frontend & UI/UX)** |
| User Stories liên quan | `US04`, `US05` |
| Màn hình liên quan | `SCR03` (Tải CV & Nhập JD), `SCR04` (Xác nhận trích xuất) |
| Hard Prerequisites | `TASK-08` (App Shell), `TASK-15` (API Client / Mock layer) |
| Chặn các tasks | `TASK-16` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng khu vực tải file CV (Drag-and-Drop Dropzone) hỗ trợ định dạng PDF và Word (.docx), kiểm tra tức thời các điều kiện biên tại phía client (dung lượng $\le 10$MB, đuôi file hợp lệ), hiển thị thanh tiến trình trích xuất nội dung và màn hình Form xác nhận/chỉnh sửa thông tin trích xuất (5 mục: Liên hệ, Học vấn, Kinh nghiệm, Kỹ năng, Chứng chỉ) trước khi chạy phân tích (US05).

---

## 2. Checklist Hành động

- [ ] Xây dựng Component `CvDropzone`:
  - Kéo thả file hoặc bấm chọn file từ máy tính.
  - Client-side validation:
    - Nếu file không phải `.pdf` hoặc `.docx` $\rightarrow$ báo lỗi đỏ inline ngay lập tức: *"Định dạng file không hỗ trợ. Vui lòng chọn file .pdf hoặc .docx"*.
    - Nếu file $> 10$MB $\rightarrow$ báo lỗi đỏ: *"File vượt quá giới hạn 10MB"*.
  - Hiển thị spinner và trạng thái upload khi đang gửi dữ liệu lên server.
- [ ] Xây dựng Component `ExtractionReviewModal` / View `SCR04`:
  - Hiển thị kết quả trích xuất dạng Form trực quan chia 5 phần:
    1. Thông tin liên hệ (Họ tên, Email, Số điện thoại, Địa điểm, LinkedIn/Portfolio).
    2. Danh sách Học vấn (Bằng cấp, Trường học, Năm tốt nghiệp).
    3. Lịch sử Kinh nghiệm (Chức danh, Tên công ty, Thời gian làm việc, Các gạch đầu dòng mô tả).
    4. Kỹ năng chuyên môn (Tags có thể thêm/xóa/sửa).
    5. Chứng chỉ (nếu có).
  - Cho phép người dùng trực tiếp sửa các trường bị sai lỗi chính tả.
  - Nút "Xác nhận nội dung này" gửi payload sang API PUT `/api/v1/cv/{id}/confirm` để lưu làm căn cứ chấm điểm (BR09, BR23).

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given người dùng chọn file Word .docx dung lượng 15MB
When kéo thả file vào Dropzone
Then hệ thống chặn lại ngay tại client và hiển thị thông báo lỗi file quá 10MB

Given file PDF 2 trang hợp lệ được tải lên thành công
When hệ thống trích xuất xong
Then giao diện hiển thị form thông tin trích xuất đầy đủ các mục
And ứng viên có thể sửa lại tên kỹ năng và bấm xác nhận để chuyển tiếp
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
cd cvanalysis-client
npm test -- CvDropzone.test.tsx
```
