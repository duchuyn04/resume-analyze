namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.UserBalances — Bất biến số dư & Concurrency RowVersion (BR04, BR13).
/// </summary>
public class UserBalance
{
    public Guid UserId { get; set; }
    public int FreeTrialCredits { get; set; } = 3;
    public int PaidCredits { get; set; } = 0;
    public int HeldCredits { get; set; } = 0;
    public byte[] RowVersion { get; set; } = Array.Empty<byte>();
    public DateTimeOffset UpdatedAt { get; set; }

    // Navigation
    public User User { get; set; } = null!;
}
