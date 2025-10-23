using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Domain.Entities;

namespace PartnerFlowAPI.Infrastructure.Persistence.Configurations
{
    public class tblpf_FatcaDetailsConfiguration : IEntityTypeConfiguration<tblpf_FatcaDetails>
    {
        public void Configure(EntityTypeBuilder<tblpf_FatcaDetails> entity)
        {
            entity.ToTable("tblpf_FatcaDetails");

            entity.HasKey(e => e.intFatcaId).HasName("PK_tblpf_FatcaDetails");

            entity.Property(e => e.intFatcaId).ValueGeneratedOnAdd();

            entity.Property(e => e.vcApplicationNumber)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.intAssureType).IsRequired();

            entity.Property(e => e.btFatca)
                  .HasDefaultValue(false);

            entity.Property(e => e.vcFatcaAddressJurisdiction).HasMaxLength(510);
            entity.Property(e => e.vcFatcaTaxIdentificationNumber).HasMaxLength(510);
            entity.Property(e => e.vcFatcaValidityOfDocumentaryEvidence).HasMaxLength(510);
            entity.Property(e => e.vcFatcaTaxResidencyCountry).HasMaxLength(510);
            entity.Property(e => e.vcFatcaTINNumberIssuingCountry).HasMaxLength(510);

            entity.Property(e => e.vcLastAccessIP)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.vcCreatedBy)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.dtCreateDate)
                  .HasDefaultValueSql("(getdate())");

            entity.Property(e => e.vcModifiedBy).HasMaxLength(100);
            entity.Property(e => e.dtModifiedDate);
            entity.Property(e => e.dtDeletedDate);

            entity.Property(e => e.bitIsDeleted);
                 

            // Indexes
            entity.HasIndex(e => e.vcApplicationNumber)
                  .HasDatabaseName("tblpf_FatcaDetails_ApplicationNumber");

            entity.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                  .HasDatabaseName("tblpf_FatcaDetails_IsDeleted_DeletedDate");
        }
    }
}
