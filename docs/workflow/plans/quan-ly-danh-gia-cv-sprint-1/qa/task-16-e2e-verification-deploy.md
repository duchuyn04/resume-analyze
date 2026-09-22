# TASK-16: Tích hợp Toàn diện End-to-End, Kiểm thử Walking Skeleton & Nghiệm thu

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-16` |
| Folder & Phân bổ | `qa/` &bull; Phân công: **Cả 2 thành viên (Dev 1 & Dev 2)** |
| User Stories liên quan | Nghiệm thu toàn bộ `US01` – `US20` |
| Điểm hội tụ | **Sync Checkpoint 3 (Thời điểm T4)** |
| Hard Prerequisites | Toàn bộ các tasks từ `TASK-01` đến `TASK-15` đã hoàn thành |
| Chặn các tasks | Không (Nghiệm thu đóng Sprint 1) |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Tiến hành ghép nối toàn diện giữa Frontend (React Client) và Backend (ASP.NET Core Web API + SQL Server), tắt chế độ Mock để chạy 100% trên API và Cơ sở dữ liệu thật, thực thi kiểm thử luồng giá trị cốt lõi (Walking Skeleton) từ đầu đến cuối, kiểm chứng các bất biến bảo mật Zero-Trust và kiểm soát tranh chấp credit, và chuẩn bị hồ sơ bàn giao nghiệm thu cho Cổng Delivery Inspection.

---

## 2. Checklist Hành động

- [ ] Cấu hình môi trường tích hợp nội bộ:
  - Khởi chạy SQL Server Database và chạy toàn bộ Migrations (`dotnet ef database update`).
  - Khởi chạy ASP.NET Core Web API trên `http://localhost:5000`.
  - Khởi chạy React Client kết nối với API thật (`VITE_USE_MOCK=false`, `VITE_API_BASE_URL=http://localhost:5000/api/v1`).
- [ ] Kiểm thử kịch bản Walking Skeleton chính (Happy Path):
  1. Đăng ký email mới $\rightarrow$ nhận email verify $\rightarrow$ kích hoạt tài khoản và nhận 3 lượt miễn phí (US01).
  2. Kéo thả file CV `NguyenVanA_Fullstack.pdf` (1.4MB, 2 trang) $\rightarrow$ xem và xác nhận nội dung trích xuất (US04, US05).
  3. Dán JD 250 từ hợp lệ $\rightarrow$ đồng ý điều khoản AI (US02, US06).
  4. Bấm chạy phân tích đối chiếu $\rightarrow$ số dư bị giữ 1 lượt $\rightarrow$ hiển thị màn hình chờ (US08, US14).
  5. Hệ thống hoàn tất $\rightarrow$ thu 1 lượt, hiển thị Bảng điểm ATS 4 trụ cột kèm cảnh báo bắt buộc (US09).
  6. Mở tab Tối ưu $\rightarrow$ kiểm tra Diff view $\rightarrow$ chỉnh sửa thủ công 1 câu $\rightarrow$ bấm Xuất PDF $\rightarrow$ file PDF tải về máy thành công (US10, US11, US12).
- [ ] Kiểm thử kịch bản Bất biến & Ranh giới (Edge Cases & Invariants):
  - Kiểm tra tranh chấp: Mở 2 tab chạy đồng thời khi còn 1 credit $\rightarrow$ xác nhận khóa `UPDLOCK` chặn âm số dư (ADR-002).
  - Kiểm tra Circuit Breaker 90s: Mô phỏng AI treo quá 90s $\rightarrow$ xác nhận tác vụ hủy và credit được hoàn trả về ví (BR04).
  - Kiểm tra Zero-Trust: Đăng nhập bằng tài khoản Hỗ trợ viên $\rightarrow$ cố gắng mở CV không được cấp quyền $\rightarrow$ xác nhận bị chặn lỗi 403 (BR07).
  - Kiểm tra Đối soát 2 ngày: Thanh toán đơn quá 15 phút $\rightarrow$ Kế toán duyệt cộng credit trên màn hình SCR13 thành công (BR11).

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given toàn bộ hệ thống frontend và backend đang chạy kết nối SQL Server thật
When ứng viên thực hiện đầy đủ luồng từ đăng ký đến tải file CV tối ưu về máy
Then toàn bộ luồng hoạt động mượt mà không có lỗi 500
And số dư tài khoản được cập nhật chính xác từng bước
And file PDF tải về mở được bình thường và có lớp text bôi đen được

Given các kịch bản kiểm tra bất biến (UPDLOCK, 90s timeout, Zero-Trust 72h)
When chạy bộ kiểm thử tích hợp
Then 100% các bất biến được bảo toàn trọn vẹn, không có rò rỉ dữ liệu hoặc âm credit
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
# Backend Test Suite
dotnet test CVAnalysis.sln

# Frontend Build Check
cd cvanalysis-client && npm run build
```
