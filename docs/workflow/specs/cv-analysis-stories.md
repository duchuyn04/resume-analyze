# Đặc tả User Stories & Trải nghiệm Người dùng (UX Flows) — Phân tích và tối ưu CV

| Thuộc tính | Giá trị |
|---|---|
| Artifact ID | STORIES-CV-001 |
| Revision | Draft 2; ngày 2026-09-22; hoàn thiện đặc tả theo mô hình 5 roles chuẩn vận hành |
| Chế độ | Cổng G2 — Thiết kế User Stories, Acceptance Criteria & UX Flows |
| Nguồn nghiệp vụ G1 | `docs/workflow/specs/cv-analysis-brief.md` (Artifact ID: BRIEF-CV-001, Revision Draft 6, Approved 2026-09-22) |
| Báo cáo kiểm định G1 | `docs/workflow/specs/cv-analysis-g1-audit.md` (AUDIT-G1-CV-002, PASS) |
| Prototype Mockup | [cv-analysis-mockup.html](../prototypes/cv-analysis-mockup.html) (Đã kiểm thử Browser Native) |
| Trạng thái G2 | `approved`: Người dùng đã phê duyệt chính thức ngày 2026-09-22 kèm bản Prototype Mockup tương tác; sẵn sàng bàn giao cho Cổng G3 (solution-design) |

---

## 1. Danh mục Epics và User Stories

Hệ thống kế thừa toàn diện 6 Epics từ Cổng G1 và phân rã thành 20 User Stories hoàn chỉnh:

### EP01: Quản lý tài khoản và quyền dữ liệu
- **US01:** Là Ứng viên mới, tôi muốn đăng ký bằng email và nhận link xác minh để kích hoạt tài khoản và nhận 3 lượt phân tích cơ bản miễn phí.
- **US02:** Là Ứng viên, tôi muốn xem rõ thông báo minh bạch về việc gửi dữ liệu đến AI bên ngoài và chủ động bấm đồng ý/từ chối để bảo vệ quyền riêng tư cá nhân.
- **US03:** Là Ứng viên, tôi muốn yêu cầu đóng tài khoản và xóa vĩnh viễn toàn bộ dữ liệu cá nhân sau khi xác thực lại và xác nhận từ bỏ số dư credit còn lại.
- **US20:** Là Quản trị viên hệ thống, tôi muốn quản lý danh sách tài khoản nội bộ, gán vai trò (Hỗ trợ, Lead hỗ trợ, Kế toán, Admin) và giám sát nhật ký bảo mật (audit logs).
### EP02: Quản lý CV và mô tả tuyển dụng
- **US04:** Là Ứng viên, tôi muốn tải lên file CV định dạng PDF hoặc DOCX (tối đa 10MB, 5 trang, không mật khẩu) để hệ thống trích xuất nội dung văn bản.
- **US05:** Là Ứng viên, tôi muốn xem trước, chỉnh sửa trực tiếp các trường thông tin trích xuất và bấm xác nhận để làm căn cứ chấm điểm chính xác.
- **US06:** Là Ứng viên, tôi muốn dán nội dung mô tả công việc (JD) và được hệ thống kiểm tra độ đầy đủ căn cứ (50–2.500 từ) để đảm bảo kết quả đối chiếu có giá trị.

### EP03: Quản lý đánh giá hồ sơ
- **US07:** Là Ứng viên, tôi muốn chọn chế độ kiểm tra CV tổng quát (không cần JD) bằng 1 lượt cơ bản để phát hiện lỗi định dạng, lỗi hành văn và thông tin liên hệ.
- **US08:** Là Ứng viên, tôi muốn chạy phân tích đối chiếu CV với JD theo Rubric 4 Trụ cột để biết mức độ phù hợp thực tế với vị trí ứng tuyển.
- **US09:** Là Ứng viên, tôi muốn xem bảng điểm tổng hợp, điểm thành phần với trọng số chuẩn hóa và danh sách cảnh báo yêu cầu bắt buộc chưa có bằng chứng.

### EP04: Quản lý tối ưu và xuất CV
- **US10:** Là Ứng viên dùng gói nâng cao, tôi muốn nhận các đề xuất viết lại gạch đầu dòng kinh nghiệm bám sát dữ kiện thật để tăng sức thuyết phục mà không gian dối.
- **US11:** Là Ứng viên, tôi muốn xem giao diện so sánh trực quan (Diff view) giữa nội dung cũ và đề xuất mới, cho phép sửa tay hoặc từ chối từng gợi ý.
- **US12:** Là Ứng viên, tôi muốn xuất bản CV tối ưu ra file PDF hoặc DOCX theo mẫu chuẩn văn bản để nộp cho nhà tuyển dụng hoặc hệ thống ATS.

### EP05: Quản lý credit và thanh toán
- **US13:** Là Ứng viên, tôi muốn chọn mua các gói credit cố định bằng tiền VND qua phiên thanh toán trực tuyến (hạn 15 phút); và là Kế toán, tôi muốn quản lý cấu hình các gói giá đang mở bán.
- **US14:** Là Hệ thống, tôi muốn tự động giữ 1 credit khi bắt đầu phân tích nâng cao, chỉ thu khi sinh đủ trọn bộ kết quả, và hoàn trả credit nếu xảy ra sự cố hoặc quá 90 giây.
- **US15:** Là Chuyên viên Tài chính / Kế toán, tôi muốn tra cứu danh sách đơn hàng Chờ đối soát (thanh toán muộn/báo trễ) và duyệt cộng credit cho khách hàng trong tối đa 2 ngày làm việc.

### EP06: Quản lý lịch sử và hỗ trợ
- **US16:** Là Ứng viên, tôi muốn xem lại danh sách các lần phân tích trước đây, tải lại báo cáo hoặc bản CV viết lại trong thời hạn lưu trữ 90 ngày.
- **US17:** Là Hệ thống, tôi muốn nhận diện các yêu cầu phân tích nộp lại nội dung y hệt khi báo cáo cũ còn hạn để trả ngay kết quả có sẵn mà không trừ thêm credit.
- **US18:** Là Ứng viên, tôi muốn cấp quyền hỗ trợ xem CV/báo cáo cho nhân viên hỗ trợ và trưởng nhóm theo từng vụ việc cụ thể (tối đa 72 giờ) và có thể thu hồi bất kỳ lúc nào.
- **US19:** Là Nhân viên hỗ trợ và Trưởng nhóm hỗ trợ, tôi muốn tiếp nhận khiếu nại báo cáo sai lệch, xác minh lỗi kỹ thuật và duyệt đền bù 1 lượt/credit cho ứng viên.

