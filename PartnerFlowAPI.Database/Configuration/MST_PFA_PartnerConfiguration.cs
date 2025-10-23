using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Database.Entities;

namespace PartnerFlowAPI.Database.Configuration
{
    public class MST_PFA_PartnerConfiguration : IEntityTypeConfiguration<MST_PFA_Partner>
    {
        public void Configure(EntityTypeBuilder<MST_PFA_Partner> builder)
        {
            // Table mapping
            builder.ToTable("MST_PFA_Partner");

            // Primary Key
            builder.HasKey(p => p.PartnerID);

            // Columns
            builder.Property(p => p.PartnerID)
                   .HasColumnName("PartnerID")
                   .IsRequired();

            builder.Property(p => p.Code)
                   .HasColumnName("Code")
                   .HasMaxLength(50)
                   .IsRequired()
                   .IsUnicode(false); // varchar

            builder.Property(p => p.Name)
                   .HasColumnName("Name")
                   .HasMaxLength(200)
                   .IsRequired()
                   .IsUnicode(false); // varchar

            builder.Property(p => p.APIKey)
                   .HasColumnName("APIKey")
                   .HasMaxLength(1000)
                   .IsUnicode(true); // nvarchar

            builder.Property(p => p.RedirectionURL)
                   .HasColumnName("RedirectionURL")
                   .HasMaxLength(1000)
                   .IsRequired()
                   .IsUnicode(true);

            builder.Property(p => p.GCRedirectionURL)
                   .HasColumnName("GCRedirectionURL")
                   .HasMaxLength(1000)
                   .IsRequired()
                   .IsUnicode(true);

            builder.Property(p => p.IsActive)
                   .HasColumnName("IsActive")
                   .IsRequired();

            builder.Property(p => p.IsDeleted)
                   .HasColumnName("IsDeleted")
                   .IsRequired();

            builder.Property(p => p.DeletedDate)
                   .HasColumnName("DeletedDate");

            builder.Property(p => p.CreatedDate)
                   .HasColumnName("CreatedDate")
                   .IsRequired();

            builder.Property(p => p.CreatedBy)
                   .HasColumnName("CreatedBy")
                   .HasMaxLength(50)
                   .IsRequired()
                   .IsUnicode(false);

            builder.Property(p => p.ModifiedDate)
                   .HasColumnName("ModifiedDate");

            builder.Property(p => p.ModifiedBy)
                   .HasColumnName("ModifiedBy")
                   .HasMaxLength(50)
                   .IsUnicode(false);


            builder.Property(p => p.PartnerSource)
                   .HasColumnName("PartnerSource")
                   .HasMaxLength(200)
                   .IsRequired()
                   .IsUnicode(false);

            builder.Property(p => p.vcPartnerType)
                   .HasColumnName("vcPartnerType")
                   .HasMaxLength(200)
                   .IsRequired()
                   .IsUnicode(false);
        }
    }
}
