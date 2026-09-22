namespace CVAnalysis.Domain.Entities;

/// <summary>
/// Bảng dbo.SupportAccessGrants — Quyền xem CV tạm thời tối đa 72h theo nguyên tắc Zero-Trust (BR07, BR22).
/// </summary>
public class SupportAccessGrant
{
    public Guid Id { get; set; }
    public Guid TicketId { get; set; }
    public Guid CVFileId { get; set; }
    public Guid GrantedByUserId { get; set; }
    public Guid GrantedToUserId { get; set; }
    public DateTimeOffset GrantedAt { get; set; }
    public DateTimeOffset ExpiresAt { get; set; }
    public DateTimeOffset? RevokedAt { get; set; }

    // Navigations
    public SupportTicket SupportTicket { get; set; } = null!;
    public CVFile CVFile { get; set; } = null!;
    public User GrantedByUser { get; set; } = null!;
    public User GrantedToUser { get; set; } = null!;
}
