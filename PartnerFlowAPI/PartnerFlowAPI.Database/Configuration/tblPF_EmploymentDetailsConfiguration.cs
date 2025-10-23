using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourNamespace.Entities;

namespace YourNamespace.Configurations
{
    public class tblPF_EmploymentDetailsConfiguration : IEntityTypeConfiguration<tblPF_EmploymentDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_EmploymentDetails> entity)
        {
            entity.ToTable("tblPF_EmploymentDetails");

            entity.HasKey(e => e.intEmploymentDetailId)
                  .HasName("PK_tblPF_EmploymentDetails");

            // Indexes
            entity.HasIndex(e => e.vcApplicationNumber)
                  .HasDatabaseName("tblPF_EmploymentDetails_ApplicationNumber");

            entity.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                  .HasDatabaseName("tblPF_EmploymentDetails_IsDeleted_DeletedDate");

            // Column configurations
            entity.Property(e => e.vcApplicationNumber)
                .HasMaxLength(200)
                .IsRequired();

            entity.Property(e => e.intAssureType).IsRequired();

            entity.Property(e => e.vcOccupation).HasMaxLength(100);

            entity.Property(e => e.btHazardousEnvironment)
                .HasDefaultValue(false);

            entity.Property(e => e.vcWorkDomain).HasMaxLength(200);
            entity.Property(e => e.vcFirmOrEmployerName).HasMaxLength(200);
            entity.Property(e => e.vcNatureOfBusiness).HasMaxLength(200);
            entity.Property(e => e.vcNatureOfDuties).HasMaxLength(200);
            entity.Property(e => e.vcDesignation).HasMaxLength(200);

            entity.Property(e => e.dcAnnualIncome)
                .HasColumnType("decimal(18,7)");

            entity.Property(e => e.dcInsuranceCover)
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.dcParentAnnualIncome)
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.dcSiblingsInsuranceCover)
                .HasColumnType("decimal(18,2)");

            entity.Property(e => e.vcBusinessAddress1).HasMaxLength(510);
            entity.Property(e => e.vcBusinessAddress2).HasMaxLength(510);
            entity.Property(e => e.vcBusinessAddress3).HasMaxLength(510);
            entity.Property(e => e.vcBusinessRoadName).HasMaxLength(510);
            entity.Property(e => e.vcBusinessAddressLandmark).HasMaxLength(510);
            entity.Property(e => e.vcBusinessCity).HasMaxLength(510);
            entity.Property(e => e.vcBusinessState).HasMaxLength(510);
            entity.Property(e => e.vcBusinessCountry).HasMaxLength(510);
            entity.Property(e => e.vcBusinessPincode).HasMaxLength(510);
            entity.Property(e => e.vcStudingInClass).HasMaxLength(100);

            entity.Property(e => e.vcLastAccessIP).HasMaxLength(100);
            entity.Property(e => e.vcCreatedBy).HasMaxLength(100);
            entity.Property(e => e.vcModifiedBy).HasMaxLength(100);

            entity.Property(e => e.dtCreateDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtModifiedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtDeletedDate).HasColumnType("datetime2(7)");

            entity.Property(e => e.bitIsDeleted);
               
        }
    }
}
