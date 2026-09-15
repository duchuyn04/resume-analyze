# TÀI LIỆU YÊU CẦU NGHIỆP VỤ & PHỎNG VẤN HỆ THỐNG AI ĐÁNH GIÁ HỒ SƠ TUYỂN DỤNG

> **Dự án:** Hệ thống Quản lý và Đánh giá Hồ sơ Tuyển dụng Tích hợp AI (AI Recruitment & Candidate Management Platform)  
> **Đơn vị triển khai:** Phòng Nhân sự (HR Department) của Doanh nghiệp - Mô hình Tuyển dụng Nội bộ (In-house HR)  
> **Mục tiêu:** Tự động hóa, tối ưu hóa quy trình sàng lọc, xếp hạng ứng viên và hỗ trợ tư vấn 2 chiều (HR Recruiter & Candidate) bằng AI.

> **LƯU Ý PHIÊN BẢN (Revision Note):** Mô hình nghiệp vụ đã được điều chỉnh từ "Công ty Dịch vụ Tuyển dụng (Agency)" sang "Doanh nghiệp Tuyển dụng Nội bộ (In-house HR)" theo xác nhận của Chủ đầu tư. Các quy tắc nghiệp vụ liên quan đến Khách hàng đối tác, Hoa hồng, Split-fee và Ownership đã được loại bỏ hoặc thay thế tương ứng.

---

## MỤC LỤC LỘ TRÌNH PHỎNG VẤN (100 CÂU HỎI)

- **Đợt 1 (Câu 1 - 20):** Mô hình tổ chức (Business Model), Quản trị Job & Phân quyền
- **Đợt 2 (Câu 21 - 40):** Module Ứng viên (Candidate Portal, Nộp hồ sơ, Quản lý CV, Trải nghiệm)
- **Đợt 3 (Câu 41 - 65):** Module Nhà tuyển dụng (Sàng lọc Pipeline, Tiêu chí Lọc cứng/Lọc mềm, Xếp hạng & Đánh giá)
- **Đợt 4 (Câu 66 - 85):** Giải pháp AI chi tiết (Automation, Parsing, Scoring, Recommendation, AI Copilot/Advisor)
- **Đợt 5 (Câu 86 - 100):** Xử lý Ngoại lệ (Edge Cases), Chống gian lận/Spam, Bảo mật dữ liệu (GDPR) & Tích hợp

---

## NHẬT KÝ PHỎNG VẤN & QUYẾT ĐỊNH NGHIỆP VỤ

### ĐỢT 1: MÔ HÌNH TỔ CHỨC, QUẢN TRỊ JOB & PHÂN QUYỀN

#### Câu 1: Mô hình hoạt động cốt lõi của hệ thống

- **Câu hỏi:** Hệ thống phục vụ mô hình hoạt động nào của công ty?
- **Các phương án:**
  - A. Công ty Tuyển dụng / Săn đầu người (Headhunting / Recruitment Agency)
  - B. Bộ phận nhân sự nội bộ doanh nghiệp (In-house HR)
  - C. Nền tảng SaaS / Marketplace tuyển dụng mở
- **Quyết định (ĐÃ ĐIỀU CHỈNH):** **B - Bộ phận nhân sự nội bộ doanh nghiệp (In-house HR)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. Dữ liệu phân cấp theo cấu trúc: `Công ty -> Phòng ban/Bộ phận (Department) -> Jobs (Vị trí tuyển dụng) -> Candidates`.
  2. Hệ thống phục vụ nhu cầu tuyển dụng nhân sự cho chính doanh nghiệp; không có đối tượng Khách hàng dịch vụ, không có phí hoa hồng tuyển dụng.
  3. Bổ sung nghiệp vụ **Quản lý Ngân sách Nhân sự (Headcount Budget)**: Trưởng phòng gửi yêu cầu tuyển dụng -> Giám đốc/Ban lãnh đạo phê duyệt -> HR triển khai.

---

#### Câu 2: Quản trị Kho dữ liệu Ứng viên Nội bộ (Internal Talent Pool)

- **Câu hỏi:** Kho dữ liệu hồ sơ ứng viên của doanh nghiệp được quản lý và chia sẻ như thế nào giữa các bộ phận?
- **Các phương án:**
  - A. Kho tập trung mở hoàn toàn (Shared Talent Pool)
  - B. Phân lập độc quyền theo từng Recruiter (Siloed)
  - C. Mô hình chuẩn quốc tế: Shared Talent Pool có cơ chế "Ownership & Exclusivity Period"
- **Quyết định (ĐÃ ĐIỀU CHỈNH):** **Talent Pool Nội bộ Dùng chung (Internal Shared Talent Pool)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. Toàn bộ hồ sơ ứng viên thuộc sở hữu của Doanh nghiệp, lưu trữ tập trung trong Kho Ứng viên Nội bộ (Internal Talent Pool). AI có quyền tìm kiếm và đề xuất ứng viên cho mọi vị trí đang tuyển.
  2. **Bỏ hoàn toàn** cơ chế Ownership độc quyền, thời hạn bảo vệ và Split-fee hoa hồng (đặc thù Agency, không áp dụng cho In-house).
  3. Mỗi hồ sơ ứng tuyển (Application) được gán cho HR Recruiter phụ trách vị trí đó để theo dõi và chịu trách nhiệm chính.

---

#### Câu 3: Tương tác của Trưởng phòng Chuyên môn (Hiring Managers) với Hệ thống

- **Câu hỏi:** Trưởng phòng chuyên môn (người trực tiếp sử dụng nhân sự) có được cấp quyền truy cập để xem xét và phản hồi hồ sơ ứng viên không?
- **Các phương án:**
  - A. Không cấp tài khoản (Internal Only, trao đổi qua Email/File xuất bên ngoài)
  - B. Cấp tài khoản Cổng Khách hàng (Client Portal có username/password)
  - C. Mô hình lai qua Link bảo mật (One-time secure token link / Guest Portal không cần nhớ mật khẩu)
- **Quyết định (ĐÃ ĐIỀU CHỈNH):** **C - Mô hình lai qua Link bảo mật (One-time secure token link / Hiring Manager Portal)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. Trưởng phòng chuyên môn (Hiring Manager) nhận email có link mã hóa bảo mật (Magic Link chứa JWT/UUID token, có hạn sử dụng ví dụ 7 ngày) ngay khi HR gửi danh sách Shortlist.
  2. Khi mở link, Trưởng phòng xem được: bản tóm tắt hồ sơ chuẩn hóa, điểm AI đánh giá, lý giải của AI (AI explanation) và CV ứng viên.
  3. Trưởng phòng thao tác trực tiếp: "Chấp thuận phỏng vấn" (chọn khung giờ có sẵn), "Từ chối" (chọn lý do), hoặc "Yêu cầu thêm thông tin". Kết quả lập tức đồng bộ về hệ thống của HR Recruiter.

---

#### Câu 4: Cơ cấu Phân quyền Nội bộ Doanh nghiệp (User Roles & RBAC)

- **Câu hỏi:** Trong Phòng Nhân sự và các bộ phận liên quan, cơ cấu phân quyền gồm những cấp độ nào?
- **Các phương án:**
  - A. Tinh gọn 2 cấp (Admin & Recruiter)
  - B. Chuẩn hóa 3 cấp theo cấu trúc Agency thực tế (Admin/Director, Consultant/Account Manager, Sourcer/Researcher)
  - C. Phân quyền động tùy biến linh hoạt (Dynamic Custom RBAC)
- **Quyết định (ĐÃ ĐIỀU CHỈNH):** **B - Cấu trúc phân quyền 4 vai trò In-house HR**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **HR Manager / Admin (Trưởng phòng Nhân sự):** Quản trị hệ thống tuyển dụng, cấu hình luật chấm điểm AI toàn cục, phê duyệt ngân sách tuyển dụng, xem toàn bộ báo cáo chất lượng tuyển dụng (Quality of Hire).
  2. **HR Recruiter (Chuyên viên Tuyển dụng):** Tạo và quản lý vị trí tuyển dụng, nạp/sàng lọc hồ sơ bằng AI, gửi danh sách Shortlist cho Trưởng phòng chuyên môn, điều phối lịch phỏng vấn và thương lượng Offer.
  3. **Hiring Manager (Trưởng phòng chuyên môn):** Xem và phản hồi các ứng viên Shortlist của phòng mình qua Magic Link/Tài khoản nhẹ, cùng tham gia đánh giá và ra quyết định tuyển chọn.
  4. **Interviewer (Người phỏng vấn):** Thành viên hội đồng phỏng vấn được phân công; chỉ xem và chấm điểm Scorecard ứng viên trong các vòng phỏng vấn được giao.
  5. Về kiến trúc: Hệ thống vẫn thiết kế theo cơ chế RBAC dựa trên quyền chi tiết (Permissions), với 4 vai trò trên là bộ vai trò mặc định.