---

## 2. Bảng Ma trận User (Actor) và User Story

| Story ID | Tên User Story | Ứng viên | Hỗ trợ viên | Lead hỗ trợ | Kế toán | Admin | Hệ thống tự động | Quy tắc nguồn |
|:---:|---|:---:|:---:|:---:|:---:|:---:|:---:|---|
| **US01** | Đăng ký, verify email & nhận 3 lượt | Thực hiện | — | — | — | — | Tự động cấp | BR02, BR31, BR34 |
| **US02** | Đồng ý xử lý AI & giảm thiểu PII | Thực hiện | — | — | — | — | Áp dụng rule | BR08, BR29, BR33 |
| **US03** | Yêu cầu đóng tài khoản & xóa dữ liệu | Thực hiện | — | — | Đối soát giao dịch | — | Xóa dữ liệu | BR30, BR32, BR34 |
| **US04** | Tải lên file CV (PDF/DOCX) | Thực hiện | — | — | — | — | Validate file | BR03, OQ06 |
| **US05** | Xem/sửa & xác nhận nội dung trích xuất | Thực hiện | — | — | — | — | Lưu phiên bản | BR09, BR23 |
| **US06** | Nhập JD & kiểm tra căn cứ (50–2.500 từ) | Thực hiện | — | — | — | — | Kiểm tra điều kiện | BR15, BR35, Mục 5.1 |
| **US07** | Kiểm tra CV tổng quát không cần JD | Thực hiện | — | — | — | — | Trừ 1 lượt cơ bản | BR12, BR24, BR25 |
| **US08** | Phân tích CV–JD theo Rubric 4 Trụ cột | Thực hiện | — | — | — | — | Giữ & thu credit | BR01, BR04, BR13 |
| **US09** | Hiển thị điểm số & cảnh báo bắt buộc | Xem kết quả | — | — | — | — | Chuẩn hóa điểm | BR05, BR18, Mục 5.2 |
| **US10** | Đề xuất viết lại gạch đầu dòng trung thực | Xem đề xuất | — | — | — | — | AI tạo bản viết | BR06, BR14 |
| **US11** | So sánh Diff view & chỉnh sửa thủ công | Thực hiện | — | — | — | — | Lưu bản sửa | BR14 |
| **US12** | Xuất file CV tối ưu (PDF/DOCX) | Thực hiện | — | — | — | — | Sinh file xuất | BR14 |
| **US13** | Mua gói credit cố định & quản lý giá gói | Thực hiện | — | — | Quản lý gói VND | — | Tạo phiên cổng | BR11, BR28 |
| **US14** | Giữ, thu trọn bộ & hoàn trả timeout 90s | Hưởng lợi | — | — | — | — | Tự động quản lý | BR04, BR13, BR17 |
| **US15** | Đối soát đơn muộn & duyệt cộng credit | Hưởng lợi | — | — | Đối soát & duyệt | — | Chuyển trạng thái | BR11, OQ05, OQ07 |
| **US16** | Lịch sử phân tích & tải lại (90 ngày) | Thực hiện | — | — | — | — | Xóa khi hết hạn | BR09, BR21, BR26 |
| **US17** | Nhận diện nộp trùng (Deduplication) | Hưởng lợi | — | — | — | — | Trả cache có sẵn | BR36 |
| **US18** | Cấp & thu hồi quyền hỗ trợ xem CV (72h) | Cấp/thu hồi | Tiếp nhận xem | Giám sát vụ việc | — | — | Tự động hủy sau 72h | BR07, BR22 |
| **US19** | Khiếu nại lỗi kỹ thuật & bù 1 credit | Yêu cầu | Xác minh/đề nghị | Phê duyệt bù | Hạch toán credit | — | Đánh dấu báo cáo lỗi | BR27, BR37 |
| **US20** | Quản trị tài khoản nội bộ & phân vai | — | — | — | — | Thực hiện | Ghi audit log | BR07, BR30 |
---

## 3. Story Map theo Hành trình Người dùng

```text
[HÀNH TRÌNH ỨNG VIÊN]
Khám phá & Đăng ký  ──►  Tải & Xác nhận CV  ──►  Cấu hình & Chạy  ──►  Xem & Tối ưu hóa  ──►  Lưu trữ & Hỗ trợ
       │                         │                       │                     │                      │
       ├─ US01: Verify Email     ├─ US04: Upload CV      ├─ US07: Check no JD  ├─ US09: Xem điểm      ├─ US16: Xem lịch sử
       ├─ US02: Consent AI       ├─ US05: Sửa trích xuất ├─ US08: Phân tích JD ├─ US10: Viết lại      ├─ US18: Cấp quyền xem
       └─ US03: Đóng Account     └─ US06: Validate JD    └─ US14: Giữ credit   ├─ US11: Diff view     └─ US19: Báo lỗi bù credit
                                                                │              └─ US12: Xuất PDF/DOCX
                                                         US13: Mua Credit

[HÀNH TRÌNH VẬN HÀNH 4 VAI TRÒ NỘI BỘ]
1. Nhóm Chăm sóc Khách hàng:
   - US18: Hỗ trợ viên xem CV khi khách cấp quyền (tối đa 72h)
   - US19: Hỗ trợ viên kiểm tra lỗi trích xuất/AI & lập phiếu đề nghị bồi thường
   - US19: Lead hỗ trợ phân công vụ việc, giám sát SLA 2 ngày & duyệt phiếu bồi thường 1 credit
2. Nhóm Tài chính & Kế toán:
   - US13: Kế toán quản lý cấu hình danh mục gói credit VND (giá bán, số credit, trạng thái)
   - US15: Kế toán đối soát đơn hàng Chờ đối soát với cổng thanh toán & duyệt cộng credit (2 ngày)
3. Quản trị viên Hệ thống:
   - US20: Admin quản lý tài khoản nội bộ, phân quyền vai trò (Hỗ trợ, Lead, Kế toán, Admin)
   - US20: Admin giám sát nhật ký bảo mật (audit logs), cấu hình hệ thống; Zero-Trust không đọc CV
```

