using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class SupportAccessGrantConfiguration : IEntityTypeConfiguration<SupportAccessGrant>
{
    public void Configure(EntityTypeBuilder<SupportAccessGrant> builder)
    {
        builder.ToTable("SupportAccessGrants", "dbo");

        builder.HasKey(g => g.Id).IsClustered();
        builder.Property(g => g.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(g => g.TicketId)
            .HasColumnName("ticket_id");

        builder.Property(g => g.CVFileId)
            .HasColumnName("cv_file_id");

        builder.Property(g => g.GrantedByUserId)
            .HasColumnName("granted_by_user_id");

        builder.Property(g => g.GrantedToUserId)
            .HasColumnName("granted_to_user_id");

        builder.Property(g => g.GrantedAt)
            .HasColumnName("granted_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.Property(g => g.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired();

        builder.Property(g => g.RevokedAt)
            .HasColumnName("revoked_at");

        // Relationships
        builder.HasOne(g => g.SupportTicket)
            .WithMany(s => s.SupportAccessGrants)
            .HasForeignKey(g => g.TicketId)
            .HasConstraintName("FK_SupportAccessGrants_Tickets")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(g => g.CVFile)
            .WithMany(f => f.SupportAccessGrants)
            .HasForeignKey(g => g.CVFileId)
            .HasConstraintName("FK_SupportAccessGrants_CVFiles")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(g => g.GrantedByUser)
            .WithMany()
            .HasForeignKey(g => g.GrantedByUserId)
            .HasConstraintName("FK_SupportAccessGrants_GrantedByUser")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(g => g.GrantedToUser)
            .WithMany()
            .HasForeignKey(g => g.GrantedToUserId)
            .HasConstraintName("FK_SupportAccessGrants_GrantedToUser")
            .OnDelete(DeleteBehavior.NoAction);

        // Filtered Index
        builder.HasIndex(g => new { g.TicketId, g.GrantedToUserId, g.ExpiresAt })
            .HasDatabaseName("IX_SupportAccessGrants_lookup")
            .HasFilter("[revoked_at] IS NULL");
    }
}
