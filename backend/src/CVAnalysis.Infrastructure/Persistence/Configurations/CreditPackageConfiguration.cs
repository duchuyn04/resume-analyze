using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class CreditPackageConfiguration : IEntityTypeConfiguration<CreditPackage>
{
    public void Configure(EntityTypeBuilder<CreditPackage> builder)
    {
        builder.ToTable("CreditPackages", "dbo");

        builder.HasKey(c => c.Id).IsClustered();
        builder.Property(c => c.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(c => c.PackageName)
            .HasColumnName("package_name")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(c => c.CreditsAmount)
            .HasColumnName("credits_amount")
            .IsRequired();

        builder.Property(c => c.PriceVnd)
            .HasColumnName("price_vnd")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(c => c.IsActive)
            .HasColumnName("is_active")
            .HasDefaultValue(true);

        builder.Property(c => c.CreatedByUserId)
            .HasColumnName("created_by_user_id");

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        // Relationship
        builder.HasOne(c => c.CreatedByUser)
            .WithMany()
            .HasForeignKey(c => c.CreatedByUserId)
            .HasConstraintName("FK_CreditPackages_Users")
            .OnDelete(DeleteBehavior.NoAction);
    }
}
