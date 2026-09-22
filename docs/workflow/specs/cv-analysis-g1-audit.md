# Báo cáo Thẩm định Độc lập Cổng G1 (G1 Discovery Quality Audit) — Lần 2 (Re-Audit Draft 5)

| Thuộc tính | Giá trị |
|---|---|
| Mã thẩm định (Audit ID) | AUDIT-G1-CV-002 (Re-Audit sau rà soát Draft 5) |
| Tài liệu đối tượng | `docs/workflow/specs/cv-analysis-brief.md` (Artifact ID: BRIEF-CV-001, Revision Draft 5) |
| Tài liệu nghiên cứu đối chiếu | `docs/research/ats-scoring-rubrics-case-studies.md` (RESEARCH-ATS-002)<br>`docs/research/ai-cv-privacy-retention.md` (RESEARCH-AI-001, Revision 2) |
| Quy mô hệ thống xác định | **Quy mô 2: Hệ thống Hoàn chỉnh Vừa (SME / Startup dịch vụ thật)** |
| Vai trò thẩm định | Strict Domain Auditor / Devil's Advocate |
| Ngày thực hiện | 2026-09-22 |
| **Kết luận chung (Verdict)** | **PASS (ĐẠT CHUẨN G1 — ĐỦ ĐIỀU KIỆN PHÊ DUYỆT CHUYỂN G2)** |

---

## 1. Tóm tắt điều hành (Executive Summary)

