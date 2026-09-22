# Thiết kế Kiến trúc & Giải pháp Kỹ thuật — Phân hệ Mô phỏng ATS (Sprint 2)

| Thuộc tính | Giá trị |
|---|---|
| Artifact ID | DESIGN-SIMULATION-001 |
| Revision | Draft 1; ngày 2026-09-22 |
| Chế độ | Cổng G3 — Thiết kế Kiến trúc, Database Schema & API Contracts |
| Phân hệ / Sprint | **Sprint 2 — Mô phỏng & Đánh giá Tương thích Đa Hệ thống ATS (Workday, Taleo, Greenhouse, Lever)** |
| Mã Epic | `EP07: Mô phỏng và Đánh giá theo hệ thống ATS` |
| Kế thừa Tech Stack | **ASP.NET Core Web API (.NET 9/8, C#) + React (Vite) + Microsoft SQL Server 2022** |
| Nguồn nghiệp vụ G1 | `docs/workflow/specs/ats-simulation-brief.md` (Artifact ID: BRIEF-SIMULATION-001, Approved 2026-09-22) |
| Nguồn User Stories G2 | `docs/workflow/specs/ats-simulation-stories.md` (Artifact ID: STORIES-SIMULATION-001, Approved 2026-09-22, 5 Stories, 13 ACs) |
| Sơ đồ ERD kiểm thử | `docs/workflow/diagrams/ats-simulation-erd.html` (Đã kiểm thử Browser Native) |
| Trạng thái G3 | `approved`: Người dùng đã phê duyệt chính thức ngày 2026-09-22; sẵn sàng bàn giao sang Cổng G4 (delivery-planning) |

---

## 1. Kế thừa Kiến trúc & Tech Stack

Phân hệ Sprint 2 kế thừa toàn bộ nền tảng công nghệ và quy chuẩn Clean Architecture đã thiết lập ở Sprint 1:
- **Backend:** ASP.NET Core Web API (.NET 9/8) với Entity Framework Core.
- **Frontend:** React (Vite + TypeScript + Tailwind CSS).
- **Cơ sở dữ liệu:** Microsoft SQL Server 2022.
- **Bảo mật:** JWT Bearer Authentication, Role-based Claims (5 roles) và Custom Authorization Handler Zero-Trust (BR07, BR22).

---

## 2. Sơ đồ ERD & Database Schema Delta (Sprint 2)

Sơ đồ ERD vật lý cho phân hệ mô phỏng ATS đã được thiết kế và kiểm thử thành công tại:
👉 **[Sơ đồ ERD trực quan Sprint 2 (docs/workflow/diagrams/ats-simulation-erd.html)](../diagrams/ats-simulation-erd.html)**

### Bảng Cơ sở Dữ liệu Mới: `dbo.AtsSimulations` (ADR-004)

Bảng `dbo.AtsSimulations` được thiết kế liên kết 1-1 với `dbo.Reports` qua khóa ngoại `report_id` với hành vi **`ON DELETE CASCADE`**: khi báo cáo hoặc CV gốc bị xóa, toàn bộ dữ liệu mô phỏng ATS tự động bị xóa đồng thời (BR10, SR07).

```sql
CREATE TABLE dbo.AtsSimulations (
    id UNIQUEIDENTIFIER NOT NULL CONSTRAINT DF_AtsSimulations_id DEFAULT NEWSEQUENTIALID(),
    report_id UNIQUEIDENTIFIER NOT NULL,
    workday_score INT NOT NULL, -- 0-100 (Điểm tương thích Workday)
    taleo_score INT NOT NULL,   -- 0-100 (Điểm tương thích Oracle Taleo)
    greenhouse_score INT NOT NULL, -- 0-100 (Điểm tương thích Greenhouse)
    lever_score INT NOT NULL,   -- 0-100 (Điểm tương thích Lever)
    simulation_details NVARCHAR(MAX) NOT NULL, -- Cấu trúc JSON chi tiết
    created_at DATETIMEOFFSET NOT NULL CONSTRAINT DF_AtsSimulations_created_at DEFAULT SYSDATETIMEOFFSET(),
    expires_at DATETIMEOFFSET NOT NULL, -- created_at + 90 ngày (BR21)
    CONSTRAINT PK_AtsSimulations PRIMARY KEY CLUSTERED (id),
    CONSTRAINT UQ_AtsSimulations_report UNIQUE NONCLUSTERED (report_id),
    CONSTRAINT FK_AtsSimulations_Reports FOREIGN KEY (report_id) REFERENCES dbo.Reports(id) ON DELETE CASCADE,
    CONSTRAINT CK_AtsSimulations_workday_score CHECK (workday_score >= 0 AND workday_score <= 100),
    CONSTRAINT CK_AtsSimulations_taleo_score CHECK (taleo_score >= 0 AND taleo_score <= 100),
    CONSTRAINT CK_AtsSimulations_greenhouse_score CHECK (greenhouse_score >= 0 AND greenhouse_score <= 100),
    CONSTRAINT CK_AtsSimulations_lever_score CHECK (lever_score >= 0 AND lever_score <= 100)
);

CREATE NONCLUSTERED INDEX IX_AtsSimulations_expires ON dbo.AtsSimulations (expires_at);
```

#### Cấu trúc Payload của trường `simulation_details` (JSON)
```json
{
  "workday": {
    "score": 70,
    "rawText": "...văn bản bóc tách...",
    "errors": [
      { "type": "InterleavedColumns", "severity": "Critical", "penalty": 30, "lineStart": 12, "lineEnd": 16, "message": "Dòng văn bản từ cột 1 bị đọc nối ngang sang cột 2" }
    ]
  },
  "taleo": {
    "score": 58,
    "rawText": "...văn bản bóc tách...",
    "errors": [
      { "type": "HeaderStripping", "severity": "Critical", "penalty": 40, "message": "Số điện thoại và email nằm trong Header lề trên (<40pt) bị Taleo xóa bỏ hoàn toàn" }
    ]
  },
  "greenhouse": {
    "score": 92,
    "rawText": "...văn bản bóc tách...",
    "errors": []
  },
  "lever": {
    "score": 88,
    "rawText": "...văn bản bóc tách...",
    "errors": []
  },
  "autoFixAvailable": true
}
```

---

## 3. Kiến trúc 4 Bộ Giả lập Parser ATS (Heuristic & AST Simulators)

Toàn bộ 4 simulator được đặt trong namespace `CVAnalysis.Infrastructure.AtsSimulators/` và triển khai chung interface `IAtsParserSimulator`:

```csharp
public interface IAtsParserSimulator
{
    AtsSystem SystemType { get; }
    Task<AtsSimulationResult> SimulateParsingAsync(
        ParsedDocument document, 
        JobDescriptionData? jdData, 
        CancellationToken ct);
}
```

### 1. `WorkdayParserSimulator` (Thuật toán Quét Bounding Box Đa Cột)
- **Cơ chế:** Phân tích tọa độ hình học $(X_1, Y_1, X_2, Y_2)$ của các khối từ (Text Tokens).
- **Phát hiện Interleaved Bug (SR08):**
  - Quét các khối văn bản có cùng tọa độ trục $Y$ ($\pm 4\text{pt}$).
  - Nếu tồn tại $\ge 2$ khối song song có khoảng cách $X_{\text{gap}} > 20\text{pt}$ (dấu hiệu cột đôi) nhưng không có thẻ bảng hoặc đường phân cách dọc rõ ràng:
    - Thuật toán mô phỏng việc đọc ngang: ghép từ $X_{\text{col1}}$ sang $X_{\text{col2}}$ trên cùng dòng.
    - Đánh dấu vùng bị lỗi và áp dụng mức phạt: `workday_score -= 30`.

### 2. `TaleoParserSimulator` (Thuật toán Quét Lề & Header/Footer Stripping)
- **Cơ chế:** Phân tích tọa độ tuyệt đối so với khổ trang chuẩn (A4 / Letter).
- **Phát hiện Header/Footer Stripping (SR09):**
  - Khối văn bản có $Y_{\text{top}} < 40\text{pt}$ hoặc $Y_{\text{bottom}} > (H_{\text{page}} - 40\text{pt})$: bị đánh dấu là Header/Footer.
  - TaleoSimulator loại bỏ toàn bộ các khối này khỏi `rawText`.
  - Kiểm tra xem thông tin liên hệ (`email`, `phone_number`) có xuất hiện trong phần `rawText` còn lại hay không:
    - Nếu không tìm thấy $\rightarrow$ áp dụng mức phạt nghiêm trọng: `taleo_score -= 40`.

### 3. `GreenhouseParserSimulator` (Thuật toán Kiểm tra Text Layer & Ngữ cảnh)
- **Cơ chế:** Kiểm tra mật độ ký tự bóc tách được trên mỗi trang.
- **Phát hiện Mixed PDF Rasterization (Finding 2):**
  - Nếu phát hiện trang có chứa thẻ ảnh nhúng lớn chiếm $> 80\%$ diện tích mà số lượng text token $< 10$ từ $\rightarrow$ đánh dấu trang ảnh không đọc được, áp dụng mức phạt `greenhouse_score -= 100` cho trang đó.
- **Xử lý Không có JD (Finding 1, SR05):**
  - Nếu `jdData == null` $\rightarrow$ tiêu chí từ khóa ngữ cảnh tự động chuyển `NOT_APPLICABLE`, không trừ 30 điểm của ứng viên.

### 4. `LeverParserSimulator` (Thuật toán Kiểm tra Thẻ Kỹ năng & Ngày tháng)
- **Cơ chế:** Quét phân tách danh từ kỹ thuật (Tokenization).
- **Phát hiện lỗi định dạng kỹ năng (SR10):**
  - Nếu các kỹ năng bị dồn cục trong một đoạn văn dài hoặc phân cách bằng emoji không chuẩn $\rightarrow$ trừ 25% điểm Lever.

---

## 4. Dịch vụ Tự động Chuẩn hóa Bố cục khi Xuất File (`AutoFixLayoutService`)

Nằm trong `CVAnalysis.Infrastructure.Export/`:
- **Đầu vào:** `ConfirmedCvData` (Dữ liệu ứng viên đã xác nhận).
- **Xử lý Dàn phẳng (Flattening):**
  1. Loại bỏ toàn bộ cấu trúc bảng `<table>`, text box thả nổi `<w:txbx>` và phân chia cột `<w:cols>`.
  2. Tự động chuyển đổi thành bố cục văn bản 1 cột tuần tự từ trên xuống dưới (Single-column layout) theo thứ tự chuẩn ATS:
     - Header: Họ tên (cỡ chữ 18pt), Email, Số điện thoại, Địa chỉ, Link Portfolio (1 dòng duy nhất ở body).
     - Phân mục 1: Tóm tắt nghề nghiệp (Professional Summary).
     - Phân mục 2: Kỹ năng chuyên môn (Skills — phân cách bằng dấu chấm tròn `•`).
     - Phân mục 3: Kinh nghiệm làm việc (Work Experience — Tên vị trí, Tên công ty, Ngày tháng, Gạch đầu dòng ACM).
     - Phân mục 4: Học vấn & Bằng cấp (Education).
     - Phân mục 5: Chứng chỉ (Certifications).
- **Đầu ra:** File PDF hoặc DOCX hoàn toàn an toàn, đảm bảo đạt 100% điểm tương thích định dạng trên cả 4 hệ thống.

---

## 5. Đặc tả Hợp đồng API (API Contracts cho US21–US25)

### Endpoint 1: Lấy kết quả mô phỏng ATS & Reader View
- **Method & Route:** `GET /api/v1/reports/{reportId}/ats-simulation` (US21, US22, US23)
- **Headers:** `Authorization: Bearer <token>`
- **Response (200 OK):**
```json
{
  "reportId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "scores": {
    "workday": 70,
    "taleo": 58,
    "greenhouse": 92,
    "lever": 88
  },
  "views": {
    "workday": {
      "rawText": "Nguyen Van A ...",
      "interleavedErrorDetected": true,
      "scrambledLines": [12, 13, 14]
    },
    "taleo": {
      "rawText": "Nguyen Van A ...",
      "headerStripped": true,
      "missingContactInfo": ["Phone", "Email"]
    },
    "greenhouse": {
      "rawText": "Nguyen Van A ...",
      "keywordStatus": "Applied"
    },
    "lever": {
      "rawText": "Nguyen Van A ..."
    }
  },
  "actionableFixes": [
    {
      "system": "Taleo",
      "title": "Chuyển số điện thoại và email vào phần thân trang giấy",
      "instructionWord": "Vào Insert -> Header -> Remove Header; dán số điện thoại vào dòng đầu của trang",
      "instructionCanva": "Kéo text box số điện thoại xuống dưới ít nhất 2cm cách mép trên trang"
    },
    {
      "system": "Workday",
      "title": "Gỡ bỏ định dạng bảng 2 cột",
      "instructionWord": "Chọn bảng -> Table Layout -> Convert to Text để dàn phẳng thành 1 cột tuần tự"
    }
  ],
  "autoFixAvailable": true
}
```

### Endpoint 2: Quét kiểm tra tương thích ATS độc lập (Không cần JD)
- **Method & Route:** `POST /api/v1/cv/scan-ats-compatibility` (US25)
- **Headers:** `Authorization: Bearer <token>`
- **Request Body:** `multipart/form-data` (`file`: PDF hoặc DOCX)
- **Response (200 OK):**
```json
{
  "cvFileId": "3fa85f64-5717-4562-b3fc-2c963f66afa6",
  "scores": {
    "workday": 85,
    "taleo": 65,
    "greenhouse": 100,
    "lever": 90
  },
  "keywordStatus": "NOT_APPLICABLE",
  "layoutSafetyStatus": "NeedsAttention"
}
```

### Endpoint 3: Xuất file có kích hoạt Auto-fix Layout 1 cột
- **Method & Route:** `POST /api/v1/reports/{reportId}/export-autofix` (US24)
- **Headers:** `Authorization: Bearer <token>`
- **Request Body:**
```json
{
  "format": "PDF", // hoặc "DOCX"
  "autoFixLayout": true
}
```
- **Response (200 OK):** Stream binary file (`application/pdf` hoặc `application/vnd.openxmlformats-officedocument.wordprocessingml.document`).

---

## 6. Các Quyết định Kiến trúc Bổ sung (ADRs)

### ADR-004: Tách riêng Thực thể `dbo.AtsSimulations` thay vì gộp JSON vào `dbo.Reports`
- **Bối cảnh:** Dữ liệu bóc tách văn bản thô của 4 hệ thống ATS có dung lượng tương đối lớn (khoảng 20KB – 50KB mỗi bản ghi). Nếu lưu chung vào một cột JSON trong `dbo.Reports`, mỗi khi ứng viên mở danh sách lịch sử (SCR09) hoặc tra cứu điểm tổng hợp, SQL Server sẽ phải đọc một lượng lớn I/O không cần thiết.
- **Quyết định:** Tạo bảng vật lý độc lập `dbo.AtsSimulations` liên kết 1-1 với `dbo.Reports` qua khóa ngoại `report_id` với `ON DELETE CASCADE`.
- **Hệ quả:** Tối ưu hóa triệt để hiệu năng đọc cơ sở dữ liệu. Báo cáo chính tải cực nhanh; dữ liệu mô phỏng nặng chỉ được truy vấn khi ứng viên chủ động bấm vào tab `ATS Reader View`.

### ADR-005: Xây dựng Bộ Giả lập Parser Heuristic Nội bộ thay vì Tích hợp Dịch vụ Bên thứ Ba
- **Bối cảnh:** Có một số dịch vụ SaaS bên ngoài cung cấp API kiểm tra ATS, nhưng việc gửi file sang bên thứ ba làm tăng độ trễ, tăng chi phí và vi phạm cam kết bảo vệ quyền riêng tư cá nhân (Zero Third-party Leakage) đã hứa với người dùng ở US02.
- **Quyết định:** Tự phát triển 4 bộ giả lập parser nội bộ bằng C# dựa trên phân tích không gian Bounding Box (`PdfPig`) và phân tích cây thẻ OpenXML (`DocumentFormat.OpenXml`).
- **Hệ quả:** Hoàn toàn chủ động về mặt công nghệ, tốc độ xử lý tức thời (dưới 500ms cho 4 bộ giả lập), chi phí vận hành bằng 0 và bảo vệ an toàn 100% dữ liệu CV của khách hàng.

---

## 7. Bàn giao sang Cổng G4 (Delivery Planning Sprint 2)

Hồ sơ thiết kế kỹ thuật này đã hoàn tất toàn diện:
1. **Database Schema:** Bảng mới `dbo.AtsSimulations` với ràng buộc toàn vẹn `ON DELETE CASCADE`.
2. **Sơ đồ ERD:** Đã render trực quan tại `docs/workflow/diagrams/ats-simulation-erd.html`.
3. **Thuật toán & Simulators:** 4 bộ giả lập bóc tách giải quyết triệt để các lỗi Workday 2 cột, Taleo header stripping và Greenhouse mixed PDF.
4. **Hợp đồng API:** 3 endpoints mới hỗ trợ đầy đủ `US21` – `US25`.
5. **Kế hoạch G4:** Sẵn sàng để chuyển sang Cổng G4 phân rã các task cards cho Sprint 2.
