# Đặc tả nghiệp vụ — Phân tích và tối ưu CV

| Thuộc tính | Giá trị |
|---|---|
| Artifact ID | BRIEF-CV-001 |
| Revision | Draft 6; ngày 2026-09-22; hoàn thiện mô hình phân quyền chuẩn 5 roles (Ứng viên, Hỗ trợ, Lead hỗ trợ, Kế toán, Admin) |
| Chế độ | Discovery / thiết kế nghiệp vụ, chưa triển khai |
| Quy mô | Hệ thống hoàn chỉnh vừa, theo lựa chọn của người dùng |
| Thị trường ban đầu | Việt Nam; gói credit bằng VND; hỗ trợ nội dung Việt–Anh |
| Trạng thái G1 | `approved`: Người dùng đã phê duyệt chính thức ngày 2026-09-22 trên cơ sở Báo cáo Thẩm định Độc lập AUDIT-G1-CV-002 (PASS); chính thức chuyển sang Cổng G2 (User Stories & UX) |
| Người xác nhận các quyết định | Người dùng trong hội thoại; chưa chỉ định chức danh/nhóm vận hành |
| Quyền lưu tài liệu | Người dùng chọn “Lưu bản đặc tả để rà soát” và “Đồng ý tích hợp Rubric vào Đặc tả” |
| Nguồn nghiệp vụ | Q01–Q39 và các xác nhận rubric/vận hành; bổ sung `system_roles_selection` (chọn bộ 5 roles chuẩn vận hành), `payment_collection_scope`, `operational_role_scope` |
| Nguồn nghiên cứu | [Chính sách dữ liệu CV/AI (ai-cv-privacy-retention.md)](../../research/ai-cv-privacy-retention.md), [Nghiên cứu Rubric & Case Studies (ats-scoring-rubrics-case-studies.md)](../../research/ats-scoring-rubrics-case-studies.md) |

Đây là hồ sơ tổng hợp để rà soát, không phải phê duyệt triển khai. Các quy tắc ghi **đã chốt** có câu trả lời của người dùng; danh mục Epic, tên trạng thái và cách tổ chức tài liệu là đề xuất của trợ lý. Câu hỏi mở không được ngầm chuyển thành yêu cầu đã duyệt.

## 1. Mục tiêu và giới hạn của sản phẩm

Hỗ trợ ứng viên đọc được vấn đề trong CV, đối chiếu CV với một JD cụ thể, cải thiện cách diễn đạt từ dữ kiện thật và xuất CV đã chỉnh sửa. Không bảo đảm vượt mọi ATS, được phỏng vấn hoặc được tuyển dụng.

Bộ trọng số 40/30/15/15 là thang đánh giá của sản phẩm do người dùng chọn, không phải chuẩn chung được mọi ATS sử dụng. Điểm số không phải xác suất vượt vòng tuyển dụng.

### Hiện trạng và mục tiêu

Các vấn đề hiện trạng dưới đây là giả thuyết bài toán, chưa có khảo sát người dùng thực địa xác nhận.

| Hiện trạng giả định | Mục tiêu sản phẩm đã định hướng | Kết quả quan sát được |
|---|---|---|
| Ứng viên không biết hệ thống trích xuất CV có đúng không | Cho kiểm tra và sửa nội dung trước phân tích | Nội dung dùng đánh giá là bản ứng viên xác nhận |
| Khó nhận ra điểm thiếu so với JD | Báo cáo điểm thành phần và dẫn chứng | Phân biệt yêu cầu chưa có bằng chứng với khẳng định ứng viên không đáp ứng |
| Viết lại CV có thể làm sai sự thật | AI chỉ dùng dữ kiện có sẵn/được bổ sung xác nhận | Không tự thêm kỹ năng, bằng cấp, số liệu hoặc thành tích |
| Khó quản lý kết quả và chi phí | Lịch sử theo phiên bản, credit theo bộ kết quả | Không thu trùng; báo cáo gắn đúng CV/JD đã dùng |
| CV chứa dữ liệu cá nhân | Giới hạn quyền truy cập và thời gian lưu | Có đồng ý xử lý AI, xóa dữ liệu, hết hạn và thu hồi quyền hỗ trợ |

## 2. Actors và danh mục nghiệp vụ

### Actors

- **Ứng viên:** chủ tài khoản, CV, báo cáo và số dư của mình.
- **Nhân viên hỗ trợ:** tiếp nhận vụ việc khiếu nại được giao, kiểm tra lỗi kỹ thuật và lập phiếu đề nghị bồi thường; chỉ xem CV khi ứng viên cấp quyền tối đa 72h theo BR22; không quản lý gói giá, không tự ý sửa số dư.
- **Trưởng nhóm hỗ trợ (Lead Support):** phân công vụ việc cho nhân viên hỗ trợ, giám sát SLA phản hồi (2 ngày), phê duyệt phiếu đề nghị bồi thường lượt/credit do lỗi kỹ thuật theo BR37; xem CV vẫn cần ứng viên cấp quyền theo vụ việc.
- **Chuyên viên Tài chính / Kế toán (Finance):** quản lý cấu hình danh mục gói credit và giá bán VND theo BR28; đối soát các giao dịch thanh toán muộn/báo trễ (Chờ đối soát) với cổng thanh toán trong 2 ngày làm việc và duyệt cộng credit đơn mua hợp lệ theo BR11; không can thiệp vụ việc kỹ thuật hay xem CV.
- **Quản trị viên hệ thống (Admin):** quản trị tài khoản và gán vai trò người dùng nội bộ, cấu hình tham số hệ thống (timeout, cổng kết nối, API AI), giám sát nhật ký bảo mật (audit logs); mặc định không có quyền xem CV của ứng viên.
- **Nhà cung cấp AI:** xử lý nội dung được gửi theo chính sách đã công bố; không có quyền quyết định trừ credit của ứng viên.
- **Cổng thanh toán:** nguồn xác nhận kết quả thanh toán; không lấy việc trình duyệt quay về trang thành công làm căn cứ duy nhất.

Chưa có quyết định triển khai cổng HR, xếp hạng hàng loạt ứng viên, multi-tenant hoặc tích hợp trực tiếp Workday/Greenhouse. Không coi các nội dung đó là phạm vi đã được yêu cầu.

### Danh mục Epic đề xuất, chưa duyệt riêng

