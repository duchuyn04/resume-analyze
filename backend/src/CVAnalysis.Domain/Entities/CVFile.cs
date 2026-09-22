namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.CVFiles — Quản lý file CV tải lên và dữ liệu trích xuất cấu trúc (BR03, BR21).
/// </summary>
public class CVFile
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string FileName { get; set; } = string.Empty;
    public long FileSizeBytes { get; set; }
    public int PageCount { get; set; }
    public string FilePath { get; set; } = string.Empty;
    public string RawText { get; set; } = string.Empty;
    public string ExtractedData { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public bool IsDeleted { get; set; }

    // Navigations
    public User User { get; set; } = null!;
    public ICollection<AnalysisRequest> AnalysisRequests { get; set; } = new List<AnalysisRequest>();
    public ICollection<SupportAccessGrant> SupportAccessGrants { get; set; } = new List<SupportAccessGrant>();
}
