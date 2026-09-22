# Đặc tả Nghiệp vụ (Business Brief) — Mô phỏng & Đánh giá theo hệ thống ATS (Sprint 2)

| Thuộc tính | Giá trị |
|---|---|
| Artifact ID | BRIEF-SIMULATION-001 |
| Revision | Draft 1; ngày 2026-09-22 |
| Phân hệ / Sprint | **Sprint 2 — Mô phỏng & Đánh giá Tương thích Đa Hệ thống ATS (ATS Simulation & Compatibility Engine)** |
| Mã Epic | `EP07: Mô phỏng và Đánh giá theo hệ thống ATS` |
| Quy mô hệ thống | Quy mô 2: Hệ thống Hoàn chỉnh Vừa (SME / Startup dịch vụ thật) |
| Trạng thái G1 | `approved`: Người dùng đã phê duyệt chính thức ngày 2026-09-22 trên cơ sở Báo cáo Thẩm định Độc lập AUDIT-G1-ATS-001 (PASS tuyệt đối); chính thức chuyển sang Cổng G2 (User Stories & UX) |
| Nguồn nghiên cứu đối chiếu | `docs/research/ats-scoring-rubrics-case-studies.md` (Mục 1 & 2: Cơ chế hoạt động của Taleo, Workday, Greenhouse, Lever) |
| Nguồn phỏng vấn case study | `ats_parser_view_mode`, `ats_multi_system_matrix`, `ats_simulation_monetization`, `ats_autofix_layout`, `ats_scan_without_jd`, `ats_simulation_retention` |

---

## 1. Mục tiêu và Giới hạn (Sprint 2 Scope)

### Mục tiêu sản phẩm
Bổ sung phân hệ **Mô phỏng & Đánh giá tương thích theo từng dòng ATS (ATS Simulation & Compatibility Engine)** nhằm tăng cường trực tiếp năng lực cốt lõi: *"Ứng dụng AI trong phân tích và tối ưu CV để vượt qua hệ thống ATS"*. Phân hệ này cho phép ứng viên:
1. Xem trực quan văn bản sau khi từng parser ATS nổi tiếng (Workday, Oracle Taleo, Greenhouse, Lever) bóc tách qua chế độ `ATS Reader View`, làm nổi bật các đoạn bị đọc lộn xộn hoặc nuốt chữ.
2. Nhận **Ma trận Điểm tương thích 4 hệ thống riêng biệt** (% Workday, % Taleo, % Greenhouse, % Lever) kèm danh sách lỗi bố cục cụ thể.
3. Nhận hướng dẫn sửa chi tiết và kích hoạt tính năng **Tự động chuẩn hóa bố cục 1 cột an toàn khi xuất file PDF/DOCX**, đảm bảo 100% tài liệu xuất ra vượt qua các thuật toán đọc khắt khe nhất.

### Giới hạn & Ngoài phạm vi Sprint 2
- Không cam kết chắc chắn đậu phỏng vấn hay tuyển dụng (giữ vững nguyên tắc trung thực tại BR14, BR18).
- Không đảo ngược quy trình của nhà tuyển dụng; chỉ mô phỏng các cơ chế bóc tách kỹ thuật đã được công bố và kiểm chứng thực nghiệm của các dòng ATS phổ biến.
- Tích hợp trọn gói vào Gói nâng cao (1 credit): Không thu thêm phí riêng rẽ để bảo toàn bất biến kinh tế BR13 của Sprint 1.

---

## 2. Actors và Ma trận Quyền Zero-Trust

| Vai trò (Actor) | Quyền hạn đối với Báo cáo Mô phỏng ATS & Reader View | Ràng buộc bảo mật |
|---|---|---|
| **Ứng viên (Candidate)** | Toàn quyền chạy mô phỏng, xem văn bản bóc tách của 4 hệ thống, xem ma trận điểm, xuất file tối ưu. | Chỉ xem được dữ liệu của chính mình. |
| **Nhân viên hỗ trợ (Support Specialist)** | Chỉ được xem báo cáo mô phỏng khi ứng viên chủ động cấp quyền hỗ trợ theo vụ việc khiếu nại (BR22, tối đa 72h). | Mặc định không xem được nội dung; tự động thu hồi khi đóng vụ việc. |
| **Trưởng nhóm hỗ trợ (Support Lead)** | Tương tự hỗ trợ viên: chỉ xem khi được phân công duyệt phiếu bồi thường có cấp quyền. | Không xem tự do. |
| **Kế toán / Tài chính (Finance)** | Không có quyền xem nội dung bóc tách CV. Chỉ đối soát credit thanh toán. | 403 Forbidden đối với nội dung CV. |
| **Quản trị viên hệ thống (Admin)** | Mặc định không có quyền xem nội dung CV và báo cáo mô phỏng (Zero-Trust — BR07). | Chỉ xem logs kỹ thuật. |

