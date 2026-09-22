using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class UserBalanceConfiguration : IEntityTypeConfiguration<UserBalance>
{
    public void Configure(EntityTypeBuilder<UserBalance> builder)
    {
        builder.ToTable("UserBalances", "dbo");

        builder.HasKey(b => b.UserId).IsClustered();
        builder.Property(b => b.UserId)
            .HasColumnName("user_id");

        builder.Property(b => b.FreeTrialCredits)
            .HasColumnName("free_trial_credits")
            .HasDefaultValue(3);

        builder.Property(b => b.PaidCredits)
            .HasColumnName("paid_credits")
            .HasDefaultValue(0);

        builder.Property(b => b.HeldCredits)
            .HasColumnName("held_credits")
            .HasDefaultValue(0);

        builder.Property(b => b.RowVersion)
            .HasColumnName("row_version")
            .IsRowVersion();

        builder.Property(b => b.UpdatedAt)
            .HasColumnName("updated_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.HasOne(b => b.User)
            .WithOne(u => u.UserBalance)
            .HasForeignKey<UserBalance>(b => b.UserId)
            .HasConstraintName("FK_UserBalances_Users")
            .OnDelete(DeleteBehavior.Cascade);
    }
}
