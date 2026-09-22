# TASK-05: Tích hợp API AI, Giảm thiểu PII & Worker Giám sát Timeout 90s

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-05` |
| Folder & Phân bổ | `backend/` &bull; Phân công: **Dev 1 (Backend & Database)** |
| User Stories liên quan | `US02`, `US08`, `US10`, `US14` |
| Bảng cơ sở dữ liệu | `dbo.AnalysisRequests`, `dbo.Reports`, `dbo.UserBalances`, `dbo.CreditLedger` |
| Hard Prerequisites | `TASK-03`, `TASK-04` |
| Chặn các tasks | `TASK-06`, `TASK-16` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng module gọi API AI thương mại (OpenAI / Anthropic API client) tuân thủ chính sách không huấn luyện mô hình (BR33), áp dụng bộ lọc giảm thiểu thông tin định danh cá nhân (PII de-identification: che email, số điện thoại trước khi gửi — BR08), và triển khai `AnalysisTimeoutWorker` (.NET BackgroundService) định kỳ quét các tác vụ quá 90 giây để tự động ngắt kết nối, hủy tác vụ và trả lại credit đang giữ về ví của người dùng (BR04, OQ02).

---

## 2. Checklist Hành động

- [ ] Cài đặt NuGet: `Microsoft.Extensions.Http.Resilience` hoặc `Polly` cho HTTP timeout/retry.
- [ ] Xây dựng service `PiiSanitizer`:
  - Dùng Regex nhận diện và thay thế: Số điện thoại (`[PHONE_REDACTED]`), Email (`[EMAIL_REDACTED]`), Địa chỉ nhà cụ thể (`[ADDRESS_REDACTED]`).
- [ ] Xây dựng `CommercialAiClient`:
  - Cấu hình Header không lưu dữ liệu huấn luyện và thiết lập client timeout tối đa 75 giây.
  - Sinh bản viết lại gạch đầu dòng Action-Context-Metric bám sát dữ kiện thật (BR06).
- [ ] Xây dựng `AnalysisTimeoutWorker` kế thừa `BackgroundService`:
  - Chu kỳ quét: mỗi 5 giây một lần.
  - Truy vấn các `dbo.AnalysisRequests` có trạng thái `Queued` hoặc `Processing` mà `timeout_at < SYSDATETIMEOFFSET()`:
    - Bắt đầu Transaction SQL Server.
    - Đổi trạng thái request thành `TimedOut`, ghi `error_message = 'Quá thời gian xử lý 90 giây. Đã hoàn trả credit.'`
    - Cập nhật `dbo.UserBalances`: giảm `held_credits` đi 1, cộng lại 1 vào `free_trial_credits` hoặc `paid_credits` tương ứng.
    - Ghi bản ghi `dbo.CreditLedger` với loại `ReleaseHold`.
    - Commit Transaction.
- [ ] Xử lý lỗi một phần (Partial Failure 24h — BR17):
  - Nếu sinh xong báo cáo nhưng lỗi ở bản viết lại: lưu báo cáo hợp lệ, ghi `partial_result_expires_at = Now + 24 hours`, giải phóng credit giữ.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given văn bản CV có chứa số điện thoại "0901234567" và email "candidate@gmail.com"
When hệ thống chuẩn bị payload gửi sang API AI
Then dữ liệu gửi đi bị thay thế thành [PHONE_REDACTED] và [EMAIL_REDACTED] (BR08)

Given một tác vụ phân tích bị treo trong hàng đợi hoặc AI xử lý quá 90 giây
When AnalysisTimeoutWorker thực hiện chu kỳ quét
Then trạng thái tác vụ chuyển thành TimedOut
And số credit đang giữ (held_credits) được giải phóng về ví khả dụng ngay lập tức (BR04)

Given tác vụ sinh xong báo cáo nhưng API viết lại trả về mã lỗi 500
When hệ thống kết thúc tác vụ
Then phần báo cáo đã hoàn tất được lưu giữ trong 24 giờ và credit giữ được trả lại ví (BR17)
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
dotnet test tests/CVAnalysis.IntegrationTests --filter FullyQualifiedName~TimeoutWorkerTests
```
