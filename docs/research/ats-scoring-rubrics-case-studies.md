# Nghiên cứu chuyên sâu: Cơ chế chấm điểm CV/ATS, phân tích trường hợp biên và đề xuất Rubric chuẩn hóa

- **Artifact ID:** RESEARCH-ATS-002
- **Tài liệu tham chiếu:** [Đặc tả nghiệp vụ BRIEF-CV-001 (cv-analysis-brief.md)](../workflow/specs/cv-analysis-brief.md), [Nghiên cứu lưu trữ dữ liệu AI (ai-cv-privacy-retention.md)](ai-cv-privacy-retention.md)
- **Mục tiêu nghiên cứu:** Điều tra thực chứng cơ chế tính điểm (Scoring Methodology), giải thuật đối chiếu (Matching Algorithms), tiêu chí đánh giá (Rubrics) của 5 nền tảng hàng đầu (Jobscan, Resume Worded, VMock, Teal, Rezi); phân tích 5 trường hợp kiểm thử biên (Edge Cases); và xây dựng bộ Rubric định lượng chi tiết cho 4 Trụ cột nhằm giải quyết dứt điểm câu hỏi mở **OQ01** trong Đặc tả nghiệp vụ BRIEF-CV-001.
- **Ngày hoàn thành:** 2026-09-22

---

## Tóm tắt điều hành (Executive Summary)

Thị trường công nghệ phân tích hồ sơ ứng viên (Resume Screening / ATS Optimization) hiện được chia làm hai trường phái rõ rệt:
1. **Trường phái mô phỏng ATS truyền thống và tuyển dụng theo từ khóa (Keyword-centric & Searchability):** Đại diện tiêu biểu là **Jobscan**, **Teal**, **Rezi**. Các hệ thống này tập trung mô phỏng cơ chế bóc tách (Parsing) và bộ lọc tìm kiếm (Search Filters) của các ATS doanh nghiệp phổ biến như Taleo (Oracle), Workday, iCIMS, Greenhouse, Bullhorn. Điểm số (Match Rate / Resume Score) phản ánh mức độ trùng khớp của từ khóa kỹ thuật (Hard Skills), chức danh (Job Title), cấp độ học vấn và tính an toàn về mặt định dạng (ATS-friendly layout).
2. **Trường phái đánh giá chất lượng hành văn và năng lực tác động (Content Quality, Competency & Impact):** Đại diện tiêu biểu là **Resume Worded** và **VMock**. Không chỉ dừng ở việc "có từ khóa hay không", các công cụ này đi sâu phân tích ngữ pháp, sức nặng của động từ hành động (Action Verbs), cấu trúc gạch đầu dòng (Bullet Points) theo mô hình STAR/CAR/XYZ, khả năng định lượng thành tích bằng con số (Metrics), và các năng lực cốt lõi (Core Competencies) theo tiêu chuẩn tuyển dụng của các trường đại học Top đầu và các tập đoàn Fortune 500.

Việc nghiên cứu đối chiếu này chứng minh rằng một bộ chấm điểm tối ưu cho sản phẩm phân tích CV không thể chỉ dựa thuần túy vào đếm từ khóa (Keyword Matching), cũng không thể chỉ đánh giá cảm tính về phong cách viết. Giải pháp tối ưu là một **Hệ thống Đánh giá Đa tầng (Multi-layered Evaluation Engine)** kết hợp: (1) Trích xuất thực thể và đối sánh từ điển/ngữ nghĩa; (2) Kiểm tra cấu trúc ngữ pháp và số liệu gạch đầu dòng; (3) Kiểm định an toàn định dạng tài liệu; và (4) Chuẩn hóa toán học linh hoạt theo trọng số nghiệp vụ (Weight Normalization).

---

## Phần 1: Điều tra cơ chế chấm điểm thực tế của 5 nền tảng hàng đầu

### 1. Jobscan (Jobscan.co)

Jobscan là nền tảng tiên phong trong việc mô phỏng thuật toán xếp hạng hồ sơ (Candidate Ranking & Keyword Filtering) của các ATS phổ biến tại các công ty Fortune 500 (Taleo, iCIMS, Jobvite, Greenhouse).

#### 1.1. Bản chất điểm Match Rate (%)
Jobscan không cung cấp một điểm số trừu tượng về "độ hay" của CV, mà tính toán **Tỷ lệ Phù hợp (Match Rate %)** so với một Mô tả công việc (Job Description - JD) cụ thể. Điểm số biến thiên từ 0% đến 100%. Theo khảo sát và công bố chính thức của Jobscan, ngưỡng **75% trở lên** là mục tiêu tối ưu giúp ứng viên vượt qua các bộ lọc tìm kiếm của nhà tuyển dụng.

Thuật toán Match Rate được xếp hạng ưu tiên theo 4 tầng dữ liệu cốt lõi:
1. **Kỹ năng chuyên môn cứng (Hard Skills):** Tỷ trọng cao nhất trong việc tính điểm. Bao gồm các ngôn ngữ lập trình, công cụ, phần mềm, phương pháp luận chuyên ngành (ví dụ: Python, AWS, PMP, Financial Modeling).
2. **Cấp độ học vấn (Education Level):** Được đưa vào tính điểm ưu tiên khi JD yêu cầu rõ ràng các bằng cấp nâng cao (như Thạc sĩ, Tiến sĩ, Cử nhân chuyên ngành).
3. **Chức danh công việc (Job Title):** Đóng vai trò cực kỳ quan trọng trong bộ lọc tìm kiếm. Thống kê của Jobscan chỉ ra rằng hồ sơ có chứa chính xác hoặc tương đương chức danh trong JD nhận được số lượng yêu cầu phỏng vấn cao gấp **10.6 lần** so với hồ sơ không chứa chức danh đó.
4. **Kỹ năng mềm (Soft Skills) và các từ khóa phụ:** Chiếm tỷ trọng thấp hơn, đóng vai trò bổ trợ.

*Lưu ý kỹ thuật quan trọng:* Số lượng từ (Word count) và việc có số liệu thành tích (Measurable results) được hiển thị trong báo cáo tư vấn của Jobscan nhưng **không được tính trực tiếp vào điểm Match Rate %**.

#### 1.2. Phân biệt Hard Skills vs Soft Skills
- **Hard Skills:** Hệ thống áp dụng cơ chế nhận diện thực thể kỹ thuật nghiêm ngặt. Hệ thống yêu cầu sự xuất hiện của kỹ năng cụ thể với tần suất tối ưu (thường là 1–3 lần tùy thuộc vào tần suất xuất hiện trong JD).
- **Soft Skills:** Được nhận diện qua từ điển kỹ năng mềm (Leadership, Communication, Problem Solving). Jobscan xếp các kỹ năng này ở nhóm cảnh báo thứ cấp, khuyến nghị ứng viên đưa vào phần kinh nghiệm thực tế thay vì chỉ liệt kê một danh sách trống rỗng.

#### 1.3. Điểm khả năng tìm kiếm (Searchability Score)
Searchability là thang đo kỹ thuật nhằm kiểm tra xem liệu ATS có "đọc" và "hiểu" được CV hay không:
- **Trích xuất thông tin liên hệ (Contact Information):** Kiểm tra sự hiện diện của Email, Số điện thoại, Địa điểm (Thành phố/Quốc gia), Link LinkedIn.
- **Tiêu đề phân mục chuẩn (Standard Section Headings):** Yêu cầu sử dụng các tiêu đề quy ước như *Work Experience*, *Education*, *Skills*. Các tiêu đề sáng tạo như *"My Career Journey"* bị gắn cờ cảnh báo vì khiến bộ phân tích (Parser) của ATS thất bại trong việc nhóm dữ liệu vào cơ sở dữ liệu.
- **Tương thích kiểu file:** Khuyến nghị chuẩn `.docx` hoặc `.pdf` được xuất trực tiếp từ trình soạn thảo văn bản, cảnh báo các file PDF rasterize (chỉ chứa ảnh).

#### 1.4. Tần suất từ khóa (Keyword Frequency) vs So khớp ngữ cảnh (Contextual Match)
- Jobscan so sánh trực tiếp số lần xuất hiện của từ khóa trong JD với số lần xuất hiện trong CV.
- Nếu JD nhắc lại một kỹ năng 4 lần (chứng tỏ đây là năng lực trọng tâm của vị trí), hệ thống khuyến nghị CV nên xuất hiện từ khóa đó từ 2 đến 3 lần. Xuất hiện 0 lần: Báo thiếu (Missing); Xuất hiện 1 lần: Đạt một phần (Partial); Xuất hiện đủ: Hoàn thành (Matched).
- Tuy nhiên, Jobscan vẫn hoạt động chủ yếu dựa trên cơ chế so khớp từ khóa chính xác (Exact match) hoặc biến thể hình thái rất gần. Nếu JD ghi *"Project Management"* mà CV chỉ ghi *"managed various projects"*, hệ thống có thể vẫn gắn cờ là thiếu từ khóa exact phrase.