| ID | Tên | Phạm vi từ các quyết định hiện có |
|---|---|---|
| EP01 | Quản lý tài khoản và quyền dữ liệu | Xác minh email, lượt trải nghiệm, đồng ý AI, đóng tài khoản |
| EP02 | Quản lý CV và mô tả tuyển dụng | Tải file, kiểm tra trích xuất, sửa/xác nhận nội dung, phiên bản |
| EP03 | Quản lý đánh giá hồ sơ | Phân tích tổng quát/đối chiếu JD, điểm số, bằng chứng, lỗi kết quả |
| EP04 | Quản lý tối ưu và xuất CV | Một bản viết lại, đối chiếu/sửa thủ công, PDF/DOCX |
| EP05 | Quản lý credit và thanh toán | Gói cố định, giữ/thu/trả credit, đối soát; không có chức năng hoàn tiền VND |
| EP06 | Quản lý lịch sử và hỗ trợ | Xem lại, nhận diện kết quả trùng, cấp quyền hỗ trợ, thời hạn lưu/xóa |

## 3. Thuật ngữ

| Thuật ngữ | Định nghĩa trong phạm vi này |
|---|---|
| ATS | Hệ thống theo dõi ứng viên; sản phẩm này không mô phỏng chính xác mọi ATS |
| JD | Nội dung mô tả tuyển dụng do người dùng cung cấp |
| Lượt cơ bản | Quyền phân tích cơ bản; tặng 3 lượt sau xác minh email theo điều kiện Q31/Q34 |
| Credit | Đơn vị mua bộ kết quả nâng cao; tách khỏi lượt cơ bản |
| Bộ kết quả nâng cao | Báo cáo chi tiết + một bản AI viết lại + khả năng xuất PDF/DOCX |
| Credit đang giữ | Credit dành cho một yêu cầu, chưa thu nhưng không được dùng đồng thời cho yêu cầu khác |
| Trả lượt/credit | Khôi phục quyền sử dụng trong ứng dụng; khác hoàn tiền về phương thức thanh toán |
| Nội dung đã xác nhận | Bản trích xuất được ứng viên kiểm tra/sửa, dùng làm căn cứ phân tích nội dung |
| Giảm dữ liệu định danh | Bỏ thông tin định danh không cần thiết nhận diện được trước khi gửi AI; không bảo đảm ẩn danh hoàn toàn |
| Không áp dụng | Tiêu chí bị loại khỏi phép tính vì không có yêu cầu tương ứng đã xác định; không phải điểm 0 |
| Báo cáo lỗi | Kết quả đã được xác minh sai do hệ thống; không dùng lại làm kết quả hợp lệ |

## 4. Quy tắc đã chốt và tình huống nguồn

Mỗi Q bên dưới tham chiếu đúng tình huống đã hỏi trong hội thoại. Tất cả các hàng là lựa chọn của người dùng, trừ các ghi chú nêu rõ phần chưa xác nhận.

### Đánh giá, trích xuất và tính trung thực

| ID / Nguồn | Điều kiện, quy tắc và kết quả quan sát | Ngoại lệ / phản ví dụ |
|---|---|---|
| BR01 / Q01 `ats_scoring_framework` | Khi đối chiếu JD, trọng số kỹ năng/từ khóa 40%, kinh nghiệm/dự án 30%, định dạng 15%, học vấn 15%; hiển thị thành phần | Người dùng đã duyệt rubric sản phẩm tại Phần 5; chưa kiểm chứng đây là công thức của các website được nghiên cứu |
| BR03 / Q03 `unparseable_file_handling` | Không trích xuất được nội dung thì dừng, không trừ lượt/credit; hướng dẫn file có văn bản | Không tự OCR; không khẳng định mọi ATS đều không đọc được file hoặc mọi CV Canva đều lỗi |
| BR05 / Q05 `education_not_required` | JD không yêu cầu học vấn: học vấn không áp dụng, chuẩn hóa các trọng số còn lại về tổng 100 | Không cho học vấn điểm 0; các tiêu chí khác áp dụng quy tắc chuẩn hóa và ngoại lệ tại Phần 5 |
| BR06 / Q06 `rewrite_factual_integrity` | Viết lại từ dữ kiện có sẵn hoặc được ứng viên bổ sung/xác nhận | Không thêm Docker hoặc số liệu thành tích chỉ vì JD yêu cầu; tách thông tin thiếu thành câu hỏi |
| BR12 / Q12 `analysis_without_jd` | Không có JD vẫn kiểm tra CV tổng quát bằng một lượt cơ bản | Không hiển thị điểm phù hợp JD hoặc gộp thành “điểm ATS” 0–100 |
| BR14 / Q14 `optimized_cv_output` | Cho đối chiếu cũ/mới, sửa hoặc từ chối đề xuất; xuất PDF/DOCX theo mẫu đơn giản có văn bản | Không cố giữ thiết kế file gốc; không bảo đảm mọi ATS đọc chính xác |
| BR15 / Q15 `cv_jd_language_scope` | Hỗ trợ Việt–Anh và đối chiếu chéo ngôn ngữ; viết lại mặc định giữ ngôn ngữ CV | Chỉ đổi ngôn ngữ khi yêu cầu; không coi khác cách dịch là thiếu kỹ năng |
| BR18 / Q18 `mandatory_jd_requirement` | Hiển thị cảnh báo yêu cầu bắt buộc chưa có bằng chứng cạnh điểm | Không tự kết luận chắc chắn đậu/rớt; “CV chưa thể hiện” khác “ứng viên không có” |
| BR23 / Q23 `extraction_review_before_analysis` | Ứng viên xem/sửa nội dung trích xuất trước khi xác nhận chạy; nội dung đã xác nhận làm căn cứ đánh giá | Nhận xét định dạng vẫn dựa file gốc; chưa chạy thì chưa dùng lượt/credit |
| BR24 / Q24 `basic_report_visibility` | Báo cáo cơ bản có điểm tổng/thành phần khi áp dụng, cảnh báo bắt buộc và lỗi chính kèm dẫn chứng ngắn | Phân tích chi tiết từng mục, sửa cụ thể và viết lại thuộc bộ nâng cao; tổng quát vẫn theo BR12 |
| BR25 / Q25 `premium_without_jd` | Không có JD vẫn được mua bộ nâng cao để tối ưu tổng quát và xuất file | Không bịa vị trí tuyển dụng; thêm JD để phân tích sau là lượt mới |
| BR35 / Q35 `insufficient_jd_for_scoring` | JD thiếu căn cứ: yêu cầu bổ sung, chưa chạy đối chiếu và chưa trừ lượt/credit | Không tự bịa yêu cầu nghề nghiệp; chỉ chuyển sang tổng quát khi ứng viên chọn |

### Lượt, credit, thanh toán và khắc phục lỗi