---

#### Câu 5: Quy trình Thiết lập Tin tuyển dụng (Job Requisition & AI JD Parsing)

- **Câu hỏi:** Khi tiếp nhận yêu cầu tuyển dụng từ Trưởng phòng chuyên môn (hoặc Ban lãnh đạo), quy trình tạo và thiết lập Tin tuyển dụng (Job Requisition) diễn ra như thế nào?
- **Các phương án:**
  - A. Nhập liệu thủ công theo form cấu trúc
  - B. Tải lên file JD và AI tự động bóc tách (AI JD Parsing)
  - C. AI Bóc tách + AI Tối ưu hóa & Benchmark thị trường (Skill Ontology & Normalization)
- **Quyết định:** **C - AI Bóc tách + AI Tối ưu hóa & Benchmark thị trường**.
- **Quy tắc nghiệp vụ xác lập:**
  1. Hệ thống cho phép upload file JD thô (PDF, DOCX, TXT) hoặc nhập text/link tuyển dụng.
  2. Engine AI phân tích và chuẩn hóa dữ liệu thành các trường có cấu trúc:
     - Kỹ năng bắt buộc (Must-have skills) & Kỹ năng ưu tiên (Nice-to-have skills).
     - Số năm kinh nghiệm tối thiểu, cấp bậc yêu cầu (Junior/Mid/Senior/Lead...).
     - Bằng cấp, chứng chỉ bắt buộc hoặc ưu tiên.
     - Dải lương ngân sách (Salary range), địa điểm và hình thức làm việc (On-site/Remote/Hybrid).
  3. **Cơ chế Chuẩn hóa Kỹ năng (Skill Ontology & Normalization):** AI tự động ánh xạ các từ khóa kỹ năng tương đương (ví dụ: `Golang` <-> `Go`, `React` <-> `ReactJS` <-> `Next.js`) để làm cơ sở cho thuật toán Semantic Matching CV sau này.
  4. **AI Cảnh báo & Khuyến nghị (JD Insights):** Cảnh báo nếu JD có mâu thuẫn (ví dụ ngân sách quá thấp so với yêu cầu Senior, hoặc yêu cầu kỹ năng quá rộng/thiếu thực tế). HR Recruiter có thể chỉnh sửa trước khi kích hoạt Job.

---

#### Câu 6: Chế độ Hiển thị Tin tuyển dụng (Job Visibility) trên Cổng Ứng viên

- **Câu hỏi:** Trong môi trường Agency, chế độ hiển thị của Tin tuyển dụng đối với Ứng viên trên Cổng tìm việc (Candidate Portal) được quy định như thế nào?
- **Các phương án:**
  - A. Tất cả đều công khai (100% Public)
  - B. Hỗ trợ 3 chế độ hiển thị linh hoạt (Public, Confidential / Blind Job, Headhunt Only / Private)
  - C. Chỉ làm Headhunt ngầm nội bộ (100% Private)
- **Quyết định (ĐÃ ĐIỀU CHỈNH):** **Hỗ trợ 3 chế độ hiển thị phù hợp Tuyển dụng Nội bộ**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Public (Công khai):** Hiển thị đầy đủ tên công ty, logo, địa điểm, chế độ đãi ngộ trên Cổng tuyển dụng của doanh nghiệp (và các kênh đăng tin) để thu hút ứng viên bên ngoài.
  2. **Internal Only (Nội bộ):** Chỉ hiển thị trên Cổng nội bộ dành riêng cho nhân viên đang làm việc tại công ty (phục vụ Internal Mobility - luân chuyển/thăng tiến nội bộ); không đăng ra bên ngoài.
  3. **Confidential / Ẩn danh Nội bộ:** Không hiển thị công khai; chỉ dành cho các vị trí thay thế nhân sự nhạy cảm (ví dụ: tuyển người thay thế vị trí quản lý cấp cao). Hồ sơ chỉ hiển thị ẩn danh cho Ban lãnh đạo và HR được phân quyền.

---

#### Câu 7: Cơ chế Cấu hình Trọng số Đánh giá AI (AI Scoring Weights) cho từng Job

- **Câu hỏi:** Khi thiết lập một Job, cơ chế cấu hình Trọng số đánh giá AI (AI Scoring Weights) cho từng vị trí tuyển dụng sẽ hoạt động như thế nào?
- **Các phương án:**
  - A. Bộ trọng số cố định toàn hệ thống cho mọi ngành nghề
  - B. Tùy chỉnh thủ công bằng thanh trượt theo từng Job
  - C. AI tự động phân tích JD và gợi ý cấu hình tối ưu (AI-suggested Weights), cho phép Recruiter tinh chỉnh lại
- **Quyết định:** **C - AI gợi ý cấu hình trọng số thông minh + Cho phép tinh chỉnh**.
- **Quy tắc nghiệp vụ xác lập:**
  1. Khi tạo Job, sau bước bóc tách JD, AI tự động phân tích cấp bậc (Seniority) và đặc thù vị trí để đề xuất bộ trọng số phù hợp (Tổng = 100%):
     - Ví dụ vị trí Kỹ thuật/Senior: Kỹ năng chuyên môn (45%), Kinh nghiệm thực chiến (40%), Bằng cấp (5%), Kỹ năng mềm (10%).
     - Ví dụ vị trí Quản lý/Sales: Kỹ năng giao tiếp/lãnh đạo (35%), Kinh nghiệm quản lý (35%), Mạng lưới quan hệ/chứng chỉ (15%), Ngoại ngữ (15%).
     - Ví dụ vị trí Fresher/Học thuật: Học vấn & Bằng cấp (35%), Kỹ năng nền tảng (35%), Điểm rèn luyện/Dự án trường học (20%), Ngoại ngữ (10%).
  2. HR Recruiter có toàn quyền kéo thanh trượt (slider) để điều chỉnh lại theo đúng yêu cầu riêng của từng vị trí/phòng ban.
  3. Cài đặt **Ngưỡng điểm đạt tối thiểu (Minimum Passing Score)** cho từng Job (mặc định ví dụ 70/100 điểm) để phân luồng tự động các hồ sơ nộp về.

---

#### Câu 8: Xử lý Tiêu chí Loại trừ Bắt buộc (Knockout Criteria / Hard Filters)

- **Câu hỏi:** Khi AI phát hiện ứng viên vi phạm tiêu chí bắt buộc (thiếu chứng chỉ, không đúng địa điểm, kinh nghiệm dưới chuẩn), hệ thống sẽ xử lý như thế nào?
- **Các phương án:**
  - A. Tự động đánh rớt ngay lập tức (Auto-Reject)
  - B. Không đánh rớt, chỉ gắn cờ cảnh báo đỏ (Flag & Alert) để con người tự quyết định
  - C. Tùy biến linh hoạt theo từng Job (Cấu hình riêng cho từng tiêu chí: Auto-Reject hoặc Flag Review, kèm độ trễ email từ chối)
- **Quyết định:** **C - Tùy biến linh hoạt theo từng tiêu chí (Auto-Reject vs Flag Review + Delay Email)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. Khi cấu hình tiêu chí bắt buộc cho Job, Recruiter được chọn hành vi xử lý khi ứng viên không đạt:
     - **Chế độ Auto-Reject:** Áp dụng cho các vi phạm mang tính pháp lý hoặc không thể thương lượng (ví dụ: thiếu chứng chỉ hành nghề bắt buộc, từ chối làm việc tại địa điểm quy định). Hồ sơ tự động chuyển sang trạng thái `Disqualified`.
     - **Chế độ Flag & Alert:** Áp dụng cho các tiêu chí có thể linh hoạt (ví dụ: kinh nghiệm thiếu 3-6 tháng nhưng điểm kỹ năng rất cao). Hồ sơ vẫn được đưa vào hàng đợi với nhãn cảnh báo đỏ để Recruiter quyết định.
  2. **Cơ chế Trì hoãn Thông báo (Polite Delay Notification):** Đối với các hồ sơ bị Auto-Reject, hệ thống không gửi email từ chối ngay lập tức mà cấu hình trì hoãn (mặc định 24h - 48h) để đảm bảo hình ảnh chuyên nghiệp và trải nghiệm nhân văn cho ứng viên.

