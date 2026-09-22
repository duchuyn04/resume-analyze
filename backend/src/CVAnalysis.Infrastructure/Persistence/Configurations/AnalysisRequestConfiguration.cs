using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class AnalysisRequestConfiguration : IEntityTypeConfiguration<AnalysisRequest>
{
    public void Configure(EntityTypeBuilder<AnalysisRequest> builder)
    {
        builder.ToTable("AnalysisRequests", "dbo");

        builder.HasKey(a => a.Id).IsClustered();
        builder.Property(a => a.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(a => a.UserId)
            .HasColumnName("user_id");

        builder.Property(a => a.CVFileId)
            .HasColumnName("cv_file_id");

        builder.Property(a => a.JobDescriptionId)
            .HasColumnName("job_description_id");

        builder.Property(a => a.AnalysisType)
            .HasColumnName("analysis_type")
            .HasMaxLength(32)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(a => a.Status)
            .HasColumnName("status")
            .HasMaxLength(32)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(a => a.ContentHash)
            .HasColumnName("content_hash")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(a => a.TimeoutAt)
            .HasColumnName("timeout_at")
            .IsRequired();

        builder.Property(a => a.PartialResultExpiresAt)
            .HasColumnName("partial_result_expires_at");

        builder.Property(a => a.ErrorMessage)
            .HasColumnName("error_message")
            .HasMaxLength(1024);

        builder.Property(a => a.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.Property(a => a.CompletedAt)
            .HasColumnName("completed_at");

        // Relationships
        builder.HasOne(a => a.User)
            .WithMany(u => u.AnalysisRequests)
            .HasForeignKey(a => a.UserId)
            .HasConstraintName("FK_AnalysisRequests_Users")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.CVFile)
            .WithMany(f => f.AnalysisRequests)
            .HasForeignKey(a => a.CVFileId)
            .HasConstraintName("FK_AnalysisRequests_CVFiles")
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(a => a.JobDescription)
            .WithMany(j => j.AnalysisRequests)
            .HasForeignKey(a => a.JobDescriptionId)
            .HasConstraintName("FK_AnalysisRequests_JobDescriptions")
            .OnDelete(DeleteBehavior.NoAction);

        // Indexes
        builder.HasIndex(a => new { a.Status, a.TimeoutAt })
            .HasDatabaseName("IX_AnalysisRequests_status_timeout");

        builder.HasIndex(a => new { a.UserId, a.ContentHash, a.Status })
            .HasDatabaseName("IX_AnalysisRequests_dedup");
    }
}
