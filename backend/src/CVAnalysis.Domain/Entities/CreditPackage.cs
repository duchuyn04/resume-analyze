namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.CreditPackages — Cấu hình bảng giá credit VND động (Role Finance quản lý, BR28).
/// </summary>
public class CreditPackage
{
    public Guid Id { get; set; }
    public string PackageName { get; set; } = string.Empty;
    public int CreditsAmount { get; set; }
    public decimal PriceVnd { get; set; }
    public bool IsActive { get; set; } = true;
    public Guid CreatedByUserId { get; set; }
    public DateTimeOffset CreatedAt { get; set; }

    // Navigations
    public User CreatedByUser { get; set; } = null!;
    public ICollection<PaymentOrder> PaymentOrders { get; set; } = new List<PaymentOrder>();
}
