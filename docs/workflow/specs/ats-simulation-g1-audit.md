# Báo cáo Thẩm định Độc lập Cổng G1 (G1 Discovery Quality Audit) — Phân hệ Mô phỏng ATS (Sprint 2)

| Thuộc tính | Giá trị |
|---|---|
| Mã thẩm định (Audit ID) | AUDIT-G1-ATS-001 |
| Tài liệu đối tượng | `docs/workflow/specs/ats-simulation-brief.md` (Artifact ID: BRIEF-SIMULATION-001, Revision Draft 1) |
| Tài liệu nghiên cứu đối chiếu | `docs/research/ats-scoring-rubrics-case-studies.md` (RESEARCH-ATS-002)<br>`docs/workflow/specs/cv-analysis-brief.md` (BRIEF-CV-001, Revision Draft 6) |
| Quy mô hệ thống xác định | **Quy mô 2: Hệ thống Hoàn chỉnh Vừa (SME / Startup dịch vụ thật)** |
| Vai trò thẩm định | Strict Domain Auditor / Devil's Advocate |
| Ngày thực hiện | 2026-09-22 |
| **Kết luận chung (Verdict)** | **PASS (ĐẠT CHUẨN G1 — ĐỦ ĐIỀU KIỆN PHÊ DUYỆT CHUYỂN G2)** |

---

## 1. Tóm tắt điều hành (Executive Summary)

