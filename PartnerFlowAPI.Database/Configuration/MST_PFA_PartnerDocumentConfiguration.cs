using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Configuration
{

    public class MST_PFA_PartnerDocumentConfiguration : IEntityTypeConfiguration<Entities.MST_PFA_PartnerDocument>
    {
        public void Configure(EntityTypeBuilder<Entities.MST_PFA_PartnerDocument> builder)
        {
            builder.ToTable("MST_PFA_Partner_Document");

            builder.HasKey(p => p.PartnerDocumentID);

            builder.Property(p => p.PartnerDocumentID)
                   .HasColumnName("intPartnerDocumentID")
                   .IsRequired();

            builder.Property(p => p.PartnerID)
                   .HasColumnName("intPartnerID")
                   .IsRequired();

            builder.Property(p => p.DocumentTypeID)
                   .HasColumnName("intDocumentTypeID")
                   .IsRequired();

            builder.Property(p => p.LastAccessIP)
                   .HasColumnName("vcLastAccessIP")
                   .HasMaxLength(50)
                   .IsUnicode(true);

            builder.Property(p => p.CreatedBy)
                   .HasColumnName("vcCreatedBy")
                   .HasMaxLength(50)
                   .IsUnicode(true);

            builder.Property(p => p.CreatedDate)
                   .HasColumnName("dtCreatedDate")
                   .IsRequired()
                   .HasDefaultValueSql("getdate()");

            builder.Property(p => p.ModifiedBy)
                   .HasColumnName("vcModifiedBy")
                   .HasMaxLength(50)
                   .IsUnicode(true);

            builder.Property(p => p.ModifiedDate)
                   .HasColumnName("dtModifiedDate");

            builder.Property(p => p.DeletedBy)
                   .HasColumnName("vdDeletedBy")
                   .HasMaxLength(50)
                   .IsUnicode(true);

            builder.Property(p => p.DeletedDate)
                   .HasColumnName("vcDeletedDate");

            builder.Property(p => p.IsDeleted)
                   .HasColumnName("bitIsDeleted")
                   .IsRequired()
                   .HasDefaultValue(false);
        }
    }

}
