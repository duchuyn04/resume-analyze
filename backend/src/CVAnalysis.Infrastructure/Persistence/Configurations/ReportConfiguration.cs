using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class ReportConfiguration : IEntityTypeConfiguration<Report>
{
    public void Configure(EntityTypeBuilder<Report> builder)
    {
        builder.ToTable("Reports", "dbo");

        builder.HasKey(r => r.Id).IsClustered();
        builder.Property(r => r.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(r => r.AnalysisRequestId)
            .HasColumnName("analysis_request_id");

        builder.Property(r => r.OverallScore)
            .HasColumnName("overall_score")
            .IsRequired();

        builder.Property(r => r.ScoreLabel)
            .HasColumnName("score_label")
            .HasMaxLength(64)
            .IsRequired();

        builder.Property(r => r.SkillsScore)
            .HasColumnName("skills_score")
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Property(r => r.ExperienceScore)
            .HasColumnName("experience_score")
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Property(r => r.FormatScore)
            .HasColumnName("format_score")
            .HasColumnType("decimal(5,2)")
            .IsRequired();

        builder.Property(r => r.EducationScore)
            .HasColumnName("education_score")
            .HasColumnType("decimal(5,2)");

        builder.Property(r => r.NormalizedDenominator)
            .HasColumnName("normalized_denominator")
            .IsRequired();

        builder.Property(r => r.MandatoryWarningsJson)
            .HasColumnName("mandatory_warnings_json");

        builder.Property(r => r.ReportDetailsJson)
            .HasColumnName("report_details_json")
            .IsRequired();

        builder.Property(r => r.IsSystemError)
            .HasColumnName("is_system_error")
            .HasDefaultValue(false);

        builder.Property(r => r.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.Property(r => r.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired();

        // Relationship
        builder.HasOne(r => r.AnalysisRequest)
            .WithOne(a => a.Report)
            .HasForeignKey<Report>(r => r.AnalysisRequestId)
            .HasConstraintName("FK_Reports_AnalysisRequests")
            .OnDelete(DeleteBehavior.Cascade);

        // Indexes
        builder.HasIndex(r => r.AnalysisRequestId)
            .IsUnique()
            .HasDatabaseName("UQ_Reports_analysis_request");

        builder.HasIndex(r => r.ExpiresAt)
            .HasDatabaseName("IX_Reports_expires")
            .HasFilter("[is_system_error] = 0");
    }
}
