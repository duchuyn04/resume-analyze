using CVAnalysis.Domain.Enums;

namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.CompensationRequests — Phiếu đề nghị đền bù credit do lỗi kỹ thuật hệ thống (BR37).
/// </summary>
public class CompensationRequest
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public Guid ProposedByUserId { get; set; }
    public Guid? ApprovedByUserId { get; set; }
    public CreditType CompensatedCreditType { get; set; } = CreditType.Free;
    public int CreditAmount { get; set; } = 1;
    public string TechnicalRootCause { get; set; } = string.Empty;
    public CompensationStatus Status { get; set; } = CompensationStatus.PendingApproval;
    public string? DecisionNotes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? DecidedAt { get; set; }

    // Navigations
    public SupportTicket SupportTicket { get; set; } = null!;
    public User ProposedByUser { get; set; } = null!;
    public User? ApprovedByUser { get; set; }
}
