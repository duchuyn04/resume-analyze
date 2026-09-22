using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class EmailHashRegistryConfiguration : IEntityTypeConfiguration<EmailHashRegistry>
{
    public void Configure(EntityTypeBuilder<EmailHashRegistry> builder)
    {
        builder.ToTable("EmailHashRegistry", "dbo");

        builder.HasKey(e => e.EmailHash).IsClustered();
        builder.Property(e => e.EmailHash)
            .HasColumnName("email_hash")
            .HasMaxLength(128);

        builder.Property(e => e.DeletedAt)
            .HasColumnName("deleted_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.Property(e => e.TrialBlockedUntil)
            .HasColumnName("trial_blocked_until");

        builder.HasIndex(e => e.TrialBlockedUntil)
            .HasDatabaseName("IX_EmailHashRegistry_blocked_until");
    }
}
