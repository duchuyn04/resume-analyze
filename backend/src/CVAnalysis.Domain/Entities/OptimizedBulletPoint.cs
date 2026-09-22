using CVAnalysis.Domain.Enums;

namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.OptimizedBulletPoints — Bản viết lại Action-Context-Metric & Diff view (BR06, BR14).
/// </summary>
public class OptimizedBulletPoint
{
    public Guid Id { get; set; }
    public Guid ReportId { get; set; }
    public string SectionName { get; set; } = string.Empty;
    public string OriginalText { get; set; } = string.Empty;
    public string SuggestedText { get; set; } = string.Empty;
    public string? CustomizedText { get; set; }
    public OptimizedBulletPointStatus Status { get; set; } = OptimizedBulletPointStatus.Pending;
    public DateTimeOffset CreatedAt { get; set; }

    // Navigation
    public Report Report { get; set; } = null!;
}