#### 1.5. Cơ chế phát hiện và phạt nhồi từ khóa (Keyword Stuffing Penalty)
- **Định nghĩa Stuffing:** Tình trạng lặp lại từ khóa bất thường hoặc liệt kê danh sách kỹ năng dày đặc không có câu cú ngữ pháp nhằm đánh lừa bộ đếm.
- **Cơ chế xử phạt:** Jobscan thiết lập ngưỡng mật độ tối đa (Threshold). Nếu một từ khóa vượt quá ngưỡng mật độ tự nhiên trong văn bản (thường trên 4% - 5% tổng số từ của CV), hệ thống sẽ:
  1. Gắn cờ cảnh báo *"Keyword Stuffing"* màu đỏ.
  2. Ngừng cộng điểm Match Rate cho các lần xuất hiện vượt ngưỡng.
  3. Cảnh báo nguy cơ bị loại trực tiếp khi nhà tuyển dụng đọc bản hiển thị thực tế.

---

### 2. Resume Worded (ResumeWorded.com)

Resume Worded là nền tảng đánh giá CV hướng đến tiêu chuẩn của các nhà tuyển dụng hàng đầu tại Google, McKinsey, Goldman Sachs. Nền tảng này nổi tiếng với tính năng **Score My Resume**.

#### 2.1. Cấu trúc hệ thống Score My Resume
Resume Worded chấm điểm hồ sơ trên thang **0–100 điểm** dựa trên **hơn 30 tiêu chí kiểm tra độc lập (30+ Recruiter Checks)**. Điểm số tổng hợp không phải trung bình cộng giản đơn mà là hàm có trọng số, phân chia thành 4 trụ cột cốt lõi:
1. **Tác động (Impact):** Chiếm tỷ trọng lớn nhất (~40%). Đo lường sức mạnh của hành văn, việc sử dụng các động từ hành động mạnh (Strong Action Verbs), loại bỏ từ thừa (Filler Words) và đặc biệt là việc **đo lường kết quả bằng số liệu (Quantified Impact)**.
2. **Độ ngắn gọn & súc tích (Brevity):** Chiếm khoảng 20-25%. Đo lường độ dài toàn thể (Word count tối ưu: 400–800 từ cho 1 trang, 800–1200 từ cho 2 trang), độ dài trung bình của gạch đầu dòng (Bullet length: 1–2 dòng, tránh các gạch đầu dòng dài trên 3 dòng hoặc các dòng treo cụt ngủn dưới 5 từ).
3. **Phong cách & Bố cục (Style):** Chiếm khoảng 15-20%. Kiểm tra tính nhất quán trong định dạng ngày tháng, việc dùng dấu câu cuối gạch đầu dòng, đại từ nhân xưng ngôi thứ nhất (cấm dùng *I, me, my*), định dạng tiêu đề phân mục.
4. **Kỹ năng (Skills):** Chiếm khoảng 20%. Đánh giá sự cân bằng giữa Hard Skills và Soft Skills, mức độ phong phú của vốn từ ngữ chuyên môn, phát hiện việc lặp lại kỹ năng vô nghĩa.

*Phân vùng điểm Resume Worded:*
- **Dưới 60 điểm:** Bắt buộc viết lại (Poor).
- **60 – 84 điểm:** Đạt yêu cầu cơ bản nhưng thiếu sức cạnh tranh (Good).
- **85 – 100 điểm:** Đạt chuẩn ứng tuyển (Interview-ready). Người dùng đạt điểm 90+ có tỷ lệ gọi phỏng vấn cao gấp 3 lần.

#### 2.2. Công thức chấm điểm gạch đầu dòng (Bullet Point Scoring Formula)
Resume Worded giải mã từng gạch đầu dòng thành một cấu trúc 3 thành phần bắt buộc:
$$\text{Bullet Quality Score} = f(\text{Action Verb Strength}) + f(\text{Context/Task}) + f(\text{Measurable Metric})$$

Một gạch đầu dòng đạt điểm tuyệt đối khi:
1. **Bắt đầu bằng một Strong Action Verb** thuộc danh mục hành vi chủ động (Accomplishment-driven verb).
2. **Nêu rõ bối cảnh/nhiệm vụ** cụ thể, không dùng các cụm mơ hồ như *"worked on various projects"*.
3. **Chứa ít nhất một số liệu định lượng:** Con số cụ thể về tiền tệ ($), tỷ lệ phần trăm (%), thời gian tiết kiệm, quy mô đội ngũ, hoặc sự thay đổi trước-sau (ví dụ: *"Cut onboarding time from 6 weeks to 4"*).

#### 2.3. Cơ chế phạt lặp từ (Repetition Penalty)
- Resume Worded phân tích tần suất xuất hiện của các động từ đầu gạch đầu dòng.
- **Quy tắc phạt:** Nếu một động từ (ví dụ: *Managed*, *Led*, *Developed*) được lặp lại **từ 3 lần trở lên** trong toàn bộ CV, hệ thống sẽ trừ điểm trong mục Style & Word Choice.
- Thuật toán yêu cầu sự đa dạng hóa vốn từ vựng (Lexical Diversity) để chứng minh các khía cạnh năng lực khác nhau của ứng viên.

#### 2.4. Phạt động từ hành động yếu (Weak / Passive Verbs)
Hệ thống lưu trữ một danh mục "Blacklist" các từ yếu và từ trách nhiệm hành chính. Khi quét thấy các từ này ở đầu câu, hệ thống trừ điểm trực tiếp và gắn cờ cảnh báo:
- **Các cụm từ bị trừ điểm nặng:** *"Responsible for"*, *"Tasked with"*, *"Helped with"*, *"Assisted in"*, *"Worked on"*, *"Participated in"*.
- **Lý do trừ điểm:** Những cụm từ này chỉ mô tả bản mô tả công việc (Job Duty) được giao chứ không chứng minh được ứng viên đã thực sự đạt được thành quả gì (Achievement).
- **Cơ chế thay thế tự động:** Hệ thống đề xuất nhóm động từ thay thế tương ứng:
  - Thay vì *"Responsible for managing client accounts"* $\rightarrow$ *"Managed business relationships with 10 clients; renewed 95% of contracts year over year"*.
  - Thay vì *"Helped with marketing emails"* $\rightarrow$ *"Designed an email toolkit; saved 5+ hours per week"*.

---

### 3. VMock (VMock.com)

VMock là nền tảng trí tuệ nhân tạo độc quyền được cấp phép sử dụng chính thức tại các trung tâm hướng nghiệp của hàng trăm trường đại học danh tiếng thế giới như **Stanford, Harvard, Yale, MIT, Columbia, UC Berkeley, Oxford, Cambridge**.

#### 3.1. Hệ thống đánh giá 3 Trụ cột (3-Pillar Evaluation Framework)
VMock đánh giá CV trên thang điểm chuẩn hóa **100 điểm**, chia đều cho 3 trụ cột (khoảng 30–35 điểm mỗi trụ cột):

```
┌─────────────────────────────────────────────────────────────┐
│                 VMOCK RESUME SCORE (0-100)                  │
├──────────────────────────────┬──────────────────────────────┤
│ 1. Core Presentation (~33%)  │ 2. Competency (~33%)         │
│ - Formatting & Layout        │ - Analytical & Tech Skills   │
│ - Structure & Section Names  │ - Leadership & Initiative    │
│ - Bullet Density & Length    │ - Communication Skills       │
│ - Consistency (Fonts, Dates) │ - Teamwork & Collaboration   │
├──────────────────────────────┴──────────────────────────────┤
│ 3. Impact (~34%)                                            │
│ - Action-Oriented Verbs (Start of bullets)                  │
│ - Context, Scope & Specificity                              │
│ - Measurable Results / Metrics (%, $, counts, efficiency)   │
│ - Avoidance of Clichés & Subjective Claims                  │
└─────────────────────────────────────────────────────────────┘
```

