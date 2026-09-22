# TASK-27: Modal So sánh & Xem trước Auto-fix Layout 1 Cột khi Xuất File (SCR16)

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-27` |
| Folder & Phân bổ | `frontend/` &bull; Phân công: **Dev 2 (Frontend & UI/UX)** |
| User Stories liên quan | `US24` |
| Màn hình liên quan | `SCR16` (Modal Xem trước Auto-fix Layout) |
| Hard Prerequisites | `TASK-26` |
| Chặn các tasks | `TASK-28` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng Modal Xem trước So sánh Bố cục (Preview Modal — SCR16 / US24) hiển thị 2 cột song song giữa bản CV thiết kế gốc của người dùng và bản CV được thuật toán tự động dàn phẳng thành 1 cột an toàn (Auto-fix Layout on Export), giúp ứng viên an tâm về mặt thẩm mỹ trước khi xác nhận tải file PDF hoặc Word (.docx) chuẩn ATS về máy.

---

## 2. Checklist Hành động

- [ ] Xây dựng Component `AutoFixPreviewModal` (SCR16):
  - Kích hoạt khi người dùng bấm nút *"Tự động sửa lỗi bố cục khi xuất file"* từ màn hình Reader View hoặc thanh công cụ xuất file.
  - Thiết kế màn hình so sánh 2 cột:
    - Cột trái: *Bố cục gốc của bạn* (hiển thị bố cục 2 cột, bảng biểu hiện tại có viền cảnh báo các điểm bị ATS làm gãy).
    - Cột phải: *Bản chuẩn hóa 1 cột an toàn (ATS Safe Layout)* (hiển thị văn bản 1 cột tuần tự, font chữ sạch sẽ, phân mục chuẩn).
  - Huy hiệu bảo chứng: *“Đạt 100% độ an toàn định dạng trên cả 4 hệ thống ATS”*.
- [ ] Bộ nút xuất file trong Modal:
  - Nút **"Tải PDF chuẩn hóa 1 cột"**: kích hoạt API POST `/api/v1/reports/{id}/export-autofix` với định dạng `PDF`.
  - Nút **"Tải DOCX chuẩn hóa 1 cột"**: kích hoạt API với định dạng `DOCX`.
  - Nút **"Giữ nguyên bản gốc và tải"**: cho phép người dùng từ chối Auto-fix nếu muốn giữ nguyên thiết kế ban đầu.
- [ ] Xử lý trạng thái tải (Loading state / Spinner) trong khi server đang sinh file nhị phân.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given người dùng bấm nút "Tự động sửa lỗi bố cục khi xuất file"
When Modal SCR16 mở ra
Then hiển thị so sánh trực quan giữa bản gốc và bản dàn phẳng 1 cột an toàn
And có huy hiệu khẳng định 100% an toàn định dạng

Given người dùng bấm nút "Tải PDF chuẩn hóa 1 cột"
When API trả về dữ liệu file
Then trình duyệt kích hoạt tải file PDF về máy và đóng modal xem trước
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
cd cvanalysis-client
npm test -- AutoFixPreviewModal.test.tsx
```
