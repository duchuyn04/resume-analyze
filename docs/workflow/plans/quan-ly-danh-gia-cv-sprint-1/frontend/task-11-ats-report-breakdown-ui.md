# TASK-11: Giao diện Báo cáo Chấm điểm ATS, Accordion 4 Trụ cột & Cảnh báo BR18

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-11` |
| Folder & Phân bổ | `frontend/` &bull; Phân công: **Dev 2 (Frontend & UI/UX)** |
| User Stories liên quan | `US08`, `US09` |
| Màn hình liên quan | `SCR06` (Báo cáo Chấm điểm ATS) |
| Hard Prerequisites | `TASK-08`, `TASK-15` |
| Chặn các tasks | `TASK-12` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng màn hình Báo cáo kết quả phân tích chuẩn ATS (SCR06), bao gồm: Banner điểm số tổng hợp làm tròn số nguyên kèm phân tầng nhãn (Xuất sắc, Khá, Trung bình, Kém), Chú thích minh bạch về trọng số chuẩn hóa mẫu số 85 khi học vấn N/A (BR05), Hộp cảnh báo yêu cầu bắt buộc chưa tìm thấy bằng chứng nổi bật (BR18), và Lưới Accordion 4 Trụ cột hiển thị chi tiết điểm thành phần, bằng chứng đối chiếu và lý do bị trừ điểm.

---

## 2. Checklist Hành động

- [ ] Xây dựng Component `ScoreBanner`:
  - Điểm tổng hợp lớn: ví dụ `82/100`.
  - Phân tầng nhãn động:
    - 85–100: Màu xanh lá đậm *"Xuất sắc — Sẵn sàng ứng tuyển"*.
    - 70–84: Màu xanh lục *"Khá — Cần sửa nhỏ"*.
    - 50–69: Màu vàng *"Trung bình — Cần cải thiện"*.
    - 0–49: Màu đỏ *"Kém — Nguy cơ bị loại cao"*.
  - Ghi chú mẫu số chuẩn hóa: *"Trọng số chuẩn hóa: Mẫu số 85 (Học vấn NOT_APPLICABLE do JD không yêu cầu)"*.
  - Thời hạn báo cáo: *"Hết hạn sau 90 ngày (BR21)"*.
- [ ] Xây dựng Component `MandatoryWarningBox` (BR18):
  - Khối màu vàng hổ phách (Amber-50) với viền đỏ/vàng nổi bật.
  - Liệt kê danh sách kỹ năng hoặc chứng chỉ bắt buộc bị điểm 0%.
  - Luôn hiển thị ở vị trí trên cùng của báo cáo, độc lập và không bị điểm số cao che lấp.
- [ ] Xây dựng Component `PillarsAccordionGrid`:
  - **Trụ cột 1 (Kỹ năng & Từ khóa):** Hiển thị danh sách kỹ năng trích xuất được kèm nhãn tỷ lệ khớp: Trùng khớp 100%, Từ đồng nghĩa 95%, Ngữ cảnh 60%, Thiếu 0%.
  - **Trụ cột 2 (Kinh nghiệm & Dự án):** Điểm số năm ($R$), độ khớp chức danh, đánh giá gạch đầu dòng và số liệu đo lường.
  - **Trụ cột 3 (Định dạng & Trích xuất):** Đánh giá trích xuất text, bố cục 1 cột, tiêu đề chuẩn, thông tin liên hệ.
  - **Trụ cột 4 (Học vấn & Bằng cấp):** Hiển thị điểm bằng cấp hoặc thẻ `NOT_APPLICABLE` kèm giải trình.
- [ ] Nút CTA dẫn sang màn hình Tối ưu câu văn & Xuất file (SCR07).

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given báo cáo trả về điểm tổng hợp là 82 điểm và học vấn là NOT_APPLICABLE
When màn hình báo cáo được kết xuất
Then nhãn phân tầng hiển thị "Khá — Cần sửa nhỏ"
And hiển thị dòng ghi chú mẫu số chuẩn hóa 85

Given CV thiếu kỹ năng bắt buộc Docker được chỉ định trong JD
When báo cáo hiển thị
Then hộp cảnh báo màu vàng hiển thị rõ thông tin thiếu Docker ở khối độc lập cạnh điểm số (BR18)

Given người dùng bấm vào Trụ cột 1 (Kỹ năng)
When accordion mở ra
Then người dùng thấy rõ các từ khóa đạt 100%, 95%, 60% và 0% kèm bằng chứng
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
cd cvanalysis-client
npm test -- ReportView.test.tsx
```