| ID / Nguồn | Điều kiện, quy tắc và kết quả quan sát | Ngoại lệ / phản ví dụ |
|---|---|---|
| BR02 / Q02 `credit_and_monetization` | Freemium: 3 lượt cơ bản ban đầu; credit mua thêm cho tính năng nâng cao | Phần Pro trong đề xuất ban đầu được loại khỏi phạm vi bởi BR16 |
| BR04 / Q04 `analysis_credit_failure`, `analysis_timeout_handling`, `analysis_timeout_start` | Giữ lượt/credit khi hệ thống nhận lượt chạy; chỉ thu khi đủ kết quả. Giới hạn 90 giây từ lúc nhận lượt và giữ credit, gồm cả chờ và xử lý; quá hạn thì hủy, trả phần giữ và thông báo lỗi | Bấm lặp không tạo phí lần hai; kết quả đến sau khi tác vụ đã hủy không được thu lại credit. Mất kết nối sau khi hoàn tất không tính thêm |
| BR11 / Q11 và các quyết định thanh toán | Chỉ thanh toán theo đơn hệ thống tạo, số tiền cố định, hạn 15 phút. Khi thanh toán muộn/báo trễ: chuyển Chờ đối soát; chuyên viên tài chính/kế toán kiểm tra tiền vào cổng và duyệt cộng credit đúng gói đã mua trong tối đa 2 ngày làm việc | Không có hoàn tiền trong ứng dụng; nếu tài khoản đã xóa thì xử lý ngoài ứng dụng. Chỉ cộng credit từ xác nhận hợp lệ của cổng, mỗi giao dịch đúng một lần; tách bạch khỏi quản trị viên kỹ thuật |
| BR13 / Q13 `premium_credit_unit` và `credit_deduction_trigger` | 1 credit cho 1 bộ nâng cao; thu khi đã có báo cáo chi tiết, bản viết lại và chức năng xuất file sẵn sàng; sửa thủ công, xem/tải lại miễn phí | Không đợi người dùng bấm tải; việc từ chối gợi ý không tự hoàn credit. Lỗi hệ thống vẫn xử lý theo BR37; yêu cầu AI tạo bản khác cần xác nhận lượt mới |
| BR16 / Q16 `insufficient_basic_credits` | Hết 3 lượt cơ bản: xác nhận dùng 1 credit cho bộ nâng cao, hết credit thì mua thêm | Không tự trừ khi tải file; chưa có Pro/subscription hoặc cấp lượt định kỳ |
| BR17 / Q17 `paid_bundle_partial_failure` và `partial_result_retention` | Chưa thu khi thiếu đầu ra; kết thúc thất bại trả phần giữ. Giữ phần đã hoàn tất tối đa 24 giờ từ khi lượt thất bại, không vượt hạn dữ liệu đầu vào, để người dùng chủ động tiếp tục phần lỗi | Tiếp tục mới giữ lại credit, chỉ thu một lần khi đủ bộ; không tự khởi động lại khi có kết quả đến muộn. Xóa CV thì xóa ngay phần dở dang; hết hạn không phục hồi |
| BR19 / Q19 `purchased_credit_expiry` | Credit đã mua không hết hạn khi tài khoản hoạt động | Đóng tài khoản theo BR30 có thể mất số dư sau xác nhận |
| BR20 / `exception_refund_amount` và hai câu trả lời cùng đợt “không áp dụng hoàn tiền” | Không áp dụng hoàn tiền VND trong chính sách dịch vụ; không đổi credit chưa dùng thành tiền, kể cả khách yêu cầu vì lỗi dịch vụ | Trả/bù lượt hoặc credit vẫn theo BR04/17/37. Không suy ra chính sách này loại bỏ nghĩa vụ bắt buộc theo pháp luật; cần xác minh trước mở bán |
| BR27 / `exception_refund_amount` (thay quyết định Q27) | Không có luồng yêu cầu, duyệt, khóa credit hoặc gọi cổng thanh toán để hoàn tiền khi dịch vụ lỗi; xử lý phần giữ và khoản đã thu theo BR04/17/37 | Không tự bù toàn bộ số dư vì một lượt lỗi, không bù lặp; khoản thanh toán sai/thu trùng theo đối soát BR11, chưa tự quy đổi thành credit |
| BR28 / Q28 `credit_pricing_policy` và `payment_order_exception` | Bán gói cố định, cấu hình số credit/giá VND; đơn hàng giữ giá/số lượng lúc tạo và có hiệu lực 15 phút | Giá mới không giảm số dư cũ hoặc đổi giá trị 1 credit; giao dịch muộn/báo trễ theo BR11; giá cụ thể cấu hình động tại G3 |
| BR31 / Q31 `free_trial_allocation` | Chỉ tặng 3 lượt sau xác minh email; chặn nhận lại bằng dấu đối chiếu tối thiểu | Không bảo đảm một người chỉ có một email; quy tắc sau xóa tài khoản được giới hạn bởi BR34 |
| BR36 / Q36 `identical_analysis_resubmission` | Cùng nội dung CV/JD, loại báo cáo, ngôn ngữ, bộ tiêu chí và báo cáo còn hiệu lực: trả kết quả có sẵn, không tính lượt mới | Chủ động tạo bản AI khác cần xác nhận phí; cơ bản lên nâng cao vẫn 1 credit; không trả báo cáo lỗi/đã xóa/hết hạn |
| BR37 / Q37 và `system_roles_selection` | Nhân viên hỗ trợ kiểm tra lỗi và lập phiếu đề nghị; Trưởng nhóm hỗ trợ duyệt bù đúng 1 lượt cơ bản hoặc credit đã thu một lần; đánh dấu báo cáo lỗi và không dùng lại | Không bù chỉ vì không thích điểm; không vừa trả phần giữ do thất bại vừa bù cùng khoản chưa thu; nhân viên hỗ trợ không tự cộng/bù số dư; tách bạch thẩm quyền khỏi kế toán và admin |
| BR38 / Q38 `user_cancels_running_analysis` | Hủy thông thường khi còn chờ: trả lượt/credit đang giữ. Đã bắt đầu xử lý thì không có hủy thông thường | Quyền xóa CV/dữ liệu vẫn giữ nguyên; hoàn tất thì thu, thất bại thì trả theo quy tắc chung |

### Dữ liệu, quyền truy cập và thị trường

