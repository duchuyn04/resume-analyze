namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.AuditLogs — Nhật ký bảo mật bất biến (Zero-Trust Audit Log, US20).
/// </summary>
public class AuditLog
{
    public long Id { get; set; }
    public Guid ActorUserId { get; set; }
    public string ActorRole { get; set; } = string.Empty;
    public string Action { get; set; } = string.Empty;
    public string TargetEntity { get; set; } = string.Empty;
    public string TargetEntityId { get; set; } = string.Empty;
    public string IpAddress { get; set; } = string.Empty;
    public string? DetailsJson { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
}
