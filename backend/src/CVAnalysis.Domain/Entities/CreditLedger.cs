using CVAnalysis.Domain.Enums;

namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.CreditLedger — Sổ cái bất biến ghi nhận biến động số dư (BR04, BR13, BR37).
/// </summary>
public class CreditLedger
{
    public long Id { get; set; }
    public Guid UserId { get; set; }
    public Guid? AnalysisRequestId { get; set; }
    public Guid? PaymentOrderId { get; set; }
    public LedgerEntryType EntryType { get; set; }
    public CreditType CreditType { get; set; }
    public int Amount { get; set; }
    public int BalanceBefore { get; set; }
    public int BalanceAfter { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTimeOffset CreatedAt { get; set; }

    // Navigation
    public User User { get; set; } = null!;
}
