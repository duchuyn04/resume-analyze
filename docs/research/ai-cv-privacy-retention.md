# Nghiên cứu: dữ liệu CV, huấn luyện AI và thời hạn lưu trữ

- Revision: 2 — đính chính báo cáo trước; đối chiếu ngày 2026-09-22.
- Phạm vi: chính sách công khai của dịch vụ CV và API AI; không kiểm toán hệ thống thực tế hoặc hợp đồng riêng của họ.
- Trạng thái quyết định: đã xác nhận hướng không huấn luyện, lưu log thông thường tối đa 30 ngày và chấp nhận ngoại lệ an toàn/pháp lý được công bố; chưa chọn nhà cung cấp/model.
- Bằng chứng quyết định: phát biểu “tôi đồng ý theo recommand”, sau đó chọn “30 ngày thông thường, có ngoại lệ” tại câu hỏi `ai_retention_exception_approval` sau khi được đính chính; không có mã tham chiếu hội thoại bền vững.

## 1. Đính chính cần thiết

Báo cáo revision trước chứa các khẳng định không đủ bằng chứng, không được tiếp tục dùng làm căn cứ:

- Không chứng minh được mọi dịch vụ CV đều dùng API thương mại hoặc đều chấp nhận đúng 30 ngày lưu trữ.
- Không chứng minh được ZDR chỉ dành cho tập đoàn lớn, cần mức chi tiêu hàng trăm nghìn USD/tháng hoặc bắt buộc ký BAA. OpenAI nêu xét duyệt và điều kiện bổ sung, không công bố các điều kiện này như báo cáo đã khẳng định. [S4]
- Không thể cam kết mọi dữ liệu API đều bị xóa vĩnh viễn sau 30 ngày: phải phân biệt abuse logs, application state, model/tính năng và ngoại lệ pháp lý/an toàn. [S4][S5]
- Loại tên/email/số điện thoại là giảm thiểu dữ liệu, không bảo đảm ẩn danh tuyệt đối. Lịch sử công việc có thể vẫn nhận diện ứng viên. Đây là đánh giá rủi ro cho dự án, không phải kết quả kiểm thử bộ lọc đã triển khai.
- Mốc lưu nội bộ 90 ngày không tự chứng minh tuân thủ GDPR hay ưu thế bảo mật so với sản phẩm khác. Chưa thực hiện đánh giá pháp lý.
- Agent nghiên cứu trước đã lỗi kết nối và không bàn giao kết quả. Bản sửa này dựa trên các nguồn chính thức Main đã đọc trực tiếp; không có kiểm định độc lập của agent.

## 2. Các dịch vụ CV công bố điều gì?

| Dịch vụ | Dữ kiện trong nguồn chính thức | Giới hạn kết luận |
|---|---|---|
| Resume Worded | Mục “Use of Artificial Intelligence” nêu dùng OpenAI, Gemini, Anthropic; nhà cung cấp bị ràng buộc không dùng dữ liệu API để huấn luyện/cải tiến mô hình; cho lưu ngắn hạn để giám sát lạm dụng. Cho yêu cầu xóa tài khoản; phần tóm tắt nói xử lý trong vài ngày. Phần về CV nói có thể dùng dữ liệu ẩn danh, tổng hợp để cải thiện engine và cho yêu cầu xóa CV qua email trong 48 giờ. [S1] | Không nêu số ngày cụ thể cho lưu trữ bên AI. Không suy ra “chính xác 30 ngày”, không đồng nhất cải thiện engine nội bộ bằng dữ liệu tổng hợp với huấn luyện mô hình của nhà cung cấp. Không công bố trong các mục này một mốc tự xóa CV 90 ngày. |
| Teal | Mục 3 cho biết gửi nội dung nghề nghiệp cho AI như OpenAI; nhà cung cấp chỉ dùng dữ liệu để cung cấp dịch vụ. Teal có thể dùng nội dung đã ẩn danh và tổng hợp để cải thiện AI. Mục 6 giữ dữ liệu khi tài khoản còn hoạt động hoặc cần cho dịch vụ; sau xóa tài khoản, xóa hoặc ẩn danh trong 30 ngày, có ngoại lệ; bản sao lưu theo lịch riêng. [S2] | 30 ngày sau xóa tài khoản không phải thời hạn log của AI provider. Không nêu thời hạn cụ thể bên AI trong mục này. Đây là tuyên bố chính sách, không phải bằng chứng kiểm toán. |
| Rezi | Chính sách nêu lưu nội dung CV khi có tài khoản, chỉ giữ thông tin khi có lý do; cho yêu cầu xóa qua email hoặc đóng tài khoản trong dashboard. Phần Choices tuyên bố nội dung tạo khi không có tài khoản không được lưu, nhưng có ngoại lệ thu thập tự động được mô tả riêng. [S3] | Không có cam kết rõ về số ngày lưu log AI hoặc loại trừ huấn luyện AI trong các mục đã đọc. Không suy ra “có huấn luyện” chỉ từ câu cải thiện dịch vụ. |
| Jobscan | Chưa có nội dung chính sách chính thức được kiểm chứng trực tiếp đủ để kết luận trong báo cáo này. | Rút các kết luận trước về huấn luyện, nhà cung cấp và thời hạn lưu. Không dùng câu quảng bá “học từ CV trước đó” để suy ra sử dụng CV định danh của khách hàng để huấn luyện. |

