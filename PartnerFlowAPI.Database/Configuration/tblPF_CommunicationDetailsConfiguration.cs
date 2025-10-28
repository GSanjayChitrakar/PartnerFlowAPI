using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Domain.Entities;

namespace PartnerFlowAPI.Infrastructure.Persistence.Configurations
{
    public class tblPF_CommunicationDetailsConfiguration : IEntityTypeConfiguration<tblPF_CommunicationDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_CommunicationDetails> entity)
        {
            // Table
            entity.ToTable("tblPF_CommunicationDetails");

            // Primary Key
            entity.HasKey(e => e.IntCommunicationId)
                  .HasName("PK_tblApp_CommunicationDetails");

            // Indexes
            entity.HasIndex(e => e.VcApplicationNumber)
                  .HasDatabaseName("tblPF_CommunicationDetails_ApplicationNumber");

            entity.HasIndex(e => new { e.BitIsDeleted, e.DtDeletedDate })
                  .HasDatabaseName("tblPF_CommunicationDetails_IsDeleted_DeletedDate");

            // Columns
            entity.Property(e => e.IntCommunicationId)
                  .ValueGeneratedOnAdd();

            entity.Property(e => e.VcApplicationNumber)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(e => e.IntAssureType)
                  .IsRequired();

            entity.Property(e => e.VcCAAddressLine1).HasMaxLength(510);
            entity.Property(e => e.VcCAAddressLine2).HasMaxLength(510);
            entity.Property(e => e.VcCAAddressLine3).HasMaxLength(510);
            entity.Property(e => e.VcCALandmark).HasMaxLength(510);
            entity.Property(e => e.VcCAPincode).HasMaxLength(20);
            entity.Property(e => e.VcCACity).HasMaxLength(510);
            entity.Property(e => e.VcCAState).HasMaxLength(510);
            entity.Property(e => e.VcCACountry).HasMaxLength(510);

            entity.Property(e => e.BtIsPASameCA)
                  .IsRequired();

            entity.Property(e => e.VcPAAddressLine1).HasMaxLength(510);
            entity.Property(e => e.VcPAAddressLine2).HasMaxLength(510);
            entity.Property(e => e.VcPAAddressLine3).HasMaxLength(510);
            entity.Property(e => e.VcPALandmark).HasMaxLength(510);
            entity.Property(e => e.VcPAPincode).HasMaxLength(20);
            entity.Property(e => e.VcPACity).HasMaxLength(510);
            entity.Property(e => e.VcPAState).HasMaxLength(510);
            entity.Property(e => e.VcPACountry).HasMaxLength(510);

            entity.Property(e => e.VcMobileNumber).HasMaxLength(40);
            entity.Property(e => e.VcAlternateNumber).HasMaxLength(40);
            entity.Property(e => e.VcWorkContactNumber).HasMaxLength(40);
            entity.Property(e => e.VcInterNationalNumber).HasMaxLength(100);
            entity.Property(e => e.VcEmailAddress).HasMaxLength(510);

            entity.Property(e => e.BtIsEditable)
                  .IsRequired()
                  .HasDefaultValue(false);

            entity.Property(e => e.VcLastAccessIP)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.VcCreatedBy)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(e => e.DtCreateDate)
                  .IsRequired()
                  .HasColumnType("datetime2(7)");

            entity.Property(e => e.VcModifiedBy).HasMaxLength(100);
            entity.Property(e => e.DtModifiedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.DtDeletedDate).HasColumnType("datetime2(7)");

            entity.Property(e => e.BitIsDeleted);
                  

            entity.Property(e => e.BtKYCAddressUpdate)
                  .HasDefaultValue(false);
        }
    }
}