---

## 4. Đặc tả Chi tiết từng User Story

### US01: Đăng ký tài khoản, xác minh email và nhận 3 lượt cơ bản
- **Mô tả:** Là Ứng viên mới, tôi muốn đăng ký tài khoản bằng email và nhận link xác minh để kích hoạt tài khoản và nhận ngay 3 lượt phân tích cơ bản miễn phí.
- **Tiền điều kiện:** Email chưa từng được kích hoạt tài khoản trên hệ thống.
- **Hậu điều kiện:** Tài khoản ở trạng thái đã kích hoạt; số dư có 3 lượt cơ bản miễn phí; ghi nhận hash email vào sổ phòng chống lạm dụng.
- **Acceptance Criteria (GWT):**
  - `AC01.1 (Happy Path):` Given người dùng nhập email hợp lệ chưa tồn tại, When bấm Đăng ký, Then hệ thống gửi email chứa liên kết xác thực có hạn 24 giờ.
  - `AC01.2 (Email Verification):` Given người dùng bấm vào link xác minh còn hạn, When hệ thống xác thực thành công, Then kích hoạt tài khoản, cộng đúng 3 lượt cơ bản miễn phí, và chuyển hướng đến trang tải CV.
  - `AC01.3 (Anti-Abuse Check):` Given email đăng ký trùng với hash token của tài khoản đã xóa trong vòng 12 tháng qua (BR34), When xác minh email thành công, Then kích hoạt tài khoản nhưng số dư lượt cơ bản là 0, hiển thị thông báo email này đã từng nhận ưu đãi dùng thử.
  - `AC01.4 (Duplicate Email):` Given email đã tồn tại ở tài khoản đang hoạt động, When bấm Đăng ký, Then hiển thị thông báo email đã được sử dụng kèm liên kết Đăng nhập / Quên mật khẩu.

### US02: Đồng ý xử lý AI bên ngoài và chính sách bảo mật dữ liệu
- **Mô tả:** Là Ứng viên, tôi muốn xem rõ thông báo minh bạch về việc gửi dữ liệu đến API AI bên ngoài và chủ động bấm đồng ý để hệ thống tiến hành phân tích.
- **Tiền điều kiện:** Đã đăng nhập và đã xác nhận nội dung trích xuất CV.
- **Hậu điều kiện:** Ghi nhận sự đồng ý (consent) của người dùng; hệ thống áp dụng cơ chế giảm thiểu PII trước khi gửi API AI.
- **Acceptance Criteria (GWT):**
  - `AC02.1 (Consent Modal Display):` Given người dùng chuẩn bị chạy phân tích lần đầu (hoặc khi chính sách AI có cập nhật), When bấm bắt đầu phân tích, Then hiển thị hộp thoại nêu rõ: (1) Nhà cung cấp AI không huấn luyện mô hình trên dữ liệu người dùng, (2) Dữ liệu được che bớt thông tin định danh cá nhân, (3) Nhà cung cấp AI lưu log giám sát lạm dụng tối đa 30 ngày.
  - `AC02.2 (Consent Accepted):` Given hộp thoại đồng ý hiển thị, When người dùng bấm "Tôi đồng ý", Then hệ thống lưu dấu thời gian đồng ý và tiếp tục luồng gửi dữ liệu sang AI.
  - `AC02.3 (Consent Declined):` Given hộp thoại hiển thị, When người dùng bấm "Từ chối", Then hệ thống dừng tác vụ phân tích, giữ nguyên nội dung trích xuất trong tài khoản, tuyệt đối không gửi dữ liệu ra bên ngoài và không trừ lượt/credit (BR29).

### US03: Yêu cầu đóng tài khoản và xóa dữ liệu cá nhân
- **Mô tả:** Là Ứng viên, tôi muốn yêu cầu đóng tài khoản để xóa toàn bộ CV, báo cáo và dữ liệu cá nhân khỏi hệ thống hoạt động.
- **Tiền điều kiện:** Đang đăng nhập tài khoản của mình.
- **Hậu điều kiện:** Dữ liệu cá nhân, file CV, báo cáo bị xóa khỏi cơ sở dữ liệu hoạt động; số dư credit còn lại bị hủy; lưu hash email trong 12 tháng.
- **Acceptance Criteria (GWT):**
  - `AC03.1 (Warning & Re-auth):` Given người dùng vào mục Cài đặt tài khoản và chọn "Đóng tài khoản", When hệ thống hiển thị cảnh báo, Then người dùng phải nhập mật khẩu (hoặc mã OTP xác thực) và tích chọn ô "Tôi hiểu rằng toàn bộ CV, báo cáo và credit còn lại sẽ bị xóa vĩnh viễn và không được hoàn tiền".
  - `AC03.2 (Pending Transaction Check):` Given tài khoản có đơn hàng đang ở trạng thái `Chờ đối soát`, When xác nhận đóng tài khoản, Then hệ thống thông báo yêu cầu chờ xử lý đối soát xong hoặc cảnh báo nếu đóng tài khoản thì việc tra soát sẽ chuyển sang kênh hỗ trợ ngoại tuyến.
  - `AC03.3 (Immediate Purge):` Given xác thực hợp lệ và không có blocker, When bấm xác nhận xóa, Then hệ thống xóa ngay lập tức tài khoản, file CV, trích xuất và báo cáo khỏi database hoạt động; hủy các tác vụ AI đang chạy nếu có (BR10, BR30).

### US04: Tải lên file CV (PDF / DOCX)
- **Mô tả:** Là Ứng viên, tôi muốn tải file CV định dạng PDF hoặc Word (.docx) lên hệ thống để trích xuất văn bản.
- **Acceptance Criteria (GWT):**
  - `AC04.1 (Valid File Upload):` Given file có đuôi `.pdf` hoặc `.docx`, dung lượng $\le 10$MB, số trang $\le 5$, không đặt mật khẩu, When tải lên thành công, Then hệ thống hiển thị trạng thái "Đang trích xuất nội dung" kèm thanh tiến trình.
  - `AC04.2 (Size & Page Boundary):` Given file có dung lượng $> 10$MB hoặc độ dài $> 5$ trang, When chọn file, Then chặn ngay tại giao diện và báo lỗi: "File vượt quá giới hạn cho phép (tối đa 10MB và 5 trang)".
  - `AC04.3 (Password Protected):` Given file PDF/DOCX có mật khẩu bảo vệ, When tải lên, Then từ chối file ngay lập tức và thông báo: "Hệ thống không hỗ trợ file có mật khẩu bảo vệ. Vui lòng gỡ mật khẩu trước khi tải lên" (OQ06).
  - `AC04.4 (Unparseable / Image-only PDF):` Given file PDF thuần hình ảnh scan không chứa văn bản ký tự, When trích xuất thất bại, Then dừng quy trình, không trừ lượt/credit, hiển thị hướng dẫn người dùng xuất file có lớp văn bản (BR03).

