using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class CompensationRequestConfiguration : IEntityTypeConfiguration<CompensationRequest>
{
    public void Configure(EntityTypeBuilder<CompensationRequest> builder)
    {
        builder.ToTable("CompensationRequests", "dbo");

        builder.HasKey(c => c.Id).IsClustered();
        builder.Property(c => c.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(c => c.TicketId)
            .HasColumnName("ticket_id");

        builder.Property(c => c.ProposedByUserId)
            .HasColumnName("proposed_by_user_id");

        builder.Property(c => c.ApprovedByUserId)
            .HasColumnName("approved_by_user_id");

        builder.Property(c => c.CompensatedCreditType)
            .HasColumnName("compensated_credit_type")
            .HasMaxLength(16)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(c => c.CreditAmount)
            .HasColumnName("credit_amount")
            .HasDefaultValue(1);

        builder.Property(c => c.TechnicalRootCause)
            .HasColumnName("technical_root_cause")
            .IsRequired();

        builder.Property(c => c.Status)
            .HasColumnName("status")
            .HasMaxLength(32)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(c => c.DecisionNotes)
            .HasColumnName("decision_notes")
            .HasMaxLength(512);

        builder.Property(c => c.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.Property(c => c.DecidedAt)
            .HasColumnName("decided_at");

        // Relationships
        builder.HasOne(c => c.SupportTicket)
            .WithOne(s => s.CompensationRequest)
            .HasForeignKey<CompensationRequest>(c => c.TicketId)
            .HasConstraintName("FK_CompensationRequests_Tickets")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(c => c.ProposedByUser)
            .WithMany()
            .HasForeignKey(c => c.ProposedByUserId)
            .HasConstraintName("FK_CompensationRequests_ProposedByUser")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(c => c.ApprovedByUser)
            .WithMany()
            .HasForeignKey(c => c.ApprovedByUserId)
            .HasConstraintName("FK_CompensationRequests_ApprovedByUser")
            .OnDelete(DeleteBehavior.NoAction);

        // Unique Index
        builder.HasIndex(c => c.TicketId)
            .IsUnique()
            .HasDatabaseName("UQ_CompensationRequests_ticket");
    }
}
