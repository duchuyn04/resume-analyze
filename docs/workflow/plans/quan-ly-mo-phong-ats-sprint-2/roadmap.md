# Lộ trình Thực thi Quản lý mô phỏng ATS + sprint 2 (Delivery Planning)

| Thuộc tính | Giá trị |
|---|---|
| Artifact ID | ROADMAP-SIMULATION-001 |
| Revision | Draft 1; ngày 2026-09-22 |
| Chế độ | Cổng G4 — Lập kế hoạch thực thi, phân rã tasks & phân phối luồng song song |
| Sprint Scope | **Quản lý mô phỏng ATS + sprint 2** |
| Quy mô nhóm khảo sát | **2 người** (Dev 1: Backend & Database, Dev 2: Frontend & UI/UX) |
| Nguồn kiến trúc G3 | `docs/workflow/architecture/ats-simulation-design.md` (Approved 2026-09-22) |
| Sơ đồ ERD G3 | `docs/workflow/diagrams/ats-simulation-erd.html` (Approved 2026-09-22) |
| Nguồn User Stories G2 | `docs/workflow/specs/ats-simulation-stories.md` (Approved 2026-09-22, 5 Stories, 13 ACs) |
| Nguồn Nghiệp vụ G1 | `docs/workflow/specs/ats-simulation-brief.md` (Approved 2026-09-22) |
| Trạng thái G4 | `approved`: Người dùng đã phê duyệt chính thức ngày 2026-09-22; sẵn sàng chuyển giao cho Cổng task-execution |

---

## 1. Mục tiêu & Phạm vi Sprint 2 (Sprint Goal)

### Sprint Goal
> Xây dựng và nghiệm thu thành công phân hệ **Mô phỏng & Đánh giá tương thích theo từng dòng ATS (Workday, Oracle Taleo, Greenhouse, Lever)**: triển khai bảng `dbo.AtsSimulations` (EF Core migration), 4 bộ giả lập parser ATS bóc tách lỗi layout thực tế, chế độ xem trực quan `ATS Reader View`, Ma trận điểm tương thích đa hệ thống, và tính năng `Auto-fix Layout on Export` tự động chuẩn hóa CV thành 1 cột an toàn khi tải file PDF/DOCX.

### Phạm vi bao phủ
- Toàn bộ 5 User Stories (`US21` – `US25`) và 13 Acceptance Criteria đã được chuẩn hóa tại Cổng G2.
- Bảng cơ sở dữ liệu `dbo.AtsSimulations` (khóa ngoại `report_id` tham chiếu `dbo.Reports` với `ON DELETE CASCADE`).
- 4 Bộ Heuristic Parser Simulators nội bộ trong `CVAnalysis.Infrastructure/AtsSimulators/`.
- Thuật toán dàn phẳng cấu trúc `AutoFixLayoutService` (OpenXML & PdfPig).
- 2 Màn hình UI mới: `SCR15` (Tab ATS Reader View & Ma trận Tương thích) và `SCR16` (Modal Xem trước Auto-fix Layout).

---

## 2. Cấu trúc Thư mục Tasks phân bổ cho 2 Thành viên

```text
docs/workflow/plans/quan-ly-mo-phong-ats-sprint-2/
├── roadmap.md                                      # Lộ trình tổng thể Sprint 2, ma trận ràng buộc & điểm hội tụ
├── backend/                                        # Phân công: Dev 1 (ASP.NET Core Web API, EF Core, Simulators)
│   ├── task-21-ats-simulation-migration.md         # Bảng dbo.AtsSimulations, EF Core Entity & Migration
│   ├── task-22-parser-simulators-engine.md         # 4 Bộ Heuristic Parser Simulators (Workday, Taleo, Greenhouse, Lever)
│   ├── task-23-autofix-export-service.md           # Dịch vụ dàn phẳng cấu trúc 1 cột AutoFixLayoutService (PDF/DOCX)
│   └── task-24-ats-simulation-api-controllers.md   # 3 REST API endpoints (GET ats-simulation, scan, export-autofix)
├── frontend/                                       # Phân công: Dev 2 (React Vite, TypeScript, Tailwind CSS)
│   ├── task-25-ats-reader-view-ui.md               # Tab ATS Reader View (SCR15), chuyển 4 góc nhìn, bôi màu lỗi
│   ├── task-26-compatibility-matrix-ui.md          # Thẻ 4 điểm %, breakdown lỗi từng dòng & hướng dẫn tự sửa
│   └── task-27-autofix-preview-modal-ui.md         # Modal So sánh trước sau 2 cột vs 1 cột (SCR16) & Nút xuất file
└── qa/                                             # Phân công: QA & Tích hợp (Cả 2 thành viên)
    └── task-28-ats-simulation-e2e-verification.md  # Ghép nối toàn diện End-to-End, test trên CV mẫu & nghiệm thu
```

---

## 3. Ma trận Ràng buộc giữa các Tasks (Task Dependency Matrix)