### US05: Xem trước, chỉnh sửa và xác nhận nội dung trích xuất
- **Mô tả:** Là Ứng viên, tôi muốn xem trước các mục thông tin đã trích xuất từ CV, trực tiếp sửa lại các chỗ bị lỗi font hoặc thiếu sót, và bấm xác nhận làm căn cứ phân tích.
- **Acceptance Criteria (GWT):**
  - `AC05.1 (Extraction Display):` Given trích xuất hoàn tất, When chuyển sang màn hình xác nhận, Then hiển thị form chia các mục chuẩn: Thông tin liên hệ, Học vấn, Kinh nghiệm làm việc, Kỹ năng, Chứng chỉ.
  - `AC05.2 (Inline Edit):` Given người dùng phát hiện trích xuất sai tên công nghệ hoặc ngày tháng, When chỉnh sửa trực tiếp trên ô nhập liệu, Then hệ thống lưu tạm các thay đổi của người dùng.
  - `AC05.3 (Confirm Baseline):` Given người dùng kiểm tra xong, When bấm "Xác nhận nội dung này", Then hệ thống lưu bản ghi `Nội dung đã xác nhận` gắn với phiên bản CV này để làm căn cứ duy nhất cho toàn bộ bước chấm điểm và viết lại sau đó (BR09, BR23).

### US06: Nhập mô tả công việc (JD) và kiểm tra căn cứ
- **Mô tả:** Là Ứng viên, tôi muốn dán nội dung JD vào hệ thống để đối chiếu mức độ phù hợp với vị trí công việc.
- **Acceptance Criteria (GWT):**
  - `AC06.1 (Valid JD):` Given JD có độ dài từ 50 đến 2.500 từ (hoặc $\ge 300$ ký tự), có chức danh và chứa $\ge 3$ kỹ năng hoặc $\ge 2$ trách nhiệm, When bấm tiếp tục, Then hệ thống xác nhận JD đủ căn cứ và cho phép chọn chế độ phân tích đối chiếu.
  - `AC06.2 (Insufficient JD Warning):` Given JD dưới 50 từ hoặc thiếu chức danh / kỹ năng, When bấm tiếp tục, Then hiển thị cảnh báo: "Mô tả công việc chưa đủ căn cứ để chấm điểm đối chiếu" kèm 2 nút lựa chọn: (1) Bổ sung thêm JD, hoặc (2) Chuyển sang kiểm tra CV tổng quát (BR35).
  - `AC06.3 (JD Exceeds 2500 words):` Given JD vượt quá 2.500 từ, When dán vào khung nhập liệu, Then hệ thống cảnh báo và yêu cầu cắt tỉa bớt các thông tin ngoài lề (chế độ đãi ngộ, giới thiệu công ty) để tập trung vào yêu cầu chuyên môn.

### US07: Phân tích CV tổng quát khi không có JD
- **Mô tả:** Là Ứng viên, tôi muốn dùng 1 lượt cơ bản để kiểm tra CV tổng quát mà không cần nhập JD.
- **Acceptance Criteria (GWT):**
  - `AC07.1 (No-JD Execution):` Given ứng viên chọn chế độ "Kiểm tra CV tổng quát", When bấm bắt đầu, Then hệ thống trừ 1 lượt cơ bản (hoặc 1 credit nếu đã hết lượt cơ bản và người dùng xác nhận).
  - `AC07.2 (General Report Output):` Given phân tích tổng quát hoàn tất, When hiển thị báo cáo, Then báo cáo tập trung vào: lỗi chính tả, câu văn dài dòng, động từ hành động yếu, lỗi định dạng cột/bảng và thông tin liên hệ thiếu sót. Tuyệt đối không hiển thị điểm phù hợp JD hoặc gộp thành điểm ATS 0–100 (BR12, BR24).

### US08: Phân tích đối chiếu CV với JD theo Rubric 4 Trụ cột
- **Mô tả:** Là Ứng viên, tôi muốn hệ thống phân tích đối chiếu CV với JD theo Rubric chuẩn để nhận đánh giá khách quan về điểm mạnh và khoảng trống kỹ năng.
- **Acceptance Criteria (GWT):**
  - `AC08.1 (Deduction Trigger & Hold):` Given ứng viên có đủ lượt/credit và xác nhận chạy, When hệ thống nhận yêu cầu, Then lập tức đưa 1 lượt/credit vào trạng thái `Đang giữ` (Hold) và chuyển tác vụ vào hàng đợi xử lý (BR04).
  - `AC08.2 (Weight Normalization when no Education):` Given JD không yêu cầu bằng cấp/học vấn, When hệ thống tính điểm tổng hợp $S$, Then tiêu chí học vấn chuyển `NOT_APPLICABLE`, mẫu số chuẩn hóa về 85, trọng số thực tế tự động co giãn theo công thức BR05, không tính điểm 0.
  - `AC08.3 (Skill Distribution):` Given JD không phân nhóm bắt buộc vs ưu tiên, When chấm điểm kỹ năng, Then 100% điểm kỹ năng được chia đều cho tất cả các kỹ năng tìm thấy.
  - `AC08.4 (Stuffing Penalty Enforcement):` Given CV có từ khóa lặp lại mật độ $> 3.5\%$ hoặc chuỗi danh từ kỹ thuật liên tiếp $> 10$ từ, When tính điểm, Then hủy điểm lần thừa và trừ 25% điểm kỹ năng, điểm trụ cột kỹ năng không bị âm (sàn 0 điểm).

