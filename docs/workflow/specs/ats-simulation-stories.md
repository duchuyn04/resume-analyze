# Đặc tả User Stories & Trải nghiệm Người dùng (UX Flows) — Mô phỏng & Đánh giá ATS (Sprint 2)

| Thuộc tính | Giá trị |
|---|---|
| Artifact ID | STORIES-SIMULATION-001 |
| Revision | Draft 1; ngày 2026-09-22 |
| Phân hệ / Sprint | **Sprint 2 — Mô phỏng & Đánh giá Tương thích Đa Hệ thống ATS (ATS Simulation & Compatibility Engine)** |
| Mã Epic | `EP07: Mô phỏng và Đánh giá theo hệ thống ATS` |
| Nguồn nghiệp vụ G1 | `docs/workflow/specs/ats-simulation-brief.md` (Artifact ID: BRIEF-SIMULATION-001, Revision Draft 1, Approved 2026-09-22) |
| Báo cáo kiểm định G1 | `docs/workflow/specs/ats-simulation-g1-audit.md` (AUDIT-G1-ATS-001, PASS) |
| Trạng thái G2 | `approved`: Người dùng đã phê duyệt chính thức ngày 2026-09-22; sẵn sàng bàn giao sang Cổng G3 (solution-design) |

---

## 1. Danh mục User Stories (`US21` – `US25`)

### EP07: Mô phỏng và Đánh giá theo hệ thống ATS
- **US21:** Là Ứng viên, tôi muốn xem văn bản bóc tách thô dưới con mắt của 4 hệ thống ATS qua chế độ `ATS Reader View` (Workday, Taleo, Greenhouse, Lever) để nhận biết chính xác đoạn văn bản nào bị đọc lỗi, xáo trộn dòng hoặc mất chữ.
- **US22:** Là Ứng viên, tôi muốn xem Ma trận 4 Điểm tương thích ATS riêng biệt (% Workday, % Taleo, % Greenhouse, % Lever) với bảng phân tích lỗi chi tiết từng hệ thống để biết CV của mình an toàn với hệ thống nào.
- **US23:** Là Ứng viên, tôi muốn xem cảnh báo các lỗi bố cục nghiêm trọng (text box trôi nổi, bảng ẩn, header/footer) kèm hướng dẫn khắc phục từng bước chi tiết để tự chỉnh sửa trên file gốc.
- **US24:** Là Ứng viên, tôi muốn kích hoạt tính năng "Tự động chuẩn hóa bố cục khi xuất file" (Auto-fix Layout on Export) kèm bản xem trước so sánh (Preview Modal) để hệ thống tự động chuyển cấu trúc CV phức tạp về dạng 1 cột an toàn tuyệt đối khi tải file PDF/DOCX.
- **US25:** Là Ứng viên, tôi muốn chạy kiểm tra mô phỏng ATS độc lập mà không cần nhập JD để kiểm tra độ an toàn kỹ thuật của CV bất kỳ lúc nào.

---

## 2. Bảng Ma trận User (Actor) và User Story

| Story ID | Tên User Story | Ứng viên | Hỗ trợ viên | Lead hỗ trợ | Kế toán | Admin | Hệ thống tự động | Quy tắc nguồn |
|:---:|---|:---:|:---:|:---:|:---:|:---:|:---:|---|
| **US21** | Chế độ xem 'ATS Reader View' 4 hệ thống | Xem/chuyển tab | Chỉ xem khi cấp 72h | Chỉ xem khi cấp 72h | Chặn 403 | Chặn 403 | Bóc tách & bôi màu lỗi | SR01, SR02 |
| **US22** | Ma trận 4 Điểm tương thích (% từng hệ thống) | Xem ma trận | Chỉ xem khi cấp 72h | Chỉ xem khi cấp 72h | Chặn 403 | Chặn 403 | Tính điểm phạt từng dòng | SR03, Mục 5.2 |
| **US23** | Cảnh báo lỗi bố cục & Hướng dẫn tự sửa | Xem hướng dẫn | Chỉ xem khi cấp 72h | Chỉ xem khi cấp 72h | Chặn 403 | Chặn 403 | Quét tọa độ & cấu trúc | SR06, SR08, SR09 |
| **US24** | Tự động chuẩn hóa 1 cột khi xuất file | Bấm tải/xem preview | — | — | — | — | Dàn phẳng cấu trúc 1 cột | SR06 |
| **US25** | Quét kiểm tra tương thích không cần JD | Thực hiện | — | — | — | — | Chuyển từ khóa sang N/A | SR05, BR12 |