---

#### Câu 9: Thiết kế Quy trình Tuyển dụng (Hiring Pipeline & Guarantee Period)

- **Câu hỏi:** Quy trình Tuyển dụng (Hiring Pipeline Stages / Kanban Workflow) để quản lý hồ sơ ứng viên từ lúc nộp đến khi đi làm thành công được thiết kế như thế nào?
- **Các phương án:**
  - A. Quy trình chuẩn cố định chung cho mọi Job
  - B. Tùy biến các bước theo từng Job
  - C. Tùy biến linh hoạt theo Job + Tích hợp 2 giai đoạn sống còn của Agency (Client Submittal & Probation Guarantee)
- **Quyết định (ĐÃ ĐIỀU CHỈNH):** **C - Tùy biến linh hoạt theo Job + Vòng Trưởng phòng duyệt & Theo dõi Thử việc**.
- **Quy tắc nghiệp vụ xác lập:**
  1. Cho phép cấu hình các vòng tuyển dụng linh hoạt theo từng Job (Sơ loại hồ sơ -> Sàng lọc AI -> Phỏng vấn HR -> Test kỹ năng -> Trưởng phòng duyệt Shortlist -> Phỏng vấn chuyên môn -> Offer).
  2. **Vòng Trưởng phòng duyệt (Hiring Manager Review):** Hồ sơ đạt chuẩn được gom vào Shortlist, sinh Magic Link gửi cho Trưởng phòng chuyên môn xem xét và phản hồi trực tuyến trước khi mời phỏng vấn.
  3. **Theo dõi Thử việc & Chất lượng Tuyển dụng (Probation Tracking & Quality of Hire):**
     - Sau khi ứng viên nhận việc (Hired), hệ thống tự động theo dõi mốc thời gian thử việc (30/60/90 ngày).
     - Hết thử việc thành công: Hệ thống chốt trạng thái "Đạt thử việc", ghi nhận KPI tuyển dụng cho HR Recruiter và Trưởng phòng, cập nhật vào báo cáo Chất lượng Tuyển dụng (Quality of Hire) gửi Ban lãnh đạo.
     - Nếu ứng viên nghỉ việc trong thời gian thử việc: Hệ thống cảnh báo Early Attrition, yêu cầu HR phối hợp Trưởng phòng ghi nhận lý do và mở lại yêu cầu tuyển dụng.

---

#### Câu 10: Cơ chế Lên lịch và Điều phối Phỏng vấn (Interview Scheduling)

- **Câu hỏi:** Việc Lên lịch và Điều phối Phỏng vấn giữa Agency Recruiter – Khách hàng – Ứng viên sẽ được hệ thống xử lý như thế nào?
- **Các phương án:**
  - A. Ghi nhận thủ công (Manual Logging)
  - B. Bán tự động (Tích hợp Email & File lịch Calendar .ics)
  - C. Tự động hóa chọn lịch thông minh (Self-scheduling kiểu Calendly + Auto Reminder 24h/2h)
- **Quyết định:** **C - Theo khuyến nghị (Tự động hóa chọn lịch thông minh + Nhắc hẹn đa kênh)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. HR Recruiter hoặc Trưởng phòng (Hiring Manager) cấu hình các khung giờ rảnh (Free Slots) trên hệ thống.
  2. Hệ thống gửi link chọn lịch riêng cho ứng viên qua Email/SMS: Ứng viên chủ động chọn khung giờ phù hợp nhất.
  3. Hệ thống tự động tạo phòng họp (tích hợp Google Meet / Zoom / MS Teams) và đồng bộ lịch Calendar (.ics) cho cả 3 bên.
  4. Cơ chế thông báo nhắc hẹn tự động (Auto Reminders): Gửi email/SMS nhắc nhở trước 24 giờ và trước 2 giờ kèm nút "Xác nhận tham gia" hoặc "Xin đổi lịch", giúp giảm tỷ lệ No-show xuống mức thấp nhất.

---

#### Câu 11: Kênh Tiếp nhận Hồ sơ Ứng viên (Candidate Ingestion Channels)

- **Câu hỏi:** Hồ sơ ứng viên (CV/Resume) sẽ đi vào hệ thống qua những kênh tiếp nhận nào?
- **Các phương án:**
  - A. Chỉ nhận ứng viên tự nộp trên web (Inbound only)
  - B. Nhận tự nộp + Recruiter tải file thủ công
  - C. Hệ sinh thái tiếp nhận đa kênh toàn diện (Omni-channel Ingestion: Web Inbound, Bulk Upload Zip/Folder, Email-to-Parse, Sourcing Extension)
- **Quyết định:** **C - Hệ sinh thái tiếp nhận đa kênh toàn diện**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Web Inbound Portal:** Ứng viên nộp hồ sơ nhanh trực tiếp trên Cổng việc làm (chỉ cần họ tên, liên hệ + file CV).
  2. **Bulk Upload (Tải lên hàng loạt):** Recruiter có thể kéo thả cả thư mục hoặc file `.zip` chứa nhiều file CV. Hệ thống đẩy vào hàng đợi Message Queue (RabbitMQ/BullMQ) để worker xử lý ngầm, trích xuất thông tin song song mà không làm đơ giao diện.
  3. **Email Ingestion (Hòm thư tự động):** Hệ thống có dịch vụ lắng nghe email (qua IMAP/Webhook), tự động tải file CV đính kèm, bóc tách và phân loại vào Job tương ứng hoặc Talent Pool.
  4. **Browser Sourcing Extension (Giai đoạn mở rộng):** Tích hợp tiện ích trình duyệt hỗ trợ bóc tách hồ sơ ứng viên từ các mạng tuyển dụng chuyên nghiệp.

---

#### Câu 12: Cơ chế Xử lý Trùng lặp Hồ sơ (Candidate Deduplication & Conflict Resolution)

- **Câu hỏi:** Khi hồ sơ ứng viên được nạp vào hệ thống từ nhiều kênh khác nhau, cơ chế xử lý trùng lặp sẽ hoạt động như thế nào?
- **Các phương án:**
  - A. Không kiểm tra trùng lặp (lưu bản ghi độc lập)
  - B. Chặn cứng nhắc theo Email/Số điện thoại
  - C. Nhận diện trùng lặp thông minh & Hợp nhất hồ sơ có phiên bản (Smart Deduplication & Versioned Merging)
- **Quyết định:** **C - Nhận diện trùng lặp thông minh & Hợp nhất hồ sơ có phiên bản**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Thuật toán đối chiếu (Entity Resolution):** Kiểm tra trùng lặp dựa trên Email HOẶC Số điện thoại (kết hợp thuật toán so khớp mờ Fuzzy match trên Họ tên + Trường đào tạo/Ngày sinh).
  2. **Hợp nhất dữ liệu (Profile Merging):**
     - Ứng viên chỉ tồn tại duy nhất 1 bản ghi định danh (Single Candidate Entity) trong hệ thống.
     - File CV mới được thêm vào lịch sử phiên bản (`CV Version History: v1, v2, v3...`). Hệ thống tự động cập nhật các kỹ năng, kinh nghiệm mới nhất vào hồ sơ gốc.
  3. **Đơn ứng tuyển độc lập (Multiple Applications):** Mỗi lần ứng viên nộp vào một Job khác nhau sẽ tạo ra một bản ghi `Application` riêng biệt, giữ nguyên trạng thái và lịch sử ứng tuyển độc lập cho Job đó.
  4. **Thông báo Nội bộ (Internal Notification):** Khi ứng viên đã tồn tại trong Kho Ứng viên Nội bộ nộp vào một vị trí mới, hệ thống tự động thông báo cho HR Recruiter phụ trách vị trí mới biết về lịch sử ứng tuyển trước đây; đồng thời ghi nhận luồng thông tin để tránh xử lý trùng lặp. Bỏ cơ chế cảnh báo độc quyền/split-fee của mô hình Agency.

---

