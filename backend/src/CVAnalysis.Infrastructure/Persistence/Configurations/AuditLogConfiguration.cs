using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class AuditLogConfiguration : IEntityTypeConfiguration<AuditLog>
{
    public void Configure(EntityTypeBuilder<AuditLog> builder)
    {
        builder.ToTable("AuditLogs", "dbo");

        builder.HasKey(a => a.Id).IsClustered();
        builder.Property(a => a.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(a => a.ActorUserId)
            .HasColumnName("actor_user_id");

        builder.Property(a => a.ActorRole)
            .HasColumnName("actor_role")
            .HasMaxLength(32)
            .IsRequired();

        builder.Property(a => a.Action)
            .HasColumnName("action")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(a => a.TargetEntity)
            .HasColumnName("target_entity")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(a => a.TargetEntityId)
            .HasColumnName("target_entity_id")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(a => a.IpAddress)
            .HasColumnName("ip_address")
            .HasMaxLength(45)
            .IsRequired();

        builder.Property(a => a.DetailsJson)
            .HasColumnName("details_json");

        builder.Property(a => a.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        // Index
        builder.HasIndex(a => new { a.ActorUserId, a.CreatedAt })
            .HasDatabaseName("IX_AuditLogs_actor_created");
    }
}