*Ghi chú Bảo mật Zero-Trust:* Nhân viên hỗ trợ và Lead hỗ trợ chỉ xem được báo cáo mô phỏng ATS khi ứng viên chủ động cấp quyền hỗ trợ vụ việc trong tối đa 72 giờ theo BR22. Toàn bộ các truy cập đều ghi audit log bất biến.

---

## 3. Story Map theo Hành trình Tối ưu ATS của Ứng viên

```text
[HÀNH TRÌNH KIỂM TRA & TỐI ƯU ATS CỦA ỨNG VIÊN]
1. Tải CV & Chạy Quét  ──►  2. Đánh giá Ma trận Đa Hệ thống  ──►  3. Mở ATS Reader View  ──►  4. Xem Lỗi Bố cục  ──►  5. Tự động Chuẩn hóa & Xuất
           │                                 │                                   │                           │                            │
           ├─ US25: Quét có JD/không JD      ├─ US22: Xem điểm % Workday         ├─ US21: Xem góc Workday    ├─ US23: Lỗi mất Header/Footer├─ US24: Bấm Auto-fix Layout
           └─ SR04: 1 Credit trọn gói        ├─ US22: Xem điểm % Taleo           ├─ US21: Xem góc Taleo      ├─ US23: Lỗi 2 cột đọc lộn xộn ├─ US24: Xem Preview so sánh
                                             ├─ US22: Xem điểm % Greenhouse      ├─ US21: Xem góc Greenhouse └─ US23: Xem hướng dẫn sửa   └─ US24: Tải PDF/DOCX 1 cột
                                             └─ US22: Xem điểm % Lever           └─ US21: Xem góc Lever
```

---

## 4. Đặc tả Chi tiết từng User Story

### US21: Chế độ xem 'ATS Reader View' mô phỏng văn bản bóc tách 4 hệ thống
- **Mô tả:** Là Ứng viên, tôi muốn chuyển đổi giữa 4 góc nhìn của Workday, Taleo, Greenhouse, Lever để xem trực tiếp đoạn văn bản mà máy quét ATS đọc được.
- **Tiền điều kiện:** Đã hoàn thành phân tích nâng cao (gói 1 credit).
- **Acceptance Criteria (GWT):**
  - `AC21.1 (Tabs Switching):` Given màn hình Báo cáo ATS, When ứng viên bấm vào tab `ATS Reader View`, Then hiển thị thanh chọn 4 góc nhìn: *Workday*, *Oracle Taleo*, *Greenhouse*, *Lever*.
  - `AC21.2 (Visual Highlights):` Given văn bản thô sau khi được bộ giả lập bóc tách, When hiển thị trên màn hình, Then hệ thống tự động bôi màu các đoạn bất thường:
    - Màu đỏ (Chữ bị mất): Hiển thị vết cắt và thông báo *"Thông tin này bị Taleo bỏ qua do nằm ở Header/Footer"*.
    - Màu vàng (Dòng xáo trộn): Đóng khung các câu văn bị đọc đan xen ngang giữa 2 cột trên Workday.
    - Màu xám (Lỗi ký tự): Đánh dấu các ký tự bị biến thành dấu hỏi `?` hoặc ô vuông rỗng.
  - `AC21.3 (Free Toggle):` Given báo cáo còn trong hạn 90 ngày, When người dùng chuyển đổi qua lại giữa 4 góc nhìn ATS nhiều lần, Then hệ thống hiển thị tức thời và hoàn toàn miễn phí (SR07).