---

## 3. Thuật ngữ Domain (Glossary)

| Thuật ngữ | Định nghĩa trong phạm vi phân hệ này |
|---|---|
| **Workday ATS** | Hệ thống ATS doanh nghiệp lớn, sử dụng parser bóc tách dạng khối nhưng hay gặp lỗi xáo trộn dòng ngang khi gặp bố cục cột đôi (Interleaved Reading Order). |
| **Oracle Taleo** | Hệ thống ATS kế thừa (Legacy ATS), có cơ chế tự động xóa sạch Header và Footer, cực kỳ nhạy cảm với bảng phức tạp và phông chữ lạ. |
| **Greenhouse & Lever** | Các hệ thống ATS thế hệ mới, hỗ trợ nhận diện thực thể kỹ thuật (NER) và từ đồng nghĩa, nhưng phạt nặng các lỗi không trích xuất được text layer hoặc text box thả nổi. |
| **ATS Reader View** | Chế độ xem văn bản thô bóc tách dưới con mắt của máy quét ATS, giúp người dùng nhìn thấy các đoạn bị gãy dòng, lỗi font hoặc mất chữ. |
| **Auto-fix Layout on Export** | Thuật toán tự động dàn phẳng cấu trúc CV phức tạp (bảng ẩn, 2 cột) thành định dạng 1 cột tuần tự an toàn tuyệt đối khi người dùng tải file PDF/Word. |

---

## 4. Quy tắc Nghiệp vụ Đã chốt (Business Rules)

### SR01: 4 Dòng Hệ thống ATS được Mô phỏng
- Hệ thống xây dựng 4 bộ giả lập parser chuyên biệt đại diện cho 4 nền tảng ATS chiếm thị phần lớn nhất thế giới:
  1. `Workday Parser Simulator` (Tập trung kiểm tra luồng đọc tuần tự và lỗi cột đôi).
  2. `Oracle Taleo Parser Simulator` (Tập trung kiểm tra mất Header/Footer và cấu trúc phân mục).
  3. `Greenhouse Parser Simulator` (Tập trung kiểm tra độ nhạy từ khóa ngữ cảnh và text layer).
  4. `Lever Parser Simulator` (Tập trung kiểm tra thẻ kỹ năng chuẩn hóa và kinh nghiệm).

### SR02: Tính năng 'ATS Reader View' Mô phỏng Văn bản Bóc tách
- Cung cấp nút chuyển đổi `ATS Reader View` trên màn hình báo cáo:
  - Cho phép ứng viên chọn 1 trong 4 góc nhìn: *Góc nhìn Workday*, *Góc nhìn Taleo*, *Góc nhìn Greenhouse*, *Góc nhìn Lever*.
  - Hiển thị chính xác chuỗi văn bản thô mà parser của hệ thống đó trích xuất được.
  - Tự động bôi màu làm nổi bật các điểm bất thường:
    - Màu đỏ: Chữ bị nuốt/biến mất (ví dụ số điện thoại trong Header bị Taleo xóa).
    - Màu vàng: Dòng chữ bị xáo trộn đan xen (Interleaved lines trên Workday).
    - Màu xám: Ký tự lạ bị biến thành dấu hỏi chấm `?` hoặc ô vuông rỗng.

### SR03: Ma trận 4 Điểm Tương thích Đa Hệ thống (Compatibility Matrix)
- Hệ thống chấm và hiển thị 4 điểm số phần trăm độc lập:
  - **Workday Compatibility Score (%):** Trọng số phạt nặng lỗi 2 cột ($ -30\% $), text box trôi nổi ($ -25\% $), bảng không viền ($ -20\% $).
  - **Taleo Compatibility Score (%):** Trọng số phạt nặng thông tin liên hệ nằm trong Header/Footer ($ -40\% $), bảng phức tạp ($ -30\% $), tiêu đề phân mục không chuẩn convention ($ -20\% $).
  - **Greenhouse Compatibility Score (%):** Trọng số phạt nặng PDF thuần ảnh ($ -100\% $), thiếu từ khóa ngữ cảnh ($ -30\% $).
  - **Lever Compatibility Score (%):** Trọng số phạt nặng kỹ năng không chuẩn hóa danh từ kỹ thuật ($ -25\% $), cấu trúc thời gian công việc không đồng nhất ($ -20\% $).
