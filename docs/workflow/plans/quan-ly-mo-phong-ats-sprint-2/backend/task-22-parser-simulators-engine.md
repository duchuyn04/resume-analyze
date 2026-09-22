# TASK-22: Xây dựng 4 Bộ Heuristic Parser Simulators (Workday, Taleo, Greenhouse, Lever)

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-22` |
| Folder & Phân bổ | `backend/` &bull; Phân công: **Dev 1 (Backend & Database)** |
| User Stories liên quan | `US21`, `US22`, `US23`, `US25` |
| Domain & Infra | `CVAnalysis.Infrastructure/AtsSimulators/` |
| Hard Prerequisites | `TASK-21` (Bảng AtsSimulations) |
| Chặn các tasks | `TASK-24` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Phát triển 4 bộ giả lập parser nội bộ bằng C# (ADR-005) dựa trên các thông số nghiên cứu thực chứng `RESEARCH-ATS-002`, phân tích tài liệu văn bản bằng Bounding Box của `PdfPig` và OpenXML DOM, mô phỏng các lỗi bóc tách đặc thù của từng dòng ATS (Workday đọc ngang 2 cột, Taleo xóa Header/Footer, Greenhouse trang ảnh rasterize, Lever thẻ kỹ năng), và tính toán 4 điểm số tương thích (% Workday, % Taleo, % Greenhouse, % Lever) với cơ chế chặn sàn 0 điểm.

---

## 2. Checklist Hành động

- [ ] Định nghĩa `IAtsParserSimulator` và Model `AtsSimulationResult`.
- [ ] Xây dựng `WorkdayParserSimulator` (SR08):
  - Phân tích tọa độ $Y$ của các từ: nếu có 2 cụm từ song song trên cùng trục $Y$ có $X_{\text{gap}} > 20\text{pt}$ mà không có đường phân cách bảng $\rightarrow$ mô phỏng đọc ngang dòng, đánh dấu lỗi `InterleavedColumns` và trừ 30% điểm Workday.
- [ ] Xây dựng `TaleoParserSimulator` (SR09):
  - Nhận diện các khối văn bản có $Y_{\text{top}} < 40\text{pt}$ hoặc $Y_{\text{bottom}} > (H - 40\text{pt})$ hoặc nằm trong thẻ `<header>`/`<footer>` của Word.
  - Loại bỏ các khối này khỏi chuỗi bóc tách; nếu số điện thoại/email bị mất $\rightarrow$ đánh dấu lỗi `HeaderStripping` và trừ 40% điểm Taleo.
- [ ] Xây dựng `GreenhouseParserSimulator`:
  - Phân tích mật độ text layer: nếu phát hiện trang có chứa ảnh rasterize chiếm $> 80\%$ diện tích mà số từ $< 10$ từ $\rightarrow$ trừ 100% điểm trang đó.
  - Xử lý quét không có JD (US25, Finding 1): nếu `jobDescription == null` $\rightarrow$ tiêu chí từ khóa ngữ cảnh tự động chuyển `NOT_APPLICABLE`, không trừ 30 điểm.
- [ ] Xây dựng `LeverParserSimulator` (SR10):
  - Kiểm tra phân tách thẻ kỹ năng (Skill Tags Tokenization) và định dạng ngày tháng.
- [ ] Xây dựng Service điều phối `CompositeAtsSimulatorService`:
  - Chạy song song 4 simulators qua `Task.WhenAll`.
  - Áp dụng công thức $C_{\text{system}} = \max(0, 100 - \sum P_{j})$.
  - Đóng gói toàn bộ payload vào chuỗi JSON `simulation_details`.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given file CV có bố cục 2 cột song song tạo bằng Canva
When WorkdayParserSimulator thực hiện bóc tách
Then phát hiện lỗi xáo trộn dòng ngang InterleavedColumns
And điểm Workday bị trừ đúng 30% kèm tọa độ các dòng bị lỗi

Given file CV đặt email và số điện thoại hoàn toàn ở Header trang giấy
When TaleoParserSimulator thực hiện bóc tách
Then thông tin liên hệ bị loại bỏ khỏi văn bản thô
And điểm Taleo bị trừ đúng 40% kèm cảnh báo HeaderStripping

Given ứng viên chạy quét mô phỏng ATS không có JD
When GreenhouseParserSimulator tính điểm
Then tiêu chí từ khóa ngữ cảnh được gán NOT_APPLICABLE và điểm không bị trừ 30% oan
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
dotnet test tests/CVAnalysis.UnitTests --filter FullyQualifiedName~AtsSimulatorTests
```