| ID / Nguồn | Điều kiện, quy tắc và kết quả quan sát | Ngoại lệ / phản ví dụ |
|---|---|---|
| BR07 / Q07 và `system_roles_selection` | Áp dụng Zero-Trust cho cả 4 vai trò nội bộ (Hỗ trợ, Lead hỗ trợ, Kế toán, Admin). Mặc định chỉ xem trạng thái kỹ thuật, giao dịch hoặc vụ việc được phân công; xem CV/báo cáo bắt buộc phải được ứng viên cấp quyền theo vụ việc (tối đa 72h) | Kế toán chỉ đối soát tiền và gói giá; Lead hỗ trợ chỉ duyệt bù lỗi kỹ thuật; Admin chỉ quản trị tài khoản và hệ thống; không vai trò nào có quyền đọc mọi CV của khách |
| BR08 / Q08 `external_ai_processing` | Giảm dữ liệu định danh nhận diện được trước khi gửi AI và lấy sự đồng ý | Không bảo đảm ẩn danh tuyệt đối; không gửi dữ liệu thật khi chưa xác minh điều khoản nhà cung cấp |
| BR09 / Q09 `analysis_version_history` | Mỗi báo cáo gắn đúng phiên bản CV/JD đã dùng; giữ các lần phân tích riêng | Nội dung đã đổi là lượt mới; không ghi đè lịch sử đã phân tích |
| BR10 / Q10 `cv_deletion_behavior` | Xóa CV: xóa file, nội dung trích xuất, bản viết lại và báo cáo liên quan khỏi dữ liệu hoạt động; hủy tác vụ chưa hoàn tất, trả phần giữ | Không thu hồi được yêu cầu AI đã gửi; không lưu kết quả đến muộn; chứng từ tối thiểu tách riêng |
| BR21 / Q21 `cv_retention_policy` và `cross_version_retention` | Mỗi báo cáo có hạn 90 ngày riêng từ ngày tạo, nhắc trước 7 ngày; giữ CV gốc tương ứng để phục vụ báo cáo còn hạn; chỉ mở lại không gia hạn | Quyền xóa sớm theo BR10 vẫn áp dụng cho CV và các dữ liệu dẫn xuất liên quan; không giữ báo cáo đã hết hạn chỉ vì CV còn được báo cáo khác sử dụng |
| BR22 / Q22 `support_access_duration` | Chỉ người được phân công vụ việc xem dữ liệu được cấp, có nhật ký; quyền hết khi đóng vụ việc hoặc sau 72 giờ, mốc nào đến trước | Ứng viên thu hồi bất kỳ lúc nào; gia hạn phải cấp lại, không mở quyền sang CV khác |
| BR26 / Q26 `paid_report_expiration` và `cross_version_retention` | Hạn 90 ngày riêng từng báo cáo áp dụng cả miễn phí và trả phí; công bố trước sử dụng/mua, nhắc tải về trước xóa | Không phục hồi báo cáo đã xóa; phân tích lại là lượt mới, trừ mở báo cáo có sẵn theo BR36; credit chưa dùng không hết hạn |
| BR29 / Q29 `declining_external_ai_consent` | Không đồng ý gửi AI: vẫn được kiểm tra/sửa trích xuất trong hệ thống, không chạy AI, không trừ lượt/credit | Không tạo báo cáo AI giả hoặc âm thầm đổi sang nhà cung cấp khác |
| BR30 / Q30 `account_deletion_scope_and_policy` | Tự yêu cầu xóa, xác thực lại và xác nhận mất số dư; không có thời gian chờ 7 ngày; đối chiếu giao dịch chờ trước khi đóng | Không hoàn vì tự nguyện đóng; xóa CV/hồ sơ cá nhân, tách chứng từ phải lưu; giao dịch chờ đối soát tối đa 2 ngày theo BR11 |
| BR32 / Q32 `backup_deletion_window` | Bản sao lưu còn dữ liệu đã xóa tối đa 30 ngày; không dùng để tra cứu CV | Phục hồi phải áp dụng lại yêu cầu xóa trước khi mở truy cập |
| BR33 / Q33 và `ai_retention_exception_approval` | Không huấn luyện; log AI thông thường tối đa 30 ngày, chấp nhận ngoại lệ an toàn/pháp lý công bố; chưa bắt buộc ZDR | Bản sửa thay thế mô tả cũ “30 ngày tuyệt đối”; phải kiểm chứng model/API/tính năng và công bố trước khi gửi |
| BR34 / Q34 `deleted_email_trial_record_retention` | Sau xóa tài khoản giữ dấu đối chiếu email tối thiểu 12 tháng để chặn nhận lại lượt, sau đó xóa dấu | Sau thời hạn có thể được tặng lại; không mô tả dấu này là dữ liệu vô danh |
| BR39 / Q39 `initial_market_scope` | Phục vụ thị trường Việt Nam trước, bán gói VND, hỗ trợ CV/JD Việt–Anh ứng tuyển quốc tế | Chưa chủ động mở bán toàn cầu; chưa xác minh nghĩa vụ dữ liệu, thuế, chứng từ và thanh toán cụ thể |

## 5. Điểm số, Rubric chi tiết và bất biến tài chính

### 5.1. Tiêu chuẩn xác định "JD đủ căn cứ để chấm điểm" (BR35 / OQ01 đã giải quyết)

Hệ thống chỉ tiến hành chấm điểm đối chiếu khi JD thỏa mãn đồng thời 4 điều kiện định lượng:
1. **Độ dài tối thiểu:** Toàn văn JD có ít nhất 50 từ (hoặc $\ge 300$ ký tự có nghĩa).
2. **Chức danh xác định:** Có chức danh/vai trò công việc nhận diện được.
3. **Mật độ yêu cầu tối thiểu:** Trích xuất được ít nhất 3 kỹ năng cụ thể hoặc 2 gạch đầu dòng trách nhiệm công việc.
4. **Ngôn ngữ hỗ trợ:** Soạn thảo bằng Tiếng Việt, Tiếng Anh hoặc song ngữ Việt–Anh (BR15).

*Xử lý khi JD không đủ căn cứ:* Dừng đối chiếu, **không trừ lượt cơ bản và không thu credit**. Hiển thị thông báo yêu cầu bổ sung hoặc cho phép người dùng chủ động chọn chế độ kiểm tra CV tổng quát (BR12/BR25).

### 5.2. Mô hình toán học và chuẩn hóa trọng số (Weight Normalization)

Điểm tổng hợp $S$ là hàm chuẩn hóa theo các tiêu chí có căn cứ áp dụng:
$$S = \frac{\sum_{i \in \mathcal{A}} w_i \cdot p_i}{\sum_{i \in \mathcal{A}} w_i}$$

Trong đó:
- $\mathcal{A}$ là tập các trụ cột có đủ căn cứ áp dụng.
- $p_i \in [0, 100]$ là điểm thành phần của trụ cột $i$.
- $w_i$ là trọng số cơ sở theo BR01: Kỹ năng $w_K = 0.40$ (40%), Kinh nghiệm $w_E = 0.30$ (30%), Định dạng $w_F = 0.15$ (15%), Học vấn $w_H = 0.15$ (15%).