### ĐỢT 2: MODULE ỨNG VIÊN & TRẢI NGHIỆM NỘP HỒ SƠ

#### Câu 13: Cơ chế Đăng ký, Đăng nhập & Xác thực Ứng viên (Candidate Authentication)

- **Câu hỏi:** Khi ứng viên truy cập vào Cổng tìm việc để nộp hồ sơ, cơ chế Đăng ký & Đăng nhập sẽ được thiết kế như thế nào?
- **Các phương án:**
  - A. Bắt buộc tạo tài khoản mật khẩu truyền thống trước khi nộp
  - B. Nộp đơn dạng Khách (Guest Application - Không tài khoản)
  - C. Nộp đơn 1 chạm không cần mật khẩu (Passwordless / Social Login) + Tự động tạo tài khoản ngầm
- **Quyết định:** **C - Nộp đơn 1 chạm không cần mật khẩu (Passwordless / Social Login) + Tự động tạo tài khoản ngầm**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Trải nghiệm nộp đơn tối giản:** Ứng viên có thể nộp bằng 1 click qua Google/LinkedIn OAuth, hoặc chỉ cần tải file CV và nhập Email/Số điện thoại. Không bắt buộc nhập mật khẩu.
  2. **Khởi tạo tài khoản ngầm (Headless Account Creation):** Hệ thống tự động kích hoạt tài khoản ứng viên gắn với Email/SĐT đó.
  3. **Đăng nhập một chạm (Magic Link & OTP):** Khi ứng viên muốn quay lại xem tiến độ hồ sơ, hệ thống gửi Magic Link đăng nhập qua Email hoặc mã OTP qua SMS/Zalo. Không lo quên mật khẩu, bảo mật tối đa.
  4. **Candidate Dashboard:** Sau khi nộp đơn, ứng viên truy cập ngay Dashboard để theo dõi trực quan tiến độ hồ sơ (Timeline/Kanban), quản lý các phiên bản CV đã nộp và xem nhận xét/gợi ý việc làm từ AI.

---

#### Câu 14: Định dạng File Tiếp nhận & Công nghệ Bóc tách CV (OCR & Multimodal Parsing)

- **Câu hỏi:** Về mặt kỹ thuật tiếp nhận file, các định dạng file CV và cơ chế bóc tách dữ liệu (CV Parsing & OCR) được quy định như thế nào?
- **Các phương án:**
  - A. Chỉ nhận file PDF text chuẩn (không scan ảnh) và DOCX
  - B. Hỗ trợ PDF/Word thông thường + Regex Parsing cơ bản
  - C. Hỗ trợ toàn diện (PDF, DOCX, DOC, Ảnh chụp/Scan) + AI Multimodal OCR & Xử lý song ngữ Anh - Việt
- **Quyết định:** **C - Hỗ trợ toàn diện (PDF, DOCX, Ảnh) + AI Multimodal OCR & Xử lý song ngữ Anh - Việt**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Đa định dạng tiếp nhận:** Cho phép tải lên `.pdf`, `.docx`, `.doc`, `.png`, `.jpg`, `.jpeg`. Giới hạn dung lượng an toàn (ví dụ: tối đa 15MB/file).
  2. **Pipeline Trích xuất Đa tầng (Hybrid Parsing Pipeline):**
     - Tầng 1: Trích xuất text trực tiếp từ digital PDF / Word documents để tối ưu tốc độ và chi phí.
     - Tầng 2: Nếu phát hiện file scan hoặc định dạng phức tạp nhiều cột (Canva layouts) -> Kích hoạt Optical Character Recognition (OCR) kết hợp Vision LLM để tái lập đúng cấu trúc phân vùng dữ liệu.
  3. **Xử lý Song ngữ Anh - Việt (Cross-lingual Processing):** Hệ thống có khả năng nhận diện và chuẩn hóa thuật ngữ tiếng Việt có dấu, không dấu và tiếng Anh. Hỗ trợ đối chiếu chéo (ví dụ: CV tiếng Việt so khớp với JD tiếng Anh và ngược lại mà không suy giảm độ chính xác).

---

#### Câu 15: Trợ lý AI Tư vấn & Tối ưu hóa CV cho Ứng viên (AI CV Copilot & Pre-application Check)

- **Câu hỏi:** Khi ứng viên tải file CV lên để chuẩn bị nộp cho một vị trí, hệ thống sẽ cung cấp tính năng Trợ lý AI Tư vấn & Tối ưu hóa như thế nào?
- **Các phương án:**
  - A. Không có AI tư vấn cho ứng viên (chỉ dùng nội bộ Recruiter)
  - B. AI bóc tách và tự động điền form cơ bản (Auto-fill Profile)
  - C. Trợ lý AI Tư vấn & Tối ưu hóa CV toàn diện (AI CV Copilot & Career Advisor)
- **Quyết định:** **C - Trợ lý AI Tư vấn & Tối ưu hóa CV toàn diện (AI CV Copilot & Career Advisor)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Auto-fill Profile Builder:** AI tự động bóc tách dữ liệu từ file CV tải lên thành hồ sơ web trực quan có cấu trúc, cho phép ứng viên chỉnh sửa, bổ sung kỹ năng, dự án trực tiếp trên web trước khi nộp.
  2. **Phân tích Độ sẵn sàng (Pre-flight Match Check):** AI đối chiếu hồ sơ ứng viên với JD của vị trí đang nộp, hiển thị trực quan các điểm mạnh tương thích và những kỹ năng quan trọng trong JD mà CV chưa đề cập rõ ràng (Skill Gaps).
  3. **AI CV Enhancement Tips:** Đưa ra lời khuyên thiết thực giúp ứng viên viết lại mô tả dự án/kinh nghiệm theo phương pháp định lượng thành tích (STAR: Situation - Task - Action - Result), giúp nâng cao chất lượng hồ sơ ứng viên trước khi gửi sang cho Khách hàng duyệt.

---

#### Câu 16: Cơ chế AI Gợi ý Việc làm Phù hợp cho Ứng viên (AI Job Recommendation Engine)

- **Câu hỏi:** Khi ứng viên đã có hồ sơ trên hệ thống, cơ chế AI tự động gợi ý việc làm phù hợp sẽ hoạt động như thế nào?
- **Các phương án:**
  - A. Không có gợi ý tự động (tìm kiếm thủ công)
  - B. Gợi ý theo từ khóa đơn giản (Keyword Matching)
  - C. AI Đề xuất Thông minh Đa chiều (Semantic & Skill-based Recommendation Engine qua Vector Embedding)
- **Quyết định:** **C - AI Đề xuất Thông minh Đa chiều (Semantic & Skill-based Matching)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Semantic Matching Engine:** Sử dụng kỹ thuật biểu diễn vector ngữ nghĩa (Vector Embedding) và đồ thị kỹ năng (Skill Graph) để tìm kiếm các công việc có độ tương đồng cao với năng lực của ứng viên, kể cả khi tiêu đề công việc không trùng khớp chữ từng chữ (Transferable Skills).
  2. **Chỉ số Tương thích Trực quan (Match Score & Reasons):** Hiển thị phần trăm phù hợp (ví dụ 85% - 95%) cùng bản giải trình ngắn gọn lý do vì sao công việc này phù hợp với định hướng nghề nghiệp và kinh nghiệm của ứng viên.
  3. **1-Click Apply:** Cho phép ứng viên ứng tuyển ngay vào các công việc được gợi ý chỉ với một nút bấm dựa trên hồ sơ đã được lưu trữ an toàn trong tài khoản.

---

#### Câu 17: Cơ chế Theo dõi Tiến độ Ứng tuyển & Phản hồi Nhân văn bằng AI (Status Tracking & AI Feedback)

- **Câu hỏi:** Hệ thống cho phép Ứng viên theo dõi Tiến độ Hồ sơ và nhận phản hồi như thế nào trên Dashboard?
- **Các phương án:**
  - A. Không hiển thị tiến độ
  - B. Thanh trạng thái cơ bản (Status Stepper)
  - C. Minh bạch tiến độ thời gian thực + AI Phản hồi Xây dựng (Constructive Feedback khi không đạt)
