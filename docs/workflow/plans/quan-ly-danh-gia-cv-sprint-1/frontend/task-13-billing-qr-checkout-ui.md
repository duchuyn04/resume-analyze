# TASK-13: Giao diện Bảng Gói Credit, Modal VietQR & Đếm Ngược 15 Phút

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-13` |
| Folder & Phân bổ | `frontend/` &bull; Phân công: **Dev 2 (Frontend & UI/UX)** |
| User Stories liên quan | `US13`, `US15` |
| Màn hình liên quan | `SCR08` (Mua Credit & Thanh toán) |
| Hard Prerequisites | `TASK-08`, `TASK-15` |
| Chặn các tasks | `TASK-16` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng giao diện xem và lựa chọn các gói credit VND cố định (SCR08), Modal hiển thị mã QR thanh toán (VietQR) kèm đồng hồ đếm ngược 15 phút thời gian thực (BR28, OQ05), cơ chế tự động kiểm tra trạng thái thanh toán (polling/webhook status) để thông báo cộng credit tức thời khi thành công, và hiển thị thông báo chuyển sang "Chờ đối soát" nếu người dùng thanh toán sau khi hết hạn 15 phút (BR11).

---

## 2. Checklist Hành động

- [ ] Xây dựng Component `CreditPackagesGrid`:
  - Lấy danh sách gói credit từ API GET `/api/v1/billing/packages`.
  - Hiển thị thẻ gói: Gói 5 Credit (100.000đ), Gói 15 Credit (250.000đ)...
  - Nút "Thanh toán ngay" kích hoạt tạo đơn hàng mới.
- [ ] Xây dựng Component `QrCheckoutModal` (US13):
  - Nhận thông tin đơn hàng từ API POST `/api/v1/billing/orders`: `orderCode`, `amountVnd`, `qrUrl`, `expiresAt`.
  - Hiển thị ảnh mã VietQR với số tiền cố định và nội dung chuyển khoản chuẩn.
  - Đồng hồ đếm ngược 15 phút (`15:00` $\rightarrow$ `00:00`):
    - Nếu hết 15 phút mà chưa nhận được tiền: chuyển sang cảnh báo hết hạn phiên thanh toán.
- [ ] Cơ chế lắng nghe trạng thái đơn hàng (Order Status Polling):
  - Gửi request mỗi 3 giây kiểm tra trạng thái đơn:
    - Nếu `Completed`: hiển thị hiệu ứng pháo hoa / modal thành công: *"Thanh toán thành công! Đã cộng X credit vào ví"*, cập nhật số dư trên thanh Header.
    - Nếu `AwaitingReconciliation`: thông báo *"Thanh toán của bạn đang được kế toán đối soát và sẽ cộng credit trong tối đa 2 ngày làm việc (BR11)"*.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given người dùng chọn Gói 5 credit và bấm thanh toán
When Modal QR hiển thị
Then màn hình xuất hiện mã VietQR đúng số tiền 100.000đ kèm đồng hồ đếm ngược 15 phút

Given người dùng hoàn tất thanh toán trên app ngân hàng
When webhook cổng cập nhật trạng thái đơn thành Completed
Then modal tự động đóng, giao diện hiển thị thông báo thành công và số dư credit trên Header tăng thêm 5
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
cd cvanalysis-client
npm test -- QrCheckoutModal.test.tsx
```