### US09: Hiển thị điểm số chi tiết, trọng số chuẩn hóa và cảnh báo bắt buộc
- **Mô tả:** Là Ứng viên, tôi muốn xem bảng điểm trực quan gồm điểm tổng hợp, điểm từng trụ cột và các cảnh báo yêu cầu bắt buộc chưa có bằng chứng.
- **Acceptance Criteria (GWT):**
  - `AC09.1 (Score Display):` Given báo cáo hoàn tất, When người dùng mở xem, Then hiển thị điểm tổng hợp $S$ làm tròn số nguyên (Round Half Up) kèm phân tầng nhãn: Xuất sắc (85–100), Khá (70–84), Trung bình (50–69), Kém (0–49).
  - `AC09.2 (Breakdown Accordion):` Given người dùng xem bảng điểm, When bấm vào từng trụ cột (Kỹ năng, Kinh nghiệm, Định dạng, Học vấn), Then hiển thị rõ trọng số chuẩn hóa đang áp dụng, bằng chứng trích xuất từ CV, và lý do bị trừ điểm.
  - `AC09.3 (Mandatory Requirement Warning):` Given CV thiếu một kỹ năng hoặc chứng chỉ bắt buộc của JD, When hiển thị báo cáo, Then hiển thị cảnh báo nổi bật ở khối độc lập: "Yêu cầu bắt buộc chưa tìm thấy bằng chứng trong CV" cạnh điểm số, không bị điểm cao của mục khác che lấp (BR18).

### US10: Đề xuất viết lại gạch đầu dòng trung thực (Gói nâng cao)
- **Mô tả:** Là Ứng viên dùng gói nâng cao, tôi muốn nhận bản đề xuất viết lại các gạch đầu dòng kinh nghiệm bám sát dữ kiện thật để tăng sức thuyết phục.
- **Acceptance Criteria (GWT):**
  - `AC10.1 (Factual Grounding):` Given AI sinh bản viết lại, When tạo từng gạch đầu dòng, Then nội dung chỉ được viết lại dựa trên các kỹ năng, công nghệ và dự án có trong `Nội dung đã xác nhận` của ứng viên; tuyệt đối không tự bịa thêm số liệu %, doanh số hay công nghệ mới (BR06).
  - `AC10.2 (Action-Context-Metric Pattern):` Given gạch đầu dòng được tối ưu, When hiển thị, Then cấu trúc câu tuân thủ công thức: Động từ hành động mạnh + Ngữ cảnh nhiệm vụ cụ thể + Kết quả/số liệu đo lường (nếu CV gốc có dữ liệu).

### US11: So sánh trực quan (Diff view) và chỉnh sửa thủ công
- **Mô tả:** Là Ứng viên, tôi muốn xem màn hình so sánh song song giữa nội dung gốc và đề xuất mới, có thể sửa tay hoặc từ chối đề xuất.
- **Acceptance Criteria (GWT):**
  - `AC11.1 (Side-by-Side Diff):` Given bản viết lại đã sẵn sàng, When người dùng vào mục tối ưu, Then hiển thị 2 cột: Cột trái là nội dung gốc, Cột phải là đề xuất viết lại kèm đánh dấu từ ngữ được thêm/thay thế (highlight diff).
  - `AC11.2 (Accept/Reject Suggestions):` Given mỗi gạch đầu dòng đề xuất, When người dùng bấm nút "Từ chối", Then gạch đầu dòng đó quay về nội dung gốc ban đầu.
  - `AC11.3 (Manual Customization):` Given người dùng muốn chỉnh sửa câu từ, When gõ trực tiếp vào ô đề xuất, Then hệ thống lưu bản chỉnh sửa của người dùng mà không tính thêm phí (BR13, BR14).

### US12: Xuất bản CV tối ưu ra file PDF và DOCX
- **Mô tả:** Là Ứng viên, tôi muốn tải file CV đã tối ưu về máy dưới dạng PDF hoặc Word (.docx) theo mẫu văn bản chuẩn ATS.
- **Acceptance Criteria (GWT):**
  - `AC12.1 (Export Buttons Ready):` Given người dùng đã duyệt bản tối ưu, When bấm nút "Xuất PDF" hoặc "Xuất DOCX", Then hệ thống tạo file tải về ngay lập tức.
  - `AC12.2 (Text Layer & Formatting):` Given file PDF/DOCX được tạo, When mở file, Then văn bản phải bôi đen/copy được, bố cục 1 cột sạch sẽ, tiêu đề phân mục chuẩn convention, không chứa text box trôi nổi hay bảng ẩn phức tạp.
  - `AC12.3 (Free Re-download):` Given người dùng đã hoàn tất lượt nâng cao, When tải lại file nhiều lần sau đó (trong hạn 90 ngày), Then hệ thống cho tải miễn phí, không trừ thêm credit.

### US13: Mua gói credit cố định qua phiên thanh toán
- **Mô tả:** Là Ứng viên, tôi muốn mua các gói credit VND cố định để sử dụng các tính năng nâng cao.
- **Acceptance Criteria (GWT):**
  - `AC13.1 (Fixed Package Selection):` Given người dùng vào trang nạp credit, When chọn một gói credit (ví dụ: Gói 5 credit hoặc Gói 10 credit), Then hệ thống tạo một đơn hàng có mã đơn duy nhất, cố định số tiền VND và số credit.
  - `AC13.2 (15-minute Checkout Session):` Given đơn hàng được tạo, When chuyển sang trang thanh toán của cổng, Then đơn hàng có đồng hồ đếm ngược 15 phút.
  - `AC13.3 (Webhook Credit Addition):` Given người dùng thanh toán thành công trong 15 phút, When hệ thống nhận Webhook hợp lệ có chữ ký điện tử từ cổng thanh toán, Then lập tức cộng đúng số credit của gói vào tài khoản và chuyển trạng thái đơn sang `Thành công` (BR11, BR28).

