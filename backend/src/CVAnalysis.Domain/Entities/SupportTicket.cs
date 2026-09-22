using CVAnalysis.Domain.Enums;

namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.SupportTickets — Khiếu nại lỗi kỹ thuật & giám sát SLA 2 ngày.
/// </summary>
public class SupportTicket
{
    public Guid Id { get; set; }
    public string TicketCode { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid ReportId { get; set; }
    public Guid? AssignedToUserId { get; set; }
    public SupportTicketStatus Status { get; set; } = SupportTicketStatus.Open;
    public string IssueDescription { get; set; } = string.Empty;
    public DateTimeOffset SlaDueAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? ResolvedAt { get; set; }

    // Navigations
    public User User { get; set; } = null!;
    public Report Report { get; set; } = null!;
    public User? AssignedToUser { get; set; }
    public ICollection<SupportAccessGrant> SupportAccessGrants { get; set; } = new List<SupportAccessGrant>();
    public CompensationRequest? CompensationRequest { get; set; }
}
