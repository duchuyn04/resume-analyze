using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class SupportTicketConfiguration : IEntityTypeConfiguration<SupportTicket>
{
    public void Configure(EntityTypeBuilder<SupportTicket> builder)
    {
        builder.ToTable("SupportTickets", "dbo");

        builder.HasKey(s => s.Id).IsClustered();
        builder.Property(s => s.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(s => s.TicketCode)
            .HasColumnName("ticket_code")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(s => s.UserId)
            .HasColumnName("user_id");

        builder.Property(s => s.ReportId)
            .HasColumnName("report_id");

        builder.Property(s => s.AssignedToUserId)
            .HasColumnName("assigned_to_user_id");

        builder.Property(s => s.Status)
            .HasColumnName("status")
            .HasMaxLength(32)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(s => s.IssueDescription)
            .HasColumnName("issue_description")
            .IsRequired();

        builder.Property(s => s.SlaDueAt)
            .HasColumnName("sla_due_at")
            .IsRequired();

        builder.Property(s => s.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.Property(s => s.ResolvedAt)
            .HasColumnName("resolved_at");

        // Relationships
        builder.HasOne(s => s.User)
            .WithMany(u => u.SupportTickets)
            .HasForeignKey(s => s.UserId)
            .HasConstraintName("FK_SupportTickets_Users")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(s => s.Report)
            .WithMany(r => r.SupportTickets)
            .HasForeignKey(s => s.ReportId)
            .HasConstraintName("FK_SupportTickets_Reports")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(s => s.AssignedToUser)
            .WithMany()
            .HasForeignKey(s => s.AssignedToUserId)
            .HasConstraintName("FK_SupportTickets_AssignedToUser")
            .OnDelete(DeleteBehavior.NoAction);

        // Indexes
        builder.HasIndex(s => s.TicketCode)
            .IsUnique()
            .HasDatabaseName("UQ_SupportTickets_code");

        builder.HasIndex(s => new { s.Status, s.SlaDueAt })
            .HasDatabaseName("IX_SupportTickets_status_sla");
    }
}