### US14: Giữ, thu trọn bộ và hoàn trả credit khi lỗi / timeout 90s
- **Mô tả:** Là Hệ thống, tôi muốn tự động quản lý trạng thái credit của lượt phân tích để đảm bảo quyền lợi tài chính minh bạch cho người dùng.
- **Acceptance Criteria (GWT):**
  - `AC14.1 (Hold on Start):` Given người dùng bấm chạy phân tích nâng cao, When hệ thống tiếp nhận, Then chuyển 1 credit từ `Khả dụng` sang `Đang giữ` (Hold).
  - `AC14.2 (Deduction on Full Bundle):` Given hệ thống hoàn thành đầy đủ cả 3 thành phần: Báo cáo chi tiết + Bản viết lại + Sẵn sàng nút xuất file, When bộ kết quả được lưu, Then chuyển 1 credit từ `Đang giữ` sang `Đã thu` (BR13, OQ03).
  - `AC14.3 (Timeout 90s Rollback):` Given tác vụ đang chạy hoặc đang chờ, When tổng thời gian từ lúc nhận lượt vượt quá 90 giây, Then hệ thống kích hoạt Circuit Breaker hủy tác vụ, chuyển ngay 1 credit từ `Đang giữ` về lại `Khả dụng` và thông báo lỗi quá thời gian xử lý (BR04, OQ02).
  - `AC14.4 (Partial Failure 24h Retention):` Given hệ thống sinh xong báo cáo nhưng lỗi ở khâu viết lại, When kết thúc lượt, Then giải phóng credit giữ về ví khả dụng, thông báo lỗi một phần và lưu giữ phần báo cáo đã xong trong tối đa 24 giờ. Khi người dùng bấm "Tiếp tục hoàn thiện" trong 24 giờ, hệ thống mới giữ lại 1 credit để tiếp tục (BR17).

### US15: Đối soát thanh toán muộn/báo trễ và cộng credit thủ công (Kế toán)
- **Mô tả:** Là Chuyên viên Tài chính / Kế toán, tôi muốn đối soát các giao dịch thanh toán muộn sau 15 phút để cộng đúng số credit gói mua cho khách hàng trong tối đa 2 ngày làm việc.
- **Acceptance Criteria (GWT):**
  - `AC15.1 (Late Payment to Reconciliation):` Given khách chuyển tiền sau khi đơn 15 phút đã hết hạn (hoặc webhook cổng thanh toán bị nghẽn gửi trễ), When hệ thống nhận được tín hiệu tiền vào cổng, Then không tự động hủy mà chuyển đơn sang trạng thái `Chờ đối soát` (BR11, OQ05).
  - `AC15.2 (Finance Verification):` Given đơn hàng `Chờ đối soát`, When Chuyên viên Kế toán kiểm tra thấy mã tham chiếu ngân hàng khớp với tiền thực nhận trên cổng thanh toán, Then thực hiện đối soát và duyệt cộng đúng số credit của gói đơn hàng vào tài khoản ứng viên.
  - `AC15.3 (SLA 2 Business Days):` Given đơn hàng Chờ đối soát, When Kế toán xử lý và duyệt thành công, Then hệ thống cộng credit, gửi email thông báo xác nhận trong tối đa 2 ngày làm việc; nếu tài khoản đã xóa thì chuyển sang xử lý hoàn tất ngoại tuyến.

### US16: Tra cứu lịch sử phân tích và tải lại báo cáo (90 ngày)
- **Mô tả:** Là Ứng viên, tôi muốn xem lại danh sách các lần phân tích trước đây và tải lại báo cáo trong hạn 90 ngày.
- **Acceptance Criteria (GWT):**
  - `AC16.1 (History List View):` Given người dùng đã đăng nhập, When vào mục Lịch sử phân tích, Then hiển thị danh sách các lần phân tích gồm: Tên file CV, Vị trí JD (nếu có), Điểm tổng hợp, Ngày phân tích và Thời hạn còn lại.
  - `AC16.2 (Open Existing Report):` Given báo cáo còn trong hạn 90 ngày, When bấm xem lại, Then mở ngay báo cáo đầy đủ kèm bản viết lại mà không trừ thêm lượt/credit (BR21, BR26).
  - `AC16.3 (Expiration Notice & Purge):` Given báo cáo còn 7 ngày nữa là hết hạn 90 ngày, When hệ thống quét lịch trình, Then gửi email nhắc nhở người dùng vào tải file về máy; khi tròn 90 ngày, hệ thống xóa báo cáo và file CV liên quan khỏi dữ liệu hoạt động.

### US17: Nhận diện nộp trùng trả kết quả có sẵn (Deduplication Cache)
- **Mô tả:** Là Hệ thống, tôi muốn tự động nhận diện yêu cầu phân tích nộp lại nội dung y hệt khi báo cáo cũ còn hạn để trả ngay kết quả có sẵn mà không trừ thêm lượt.
- **Acceptance Criteria (GWT):**
  - `AC17.1 (Cache Hit Detection):` Given người dùng tải lên cùng nội dung CV, cùng JD, cùng loại phân tích và cùng ngôn ngữ với một lần phân tích trước đó, When báo cáo cũ còn hiệu lực (chưa bị xóa và chưa hết hạn), Then hệ thống hiển thị thông báo: "Đã tìm thấy báo cáo tương ứng còn hiệu lực" và mở lại báo cáo đó.
  - `AC17.2 (Zero Cost on Cache Hit):` Given tình huống nhận diện kết quả trùng, When mở báo cáo, Then số dư lượt cơ bản và credit của người dùng giữ nguyên không bị trừ (BR36).

### US18: Cấp và thu hồi quyền hỗ trợ xem CV theo vụ việc (Tối đa 72h)
- **Mô tả:** Là Ứng viên, tôi muốn chủ động cấp quyền xem nội dung CV/báo cáo cho nhân viên hỗ trợ khi có thắc mắc kỹ thuật và có thể thu hồi bất kỳ lúc nào.
- **Acceptance Criteria (GWT):**
  - `AC18.1 (Explicit Grant Modal):` Given ứng viên tạo yêu cầu hỗ trợ về một báo cáo phân tích, When gửi yêu cầu, Then hệ thống hiển thị nút tích chọn: "Tôi đồng ý cấp quyền cho nhân viên hỗ trợ xem nội dung CV và báo cáo này trong tối đa 72 giờ để xử lý sự cố" (BR07, BR22).
  - `AC18.2 (Scoped & Time-limited Access):` Given quyền hỗ trợ được cấp, When nhân viên được phân công mở vụ việc, Then nhân viên đó chỉ được xem đúng CV và báo cáo của vụ việc đó; hệ thống ghi log truy cập chi tiết.
  - `AC18.3 (Auto Revocation / Manual Revoke):` Given vụ việc được đóng (hoặc sau 72 giờ, mốc nào đến trước), When nhân viên truy cập lại, Then quyền xem bị chặn; ứng viên có thể bấm "Thu hồi quyền truy cập" ngay lập tức bất kỳ lúc nào trên màn hình chi tiết vụ việc.