- **Quyết định:** **C - Minh bạch tiến độ thời gian thực + AI Phản hồi Xây dựng (Constructive Feedback)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Live Timeline Tracking:** Cung cấp thanh tiến trình trực quan theo thời gian thực (đã nộp -> Recruiter đã xem -> Đã gửi đề xuất sang Khách hàng -> Mời phỏng vấn -> Đang thương lượng Offer -> Hoàn tất).
  2. **Bảo mật vị trí nhạy cảm:** Đối với các vị trí Confidential (nội bộ/ẩn danh), hệ thống tự động hiển thị tên định danh chung (ví dụ: "Vị trí Quản lý cấp cao - Khối Vận hành") nhằm bảo vệ tính bí mật của kế hoạch nhân sự nội bộ cho đến khi được phép công bố.
  3. **AI Constructive Feedback:** Trong trường hợp hồ sơ không được chọn, AI tự động sinh nội dung phản hồi mang tính đóng góp, chỉ ra các kỹ năng cần hoàn thiện thêm và khuyến khích ứng viên tiếp tục nhận các cơ hội phù hợp khác trong tương lai từ Talent Pool của Agency.

---

#### Câu 18: Quyền Kiểm soát Dữ liệu Cá nhân của Ứng viên (Data Privacy & Self-service Control)

- **Câu hỏi:** Hệ thống quy định quyền kiểm soát dữ liệu cá nhân của ứng viên như thế nào để tuân thủ Nghị định 13/2023/NĐ-CP và chuẩn bảo mật?
- **Các phương án:**
  - A. Hồ sơ một chiều (không sửa/xóa)
  - B. Chỉnh sửa thông tin cơ bản
  - C. Tuân thủ chuẩn bảo vệ dữ liệu toàn diện (Nghị định 13 & GDPR) + Quản trị trạng thái tìm việc chủ động
- **Quyết định:** **C - Tuân thủ chuẩn bảo vệ dữ liệu toàn diện (Nghị định 13 / GDPR) + Quản trị trạng thái tìm việc**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Quyền rút đơn (Withdraw Application):** Ứng viên có thể chủ động rút đơn ứng tuyển nếu đã có công việc khác hoặc thay đổi ý định. Hệ thống tự động gỡ hồ sơ khỏi Pipeline hiện tại và gắn nhãn "Withdrawn by Candidate".
  2. **Trạng thái Tìm việc (Talent Pool Visibility Toggle):** Cung cấp công tắc bật/tắt: "Đang mở cơ hội mới" (AI được quét và gợi ý) hoặc "Tạm thời không tìm việc" (AI tạm ẩn hồ sơ, không phân phối cho Recruiter).
  3. **Quyền được lãng quên (Right to be Forgotten):** Ứng viên có quyền yêu cầu xóa vĩnh viễn dữ liệu định danh (file CV, số điện thoại, email, địa chỉ). Hệ thống thực hiện Soft-delete rồi Hard-delete sau thời gian lưu trữ pháp lý quy định, chỉ giữ lại định danh ẩn danh phục vụ thống kê tổng thể.

---

#### Câu 19: Hộp thư Giao tiếp Hợp nhất & AI Hỗ trợ Tương tác (Unified Communication Hub & AI Messaging)

- **Câu hỏi:** Hệ thống hỗ trợ các kênh liên lạc giữa Agency Recruiter và Ứng viên như thế nào ngay trên nền tảng?
- **Các phương án:**
  - A. Hoàn toàn thủ công bên ngoài (điện thoại/email cá nhân)
  - B. Gửi Email template từ hệ thống + Ghi chú cuộc gọi
  - C. Hộp thư Giao tiếp Hợp nhất Đa kênh (In-app Chat, Zalo ZNS/Email Fallback, AI Smart Reply & Generator)
- **Quyết định:** **C - Hộp thư Giao tiếp Hợp nhất Đa kênh (In-app Chat, Zalo/Email Fallback, AI Smart Reply)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **In-app Chat thời gian thực:** Hỗ trợ nhắn tin trực tiếp giữa Recruiter và Ứng viên ngay trên giao diện web/dashboard, cho phép gửi tệp tin, đường dẫn bài test kỹ năng.
  2. **Cơ chế Chuyển tiếp Tự động (Omni-channel Fallback):** Khi ứng viên không trực tuyến trên web, tin nhắn hoặc thông báo quan trọng sẽ tự động gửi qua Zalo Notification Service (ZNS) hoặc Email có nút trả lời nhanh.
  3. **AI Smart Reply & Message Generator:** Hỗ trợ Recruiter sinh tự động thư mời phỏng vấn, thư đề nghị nhận việc (Offer letter), hoặc phản hồi thắc mắc của ứng viên với văn phong chuyên nghiệp, đúng nhận diện thương hiệu Agency. Lịch sử trao đổi được lưu vết tập trung trong hồ sơ ứng viên để người quản lý kiểm tra chất lượng.

---

#### Câu 20: Trợ lý AI Tư vấn Hướng nghiệp & Hỏi đáp Tuyển dụng 24/7 (AI Career Assistant & FAQ Chatbot)

- **Câu hỏi:** Trên Cổng thông tin của Agency, hệ thống có trang bị Trợ lý AI Tư vấn & Giải đáp 24/7 cho ứng viên không?
- **Các phương án:**
  - A. Không có Chatbot
  - B. Chatbot kịch bản tĩnh dựa trên nút bấm (Rule-based FAQ Bot)
  - C. Trợ lý AI Hướng nghiệp & Tư vấn Tuyển dụng 24/7 (AI Career Assistant dựa trên RAG & LLM)
- **Quyết định:** **C - Trợ lý AI Hướng nghiệp & Tư vấn Tuyển dụng 24/7 (RAG & LLM)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Context-aware RAG Engine:** AI truy xuất tự động từ cơ sở tri thức công khai của Agency và các tin tuyển dụng đang mở để trả lời chính xác mọi câu hỏi về yêu cầu, chế độ đãi ngộ, văn hóa làm việc và lộ trình tuyển dụng.
  2. **Tư vấn Định hướng Nghề nghiệp:** Phân tích CV hoặc kinh nghiệm ứng viên tự nhập để định hướng và đề xuất các vị trí tuyển dụng phù hợp nhất đang tuyển tại Agency.
  3. **Tuân thủ Bảo mật Tuyệt đối:** Tự động nhận diện các vị trí Confidential/Internal-Only để không để lộ thông tin nhạy cảm về kế hoạch nhân sự nội bộ trong các phiên hội thoại công khai với ứng viên.

---

#### Câu 21: Công cụ Tự tạo CV Chuẩn ATS Tích hợp AI (AI-Powered ATS-friendly CV Builder)

- **Câu hỏi:** Trên Cổng Ứng viên, hệ thống có cung cấp công cụ tự tạo CV trực tuyến tích hợp AI không?
- **Các phương án:**
  - A. Không hỗ trợ (ứng viên tự chuẩn bị file ngoài)
  - B. Form điền thông tin và xuất PDF cơ bản
  - C. Bộ công cụ Tạo CV chuẩn ATS thông minh (AI-Powered ATS CV Builder)
- **Quyết định:** **C - Bộ công cụ Tạo CV chuẩn ATS thông minh**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Thư viện Template chuẩn ATS:** Cung cấp đa dạng mẫu thiết kế chuyên nghiệp, cấu trúc 1 cột hoặc 2 cột được tối ưu hóa để các thuật toán máy đọc ATS trích xuất chuẩn xác 100%.
  2. **AI Content Suggestions:** Khi ứng viên chọn ngành nghề và vị trí, AI tự động gợi ý các mô tả nhiệm vụ, kỹ năng cốt lõi và mẫu định lượng kết quả thành tích phù hợp để ứng viên chèn nhanh vào CV.
  3. **Xuất bản & Tích hợp hồ sơ:** Cho phép ứng viên tải về file PDF sắc nét, đồng thời dữ liệu có cấu trúc được lưu trực tiếp vào tài khoản ứng viên trên hệ thống, sẵn sàng dùng để ứng tuyển 1 chạm cho các vị trí của Agency.

---

#### Câu 22: Quản lý Hồ sơ Năng lực Đa phương tiện & Sản phẩm Thực tế (Rich Portfolio Hub & Verified Credentials)