### US22: Ma trận 4 Điểm tương thích ATS riêng biệt
- **Mô tả:** Là Ứng viên, tôi muốn xem ma trận 4 điểm tương thích độc lập kèm danh sách lỗi chi tiết để biết CV của mình phù hợp nhất với hệ thống nào.
- **Acceptance Criteria (GWT):**
  - `AC22.1 (Matrix Display):` Given báo cáo mô phỏng hoàn tất, When mở xem bảng tổng hợp, Then hiển thị 4 thẻ điểm tương thích:
    - *Workday:* Điểm % (trừ lỗi cột đôi, text box trôi nổi, bảng ẩn).
    - *Oracle Taleo:* Điểm % (trừ lỗi mất Header/Footer, font lạ, bảng phức tạp).
    - *Greenhouse:* Điểm % (trừ lỗi PDF ảnh/trang rasterize, thiếu từ khóa).
    - *Lever:* Điểm % (trừ lỗi không nhận diện được thẻ kỹ năng, cấu trúc ngày tháng).
  - `AC22.2 (Score Floor & Rounding):` Given CV mắc nhiều lỗi bố cục nghiêm trọng, When hệ thống tính điểm tương thích, Then điểm số chặn sàn tại 0% (không bao giờ âm) và làm tròn số nguyên Round Half Up.
  - `AC22.3 (Mixed PDF Defense):` Given file PDF hỗn hợp có trang 2 là ảnh rasterize (không bóc tách được text), When tính điểm Greenhouse, Then hệ thống áp dụng mức phạt -100% cho trang ảnh đó và cảnh báo rõ trang không đọc được.

### US23: Cảnh báo lỗi bố cục nghiêm trọng & Hướng dẫn tự sửa từng bước
- **Mô tả:** Là Ứng viên, tôi muốn nhận danh sách các lỗi định dạng phá vỡ ATS kèm hướng dẫn từng bước để tự chỉnh sửa trên file thiết kế gốc.
- **Acceptance Criteria (GWT):**
  - `AC23.1 (Categorized Warning List):` Given báo cáo phát hiện các lỗi định dạng, When hiển thị, Then phân nhóm rõ: *Lỗi nghiêm trọng (Critical - nguy cơ rớt 100%)* và *Lỗi cần cải thiện (Warning)*.
  - `AC23.2 (Actionable Fix Guidance):` Given mỗi lỗi được liệt kê, When bấm xem chi tiết, Then hiển thị:
    - Nguyên nhân kỹ thuật (Tại sao ATS bị gãy).
    - Hướng dẫn sửa trên Microsoft Word (ví dụ: *"Vào Insert ──► Header ──► Remove Header; dán số điện thoại vào dòng đầu body"*).
    - Hướng dẫn sửa trên Canva / Google Docs.

### US24: Tự động chuẩn hóa bố cục 1 cột an toàn khi xuất file (Auto-fix Layout)
- **Mô tả:** Là Ứng viên, tôi muốn hệ thống tự động dàn phẳng CV nhiều cột thành 1 cột an toàn khi xuất file PDF/DOCX để đảm bảo 100% vượt qua vòng quét của Taleo & Workday.
- **Acceptance Criteria (GWT):**
  - `AC24.1 (Auto-fix Toggle on Export):` Given màn hình xuất file, When chuẩn bị tải file, Then có tùy chọn tích chọn: *"Tự động chuẩn hóa bố cục 1 cột an toàn cho ATS (Khuyên dùng)"*.
  - `AC24.2 (Preview Comparison Modal):` Given người dùng tích chọn Auto-fix, When bấm xem trước, Then mở Modal Preview so sánh song song giữa bản gốc và bản chuẩn hóa 1 cột (giúp người dùng an tâm về diện mạo văn bản trước khi tải).
  - `AC24.3 (Safe Output Generation):` Given file được xuất với Auto-fix, When mở file PDF hoặc DOCX tải về, Then toàn bộ bảng ẩn và cột đôi được chuyển thành các khối văn bản 1 cột tuần tự, tiêu đề phân mục chuẩn convention, không chứa text box trôi nổi.

### US25: Quét tương thích ATS độc lập khi không có JD
- **Mô tả:** Là Ứng viên, tôi muốn quét kiểm tra độ an toàn kỹ thuật của CV trước các dòng ATS ngay cả khi chưa có JD tuyển dụng cụ thể.
- **Acceptance Criteria (GWT):**
  - `AC25.1 (No-JD Compatibility Scan):` Given ứng viên tải file CV và chọn chế độ *"Quét tương thích ATS & Bố cục (Không cần JD)"*, When bắt đầu quét, Then hệ thống kiểm tra toàn diện bố cục, font chữ, bảng ẩn, Header/Footer và trả về Ma trận tương thích 4 hệ thống.
  - `AC25.2 (Keyword Criteria N/A Handling):` Given chế độ quét không có JD, When tính điểm tương thích cho Greenhouse và Lever, Then tiêu chí *"thiếu từ khóa ngữ cảnh (-30%)"* tự động chuyển sang trạng thái `NOT_APPLICABLE`, điểm số chỉ tính trên các lỗi định dạng kỹ thuật thực tế (không trừ oan điểm ứng viên).