Kết luận: có ví dụ thực tế công khai cho hướng không huấn luyện bởi AI provider nhưng vẫn cho lưu tạm (Resume Worded). Mẫu khảo sát này không chứng minh tỷ lệ phổ biến hoặc một chuẩn bắt buộc của toàn ngành. [S1–S3]

## 3. Điều khoản API: phải xem đúng sản phẩm và tính năng

### OpenAI API

- Dữ liệu API mặc định không dùng để huấn luyện/cải tiến mô hình, trừ khi khách hàng chủ động opt-in. Không áp dụng kết luận này cho mọi sản phẩm tiêu dùng. [S4]
- Abuse monitoring logs có thể chứa prompt/response; mặc định giữ tối đa 30 ngày, trừ khi pháp luật yêu cầu lâu hơn hoặc cần bảo vệ dịch vụ/bên thứ ba khỏi tác hại. [S4]
- Application state là lớp lưu riêng. Ví dụ Responses API mặc định có lưu trạng thái; files, conversations và các tài nguyên khác có thể được giữ đến khi xóa. `store=false` không đồng nghĩa loại bỏ abuse logs. [S4]
- ZDR/Modified Abuse Monitoring cần phê duyệt trước và điều kiện bổ sung. Khả năng áp dụng phụ thuộc endpoint, model và tính năng; vẫn có ngoại lệ được tài liệu nêu. Không diễn giải ZDR thành “không ghi bất kỳ dữ liệu nào trong mọi trường hợp”. [S4]

### Anthropic API

- Inputs/outputs của sản phẩm commercial mặc định không dùng để huấn luyện. Chủ động gửi feedback/bug report hoặc cho phép dùng dữ liệu có thể tạo ngoại lệ; nội dung liên quan feedback có thể được giữ đến 5 năm. [S6]
- API chuẩn xóa inputs/outputs trong vòng 30 ngày kể từ tiếp nhận/tạo, trừ tính năng lưu dài hơn do khách hàng kiểm soát, thỏa thuận khác, thực thi Usage Policy hoặc pháp luật. Có yêu cầu riêng cho một số nhóm model. [S5]
- Nguồn nêu dữ liệu bị hệ thống đánh dấu vi phạm Usage Policy có thể được giữ đến 2 năm; điểm phân loại an toàn đến 7 năm. Vì vậy không được cam kết giới hạn cứng 30 ngày dựa trên chính sách API mặc định. [S5]
- Nguồn có đề cập thỏa thuận zero retention, nhưng không chứng minh chỉ doanh nghiệp lớn mới có thể ký. [S5]

### Google Cloud / Gemini

Chưa kiểm chứng đủ tài liệu trực tiếp cho kết luận ở revision này. Rút các khẳng định gộp Vertex AI với Gemini API Paid Tier và tuyên bố chỉ lưu trong vùng khách hàng chọn. Nếu chọn Google ở bước kiến trúc, cần đánh giá riêng đúng dịch vụ, tier, model, tính năng, khu vực và ngoại lệ.

## 4. Khuyến nghị cho dự án

