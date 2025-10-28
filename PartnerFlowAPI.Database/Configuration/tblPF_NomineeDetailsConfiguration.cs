using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Entities;

namespace PartnerFlowAPI.Configurations
{
    public class tblPF_NomineeDetailsConfiguration : IEntityTypeConfiguration<tblPF_NomineeDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_NomineeDetails> builder)
        {
            builder.ToTable("tblPF_NomineeDetails");

            builder.HasKey(e => e.intNomineeDetailsId)
                   .HasName("PK_tblPF_NomineeDetails");

            builder.HasIndex(e => e.vcApplicationNumber)
                   .HasDatabaseName("tblPF_NomineeDetails_ApplicationNumber");

            builder.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                   .HasDatabaseName("tblPF_NomineeDetails_IsDeleted_DeletedDate");

            // Column configurations
            builder.Property(e => e.intNomineeDetailsId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.intAssureType);
            builder.Property(e => e.intFamilyDetailsId);
            builder.Property(e => e.vcRelation).HasMaxLength(100);
            builder.Property(e => e.vcApplicationNumber).HasMaxLength(200).IsRequired();
            builder.Property(e => e.vcTitle).HasMaxLength(20);
            builder.Property(e => e.vcFirstName).HasMaxLength(100).IsRequired();
            builder.Property(e => e.vcMiddleName).HasMaxLength(100);
            builder.Property(e => e.vcLastName).HasMaxLength(100);
            builder.Property(e => e.dtDOB);
            builder.Property(e => e.chGender).HasMaxLength(2);
            builder.Property(e => e.dcAnnualIncome).HasColumnType("decimal(18,2)");
            builder.Property(e => e.vcMobileNumber).HasMaxLength(100);
            builder.Property(e => e.ftNomineePercentage);
            builder.Property(e => e.vcOccupation).HasMaxLength(200);
            builder.Property(e => e.btIsAppointee);
            builder.Property(e => e.vcAppointeeRelation).HasMaxLength(1000);
            builder.Property(e => e.vcAppointeeTile).HasMaxLength(100);
            builder.Property(e => e.vcAppointeeFirstName).HasMaxLength(1000);
            builder.Property(e => e.vcAppointeeMiddleName).HasMaxLength(1000);
            builder.Property(e => e.vcAppointeeLastName).HasMaxLength(1000);
            builder.Property(e => e.vcAppointeeDob);
            builder.Property(e => e.chAppointeeGender).HasMaxLength(2).IsFixedLength();
            builder.Property(e => e.vcAppointeeContactNumber).HasMaxLength(40);
            builder.Property(e => e.vcAppointeeCkycNumber).HasMaxLength(20);
            builder.Property(e => e.vcAppointeeAddress1).HasMaxLength(1000);
            builder.Property(e => e.vcAppointeeAddress2).HasMaxLength(1000);
            builder.Property(e => e.vcAppointeeLandmark).HasMaxLength(1000);
            builder.Property(e => e.vcAppointeeCity).HasMaxLength(1000);
            builder.Property(e => e.vcAppointeeState).HasMaxLength(1000);
            builder.Property(e => e.vcAppointeeCountry).HasMaxLength(1000);
            builder.Property(e => e.vcAppointeePincode).HasMaxLength(100);
            builder.Property(e => e.btAppointeeAddressSameAsNominee);
            builder.Property(e => e.vcRelationshipWithNominee).HasMaxLength(100);
            builder.Property(e => e.vcAppointeeSignature).HasMaxLength(1000);
            builder.Property(e => e.intAgeAtOnset);
            builder.Property(e => e.vcLivingOrDeceased).HasMaxLength(100);
            builder.Property(e => e.vcDiagnosis).HasMaxLength(100);
            builder.Property(e => e.vcLastAccessIP).HasMaxLength(100).IsRequired();
            builder.Property(e => e.vcCreatedBy).HasMaxLength(100).IsRequired();
            builder.Property(e => e.dtCreateDate).IsRequired();
            builder.Property(e => e.vcModifiedBy).HasMaxLength(100);
            builder.Property(e => e.dtModifiedDate);
            builder.Property(e => e.dtDeletedDate);
            builder.Property(e => e.bitIsDeleted);
            builder.Property(e => e.intDeDupeStatus).HasDefaultValue(0);
            builder.Property(e => e.intImageQCStatus).HasDefaultValue(0);
            builder.Property(e => e.intDocQCStatus).HasDefaultValue(0);
            builder.Property(e => e.vcLifeAsiaClientId).HasMaxLength(20);
            builder.Property(e => e.dtClientIDGeneratedOn);
            builder.Property(e => e.intAppointeeDeDupeStatus).HasDefaultValue(0);
            builder.Property(e => e.intAppointeeImageQCStatus).HasDefaultValue(0);
            builder.Property(e => e.intAppointeeDocQCStatus).HasDefaultValue(0);
            builder.Property(e => e.vcAppointeeLAClientID).HasMaxLength(20);
            builder.Property(e => e.dtAppointeeClientIDGeneratedOn);
            builder.Property(e => e.vcBankAccountNumber).HasMaxLength(100);
            builder.Property(e => e.vcIFSCCODE).HasMaxLength(100);
            builder.Property(e => e.vcBANKNAME).HasMaxLength(100);
            builder.Property(e => e.vcBranchLocation).HasMaxLength(100);
            builder.Property(e => e.vcAppointeeDedupeMode).HasMaxLength(200);
            builder.Property(e => e.vcNomineeDedupeMode).HasMaxLength(200);
        }
    }
}