- Điểm số làm tròn đến số nguyên gần nhất (Round Half Up).

### SR04: Tích hợp Toàn diện vào Gói Nâng cao (1 Credit Trọn Gói — BR13)
- Không phát sinh thêm phí hay đòi hỏi credit thứ hai cho tính năng mô phỏng ATS.
- **Bất biến 1 Credit bộ nâng cao (BR13):** Khi người dùng xác nhận dùng 1 credit, hệ thống sinh trọn vẹn 4 thành phần:
  1. Báo cáo chấm điểm ATS 4 trụ cột chi tiết.
  2. Bản viết lại câu văn Action-Context-Metric (Diff view).
  3. Sẵn sàng chức năng xuất file PDF và DOCX.
  4. **Ma trận tương thích 4 dòng ATS & Chế độ xem `ATS Reader View`**.
- Hệ thống chỉ thu credit khi đủ cả 4 thành phần; nếu thiếu hoặc lỗi mô phỏng thì không được phép thu credit (BR04, BR17, OQ03).

### SR05: Cho phép Quét Tương thích Độc lập khi Không Có JD
- Ứng viên được phép tải file CV lên và chọn chế độ *"Quét tương thích ATS & Bố cục (Không cần JD)"*.
- Ở chế độ này, hệ thống tập trung 100% vào kiểm tra tính hợp lệ về mặt kỹ thuật (định dạng, font chữ, thứ tự đọc, tiêu đề mục, bảng ẩn) và xuất Ma trận an toàn kỹ thuật của 4 hệ thống, không yêu cầu đối chiếu từ khóa với JD tuyển dụng (tương tự BR12/BR25).

### SR06: Hướng dẫn Sửa Chi tiết & Tự động Chuẩn hóa Bố cục khi Xuất file
- Với mỗi lỗi bóc tách được phát hiện, hệ thống cung cấp:
  - **Hướng dẫn tự sửa (Actionable Advice):** Giải thích rõ nguyên nhân và cách sửa trên file Word/Canva gốc (ví dụ: *"Chuyển số điện thoại từ Header vào dòng đầu tiên của trang giấy để tránh bị Taleo xóa"*).
  - **Tự động chuẩn hóa khi xuất file (Auto-fix Layout on Export):** Khi người dùng bấm nút Xuất PDF hoặc Xuất DOCX, bộ tạo file tự động áp dụng template 1 cột văn bản tuần tự chuẩn ATS, tự động dàn phẳng mọi bảng biểu phức tạp thành danh sách rõ ràng.

### SR07: Lưu trữ 90 Ngày & Xem lại Miễn phí (BR21, BR26)
- Dữ liệu văn bản bóc tách và Ma trận điểm của 4 dòng ATS được lưu kèm với bản ghi `dbo.Reports` trong cơ sở dữ liệu.
- Thời hạn lưu trữ là **đúng 90 ngày độc lập** theo ngày tạo của báo cáo.
- Trong 90 ngày, ứng viên mở lại xem các góc nhìn ATS Reader View hoặc tải lại file xuất đều hoàn toàn miễn phí.
- Khi CV gốc bị xóa (BR10) hoặc hết hạn 90 ngày: dữ liệu mô phỏng ATS tự động bị xóa theo.

### SR08: Cơ chế Phát hiện Lỗi Đọc Đan Xen của Workday (Interleaved Reading Bug)
- Hệ thống phân tích tọa độ không gian (bounding boxes) của các khối văn bản trong file PDF/DOCX.
- Nếu phát hiện có từ 2 cột trở lên trên cùng một tọa độ trục $Y$ nhưng khoảng cách giữa các cột không có đường phân cách rõ ràng, hệ thống mô phỏng việc đọc nối ngang dòng từ cột trái sang cột phải, gắn cờ cảnh báo lỗi đọc lộn xộn trên Workday.

### SR09: Cơ chế Phát hiện Lỗi Mất Header/Footer của Taleo
- Hệ thống phân tích vị trí tọa độ $Y$ của thông tin liên hệ (Email, Số điện thoại, Địa chỉ).
- Nếu các thông tin này nằm trong phạm vi lề trên (Top Margin $< 40$pt) hoặc lề dưới (Bottom Margin $< 40$pt) hoặc nằm trong thẻ `<header>` / `<footer>` của file Word $\rightarrow$ gắn cờ cảnh báo nguy cơ bị Taleo loại bỏ hoàn toàn thông tin liên lạc.

