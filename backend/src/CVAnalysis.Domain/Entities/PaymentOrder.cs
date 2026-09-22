using CVAnalysis.Domain.Enums;

namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.PaymentOrders — Đơn hàng mua credit 15 phút & chờ đối soát (BR11, OQ05).
/// </summary>
public class PaymentOrder
{
    public Guid Id { get; set; }
    public string OrderCode { get; set; } = string.Empty;
    public Guid UserId { get; set; }
    public Guid PackageId { get; set; }
    public int CreditsAmount { get; set; }
    public decimal AmountVnd { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public string? GatewayTransactionId { get; set; }
    public string? ReconciliationNotes { get; set; }
    public Guid? ReconciledByUserId { get; set; }
    public DateTimeOffset? ReconciledAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? CompletedAt { get; set; }

    // Navigations
    public User User { get; set; } = null!;
    public CreditPackage CreditPackage { get; set; } = null!;
    public User? ReconciledByUser { get; set; }
}
