using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class OptimizedBulletPointConfiguration : IEntityTypeConfiguration<OptimizedBulletPoint>
{
    public void Configure(EntityTypeBuilder<OptimizedBulletPoint> builder)
    {
        builder.ToTable("OptimizedBulletPoints", "dbo");

        builder.HasKey(b => b.Id).IsClustered();
        builder.Property(b => b.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(b => b.ReportId)
            .HasColumnName("report_id");

        builder.Property(b => b.SectionName)
            .HasColumnName("section_name")
            .HasMaxLength(128)
            .IsRequired();

        builder.Property(b => b.OriginalText)
            .HasColumnName("original_text")
            .IsRequired();

        builder.Property(b => b.SuggestedText)
            .HasColumnName("suggested_text")
            .IsRequired();

        builder.Property(b => b.CustomizedText)
            .HasColumnName("customized_text");

        builder.Property(b => b.Status)
            .HasColumnName("status")
            .HasMaxLength(32)
            .HasConversion<string>()
            .IsRequired();

        builder.Property(b => b.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        // Relationship
        builder.HasOne(b => b.Report)
            .WithMany(r => r.OptimizedBulletPoints)
            .HasForeignKey(b => b.ReportId)
            .HasConstraintName("FK_OptimizedBulletPoints_Reports")
            .OnDelete(DeleteBehavior.Cascade);

        // Index
        builder.HasIndex(b => b.ReportId)
            .HasDatabaseName("IX_OptimizedBulletPoints_report");
    }
}