- **Câu hỏi:** Trên Cổng Ứng viên, hệ thống sẽ quản lý hồ sơ năng lực nâng cao (Portfolio, Dự án thực tế, Chứng chỉ & Video Pitch) như thế nào?
- **Các phương án:**
  - A. Chỉ nhận duy nhất 1 file CV
  - B. Đính kèm cơ bản (Cover letter + liên kết web ngoài)
  - C. Trung tâm Hồ sơ Năng lực Đa phương tiện (Rich Portfolio Hub) + AI Phân tích Sản phẩm thực tế
- **Quyết định:** **C - Trung tâm Hồ sơ Năng lực Đa phương tiện + AI Phân tích Sản phẩm**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Hỗ trợ đa phương tiện:** Cho phép ứng viên liên kết tài khoản chuyên môn (GitHub, Behance, Dribbble, LinkedIn), tải file Portfolio dự án (PDF/Slide), và tải lên Video Pitch giới thiệu bản thân (thời lượng 60 - 90 giây).
  2. **AI Phân tích Bằng chứng Năng lực:** Tự động đọc hiểu và bóc tách thông tin từ các sản phẩm thực tế (ví dụ: phân tích mức độ hoạt động và tech stack từ GitHub, tóm tắt các dự án nổi bật trong file Portfolio) để làm giàu thêm dữ liệu hồ sơ và cộng điểm vào tiêu chí kinh nghiệm thực chiến.
  3. **Xác thực Chứng chỉ (Verified Credentials):** Cho phép ghi nhận và hiển thị các huy hiệu chứng chỉ đã được kiểm chứng (AWS, PMP, CFA, IELTS, CPA...) nhằm tạo niềm tin tối đa khi HR gửi hồ sơ tới Trưởng phòng chuyên môn và Ban lãnh đạo.

---

#### Câu 23: Phòng Luyện Phỏng Vấn Thử với AI Đa Phương Thức (AI Mock Interview Studio & Cost Guardrails)

- **Câu hỏi:** Hệ thống có cung cấp tính năng AI Luyện Phỏng Vấn Thử cho ứng viên trên Cổng Ứng viên không và kiểm soát chi phí ra sao?
- **Các phương án:**
  - A. Không hỗ trợ
  - B. Cung cấp ngân hàng câu hỏi tĩnh
  - C. Phòng Luyện Phỏng Vấn Tương Tác AI Thông Minh (Interactive AI Mock Interview Studio kèm chốt chặn chi phí)
- **Quyết định:** **C - Phòng Luyện Phỏng Vấn Tương Tác AI Thông Minh (Kèm cơ chế kiểm soát chi phí)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Tạo câu hỏi ngữ cảnh theo Job & CV:** AI phân tích JD và hồ sơ thực tế của ứng viên để đưa ra 5 - 7 câu hỏi phỏng vấn chuẩn hóa (chuyên môn, tình huống thực tế, văn hóa ứng xử).
  2. **Tương tác Đa phương thức:** Ứng viên có thể trả lời bằng văn bản (Text) hoặc giọng nói (Voice qua Web Speech API miễn phí hoặc Speech-to-Text).
  3. **AI Chấm điểm & Góp ý theo chuẩn STAR:** AI phân tích câu trả lời, chỉ ra điểm chưa thuyết phục và đưa ra câu trả lời mẫu tối ưu để ứng viên nâng cao kỹ năng phản xạ trước vòng phỏng vấn thực tế với Khách hàng.
  4. **Cơ chế Chốt chặn Kiểm soát Chi phí (Cost Guardrails):**
     - Giới hạn số lượt dùng (Quota): Tối đa 02 - 03 lượt/ứng viên cho mỗi vị trí ứng tuyển.
     - Phân quyền theo tiến độ: Chỉ mở tính năng cho ứng viên đã qua vòng sơ loại của Agency và chuẩn bị phỏng vấn với Khách hàng.
     - Sử dụng các mô hình LLM tối ưu chi phí (GPT-4o-mini / Gemini 1.5 Flash), chi phí chỉ ~50đ VNĐ/lượt.

---

#### Câu 24: Bài Đánh Giá Năng Lực & Tương Thích Văn Hóa Trực Tuyến (AI Adaptive Assessment & Anti-cheat)

- **Câu hỏi:** Hệ thống có hỗ trợ tính năng làm bài kiểm tra năng lực và độ tương thích văn hóa trực tuyến trên Cổng Ứng viên không?
- **Các phương án:**
  - A. Không hỗ trợ làm bài test
  - B. Bộ câu hỏi trắc nghiệm tĩnh thủ công
  - C. Hệ thống Bài Test Năng Lực Thông Minh Sinh Đề Tự Động bằng AI (Adaptive Test Generation & Anti-cheat, cho phép Bật/Tắt theo Job)
- **Quyết định:** **C - Bài Test Năng Lực Sinh Đề Tự Động bằng AI (Adaptive Test + Culture Fit)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Cấu hình linh hoạt theo Job:** Recruiter có thể bật hoặc tắt yêu cầu làm bài test tùy theo tính chất vị trí tuyển dụng.
  2. **AI Adaptive Question Generation:** AI tự động tạo ngẫu nhiên ngân hàng đề trắc nghiệm 10 - 15 câu theo đúng kỹ năng trọng tâm và cấp bậc của Job, hạn chế việc chia sẻ đáp án giữa các ứng viên.
  3. **Đánh giá Phong cách Làm việc (Culture & Behavior Fit):** Tích hợp bài trắc nghiệm hành vi/tính cách ngắn (5 phút) để nhận diện phong cách làm việc (hợp tác, lãnh đạo, phân tích chi tiết, kiên định).
  4. **Cơ chế Chống Gian lận Cơ bản:** Giới hạn thời gian làm bài, tự động nộp bài khi hết giờ, phát hiện hành vi chuyển tab trình duyệt để tìm kiếm câu trả lời bên ngoài. Kết quả điểm số được đồng bộ trực tiếp vào hồ sơ ứng viên.

---

#### Câu 25: Đăng Ký Cảnh Báo Việc Làm Cá Nhân Hóa (AI Smart Job Alerts & Notification Channels)

- **Câu hỏi:** Hệ thống sẽ cung cấp tính năng Đăng ký nhận thông báo việc làm cá nhân hóa như thế nào trên Cổng Ứng viên?
- **Các phương án:**
  - A. Không hỗ trợ thông báo tự động
  - B. Bản tin email hàng tuần chung chung (Generic Newsletter)
  - C. AI Tự Động Thiết Lập Cảnh Báo Việc Làm Cá Nhân Hóa Đa Kênh (Email, Zalo, Web Push)
- **Quyết định:** **C - AI Tự Động Thiết Lập Cảnh Báo Việc Làm Cá Nhân Hóa Đa Kênh**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **AI Auto-matching Filter:** AI tự động phân tích hồ sơ và tạo tiêu chí nhận tin chuẩn xác (ngành nghề, kỹ năng cốt lõi, cấp bậc kinh nghiệm, khoảng lương mong muốn, địa điểm làm việc).
  2. **Đa kênh nhận tin:** Cho phép ứng viên linh hoạt nhận thông báo qua Email, tin nhắn Zalo ZNS hoặc Web Push Notification trên trình duyệt.
  3. **Tùy chọn tần suất:** Tức thì (ngay khi có Job phù hợp > 85%), Tổng hợp hàng ngày, hoặc Bản tin hàng tuần.
  4. **Kiểm soát & Bảo mật:** Hỗ trợ nút Hủy nhận tin 1 chạm (1-Click Unsubscribe) trong mọi thông báo, đảm bảo sự thoải mái và tuân thủ chuẩn chống spam.

---

#### Câu 26: Đo Lường Độ Hài Lòng của Ứng viên (Candidate NPS & Sentiment Feedback)

- **Câu hỏi:** Hệ thống có cung cấp tính năng đo lường mức độ hài lòng của ứng viên (Candidate NPS) không?
- **Các phương án:**
  - A. Không thu thập phản hồi
  - B. Form khảo sát dài gửi qua email sau cùng
  - C. Khảo sát 1 Chạm Tự Động (Micro-NPS 1-Click Survey) + AI Phân Tích Cảm Xúc (Sentiment Analysis)
