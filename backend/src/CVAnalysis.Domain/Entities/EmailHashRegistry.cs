namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.EmailHashRegistry — Chống lạm dụng lượt dùng thử 12 tháng (BR34).
/// </summary>
public class EmailHashRegistry
{
    public string EmailHash { get; set; } = string.Empty;
    public DateTimeOffset DeletedAt { get; set; }
    public DateTimeOffset TrialBlockedUntil { get; set; }
}