### US19: Tiếp nhận khiếu nại báo cáo lỗi và duyệt bồi thường credit (Hỗ trợ & Lead)
- **Mô tả:** Là Nhân viên hỗ trợ và Trưởng nhóm hỗ trợ, tôi muốn tiếp nhận khiếu nại báo cáo lỗi, xác minh nguyên nhân kỹ thuật và duyệt bồi thường đúng 1 lượt/credit cho ứng viên.
- **Acceptance Criteria (GWT):**
  - `AC19.1 (Complaint Submission):` Given báo cáo còn trong hạn 90 ngày, When ứng viên bấm "Báo cáo kết quả sai lệch", Then form tiếp nhận yêu cầu mô tả rõ phần bị lỗi và tự động kích hoạt cấp quyền xem hỗ trợ theo US18.
  - `AC19.2 (Staff Verification & Recommendation):` Given nhân viên hỗ trợ kiểm tra và xác nhận có lỗi hệ thống (ví dụ: lỗi trích xuất gãy ký tự làm điểm kỹ năng bị sai lệch nghiêm trọng), When xác minh hoàn tất, Then lập phiếu đề nghị: "Bồi thường 1 credit / 1 lượt cơ bản" kèm lý do kỹ thuật.
  - `AC19.3 (Lead Approval & Report Flagging):` Given phiếu đề nghị bồi thường, When Trưởng nhóm hỗ trợ (Lead Support) phê duyệt, Then hệ thống cộng đúng 1 credit (hoặc 1 lượt cơ bản) vào tài khoản ứng viên, đồng thời đánh dấu báo cáo đó là `Lỗi đã xác minh` để cách ly khỏi cache nộp lại (BR37).

### US20: Quản trị tài khoản nội bộ, phân quyền vai trò và nhật ký bảo mật (Admin)
- **Mô tả:** Là Quản trị viên hệ thống, tôi muốn quản trị danh sách người dùng nội bộ, gán vai trò và giám sát nhật ký bảo mật để đảm bảo an toàn vận hành.
- **Acceptance Criteria (GWT):**
  - `AC20.1 (Role Assignment):` Given Admin đăng nhập trang quản trị, When tạo mới hoặc sửa người dùng nội bộ, Then có thể gán đúng 1 trong các vai trò: Nhân viên hỗ trợ, Trưởng nhóm hỗ trợ, Kế toán, Quản trị viên.
  - `AC20.2 (Zero-Trust Enforcement):` Given tài khoản nội bộ đăng nhập, When cố gắng truy cập nội dung CV của ứng viên mà không có mã vụ việc được cấp quyền hợp lệ, Then hệ thống chặn truy cập và ghi nhận cảnh báo vi phạm quyền riêng tư.
  - `AC20.3 (Audit Log Tracking):` Given bất kỳ thao tác nào liên quan đến: Duyệt cộng credit, Duyệt bồi thường, Phân vai tài khoản, Xem CV được cấp quyền, When thao tác thực hiện, Then hệ thống ghi nhận bản ghi nhật ký bất biến gồm: Actor ID, Hành động, Thời điểm, IP, và Chi tiết dữ liệu.

---

## 5. Danh mục Luồng Thao tác UX (Flow Catalogue)

### FL01: Luồng Đăng ký, Xác thực và Nhận ưu đãi ban đầu
1. Khách truy cập bấm "Bắt đầu miễn phí" trên trang chủ.
2. Nhập email và mật khẩu $\rightarrow$ Bấm "Đăng ký".
3. Màn hình thông báo: "Vui lòng kiểm tra hộp thư để kích hoạt tài khoản".
4. Khách bấm link trong email $\rightarrow$ Mở tab mới xác thực thành công.
5. Hệ thống hiển thị Modal chúc mừng: "Bạn đã nhận được 3 lượt phân tích cơ bản miễn phí!" $\rightarrow$ Chuyển thẳng vào màn hình Tải CV.

### FL02: Luồng Tải CV, Trích xuất và Xác nhận Nội dung
1. Ứng viên kéo-thả file PDF/DOCX vào vùng tải lên.
2. Hệ thống kiểm tra client-side (dung lượng $\le 10$MB, đuôi file hợp lệ). Nếu vi phạm $\rightarrow$ Báo lỗi inline ngay lập tức.
3. Upload file lên server $\rightarrow$ Kiểm tra số trang ($\le 5$) và kiểm tra mật khẩu. Nếu có mật khẩu $\rightarrow$ Báo lỗi và dừng.
4. Hệ thống trích xuất văn bản $\rightarrow$ Hiển thị màn hình xem trước / chỉnh sửa thông tin trích xuất (Form chia 5 khối: Liên hệ, Học vấn, Kinh nghiệm, Kỹ năng, Chứng chỉ).
5. Ứng viên xem, chỉnh sửa lỗi chính tả nếu có $\rightarrow$ Bấm "Xác nhận nội dung".

### FL03: Luồng Nhập JD và Chạy Phân tích (Cơ bản / Nâng cao)
1. Ứng viên chọn chế độ: "Đối chiếu JD" hoặc "Kiểm tra CV tổng quát".
2. Nếu chọn "Đối chiếu JD": Dán nội dung JD vào khung nhập liệu.
3. Hệ thống đếm từ và kiểm tra điều kiện căn cứ:
   - Nếu $< 50$ từ $\rightarrow$ Hiển thị cảnh báo inline màu vàng kèm gợi ý bổ sung hoặc chuyển sang chế độ tổng quát.
   - Nếu $\ge 50$ từ $\rightarrow$ Hiện nút xanh "Sẵn sàng phân tích".
4. Ứng viên bấm "Bắt đầu phân tích" $\rightarrow$ Hiển thị Modal thông báo quyền riêng tư AI (nếu chưa từng đồng ý).
5. Ứng viên bấm "Tôi đồng ý" $\rightarrow$ Hệ thống giữ 1 lượt/credit, hiển thị màn hình chờ (Loading skeleton) với thông điệp: "Đang phân tích đối chiếu... Quá trình này thường mất 15–45 giây".
6. Nếu quá 90 giây $\rightarrow$ Tự động ngắt, trả lại credit, báo lỗi: "Hệ thống đang quá tải, vui lòng thử lại sau".
7. Nếu thành công $\rightarrow$ Chuyển sang màn hình Báo cáo kết quả.

