# TASK-28: Tích hợp Toàn diện End-to-End, Kiểm thử 4 Simulator & Nghiệm thu Sprint 2

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-28` |
| Folder & Phân bổ | `qa/` &bull; Phân công: **Cả 2 thành viên (Dev 1 & Dev 2)** |
| User Stories liên quan | Nghiệm thu toàn bộ `US21` – `US25` |
| Điểm hội tụ | **Sync Checkpoint 2 (Sprint 2)** |
| Hard Prerequisites | Toàn bộ các tasks từ `TASK-21` đến `TASK-27` đã hoàn thành |
| Chặn các tasks | Không (Nghiệm thu đóng Sprint 2) |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Tiến hành ghép nối toàn diện giữa Frontend (Tab ATS Reader View, Ma trận Điểm %, Modal Auto-fix) và Backend (ASP.NET Core Web API, 4 Simulators, SQL Server `dbo.AtsSimulations`), chạy bộ kiểm thử tích hợp trên các file CV mẫu có lỗi layout thực tế (CV 2 cột, CV có số điện thoại ở Header, CV quét không có JD), kiểm chứng tính toàn vẹn của bất biến 1 credit và dọn dẹp dữ liệu khi xóa CV, chuẩn bị bàn giao nghiệm thu Sprint 2.

---

## 2. Checklist Hành động

- [ ] Cấu hình môi trường tích hợp:
  - Chạy migration `AddAtsSimulationsTable` trên cơ sở dữ liệu SQL Server.
  - Khởi chạy Backend Web API và React Client kết nối trực tiếp (`VITE_USE_MOCK=false`).
- [ ] Kiểm thử Kịch bản 1 — CV 2 Cột phức tạp (Workday & Taleo):
  - Tải file mẫu `CV_Canva_2Column_HeaderContact.pdf`.
  - Chạy phân tích nâng cao $\rightarrow$ mở tab `ATS Reader View`.
  - Xác nhận:
    - Góc nhìn Workday bôi vàng đoạn xáo trộn dòng, điểm Workday bị trừ đúng 30%.
    - Góc nhìn Taleo bôi đỏ đoạn mất số điện thoại/email, điểm Taleo bị trừ đúng 40%.
- [ ] Kiểm thử Kịch bản 2 — Quét độc lập không cần JD (US25):
  - Tải file CV và chọn *"Quét tương thích ATS (Không cần JD)"*.
  - Xác nhận: API trả về kết quả nhanh dưới 15 giây, tiêu chí từ khóa Greenhouse hiển thị `N/A`, điểm số chỉ phản ánh lỗi định dạng kỹ thuật.
- [ ] Kiểm thử Kịch bản 3 — Auto-fix Layout 1 Cột (US24):
  - Bấm *"Tự động sửa lỗi bố cục khi xuất file"* $\rightarrow$ Modal SCR16 hiển thị so sánh.
  - Bấm tải file PDF đã chuẩn hóa.
  - Đưa file PDF vừa tải tải ngược lại vào hệ thống để quét lại: xác nhận điểm số của cả 4 hệ thống đạt 100% an toàn định dạng.
- [ ] Kiểm thử Bất biến Dữ liệu & Xóa Cascade:
  - Bấm xóa CV gốc $\rightarrow$ kiểm tra database SQL Server xác nhận bản ghi `dbo.AtsSimulations` tương ứng tự động bị xóa đồng thời (`ON DELETE CASCADE` — BR10, SR07).

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given ứng viên mở xem tab ATS Reader View cho file CV 2 cột
When chuyển đổi qua lại giữa 4 hệ thống Workday, Taleo, Greenhouse, Lever
Then giao diện hiển thị chính xác chuỗi text sau bóc tách và các điểm bôi màu lỗi
And ma trận 4 điểm tương thích hiển thị đúng tỷ lệ % theo công thức đã chốt

Given file CV được xuất với tùy chọn Auto-fix Layout 1 cột
When kiểm tra cấu trúc file tải về
Then file hoàn toàn không còn bảng phức tạp hay cột đôi
And đạt 100% điểm an toàn định dạng khi quét lại qua 4 simulators
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
# Backend Test Suite cho Simulators
dotnet test src/CVAnalysis.sln --filter Category=AtsSimulation

# Frontend Test Suite cho UI Reader View
cd cvanalysis-client && npm test -- ats-simulation
```
