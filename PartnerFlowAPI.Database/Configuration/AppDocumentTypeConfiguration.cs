using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PartnerFlowAPI.Database.Entities;
using PartnerFlowAPI.Database.Enums;


namespace PartnerFlowAPI.Database.Configuration
{
    public class AppDocumentTypeConfiguration : IEntityTypeConfiguration<AppDocumentType>
    {
        public void Configure(EntityTypeBuilder<AppDocumentType> entity)
        {
            entity.ToTable("mstDocumentType");
            entity.HasKey(e => e.IntDocumentTypeID);

            entity.Property(e => e.intCommonTypeId)
                .IsRequired(false);

            entity.Property(e => e.VcDocumentTypeName).HasColumnType("nvarchar")
                .HasMaxLength(300);

            entity.Property(e => e.intAssureType)
            .HasConversion(new EnumToNumberConverter<AssureType, int>())
            .HasColumnType("int");

            entity.Property(e => e.vcDMSName).HasColumnType("nvarchar(100)")
           .HasMaxLength(100);

        }
    }
}
