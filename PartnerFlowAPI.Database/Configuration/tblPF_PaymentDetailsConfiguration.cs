using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Entities;

namespace PartnerFlowAPI.Configurations
{
    public class tblPF_PaymentDetailsConfiguration : IEntityTypeConfiguration<tblPF_PaymentDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_PaymentDetails> builder)
        {
            builder.ToTable("tblPF_PaymentDetails");

            builder.HasKey(e => e.intPaymentID)
                   .HasName("PK_tblPF_PaymentDetails");

            builder.HasIndex(e => e.vcApplicationNumber)
                   .HasDatabaseName("tblPF_PaymentDetails_ApplicationNumber");

            builder.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                   .HasDatabaseName("tblPF_PaymentDetails_IsDeleted_DeletedDate");

            builder.Property(e => e.intPaymentID).IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.vcApplicationNumber).HasMaxLength(200).IsRequired();
            builder.Property(e => e.intPersonalID);
            builder.Property(e => e.intAssureType).IsRequired();
            builder.Property(e => e.vcTransactionId).HasMaxLength(100);
            builder.Property(e => e.intPaymentStatus).IsRequired();
            builder.Property(e => e.intMandateStatus);
            builder.Property(e => e.btIsMandateCase).HasDefaultValue(false);
            builder.Property(e => e.dtMandateStartDate);
            builder.Property(e => e.dtMandateEndDate);
            builder.Property(e => e.vcPaymentType).HasMaxLength(100);
            builder.Property(e => e.vcRenewalMode).HasMaxLength(100);
            builder.Property(e => e.dcAmount).HasColumnType("decimal(18,0)");
            builder.Property(e => e.vcMandateOption).HasMaxLength(100);
            builder.Property(e => e.vcMandateLink).HasMaxLength(300);
            builder.Property(e => e.vcChequeDDNumber).HasMaxLength(100);
            builder.Property(e => e.vcChequeDDDate);
            builder.Property(e => e.ftChequeDDAmount).HasColumnType("decimal(18,2)");
            builder.Property(e => e.vcBankName).HasMaxLength(300);
            builder.Property(e => e.vcBankBranch).HasMaxLength(300);
            builder.Property(e => e.vcBankAccountType).HasMaxLength(100);
            builder.Property(e => e.vcBankAccountNumber).HasMaxLength(100);
            builder.Property(e => e.vcBankIFSCCode).HasMaxLength(100);
            builder.Property(e => e.vcBankMICRCode).HasMaxLength(100);
            builder.Property(e => e.btIsAlreadyEInsurance);
            builder.Property(e => e.btOpenEInsuranceAccount);
            builder.Property(e => e.dcTotalInstalmentPremiumWithTaxes).HasColumnType("decimal(18,0)");
            builder.Property(e => e.dtChequeDate);
            builder.Property(e => e.ftFixedIncomePayoutPercentage);
            builder.Property(e => e.ftLumpsumPayoutPercentage);
            builder.Property(e => e.intFirstYearPremiumMode);
            builder.Property(e => e.intPaymentFrequency);
            builder.Property(e => e.intPayoutFrequency);
            builder.Property(e => e.intPayoutOption);
            builder.Property(e => e.intPayoutTerm);
            builder.Property(e => e.intRenewalPremiumMode);
            builder.Property(e => e.intRepository);
            builder.Property(e => e.vcAccountNumber).HasMaxLength(1000);
            builder.Property(e => e.vcBranch).HasMaxLength(1000);
            builder.Property(e => e.vcCardNumber).HasMaxLength(1000);
            builder.Property(e => e.vcChequeNumber).HasMaxLength(1000);
            builder.Property(e => e.vcDemandDraft).HasMaxLength(1000);
            builder.Property(e => e.vcDrownOn).HasMaxLength(1000);
            builder.Property(e => e.vcEInsuranceAccountNumber).HasMaxLength(1000);
            builder.Property(e => e.vcIfscCode).HasMaxLength(1000);
            builder.Property(e => e.vcInsuranceRepositoryName).HasMaxLength(1000);
            builder.Property(e => e.vcPaymentCardNetwork).HasMaxLength(1000);
            builder.Property(e => e.btIsRecieveEInsurance);
            builder.Property(e => e.vcLastAccessIP).HasMaxLength(100).IsRequired();
            builder.Property(e => e.vcCreatedBy).HasMaxLength(100).IsRequired();
            builder.Property(e => e.dtCreateDate).IsRequired();
            builder.Property(e => e.vcModifiedBy).HasMaxLength(100);
            builder.Property(e => e.dtModifiedDate);
            builder.Property(e => e.dtDeletedDate);
            builder.Property(e => e.bitIsDeleted);
            builder.Property(e => e.vcEmail).HasMaxLength(400);
            builder.Property(e => e.vcDemanddraftno).HasMaxLength(100);
            builder.Property(e => e.dtDemaddraftDate);
            builder.Property(e => e.vcAgentTitle).HasMaxLength(100);
            builder.Property(e => e.vcAgentFirstName).HasMaxLength(100);
            builder.Property(e => e.vcAgentMiddleName).HasMaxLength(100);
            builder.Property(e => e.vcAgentLastName).HasMaxLength(100);
            builder.Property(e => e.vcAgentPhoneNo).HasMaxLength(40);
            builder.Property(e => e.vcAgentEmailID).HasMaxLength(400);
            builder.Property(e => e.vcPolicyNumber).HasMaxLength(100);
            builder.Property(e => e.vcReferenceNumber).HasMaxLength(200);
            builder.Property(e => e.vcPGReferenceNo).HasMaxLength(200);
            builder.Property(e => e.vcInternalTransactionID).HasMaxLength(200);
        }
    }
}
