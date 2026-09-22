# TASK-26: Thẻ Ma trận 4 Điểm Tương thích & Danh sách Hướng dẫn Tự Sửa Lỗi (SCR15)

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-26` |
| Folder & Phân bổ | `frontend/` &bull; Phân công: **Dev 2 (Frontend & UI/UX)** |
| User Stories liên quan | `US22`, `US23` |
| Màn hình liên quan | `SCR15` (Tab ATS Reader View & Ma trận Tương thích) |
| Hard Prerequisites | `TASK-25` (Khung Tab Reader View) |
| Chặn các tasks | `TASK-27` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng cụm Thẻ Ma trận 4 Điểm Tương thích Đa Hệ thống (% Workday, % Taleo, % Greenhouse, % Lever) với phân tầng màu sắc trực quan (Xanh: An toàn $> 80\%$; Vàng: Cần chú ý $60–79\%$; Đỏ: Rủi ro cao $< 60\%$), và danh sách các lỗi bố cục được phân loại kèm hướng dẫn từng bước cụ thể (Actionable Guidance) để ứng viên tự chỉnh sửa trên file Microsoft Word hoặc Canva gốc.

---

## 2. Checklist Hành động

- [ ] Xây dựng Component `CompatibilityMatrixCards`:
  - Lưới 4 thẻ điểm đại diện cho 4 dòng hệ thống:
    1. Thẻ Workday (ví dụ: `70% - Cần chú ý`)
    2. Thẻ Oracle Taleo (ví dụ: `58% - Rủi ro cao`)
    3. Thẻ Greenhouse (ví dụ: `92% - An toàn`)
    4. Thẻ Lever (ví dụ: `88% - An toàn`)
  - Thanh tiến trình mini (progress bar) thể hiện tỷ lệ % trực quan.
- [ ] Xây dựng Component `ActionableFixesList` (US23):
  - Phân nhóm lỗi:
    - *Lỗi Nghiêm trọng (Critical)*: Nguy cơ bị loại tự động 100% (ví dụ: Header stripping trên Taleo, PDF scan ảnh trên Greenhouse).
    - *Lỗi Định dạng (Warning)*: Bảng ẩn, cột đôi trên Workday, thẻ kỹ năng thiếu dấu phân cách trên Lever.
  - Accordion mở rộng cho từng lỗi:
    - Giải thích nguyên nhân kỹ thuật bằng ngôn ngữ dễ hiểu.
    - Hướng dẫn sửa trên Microsoft Word (từng bước kèm menu click).
    - Hướng dẫn sửa trên Canva / Google Docs.
- [ ] Nút kêu gọi hành động CTA: *"Tự động sửa lỗi bố cục khi xuất file (Auto-fix Layout)"* dẫn sang Modal SCR16.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given báo cáo trả về điểm Taleo là 58% do lỗi số điện thoại ở Header
When cụm ma trận hiển thị
Then thẻ Taleo hiển thị viền đỏ và nhãn "Rủi ro cao (58%)"
And danh sách lỗi xuất hiện cảnh báo nghiêm trọng kèm hướng dẫn gỡ bỏ Header trong Word

Given người dùng bấm vào một lỗi định dạng
When accordion mở ra
Then hiển thị chi tiết các bước sửa trên Word và Canva rõ ràng, dễ thao tác
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
cd cvanalysis-client
npm test -- CompatibilityMatrix.test.tsx
```
