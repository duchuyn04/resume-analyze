using CVAnalysis.Domain.Enums;

namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.AnalysisRequests — Vòng đời phân tích CV & Circuit Breaker 90s (BR04, BR17).
/// </summary>
public class AnalysisRequest
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid CVFileId { get; set; }
    public Guid? JobDescriptionId { get; set; }
    public AnalysisType AnalysisType { get; set; }
    public RequestStatus Status { get; set; }
    public string ContentHash { get; set; } = string.Empty;
    public DateTimeOffset TimeoutAt { get; set; }
    public DateTimeOffset? PartialResultExpiresAt { get; set; }
    public string? ErrorMessage { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }

    // Navigations
    public User User { get; set; } = null!;
    public CVFile CVFile { get; set; } = null!;
    public JobDescription? JobDescription { get; set; }
    public Report? Report { get; set; }
}