Thang phân vùng theo màu của VMock:
- **Red Zone (0 – 59 điểm):** Dưới chuẩn. Bố cục lỗi, thiếu số liệu đo lường, dùng từ yếu.
- **Yellow Zone (60 – 85 điểm):** Đang hoàn thiện. Cấu trúc tốt nhưng chưa nhất quán, tỷ lệ gạch đầu dòng có số liệu dưới 50%.
- **Green Zone (86 – 100 điểm):** Sẵn sàng tuyển dụng (Benchmark standard). Tối thiểu 70% gạch đầu dòng có số liệu định lượng, từ vựng mạnh, cấu trúc hoàn hảo.

#### 3.2. Tiêu chuẩn Core Presentation
- **Độ dài (Length):** 1 trang tuyệt đối cho sinh viên và người có dưới 5 năm kinh nghiệm. Dưới 1 trang (bị chừa khoảng trắng > 20% cuối trang) hoặc nhảy sang trang 2 vài dòng bị trừ điểm rất nặng.
- **Mật độ gạch đầu dòng (Bullet Density):** Mỗi vị trí công việc cần từ 3 đến 6 gạch đầu dòng. Dưới 2 gạch đầu dòng bị coi là thiếu thông tin; trên 7 gạch đầu dòng bị coi là lan man.
- **Quy cách dòng (Single-line vs Multi-line):** VMock tối ưu hóa các gạch đầu dòng có độ dài từ **1.5 đến 2 dòng**. Tránh tối đa hiện tượng "orphan words" (gạch đầu dòng chỉ có 1–2 từ ở dòng tiếp theo gây lãng phí không gian).

#### 3.3. Tiêu chuẩn Competency (5 Năng lực toàn cầu)
VMock quét và phân tích hồ sơ dựa trên 5 năng lực cốt lõi mà mọi nhà tuyển dụng tìm kiếm:
1. *Analytical & Problem Solving* (Năng lực phân tích và giải quyết vấn đề)
2. *Leadership & Initiative* (Khả năng lãnh đạo và chủ động)
3. *Communication & Teamwork* (Giao tiếp và làm việc nhóm)
4. *Organizational & Planning* (Tổ chức và lập kế hoạch)
5. *Technical / Functional Expertise* (Chuyên môn kỹ thuật)
Hệ thống chấm điểm dựa trên sự cân bằng: một CV chỉ toàn từ khóa kỹ thuật mà không có bằng chứng về kỹ năng lãnh đạo hay giao tiếp sẽ bị khống chế trần điểm Competency.

#### 3.4. Bóc tách cấu trúc gạch đầu dòng: STAR, CAR và XYZ Formula
VMock áp dụng các mô hình hành vi để kiểm tra từng câu văn:
- **Khung CAR (Context - Action - Result) / STAR (Situation - Task - Action - Result):** Gạch đầu dòng phải thể hiện: Hoàn cảnh/Nhiệm vụ là gì? Ứng viên đã có hành động cụ thể nào? Kết quả đạt được ra sao?
- **Khung XYZ Formula (Google Inc.):**
  $$\text{"Accomplished [X] as measured by [Y], by doing [Z]"}$$
  *(Hoàn thành mục tiêu X, được đo lường bằng con số Y, thông qua hành động Z).*
- VMock kiểm tra tự động vị trí của số liệu: các gạch đầu dòng đặt số liệu kết quả ở đầu câu hoặc kết nối bằng mệnh đề chỉ hệ quả (*"resulting in..."*, *"which led to..."*, *"generating $X"*) nhận được điểm Impact tối đa.

---

### 4. Teal (TealHQ.com) & Rezi (Rezi.ai)

Cả Teal và Rezi đại diện cho thế hệ công cụ tối ưu hóa hồ sơ hiện đại (Modern ATS Builders & Checkers), tập trung vào khả năng tạo lập CV chuẩn chỉnh và chấm điểm độ sẵn sàng ứng tuyển theo các tiêu chuẩn kỹ thuật số.

#### 4.1. Teal: Matching Mode & Resume Score
- **Matching Mode:** Teal phân tích văn bản JD thành các cụm từ khóa danh từ (Noun Chunks) và kỹ năng (Skills). Khi ứng viên kích hoạt chế độ so khớp, Teal tính toán tỷ lệ phần trăm các từ khóa trong JD đã xuất hiện trong CV.
- **Tiêu chí kiểm tra bố cục và độ dài:**
  - Teal khuyến nghị số từ chính xác dựa trên cấp bậc nghề nghiệp: Early-career (350–500 từ), Mid-level (500–800 từ), Senior (800–1200 từ).
  - Kiểm tra các trường thông tin danh bạ bắt buộc: Name, Phone, Email, Location (City/State), LinkedIn URL. Nếu thiếu Location, Teal cảnh báo rủi ro bị ATS lọc tự động theo vùng địa lý.
- **Kiểm tra định dạng:** Teal bắt buộc sử dụng định dạng cột đơn (Single-column layout) và kiểm soát kích thước lề tiêu chuẩn (0.5 inch đến 1 inch).

#### 4.2. Rezi: Rezi Score & 23 ATS Checkpoints
Rezi phát triển thuật toán **Rezi Score (0–100)** dựa trên 23 chốt kiểm soát ATS (Checkpoints) phân chia thành 4 nhóm chức năng:
1. **Content (Nội dung):** Kiểm tra lỗi chính tả, ngữ pháp, các từ sáo rỗng (buzzwords như *hardworking, self-starter, team-player*), độ dài của phần tóm tắt (Professional Summary: khuyến nghị 2–4 câu), và việc định lượng gạch đầu dòng.
2. **Format (Định dạng):** Bắt buộc loại bỏ bảng biểu (Tables), hộp văn bản (Text boxes), thanh đồ họa kỹ năng (Skill rating bars), biểu tượng cảm xúc hoặc hình ảnh.
3. **Optimization (Tối ưu hóa từ khóa):** So sánh mức độ bao phủ của kỹ năng chuyên môn giữa CV và JD.
4. **Best Practices (Thực hành tuyển dụng chuẩn):** Kiểm tra thứ tự các phân mục (Section Order: Summary $\rightarrow$ Experience $\rightarrow$ Education $\rightarrow$ Skills cho người đi làm; Education đưa lên đầu cho sinh viên mới tốt nghiệp).

---

### 1.5. Bảng ma trận so sánh tổng hợp 5 nền tảng

| Tiêu chí | Jobscan | Resume Worded | VMock | Teal | Rezi |
|---|---|---|---|---|---|
| **Thang điểm** | 0 – 100% (Match Rate) | 0 – 100 (Score My Resume) | 0 – 100 (VMock Score) | 0 – 100% (Keyword Match) | 0 – 100 (Rezi Score) |
| **Bản chất điểm** | Độ tương thích từ khóa với một JD | Điểm chất lượng hành văn theo chuẩn Recruiter | Điểm năng lực & tác động theo chuẩn Đại học Top đầu | Độ bao phủ từ khóa & độ hoàn thiện hồ sơ | Điểm tuân thủ 23 tiêu chí kỹ thuật ATS |
| **Yêu cầu JD?** | **Bắt buộc** để tính Match Rate | Không bắt buộc (chấm tổng quát hoặc theo JD) | Không bắt buộc (chấm theo chuẩn ngành/benchmark) | Tùy chọn (Matching Mode cần JD) | Tùy chọn (Targeting Mode cần JD) |
| **Trọng số Kỹ năng / Từ khóa** | Cực cao (Hard skills chiếm ưu tiên số 1) | Trung bình (~20%) | Trung bình (~33% Competency) | Cao (~50% trong chế độ match) | Cao (~35% Optimization) |
| **Trọng số Đo lường / Số liệu** | Không tính vào Match Rate (chỉ cảnh báo) | Cực cao (~40% nằm trong Impact) | Cực cao (~34% nằm trong Impact) | Trung bình (cảnh báo trong từng bullet) | Cao (nằm trong mục Content check) |
| **Cơ chế kiểm tra gạch đầu dòng** | Đếm từ khóa chứa trong bullet | Chấm cấu trúc: Verb mạnh + Ngữ cảnh + Con số | Chấm cấu trúc STAR/CAR/XYZ | Đề xuất bổ sung số liệu | Kiểm tra độ dài & động từ mở đầu |
| **Phạt lặp từ & Động từ yếu** | Không phạt lặp từ | Phạt nặng từ lặp $\ge 3$ lần, phạt weak verbs | Phạt động từ yếu, yêu cầu đa dạng vốn từ | Cảnh báo động từ yếu | Cảnh báo buzzwords & weak verbs |
| **Kiểm tra định dạng / Parser** | Kiểm tra sâu Searchability (layout, headings) | Kiểm tra cơ bản (file type, single-column) | Kiểm tra rất sâu (margin, font, line length) | Kiểm soát chặt chẽ qua template nội bộ | 23 Checkpoints định dạng & bố cục |
| **Phạt Keyword Stuffing** | Có (ngưỡng mật độ >4-5%, dừng tính điểm) | Không áp dụng trực tiếp | Phạt nếu câu văn mất cấu trúc ngữ pháp | Không áp dụng trực tiếp | Cảnh báo mật độ từ khóa bất thường |

