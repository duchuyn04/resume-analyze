# TASK-08: Khởi tạo React Vite, App Shell Layout & Navigation 5 Roles

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-08` |
| Folder & Phân bổ | `frontend/` &bull; Phân công: **Dev 2 (Frontend & UI/UX)** |
| User Stories liên quan | Nền tảng giao diện cho toàn bộ `US01` – `US20` |
| Màn hình liên quan | Khung giao diện chung cho `SCR01` – `SCR14` |
| Hard Prerequisites | Không (Bắt đầu ngay tại thời điểm T0 song song với Backend) |
| Chặn các tasks | `TASK-09`, `TASK-10`, `TASK-11`, `TASK-12`, `TASK-13`, `TASK-14` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Khởi tạo ứng dụng frontend `cvanalysis-client` bằng **React 18/19 (Vite + TypeScript + Tailwind CSS)**, xây dựng App Shell hoàn chỉnh kế thừa trực tiếp từ bản Prototype Mockup đã duyệt tại `docs/workflow/prototypes/cv-analysis-mockup.html`, bao gồm: Thanh điều hướng trên cùng, Huy hiệu số dư lượt cơ bản / credit nâng cao, Nút nạp credit nhanh, và Bộ chuyển đổi vai trò (5 Roles Switcher).

---

## 2. Checklist Hành động

- [ ] Khởi tạo dự án React: `npm create vite@latest cvanalysis-client -- --template react-ts`.
- [ ] Cài đặt các thư viện thiết yếu:
  - `tailwindcss`, `postcss`, `autoprefixer`
  - `lucide-react` (icons)
  - `zustand` (quản lý state ứng dụng & user auth/credits)
  - `@tanstack/react-query` (quản lý server state)
  - `axios` (HTTP client)
- [ ] Cấu hình Tailwind CSS với font chữ `Inter`.
- [ ] Xây dựng Component `AppHeader`:
  - Logo `CVScore.vn`.
  - Bộ chọn 5 Roles (`<select>`): Ứng viên, Hỗ trợ viên, Lead hỗ trợ, Kế toán, Quản trị viên.
  - Khối hiển thị số dư: `Lượt miễn phí: 3`, `Credit nâng cao: X credit`.
  - Nút "Nạp Credit" kích hoạt modal thanh toán.
- [ ] Xây dựng Component `CandidateNav`:
  - Tab điều hướng: `Tải CV & Phân tích`, `Báo cáo ATS`, `Tối ưu & Xuất file`, `Lịch sử (90 ngày)`.
- [ ] Tạo `AuthContext` / `useAuthStore` lưu trữ token JWT, thông tin người dùng và vai trò hiện tại.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given người dùng mở ứng dụng trên trình duyệt
When trang web được tải thành công
Then giao diện hiển thị App Shell chuẩn Tailwind CSS sắc nét, không có lỗi console
And thanh điều hướng hiển thị đúng số dư lượt miễn phí và credit của người dùng
And người dùng có thể chuyển đổi mượt mà giữa các tab điều hướng
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
cd cvanalysis-client
npm run build
npm run preview
```
