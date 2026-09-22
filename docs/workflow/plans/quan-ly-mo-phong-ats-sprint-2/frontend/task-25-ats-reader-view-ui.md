# TASK-25: Giao diện Tab 'ATS Reader View' & Chuyển đổi 4 Góc Nhìn (SCR15)

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-25` |
| Folder & Phân bổ | `frontend/` &bull; Phân công: **Dev 2 (Frontend & UI/UX)** |
| User Stories liên quan | `US21` |
| Màn hình liên quan | `SCR15` (Tab ATS Reader View & Ma trận Tương thích) |
| Hard Prerequisites | Không (Bắt đầu ngay tại T0 kế thừa API Contracts G3) |
| Chặn các tasks | `TASK-26`, `TASK-27` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng tab giao diện `ATS Reader View` trên màn hình Báo cáo kết quả (SCR15), cho phép ứng viên bấm chuyển đổi trực quan giữa 4 góc nhìn của các hệ thống tuyển dụng hàng đầu (Workday, Oracle Taleo, Greenhouse, Lever), hiển thị văn bản thô bóc tách và tự động bôi màu các đoạn lỗi (đỏ: mất chữ; vàng: xáo trộn dòng; xám: lỗi font) để người dùng thấy tận mắt nguyên nhân CV có thể bị hệ thống loại bỏ.

---

## 2. Checklist Hành động

- [ ] Tạo thư mục `cvanalysis-client/src/features/ats-simulation/`.
- [ ] Xây dựng Component `AtsReaderViewTab`:
  - Thanh chọn 4 hệ thống (System Selector):
    - *Góc nhìn Workday* (biểu tượng Workday)
    - *Góc nhìn Oracle Taleo* (biểu tượng Taleo)
    - *Góc nhìn Greenhouse* (biểu tượng Greenhouse)
    - *Góc nhìn Lever* (biểu tượng Lever)
  - Bộ đệm dữ liệu (state) lưu góc nhìn đang chọn.
- [ ] Xây dựng Component `RawTextParserRenderer`:
  - Khung văn bản mô phỏng màn hình console / tài liệu thô (font `Geist Mono` hoặc `font-mono`).
  - Hệ thống highlight đoạn văn bản theo vị trí (token offset / line index):
    - `highlight-stripped` (Đỏ): Vạch gạch ngang màu đỏ kèm tooltip: *"Bị Taleo xóa do nằm ở Header/Footer"*.
    - `highlight-interleaved` (Vàng): Khung viền vàng kèm tooltip: *"Đoạn văn bị đọc đan xen ngang giữa 2 cột trên Workday"*.
    - `highlight-corrupted` (Xám): Nền xám cho ký tự lỗi mã hóa.
- [ ] Chú thích mã màu trực quan (Color Legend) đặt ngay dưới khung văn bản.
- [ ] Đảm bảo chuyển đổi tab mượt mà, không giật lag và miễn phí 100% trong suốt 90 ngày (SR07).

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given ứng viên đang xem báo cáo phân tích nâng cao
When bấm vào tab "ATS Reader View" và chọn "Góc nhìn Oracle Taleo"
Then khung văn bản hiển thị chuỗi text sau khi Taleo bóc tách
And số điện thoại nằm ở Header bị gạch đỏ và hiển thị chú thích cảnh báo

Given ứng viên chuyển sang tab "Góc nhìn Workday"
When CV gốc có thiết kế 2 cột song song
Then các dòng văn bản bị đọc nối ngang được đóng khung màu vàng cảnh báo
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
cd cvanalysis-client
npm test -- AtsReaderViewTab.test.tsx
```