---

## Phần 2: Tổng hợp 5 Case Study / Edge Cases kinh điển và giải pháp kỹ thuật

### Case 1: Từ đồng nghĩa & Viết tắt (Synonyms & Acronyms)
**Tình huống thực tế:**
- JD yêu cầu: *"Amazon Web Services"*, CV ứng viên ghi: *"AWS"*.
- JD yêu cầu: *"JavaScript"*, CV ứng viên ghi: *"JS"*.
- JD yêu cầu: *"ReactJS"*, CV ứng viên ghi: *"React"*.
- JD yêu cầu: *"Search Engine Optimization"*, CV ứng viên ghi: *"SEO"*.

**Phân tích hành vi hệ thống:**
- *Hệ thống ATS cũ (Legacy Regex / Exact-string Matching như Taleo phiên bản cũ):* Tìm kiếm chuỗi ký tự chính xác. Nếu nhà tuyển dụng gõ *"Amazon Web Services"*, hồ sơ chỉ ghi *"AWS"* nhận điểm tương thích bằng 0 đối với tiêu chí này và bị loại khỏi kết quả lọc.
- *Hệ thống ATS hiện đại (Semantic Search, Knowledge Graph như Workday Skills Cloud, Textkernel/Sovren):* Sử dụng mạng lưới phân loại kỹ năng (Skill Taxonomy như ESCO, Lightcast/O*NET) và mô hình ngôn ngữ ngữ nghĩa (Vector Embeddings) để quy đổi *"AWS"* và *"Amazon Web Services"* về cùng một mã định danh thực thể (Entity ID: `SKILL_AWS_001`).
- *Cách Jobscan, Resume Worded, Rezi khuyến nghị:* Dù các bộ lọc hiện đại có khả năng hiểu từ đồng nghĩa, một tỷ lệ lớn nhà tuyển dụng con người vẫn gõ tìm kiếm từ khóa chuỗi chính xác trên thanh công cụ ATS.

**Giải pháp kỹ thuật cho hệ thống phân tích CV:**
1. **Áp dụng quy tắc "Ghi kép chính quy" (Dual Inscription Rule):** Khuyến nghị ứng viên luôn viết cả dạng đầy đủ và viết tắt trong lần xuất hiện đầu tiên: `Amazon Web Services (AWS)`, `Search Engine Optimization (SEO)`.
2. **Xây dựng Bảng từ điển từ đồng nghĩa chuẩn hóa (Canonical Synonym Mapping Table):**
   - Cấp 1 (Exact Match): Trùng 100% chuỗi ký tự $\rightarrow$ 100% điểm kỹ năng.
   - Cấp 2 (Canonical Alias / Acronym): Khớp từ điển từ đồng nghĩa được phê duyệt (AWS $\leftrightarrow$ Amazon Web Services; JS $\leftrightarrow$ JavaScript; React $\leftrightarrow$ ReactJS $\leftrightarrow$ React.js) $\rightarrow$ 95% điểm kỹ năng.
   - Cấp 3 (Contextual / Related Skill): Kỹ năng liên quan trong cùng họ công nghệ (ví dụ: JD yêu cầu Docker, CV có Kubernetes; JD yêu cầu MySQL, CV có PostgreSQL) $\rightarrow$ 60% – 70% điểm liên quan (hệ thống ghi chú rõ: *Kỹ năng tương đương/liên quan*).

---

### Case 2: Nhồi từ khóa & Text ẩn (Keyword Stuffing, White Text & Hidden Layers)
**Tình huống thực tế:**
- Ứng viên sao chép toàn bộ nội dung của JD, dán vào phần chân trang (Footer) hoặc một khối văn bản trống, chỉnh kích thước chữ về 1pt và đổi màu chữ sang màu trắng (White Text trên nền trắng), hoặc đặt văn bản nằm dưới một lớp ảnh (Transparent/Layered Text).
- Mục đích: Đánh lừa bộ đếm từ khóa của hệ thống để đạt điểm tương thích tuyệt đối 100% mà mắt thường nhà tuyển dụng không nhìn thấy.

**Phân tích kỹ thuật quá trình xử lý của ATS:**
- *Bóc tách văn bản thô (Text Stream Extraction):* Các công cụ giải mã PDF/Word (như PDFBox, Tika, pdfminer, Calamine) trích xuất chuỗi ký tự từ luồng văn bản (Text Streams) mà **hoàn toàn bỏ qua thuộc tính màu sắc (`color: #ffffff`) hoặc kích thước font (`font-size: 1pt`)**. Toàn bộ khối văn bản ẩn sẽ hiển thị nguyên dạng văn bản thô trong cơ sở dữ liệu của ATS.
- *Phát hiện bất thường ngữ pháp (Grammar & Syntactic Disruption):* Khi trích xuất ra văn bản thô, đoạn văn nhồi từ khóa xuất hiện dưới dạng một chuỗi hàng trăm danh từ vô nghĩa, không có chủ vị, phá vỡ cấu trúc cây phân tích cú pháp (Syntax Tree).
- *Đối chiếu giao diện hiển thị (Visual Inspection):* Nhà tuyển dụng khi mở hồ sơ trên hệ thống ATS luôn xem bản PDF render song song với bản text trích xuất. Nếu họ thấy hệ thống báo Match 100% nhưng trong văn bản không thấy từ khóa đâu, hồ sơ sẽ bị đánh dấu gian lận (Fraudulent).

**Giải pháp kỹ thuật cho hệ thống phân tích CV:**
1. **Kiểm tra mật độ từ khóa (Keyword Density Check):** Thiết lập ngưỡng mật độ tối đa cho một từ/cụm từ khóa. Nếu một từ khóa xuất hiện với mật độ $> 3.5\%$ trên tổng số từ của CV, kích hoạt cảnh báo Stuffing.
2. **Kiểm tra tính mạch lạc ngữ pháp (POS Tagging & Syntax Check):** Sử dụng mô hình nhận diện từ loại (Part-of-Speech Tagging). Nếu phát hiện chuỗi danh từ kỹ thuật liên tiếp không có động từ/giới từ liên kết dài quá 10 từ $\rightarrow$ Định danh là khối nhồi từ khóa.
3. **Cơ chế phạt:**
   - Hủy bỏ toàn bộ điểm số đối với các lần xuất hiện vượt quá tần suất hợp lý (tối đa chỉ tính điểm cho 3 lần xuất hiện đầu tiên có ngữ cảnh).
   - Trừ trực tiếp **25% điểm Trụ cột Kỹ năng** nếu phát hiện dấu hiệu cố tình liệt kê từ khóa không ngữ cảnh.
   - Gắn cờ cảnh báo rủi ro cao: *"Cảnh báo: Hồ sơ có dấu hiệu lặp từ bất thường hoặc nhồi từ khóa dạng khối."*

---

### Case 3: Lệch số năm kinh nghiệm (Experience Discrepancy: Underqualified vs Overqualified)
**Tình huống thực tế:**
- **Tình huống A (Thiếu năm kinh nghiệm - Underqualified):** JD yêu cầu tối thiểu 5 năm kinh nghiệm; CV của ứng viên chỉ có 2 năm kinh nghiệm thực tế.
- **Tình huống B (Thừa năm kinh nghiệm - Overqualified):** JD tuyển vị trí Junior/Mid-level yêu cầu 2–3 năm kinh nghiệm; CV của ứng viên có 12 năm kinh nghiệm làm Quản lý/Trưởng nhóm.

**Phân tích hành vi hệ thống và rủi ro tuyển dụng:**
- *Đối với Tình huống A (Thiếu năm):*
  - Nếu áp dụng quy tắc suy giảm tuyến tính (Linear Deduction, ví dụ: 2 năm / 5 năm = 40% điểm), hệ thống có thể trừ điểm quá nặng, loại oan những ứng viên tài năng có tốc độ thăng tiến nhanh và năng lực vượt trội.
  - Tuy nhiên, trong nhiều ATS doanh nghiệp, số năm kinh nghiệm nằm trong câu hỏi loại trừ (Knockout Questions).