### FL04: Luồng Tối ưu hóa Bản viết lại và Xuất File (Gói nâng cao)
1. Từ màn hình báo cáo, ứng viên bấm tab "Bản viết lại tối ưu".
2. Giao diện hiển thị Diff view so sánh song song giữa câu văn cũ và câu văn mới được tối ưu.
3. Ứng viên xem từng gạch đầu dòng:
   - Bấm "Chấp nhận" hoặc "Từ chối".
   - Bấm trực tiếp vào văn bản để sửa tay câu từ theo ý muốn.
4. Bấm "Lưu bản hoàn chỉnh".
5. Ứng viên bấm "Xuất PDF" hoặc "Xuất DOCX" $\rightarrow$ Trình duyệt tải file về máy.

### FL05: Luồng Mua Gói Credit Cố định
1. Khi ứng viên hết lượt cơ bản hoặc muốn dùng tính năng nâng cao $\rightarrow$ Bấm "Nạp credit".
2. Màn hình bảng giá hiển thị các gói cố định: Gói 5 credit (100.000đ), Gói 15 credit (250.000đ)...
3. Chọn gói $\rightarrow$ Bấm "Thanh toán ngay".
4. Mở phiên thanh toán cổng (QR Pay / Thẻ ATM) kèm đồng hồ đếm ngược 15 phút.
5. Khi thanh toán thành công $\rightarrow$ Webhook cổng cập nhật $\rightarrow$ Màn hình hiển thị thông báo "Thanh toán thành công! Đã cộng X credit vào ví".
6. Nếu thanh toán sau 15 phút $\rightarrow$ Đơn chuyển sang `Chờ đối soát`. Chuyên viên Kế toán đối soát và duyệt cộng credit trong tối đa 2 ngày làm việc.

### FL06: Luồng Khiếu nại Lỗi và Bồi thường Credit
1. Ứng viên xem báo cáo phát hiện hệ thống chấm sai do trích xuất lỗi $\rightarrow$ Bấm "Khiếu nại kết quả".
2. Nhập nội dung mô tả lỗi $\rightarrow$ Tích chọn cấp quyền hỗ trợ 72 giờ $\rightarrow$ Bấm "Gửi khiếu nại".
3. Nhân viên hỗ trợ mở giao diện quản trị $\rightarrow$ Kiểm tra nội dung CV được cấp quyền $\rightarrow$ Xác nhận lỗi kỹ thuật $\rightarrow$ Bấm "Lập đề nghị bồi thường".
4. Trưởng nhóm hỗ trợ duyệt đề nghị $\rightarrow$ Hệ thống tự động cộng 1 credit/lượt cho khách hàng, gửi email xin lỗi và đánh dấu báo cáo là `Lỗi đã xác minh`.

---

## 6. Danh mục Màn hình và Ma trận Trạng thái UI

| Mã màn hình | Tên màn hình / View | Vai trò truy cập | Thao tác chính | Trạng thái Loading | Trạng thái Empty | Trạng thái Error |
|---|---|---|---|---|---|---|
| **SCR01** | Trang chủ & Landing Page | Khách, Ứng viên | Xem giới thiệu, Bấm đăng ký / dùng thử | Không áp dụng | Banner mặc định | Lỗi tải trang |
| **SCR02** | Đăng ký & Đăng nhập | Khách | Nhập form, Gửi link verify email | Spinner nút bấm | Không áp dụng | Báo lỗi validation đỏ |
| **SCR03** | Tải CV & Nhập JD | Ứng viên | Kéo thả file, Dán JD, Chọn chế độ | Tiến trình upload % | Vùng dropzone trống | Báo file quá 10MB/5 trang |
| **SCR04** | Xác nhận Trích xuất CV | Ứng viên | Sửa các trường text, Xác nhận | Skeleton trích xuất | Form chưa có chữ | Báo lỗi không đọc được |
| **SCR05** | Màn hình Chờ xử lý AI | Ứng viên | Xem tiến trình, Hủy khi còn chờ | Progress bar 0–90s | Không áp dụng | Báo timeout 90s + hoàn credit |
| **SCR06** | Báo cáo Chấm điểm ATS | Ứng viên | Xem điểm, Mở accordion, Xem cảnh báo | Skeleton bảng điểm | Không có dữ liệu | Báo lỗi phân tích hệ thống |
| **SCR07** | Tối ưu & Diff View | Ứng viên | Sửa gợi ý, Chấp nhận/từ chối, Xuất file | Spinner sinh bản viết | Chưa có bản viết lại | Báo lỗi sinh nội dung |
| **SCR08** | Mua Credit & Thanh toán | Ứng viên | Chọn gói giá, Quét mã QR thanh toán | Spinner tạo đơn | Bảng giá rỗng | Đơn hết hạn 15 phút |
| **SCR09** | Lịch sử Phân tích | Ứng viên | Xem danh sách cũ, Mở lại, Tải file | Skeleton danh sách | "Chưa có CV nào được phân tích" | Lỗi tải danh sách |
| **SCR10** | Cài đặt & Quyền dữ liệu | Ứng viên | Đóng tài khoản, Thu hồi quyền hỗ trợ | Spinner xử lý | Không áp dụng | Sai mật khẩu xác thực |
| **SCR11** | Giao diện Tiếp nhận Vụ việc Khiếu nại | Hỗ trợ viên | Xem CV được cấp quyền, lập đề nghị bù | Spinner tải danh sách | "Chưa có vụ việc nào được giao" | Lỗi kết nối server |
| **SCR12** | Giao diện Quản trị Vụ việc & Duyệt bồi thường | Lead hỗ trợ | Phân công vụ việc, duyệt bồi thường credit | Spinner duyệt | "Không có đề nghị bồi thường chờ duyệt" | Lỗi phê duyệt |
| **SCR13** | Giao diện Đối soát & Quản lý Bảng giá VND | Kế toán | Đối soát đơn muộn, duyệt cộng credit, sửa gói | Spinner cập nhật | "Không có đơn hàng cần đối soát" | Lỗi kết nối cổng |
| **SCR14** | Giao diện Phân quyền & Nhật ký Bảo mật | Quản trị viên | Gán vai trò nội bộ, xem audit logs hệ thống | Spinner tải logs | "Chưa có bản ghi nhật ký mới" | Lỗi phân quyền |
