using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourNamespace.Entities;

namespace YourNamespace.Configurations
{
    public class tblPF_MandateDetailsConfiguration : IEntityTypeConfiguration<tblPF_MandateDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_MandateDetails> entity)
        {
            entity.ToTable("tblPF_MandateDetails");

            entity.HasKey(e => e.intMandateID)
                  .HasName("PK_tblPF_MandateDetails");

            entity.HasIndex(e => e.vcApplicationNumber)
                  .HasDatabaseName("tblPF_MandateDetails_ApplicationNumber");

            entity.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                  .HasDatabaseName("tblPF_MandateDetails_IsDeleted_DeletedDate");

            entity.Property(e => e.intMandateID);
            entity.Property(e => e.vcApplicationNumber).IsRequired().HasMaxLength(200);
            entity.Property(e => e.intPersonalID);
            entity.Property(e => e.intAssureType);
            entity.Property(e => e.vcTransactionId).HasMaxLength(100);
            entity.Property(e => e.intPaymentStatus).HasDefaultValue(0);
            entity.Property(e => e.intMandateStatus);
            entity.Property(e => e.btIsMandateCase).HasDefaultValue(false);
            entity.Property(e => e.dtMandateStartDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtMandateEndDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.vcPaymentType).HasMaxLength(100);
            entity.Property(e => e.vcRenewalMode).HasMaxLength(100);
            entity.Property(e => e.dcAmount).HasColumnType("decimal(18,0)");
            entity.Property(e => e.vcMandateOption).HasMaxLength(100);
            entity.Property(e => e.vcMandateLink).HasMaxLength(300);
            entity.Property(e => e.vcChequeDDNumber).HasMaxLength(100);
            entity.Property(e => e.vcChequeDDDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.ftChequeDDAmount);
            entity.Property(e => e.vcBankName).HasMaxLength(300);
            entity.Property(e => e.vcBankBranch).HasMaxLength(300);
            entity.Property(e => e.vcBankAccountType).HasMaxLength(100);
            entity.Property(e => e.vcBankAccountNumber).HasMaxLength(100);
            entity.Property(e => e.vcBankIFSCCode).HasMaxLength(100);
            entity.Property(e => e.vcBankMICRCode).HasMaxLength(100);
            entity.Property(e => e.btIsAlreadyEInsurance);
            entity.Property(e => e.btOpenEInsuranceAccount);
            entity.Property(e => e.dcTotalInstalmentPremiumWithTaxes).HasColumnType("decimal(18,0)");
            entity.Property(e => e.dtChequeDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.ftFixedIncomePayoutPercentage);
            entity.Property(e => e.ftLumpsumPayoutPercentage);
            entity.Property(e => e.intFirstYearPremiumMode);
            entity.Property(e => e.intPaymentFrequency);
            entity.Property(e => e.intPayoutFrequency);
            entity.Property(e => e.intPayoutOption);
            entity.Property(e => e.intPayoutTerm);
            entity.Property(e => e.intRenewalPremiumMode);
            entity.Property(e => e.intRepository);
            entity.Property(e => e.vcAccountNumber).HasMaxLength(1000);
            entity.Property(e => e.vcBranch).HasMaxLength(1000);
            entity.Property(e => e.vcCardNumber).HasMaxLength(1000);
            entity.Property(e => e.vcChequeNumber).HasMaxLength(1000);
            entity.Property(e => e.vcDemandDraft).HasMaxLength(1000);
            entity.Property(e => e.vcDrownOn).HasMaxLength(1000);
            entity.Property(e => e.vcEInsuranceAccountNumber).HasMaxLength(1000);
            entity.Property(e => e.vcIfscCode).HasMaxLength(1000);
            entity.Property(e => e.vcInsuranceRepositoryName).HasMaxLength(1000);
            entity.Property(e => e.vcPaymentCardNetwork).HasMaxLength(1000);
            entity.Property(e => e.btIsRecieveEInsurance);
            entity.Property(e => e.vcLastAccessIP).IsRequired().HasMaxLength(100);
            entity.Property(e => e.vcCreatedBy).IsRequired().HasMaxLength(100);
            entity.Property(e => e.dtCreateDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.vcModifiedBy).HasMaxLength(100);
            entity.Property(e => e.dtModifiedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtDeletedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.bitIsDeleted);
            entity.Property(e => e.vcEmail).HasMaxLength(400);
            entity.Property(e => e.vcDemanddraftno).HasMaxLength(100);
            entity.Property(e => e.dtDemaddraftDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.vcAgentTitle).HasMaxLength(100);
            entity.Property(e => e.vcAgentFirstName).HasMaxLength(100);
            entity.Property(e => e.vcAgentMiddleName).HasMaxLength(100);
            entity.Property(e => e.vcAgentLastName).HasMaxLength(100);
            entity.Property(e => e.vcAgentPhoneNo).HasMaxLength(40);
            entity.Property(e => e.vcAgentEmailID).HasMaxLength(400);
            entity.Property(e => e.vcPolicyNumber).HasMaxLength(100);
            entity.Property(e => e.vcPGMandateNo).HasMaxLength(200);
            entity.Property(e => e.vcMandateMode).HasMaxLength(200);
            entity.Property(e => e.vcLifeAsiaMandateNumber).HasMaxLength(200);
            entity.Property(e => e.vcMandateStatus).HasMaxLength(200);
            entity.Property(e => e.vcReferenceNumber).HasMaxLength(200);
            entity.Property(e => e.vcPGStatus).HasMaxLength(200);
            entity.Property(e => e.vcPGReferenceNo).HasMaxLength(200);
            entity.Property(e => e.vcPGMsg).HasMaxLength(1000);
            entity.Property(e => e.vcInternalBankId).HasMaxLength(200);
            entity.Property(e => e.vcInternalTransactionID).HasMaxLength(200);
            entity.Property(e => e.vcToken).HasMaxLength(200);
            entity.Property(e => e.vcFactHouse).HasMaxLength(200);
        }
    }
}
