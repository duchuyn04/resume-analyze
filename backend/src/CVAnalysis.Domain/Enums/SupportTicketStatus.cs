namespace CVAnalysis.Domain.Enums;

/// <summary>
/// Trạng thái yêu cầu hỗ trợ khiếu nại kỹ thuật của ứng viên.
/// </summary>
public enum SupportTicketStatus
{
    Open,
    Assigned,
    PendingLeadApproval,
    Resolved,
    Rejected
}
