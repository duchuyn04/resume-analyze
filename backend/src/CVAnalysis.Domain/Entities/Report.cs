namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.Reports — Báo cáo điểm ATS & Rubric 4 Trụ cột (BR01, BR05, BR18).
/// </summary>
public class Report
{
    public Guid Id { get; set; }
    public Guid AnalysisRequestId { get; set; }
    public int OverallScore { get; set; }
    public string ScoreLabel { get; set; } = string.Empty;
    public decimal SkillsScore { get; set; }
    public decimal ExperienceScore { get; set; }
    public decimal FormatScore { get; set; }
    public decimal? EducationScore { get; set; }
    public int NormalizedDenominator { get; set; } = 100;
    public string? MandatoryWarningsJson { get; set; }
    public string ReportDetailsJson { get; set; } = string.Empty;
    public bool IsSystemError { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }

    // Navigations
    public AnalysisRequest AnalysisRequest { get; set; } = null!;
    public ICollection<OptimizedBulletPoint> OptimizedBulletPoints { get; set; } = new List<OptimizedBulletPoint>();
    public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
}
