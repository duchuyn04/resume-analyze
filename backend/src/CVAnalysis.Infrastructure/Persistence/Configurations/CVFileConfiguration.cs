using CVAnalysis.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CVAnalysis.Infrastructure.Persistence.Configurations;

public class CVFileConfiguration : IEntityTypeConfiguration<CVFile>
{
    public void Configure(EntityTypeBuilder<CVFile> builder)
    {
        builder.ToTable("CVFiles", "dbo");

        builder.HasKey(f => f.Id).IsClustered();
        builder.Property(f => f.Id)
            .HasColumnName("id")
            .HasDefaultValueSql("NEWSEQUENTIALID()");

        builder.Property(f => f.UserId)
            .HasColumnName("user_id");

        builder.Property(f => f.FileName)
            .HasColumnName("file_name")
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(f => f.FileSizeBytes)
            .HasColumnName("file_size_bytes")
            .IsRequired();

        builder.Property(f => f.PageCount)
            .HasColumnName("page_count")
            .IsRequired();

        builder.Property(f => f.FilePath)
            .HasColumnName("file_path")
            .HasMaxLength(512)
            .IsRequired();

        builder.Property(f => f.RawText)
            .HasColumnName("raw_text")
            .IsRequired();

        builder.Property(f => f.ExtractedData)
            .HasColumnName("extracted_data")
            .IsRequired();

        builder.Property(f => f.CreatedAt)
            .HasColumnName("created_at")
            .HasDefaultValueSql("SYSDATETIMEOFFSET()");

        builder.Property(f => f.ExpiresAt)
            .HasColumnName("expires_at")
            .IsRequired();

        builder.Property(f => f.IsDeleted)
            .HasColumnName("is_deleted")
            .HasDefaultValue(false);

        builder.HasOne(f => f.User)
            .WithMany(u => u.CVFiles)
            .HasForeignKey(f => f.UserId)
            .HasConstraintName("FK_CVFiles_Users")
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasIndex(f => new { f.UserId, f.ExpiresAt })
            .HasDatabaseName("IX_CVFiles_user_expires")
            .HasFilter("[is_deleted] = 0");
    }
}
