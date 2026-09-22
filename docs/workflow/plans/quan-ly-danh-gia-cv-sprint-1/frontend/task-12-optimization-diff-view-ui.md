# TASK-12: Giao diện Tối ưu hóa, So sánh Diff View & Xuất Bản File PDF/DOCX

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-12` |
| Folder & Phân bổ | `frontend/` &bull; Phân công: **Dev 2 (Frontend & UI/UX)** |
| User Stories liên quan | `US10`, `US11`, `US12` |
| Màn hình liên quan | `SCR07` (Tối ưu hóa & Diff View) |
| Hard Prerequisites | `TASK-11` (Báo cáo ATS) |
| Chặn các tasks | `TASK-16` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng màn hình Tối ưu câu văn và So sánh thay đổi trực quan (Diff View) giữa nội dung gốc trong CV và bản đề xuất viết lại theo chuẩn Action-Context-Metric (BR06, BR14), cho phép người dùng bấm "Chấp nhận", "Từ chối" hoặc trực tiếp chỉnh sửa thủ công văn bản mà không tính thêm phí (BR13), đồng thời cung cấp các nút kích hoạt xuất file CV hoàn chỉnh định dạng PDF và Word (.docx).

---

## 2. Checklist Hành động

- [ ] Xây dựng Component `DiffViewer`:
  - Trình bày 2 cột song song (Side-by-side) trên màn hình máy tính và xếp chồng trên màn hình di động:
    - Cột trái: *Nội dung gốc trong CV*.
    - Cột phải: *Đề xuất tối ưu (Action + Context + Metric)* kèm highlight các từ ngữ được thay thế/thêm mới (dùng class `diff-add` nền xanh và `diff-del` nền đỏ).
- [ ] Xây dựng bộ điều khiển tương tác từng gạch đầu dòng:
  - Nút **"Chấp nhận"**: đánh dấu gạch đầu dòng đã được áp dụng vào bản xuất cuối.
  - Nút **"Từ chối"**: khôi phục lại câu văn gốc ban đầu của ứng viên.
  - Nút **"Sửa thủ công"**: mở ô nhập liệu cho phép gõ trực tiếp và lưu nội dung sửa (không trừ thêm credit — BR13).
- [ ] Xây dựng Component `ExportButtonGroup`:
  - Nút **"Xuất PDF"**: gọi API POST `/api/v1/reports/{id}/export` với format `PDF`, tự động kích hoạt tải file về máy `CV_PDF_ToiUu_[TenUngVien].pdf`.
  - Nút **"Xuất DOCX"**: gọi API POST `/api/v1/reports/{id}/export` với format `DOCX`, tải file `CV_DOCX_ToiUu_[TenUngVien].docx`.
  - Hỗ trợ tải lại miễn phí trong suốt 90 ngày hiệu lực của báo cáo (BR14, BR26).

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given bản viết lại được hiển thị
When người dùng bấm nút "Từ chối" ở gạch đầu dòng số 1
Then gạch đầu dòng số 1 quay về đúng nguyên văn câu gốc trong CV của ứng viên

Given người dùng muốn chỉnh sửa thêm số liệu thành tích
When bấm "Sửa thủ công" và gõ nội dung mới vào ô văn bản
Then hệ thống lưu bản chỉnh sửa của người dùng mà không yêu cầu thêm credit nào (BR13)

Given người dùng đã hoàn thành chỉnh sửa
When bấm nút "Xuất PDF"
Then trình duyệt kích hoạt tải file PDF chuẩn văn bản 1 cột về máy của người dùng
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
cd cvanalysis-client
npm test -- DiffViewer.test.tsx
```