**Chuẩn hóa khi học vấn không áp dụng (BR05):**
- Nếu JD không yêu cầu học vấn: Học vấn chuyển sang trạng thái `NOT_APPLICABLE` (không tính điểm 0).
- Tổng trọng số các tiêu chí còn lại: $W_{\text{active}} = 40 + 30 + 15 = 85$.
- Trọng số chuẩn hóa hiển thị: Kỹ năng $w'_K \approx 47.0588\%$, Kinh nghiệm $w'_E \approx 35.2941\%$, Định dạng $w'_F \approx 17.6471\%$.
- Công thức: $S = \frac{0.40 \cdot p_K + 0.30 \cdot p_E + 0.15 \cdot p_F}{0.85}$.

### 5.3. Bảng Rubric chi tiết từng trụ cột (Nguồn: RESEARCH-ATS-002)

#### Trụ cột 1: Kỹ năng & Từ khóa ($p_K$ — Trọng số 40%)
- **Phân nhóm:** Kỹ năng Bắt buộc (Must-have, chiếm 70% của $p_K$) và Kỹ năng Ưu tiên (Nice-to-have, chiếm 30% của $p_K$). Nếu JD không có kỹ năng ưu tiên hoặc không phân loại rõ ràng: chia đều 100% điểm cho toàn bộ các kỹ năng trích xuất được.
- **Thang điểm đối soát từng kỹ năng:**
  - *Exact Match:* Trùng khớp chuỗi chính xác hoặc dạng chuẩn hóa hình thái $\rightarrow$ **100% điểm**.
  - *Synonym Match:* Khớp từ điển từ đồng nghĩa chuẩn (AWS $\leftrightarrow$ Amazon Web Services) $\rightarrow$ **95% điểm**.
  - *Contextual Match:* Kỹ năng liên quan cùng họ công nghệ (vd PostgreSQL $\leftrightarrow$ MySQL) $\rightarrow$ **60% điểm**.
  - *Missing:* Không có bằng chứng trong CV $\rightarrow$ **0% điểm**. Kỹ năng bắt buộc bị 0% sẽ kích hoạt cảnh báo BR18.
- **Phạt nhồi từ khóa (Stuffing Penalty):** Nếu một từ khóa có mật độ $> 3.5\%$ tổng số từ hoặc chuỗi danh từ kỹ thuật liên tiếp $> 10$ từ không ngữ pháp $\rightarrow$ hủy điểm lần xuất hiện thừa và trừ trực tiếp 25% điểm Trụ cột Kỹ năng; điểm $p_K$ sau khi trừ không bao giờ âm (chặn sàn tại 0 điểm).

#### Tiêu chí 2: Kinh nghiệm & Dự án ($p_E$ — Trọng số 30%)
$$p_E = 0.40 \cdot \text{Số năm kinh nghiệm} + 0.30 \cdot \text{Độ khớp chức danh} + 0.30 \cdot \text{Chất lượng gạch đầu dòng}$$
1. **Số năm kinh nghiệm (40%):** So sánh thời gian làm việc thực tế với yêu cầu của JD ($R = Y_{\text{candidate}} / Y_{\text{req}}$):
   - $R \ge 1.0$: **100 điểm** (Nếu $R \ge 2.5$, kích hoạt cờ thông tin Overqualified, không trừ điểm).
   - $0.75 \le R < 1.0$: **85 điểm**.
   - $0.50 \le R < 0.75$: **65 điểm**.
   - $0.30 \le R < 0.50$: **40 điểm**.
   - $R < 0.30$: **15 điểm** (kèm cảnh báo thiếu kinh nghiệm theo BR18).
   - *Ngoại lệ:* JD không yêu cầu số năm cụ thể $\rightarrow$ Điểm kinh nghiệm đạt 100 điểm.
2. **Độ khớp chức danh (30%):**
   - Trùng khớp chức danh chính xác: **100 điểm**.
   - Khớp chức danh tương đương cùng cấp bậc: **90 điểm**.
   - Khớp chức danh khác cấp bậc (vd Junior vs Senior): **70 điểm**.
   - Cùng lĩnh vực khác vai trò: **50 điểm**; Khác ngành hoàn toàn: **20 điểm**.
3. **Chất lượng gạch đầu dòng mô tả công việc (30%):**
   - Mỗi gạch đầu dòng chấm: Động từ hành động mạnh (30đ), Ngữ cảnh/Nhiệm vụ rõ ràng (30đ), Số liệu định lượng %, $, thời gian, số lượng (40đ).
   - Trừ 5 điểm nếu một động từ lặp lại $\ge 3$ lần. Điểm tiêu chí là trung bình cộng các gạch đầu dòng trừ điểm phạt lặp từ.
#### Trụ cột 3: Định dạng & Khả năng trích xuất ($p_F$ — Trọng số 15%)
- Khả năng trích xuất văn bản (Text Extractability): **40 điểm** (chọn/copy được, lỗi font $< 2\%$; nếu PDF thuần ảnh $\rightarrow$ dừng theo BR03).
- Bố cục 1 cột không bảng phức tạp (Layout & Tables): **30 điểm** (không text box thả nổi, không bảng ẩn chia cột).
- Tiêu đề phân mục chuẩn (Section Headings): **20 điểm** (Work Experience, Education, Skills chuẩn convention).
- Thông tin liên hệ đầy đủ (Contact Info): **10 điểm** (Email, SĐT, Địa điểm, Link LinkedIn/Portfolio).

#### Trụ cột 4: Học vấn & Chứng chỉ ($p_H$ — Trọng số 15%)
Khi JD có yêu cầu:
- Cấp bằng (Degree Level): Đạt/vượt yêu cầu $\rightarrow$ **100 điểm**; Thấp hơn 1 bậc $\rightarrow$ **60 điểm**; Thấp hơn $\ge 2$ bậc $\rightarrow$ **20 điểm**.
- Chuyên ngành (Field of Study): Đúng ngành $\rightarrow$ **100 điểm**; Ngành gần $\rightarrow$ **70 điểm**; Khác ngành $\rightarrow$ **40 điểm**.
- Chứng chỉ bắt buộc (Certifications): Đầy đủ $\rightarrow$ **100 điểm**; Thiếu $\rightarrow$ **0 điểm** (kèm cảnh báo BR18). Nếu JD không đòi chứng chỉ cụ thể $\rightarrow$ lấy bằng điểm Cấp bằng.

