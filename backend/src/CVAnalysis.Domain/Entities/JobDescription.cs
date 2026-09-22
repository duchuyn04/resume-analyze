namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.JobDescriptions — Mô tả tuyển dụng & kiểm tra căn cứ (BR35, Mục 5.1).
/// </summary>
public class JobDescription
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string RawText { get; set; } = string.Empty;
    public int WordCount { get; set; }
    public bool MinRequirementsMet { get; set; }
    public string? ExtractedSkillsJson { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    // Navigations
    public User User { get; set; } = null!;
    public ICollection<AnalysisRequest> AnalysisRequests { get; set; } = new List<AnalysisRequest>();
}