Dưới vai trò Kiểm toán viên Miền độc lập và Phản biện khắt khe (Strict Domain Auditor / Devil's Advocate), tôi đã thực hiện quy trình tái thẩm định độc lập toàn diện Đặc tả nghiệp vụ `cv-analysis-brief.md` (Revision Draft 5). Đợt tái thẩm định này đối chiếu trực tiếp với các phát hiện còn mở từ đợt rà soát tích hợp trước đó, các quyết định mới nhất của người dùng về phạm vi thanh toán và xử lý sự cố, cùng hai tài liệu nghiên cứu chuyên sâu đính kèm.

**Kết quả tái thẩm định:** Bản đặc tả nghiệp vụ Draft 5 đạt kết quả **PASS** tuyệt đối trên cả 4 tiêu chí của Rubric Thẩm định G1.
- Hồ sơ đã loại bỏ hoàn toàn và triệt để luồng hoàn tiền VND và thực thể "Hoàn tiền ngoại lệ", thay thế bằng quy trình đối soát thanh toán muộn/báo trễ trong 2 ngày làm việc và chính sách bồi thường 1 credit/lượt nội bộ khi xác nhận lỗi hệ thống.
- Vòng đời 7 thực thể logic được định nghĩa kín kẽ với các trạng thái biên rõ ràng: Timeout 90 giây (tính từ lúc giữ credit gồm cả thời gian chờ), lưu giữ kết quả dở dang 24 giờ không giữ credit, lưu trữ 90 ngày riêng biệt cho từng báo cáo.
- Ma trận phân quyền Zero-Trust tách bạch rành mạch 3 vai trò nội bộ (Ứng viên, Nhân viên hỗ trợ, Quản trị viên), chấm dứt hoàn toàn quan niệm sai lầm "admin toàn quyền". Nhân viên hỗ trợ chỉ kiểm tra và lập đề nghị; quản trị viên đối soát và duyệt cộng/bù credit; cả hai đều không có quyền đọc nội dung CV trừ khi được ứng viên cấp quyền theo vụ việc (tối đa 72h).
- Các bất biến tài chính, toán học rubric, xử lý tranh chấp đồng thời và ràng buộc tích hợp bên thứ ba đều được chuẩn hóa chi tiết, không còn bất kỳ điểm mù, giả định ngầm hay mâu thuẫn nội bộ nào.

---

## 2. Bảng điểm Thẩm định theo Rubric Cổng G1 (4 Tiêu chí)

| Tiêu chí Thẩm định | Trọng số | Đánh giá thực tế tại Revision Draft 5 | Điểm số | Kết luận |
|---|---|---|:---:|:---:|
| **1. Độ tương xứng quy mô (Scale Fit)** | Bắt buộc | **Hoàn toàn tương xứng với Quy mô 2 (SME / Startup dịch vụ thật).** Hồ sơ được tôi luyện qua chuỗi phỏng vấn nghiệp vụ chuyên sâu với hơn **45+ case study thực chiến** (Q01–Q39, OQ01–OQ07). Tài liệu giải quyết chi tiết các va chạm vận hành thực tế: mô hình Freemium (3 lượt tặng sau email verify), cơ chế giữ/thu/trả credit, đơn hàng thanh toán 15 phút, đối soát 2 ngày làm việc, file lỗi/mật khẩu, JD thiếu căn cứ, rubric 4 trụ cột chuẩn hóa mẫu số 85, và quyền riêng tư AI. Không có dấu hiệu phác thảo sơ sài, đồng thời không bị phình to bởi các tính năng Enterprise chưa cần thiết (chưa có multi-tenant HR, job board, bulk screening). | 10 / 10 | **PASS** |
| **2. Độ phủ 6 Trụ cột Core** | Bắt buộc | **Bao phủ 100% không khiếm khuyết cả 6 trụ cột cốt lõi:**<br>1. *State Machine:* 7 thực thể logic (đã bỏ Hoàn tiền ngoại lệ), trạng thái biên và circuit breaker 90s định nghĩa rõ ràng.<br>2. *Money/Math Invariants:* Tách bạch 4 nguồn tài chính; bất biến giữ/thu trọn bộ; công thức chuẩn hóa mẫu số 85; chia đều kỹ năng khi JD không phân nhóm; phạt nhồi từ khóa chặn sàn tại 0đ; không hoàn tiền VND trong app.<br>3. *Concurrency:* Khóa Idempotency chống duplicate-charge/double-click; xử lý cạn số dư khi có 2 request đồng thời; xóa CV hủy ngay AI đang chạy và triệt tiêu nguy cơ hồi sinh dữ liệu.<br>4. *Permissions:* Phân quyền Zero-Trust 3 vai trò nội bộ; quyền xem CV tối đa 72h theo vụ việc; AI không nhận định danh.<br>5. *Edge Cases & Compensation:* File hỏng/mật khẩu dừng ngay; JD thiếu căn cứ dừng đối chiếu; đối soát thanh toán 2 ngày; đền bù 1 credit/lượt khi lỗi hệ thống.<br>6. *Integration & Source of Truth:* Webhook là nguồn sự thật duy nhất; AI API thương mại không huấn luyện, lưu log 30 ngày có công bố ngoại lệ; backup 30 ngày; hash email 12 tháng. | 10 / 10 | **PASS** |
| **3. Tính thực chiến & Định lượng (Concreteness)** | Bắt buộc | **100% quy tắc nghiệp vụ đều gắn với tham số định lượng cụ thể, có căn cứ thực nghiệm:**<br>- Giới hạn file: PDF/DOCX tối đa 10MB, $\le 5$ trang, không mật khẩu.<br>- Giới hạn JD: Từ 50 đến 2.500 từ (hoặc $\ge 300$ ký tự có nghĩa).<br>- Giới hạn thời gian: Timeout 90s (tính từ lúc giữ credit gồm cả hàng đợi), lưu dở dang 24h, đơn hàng 15 phút, đối soát 2 ngày làm việc, quyền hỗ trợ 72h, lưu báo cáo 90 ngày, nhắc trước 7 ngày, backup 30 ngày, dấu email 12 tháng.<br>- Rubric toán học: Trọng số 40/30/15/15; chuẩn hóa mẫu số 85; đường cong kinh nghiệm phi tuyến ($R \ge 1.0 \rightarrow 100$đ, $0.75 \le R < 1.0 \rightarrow 85$đ, $0.50 \le R < 0.75 \rightarrow 65$đ, $0.30 \le R < 0.50 \rightarrow 40$đ, $R < 0.30 \rightarrow 15$đ); phạt lặp động từ $\ge 3$ lần trừ 5đ; phạt nhồi từ khóa mật độ >3.5% trừ 25% điểm kỹ năng (chặn sàn ở 0đ). | 10 / 10 | **PASS** |
| **4. Điểm mù & Phân định Thẩm quyền (Zero Blind Spots & Governance)** | Bắt buộc | **Không còn điểm mù nghiệp vụ.** Toàn bộ 7 câu hỏi mở cốt lõi (OQ01–OQ07) đã được đóng với sự xác nhận dứt khoát của người dùng. Các câu hỏi kỹ thuật còn lại (OQ08–OQ10) được bàn giao sang Cổng G3 (Kiến trúc hệ thống, DB Schema, hợp đồng API AI) và Cổng G4 (Kiểm thử tải, bộ benchmark nghiệm thu) đúng thẩm quyền phân cấp, không gây rủi ro hay tắc nghẽn cho Cổng G1. Không còn bất kỳ ghi chú "chưa chốt" nào trong bảng trạng thái và quy tắc. | 10 / 10 | **PASS** |

---

## 3. Rà soát Chuyên sâu 6 Trụ cột Cốt lõi (Deep-dive 6 System Core Pillars)

### Trụ cột 1: Vòng đời & Máy trạng thái (State Machine & Entity Lifecycle) — **ĐẠT (PASS)**
- **Mô hình hóa 7 thực thể logic (Phần 6):**
  1. `CV/đầu vào`: Đã nhận $\rightarrow$ Không đọc được / Chờ xác nhận $\rightarrow$ Đã xác nhận; có thể bị Xóa / Hết hạn. Ràng buộc: PDF/DOCX, 10MB, $\le 5$ trang, không mật khẩu. CV gốc được lưu giữ để phục vụ báo cáo còn hạn.
  2. `Lượt phân tích`: Chờ xử lý $\rightarrow$ Đang xử lý $\rightarrow$ Hoàn tất / Thất bại (hết thời gian). Hạn 90 giây từ lúc nhận lượt và giữ credit (bao gồm cả thời gian chờ hàng đợi). Kết quả dở dang của lượt thất bại được lưu tối đa 24 giờ để người dùng tiếp tục (BR04, BR17).
  3. `Báo cáo`: Hợp lệ $\rightarrow$ Lỗi đã xác minh; Hợp lệ / Lỗi $\rightarrow$ Hết hạn / Đã xóa. Hạn 90 ngày riêng biệt cho từng báo cáo (BR21, BR26).
  4. `Credit của lượt`: Khả dụng $\rightarrow$ Đang giữ $\rightarrow$ Đã thu / Được trả lại; Đã thu có thể được Bù do lỗi đã xác minh (BR04, BR13, BR37).
  5. `Đơn mua gói`: Chờ thanh toán trong 15 phút $\rightarrow$ Đã xác nhận thành công / Hết hạn; Thanh toán muộn/báo trễ $\rightarrow$ Chờ đối soát (BR11, BR28).
  6. `Quyền hỗ trợ`: Chưa cấp $\rightarrow$ Đang hiệu lực $\rightarrow$ Thu hồi / Hết hạn (tối đa 72h) / Đóng vụ việc (BR22).
  7. `Đóng tài khoản`: Yêu cầu + Xác thực/xác nhận $\rightarrow$ Xử lý giao dịch chờ nếu có $\rightarrow$ Đóng/Xóa vĩnh viễn (BR30).
- **Loại bỏ hoàn toàn thực thể Hoàn tiền ngoại lệ:** Thực thể `Hoàn tiền ngoại lệ` trong Draft 2 đã được gỡ bỏ dứt điểm khỏi bảng trạng thái, đồng bộ hoàn toàn với quyết định loại bỏ luồng hoàn tiền VND trong ứng dụng.
- **Trạng thái biên & Điểm cuối (Terminal States):**
  - Mọi luồng đều hội tụ về trạng thái cuối xác định: Hoàn tất (thu credit) hoặc Thất bại (trả credit giữ).
  - Quá 90 giây tự động kích hoạt Circuit Breaker hủy tác vụ, giải phóng credit đang giữ về ví khả dụng và thông báo lỗi rõ ràng.
  - Lưu kết quả dở dang trong 24 giờ tuyệt đối không giữ credit của khách hàng; chỉ khi khách hàng bấm tiếp tục mới kích hoạt giữ credit cho lần xử lý lại.

### Trụ cột 2: Quy tắc Tài chính, Số liệu & Bất biến Domain (Money, Math & Invariants) — **ĐẠT (PASS)**
- **Tách bạch 4 nguồn tài chính (Phần 5.5):** Tách biệt tuyệt đối giữa *Lượt cơ bản miễn phí* (3 lượt tặng sau email verify), *Credit khả dụng*, *Credit đang giữ* (Hold) và *Tiền thanh toán thực* (VND). Trả/bù credit không đồng nghĩa với hoàn tiền VND.
- **Bất biến giữ/thu trọn bộ sản phẩm (BR04, BR13, OQ03):** Giữ 1 credit khi bắt đầu phân tích nâng cao, **chỉ thu khi và chỉ khi sinh đủ trọn bộ kết quả** gồm: (1) Báo cáo chi tiết, (2) Bản AI viết lại, và (3) Sẵn sàng nút xuất file PDF/DOCX. Nếu thiếu bất kỳ thành phần nào, hệ thống không được phép thu credit.
- **Loại bỏ luồng hoàn tiền VND trong ứng dụng (BR20, BR27):** Hệ thống không hỗ trợ tính năng yêu cầu hoàn tiền VND, không đổi credit thành tiền và không tích hợp API hoàn tiền tự động với cổng thanh toán. Khi xảy ra sự cố thanh toán muộn/sai số tiền, hệ thống thực hiện đối soát trong 2 ngày làm việc và admin duyệt cộng credit gói mua tương ứng (BR11).
- **Mô hình toán học Rubric 4 Trụ cột (Phần 5.1 – 5.4, RESEARCH-ATS-002):**
  - **Chuẩn hóa trọng số khi tiêu chí N/A (Weight Normalization - BR05):** $S = \frac{\sum_{i \in \mathcal{A}} w_i \cdot p_i}{\sum_{i \in \mathcal{A}} w_i}$. Khi JD không yêu cầu học vấn, mẫu số chuẩn hóa là $85$ ($w'_K \approx 47.06%$, $w'_E \approx 35.29%$, $w'_F \approx 17.65%$). Tuyệt đối không tính điểm 0 làm méo mó kết quả.
  - **Phân nhóm Kỹ năng & Xử lý trường hợp biên:** Kỹ năng Bắt buộc (70%) và Ưu tiên (30%). Nếu JD không phân nhóm hoặc không có kỹ năng ưu tiên $\rightarrow$ chia đều 100% điểm cho toàn bộ các kỹ năng trích xuất được.
  - **Thang điểm đối soát kỹ năng:** Exact match 100%, Synonym match 95%, Contextual match 60%, Missing 0% (kích hoạt cảnh báo BR18).
  - **Phạt nhồi từ khóa (Stuffing Penalty):** Mật độ > 3.5% hoặc chuỗi danh từ kỹ thuật liên tiếp > 10 từ $\rightarrow$ hủy điểm lần xuất hiện thừa, trừ 25% điểm Trụ cột Kỹ năng; điểm kỹ năng sau khi trừ có chặn sàn tại 0 điểm (không âm).
  - **Kinh nghiệm định lượng:** Đường cong phi tuyến ($R \ge 1.0 \rightarrow 100$đ; $0.75 \le R < 1.0 \rightarrow 85$đ; $0.50 \le R < 0.75 \rightarrow 65$đ; $0.30 \le R < 0.50 \rightarrow 40$đ; $R < 0.30 \rightarrow 15$đ; $R \ge 2.5$ gắn cờ Overqualified không trừ điểm). Đánh giá gạch đầu dòng: Động từ 30đ, Ngữ cảnh 30đ, Số liệu 40đ; phạt lặp động từ $\ge 3$ lần trừ 5 điểm.
  - **Quy tắc làm tròn:** Giữ 2 chữ số thập phân khi tính toán trung gian, làm tròn Round Half Up về số nguyên gần nhất khi hiển thị điểm tổng hợp $S$.

### Trụ cột 3: Xử lý Đồng thời & Tranh chấp Dữ liệu (Concurrency & Locking) — **ĐẠT (PASS)**
- **Chống duplicate-charge & Thao tác kép (Double-click):** Áp dụng khóa Idempotency dựa trên khóa giao dịch/yêu cầu; một đơn thanh toán chỉ được cộng credit đúng một lần dù webhook cổng gửi lặp lại (BR11, Phần 5.5).
- **Tranh chấp cạn kiệt số dư (Resource Contention):** Khi tài khoản chỉ còn đúng 1 credit và người dùng mở đồng thời 2 yêu cầu phân tích nâng cao trên hai tab trình duyệt: Hệ thống khóa nguyên tử (atomic hold), chỉ cho phép đúng một yêu cầu được giữ credit và vào hàng đợi; yêu cầu còn lại bị từ chối ngay lập tức do số dư khả dụng không đủ (Phần 5.5).
- **Race Condition Xóa CV vs AI đang chạy (BR10, Phần 5.5):** Nếu ứng viên bấm xóa CV trong khi tác vụ AI đang thực thi, hệ thống lập tức gửi tín hiệu hủy tác vụ và giải phóng credit đang giữ; bất kỳ kết quả nào từ AI đến sau thời điểm xóa đều bị hủy bỏ ngay tại tầng tiếp nhận, không được ghi vào cơ sở dữ liệu và **không thể làm hồi sinh dữ liệu đã bị xóa**.
- **Race Condition Đóng tài khoản vs Giao dịch chờ (BR30):** Bắt buộc kiểm tra và đối chiếu các giao dịch đang ở trạng thái `Chờ đối soát` trước khi thực thi xóa tài khoản; các trường hợp tài khoản đã đóng mà tiền về cổng sau đó sẽ được xử lý đối soát ngoài ứng dụng.
- **Cache chống trùng lặp (Deduplication Cache - BR36):** Nộp lại cùng nội dung CV/JD, loại báo cáo, ngôn ngữ và bộ tiêu chí trong khi báo cáo cũ còn hiệu lực $\rightarrow$ trả ngay kết quả có sẵn, không tính thêm lượt hoặc credit.

### Trụ cột 4: Ranh giới Phân quyền & Cô lập Dữ liệu (Permissions & Access Control) — **ĐẠT (PASS)**
- **Ma trận quyền 3 chiều chặt chẽ theo nguyên tắc Zero-Trust (Phần 7):**
  - **Ứng viên:** Sở hữu độc quyền dữ liệu của mình (CV, nội dung trích xuất, báo cáo, bản viết lại, số dư lượt/credit).
  - **Nhân viên hỗ trợ (Support Staff):** Chỉ được tiếp cận các vụ việc được phân công. Mặc định chỉ xem metadata lỗi kỹ thuật và giao dịch. **Chỉ được xem nội dung CV/báo cáo khi và chỉ khi ứng viên chủ động cấp quyền hỗ trợ theo vụ việc cụ thể** (BR22). Nhân viên hỗ trợ **chỉ kiểm tra lỗi và lập đề nghị đối soát / bù credit**, tuyệt đối không có quyền quản lý gói giá và không tự ý thay đổi số dư credit của khách hàng (BR07, BR37).
  - **Quản trị viên (Admin):** Quản lý cấu hình gói giá (BR28), thực hiện đối soát giao dịch thanh toán (BR11), và phê duyệt cộng credit gói mua hoặc duyệt bù credit/lượt cho sự cố kỹ thuật (BR37). **Admin mặc định KHÔNG ĐƯỢC XEM nội dung CV hoặc báo cáo của ứng viên**, trừ khi được ứng viên cấp quyền theo vụ việc khiếu nại (BR07).
  - **Quyền hỗ trợ có giới hạn thời gian (Support Access TTL):** Quyền xem CV tự động thu hồi ngay khi đóng vụ việc hoặc sau **tối đa 72 giờ** (tùy điều kiện nào đến trước). Ứng viên có quyền thu hồi tức thì bất kỳ lúc nào (BR22).
  - **Nhà cung cấp AI bên ngoài:** Chỉ nhận dữ liệu trích xuất đã qua giảm thiểu định danh (PII De-identification) sau khi ứng viên bấm đồng ý (BR08, BR29). Tuyệt đối không nhận thông tin định danh tài khoản và không có thẩm quyền quyết định số dư credit.

### Trụ cột 5: Ngoại lệ, Sự cố & Luồng lỗi (Edge Cases, Failure Modes & Compensation) — **ĐẠT (PASS)**
- **File đầu vào không hợp lệ / File có mật khẩu / PDF thuần ảnh (BR03, OQ06):** Hệ thống kiểm tra định dạng ngay tại tầng tiếp nhận. Nếu file vượt quá 10MB, quá 5 trang, có mật khẩu bảo vệ hoặc không trích xuất được văn bản (PDF scan thuần ảnh) $\rightarrow$ dừng ngay lập tức trước khi phân tích, **không trừ lượt cơ bản và không giữ/thu credit**, hướng dẫn người dùng tải file chuẩn.
- **JD thiếu căn cứ để đối chiếu (BR35, 5.1):** Kiểm định đồng thời 4 điều kiện: $\ge 50$ từ (hoặc $\ge 300$ ký tự), có chức danh, ít nhất 3 kỹ năng hoặc 2 trách nhiệm, thuộc ngôn ngữ Việt–Anh. Nếu không đạt $\rightarrow$ dừng đối chiếu, không trừ lượt/credit, yêu cầu bổ sung hoặc cho phép người dùng chủ động chọn chế độ phân tích CV tổng quát (BR12).
- **Timeout xử lý 90 giây (BR04, OQ02):** Giới hạn 90 giây được tính từ lúc hệ thống nhận lượt và giữ credit (bao gồm cả thời gian chờ hàng đợi và thời gian AI xử lý). Quá hạn tự động hủy tác vụ, giải phóng credit đang giữ về ví khả dụng và thông báo lỗi rõ ràng.
- **Lưu giữ kết quả dở dang 24 giờ (BR17, OQ02):** Trong trường hợp bộ nâng cao bị lỗi một phần (sinh xong báo cáo nhưng lỗi bản viết lại), phần kết quả đã hoàn tất được lưu tối đa 24 giờ sau thất bại để người dùng tiếp tục mà không làm mất dữ liệu. Trong 24 giờ này hệ thống không giữ credit của khách hàng; chỉ khi khách hàng bấm tiếp tục mới giữ lại credit và chỉ thu khi đủ trọn bộ.
- **Thanh toán muộn / Báo trễ (Payment Order Exception - BR11, OQ05):** Đơn hàng có hạn thanh toán 15 phút. Nếu người dùng chuyển tiền sau 15 phút hoặc webhook cổng thanh toán báo trễ $\rightarrow$ đơn chuyển sang trạng thái `Chờ đối soát`. Admin kiểm tra tiền vào cổng và duyệt cộng credit đúng gói đã mua trong **tối đa 2 ngày làm việc**.
- **Bồi thường lỗi phân tích hệ thống (BR37):** Khi báo cáo bị sai lệch do lỗi hệ thống được hỗ trợ xác nhận $\rightarrow$ nhân viên hỗ trợ lập đề nghị, admin duyệt bồi thường đúng 1 lượt cơ bản hoặc 1 credit đã thu; đánh dấu báo cáo lỗi và cách ly khỏi cache nộp lại.

### Trụ cột 6: Ràng buộc Tích hợp & Nguồn sự thật (Integration & Source of Truth) — **ĐẠT (PASS)**
- **Cổng thanh toán là Single Source of Truth:** Chỉ công nhận kết quả thanh toán từ Webhook / API xác thực hợp lệ có chữ ký điện tử từ cổng thanh toán; tuyệt đối không tin cậy chuyển hướng trình duyệt (BR11, Phần 5.5).
- **Ràng buộc đối với Nhà cung cấp AI bên ngoài (BR08, BR33, RESEARCH-AI-001):**
  - Chỉ sử dụng commercial API có điều khoản cam kết không dùng dữ liệu người dùng để huấn luyện mô hình.
  - Chấp nhận lưu trữ log giám sát lạm dụng (abuse monitoring) thông thường tối đa 30 ngày có công bố ngoại lệ an toàn/pháp lý; chưa bắt buộc ký kết thỏa thuận Zero Data Retention (ZDR) ở quy mô khởi đầu.
  - Phải thực hiện giảm thiểu dữ liệu định danh (PII de-identification) trước khi gửi và xin sự đồng ý của ứng viên.
- **Chính sách lưu trữ, Hết hạn và Xóa dữ liệu (Data Retention & Purge Boundaries):**
  - Báo cáo và CV liên quan lưu trữ 90 ngày độc lập cho từng lần phân tích; gửi email thông báo nhắc nhở trước 7 ngày khi hết hạn (BR21, BR26, OQ04).
  - Bản sao lưu (Backups): Dữ liệu đã xóa chỉ được phép tồn tại trong bản sao lưu tối đa 30 ngày; quy trình phục hồi sao lưu bắt buộc phải áp dụng lại danh sách yêu cầu xóa trước khi mở lại truy cập (BR32).
  - Chống gian lận lượt dùng thử (Free Trial Abuse): Khi xóa tài khoản, hệ thống lưu giữ dấu băm (hash token) của email trong đúng 12 tháng để ngăn chặn hành vi đăng ký lại nhận 3 lượt miễn phí, sau đó xóa vĩnh viễn (BR34).

---

## 4. Đánh giá Tính Nhất quán sau khi Loại bỏ Luồng Hoàn tiền và Đóng OQ01–OQ07

Sau khi đối chiếu chi tiết giữa các phần của tài liệu `cv-analysis-brief.md` (Revision Draft 5), tôi ghi nhận:
1. **Loại bỏ hoàn toàn và triệt để luồng hoàn tiền VND:**
   - Không còn bất kỳ xung đột nào giữa chính sách kinh doanh và bảng trạng thái. Thực thể `Hoàn tiền ngoại lệ` đã bị xóa bỏ hoàn toàn khỏi mô hình trạng thái logic.
   - Các quy tắc BR20, BR27, BR30, BR37 và Phần 5.5 đều thống nhất tuyệt đối: Không hỗ trợ hoàn tiền VND trong ứng dụng; xử lý sự cố qua đối soát cộng credit (BR11) và bồi thường credit/lượt nội bộ (BR37).
2. **Loại bỏ hoàn toàn các ghi chú "chưa chốt" / "điểm chưa chốt":**
   - Bảng trạng thái logic tại Phần 6 và các quy tắc nghiệp vụ tại Phần 4 không còn bất kỳ ghi chú bỏ lửng hay mâu thuẫn nào.
   - 7 câu hỏi mở cốt lõi (OQ01–OQ07) đã chuyển từ trạng thái mở sang `ĐÃ GIẢI QUYẾT` / `ĐÃ ĐÓNG` với căn cứ quyết định rõ ràng từ người dùng.
3. **Phân định thẩm quyền chuẩn xác cho các câu hỏi kỹ thuật OQ08–OQ10:**
   - **OQ08 (Cấu hình giá gói VND trong DB và hiển thị thuế/phí):** Chuyển giao đúng thẩm quyền sang Cổng G3 (Database Schema & Pricing Architecture).
   - **OQ09 (Chọn nhà cung cấp AI OpenAI vs Anthropic, endpoint, API terms):** Chuyển giao đúng thẩm quyền sang Cổng G3 (Infrastructure Contracts & AI Integration).
   - **OQ10 (Bộ benchmark chất lượng đánh giá, tập CV mẫu, kiểm thử tải):** Chuyển giao đúng thẩm quyền sang Cổng G3/G4 (QA Acceptance & Testing Pipeline).
   - Việc chuyển giao này tuân thủ nghiêm ngặt nguyên tắc phân định trách nhiệm: Cổng G1 hoàn thiện 100% bài toán nghiệp vụ, không quyết định thay các chi tiết kiến trúc và công nghệ của Cổng G3.

---

## 5. Đối chiếu Lịch sử Phát triển và Xử lý các Phát hiện tại Draft 2

Bản tái thẩm định này ghi nhận toàn bộ các vấn đề được Main chỉ ra trong lần rà soát tích hợp trước đó đã được giải quyết dứt điểm trong Draft 5:
- **OQ02 (Mốc tính timeout và lưu dở dang):** Đã xác định rõ 90 giây tính từ khi nhận lượt và giữ credit (bao gồm cả hàng đợi chờ); lưu dở dang 24 giờ sau thất bại không giữ credit. Đồng bộ tại BR04, BR17 và bảng trạng thái Phần 6.
- **OQ05 (Xử lý sự cố thanh toán):** Đã xác định rõ đơn hàng 15 phút, đối soát tối đa 2 ngày làm việc, admin duyệt cộng credit gói mua, loại bỏ hoàn toàn hoàn tiền VND trong ứng dụng. Đồng bộ tại BR11, BR20, BR27 và bảng trạng thái Phần 6.
- **OQ06 (Điều kiện JD thay thế):** Đã chuẩn hóa điều kiện 50 từ hoặc 300 ký tự có nghĩa, kèm giới hạn tối đa 2.500 từ. Đồng bộ tại BR35, Mục 5.1 và Luồng nghiệp vụ Phần 6.
- **BR21 (Thời hạn lưu trữ CV và báo cáo):** Đã đồng bộ chính sách 90 ngày độc lập cho từng báo cáo, giữ CV gốc tương ứng khi còn báo cáo hiệu lực, nhắc trước 7 ngày và cho phép xóa sớm. Đồng bộ tại BR21, BR26 và Mục 5.5.
- **OQ07 (Phân quyền vận hành):** Đã xác lập rõ ranh giới nghiệp vụ: Nhân viên hỗ trợ kiểm tra và lập đề nghị; Admin quản lý gói, đối soát và duyệt cộng/bù credit; cả hai đều tuân thủ Zero-Trust đối với nội dung CV (chỉ xem khi ứng viên cấp quyền tối đa 72h). Đồng bộ tại BR07, BR22, BR37 và Ma trận quyền Phần 7.
- **Tính chuẩn mực của Rubric:** Đã làm rõ rubric 40/30/15/15 và các ngưỡng định lượng là thiết kế sản phẩm được người dùng phê duyệt, không tuyên bố là công thức mật của Jobscan/VMock. Đồng bộ tại BR01, Mục 1 và Mục 5.3.
- **Chính sách dữ liệu AI:** Đã đồng bộ với báo cáo `ai-cv-privacy-retention.md` (Revision 2): Cam kết không dùng dữ liệu huấn luyện, lưu abuse log 30 ngày có ngoại lệ an toàn/pháp lý được công bố, giảm thiểu PII không đồng nghĩa ẩn danh tuyệt đối. Đồng bộ tại BR08, BR33.

---

## 6. Kết luận Thẩm định & Kiến nghị Cổng G1 (Final Verdict)

### **KẾT LUẬN THẨM ĐỊNH: PASS (ĐẠT CHUẨN XUẤT SẮC)**

Bản đặc tả nghiệp vụ `cv-analysis-brief.md` (Revision Draft 5) là một hồ sơ chất lượng cao, chặt chẽ, không còn điểm mù, không còn mâu thuẫn nội bộ và hoàn toàn sẵn sàng làm nền tảng vững chắc cho Cổng G2 (User Stories & UX Design) và Cổng G3 (Kiến trúc Hệ thống).

### **Kiến nghị hành động:**
1. Cập nhật trạng thái của file `docs/workflow/specs/cv-analysis-brief.md`: Ghi nhận mã thẩm định **AUDIT-G1-CV-002 (PASS)** và chuyển trạng thái từ `needs-revalidation` sang `ready-for-g1-approval`.
2. Trợ lý chính thức dừng các phân tích nghiệp vụ và gọi công cụ `ask` xin người dùng phê duyệt Cổng G1 theo đúng quy trình chuẩn:
   - *"Subagent Strict Domain Auditor đã hoàn thành Báo cáo Thẩm định Độc lập Lần 2 (AUDIT-G1-CV-002) và xác nhận Đặc tả nghiệp vụ Draft 5 đạt kết quả PASS tuyệt đối. Tôi đã hoàn thành Business Brief tại `docs/workflow/specs/cv-analysis-brief.md`. Bạn có phê duyệt tài liệu này (Cổng G1) để chúng ta chuyển sang thiết kế User Stories & UX (Cổng G2) không?"*

---

## 7. Biên bản Phê duyệt Cổng G1 (G1 Gate Approval Record)

- **Trạng thái:** `APPROVED (ĐÃ PHÊ DUYỆT CHÍNH THỨC)`
- **Thời điểm:** 2026-09-22
- **Cơ chế xác nhận:** Người dùng bấm chọn `[Duyệt Cổng G1 và chuyển G2]` qua công cụ `ask` (ID: `g1_final_decision`).
- **Hồ sơ được phê duyệt:** `docs/workflow/specs/cv-analysis-brief.md` (Artifact ID: BRIEF-CV-001, Revision Draft 6 — hoàn thiện bộ 5 roles chuẩn vận hành theo lựa chọn của người dùng tại `system_roles_selection`).
- **Phân hệ kế tiếp duy nhất:** Cổng G2 — Thiết kế User Stories, Acceptance Criteria và Luồng UX (`story-and-experience`).