- **Quyết định:** **C - Khảo sát 1 Chạm Tự Động + AI Phân Tích Cảm Xúc (Sentiment Analysis)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Micro-NPS 1-Click Survey:** Kích hoạt câu hỏi khảo sát ngắn gọn (đánh giá 1 - 5 sao / emoji) ngay sau các mốc chạm quan trọng (hoàn thành phỏng vấn hoặc nhận kết quả cuối cùng).
  2. **AI Phân tích Cảm xúc & Gắn thẻ Vấn đề:** Tự động đọc hiểu ý kiến đóng góp dạng chữ, phân loại sắc thái cảm xúc (Tích cực / Tiêu cực / Trung tính) và tự động gắn thẻ nguyên nhân (thái độ Recruiter, sự cố lịch hẹn, tính chuyên nghiệp của khách hàng đối tác).
  3. **Báo cáo Chất lượng Dịch vụ:** Tổng hợp chỉ số cNPS (Candidate Net Promoter Score) trên Dashboard của Ban lãnh đạo để đánh giá chất lượng phối hợp tuyển dụng của từng Phòng ban và hiệu quả hỗ trợ của từng HR Recruiter.

---

#### Câu 27: Tính Năng Giới Thiệu Ứng Viên / Bạn Bè (Candidate Referral Program)

- **Câu hỏi:** Hệ thống có cung cấp tính năng giới thiệu bạn bè nhận thưởng hoa hồng (Referral Program) trên Cổng Ứng viên không?
- **Các phương án:**
  - A. Không có tính năng giới thiệu (chỉ tập trung ứng tuyển trực tiếp cá nhân)
  - B. Ghi nhận thủ công qua ô nhập chữ
  - C. Hệ thống Giới thiệu Nhận thưởng Tự động Toàn diện (Referral Link, Bounty Tracking)
- **Quyết định:** **A - Không có tính năng giới thiệu**.
- **Quy tắc nghiệp vụ xác lập:**
  1. Hệ thống tinh gọn Module Ứng viên, tập trung 100% vào trải nghiệm nộp đơn, tự tạo hồ sơ, tương tác AI và theo dõi tiến độ của chính cá nhân ứng viên.
  2. Loại bỏ hoàn toàn các cấu trúc bảng dữ liệu phức tạp liên quan đến mã giới thiệu (Referral Codes), hoa hồng giới thiệu (Bounty Payouts) và kiểm tra gian lận liên kết chéo, giúp hệ thống nhẹ tải và tối ưu chi phí phát triển.

---

#### Câu 28: Trải Nghiệm Di Động của Ứng Viên (Mobile-First & Progressive Web App - PWA)

- **Câu hỏi:** Hệ thống sẽ hỗ trợ trải nghiệm di động cho ứng viên theo phương án công nghệ nào?
- **Các phương án:**
  - A. Web Responsive thông thường
  - B. Xây dựng Mobile App Native riêng biệt (iOS & Android)
  - C. Giao diện Web Tối Ưu Hóa Di Động (Mobile-First) + Công nghệ PWA (Progressive Web App)
- **Quyết định:** **C - Mobile-First Responsive + Progressive Web App (PWA)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Mobile-First UX/UI:** Thiết kế mọi quy trình nộp hồ sơ, xem bảng tin và chat tương tác tối ưu tuyệt đối cho thao tác một tay trên màn hình điện thoại cảm ứng.
  2. **Tích hợp Lưu trữ Đám mây Di động:** Cho phép ứng viên tải file CV hoặc hình ảnh trực tiếp từ Apple Files/iCloud, Google Drive, Microsoft OneDrive hoặc chụp ảnh camera điện thoại.
  3. **PWA Capabilities:** Hỗ trợ tính năng "Thêm vào Màn hình chính" (Add to Home Screen) mà không cần cài đặt qua App Store/Google Play, đồng thời kích hoạt Web Push Notifications để nhận thông báo lịch hẹn và tin nhắn trực tiếp trên màn hình khóa.

---

#### Câu 29: Hỗ Trợ Đa Ngôn Ngữ trên Cổng Ứng Viên (Internationalization - i18n Song Ngữ Anh - Việt)

- **Câu hỏi:** Giao diện Cổng Ứng viên sẽ hỗ trợ ngôn ngữ hiển thị như thế nào?
- **Các phương án:**
  - A. Chỉ dùng duy nhất Tiếng Việt
  - B. Chỉ dùng duy nhất Tiếng Anh
  - C. Hỗ trợ Song Ngữ Linh Hoạt (Tiếng Việt & Tiếng Anh - Tự động nhận diện)
- **Quyết định:** **C - Hỗ trợ Song Ngữ Linh Hoạt (Tiếng Việt & Tiếng Anh)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Bilingual Switcher:** Tích hợp nút chuyển đổi ngôn ngữ nhanh Tiếng Việt (VI) / Tiếng Anh (EN) trên toàn bộ Cổng ứng viên.
  2. **Tự động nhận diện (Locale Auto-detection):** Tự động phát hiện ngôn ngữ ưu tiên của trình duyệt thiết bị để hiển thị ngôn ngữ phù hợp ngay trong lần đầu truy cập.
  3. **Đồng bộ hóa Tương tác:** Toàn bộ mẫu email thông báo, tin nhắn SMS/Zalo, thông báo trạng thái hồ sơ và Trợ lý AI Chatbot sẽ phản hồi chuẩn xác theo ngôn ngữ mà ứng viên đã chọn.

---

#### Câu 30: Trợ Lý AI Tư Vấn & Định Giá Lương Thị Trường (AI Salary Benchmark & Guidance)

- **Câu hỏi:** Hệ thống có cung cấp tính năng AI tư vấn mức lương thị trường cho ứng viên trên Cổng Ứng viên không?
- **Các phương án:**
  - A. Không có tính năng tư vấn lương
  - B. Chỉ hiển thị dải lương của Job
  - C. Trợ Lý AI Tư Vấn & Định Giá Lương Thị Trường (AI Salary Benchmark & Guidance)
- **Quyết định:** **C - Trợ Lý AI Tư Vấn & Định Giá Lương Thị Trường**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Market Salary Benchmark:** AI tự động phân tích dữ liệu ngành nghề, cấp bậc, kỹ năng chuyên sâu và địa điểm để cung cấp biểu đồ dải lương thị trường tham khảo (P25, P50, P75).
  2. **Tư vấn Tương thích Ngân sách:** Khi ứng viên nhập mức lương kỳ vọng, nếu vượt quá trần ngân sách của vị trí đang ứng tuyển, AI đưa ra cảnh báo và lời khuyên tinh tế giúp ứng viên cân nhắc điều chỉnh để tránh bị loại sớm ở vòng lọc cứng.
  3. **Tối ưu Hóa Quy trình Đàm phán:** Giúp Phòng Nhân sự chuẩn hóa kỳ vọng tài chính của ứng viên ngay từ đầu vào, giảm thiểu thời gian đàm phán thủ công cho HR Recruiter và tăng tỷ lệ nhận Offer thành công.

---

#### Câu 31: Quản Trị Đa Phiên Bản CV Thông Minh (Smart Multi-Resume Vault)

- **Câu hỏi:** Trên Cổng Ứng viên, hệ thống sẽ quản lý nhiều phiên bản CV của một ứng viên như thế nào?
- **Các phương án:**
  - A. Chỉ lưu duy nhất 1 bản CV
  - B. Lưu file đính kèm đơn thuần
  - C. Kho Quản Trị Đa Phiên Bản CV Thông Minh (Smart Multi-Resume Vault)
- **Quyết định:** **C - Kho Quản Trị Đa Phiên Bản CV Thông Minh**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Lưu trữ nhiều phiên bản:** Cho phép ứng viên tạo và quản lý nhiều bản CV định hướng theo từng vị trí chuyên môn (ví dụ: bản tiếng Anh, bản tiếng Việt, bản Frontend, bản Tech Lead).
  2. **Primary Resume Tagging:** Cho phép gắn nhãn 1 bản CV chính (Default Profile) để AI sử dụng quét gợi ý tự động trong Kho Ứng viên Nội bộ của doanh nghiệp.
  3. **Lựa chọn khi ứng tuyển:** Khi nộp vào bất kỳ vị trí nào, ứng viên được chọn phiên bản CV tối ưu nhất cho Job đó, hệ thống giữ nguyên lịch sử bản CV đã nộp cho từng hồ sơ ứng tuyển độc lập.

---

#### Câu 32: Đồng Bộ Dữ Liệu Hồ Sơ từ Mạng Xã Hội Nghề Nghiệp (Smart Profile Ingestion từ LinkedIn & GitHub)

