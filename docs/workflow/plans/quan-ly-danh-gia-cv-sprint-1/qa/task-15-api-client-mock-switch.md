# TASK-15: Axios API Client, TanStack Query Hooks & Mock Contracts Layer

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-15` |
| Folder & Phân bổ | `qa/` &bull; Phân công: **Dev 2 (Frontend) hỗ trợ tích hợp** |
| User Stories liên quan | Cầu nối kỹ thuật cho toàn bộ `US01` – `US20` |
| Nguồn Contracts G3 | Mục 4 `docs/workflow/architecture/cv-analysis-design.md` |
| Hard Prerequisites | Không (Bắt đầu ngay tại T0 kế thừa API Contracts từ G3) |
| Chặn các tasks | Các task UI `TASK-09` đến `TASK-14` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng tầng giao tiếp dữ liệu phía client bao gồm: Axios HTTP Client được cấu hình sẵn Interceptors (tự động đính kèm JWT Bearer token và bắt lỗi 401/403/500), các Custom Hooks của TanStack Query cho từng nhóm chức năng, và một tầng giả lập Mock Service Worker (MSW) phản ánh 100% API Contracts đã chốt tại Cổng G3. Tầng Mock này là yếu tố cốt lõi cho phép **Dev 2 (Frontend) phát triển độc lập và song song toàn bộ giao diện mà không phải chờ đợi Backend**.

---

## 2. Checklist Hành động

- [ ] Cài đặt `axios`, `@tanstack/react-query`, `msw` (Mock Service Worker).
- [ ] Xây dựng `apiClient` instance (`src/services/api/apiClient.ts`):
  - Base URL từ biến môi trường `VITE_API_BASE_URL`.
  - Request Interceptor: đính kèm Header `Authorization: Bearer <token>` nếu có trong `useAuthStore`.
  - Response Interceptor: xử lý tự động khi gặp lỗi `401 Unauthorized` (chuyển về đăng nhập) và hiển thị thông báo lỗi chuẩn hóa cho `403/404/500`.
- [ ] Khai báo đầy đủ TypeScript Interfaces phản ánh chính xác DTOs trong G3:
  - `CvExtractionDto`, `JobDescriptionDto`, `ReportScoreDto`, `OptimizedBulletDto`, `PaymentOrderDto`, `TicketDto`...
- [ ] Xây dựng Mock Handlers (`src/services/mocks/handlers.ts`):
  - Giả lập phản hồi cho toàn bộ các endpoint tại Mục 4 `cv-analysis-design.md`.
  - Hỗ trợ cờ chuyển đổi `VITE_USE_MOCK=true/false` trong `.env`.
- [ ] Đóng gói các TanStack Query Custom Hooks:
  - `useCvUpload()`, `useAnalysisStatus(requestId)`, `useReportDetails(reportId)`, `useBulletPoints(reportId)`, `useCreditPackages()`, `usePaymentOrders()`.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given biến môi trường VITE_USE_MOCK=true
When Dev 2 phát triển các màn hình UI và gọi API upload hoặc lấy điểm ATS
Then hệ thống trả về dữ liệu mẫu chuẩn G3 ngay lập tức mà không cần backend đang chạy

Given khi backend ASP.NET Core hoàn thành và cờ VITE_USE_MOCK=false
When chuyển sang gọi API thật
Then apiClient tự động kết nối đến backend URL và đính kèm JWT Bearer token hợp lệ
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
cd cvanalysis-client
npm test -- ApiClient.test.tsx
```