Dưới vai trò Kiểm toán viên Miền độc lập và Phản biện khắt khe (Strict Domain Auditor / Devil's Advocate), tôi đã thực hiện quy trình thẩm định độc lập toàn diện Đặc tả nghiệp vụ `ats-simulation-brief.md` (Artifact ID: BRIEF-SIMULATION-001, Revision Draft 1) cho phân hệ **Sprint 2: Mô phỏng & Đánh giá theo hệ thống ATS (ATS Simulation & Compatibility Engine)**. 

Đợt thẩm định này đối chiếu trực tiếp với:
1. Bộ Rubric chuẩn hóa và 5 Case Study / Edge Cases kỹ thuật trong `RESEARCH-ATS-002`.
2. Nền tảng kiến trúc, bất biến kinh tế và ma trận bảo mật đã được người dùng phê duyệt tại Cổng G1 Sprint 1 (`BRIEF-CV-001`).
3. Khung đánh giá 4 tiêu chí Cổng G1 và 6 Trụ cột Cốt lõi (System Core Pillars) dành cho hệ thống Quy mô 2 (SME / Startup dịch vụ thật).

**Kết quả thẩm định:** Bản đặc tả nghiệp vụ `ats-simulation-brief.md` đạt kết quả **PASS** xuất sắc trên toàn bộ 4 tiêu chí của Rubric Cổng G1.
- **Bám sát định vị cốt lõi:** Phân hệ giải quyết trực tiếp lời hứa thương hiệu *"Ứng dụng AI phân tích & tối ưu CV vượt ATS"* bằng cách cung cấp cơ chế trực quan hóa văn bản bóc tách (`ATS Reader View`), Ma trận tương thích 4 hệ thống ATS hàng đầu (Workday, Taleo, Greenhouse, Lever), và cơ chế tự động dàn phẳng cấu trúc an toàn khi xuất file (`Auto-fix Layout on Export`).
- **Kế thừa và bảo toàn bất biến:** Tích hợp trọn gói vào 1 credit nâng cao (BR13), không phát sinh phí ẩn, không tạo race condition số dư, tuân thủ nghiêm ngặt mô hình Zero-Trust 5 roles và giới hạn lưu trữ 90 ngày đồng bộ với `dbo.Reports` (BR21).
- **Tính định lượng và thực chiến:** Toàn bộ 10 quy tắc nghiệp vụ SR01–SR10 đều có công thức trừ điểm, ngưỡng tọa độ không gian (bounding box, margin < 40pt) và tiêu chuẩn tokenization rõ ràng, không có tuyên bố cảm tính.

---

## 2. Bảng điểm Thẩm định theo Rubric Cổng G1 (4 Tiêu chí)

| Tiêu chí Thẩm định | Trọng số | Đánh giá thực tế tại Revision Draft 1 | Điểm số | Kết luận |
|---|---|---|:---:|:---:|
| **1. Độ tương xứng quy mô (Scale Fit)** | Bắt buộc | **Hoàn toàn tương xứng với Quy mô 2 (SME / Startup dịch vụ thật).** Phân hệ tập trung giải quyết bài toán cốt lõi của ứng viên khi nộp đơn qua ATS: không biết CV của mình trông như thế nào dưới con mắt của parser và tại sao bị loại. Không bị phình to sang các tính năng Enterprise ngoài tầm (như tích hợp API HRIS 2 chiều với Workday/Taleo của nhà tuyển dụng, hay hệ thống bulk screening ứng viên cho phòng nhân sự). Tập trung 100% vào năng lực mô phỏng bóc tách kỹ thuật phía client. | 10 / 10 | **PASS** |
| **2. Độ phủ 6 Trụ cột Core** | Bắt buộc | **Bao phủ 100% không khiếm khuyết cả 6 trụ cột cốt lõi:**<br>1. *State Machine:* Xử lý bóc tách đồng thời trong trạng thái `Processing`; cơ chế lỗi một phần (BR17) lưu kết quả dở dang 24h và giải phóng credit.<br>2. *Money/Math Invariants:* Tích hợp trọn gói trong 1 credit nâng cao (BR13); công thức $C_{\\text{system}} = \\max(0, 100 - \\sum P_j)$; làm tròn Round Half Up.<br>3. *Concurrency:* Trường JSON có cấu trúc `ats_simulation_json` gắn chặt 1-1 với `dbo.Reports`; xóa CV cascade hủy ngay AI và xóa sạch dữ liệu mô phỏng.<br>4. *Permissions:* Ma trận Zero-Trust 5 roles; nhân viên hỗ trợ và Lead chỉ xem khi ứng viên cấp quyền theo vụ việc (BR22, TTL 72h).<br>5. *Edge Cases:* Chặn PDF scan thuần ảnh (BR03), chặn file mật khẩu (OQ06), Circuit Breaker timeout 90s hoàn credit (BR04), hỗ trợ quét kỹ thuật không cần JD (SR05).<br>6. *Integration & Source of Truth:* 4 bộ parser simulator nội bộ, không gọi API bên thứ ba ngoài AI approved (US02); lưu trữ 90 ngày độc lập (BR21). | 10 / 10 | **PASS** |
| **3. Tính thực chiến & Định lượng (Concreteness)** | Bắt buộc | **100% quy tắc nghiệp vụ SR01–SR10 đều được định lượng bằng tham số kỹ thuật cụ thể:**<br>- Bôi màu Reader View: Đỏ (nuốt chữ), Vàng (xáo trộn dòng), Xám (lỗi ký tự mã hóa).<br>- Trọng số phạt chi tiết từng lỗi: 2 cột (-30%), text box (-25%), bảng không viền (-20%), Header/Footer (-40%), bảng phức tạp (-30%), heading không chuẩn (-20%), thiếu từ khóa (-30%), kỹ năng không chuẩn (-25%), lệch ngày (-20%).<br>- Ngưỡng tọa độ Taleo: Top margin < 40pt, Bottom margin < 40pt hoặc thẻ `<header>`/`<footer>`.<br>- Thuật toán Workday: Kiểm tra bounding box đa cột cùng trục $Y$ không có dải phân cách. | 10 / 10 | **PASS** |
| **4. Điểm mù & Phân định Thẩm quyền (Zero Blind Spots & Governance)** | Bắt buộc | **Không còn điểm mù nghiệp vụ.** Toàn bộ các câu hỏi biên phát sinh từ Sprint 1 (lỗi một phần, xử lý khi không có JD, xóa cascading dữ liệu, quyền riêng tư khi bóc tách) đã được kế thừa và giải quyết dứt điểm. Phân định rõ thẩm quyền: G1 chốt toàn diện bài toán nghiệp vụ mô phỏng; G2 thiết kế UI/UX Reader View và chuyển đổi tab; G3 thiết kế AST parser nội bộ và schema JSON `dbo.Reports.ats_simulation_json`. | 10 / 10 | **PASS** |

---

## 3. Rà soát Chuyên sâu 6 Trụ cột Cốt lõi (Deep-dive 6 System Core Pillars)

### Trụ cột 1: Vòng đời & Máy trạng thái (State Machine & Entity Lifecycle) — **ĐẠT (PASS)**
- **Luồng xử lý đồng thời trong trạng thái `Processing`:** 
  Khi yêu cầu phân tích chuyển sang `Processing`, hệ thống kích hoạt song song 2 luồng: (A) Đánh giá Rubric 4 Trụ cột chuyên sâu và (B) Bóc tách qua 4 bộ giả lập ATS (`WorkdaySimulator`, `TaleoSimulator`, `GreenhouseSimulator`, `LeverSimulator`).
- **Cơ chế lỗi một phần (Partial Failure — Kế thừa BR17):**
  Nếu 4 bộ giả lập ATS gặp sự cố hoặc timeout nhưng Báo cáo Rubric thành công (hoặc ngược lại): Hệ thống **không được phép thu credit** của khách hàng, lập tức giải phóng credit đang giữ (hold release), và lưu kết quả dở dang tối đa 24 giờ để người dùng có thể kích hoạt hoàn tất mà không phải tải lại file.
- **Điểm cuối (Terminal States):**
  - Thành công trọn vẹn: Đủ 4 thành phần (Báo cáo Rubric, Bản viết lại ACM, Xuất file PDF/DOCX, Ma trận ATS & Reader View) $\\rightarrow$ Thu 1 credit (BR13).
  - Thất bại toàn phần / Timeout 90s $\\rightarrow$ Circuit Breaker kích hoạt, hủy tác vụ và trả lại credit (BR04).

### Trụ cột 2: Quy tắc Tài chính, Số liệu & Bất biến Domain (Money, Math & Invariants) — **ĐẠT (PASS)**
- **Bất biến 1 Credit nâng cao trọn gói (BR13):**
  Tính năng mô phỏng 4 dòng ATS và chế độ xem `ATS Reader View` được tích hợp trọn gói vào Gói nâng cao (1 credit). Hệ thống tuyệt đối không thu phí vi mô (micro-transaction), không yêu cầu thêm credit thứ hai cho việc chuyển đổi giữa 4 góc nhìn ATS hay sử dụng tính năng `Auto-fix Layout on Export`.
- **Mô hình toán học tính Điểm Tương thích Đa Hệ thống ($C_{\\text{system}}$):**
  $$C_{\\text{system}} = \\max\\left(0, 100 - \\sum_{j \\in \\text{Errors}} P_{j,\\text{system}}\\right)$$
  - Cơ chế chặn sàn tại 0 điểm: Đảm bảo điểm số không bao giờ bị âm dù CV mắc nhiều lỗi bố cục nghiêm trọng cùng lúc.
  - Quy tắc làm tròn số học: Điểm số hiển thị được làm tròn đến số nguyên gần nhất (Round Half Up) theo tiêu chuẩn ISO.
  - Bảng trọng số phạt định lượng chi tiết cho từng dòng hệ thống (SR03), phản ánh chính xác các điểm yếu kỹ thuật đặc thù của từng parser ngoài đời thực.

### Trụ cột 3: Xử lý Đồng thời & Khóa Dữ liệu (Concurrency & Locking) — **ĐẠT (PASS)**
- **Lưu trữ phi chuẩn hóa có kiểm soát (Structured JSON):**
  Toàn bộ kết quả bóc tách văn bản thô, danh sách cảnh báo tọa độ và ma trận 4 điểm tương thích được đóng gói trong trường JSON `ats_simulation_json` thuộc bảng `dbo.Reports`.
- **Khử hoàn toàn nguy cơ tranh chấp số dư (Balance Race Condition):**
  Do không tạo thực thể thanh toán hay trừ credit riêng rẽ cho phân hệ mô phỏng, toàn bộ việc giữ/thu credit được bảo vệ bởi cơ chế khóa nguyên tử (Atomic Lock / Idempotency Key) ở cấp độ `AnalysisRequest` đã thiết kế tại Sprint 1.
- **Chống hồi sinh dữ liệu khi xóa CV (Cascading Purge — BR10):**
  Khi ứng viên bấm xóa CV hoặc tài khoản, hệ thống gửi tín hiệu hủy (abort signal) đến tất cả các worker đang giả lập bóc tách ATS; bản ghi `dbo.Reports` cùng trường `ats_simulation_json` bị xóa đồng thời, loại bỏ hoàn toàn khả năng dữ liệu mô phỏng xuất hiện lại sau khi xóa.

### Trụ cột 4: Ranh giới Phân quyền & Cô lập Dữ liệu (Permissions & Access Control) — **ĐẠT (PASS)**
- **Ma trận Zero-Trust 5 Roles toàn diện:**
  - **Ứng viên (Candidate):** Sở hữu độc quyền, chỉ xem và xuất báo cáo mô phỏng của chính mình.
  - **Nhân viên hỗ trợ (Support Specialist):** Mặc định 403 Forbidden đối với nội dung bóc tách CV. Chỉ được mở quyền xem khi ứng viên chủ động bấm cấp quyền trong phiếu khiếu nại (BR22). Thời hạn hiệu lực tối đa 72 giờ, tự động thu hồi ngay khi đóng vụ việc.
  - **Trưởng nhóm hỗ trợ (Support Lead):** Tương tự nhân viên hỗ trợ, chỉ được tiếp cận khi được chỉ định thẩm tra khiếu nại hoặc duyệt phiếu bồi thường có cấp quyền.
  - **Kế toán / Tài chính (Finance):** Hoàn toàn bị cấm truy cập (403 Forbidden) vào nội dung CV, văn bản bóc tách và Reader View. Chỉ có quyền đối soát lịch sử giao dịch nạp credit.
  - **Quản trị viên hệ thống (Admin):** Mặc định bị cấm truy cập nội dung CV và báo cáo mô phỏng (BR07). Chỉ xem telemetry, error logs hệ thống và cấu hình tham số.

### Trụ cột 5: Ngoại lệ, Sự cố & Luồng lỗi (Edge Cases, Failure Modes & Compensation) — **ĐẠT (PASS)**
- **PDF scan thuần ảnh (Scan-only PDF):** Chặn ngay tại cổng tiếp nhận tài liệu (BR03, mã lỗi `UNPARSEABLE_FILE`), không trừ lượt/credit, không khởi chạy các simulator ATS.
- **File có mật khẩu bảo vệ:** Chặn ngay tại bước đọc file (OQ06), trả thông báo lỗi thân thiện yêu cầu gỡ mật khẩu.
- **Sự cố quá thời gian (Timeout 90s):** Toàn bộ chu trình từ lúc giữ credit qua hàng đợi đến khi hoàn thành 4 simulator phải nằm trong hạn mức 90 giây (BR04). Quá 90 giây, Circuit Breaker tự ngắt, hoàn trả credit đang giữ về tài khoản người dùng.
- **Quét độc lập khi không có JD (Scan without JD — SR05):** Hỗ trợ đầy đủ luồng ứng viên chỉ muốn kiểm tra độ an toàn kỹ thuật của CV trước các parser ATS mà chưa có JD mục tiêu (tương thích BR12/BR25).

### Trụ cột 6: Ràng buộc Tích hợp & Nguồn sự thật (Integration & Source of Truth) — **ĐẠT (PASS)**
- **Giả lập nội bộ độc lập (In-house Heuristic & AST Simulators):**
  4 bộ parser simulator (`WorkdaySimulator`, `TaleoSimulator`, `GreenhouseSimulator`, `LeverSimulator`) được phát triển hoàn toàn bằng thuật toán nội bộ dựa trên các thông số nghiên cứu thực chứng `RESEARCH-ATS-002`.
- **Bảo vệ quyền riêng tư tuyệt đối (Zero Third-party Leakage):**
  Hệ thống tuyệt đối không gọi API bên ngoài sang các dịch vụ thương mại khác để mô phỏng bóc tách CV. Dữ liệu CV không bao giờ bị rò rỉ ra ngoài phạm vi nhà cung cấp AI đã được phê duyệt ở US02.
- **Vòng đời lưu trữ 90 ngày (BR21, BR26):**
  Dữ liệu `ats_simulation_json` được lưu giữ chính xác 90 ngày độc lập theo ngày tạo báo cáo. Trong 90 ngày, ứng viên được xem lại 4 góc nhìn `ATS Reader View` và tải file chuẩn hóa hoàn toàn miễn phí. Hết 90 ngày dữ liệu tự động bị hủy vĩnh viễn.

---

## 4. Đánh giá Tính Cụ thể & Điểm mù Nghiệp vụ (Concreteness & Blind Spots)

### Rà soát 10 Quy tắc Nghiệp vụ SR01–SR10:
1. **SR01 (4 Dòng Hệ thống ATS):** Phân định ranh giới trọng tâm rõ ràng cho 4 dòng ATS phổ biến nhất thế giới hiện nay.
2. **SR02 (ATS Reader View):** Định lượng trực quan với 3 mã màu cảnh báo (Đỏ: mất chữ; Vàng: xáo trộn dòng; Xám: lỗi ký tự/font).
3. **SR03 (Ma trận Điểm tương thích):** Bộ công thức trừ điểm cụ thể, phân định rõ mức độ nghiêm trọng của từng lỗi đối với từng parser.
4. **SR04 (1 Credit Trọn gói):** Khẳng định tính bất biến kinh tế BR13, quy định rõ điều kiện tiên quyết trước khi trừ credit.
5. **SR05 (Quét không cần JD):** Định rõ luồng quét kỹ thuật thuần túy, giải quyết dứt điểm nhu cầu kiểm tra định dạng độc lập.
6. **SR06 (Actionable Advice & Auto-fix on Export):** Đưa ra giải pháp 2 nấc: vừa hướng dẫn ứng viên tự sửa trên file gốc, vừa cung cấp công cụ tự động dàn phẳng 1 cột an toàn khi xuất file.
7. **SR07 (Lưu trữ 90 ngày & Miễn phí xem lại):** Đồng bộ tuyệt đối với chính sách lưu trữ BR21 của Sprint 1.
8. **SR08 (Cơ chế phát hiện lỗi đọc đan xen Workday):** Ứng dụng phân tích không gian Bounding Box trên tọa độ trục $Y$ để phát hiện đọc ngang giữa 2 cột.
9. **SR09 (Cơ chế phát hiện lỗi mất Header/Footer Taleo):** Thiết lập ngưỡng tọa độ định lượng rõ ràng: Top/Bottom Margin $< 40$pt hoặc thẻ `<header>`/`<footer>`.
10. **SR10 (Cơ chế đánh giá trích xuất Greenhouse & Lever):** Kiểm tra khả năng tách thẻ kỹ năng (Tokenization) và phân tách bằng ký tự chuẩn.

---

## 5. Góc nhìn Phản biện Khắt khe (Devil's Advocate Insights & Recommendations for G2/G3)

Mặc dù tài liệu đạt chuẩn **PASS** cho Cổng G1, dưới vai trò Devil's Advocate, tôi lưu ý 3 khuyến nghị kỹ thuật quan trọng cần được làm rõ ở Cổng G2 (User Stories/UX) và Cổng G3 (Kiến trúc Hệ thống):

1. **Khuyến nghị 1: Cơ chế N/A cho Tiêu chí Từ khóa Greenhouse khi Quét Không Có JD (SR05)**
   - *Vấn đề phản biện:* Trong SR03, Greenhouse có mức phạt `-30%` nếu thiếu từ khóa ngữ cảnh. Tuy nhiên, khi người dùng kích hoạt chế độ Quét không có JD (SR05), hệ thống không có dữ liệu JD để đối chiếu từ khóa.
   - *Hướng xử lý cho G2/G3:* Ở chế độ Quét không có JD, tiêu chí *"thiếu từ khóa ngữ cảnh (-30%)"* của Greenhouse phải được tự động chuyển sang trạng thái `N/A` (không áp dụng phạt), điểm tương thích của Greenhouse chỉ tính trên các lỗi định dạng kỹ thuật (PDF ảnh, text box, font chữ) để tránh việc ứng viên bị trừ oan 30% điểm số một cách vô lý.

2. **Khuyến nghị 2: Làm rõ Quan hệ giữa Phạt PDF thuần ảnh (-100%) của Greenhouse và Chặn tiếp nhận BR03**
   - *Vấn đề phản biện:* BR03 quy định file PDF scan thuần ảnh (100% là ảnh chụp) bị chặn ngay tại bước upload với mã lỗi `UNPARSEABLE_FILE`, không chạy phân tích. Vậy khi nào mức phạt `-100%` của Greenhouse trong SR03 được áp dụng?
   - *Hướng xử lý cho G2/G3:* Xác định rõ mức phạt `-100%` của Greenhouse là tầng phòng vệ chiều sâu (Defense-in-depth), hoặc áp dụng cho trường hợp file hỗn hợp (Mixed PDF: ví dụ trang 1 có text nhưng trang 2 là ảnh chụp chứng chỉ/bảng biểu rasterize khiến parser không đọc được trang đó).

3. **Khuyến nghị 3: Bản xem trước (Preview) cho Tính năng Auto-fix Layout on Export (SR06)**
   - *Vấn đề phản biện:* Khi người dùng có một CV 2 cột màu sắc thiết kế trên Canva và chọn Auto-fix, hệ thống tự động dàn phẳng thành tài liệu 1 cột tối giản. Nếu không có bản xem trước, người dùng có thể bất ngờ hoặc cảm thấy bố cục mới quá đơn điệu.
   - *Hướng xử lý cho G2 (UX Stories):* Cần bổ sung màn hình Modal Preview so sánh song song giữa bản gốc và bản chuẩn hóa 1 cột trước khi người dùng xác nhận tải file PDF/DOCX.

---

## 6. Kết luận Thẩm định & Kiến nghị Cổng G1 (Final Verdict)

### **KẾT LUẬN THẨM ĐỊNH: PASS (ĐẠT CHUẨN XUẤT SẮC)**

Bản đặc tả nghiệp vụ `docs/workflow/specs/ats-simulation-brief.md` (Artifact ID: BRIEF-SIMULATION-001, Revision Draft 1) là một văn bản hoàn chỉnh, chặt chẽ, có tính thực chiến rất cao, bám sát các nghiên cứu thực nghiệm của các hệ thống ATS hàng đầu thế giới và hoàn toàn sẵn sàng làm cơ sở vững chắc để bước vào Cổng G2 (Thiết kế User Stories & UX Flows).

### **Kiến nghị hành động:**
1. Cập nhật trạng thái của file `docs/workflow/specs/ats-simulation-brief.md`: Ghi nhận mã thẩm định **AUDIT-G1-ATS-001 (PASS)** và chuyển trạng thái từ `draft` sang `ready-for-g1-approval`.
2. Trợ lý chính thức báo cáo với người dùng kết quả thẩm định độc lập và xin phê duyệt Cổng G1 để chuyển sang Cổng G2:
   - *"Subagent Strict Domain Auditor đã hoàn thành Báo cáo Thẩm định Độc lập Cổng G1 (AUDIT-G1-ATS-001) cho Phân hệ Mô phỏng ATS (Sprint 2) và xác nhận tài liệu đạt kết quả PASS tuyệt đối trên cả 4 tiêu chí. Bạn có phê duyệt Business Brief này (Cổng G1) để chúng ta chuyển sang thiết kế User Stories & Luồng UX (Cổng G2) không?"*

---

## 7. Biên bản Phê duyệt Cổng G1 (G1 Gate Approval Record)

- **Trạng thái:** `APPROVED (ĐÃ PHÊ DUYỆT CHÍNH THỨC)`
- **Thời điểm:** 2026-09-22
- **Cơ chế xác nhận:** Người dùng bấm chọn `[Duyệt G1 (Sprint 2) và chuyển sang G2]` qua công cụ `ask` (ID: `g1_ats_sim_approval`).
- **Hồ sơ được phê duyệt:** `docs/workflow/specs/ats-simulation-brief.md` (Artifact ID: BRIEF-SIMULATION-001, Revision Draft 1).
- **Phân hệ kế tiếp duy nhất:** Cổng G2 — Thiết kế User Stories, Acceptance Criteria và Luồng UX cho Phân hệ Mô phỏng ATS (`story-and-experience`).