| Task ID | Tiêu đề Task | Phân bổ / Role | Hard Prerequisites (Phải xong trước) | Blocking (Chặn các tasks sau) | Shared-Write Areas (Vùng code chung) |
|:---:|---|---|---|---|---|
| `TASK-21` | DDL `dbo.AtsSimulations` & EF Migration | `backend/` (Dev 1) | Kế thừa Sprint 1 (`dbo.Reports`) | Chặn `TASK-22`, `TASK-24` | `src/CVAnalysis.Domain/Entities/`, `Persistence/` |
| `TASK-25` | Tab ATS Reader View & 4 Góc nhìn UI | `frontend/` (Dev 2) | Không (Dùng Mock từ G3 Contracts) | Chặn `TASK-26`, `TASK-27` | `cvanalysis-client/src/features/ats-sim/` |
| `TASK-22` | 4 Bộ Giả lập Parser ATS Simulators | `backend/` (Dev 1) | Cần `TASK-21` xong | Chặn `TASK-24` | `src/CVAnalysis.Infrastructure/AtsSimulators/` |
| `TASK-23` | Dịch vụ Dàn phẳng Bố cục Auto-fix | `backend/` (Dev 1) | Kế thừa `IPdfParser` từ Sprint 1 | Chặn `TASK-24` | `src/CVAnalysis.Infrastructure/Export/` |
| `TASK-26` | Ma trận 4 Điểm % & Hướng dẫn tự sửa UI | `frontend/` (Dev 2) | Cần `TASK-25` | Chặn `TASK-27` | `cvanalysis-client/src/features/ats-sim/` |
| `TASK-24` | REST API Controllers cho Sprint 2 | `backend/` (Dev 1) | Cần `TASK-22`, `TASK-23` xong | Chặn `TASK-28` | `src/CVAnalysis.WebApi/Controllers/Ats/` |
| `TASK-27` | Modal Xem trước So sánh Auto-fix UI | `frontend/` (Dev 2) | Cần `TASK-26` | Chặn `TASK-28` | `cvanalysis-client/src/features/ats-sim/` |
| `TASK-28` | E2E Integration & Nghiệm thu Sprint 2 | `qa/` (Cả 2) | Cần tất cả tasks 21–27 xong | Không (Hoàn tất Sprint 2) | Toàn bộ repo & Test Suite |

---

## 4. Phân phối 2 Luồng Thực thi Song song (Parallel Tracks)

```text
THỜI GIAN       TRACK 1: DEV 1 (BACKEND & SIMULATORS)          TRACK 2: DEV 2 (FRONTEND & CLIENT)
─────────       ─────────────────────────────────────          ──────────────────────────────────
T0 (Bắt đầu)    TASK-21: Migration dbo.AtsSimulations          TASK-25: Giao diện ATS Reader View (4 tabs)
                (Liên kết 1-1 với Reports CASCADE)             (Dùng Mock Contracts đã chốt ở G3)
                                  │                                              │
T1              TASK-22: Xây dựng 4 Bộ Parser Simulators       TASK-26: Thẻ Ma trận 4 Điểm Tương thích %
                TASK-23: Dịch vụ Dàn phẳng Auto-fix            TASK-27: Modal So sánh Auto-fix Layout 1 cột
                                  │                                              │
               ┌──────────────────┴──────────────────────────────────────────────┴─────────────────┐
               │ SYNC CHECKPOINT 1: Ghép nối API Simulator thật & Hiển thị văn bản Reader View     │
               └──────────────────┬──────────────────────────────────────────────┬─────────────────┘
                                  │                                              │
T2              TASK-24: Hoàn tất 3 REST API Endpoints         Ghép nối API Auto-fix & Tải file thật
                                  │                                              │
               ┌──────────────────┴──────────────────────────────────────────────┴─────────────────┐
               │ SYNC CHECKPOINT 2: Nghiệm thu Toàn diện (TASK-28)                                 │
               │ Chạy bộ test với CV 2 cột có lỗi: kiểm chứng xáo trộn Workday & mất Header Taleo  │
               └───────────────────────────────────────────────────────────────────────────────────┘
```

---

## 5. Quy tắc Kiểm chứng & Định nghĩa Hoàn thành (DoD)

1. **Kiểm thử Thuật toán Simulator:** 4 bộ giả lập bóc tách có Unit Test độc lập kiểm tra các file mẫu:
   - File CV 2 cột $\rightarrow$ `WorkdaySimulator` phát hiện đọc đan xen, trừ đúng 30% điểm.
   - File CV có số điện thoại ở Header $\rightarrow$ `TaleoSimulator` phát hiện mất thông tin liên hệ, trừ đúng 40% điểm.
   - File quét không có JD $\rightarrow$ tiêu chí từ khóa Greenhouse tự động chuyển `NOT_APPLICABLE` (không trừ 30 điểm).
2. **Kiểm chứng Auto-fix Layout:** File PDF/DOCX xuất qua `AutoFixLayoutService` được kiểm tra lại qua 4 bộ parser, đảm bảo đạt 100% điểm định dạng.
3. **Bảo toàn bất biến:**
   - 1 credit bao trọn toàn bộ; xóa CV gốc tự động xóa lan tỏa bản ghi `dbo.AtsSimulations`.
   - Quyền Zero-Trust 72h của nhân viên hỗ trợ được áp dụng đồng bộ cho dữ liệu mô phỏng.