### 5.4. Quy tắc làm tròn và Phân tầng nhãn diễn giải
- Điểm thành phần giữ 2 số thập phân khi tính toán.
- Điểm tổng hợp cuối cùng $S$ làm tròn đến số nguyên gần nhất (Round Half Up).
- **4 Phân tầng nhãn:**
  - **85 – 100 điểm:** *Xuất sắc — Sẵn sàng ứng tuyển (Interview Ready)*
  - **70 – 84 điểm:** *Khá — Đạt chuẩn cơ bản (Minor Tweaks Needed)*
  - **50 – 69 điểm:** *Trung bình — Cần cải thiện (Significant Gaps)*
  - **0 – 49 điểm:** *Kém — Nguy cơ bị loại cao (High Rejection Risk)*
- Cảnh báo yêu cầu bắt buộc (BR18) hiển thị độc lập ở khối riêng, không bị che lấp bởi điểm số cao.

### 5.5. Bất biến phải giữ khi diễn giải các quyết định đã chốt

- Tách lượt cơ bản, credit khả dụng, credit đang giữ và tiền thanh toán; trả credit không đồng nghĩa hoàn tiền.
- Một giao dịch thanh toán được cộng credit đúng một lần dù thông báo lặp.
- Credit đang giữ cho yêu cầu A không đồng thời được dùng cho yêu cầu B; không có chức năng đổi credit thành tiền.
- Không thu khi bộ nâng cao còn thiếu đầu ra; đủ kết quả chỉ thu một lần.
- Trả khoản đang giữ hoặc bù khoản đã thu theo đúng trạng thái, không cộng lại hai lần cho cùng sự cố.
- Kết quả đến sau khi yêu cầu đã hủy/xóa không được hồi sinh dữ liệu hoặc phát sinh khoản thu.
- Tranh chấp hoàn tất/hủy/xóa phải có một kết quả nhất quán; cơ chế thực thi sẽ thiết kế tại G3, không dùng thao tác bấm của trình duyệt làm nguồn sự thật.
- Ví dụ: còn 1 credit và mở hai yêu cầu khác nhau đồng thời thì chỉ một yêu cầu được giữ credit. Không cho số dư âm hoặc hứa xử lý cả hai bằng cùng credit.

## 6. Luồng nghiệp vụ

| Bước | Actor / đầu vào | Hành động và đầu ra | Khi thất bại / quy tắc |
|---|---|---|---|
| 1 | Ứng viên đăng ký | Xác minh email; cấp 3 lượt nếu đủ điều kiện | BR31/BR34; không cấp lặp |
| 2 | Ứng viên tải CV, có thể nhập JD | Chỉ nhận PDF/DOCX tối đa 10MB, 5 trang, không mật khẩu; JD tối đa 2.500 từ; trích xuất rồi cho xem/sửa/xác nhận | File không đạt giới hạn, có mật khẩu hoặc không đọc được thì dừng trước phân tích, không trừ lượt |
| 3 | Ứng viên chọn loại phân tích | Kiểm tra JD nếu đối chiếu; thông báo AI và xin đồng ý | JD thiếu thì bổ sung/chủ động chọn tổng quát; từ chối AI chỉ dừng ở trích xuất |
| 4 | Đầu vào đã xác nhận | Tìm báo cáo còn hiệu lực cùng ngữ cảnh | Có thì mở lại miễn phí; không lấy báo cáo của người dùng khác |
| 5 | Ứng viên xác nhận chạy | Kiểm tra quyền sử dụng, giữ lượt/credit và đưa vào chờ | Không đủ thì không chạy; hủy khi còn chờ thì trả phần giữ |
| 6 | Hệ thống + AI | Phân tích; nâng cao có thêm bản viết lại và xuất file; tối đa 90 giây từ khi nhận lượt và giữ credit, gồm cả chờ; quá hạn thì hủy và trả phần giữ | Phần đã hoàn tất được giữ tối đa 24 giờ sau thất bại và không vượt hạn đầu vào; người dùng tiếp tục mới giữ lại credit; kết quả đến muộn không phục hồi tác vụ |
| 7 | Bộ kết quả hoàn tất | Lưu đúng phiên bản và thu lượt/credit một lần: cơ bản cần báo cáo cơ bản; nâng cao cần báo cáo chi tiết, bản viết lại và xuất file sẵn sàng | Không chờ người dùng bấm tải; mất kết nối trình duyệt không tạo lần thu khác |
| 8 | Ứng viên xem/sửa kết quả | Sửa thủ công, từ chối đề xuất, tải PDF/DOCX; mở lịch sử | Chủ động tạo bản AI mới cần xác nhận lượt mới |
| 9 | Ứng viên yêu cầu hỗ trợ | Cấp quyền vụ việc nếu cần; xác minh lỗi | Lỗi đúng thì trả lượt/credit một lần, đánh dấu báo cáo lỗi |
| 10 | Đến hạn hoặc yêu cầu xóa | Nhắc trước hạn, xóa dữ liệu liên quan, xử lý backup theo lịch | Không hồi sinh dữ liệu từ kết quả AI đến muộn hoặc phục hồi backup |

Các bảng chia luồng phân tích, thanh toán và xóa dữ liệu thay vì một sơ đồ tổng hợp; chưa tạo sơ đồ trong bản nháp này.

### Trạng thái logic đề xuất để rà soát

Tên trạng thái dưới đây chỉ là cách mô tả nghiệp vụ, chưa phải enum hoặc schema kỹ thuật được duyệt.

| Thực thể | Trạng thái và chuyển tiếp chính | Ràng buộc vận hành |
|---|---|---|
| CV/đầu vào | Đã nhận → không đọc được hoặc chờ xác nhận → đã xác nhận; có thể bị xóa/hết hạn | Đã chốt PDF/DOCX, 10MB, 5 trang, không mật khẩu; giữ CV cho báo cáo còn hạn; cách xác định số trang DOCX thuộc G3 |
| Lượt phân tích | Chờ xử lý → đang xử lý → hoàn tất hoặc thất bại/hết thời gian; chờ có thể hủy; yêu cầu xóa có thể hủy đang chạy | Hạn 90 giây từ khi nhận lượt và giữ credit, gồm cả chờ; phần hoàn tất giữ tối đa 24 giờ sau thất bại, không vượt hạn đầu vào |
| Báo cáo | Hợp lệ → lỗi đã xác minh; hợp lệ/lỗi → hết hạn/đã xóa | Hạn 90 ngày riêng; chỉ khiếu nại khi báo cáo còn hạn và ứng viên cấp quyền hỗ trợ (tối đa 72h) |
| Credit của lượt | Khả dụng → đang giữ → đã thu hoặc được trả; đã thu có thể được bù do lỗi đã xác minh | Quá hạn 90 giây gồm cả chờ thì trả phần giữ; lưu kết quả dở dang 24h không giữ credit |
| Đơn mua gói | Chờ thanh toán trong 15 phút → đã xác nhận thành công/hết hạn; thanh toán muộn/báo trễ → Chờ đối soát | Kế toán đối soát tối đa 2 ngày làm việc; duyệt cộng credit gói mua khi xác minh tiền vào cổng |
| Quyền hỗ trợ | Chưa cấp → đang hiệu lực → thu hồi/hết hạn/đóng vụ việc | Áp dụng đúng người được cấp (tối đa 72h); hỗ trợ lập đề nghị, Trưởng nhóm hỗ trợ duyệt bù credit |
| Đóng tài khoản | Yêu cầu + xác thực/xác nhận → xử lý giao dịch chờ nếu có → đóng/xóa | Không hoàn tiền số dư; giao dịch chờ đối soát xử lý ngoài ứng dụng nếu tài khoản đã xóa |

