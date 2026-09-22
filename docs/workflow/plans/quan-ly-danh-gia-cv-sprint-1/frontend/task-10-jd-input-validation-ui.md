# TASK-10: Giao diện Nhập JD, Bộ Đếm Từ Căn Cứ & Modal Đồng Ý Xử Lý AI

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-10` |
| Folder & Phân bổ | `frontend/` &bull; Phân công: **Dev 2 (Frontend & UI/UX)** |
| User Stories liên quan | `US02`, `US06`, `US07`, `US08` |
| Màn hình liên quan | `SCR03` (Nhập JD & Chọn chế độ), Modal `US02` (AI Consent) |
| Hard Prerequisites | `TASK-08`, `TASK-15` |
| Chặn các tasks | `TASK-16` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng ô nhập liệu mô tả công việc (JD Textarea) với cơ chế đếm từ thời gian thực (Real-time Word Counter) hiển thị màu sắc trực quan (vàng: $< 50$ từ chưa đủ căn cứ; xanh: $50 \le N \le 2500$ từ đủ căn cứ; đỏ: $> 2500$ từ), hỗ trợ nút chọn "Kiểm tra CV tổng quát (Không cần JD)" (US07), và xây dựng Modal thông báo cam kết quyền riêng tư AI (US02) yêu cầu người dùng chủ động đồng ý trước khi gửi dữ liệu sang AI bên ngoài.

---

## 2. Checklist Hành động

- [ ] Xây dựng Component `JdInputBox`:
  - Khung Textarea lớn với placeholder hướng dẫn chi tiết.
  - Hàm `calculateWordCount(text)`: đếm từ dựa trên khoảng trắng và ký tự có nghĩa.
  - Bộ nhãn trạng thái động:
    - $= 0$ từ: hiển thị xám *"0 từ (Yêu cầu: 50 – 2.500 từ; hoặc để trống để kiểm tra tổng quát)"*.
    - $< 50$ từ: hiển thị màu vàng hổ phách (Amber) *"X từ (Chưa đủ căn cứ, tối thiểu 50 từ — BR35)"*.
    - $50 \le N \le 2500$ từ: hiển thị màu xanh ngọc (Emerald) *"X từ (Đủ căn cứ đối chiếu chuẩn ATS)"*.
    - $> 2500$ từ: hiển thị màu đỏ (Rose) *"X từ (Vượt quá giới hạn 2.500 từ)"*.
- [ ] Xây dựng Component `AiConsentModal` (US02):
  - Hiển thị 3 cam kết an toàn dữ liệu:
    1. Không huấn luyện mô hình (Non-training agreement).
    2. Che thông tin định danh cá nhân (PII de-identification).
    3. Nhật ký giám sát lạm dụng được xóa sau tối đa 30 ngày.
  - Nút "Tôi đồng ý & Tiếp tục" $\rightarrow$ kích hoạt API bắt đầu phân tích.
  - Nút "Từ chối" $\rightarrow$ đóng modal, giữ nguyên CV trong tài khoản, không trừ lượt/credit (BR29).
- [ ] Xây dựng màn hình chờ `SCR05` (Loading Skeleton & Progress Bar 0–90s):
  - Hiệu ứng thanh tiến trình đếm thời gian với thông báo: *"Đang phân tích đối chiếu... Quá trình này thường mất 15–45 giây"*.
  - Nút "Hủy tác vụ" khi còn ở hàng đợi chờ (BR38).

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given người dùng dán đoạn văn bản JD có 32 từ vào ô nhập liệu
When kiểm tra trạng thái bộ đếm từ
Then nhãn đếm từ hiển thị màu vàng cảnh báo "Chưa đủ căn cứ, tối thiểu 50 từ"
And nút "Phân tích đối chiếu JD" hiển thị cảnh báo gợi ý bổ sung hoặc chuyển sang chế độ tổng quát

Given người dùng dán đoạn JD 250 từ hợp lệ và bấm bắt đầu phân tích
When Modal cam kết AI hiển thị và người dùng bấm "Từ chối"
Then modal đóng lại, không có request phân tích nào được gửi, số dư credit giữ nguyên (BR29)
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
cd cvanalysis-client
npm test -- JdInputBox.test.tsx
```
