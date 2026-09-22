# TASK-07: Phân hệ API Vận hành 4 Vai trò (Hỗ trợ, Lead, Kế toán, Admin)

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-07` |
| Folder & Phân bổ | `backend/` &bull; Phân công: **Dev 1 (Backend & Database)** |
| User Stories liên quan | `US13`, `US15`, `US18`, `US19`, `US20` |
| Bảng cơ sở dữ liệu | `dbo.SupportTickets`, `dbo.SupportAccessGrants`, `dbo.CompensationRequests`, `dbo.AuditLogs`, `dbo.Users` |
| Hard Prerequisites | `TASK-02`, `TASK-06` |
| Chặn các tasks | `TASK-16` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng toàn bộ các API dành cho 4 phân hệ vận hành nội bộ theo đúng phân quyền rạch ròi đã được phê duyệt tại Cổng G1 & G2:
1. **Nhân viên hỗ trợ (Support Specialist):** Tiếp nhận vụ việc, đọc CV được cấp quyền 72h, lập phiếu đề nghị bồi thường.
2. **Trưởng nhóm hỗ trợ (Support Lead):** Phân công vụ việc, giám sát SLA 2 ngày làm việc, phê duyệt bồi thường lỗi kỹ thuật (BR37).
3. **Kế toán / Tài chính (Finance):** Quản trị bảng giá gói credit VND, đối soát và duyệt credit đơn muộn (BR11, BR28).
4. **Quản trị viên hệ thống (System Admin):** Quản lý người dùng nội bộ, gán vai trò (US20) và giám sát nhật ký kiểm toán bất biến `dbo.AuditLogs`.

---

## 2. Checklist Hành động

- [ ] Nhóm API Hỗ trợ viên (`/api/v1/operations/support/` — Role `SupportSpecialist`):
  - `GET /assigned-tickets`: Danh sách vụ việc được phân công.
  - `GET /tickets/{id}/view-cv`: Xem nội dung CV/báo cáo (qua `ZeroTrustCvAccessHandler` kiểm tra hạn 72h).
  - `POST /tickets/{id}/propose-compensation`: Lập phiếu đề nghị bồi thường 1 credit (chuyển sang `PendingApproval`).
- [ ] Nhóm API Trưởng nhóm hỗ trợ (`/api/v1/operations/lead/` — Role `SupportLead`):
  - `POST /tickets/{id}/assign`: Phân công vụ việc cho nhân viên hỗ trợ.
  - `GET /compensation-proposals`: Danh sách đề xuất bồi thường chờ duyệt kèm đồng hồ SLA 2 ngày.
  - `POST /compensation-proposals/{id}/decide`: Phê duyệt hoặc từ chối bồi thường:
    - Nếu duyệt: cộng 1 credit/lượt cho khách hàng, đánh dấu báo cáo liên quan là `is_system_error = 1` (BR37).
- [ ] Nhóm API Kế toán (`/api/v1/operations/finance/` — Role `Finance`):
  - `GET /packages`: Danh sách gói giá; `POST /packages`: Tạo gói credit VND mới (BR28, OQ08).
  - `PUT /packages/{id}/toggle`: Ngừng bán hoặc kích hoạt gói giá.
  - `GET /reconciliation-orders`: Danh sách đơn `AwaitingReconciliation`.
  - `POST /reconcile-order`: Duyệt cộng credit đơn thanh toán muộn (BR11, OQ05).
- [ ] Nhóm API Quản trị viên (`/api/v1/operations/admin/` — Role `SystemAdmin`):
  - `GET /users`: Danh sách nhân viên nội bộ.
  - `POST /assign-role`: Gán vai trò cho nhân viên (`SupportSpecialist`, `SupportLead`, `Finance`, `SystemAdmin`).
  - `GET /audit-logs`: Tra cứu nhật ký bảo mật có phân trang và lọc theo hành vi.
- [ ] Xây dựng `AuditLogInterceptor` / `AuditLogService` tự động ghi nhận bản ghi vào `dbo.AuditLogs` cho mọi hành động quản trị nhạy cảm.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given nhân viên hỗ trợ được giao xử lý vụ việc khiếu nại của ứng viên
When ứng viên đã bấm cấp quyền hỗ trợ còn hạn 72 giờ
Then nhân viên hỗ trợ đọc được nội dung CV và hệ thống ghi 1 dòng audit log loại ViewCVContent

Given phiếu đề nghị bồi thường do hỗ trợ viên lập
When Trưởng nhóm hỗ trợ bấm phê duyệt phiếu đề nghị
Then tài khoản ứng viên được cộng đúng 1 credit (hoặc 1 lượt cơ bản)
And báo cáo bị khiếu nại được gắn cờ is_system_error = 1 để loại khỏi cache nộp lại (BR37)

Given tài khoản có role SupportSpecialist hoặc Finance
When cố gắng gọi API gán vai trò POST /api/v1/operations/admin/assign-role
Then hệ thống chặn truy cập với mã lỗi 403 Forbidden
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
dotnet test tests/CVAnalysis.IntegrationTests --filter FullyQualifiedName~OperationsApiTests
```