- *Đối với Tình huống B (Thừa năm):*
  - Về mặt toán học, ứng viên đáp ứng 100% (thậm chí 200%) yêu cầu về thời gian. Nếu cho điểm 100% mà không có cảnh báo, ứng viên sẽ nghĩ hồ sơ của mình rất phù hợp.
  - Nhưng trong thực tế tuyển dụng, ứng viên thừa kinh nghiệm thường bị từ chối vì rủi ro chi phí lương cao, dễ chán việc (flight risk), hoặc không phù hợp với văn hóa đội ngũ trẻ.

**Giải pháp kỹ thuật cho hệ thống phân tích CV:**
1. **Đường cong suy giảm phi tuyến tính (Asymmetric Piecewise Curve) cho ứng viên thiếu năm kinh nghiệm:**
   - Đạt $\ge 100\%$ số năm yêu cầu: **100% điểm thâm niên**.
   - Đạt từ $75\% - 99\%$ (ví dụ: có 4 năm / yêu cầu 5 năm): **85% điểm thâm niên** (Trừ nhẹ).
   - Đạt từ $50\% - 74\%$ (ví dụ: có 2.5–3 năm / yêu cầu 5 năm): **65% điểm thâm niên** (Mức độ vừa phải).
   - Đạt từ $25\% - 49\%$ (ví dụ: có 1.5–2 năm / yêu cầu 5 năm): **40% điểm thâm niên**.
   - Dưới $25\%$ số năm yêu cầu: **15% điểm thâm niên**.
   - *Kết hợp BR18:* Khi tỷ lệ đạt dưới 60%, hệ thống tự động gắn nhãn cảnh báo độc lập: *"Yêu cầu bắt buộc chưa có đủ bằng chứng về số năm kinh nghiệm tối thiểu."*
2. **Xử lý ứng viên thừa kinh nghiệm (Overqualification Handling):**
   - Về mặt điểm kỹ thuật: Giữ nguyên điểm số tối đa (100% điểm thâm niên), không trừ điểm toán học.
   - Về mặt nghiệp vụ tư vấn: Gắn cờ thông tin (Informational Tag): *"Cảnh báo lệch cấp bậc: Bạn có 12 năm kinh nghiệm cho vị trí yêu cầu 3 năm. Hãy lưu ý điều chỉnh thư ứng tuyển và tóm tắt nghề nghiệp để giải thích rõ lý do ứng tuyển vị trí này."*

---

### Case 4: Lỗi định dạng Parser (Tables, Columns, Text Boxes, Dates, Fonts, Headers/Footers)
**Tình huống thực tế:**
- Ứng viên thiết kế CV đồ họa trên Canva, Photoshop, hoặc dùng mẫu Word có chia 2–3 cột dọc (Multi-column), tạo bảng biểu (Tables) để căn lề, chèn Text Box nổi để ghi kỹ năng.
- Ứng viên sử dụng định dạng ngày kiểu `04/05/2022` (nhập nhằng giữa 04 tháng 5 và 05 tháng 4), hoặc dùng font chữ tải từ trên mạng không nhúng (embedded) mã Unicode.

**Cơ chế phát sinh lỗi phân tích (Parsing Failures):**
1. **Đọc đan xen giữa các cột (Horizontal Interleaving):**
   - Cấu trúc PDF không lưu văn bản theo "cột", mà lưu tọa độ các ký tự $(x, y)$ trên trang giấy. Hầu hết các thư viện trích xuất văn bản dòng lệnh (như `pdftotext`, các parser mã nguồn mở cũ) đọc dữ liệu quét từ trái qua phải theo trục hoành $y$.
   - Khi gặp CV 2 cột: Cột trái (Kinh nghiệm: *"Senior Developer at ABC Company"*), Cột phải (Kỹ năng: *"React, Node.js"*). Parser sẽ đọc thành: *"Senior Developer at React, Node.js ABC Company"*. Kết quả là toàn bộ câu văn bị xáo trộn, ATS không thể nhận diện được công ty, chức danh hay nội dung công việc.
2. **Mất dữ liệu trong Text Box & Table:**
   - Các hộp văn bản trôi nổi (Floating Shapes/Text Boxes) trong file `.docx` được lưu trữ trong luồng XML đồ họa riêng biệt (`word/drawing/`). Parser thông thường chỉ đọc luồng văn bản chính (`word/document.xml`), dẫn đến việc toàn bộ nội dung trong Text Box biến mất hoàn toàn.
3. **Mất thông tin liên hệ trong Header/Footer:**
   - Để tránh trùng lặp thông tin đầu trang và số trang khi bóc tách hồ sơ nhiều trang, rất nhiều ATS tự động cắt bỏ hoàn toàn vùng lề trên (Top margin $< 0.75\text{ inch}$) và lề dưới (Bottom margin). Nếu ứng viên để Email và Số điện thoại trong Header của Word, ATS sẽ trích xuất hồ sơ không có thông tin liên hệ.
4. **Lỗi mã hóa Font (Font Ligatures & CMap Issues):**
   - Một số font nghệ thuật gộp các chữ cái liền nhau thành một ký tự liên kết (Ligature), ví dụ `fi`, `fl`, `ff`. Khi xuất sang PDF thiếu bảng mã ToUnicode CMap, chữ *"Finance"* bị đọc thành `"ance"` hoặc ký tự lạ `\uFB01nance`, khiến bộ lọc từ khóa thất bại trong việc tìm từ *"Finance"*.

**Giải pháp kỹ thuật cho hệ thống phân tích CV:**
1. **Kiểm định khả năng trích xuất (Text Extractability Audit):** Đọc file bằng bộ parser và kiểm tra tỷ lệ từ có nghĩa (Dictionary Words Ratio). Nếu $>15\%$ từ trích xuất bị dính chùm hoặc chứa ký tự đặc biệt vô nghĩa $\rightarrow$ Phạt điểm Trụ cột Định dạng và yêu cầu kiểm tra nội dung xác nhận (theo BR23).
2. **Quy tắc phân tích cấu trúc:**
   - Cảnh báo bắt buộc: Gắn cờ khi phát hiện cấu trúc bảng phức tạp ($> 2$ hàng/cột có viền ẩn), hộp văn bản thả nổi, hoặc thông tin liên hệ nằm trong Header.
   - Hướng dẫn chuyển đổi: Khuyến nghị ứng viên sử dụng mẫu chuẩn 1 cột (Single-column layout), căn lề bằng Tab/Indentation thay vì chèn Table.
3. **Chuẩn hóa định dạng ngày tháng:** Hướng dẫn sử dụng định dạng ngày có chữ rõ ràng (ví dụ: `MM/YYYY` như `05/2021` hoặc `May 2021 – Present`), loại bỏ định dạng `DD/MM/YYYY` nhập nhằng.

---

### Case 5: Thành tích không đo lường được (Unquantified Achievements & Missing Metrics)
**Tình huống thực tế:**
- Ứng viên viết CV với các gạch đầu dòng chỉ toàn mô tả nhiệm vụ thông thường (Task-based bullets):
  - *"Responsible for managing the sales team."*
  - *"Worked on developing company website."*
  - *"Helped customer support team handle complaints."*
- Toàn bộ hồ sơ không xuất hiện bất kỳ một con số, dấu phần trăm (`%`), ký hiệu tiền tệ (`$`, `VND`), hoặc quy mô đo lường nào.

**Phân tích đánh giá tuyển dụng:**
- Hồ sơ không có số liệu chứng minh thành tích là nguyên nhân số 1 khiến ứng viên bị loại ở vòng thẩm định của nhà tuyển dụng con người (Human Recruiter Review).
- Các hệ thống tiên tiến (Resume Worded, VMock) coi số liệu là thước đo duy nhất để xác định tính xác thực và quy mô tác động của ứng viên.

**Giải pháp kỹ thuật cho hệ thống phân tích CV:**
1. **Bộ dò tìm số liệu đa dạng (Multi-pattern Metric Detection Engine):**
   Sử dụng biểu thức chính quy (Regex) kết hợp nhận diện thực thể số lượng (Numeric NER) để quét các gạch đầu dòng:
   - Phần trăm: `\d+(\.\d+)?%` hoặc các cụm từ *"percent"*, *"percentage"*.
   - Tiền tệ: `(\$|€|£|¥|VND|USD)\s?\d+` hoặc `\d+\s?(triệu|tỷ|k|M|B)`.
   - Cải thiện trước/sau: `from \d+ to \d+`, `reduced by \d+`, `increased by \d+`.
   - Quy mô / Khối lượng: `\d+\+? (team members|clients|users|contracts|projects|features|servers)`.
   - Tần suất / Thời gian: `\d+ (hours|days|weeks|months) per (week|month)`.
