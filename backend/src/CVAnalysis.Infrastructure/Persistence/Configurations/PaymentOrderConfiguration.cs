using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class PaymentOrderConfiguration : IEntityTypeConfiguration<PaymentOrder>
{
    public void Configure(EntityTypeBuilder<PaymentOrder> builder)
    {
        builder.ToTable("PaymentOrders", "dbo");

        builder.HasKey(p => p.Id).IsClustered();
        builder.Property(p => p.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(p => p.OrderCode)
            .HasColumnName("order_code")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(p => p.UserId)
            .HasColumnName("user_id");

        builder.Property(p => p.PackageId)
            .HasColumnName("package_id");

        builder.Property(p => p.CreditsAmount)
            .HasColumnName("credits_amount")
            .IsRequired();

        builder.Property(p => p.AmountVnd)
            .HasColumnName("amount_vnd")
            .HasColumnType("decimal(18,2)")
            .IsRequired();

        builder.Property(p => p.Status)
            .HasColumnName("status")
            .HasMaxLength(32)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(p => p.GatewayTransactionId)
            .HasColumnName("gateway_transaction_id")
            .HasMaxLength(128);

        builder.Property(p => p.ReconciliationNotes)
            .HasColumnName("reconciliation_notes")
            .HasMaxLength(512);

        builder.Property(p => p.ReconciledByUserId)
            .HasColumnName("reconciled_by_user_id");

        builder.Property(p => p.ReconciledAt)
            .HasColumnName("reconciled_at");

        builder.Property(p => p.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired();

        builder.Property(p => p.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.Property(p => p.CompletedAt)
            .HasColumnName("completed_at");

        // Relationships
        builder.HasOne(p => p.User)
            .WithMany(u => u.PaymentOrders)
            .HasForeignKey(p => p.UserId)
            .HasConstraintName("FK_PaymentOrders_Users")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.CreditPackage)
            .WithMany(c => c.PaymentOrders)
            .HasForeignKey(p => p.PackageId)
            .HasConstraintName("FK_PaymentOrders_CreditPackages")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(p => p.ReconciledByUser)
            .WithMany()
            .HasForeignKey(p => p.ReconciledByUserId)
            .HasConstraintName("FK_PaymentOrders_ReconciledByUser")
            .OnDelete(DeleteBehavior.NoAction);

        // Indexes
        builder.HasIndex(p => p.OrderCode)
            .IsUnique()
            .HasDatabaseName("UQ_PaymentOrders_code");

        builder.HasIndex(p => new { p.Status, p.ExpiresAt })
            .HasDatabaseName("IX_PaymentOrders_reconciliation");
    }
}