### SR10: Cơ chế Đánh giá Trích xuất của Greenhouse & Lever
- Hệ thống kiểm tra khả năng phân tách thẻ kỹ năng (Skill Tags Tokenization).
- Nếu các kỹ năng bị dồn cục trong một đoạn văn dài hoặc phân cách bằng các ký tự lạ không chuẩn (ví dụ emoji, ký hiệu đồ họa) làm parser không tách được từ khóa $\rightarrow$ cảnh báo và hướng dẫn phân cách bằng dấu phẩy `,` hoặc dấu chấm tròn chuẩn `&bull;`.

---

## 5. Rà soát Chuyên sâu 6 Trụ cột Cốt lõi (System Core Deep-Dive)

### 1. Vòng đời & Máy trạng thái (State Machine & Entity Lifecycle)
- Thêm trạng thái bổ trợ trong `AnalysisRequest`:
  `Processing` $\rightarrow$ sinh đồng thời Báo cáo Rubric + Bóc tách 4 Parser ATS.
- Nếu việc bóc tách ATS bị lỗi nhưng Báo cáo Rubric thành công $\rightarrow$ áp dụng quy tắc lỗi một phần (BR17), lưu phần đã hoàn tất trong 24 giờ và giải phóng credit đang giữ.

### 2. Quy tắc Tài chính, Số liệu & Bất biến Domain (Money & Math Invariants)
- **Bất biến giữ/thu credit (BR04, BR13, OQ03):** 1 credit bao trọn toàn bộ. Không thu tiền riêng cho việc xem Reader View.
- **Công thức tính Điểm Tương thích Đa Hệ thống ($C_{\text{system}}$):**
  $$C_{\text{system}} = 100 - \sum_{j \in \text{Errors}} P_{j,\text{system}}$$
  Điểm số chặn sàn tại 0 điểm (không bị âm): $C_{\text{system}} = \max(0, C_{\text{system}})$.

### 3. Xử lý Đồng thời & Khóa Dữ liệu (Concurrency & Locking)
- Toàn bộ kết quả mô phỏng 4 dòng ATS được lưu dưới dạng một trường JSON có cấu trúc (`ats_simulation_json`) trong bảng `dbo.Reports`.
- Do gắn liền với `dbo.Reports` (1-1 với `AnalysisRequest`), không phát sinh tranh chấp ghi độc lập giữa các tiến trình.

### 4. Ranh giới Phân quyền & Cô lập Dữ liệu (Permissions & Access Control)
- Tuân thủ nghiêm ngặt Ma trận Zero-Trust 5 roles đã chốt ở Sprint 1.
- Nhân viên hỗ trợ và Lead hỗ trợ chỉ xem được báo cáo mô phỏng ATS khi ứng viên cấp quyền hỗ trợ vụ việc trong thời hạn tối đa 72h (BR22).

### 5. Ngoại lệ, Sự cố & Luồng lỗi (Edge Cases, Failure Modes & Compensation)
- File PDF scan thuần ảnh (không có text layer) $\rightarrow$ dừng ngay từ bước tiếp nhận theo BR03, không trừ lượt/credit và không chạy mô phỏng ATS.
- File có mật khẩu $\rightarrow$ từ chối ngay (OQ06).
- Lỗi timeout 90 giây $\rightarrow$ Circuit Breaker tự ngắt và hoàn trả credit theo BR04.

### 6. Ràng buộc Tích hợp & Nguồn sự thật (Integration & Source of Truth)
- Bộ giải lập parser nội bộ (`WorkdaySimulator`, `TaleoSimulator`, `GreenhouseSimulator`, `LeverSimulator`) được xây dựng dựa trên thông số nghiên cứu thực chứng `RESEARCH-ATS-002`.
- Không gọi API ra bên ngoài để mô phỏng nhằm bảo vệ quyền riêng tư tuyệt đối cho CV của ứng viên (không gửi file sang bên thứ ba nào ngoài nhà cung cấp AI đã được chấp thuận ở US02).

---

## 6. Sẵn sàng cho G1 Discovery Quality Audit

Tài liệu này đã giải quyết toàn diện bài toán nghiệp vụ của phân hệ **Mô phỏng & Đánh giá theo hệ thống ATS (Sprint 2)**, hoàn toàn khớp với định vị cốt lõi của dự án và sẵn sàng kích hoạt Subagent Reviewer thực hiện quy trình thẩm định độc lập Cổng G1.