2. **Công thức tính Tỷ lệ Gạch đầu dòng Định lượng (Quantified Bullet Ratio - QBR):**
   $$\text{QBR} = \frac{\text{Số gạch đầu dòng chứa ít nhất 1 số liệu hợp lệ}}{\text{Tổng số gạch đầu dòng trong phần Kinh nghiệm / Dự án}}$$
3. **Thang điểm trừ bậc thang cho Trụ cột Kinh nghiệm:**
   - $\text{QBR} \ge 60\%$: Đạt chuẩn xuất sắc $\rightarrow$ **100% điểm đo lường thành tích**.
   - $40\% \le \text{QBR} < 60\%$: Khá $\rightarrow$ **80% điểm đo lường thành tích**.
   - $20\% \le \text{QBR} < 40\%$: Trung bình $\rightarrow$ **50% điểm đo lường thành tích**.
   - $\text{QBR} < 20\%$: Rất kém $\rightarrow$ **20% điểm đo lường thành tích**, gắn cờ cảnh báo: *"Hồ sơ thiếu nghiêm trọng các số liệu minh chứng kết quả (metrics/KPIs)."*
4. **Tuân thủ nguyên tắc trung thực (BR06):**
   - Khi viết lại hoặc đưa ra đề xuất cải thiện, hệ thống **tuyệt đối không tự ý bịa số liệu** (ví dụ: không tự ý chèn *"tăng doanh số 25%"* vào câu của ứng viên).
   - Hệ thống phải chèn khung câu hỏi định hướng (Placeholder Prompts) để ứng viên tự điền: *"Hãy bổ sung số liệu: Bạn đã quản lý đội ngũ bao nhiêu người? Doanh số hoặc hiệu suất đạt được là bao nhiêu?"*

---

## Phần 3: Đề xuất Bộ Rubric Chấm Điểm Chi Tiết cho 4 Trụ Cột (Giải quyết dứt điểm OQ01)

Căn cứ vào Đặc tả nghiệp vụ `BRIEF-CV-001` (Quy tắc BR01, BR05, BR18, BR35), mục này thiết kế chi tiết bộ quy chuẩn toán học và tiêu chí đánh giá sẵn sàng áp dụng vào mã nguồn.

### 3.1. Mô hình toán học tổng quát và cơ chế chuẩn hóa trọng số (Weight Normalization)

Điểm tổng hợp $S$ (Overall Score) là hàm chuẩn hóa theo các tiêu chí có căn cứ áp dụng:
$$S = \frac{\sum_{i \in \mathcal{A}} w_i \cdot p_i}{\sum_{i \in \mathcal{A}} w_i}$$

Trong đó:
- $\mathcal{A}$ là tập hợp các trụ cột có đủ căn cứ áp dụng.
- $p_i \in [0, 100]$ là điểm thành phần của trụ cột $i$.
- $w_i$ là trọng số cơ sở của trụ cột $i$.

**Bảng trọng số cơ sở theo BR01:**
- **Trụ cột Kỹ năng & Từ khóa ($K$):** $w_K = 0.40$ (40%)
- **Trụ cột Kinh nghiệm & Dự án ($E$):** $w_E = 0.30$ (30%)
- **Trụ cột Định dạng & Trích xuất ($F$):** $w_F = 0.15$ (15%)
- **Trụ cột Học vấn & Chứng chỉ ($H$):** $w_H = 0.15$ (15%)

**Quy tắc kích hoạt "Không áp dụng" (Not Applicable - N/A) và chuẩn hóa trọng số:**
1. **Trường hợp đầy đủ tiêu chí:**
   $$S = 0.40 \cdot p_K + 0.30 \cdot p_E + 0.15 \cdot p_F + 0.15 \cdot p_H$$
2. **Trường hợp JD không yêu cầu học vấn ($H \notin \mathcal{A}$ theo BR05):**
   - Tiêu chí Học vấn chuyển sang trạng thái `N/A` (không gán điểm 0).
   - Tổng trọng số các tiêu chí còn lại: $W_{\text{active}} = 40 + 30 + 15 = 85$.
   - Trọng số chuẩn hóa hiển thị:
     - Kỹ năng: $w'_K = 40 / 85 \approx 47.0588\%$
     - Kinh nghiệm: $w'_E = 30 / 85 \approx 35.2941\%$
     - Định dạng: $w'_F = 15 / 85 \approx 17.6471\%$
   - Công thức tính:
     $$S = \frac{0.40 \cdot p_K + 0.30 \cdot p_E + 0.15 \cdot p_F}{0.85}$$
3. **Trường hợp JD không yêu cầu số năm kinh nghiệm cụ thể:**
   - Trụ cột Kinh nghiệm vẫn áp dụng nhưng phần đánh giá thâm niên chuyển sang đánh giá chất lượng dự án và năng lực liên quan.

---

### 3.2. Tiêu chuẩn xác định "Mô tả công việc (JD) đủ căn cứ để chấm điểm" (BR35 / OQ01)

Hệ thống bắt buộc phải kiểm tra tính hợp lệ của JD trước khi tiến hành đối chiếu chấm điểm nhằm bảo vệ quyền lợi người dùng và không trừ credit/lượt cơ bản sai quy định.

**Quy tắc định lượng: JD được coi là "Đủ căn cứ" (Sufficient JD) khi và chỉ khi thỏa mãn đồng thời 4 điều kiện sau:**
1. **Độ dài tối thiểu (Length Threshold):** Toàn văn JD phải có ít nhất **50 từ** (hoặc tối thiểu 300 ký tự có nghĩa). Các chuỗi nhập cụt ngủn như *"Tuyển lập trình viên React"* không đủ căn cứ.
2. **Chức danh công việc xác định (Job Title Detectability):** Chứa chức danh hoặc vai trò có thể nhận diện được (ví dụ: *Frontend Developer, Kế toán trưởng, Sales Executive*).
3. **Yêu cầu kỹ năng hoặc trách nhiệm tối thiểu (Skill/Duty Density):** Hệ thống trích xuất được tối thiểu **3 kỹ năng cụ thể** hoặc ít nhất **2 gạch đầu dòng mô tả trách nhiệm công việc**.
4. **Ngôn ngữ hỗ trợ (Supported Language):** Soạn thảo bằng Tiếng Việt, Tiếng Anh hoặc song ngữ Việt–Anh (BR15).

**Quy trình xử lý khi JD KHÔNG đủ căn cứ:**
- Dừng quy trình đối chiếu, **không trừ lượt cơ bản và không thu credit** (BR35).
- Hiển thị thông báo chi tiết: *"Mô tả tuyển dụng hiện quá ngắn hoặc thiếu thông tin yêu cầu cụ thể (yêu cầu tối thiểu 50 từ và ít nhất 3 kỹ năng/trách nhiệm). Vui lòng bổ sung thêm thông tin JD."*
- Cho phép người dùng lựa chọn: (A) Bổ sung nội dung JD để quét tiếp; hoặc (B) Chuyển sang chế độ *"Phân tích CV tổng quát không có JD"* (BR12/BR25).

---

### 3.3. Trụ cột 1: Kỹ năng & Từ khóa ($p_K$ — Trọng số 40%)

Trụ cột Kỹ năng đo lường mức độ bao phủ và tương thích của năng lực chuyên môn giữa CV và JD.

#### 3.3.1. Phân loại kỹ năng trong JD
Hệ thống tự động phân loại các kỹ năng trích xuất từ JD thành hai nhóm:
- **Kỹ năng Bắt buộc (Must-have Skills):** Chiếm **70% trọng số của Trụ cột Kỹ năng**. Là các kỹ năng xuất hiện dưới các mệnh đề bắt buộc (*"Must have"*, *"Yêu cầu"*, *"Bắt buộc"*, *"Tối thiểu"*, hoặc được nhắc lại $\ge 2$ lần trong JD).
- **Kỹ năng Ưu tiên / Tùy chọn (Nice-to-have / Preferred Skills):** Chiếm **30% trọng số của Trụ cột Kỹ năng**. Là các kỹ năng xuất hiện dưới các mệnh đề ưu tiên (*"Ưu tiên ứng viên biết"*, *"Plus"*, *"Preferred"*, *"Nice to have"*, *"Có kinh nghiệm với... là lợi thế"*).

#### 3.3.2. Bảng điểm đối chiếu từng kỹ năng
Mỗi kỹ năng trong JD được gán điểm đối soát với CV theo 4 mức độ:

