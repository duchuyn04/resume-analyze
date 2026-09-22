using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class CreditLedgerConfiguration : IEntityTypeConfiguration<CreditLedger>
{
    public void Configure(EntityTypeBuilder<CreditLedger> builder)
    {
        builder.ToTable("CreditLedger", "dbo");

        builder.HasKey(c => c.Id).IsClustered();
        builder.Property(c => c.Id)
            .HasColumnName("id")
            .ValueGeneratedOnAdd();

        builder.Property(c => c.UserId)
            .HasColumnName("user_id");

        builder.Property(c => c.AnalysisRequestId)
            .HasColumnName("analysis_request_id");

        builder.Property(c => c.PaymentOrderId)
            .HasColumnName("payment_order_id");

        builder.Property(c => c.EntryType)
            .HasColumnName("entry_type")
            .HasMaxLength(32)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(c => c.CreditType)
            .HasColumnName("credit_type")
            .HasMaxLength(16)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(c => c.Amount)
            .HasColumnName("amount")
            .IsRequired();

        builder.Property(c => c.BalanceBefore)
            .HasColumnName("balance_before")
            .IsRequired();

        builder.Property(c => c.BalanceAfter)
            .HasColumnName("balance_after")
            .IsRequired();

        builder.Property(c => c.Description)
            .HasColumnName("description")
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        // Relationship
        builder.HasOne(c => c.User)
            .WithMany(u => u.CreditLedgers)
            .HasForeignKey(c => c.UserId)
            .HasConstraintName("FK_CreditLedger_Users")
            .OnDelete(DeleteBehavior.NoAction);

        // Index
        builder.HasIndex(c => new { c.UserId, c.CreatedAt })
            .HasDatabaseName("IX_CreditLedger_user_created");
    }
}