## 7. Ma trận quyền theo các quyết định hiện có

| Hành động/dữ liệu | Ứng viên | Nhân viên hỗ trợ | Trưởng nhóm hỗ trợ | Kế toán / Tài chính | Quản trị viên hệ thống | Bên ngoài (AI / Cổng) |
|---|---|---|---|---|---|---|
| CV, nội dung xác nhận, báo cáo, bản viết lại | Toàn quyền dữ liệu của mình | Chỉ vụ việc được giao và được cấp quyền, còn hạn | Chỉ vụ việc cần duyệt bồi thường và được cấp quyền | Tuyệt đối không có quyền xem | Mặc định không xem; chỉ xem khi được cấp quyền hỗ trợ | AI chỉ nhận nội dung cần thiết sau khi ứng viên đồng ý |
| Thu hồi quyền hỗ trợ | Chủ dữ liệu thu hồi ngay | Không tự gia hạn hoặc mở rộng | Không tự gia hạn hoặc mở rộng | Không áp dụng | Không tự gia hạn hoặc mở rộng | Không áp dụng |
| Chạy phân tích, dùng credit | Chủ tài khoản xác nhận | Không được cấp quyền chạy thay | Không được cấp quyền chạy thay | Không được cấp quyền chạy thay | Không được cấp quyền chạy thay | AI không quyết định số dư |
| Quản lý gói giá credit | Xem/mua gói đang bán | Không có quyền | Không có quyền | Tạo/sửa/ngừng bán gói VND theo BR28 | Phối hợp triển khai kỹ thuật | Cổng thanh toán xử lý số tiền đơn |
| Giao dịch và đối soát | Xem đơn của mình | Kiểm tra lỗi giao dịch trong vụ việc | Giám sát khiếu nại thanh toán | Đối soát tiền vào cổng, duyệt cộng credit đơn trễ (BR11) | Xem log kỹ thuật hệ thống | Cổng thanh toán là nguồn xác nhận |
| Bồi thường lỗi phân tích | Yêu cầu xác minh theo BR37 | Kiểm tra lỗi trích xuất, lập phiếu đề nghị | Phê duyệt phiếu bồi thường 1 credit/lượt | Hạch toán số credit bù phát sinh | Giám sát nhật ký hệ thống | AI không quyết định bồi thường |
| Quản trị tài khoản & phân vai | Quản lý tài khoản cá nhân | Không có quyền | Phân công vụ việc cho hỗ trợ viên | Không có quyền | Gán vai trò nội bộ, khóa/mở tài khoản, xem audit logs | Không áp dụng |
| Xóa CV / Đóng tài khoản | Chủ dữ liệu yêu cầu | Không được tự ý xóa CV khách | Không được tự ý xóa CV khách | Không được tự ý xóa CV khách | Không được tự ý xóa dữ liệu khách qua hỗ trợ | Không thu hồi được request AI đã gửi |

Không bổ sung quyền tìm kiếm/xem CV của mọi ứng viên cho nhà tuyển dụng. Phân quyền quản trị không được suy ra thành “admin toàn quyền”.

## 8. Phạm vi và yêu cầu chất lượng

### Trong phạm vi đã chốt

Tài khoản xác minh email; CV/JD Việt–Anh; xem/sửa trích xuất; phân tích cơ bản và nâng cao; kiểm tra tổng quát không JD; điểm có giải thích; viết lại trung thực; xuất PDF/DOCX; lịch sử phiên bản; gói credit cố định VND; đối soát thanh toán; trả/bù credit khi lỗi; quyền hỗ trợ; đồng ý AI; xóa/hết hạn dữ liệu và tài khoản.

### Ngoài phạm vi đã chọn hoặc chưa được yêu cầu

- Không có Pro/subscription, lượt miễn phí định kỳ, OCR tự động, khôi phục kết quả đã hết hạn hoặc giữ nguyên thiết kế file CV gốc.
- Không có chức năng hoàn tiền VND, quyền duyệt hoàn tiền hoặc trạng thái hoàn tiền. Trả/bù credit không phải hoàn tiền.
- Không cam kết vượt ATS, tư cách tuyển dụng hoặc độ chính xác tuyệt đối của AI.
- Chưa có yêu cầu cổng HR, phân tích hàng loạt, job board, ứng tuyển tự động, kết nối ATS doanh nghiệp, chương trình khuyến mãi hoặc mở bán toàn cầu.
- Chưa chọn stack, database, API/model, cổng thanh toán; không có mã nguồn hoặc triển khai được phê duyệt trong hồ sơ này.

### Yêu cầu có thể kiểm chứng từ chính sách đã chốt

| Yêu cầu | Bằng chứng cần ở giai đoạn triển khai |
|---|---|
| Không thu trùng/không chi trùng số dư | Bấm lặp, thông báo thanh toán lặp, hai yêu cầu đồng thời cùng số dư cuối phải ra đúng số dư và số lần thu |
| Không xâm phạm dữ liệu người khác | Tài khoản khác hoặc hỗ trợ không được cấp quyền không đọc được CV/báo cáo; thu hồi/hết hạn chặn truy cập tiếp theo |
| Không chạy AI trước đồng ý | Từ chối đồng ý vẫn trích xuất được nhưng không có request nội dung tới AI |
| Xóa không hồi sinh dữ liệu | Kết quả AI đến sau xóa và phục hồi backup không làm nội dung xuất hiện lại |
| Giải thích điểm | Báo cáo cho biết tiêu chí áp dụng, trọng số, bằng chứng; JD thiếu không có điểm phù hợp bịa ra |
| Không bịa dữ kiện khi viết lại | Đối chiếu với nội dung ứng viên xác nhận; kỹ năng/số liệu mới phải có xác nhận trước khi dùng |
| PDF/DOCX dùng được | Mở file, trích xuất được văn bản, đúng nội dung đã duyệt và tiếng Việt/Anh; không suy ra mọi ATS đều tương thích |
| Thời hạn | Mô phỏng qua mốc 72 giờ, 90 ngày, backup 30 ngày và dấu email 12 tháng theo đúng quy tắc đã chốt |