| Mức độ khớp | Tiêu chí nhận diện | Điểm thành phần kỹ năng |
|---|---|---|
| **Exact Match** | Trùng khớp chuỗi chính xác (Exact string) hoặc dạng chuẩn hóa hình thái (Lemmatization/Stemming). | 100% |
| **Synonym Match** | Trùng khớp thông qua từ điển từ đồng nghĩa chuẩn (Acronym, Canonical Alias, ví dụ: AWS $\leftrightarrow$ Amazon Web Services). | 95% |
| **Contextual Match** | Kỹ năng liên quan trong cùng họ công nghệ/nghiệp vụ (ví dụ: JD yêu cầu PostgreSQL, CV có MySQL/Oracle). | 60% |
| **Missing** | Hoàn toàn không có bằng chứng xuất hiện trong CV. | 0% |

#### 3.3.3. Công thức tính điểm Trụ cột Kỹ năng ($p_K$)
$$p_K = 0.70 \cdot \left( \frac{\sum_{i=1}^{N_{\text{must}}} \text{Score}(S_i)}{N_{\text{must}}} \right) + 0.30 \cdot \left( \frac{\sum_{j=1}^{N_{\text{nice}}} \text{Score}(S_j)}{N_{\text{nice}}} \right) - \text{Penalty}_{\text{stuffing}}$$

*Ngoại lệ:* Nếu JD không có kỹ năng tùy chọn ($N_{\text{nice}} = 0$), toàn bộ 100% điểm kỹ năng được tính cho nhóm kỹ năng bắt buộc.

*Cảnh báo bắt buộc (BR18):* Bất kỳ kỹ năng bắt buộc (Must-have) nào bị chấm điểm 0 (Missing) sẽ được hiển thị trong bảng cảnh báo độc lập: *"CV chưa thể hiện kỹ năng bắt buộc: [Tên kỹ năng]"*.

---

### 3.4. Trụ cột 2: Kinh nghiệm & Dự án ($p_E$ — Trọng số 30%)

Trụ cột Kinh nghiệm đánh giá thâm niên, sự tương thích về vai trò và chất lượng hành văn thành tích. Điểm số $p_E$ được cấu thành từ 3 tiêu chí con:

$$\begin{aligned}
p_E = \; &0.40 \cdot \text{Điểm Thâm niên (Tenure Score)} \\
&+ 0.30 \cdot \text{Điểm Tương thích Vai trò (Role Relevance Score)} \\
&+ 0.30 \cdot \text{Điểm Chất lượng Tác động Gạch đầu dòng (Bullet Impact Score)}
\end{aligned}$$

#### 3.4.1. Tiêu chí 1: Điểm Thâm niên (Tenure Score - 40% của $p_E$)
Đo lường số năm kinh nghiệm thực tế của ứng viên so với yêu cầu tối thiểu trong JD ($Y_{\text{req}}$):
- Tỷ lệ đáp ứng $R = \frac{Y_{\text{candidate}}}{Y_{\text{req}}}$.
- Áp dụng hàm suy giảm phi tuyến tính:
  - $R \ge 1.0$ (Đạt hoặc vượt số năm yêu cầu): **100 điểm**. (Nếu $R \ge 2.5$, kích hoạt thêm cờ thông tin Overqualified nhưng không trừ điểm).
  - $0.75 \le R < 1.0$: **85 điểm**.
  - $0.50 \le R < 0.75$: **65 điểm**.
  - $0.30 \le R < 0.50$: **40 điểm**.
  - $R < 0.30$: **15 điểm** (Kích hoạt cảnh báo thiếu thâm niên theo BR18).

*Ngoại lệ:* Nếu JD không yêu cầu số năm kinh nghiệm cụ thể $\rightarrow$ Tenure Score mặc định đạt 100 điểm.

#### 3.4.2. Tiêu chí 2: Điểm Tương thích Vai trò (Role Relevance Score - 30% của $p_E$)
- Chức danh công việc gần nhất hoặc chức danh mục tiêu có trùng khớp hoặc tương đương với chức danh trong JD (sử dụng từ điển chức danh nghề nghiệp):
  - Khớp hoàn toàn chức danh (Exact Job Title): **100 điểm**.
  - Khớp chức danh tương đương cùng cấp bậc (ví dụ: Software Engineer $\leftrightarrow$ Software Developer): **90 điểm**.
  - Khớp chức danh khác cấp bậc (ví dụ: Junior Developer vs Senior Developer): **70 điểm**.
  - Khớp lĩnh vực chuyên ngành nhưng khác vai trò: **50 điểm**.
  - Hoàn toàn khác ngành/vai trò: **20 điểm**.

#### 3.4.3. Tiêu chí 3: Điểm Chất lượng Tác động Gạch đầu dòng (Bullet Impact Score - 30% của $p_E$)
Mỗi gạch đầu dòng trong phần Kinh nghiệm / Dự án được chấm điểm độc lập trên thang 100:
- **Động từ hành động mở đầu (Action Verb):** 30 điểm (Động từ mạnh: 30 điểm; Động từ bình thường: 20 điểm; Bắt đầu bằng cụm từ yếu *"Responsible for/Helped"*: 5 điểm; Bắt đầu bằng danh từ/tính từ: 0 điểm).
- **Ngữ cảnh & Nhiệm vụ rõ ràng (Context & Specificity):** 30 điểm.
- **Có số liệu định lượng (Measurable Metrics):** 40 điểm (Chứa số liệu hợp lệ %, $, thời gian, số lượng: 40 điểm; Hoàn toàn không có số liệu: 0 điểm).
- **Phạt lặp từ:** Trừ 5 điểm trên toàn bộ tiêu chí này nếu một động từ lặp lại quá 3 lần.

$$\text{Bullet Impact Score} = \text{Trung bình cộng điểm chất lượng của tất cả các gạch đầu dòng} - \text{Điểm phạt lặp từ}$$

---

### 3.5. Trụ cột 3: Định dạng & Khả năng trích xuất ($p_F$ — Trọng số 15%)

Trụ cột Định dạng đánh giá tính toàn vẹn kỹ thuật khi tài liệu đi qua các bộ phân tích ATS (ATS Parsing Readiness). Điểm tối đa là 100 điểm, chia làm 4 chốt kiểm soát kỹ thuật:

| Chốt kiểm soát | Tiêu chuẩn đánh giá | Điểm tối đa |
|---|---|---|
| **Khả năng trích xuất văn bản (Text Extractability)** | Văn bản chọn và sao chép được (Highlightable). Không phải ảnh scan thuần túy. Tỷ lệ lỗi font mã hóa (CMap/Ligature) dưới 2%. | **40 điểm** |
| **Bố cục & Bảng biểu (Layout & Tables)** | Sử dụng bố cục 1 cột (Single-column). Không có bảng phức tạp để chia cột nội dung. Không có hộp văn bản trôi nổi (Floating Text Box). | **30 điểm** |
| **Tiêu đề phân mục chuẩn (Standard Section Headings)** | Sử dụng các tiêu đề quy ước (Work Experience, Education, Skills). Tiêu đề nằm riêng biệt một dòng, không chứa ký tự đồ họa đặc biệt. | **20 điểm** |
| **Thông tin liên hệ đầy đủ (Contact Information)** | Đầy đủ Email hợp lệ, Số điện thoại, Địa điểm (Tỉnh/Thành phố/Quốc gia), Link hồ sơ nghề nghiệp (LinkedIn/GitHub/Portfolio). | **10 điểm** |

*Quy tắc loại trừ nghiêm ngặt (BR03):* Nếu file tải lên hoàn toàn không thể trích xuất văn bản (PDF dạng ảnh chụp 100%), hệ thống **dừng quy trình, không trừ credit/lượt, gán mã lỗi `UNPARSEABLE_FILE`** và hướng dẫn ứng viên tải file có văn bản.

---

### 3.6. Trụ cột 4: Học vấn & Chứng chỉ ($p_H$ — Trọng số 15%)

Trụ cột Học vấn đánh giá sự tương thích về trình độ đào tạo và chứng chỉ chuyên môn theo yêu cầu của JD.

#### 3.6.1. Khi JD có yêu cầu học vấn / chứng chỉ cụ thể
Điểm $p_H$ được tính toán dựa trên mức độ đáp ứng:
1. **Cấp độ bằng cấp (Degree Level Match - 60% của $p_H$):**
   - Đạt hoặc vượt cấp bằng yêu cầu (ví dụ: JD đòi Cử nhân, ứng viên có Cử nhân hoặc Thạc sĩ): **100 điểm**.
   - Thấp hơn 1 bậc bằng cấp (ví dụ: JD đòi Cử nhân, ứng viên có Cao đẳng): **60 điểm**.
   - Thấp hơn từ 2 bậc trở lên hoặc không có thông tin: **20 điểm**.
