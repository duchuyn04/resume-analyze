# TASK-02: Dịch vụ Xác thực, RBAC 5 Roles, Zero-Trust Handler & Email Hash Registry

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-02` |
| Folder & Phân bổ | `backend/` &bull; Phân công: **Dev 1 (Backend & Database)** |
| User Stories liên quan | `US01`, `US02`, `US03`, `US18`, `US20` |
| Bảng cơ sở dữ liệu | `dbo.Users`, `dbo.EmailHashRegistry`, `dbo.UserBalances`, `dbo.SupportAccessGrants` |
| Hard Prerequisites | `TASK-01` (Cần DbContext và bảng Users) |
| Chặn các tasks | `TASK-06`, `TASK-07` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng hệ thống xác thực người dùng bằng JWT Bearer Token, hỗ trợ đầy đủ 5 vai trò hệ thống (`Candidate`, `SupportSpecialist`, `SupportLead`, `Finance`, `SystemAdmin`), cấp 3 lượt dùng thử miễn phí có kiểm tra chặn lạm dụng 12 tháng qua `dbo.EmailHashRegistry` (BR34), và thiết lập `ZeroTrustCvAccessHandler` để kiểm soát quyền xem CV tạm thời tối đa 72h theo vụ việc (ADR-003).

---

## 2. Checklist Hành động

- [ ] Cài đặt NuGet: `Microsoft.AspNetCore.Authentication.JwtBearer`, `System.IdentityModel.Tokens.Jwt`, `BCrypt.Net-Next`.
- [ ] Xây dựng Command `RegisterCandidateCommand`:
  - Mã hóa mật khẩu bằng BCrypt.
  - Tạo token xác minh email ngẫu nhiên có hạn 24 giờ.
- [ ] Xây dựng Command `VerifyEmailCommand`:
  - Kích hoạt `is_email_verified = true`.
  - Tính SHA-256 hash của email, kiểm tra bảng `dbo.EmailHashRegistry`:
    - Nếu hash tồn tại và `trial_blocked_until > SYSDATETIMEOFFSET()`: tạo `UserBalance` với `free_trial_credits = 0`.
    - Nếu không tồn tại: tạo `UserBalance` với `free_trial_credits = 3`.
- [ ] Cấu hình JWT Token Generator với Claims: `Sub` (UserId), `Email`, `Role` (5 roles).
- [ ] Xây dựng Command `CloseAccountCommand` (US03):
  - Xác thực lại mật khẩu.
  - Lưu SHA-256 hash email vào `dbo.EmailHashRegistry` với hạn 12 tháng.
  - Xóa vĩnh viễn dữ liệu user và các file CV liên quan.
- [ ] Xây dựng `CvAccessRequirement` và `ZeroTrustCvAccessHandler`:
  - Kiểm tra xem user hiện tại có phải là chủ sở hữu CV không (`UserId == CV.UserId`).
  - Nếu là nhân viên hỗ trợ/lead: kiểm tra bản ghi `dbo.SupportAccessGrants` có `granted_to_user_id == CurrentUser`, `ticket_id` khớp, `expires_at > Now` và `revoked_at IS NULL`. Nếu không thỏa $\rightarrow$ trả về `403 Forbidden` và ghi nhật ký cảnh báo.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given email mới đăng ký và xác thực lần đầu
When người dùng xác thực email thành công
Then tài khoản được kích hoạt và ví nhận đúng 3 lượt miễn phí

Given email từng đóng tài khoản trong vòng 12 tháng trước đó
When đăng ký lại và xác thực email
Then tài khoản được kích hoạt nhưng số dư lượt miễn phí là 0 (BR34)

Given nhân viên hỗ trợ đăng nhập cố gắng truy cập file CV của khách hàng
When không có bản ghi SupportAccessGrants hợp lệ còn trong hạn 72h
Then hệ thống trả về mã lỗi 403 Forbidden và ghi log vào dbo.AuditLogs
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
dotnet test tests/CVAnalysis.UnitTests --filter FullyQualifiedName~AuthServiceTests
```