---

## 5. Danh mục Luồng Thao tác UX (Flow Catalogue)

### FL07: Luồng Xem 'ATS Reader View' và Ma trận Tương thích
1. Từ màn hình Báo cáo phân tích nâng cao, ứng viên bấm tab *"Mô phỏng ATS & Reader View"* (SCR15).
2. Màn hình hiển thị Ma trận 4 Điểm tương thích: Workday (70%), Taleo (58%), Greenhouse (92%), Lever (88%).
3. Ứng viên bấm vào tab *"Góc nhìn Oracle Taleo"*:
   - Khung văn bản thô hiển thị đoạn text sau khi Taleo bóc tách.
   - Dòng số điện thoại và email ở Header bị bôi màu đỏ gạch ngang kèm nhãn: *"Bị Taleo xóa do nằm ở Header"*.
4. Ứng viên bấm sang tab *"Góc nhìn Workday"*:
   - Khung văn bản hiển thị các câu văn của 2 cột kinh nghiệm bị nối ngang vào nhau, viền vàng cảnh báo đọc đan xen dòng.

### FL08: Luồng Xem Hướng dẫn Tự sửa & Kích hoạt Auto-fix Layout
1. Bên dưới màn hình Reader View, hiển thị danh sách 2 lỗi nghiêm trọng được phát hiện.
2. Ứng viên bấm vào lỗi *"Bảng 2 cột đọc đan xen dòng trên Workday"*:
   - Mở rộng Accordion hiển thị hướng dẫn chi tiết cách gỡ bỏ bảng trong Word.
3. Ứng viên bấm nút *"Tự động sửa lỗi bố cục khi xuất file"* (SCR16).
4. Modal mở ra hiển thị bản xem trước: Cột trái là CV 2 cột gốc, Cột phải là bản chuẩn hóa 1 cột tuần tự.
5. Ứng viên bấm *"Xuất PDF chuẩn hóa 1 cột"* $\rightarrow$ Tải file an toàn về máy.

### FL09: Luồng Quét Tương thích Độc lập không cần JD
1. Ứng viên tải file CV tại trang chủ $\rightarrow$ Chọn chế độ *"Quét tương thích ATS (Không cần JD)"*.
2. Hệ thống kiểm tra kỹ thuật trong 10–15 giây $\rightarrow$ Chuyển thẳng vào Ma trận 4 Điểm tương thích.
3. Tiêu chí từ khóa ngữ cảnh tự động hiển thị nhãn `N/A`, điểm số phản ánh độ sạch của định dạng file.

### FL10: Luồng Hỗ trợ Kỹ thuật & Quyền xem Reader View
1. Ứng viên khiếu nại kết quả mô phỏng $\rightarrow$ cấp quyền hỗ trợ 72h (BR22).
2. Nhân viên hỗ trợ mở giao diện vụ việc $\rightarrow$ xem được màn hình ATS Reader View của khách hàng để đối chiếu lỗi trích xuất.
3. Khi đóng vụ việc $\rightarrow$ quyền xem tự động bị thu hồi ngay lập tức.

---

## 6. Danh mục Màn hình Mới thuộc Sprint 2

| Mã màn hình | Tên màn hình / View | Vai trò | Thao tác chính | Trạng thái Loading | Trạng thái Empty | Trạng thái Error |
|---|---|---|---|---|---|---|
| **SCR15** | Tab Mô phỏng ATS & Reader View (trong Báo cáo) | Ứng viên | Chuyển 4 góc nhìn ATS, xem ma trận %, xem hướng dẫn sửa | Skeleton text bóc tách | "Chưa có dữ liệu mô phỏng" | Lỗi trích xuất parser |
| **SCR16** | Modal So sánh & Xem trước Auto-fix Layout 1 cột | Ứng viên | So sánh trực quan 2 cột vs 1 cột, bấm xuất PDF/DOCX | Spinner dàn phẳng | Không áp dụng | Lỗi tạo file xuất |