2. **Chuyên ngành đào tạo (Field of Study Match - 20% của $p_H$):**
   - Trùng khớp chính xác chuyên ngành (hoặc thuộc nhóm ngành tương đương, ví dụ: Khoa học máy tính $\leftrightarrow$ Công nghệ thông tin): **100 điểm**.
   - Ngành gần hoặc ngành có liên quan: **70 điểm**.
   - Khác ngành hoàn toàn: **40 điểm**.
3. **Chứng chỉ nghề nghiệp bắt buộc (Mandatory Certifications - 20% của $p_H$):**
   - Nếu JD yêu cầu chứng chỉ (PMP, CPA, AWS Certified, IELTS...): Có chứng chỉ đạt **100 điểm**; Thiếu chứng chỉ đạt **0 điểm** (và kích hoạt cảnh báo BR18).
   - Nếu JD không yêu cầu chứng chỉ cụ thể: Phần này tự động lấy điểm bằng điểm Cấp độ bằng cấp.

#### 3.6.2. Khi JD KHÔNG yêu cầu học vấn (Áp dụng BR05)
- Trụ cột Học vấn được chuyển sang trạng thái `NOT_APPLICABLE`.
- Không đưa $p_H$ vào phép tính, không chấm điểm 0.
- Tự động phân bổ lại trọng số cho 3 trụ cột còn lại theo công thức chuẩn hóa tại mục 3.1.

---

### 3.7. Quy tắc làm tròn và Phân tầng nhãn diễn giải (Tier Labels & Rounding Rules)

#### 3.7.1. Quy tắc làm tròn số học
- Điểm thành phần các trụ cột ($p_K, p_E, p_F, p_H$) được giữ ở dạng số thực dấu phẩy động 2 chữ số thập phân trong quá trình tính toán nội bộ.
- Điểm tổng hợp cuối cùng $S$ được làm tròn theo quy tắc **Làm tròn đến số nguyên gần nhất (Round Half Up)**:
  - Điểm số $< 0.5$ làm tròn xuống; $\ge 0.5$ làm tròn lên (Ví dụ: $78.23 \rightarrow 78$; $78.50 \rightarrow 79$).
- Điểm hiển thị luôn là số nguyên trong khoảng $[0, 100]$.

#### 3.7.2. Hệ thống 4 Phân tầng nhãn diễn giải (Tier Labels)

| Khoảng điểm | Nhãn diễn giải (Tiếng Việt) | Tier Label (English) | Định hướng tư vấn cho ứng viên |
|---|---|---|---|
| **85 – 100** | **Xuất sắc — Sẵn sàng ứng tuyển** | **Strong Match — Interview Ready** | Hồ sơ có độ tương thích rất cao với JD, cấu trúc định dạng chuẩn ATS và gạch đầu dòng có số liệu thuyết phục. Ứng viên tự tin nộp đơn. |
| **70 – 84** | **Khá — Đạt chuẩn cơ bản** | **Good Match — Minor Tweaks Needed** | Đạt hầu hết các tiêu chuẩn cốt lõi. Cần bổ sung thêm một số từ khóa thứ cấp hoặc thay thế các động từ yếu để tăng tính cạnh tranh. |
| **50 – 69** | **Trung bình — Cần cải thiện** | **Moderate Match — Significant Gaps** | Có lỗ hổng đáng kể về kỹ năng bắt buộc, số năm kinh nghiệm hoặc thiếu hẳn số liệu minh chứng thành tích. Cần tối ưu kỹ trước khi nộp. |
| **0 – 49** | **Kém — Nguy cơ bị loại cao** | **Weak Match — High Rejection Risk** | Hồ sơ lệch chuyên môn nghiêm trọng, định dạng có nguy cơ vỡ parser, hoặc thiếu các tiêu chí bắt buộc then chốt. Cần tái cấu trúc toàn diện. |

#### 3.7.3. Quy tắc hiển thị Cảnh báo Bắt buộc (BR18) độc lập với Điểm số
- Điểm số cao **không đồng nghĩa với việc chắc chắn vượt qua vòng hồ sơ** nếu ứng viên thiếu một điều kiện tiên quyết (Deal-breaker / Knockout requirement).
- Do đó, giao diện báo cáo luôn phân tách rõ:
  1. Thẻ điểm tổng hợp và điểm 4 thành phần.
  2. Khối cảnh báo đỏ độc lập: *"Các yêu cầu bắt buộc của JD chưa tìm thấy bằng chứng trong CV"* (Ví dụ: thiếu chứng chỉ hành nghề, thiếu kỹ năng bắt buộc, hoặc thâm niên dưới ngưỡng sàn).

---

## Phần 4: Danh mục Nguồn tài liệu Tham khảo Chính thống (Official Citations & URLs)

1. **Jobscan Official Documentation & Research:**
   - Jobscan Help Center, *What exactly is being checked? Can you rate my resume?*: https://jobscansupport.frontkb.com/en/articles/11537409
   - Jobscan Help Center, *Is Jobscan’s algorithm based off of real Applicant Tracking Systems?*: https://jobscansupport.frontkb.com/en/articles/11537345
   - Jobscan Help Center, *Why do my results say I'm missing skills when they're already on my resume?*: https://jobscansupport.frontkb.com/en/articles/11537473
   - Jobscan Help Center, *What match rate should I aim for?*: https://jobscansupport.frontkb.com/en/articles/11537281
   - Jobscan Learning Center, *Applicant Tracking Systems: Everything You Need to Know*: https://www.jobscan.co/applicant-tracking-systems
   - Jobscan Research, *Research on Recruiter Search Behavior and Job Titles Impact*: https://www.jobscan.co/state-of-the-job-search

2. **Resume Worded Knowledge Base & Recruiter Guides:**
   - Resume Worded Help Center, *How is my score calculated?*: https://help.resumeworded.com/article/33-how-is-my-score-calculated
   - Resume Worded Help Center, *How do I make sure my resume is ATS-compliant?*: https://help.resumeworded.com/article/37-how-do-i-make-sure-my-resume-is-ats-compliant
   - Resume Worded Help Center, *How do I make sure my experience is correctly identified?*: https://help.resumeworded.com/article/52-how-do-i-make-sure-my-experience-is-correctly-identified
   - Resume Worded Product Guide, *Score My Resume Methodology & AI Evaluation Criteria*: https://resumeworded.com/score
   - Resume Worded Core Guide, *Resume Action Verbs Curated by Hiring Managers*: https://resumeworded.com/action-verbs

3. **VMock Platform Architecture & University Career Standards:**
   - VMock Official Platform, *Smart Career Platform Architecture & 3 Pillars Framework (Core Presentation, Competency, Impact)*: https://www.vmock.com/
   - Yale University Office of Career Strategy, *Resume Formatting and VMock Evaluation Guidelines*: https://ocs.yale.edu
   - UC Berkeley Career Center, *VMock Automated Resume Review Guide and 5 Core Competencies*: https://career.berkeley.edu
   - Google Inc. Hiring Formula, *Laszlo Bock (Former SVP of People Operations at Google), "Work Rules!": The XYZ Formula for Resumes ("Accomplished [X] as measured by [Y], by doing [Z]")*.

4. **Teal & Rezi Technical Specifications:**
   - Teal Platform Documentation, *Matching Mode and Resume Builder Architecture*: https://www.tealhq.com/tools/resume-builder
   - Rezi Official Documentation, *Free ATS Resume Checker: 23 Critical Checkpoints and Rezi Score Calculation*: https://www.rezi.ai/tools/resume-checker
   - Rezi Guides, *AI Resume Agent and Optimization Rules*: https://www.rezi.ai/rezi-docs

5. **Enterprise ATS Standards & Skill Taxonomies:**
   - European Commission, *ESCO (European Skills, Competences, Qualifications and Occupations) Taxonomy*: https://esco.ec.europa.eu
   - Lightcast (formerly EMSI Burning Glass), *Open Skills Taxonomy and Canonical Skill Mapping*: https://lightcast.io
   - Oracle Taleo Enterprise Edition, *Candidate Selection Workflows and Requisition Ranking (Req Rank) Guide*.
   - Workday, *Workday Skills Cloud: Machine Learning-Powered Skill Foundation Technical Whitepaper*.
