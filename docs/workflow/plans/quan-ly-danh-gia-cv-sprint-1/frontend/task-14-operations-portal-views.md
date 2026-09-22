# TASK-14: Giao diện Phân hệ Quản trị 4 Vai trò Nội bộ (SCR11 – SCR14)

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-14` |
| Folder & Phân bổ | `frontend/` &bull; Phân công: **Dev 2 (Frontend & UI/UX)** |
| User Stories liên quan | `US13`, `US15`, `US18`, `US19`, `US20` |
| Màn hình liên quan | `SCR11`, `SCR12`, `SCR13`, `SCR14` |
| Hard Prerequisites | `TASK-08`, `TASK-15` |
| Chặn các tasks | `TASK-16` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng 4 giao diện phân hệ chuyên biệt dành cho 4 vai trò vận hành nội bộ theo đúng phân quyền rạch ròi đã được phê duyệt tại Cổng G1, G2 và G3:
1. **SCR11 (Hỗ trợ viên):** Tiếp nhận vụ việc được giao, xem CV được cấp quyền 72h, lập phiếu đề nghị bồi thường 1 credit.
2. **SCR12 (Lead hỗ trợ):** Phân công vụ việc, giám sát đồng hồ SLA 2 ngày làm việc, duyệt phiếu bồi thường credit lỗi kỹ thuật.
3. **SCR13 (Kế toán):** Quản lý cấu hình gói giá credit VND, bảng đối soát đơn hàng muộn/báo trễ và duyệt cộng credit.
4. **SCR14 (Quản trị viên hệ thống):** Quản lý tài khoản nội bộ, gán vai trò người dùng (US20) và bảng tra cứu Audit Logs bảo mật.

---

## 2. Checklist Hành động

- [ ] Xây dựng View `SCR11` (`SupportSpecialistView`):
  - Bảng danh sách vụ việc được phân công kèm badge thời hạn quyền xem CV (ví dụ: *"Quyền xem CV còn: 68 giờ"*).
  - Nút "Mở xem CV & Báo cáo" (gọi API có kiểm tra quyền).
  - Modal tạo phiếu đề nghị: nhập mô tả lỗi trích xuất/kỹ thuật và bấm "Gửi Lead duyệt".
- [ ] Xây dựng View `SCR12` (`SupportLeadView`):
  - Danh sách đề nghị bồi thường chờ duyệt kèm đồng hồ đếm ngược SLA 2 ngày làm việc.
  - Nút "Duyệt bồi thường 1 Credit" (kích hoạt bồi thường và đánh dấu báo cáo lỗi).
  - Nút "Từ chối đề nghị" kèm ô nhập lý do từ chối.
- [ ] Xây dựng View `SCR13` (`FinanceView`):
  - Bảng quản lý danh mục gói giá credit VND: cho phép tạo gói mới, sửa giá VND, bật/tắt bán gói.
  - Bảng đơn hàng `Chờ đối soát`: hiển thị mã đơn, thông tin ngân hàng, thời gian thanh toán muộn và nút "Duyệt cộng credit".
- [ ] Xây dựng View `SCR14` (`SystemAdminView`):
  - Bảng danh sách nhân viên nội bộ kèm dropdown chọn 1 trong 4 vai trò: `Nhân viên hỗ trợ`, `Trưởng nhóm hỗ trợ`, `Kế toán`, `Quản trị viên`.
  - Bảng tra cứu Audit Logs bất biến: hiển thị Actor, Hành động, Entity, IP, Thời điểm và chi tiết.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given tài khoản có vai trò SupportSpecialist đăng nhập
When mở danh sách vụ việc
Then chỉ nhìn thấy các vụ việc được phân công cho chính mình
And chỉ xem được nội dung CV khi có quyền hỗ trợ còn hiệu lực trong 72 giờ

Given Kế toán mở màn hình SCR13
When đối soát thấy tiền đã vào cổng hợp lệ cho đơn hàng muộn ORD-20260922-88
Then bấm duyệt cộng credit và hệ thống gửi thông báo xác nhận thành công trong 2 ngày làm việc

Given Quản trị viên mở màn hình SCR14
When gán vai trò "Kế toán / Tài chính" cho nhân viên mới
Then vai trò được cập nhật thành công và hiển thị bản ghi nhật ký kiểm toán tương ứng
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
cd cvanalysis-client
npm test -- OperationsViews.test.tsx
```
