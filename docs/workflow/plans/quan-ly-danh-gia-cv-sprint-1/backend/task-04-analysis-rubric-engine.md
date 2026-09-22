# TASK-04: Lõi Tính điểm Rubric 4 Trụ cột & Bất biến Toán học (Domain Engine)

| Thuộc tính | Giá trị |
|---|---|
| Mã task | `TASK-04` |
| Folder & Phân bổ | `backend/` &bull; Phân công: **Dev 1 (Backend & Database)** |
| User Stories liên quan | `US08`, `US09` |
| Domain Models | `CVAnalysis.Domain/Invariants/RubricScoreCalculator.cs` |
| Hard Prerequisites | `TASK-01`, `TASK-03` |
| Chặn các tasks | `TASK-05` |
| Trạng thái | `Todo` |

---

## 1. Mục tiêu & Phạm vi

Xây dựng bộ tính toán thuần logic (Pure Domain Engine) thực thi mô hình toán học chấm điểm ATS 4 Trụ cột theo chuẩn nghiên cứu `RESEARCH-ATS-002` và các quyết định đã duyệt tại Cổng G1 (Mục 5 brief), bao gồm: công thức chuẩn hóa mẫu số 85 khi không có học vấn (BR05), chia đều kỹ năng khi không phân loại, phạt nhồi từ khóa chặn sàn tại 0 điểm, và kích hoạt cảnh báo yêu cầu bắt buộc (BR18).

---

## 2. Checklist Hành động

- [ ] Tạo class `RubricScoreCalculator` trong `CVAnalysis.Domain/Invariants/`:
  - Input: `ConfirmedCvData`, `JobDescriptionData`.
  - Output: `ReportScoreResult` (Điểm tổng $S$, 4 điểm thành phần, danh sách bằng chứng và cảnh báo bắt buộc).
- [ ] Cài đặt Trụ cột 1 (Kỹ năng & Từ khóa — Trọng số 40%):
  - Phân nhóm: Bắt buộc (70%) và Ưu tiên (30%). Nếu không phân loại rõ $\rightarrow$ chia đều 100% điểm cho tất cả kỹ năng.
  - Thang điểm: Exact Match (100%), Synonym Match (95%), Contextual Match (60%), Missing (0%).
  - Stuffing Penalty: Nếu mật độ từ khóa $> 3.5\%$ hoặc chuỗi danh từ kỹ thuật liên tiếp $> 10$ từ $\rightarrow$ trừ 25% điểm Trụ cột Kỹ năng; áp dụng hàm `Math.Max(0, score)` để chặn sàn tại 0 điểm.
- [ ] Cài đặt Trụ cột 2 (Kinh nghiệm & Dự án — Trọng số 30%):
  - Tỷ lệ số năm $R = Y_{\text{candidate}} / Y_{\text{req}}$: $R \ge 1.0 \rightarrow 100$đ; $0.75 \le R < 1.0 \rightarrow 85$đ; $0.50 \le R < 0.75 \rightarrow 65$đ; $0.30 \le R < 0.50 \rightarrow 40$đ; $R < 0.30 \rightarrow 15$đ. Nếu $R \ge 2.5 \rightarrow$ gắn cờ Overqualified không trừ điểm.
  - Khớp chức danh: Chính xác (100đ), Tương đương (90đ), Khác bậc (70đ), Cùng ngành khác vai trò (50đ), Khác ngành (20đ).
  - Chất lượng gạch đầu dòng: Động từ mạnh (30đ), Ngữ cảnh (30đ), Số liệu (40đ); trừ 5 điểm nếu động từ lặp $\ge 3$ lần.
- [ ] Cài đặt Trụ cột 3 (Định dạng & Trích xuất — Trọng số 15%):
  - Trích xuất văn bản (40đ), Bố cục 1 cột không bảng ẩn (30đ), Tiêu đề chuẩn (20đ), Thông tin liên hệ (10đ).
- [ ] Cài đặt Trụ cột 4 (Học vấn & Bằng cấp — Trọng số 15%):
  - Nếu JD không yêu cầu học vấn: Trạng thái `NOT_APPLICABLE`, loại bỏ khỏi mẫu số.
- [ ] Cài đặt Công thức Chuẩn hóa Trọng số (BR05):
  $$S = \frac{\sum_{i \in \mathcal{A}} w_i \cdot p_i}{\sum_{i \in \mathcal{A}} w_i}$$
  - Khi học vấn N/A: Mẫu số là 85, làm tròn Round Half Up đến số nguyên gần nhất.
- [ ] Kích hoạt cảnh báo yêu cầu bắt buộc (BR18): Nếu kỹ năng/chứng chỉ bắt buộc bị 0% $\rightarrow$ thêm vào mảng `MandatoryWarnings`.

---

## 3. Tiêu chí Nghiệm thu (Given-When-Then)

```text
Given JD không yêu cầu học vấn, điểm Kỹ năng = 80, Kinh nghiệm = 90, Định dạng = 90
When tính điểm tổng hợp S
Then hệ thống chia cho mẫu số 85 theo công thức S = (0.4*80 + 0.3*90 + 0.15*90) / 0.85
And kết quả làm tròn Round Half Up bằng đúng 85 điểm (Xuất sắc)

Given CV có mật độ từ khóa "Java" chiếm 4.0% tổng số từ
When tính điểm Trụ cột Kỹ năng
Then hệ thống áp dụng Stuffing Penalty trừ 25% điểm kỹ năng và điểm không bị âm

Given JD yêu cầu kỹ năng bắt buộc "Docker" nhưng CV không có từ này
When hoàn tất tính điểm
Then kết quả trả về có mảng mandatoryWarnings chứa cảnh báo thiếu Docker (BR18)
```

---

## 4. Lệnh Kiểm chứng Độc lập

```bash
dotnet test tests/CVAnalysis.UnitTests --filter FullyQualifiedName~RubricCalculatorTests
```