Đã chốt hủy tác vụ và trả credit khi quá hạn 90 giây. Đây chưa phải cam kết mọi lượt đều hoàn tất trong 90 giây. Chưa có mục tiêu tải, độ chính xác hoặc tỷ lệ phân tích đúng được duyệt. Các yêu cầu kiểm chứng trên chưa được triển khai hoặc nghiệm thu.

## 9. Câu hỏi mở và điều kiện để chốt G1

| ID | Quyết định/điểm cần làm rõ | Nguồn và ảnh hưởng | Người trả lời / mốc |
|---|---|---|---|
| OQ01 | **ĐÃ GIẢI QUYẾT**: Rubric 4 trụ cột, ngưỡng JD đủ căn cứ ($\ge 50$ từ/300 ký tự), chia đều kỹ năng khi không phân loại bắt buộc/ưu tiên, phạt nhồi từ khóa chặn sàn ở 0 điểm | BR01/05/18/35 và RESEARCH-ATS-002 | Người dùng đã duyệt rubric và quy tắc biên `rubric_skill_edge_rules` | ĐÃ ĐÓNG |
| OQ02 | **ĐÃ GIẢI QUYẾT**: Hạn 90 giây gồm cả chờ từ lúc nhận lượt/giữ credit; quá hạn hủy và trả phần giữ. Kết quả dở dang giữ tối đa 24 giờ sau thất bại, không vượt hạn đầu vào | BR04/17/38 | Người dùng đã duyệt `analysis_timeout_handling`, `analysis_timeout_start`, `partial_result_retention` | ĐÃ ĐÓNG |
| OQ03 | **ĐÃ GIẢI QUYẾT**: Thu credit khi báo cáo chi tiết, bản viết lại và xuất file sẵn sàng; sửa thủ công/tải lại miễn phí | BR13/14 | Người dùng đã duyệt `credit_deduction_trigger` | ĐÃ ĐÓNG |
| OQ04 | **ĐÃ GIẢI QUYẾT**: Hạn 90 ngày riêng theo từng báo cáo, CV gốc giữ phục vụ báo cáo còn hạn; xóa sớm theo BR10 | BR09/10/21/26 | Người dùng đã duyệt `cross_version_retention` | ĐÃ ĐÓNG |
| OQ05 | **ĐÃ GIẢI QUYẾT**: Đơn 15 phút, thanh toán theo đơn số tiền cố định; thanh toán muộn/báo trễ đối soát trong 2 ngày làm việc, admin duyệt cộng credit gói mua; không hoàn tiền trong app | BR11/20/27/28/30/37 | Người dùng đã duyệt `payment_incident_resolution`, `payment_collection_scope` | ĐÃ ĐÓNG |
| OQ06 | **ĐÃ GIẢI QUYẾT**: PDF/DOCX tối đa 10MB, 5 trang, không mật khẩu; JD từ 50 đến 2.500 từ (hoặc $\ge 300$ ký tự có nghĩa) | BR03/15/35 | Người dùng đã duyệt `file_input_constraints` | ĐÃ ĐÓNG |
| OQ07 | **ĐÃ GIẢI QUYẾT**: Chuẩn hóa bộ 5 roles vận hành (Ứng viên, Hỗ trợ, Trưởng nhóm hỗ trợ, Kế toán, Quản trị viên). Tách bạch rạch ròi: Kế toán đối soát và duyệt credit thanh toán; Lead hỗ trợ duyệt bồi thường lỗi kỹ thuật; Admin quản trị tài khoản nội bộ và hệ thống; Zero-Trust bảo vệ nội dung CV | BR07/22/28/37 | Người dùng đã duyệt phương án bộ 5 roles tại `system_roles_selection` | ĐÃ ĐÓNG |
| OQ08 | Gói cố định bằng VND được cấu hình; giá thực tế và cách công bố thuế/phí còn mở | BR28/39 | Người dùng quyết định trước mở bán sau đối chiếu chi phí; không tự bịa giá hoặc cơ chế lưu database |
| OQ09 | Cần chọn nhà cung cấp/model/tính năng, kiểm tra điều khoản dữ liệu và nghĩa vụ tại Việt Nam; giữ chính sách 30 ngày thông thường có ngoại lệ | BR30/33/39; báo cáo `ai-cv-privacy-retention.md`, revision 2 | Chọn kỹ thuật tại G3; đơn vị vận hành xác nhận nghĩa vụ pháp lý trước dữ liệu thật |
| OQ10 | Chưa chọn mục tiêu chất lượng, bộ hồ sơ đánh giá có quyền sử dụng và quy mô tải | BR01/05/18/35 | Mục tiêu cần người dùng xác nhận; phương pháp đo và triển khai sẽ thiết kế sau |
Không cần người dùng trả lời câu hỏi triển khai như dùng queue, khóa hay database nào ở discovery. Cần ưu tiên các quyết định OQ01–OQ07 theo từng nhóm nhỏ, không hỏi lại các lựa chọn đã chốt.

## 10. Kết quả rà soát bản nháp và điểm tiếp tục

- Đã đóng toàn bộ 7 câu hỏi mở cốt lõi về nghiệp vụ: OQ01 (Rubric & trường hợp biên), OQ02 (Timeout 90s & 24h dở dang), OQ03 (Thu credit khi đủ bộ), OQ04 (Lưu trữ 90 ngày độc lập), OQ05 (Đơn 15p, đối soát 2 ngày, cộng credit gói, không hoàn tiền), OQ06 (Giới hạn file & JD), OQ07 (Chuẩn hóa bộ 5 roles vận hành chuyên nghiệp).
- Các câu hỏi OQ08–OQ10 thuộc thẩm quyền kỹ thuật và kiến trúc tại Cổng G3 (cấu hình giá DB, chọn model AI OpenAI/Anthropic) và G4 (bộ benchmark nghiệm thu).
- Tài liệu Đặc tả nghiệp vụ Draft 6 đã cập nhật trọn vẹn bộ 5 roles, đảm bảo tính nhất quán tuyệt đối cho toàn bộ hồ sơ.
