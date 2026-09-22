using CVAnalysis.Domain.Enums;

namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.Users — Quản lý tài khoản người dùng và 5 roles hệ thống.
/// </summary>
public class User
{
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public bool IsEmailVerified { get; set; }
    public DateTimeOffset? EmailVerifiedAt { get; set; }
    public bool IsActive { get; set; } = true;
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset UpdatedAt { get; set; }

    // Navigations
    public UserBalance? UserBalance { get; set; }
    public ICollection<CVFile> CVFiles { get; set; } = new List<CVFile>();
    public ICollection<JobDescription> JobDescriptions { get; set; } = new List<JobDescription>();
    public ICollection<AnalysisRequest> AnalysisRequests { get; set; } = new List<AnalysisRequest>();
    public ICollection<PaymentOrder> PaymentOrders { get; set; } = new List<PaymentOrder>();
    public ICollection<CreditLedger> CreditLedgers { get; set; } = new List<CreditLedger>();
    public ICollection<SupportTicket> SupportTickets { get; set; } = new List<SupportTicket>();
}