- **Câu hỏi:** Hệ thống có hỗ trợ tính năng đồng bộ dữ liệu hồ sơ 1 chạm từ mạng xã hội nghề nghiệp như LinkedIn và GitHub không?
- **Các phương án:**
  - A. Không hỗ trợ đồng bộ
  - B. Chỉ hỗ trợ đăng nhập Social Login đơn thuần
  - C. Đồng Bộ Dữ Liệu Hồ Sơ Thông Minh (Smart Profile Ingestion từ LinkedIn & GitHub)
- **Quyết định:** **C - Đồng Bộ Dữ Liệu Hồ Sơ Thông Minh (Chi tiết kỹ thuật sẽ chốt ở Giai đoạn Thiết kế Kiến trúc)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Đồng bộ LinkedIn:** Cho phép ứng viên kết nối/nhập hồ sơ LinkedIn để tự động trích xuất các trường dữ liệu chuẩn: quá trình làm việc, chức danh, bằng cấp học vấn, kỹ năng chuyên môn.
  2. **Đồng bộ GitHub (Chuyên ngành Tech):** Hỗ trợ liên kết tài khoản GitHub để tự động trích xuất các kho dự án công khai (Public Repositories), ngôn ngữ lập trình chủ đạo và tần suất hoạt động đóng góp mã nguồn.
  3. **Rà soát & Xác nhận (Review & Confirm):** Sau khi hệ thống kéo dữ liệu về, ứng viên được xem trước bản tóm tắt hồ sơ và có toàn quyền chỉnh sửa/bổ sung trước khi bấm lưu chính thức vào hệ thống.
  4. **Triển khai kỹ thuật:** Phương án triển khai cụ thể (kết hợp OAuth 2.0, bóc tách LinkedIn PDF và Browser Extension) sẽ được xác định chi tiết trong Giai đoạn 4 (Kiến trúc Hệ thống & Tech Stack).

---

#### Câu 33: Chế Độ Tìm Việc Kín / Ẩn Danh Thông Minh (AI Smart Incognito Mode & Company Blocklist)

- **Câu hỏi:** Trên Cổng Ứng viên, hệ thống có hỗ trợ chế độ tìm việc ẩn danh để bảo vệ danh tính đối với công ty hiện tại không?
- **Các phương án:**
  - A. Không có chế độ ẩn danh
  - B. Ứng viên tự che thủ công
  - C. Chế Độ Tìm Việc Kín Thông Minh (AI Smart Incognito Mode & Blocklist)
- **Quyết định (ĐÃ ĐIỀU CHỈNH):** **C - Chế Độ Tìm Việc Kín Nội Bộ (Internal Confidential Mode - Ẩn với Quản lý hiện tại)**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Ẩn danh với Quản lý Hiện tại (Hide from Current Manager):** Nhân viên nội bộ ứng tuyển vị trí khác có thể bật chế độ ẩn danh để Quản lý trực tiếp hiện tại của họ không nhìn thấy hồ sơ ứng tuyển này trên hệ thống.
  2. **Bỏ Company Blocklist:** Không còn áp dụng danh sách chặn công ty đối thủ (đặc thù Agency) do hệ thống chỉ phục vụ duy nhất một doanh nghiệp.
  3. **AI Blind Candidate Profile:** Khi kích hoạt chế độ kín, AI tự động thay tên nhân viên bằng mã ẩn danh UID trong các màn hình chia sẻ rộng (như báo cáo thống kê), chỉ HR phụ trách được xem danh tính thật để triển khai quy trình.
  4. **Cơ chế Tiết lộ Có Điều kiện (Consensual Disclosure):** Danh tính chỉ được công bố cho Trưởng phòng tuyển dụng và Ban lãnh đạo khi cần thiết, mọi thông tin sẽ được mở sau khi ứng viên chủ động đồng ý.

---

#### Câu 34: Quy Trình Thẩm Định Người Tham Chiếu (Reference Checking Process)

- **Câu hỏi:** Hệ thống có hỗ trợ tính năng thu thập và xác minh người tham chiếu (Reference Check) trên phần mềm không?
- **Các phương án:**
  - A. Không hỗ trợ trên hệ thống (Recruiter thực hiện thủ công bên ngoài)
  - B. Lưu trữ thông tin liên hệ cơ bản
  - C. Quy trình Thẩm tra Kỹ thuật số Tự động (Automated Digital Reference Checking)
- **Quyết định:** **A - Không hỗ trợ trên hệ thống**.
- **Quy tắc nghiệp vụ xác lập:**
  1. Quy trình thẩm tra thông tin người tham chiếu (Reference Check) sẽ được đội ngũ Recruiter của Agency thực hiện thủ công qua điện thoại hoặc trao đổi trực tiếp bên ngoài hệ thống.
  2. Phần mềm không cần xây dựng module khảo sát người tham chiếu tự động, giúp tinh gọn kiến trúc và tập trung vào các tính năng lõi. Recruiter có thể ghi chú tóm tắt kết quả thẩm tra vào ô Notes nội bộ của ứng viên nếu cần.

---

#### Câu 35: Trung Tâm Hành Động & Lịch Phỏng Vấn Trực Quan (Candidate Action Center & Calendar Hub)

- **Câu hỏi:** Trên Cổng Ứng viên, hệ thống sẽ tổ chức trung tâm lịch hẹn và các việc cần làm như thế nào trên Dashboard?
- **Các phương án:**
  - A. Không có khu vực tập trung
  - B. Danh sách văn bản đơn giản
  - C. Trung Tâm Hành Động Thông Minh & Lịch Phỏng Vấn Trực Quan (Smart Action Center & Calendar Hub)
- **Quyết định:** **C - Trung Tâm Hành Động Thông Minh & Lịch Phỏng Vấn Trực Quan**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Action Items Banner:** Hiển thị nổi bật ở đầu trang các nhiệm vụ đang chờ ứng viên tương tác (chọn khung giờ phỏng vấn, hoàn thành bài test kỹ năng trước deadline, xác nhận tham gia).
  2. **Interview Calendar Hub:** Trình bày trực quan danh sách các buổi phỏng vấn sắp diễn ra, phân loại rõ hình thức Online (tích hợp sẵn nút mở Google Meet / Zoom / MS Teams) hoặc Trực tiếp (kèm địa chỉ, chỉ dẫn đường đi và thông tin người liên hệ tại văn phòng công ty).
  3. **Đồng bộ Lịch Cá nhân:** Cung cấp nút đồng bộ 1 chạm vào Google Calendar, Apple Calendar hoặc Outlook Calendar trên máy tính và điện thoại.

---

#### Câu 36: Xuất Dữ Liệu Hồ Sơ Cá Nhân (Profile Export & Data Portability)

- **Câu hỏi:** Trên Cổng Ứng viên, hệ thống có cung cấp tính năng xuất toàn bộ dữ liệu hồ sơ cho ứng viên không?
- **Các phương án:**
  - A. Không hỗ trợ xuất dữ liệu
  - B. Chỉ cho phép tải lại file CV gốc đã nộp
  - C. Xuất Hồ Sơ Đã Chuẩn Hóa Thành File PDF & Xuất Gói Dữ Liệu Cá Nhân (Standardized PDF & Data Package Export)
- **Quyết định:** **C - Xuất Hồ Sơ Chuẩn hóa thành PDF & Xuất Gói Dữ Liệu Cá Nhân**.
- **Quy tắc nghiệp vụ xác lập:**
  1. **Chuẩn hóa Profile PDF:** Cho phép ứng viên tải về bản hồ sơ đã được AI tổng hợp và trình bày đẹp mắt dưới dạng PDF chuẩn ATS, có thể sử dụng ngay để ứng tuyển ở các kênh khác.
  2. **Data Portability Package (Quyền Chuyển dịch Dữ liệu):** Cung cấp tính năng đóng gói dữ liệu cá nhân (.zip) gồm thông tin định danh, lịch sử ứng tuyển, kết quả các bài kiểm tra đánh giá năng lực đã thực hiện, tuân thủ nghiêm ngặt Nghị định 13/2023/NĐ-CP về quyền của chủ thể dữ liệu.
  3. **Bảo mật khi tải:** Yêu cầu xác thực OTP qua Email hoặc SĐT trước khi cho phép tải gói dữ liệu nhạy cảm để đảm bảo an toàn tuyệt đối.
