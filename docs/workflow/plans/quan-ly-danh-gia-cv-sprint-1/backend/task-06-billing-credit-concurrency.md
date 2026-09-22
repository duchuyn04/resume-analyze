# TASK-06: Quản lý Credit, Khóa Giao dịch UPDLOCK, Webhook Thanh toán & Đối soát

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-06` |
| Folder & Phân bổ | `backend/` &bull; Phân công: **Dev 1 (Backend & Database)** |
| User Stories liên quan | `US13`, `US14`, `US15` |
| Bảng cơ sở dữ liệu | `dbo.UserBalances`, `dbo.CreditPackages`, `dbo.PaymentOrders`, `dbo.CreditLedger` |
| Hard Prerequisites | `TASK-01`, `TASK-02`, `TASK-05` |
| Chặn các tasks | `TASK-07`, `TASK-16` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng hệ thống quản lý credit với cơ chế khóa nguyên tử `UPDLOCK, ROWLOCK` trên SQL Server để loại bỏ hoàn toàn race condition số dư (ADR-002), triển khai quy trình tạo đơn mua credit có hạn 15 phút, xử lý Webhook cổng thanh toán an toàn với chữ ký điện tử HMAC và bảo vệ Idempotency (chống cộng credit hai lần), và cung cấp API đối soát đơn hàng muộn cho vai trò Kế toán (SLA 2 ngày làm việc — BR11, OQ05).

---

## 2. Checklist Hành động

- [ ] Xây dựng Command `HoldCreditCommand` (ADR-002):
  - Mở Transaction với `IsolationLevel.ReadCommitted`.
  - Thực thi câu lệnh SQL với gợi ý khóa:
    ```sql
    SELECT * FROM dbo.UserBalances WITH (UPDLOCK, ROWLOCK) WHERE user_id = @userId
    ```
  - Kiểm tra `(free_trial_credits + paid_credits) > 0`:
    - Nếu $= 0 \rightarrow$ ném ngoại lệ `InsufficientCreditsException` và rollback.
    - Nếu $> 0 \rightarrow$ trừ 1 credit khả dụng, tăng 1 `held_credits`.
  - Ghi bản ghi `dbo.CreditLedger` loại `Hold`. Commit Transaction.
- [ ] Xây dựng Command `ConsumeCreditCommand`:
  - Chỉ thu khi trọn bộ kết quả đã sẵn sàng (BR13, OQ03): giảm 1 `held_credits`, ghi `CreditLedger` loại `ConsumePaid` hoặc `ConsumeFree`.
- [ ] Xây dựng Command `CreatePaymentOrderCommand` (US13):
  - Kiểm tra gói credit hợp lệ trong `dbo.CreditPackages`.
  - Sinh mã đơn duy nhất `order_code = ORD-YYYYMMDD-XXXX`.
  - Đặt `expires_at = SYSDATETIMEOFFSET() + 15 minutes`.
  - Trả về thông tin đơn hàng và mã QR thanh toán (VietQR format).
- [ ] Xây dựng Webhook Controller `POST /api/v1/webhooks/payment`:
  - Kiểm tra chữ ký HMAC-SHA256 từ Header `X-Signature`.
  - Tra cứu `dbo.PaymentOrders` theo mã đơn:
    - Nếu đơn đã ở trạng thái `Completed` hoặc `AwaitingReconciliation`: trả về HTTP 200 OK ngay lập tức (Idempotency).
    - Nếu thanh toán trong hạn 15 phút: cập nhật đơn `Completed`, cộng đúng số credit gói mua vào `dbo.UserBalances`, ghi `CreditLedger` loại `ManualCreditAdd`.
    - Nếu thanh toán sau 15 phút: cập nhật đơn sang `AwaitingReconciliation` (Chờ đối soát) để Kế toán xử lý (BR11, OQ05).
- [ ] Xây dựng API Đối soát dành riêng cho Role Finance `POST /api/v1/operations/finance/reconcile-order`:
  - Chỉ cho phép vai trò `Finance` truy cập.
  - Chuyển đơn từ `AwaitingReconciliation` sang `Completed`, cộng credit cho khách hàng, ghi nhật ký đối soát.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given tài khoản chỉ còn đúng 1 credit khả dụng
When người dùng mở 2 tab và gửi 2 yêu cầu phân tích đồng thời
Then hệ thống áp dụng khóa UPDLOCK xử lý tuần tự: đúng 1 yêu cầu thành công đưa vào chờ
And yêu cầu còn lại bị từ chối với lỗi InsufficientCreditsException, số dư không bao giờ âm

Given webhook cổng thanh toán gửi lại 3 lần cho cùng một giao dịch thành công
When hệ thống tiếp nhận các request webhook
Then chỉ có lần đầu tiên thực hiện cộng credit vào tài khoản
And các lần gửi lặp sau trả về 200 OK mà không cộng thêm credit (Idempotency)

Given khách chuyển tiền sau khi đơn 15 phút đã hết hạn 3 phút
When webhook cổng báo nhận tiền thành công
Then hệ thống không tự hủy mà chuyển đơn sang trạng thái AwaitingReconciliation (Chờ đối soát)
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
dotnet test tests/CVAnalysis.IntegrationTests --filter FullyQualifiedName~CreditConcurrencyTests
```
