using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class JobDescriptionConfiguration : IEntityTypeConfiguration<JobDescription>
{
    public void Configure(EntityTypeBuilder<JobDescription> builder)
    {
        builder.ToTable("JobDescriptions", "dbo");

        builder.HasKey(j => j.Id).IsClustered();
        builder.Property(j => j.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(j => j.UserId)
            .HasColumnName("user_id");

        builder.Property(j => j.Title)
            .HasColumnName("title")
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(j => j.RawText)
            .HasColumnName("raw_text")
            .IsRequired();

        builder.Property(j => j.WordCount)
            .HasColumnName("word_count")
            .IsRequired();

        builder.Property(j => j.MinRequirementsMet)
            .HasColumnName("min_requirements_met")
            .IsRequired();

        builder.Property(j => j.ExtractedSkillsJson)
            .HasColumnName("extracted_skills_json");

        builder.Property(j => j.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.HasOne(j => j.User)
            .WithMany(u => u.JobDescriptions)
            .HasForeignKey(j => j.UserId)
            .HasConstraintName("FK_JobDescriptions_Users")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(j => new { j.UserId, j.CreatedAt })
            .HasDatabaseName("IX_JobDescriptions_user_created");
    }
}
