using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourNamespace.Entities;

namespace YourNamespace.Configurations
{
    public class Tblpf_SummaryDetailsConfiguration : IEntityTypeConfiguration<Tblpf_SummaryDetails>
    {
        public void Configure(EntityTypeBuilder<Tblpf_SummaryDetails> builder)
        {
            builder.ToTable("Tblpf_SummaryDetails");

            // Primary Key
            builder.HasKey(e => e.intSummaryDeatils)
                   .HasName("PK_Tblpf_SummaryDetails");

            // Indexes
            builder.HasIndex(e => e.vcApplicationNumber)
                   .HasDatabaseName("Tblpf_SummaryDetails_ApplicationNumber");

            builder.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                   .HasDatabaseName("Tblpf_SummaryDetails_IsDeleted_DeletedDate");

            // Column Configurations
            builder.Property(e => e.intSummaryDeatils)
                   .HasColumnName("intSummaryDeatils")
                   .IsRequired();

            builder.Property(e => e.vcApplicationNumber)
                   .HasColumnName("vcApplicationNumber")
                   .HasMaxLength(200)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcSummary)
                   .HasColumnName("vcSummary")
                   .HasMaxLength(8000)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.vcLastAccessIP)
                   .HasColumnName("vcLastAccessIP")
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(e => e.vcCreatedBy)
                   .HasColumnName("vcCreatedBy")
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired();

            builder.Property(e => e.dtCreateDate)
                   .HasColumnName("dtCreateDate")
                   .HasColumnType("datetime2")
                   .IsRequired();

            builder.Property(e => e.vcModifiedBy)
                   .HasColumnName("vcModifiedBy")
                   .HasMaxLength(100)
                   .IsUnicode()
                   .IsRequired(false);

            builder.Property(e => e.dtModifiedDate)
                   .HasColumnName("dtModifiedDate")
                   .HasColumnType("datetime2")
                   .IsRequired(false);

            builder.Property(e => e.dtDeletedDate)
                   .HasColumnName("dtDeletedDate")
                   .HasColumnType("datetime2")
                   .IsRequired(false);

            builder.Property(e => e.bitIsDeleted);
                   
        }
    }
}