Đây là đề xuất sản phẩm, không phải cam kết rằng hệ thống đã triển khai hoặc nhà cung cấp đã được chọn.

1. Không dùng CV/JD để huấn luyện mô hình; chọn điều khoản API phù hợp, không opt-in chia sẻ huấn luyện và không gửi feedback kèm nội dung người dùng sang nhà cung cấp. [Căn cứ: S4, S6]
2. Chấp nhận mục tiêu lưu log thông thường tối đa 30 ngày nếu người dùng đồng ý các ngoại lệ an toàn/pháp lý được công bố. Nếu yêu cầu là giới hạn cứng 30 ngày, chưa được coi API mặc định là đáp ứng; phải kiểm tra thỏa thuận và giải pháp khác. [Căn cứ: S4, S5]
3. Giảm thông tin định danh không cần thiết trước khi gửi; vẫn coi phần còn lại là dữ liệu cần bảo vệ. Không hứa bảo mật/ẩn danh tuyệt đối.
4. Trước tích hợp, xác minh nhà cung cấp, model, endpoint, cache, application state, feedback, log, xóa dữ liệu và điều khoản xử lý dữ liệu. Không coi việc chọn một tên hãng là đủ chứng minh tuân thủ. [Căn cứ: S4–S6]
5. Trước khi gửi, công bố bên nhận dữ liệu, nội dung/mục đích, thời hạn thường lệ và ngoại lệ; lấy sự đồng ý như quyết định nghiệp vụ đã có. Không đồng ý thì chỉ trích xuất, không chạy AI và không trừ lượt/credit.
6. Giữ chính sách nội bộ đã được người dùng chọn: CV/kết quả hoạt động 90 ngày, nhắc trước 7 ngày, cho xóa sớm; bản sao lưu tối đa 30 ngày sau khi xóa, áp dụng lại yêu cầu xóa khi phục hồi. Các thời hạn này không thay thế thời hạn phía AI.
7. Giữ dấu đối chiếu email tối thiểu để chặn nhận lại lượt miễn phí trong 12 tháng sau xóa tài khoản, theo lựa chọn của người dùng; không mô tả đó là dữ liệu vô danh và cần đối chiếu cơ sở pháp lý trước vận hành.

## 5. Phê duyệt và điểm còn mở

- Đã xác nhận sau đính chính: không huấn luyện; lưu log thông thường tối đa 30 ngày, có ngoại lệ an toàn/pháp lý được công bố; không bắt buộc ZDR ngay từ đầu.
- Điều kiện áp dụng: kiểm tra đúng API/model/tính năng và giải thích ngoại lệ cho ứng viên trước khi gửi; không quảng bá giới hạn cứng 30 ngày hay ẩn danh tuyệt đối.
- Chưa xác minh: nhà cung cấp/model/tính năng cụ thể; hợp đồng/DPA; thị trường pháp lý; điều khoản xóa phía AI. Đây là đầu vào cần giải quyết trước tích hợp dữ liệu thật.
- Không phải phê duyệt G1 toàn dự án hoặc quyền triển khai mã nguồn.

## Nguồn chính thức

- [S1] Resume Worded, Privacy Policy: https://resumeworded.com/privacy — Use of Artificial Intelligence; Can I update or delete my personal data?; phần quyền riêng tư CV.
- [S2] Teal, Privacy Policy: https://www.tealhq.com/privacy-policy — mục 3 AI-Powered Features; mục 6 Data Retention; cập nhật trên trang 2026-08-12.
- [S3] Rezi, Terms & Privacy: https://www.rezi.ai/legal — Privacy Policy; How And Why We Use Information; Choices; Deleting your data.
- [S4] OpenAI, Data controls in the OpenAI platform: https://developers.openai.com/api/docs/guides/your-data — Data retention controls; Zero Data Retention; Storage requirements and retention controls per endpoint.
- [S5] Anthropic, How long do you store my organization’s data?: https://privacy.claude.com/en/articles/7996866-how-long-do-you-store-my-organization-s-data — Standard Retention Timeframe; Covered Models; Usage Policy Violations; Feedback Data.
- [S6] Anthropic, Is my data used for model training?: https://privacy.claude.com/en/articles/7996868-is-my-data-used-for-model-training — commercial products; Feedback.
