using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Domain.Entities;
using YourNamespace.Entities;

namespace YourNamespace.Configurations
{
    public class tblpf_financialQuestionConfiguration : IEntityTypeConfiguration<tblpf_financialQuestion>
    {
        public void Configure(EntityTypeBuilder<tblpf_financialQuestion> entity)
        {
            entity.ToTable("tblpf_financialQuestion");

            // Primary Key
            entity.HasKey(e => e.FinancialID)
                  .HasName("PK_tblpf_financialQuestion");

            // Columns
            entity.Property(e => e.FinancialID)
                .HasColumnName("FinancialID")
                .IsRequired();

            entity.Property(e => e.vcpremiumFinancedBy).HasMaxLength(200);
            entity.Property(e => e.vcoccupation).HasMaxLength(200);
            entity.Property(e => e.vcannualIncome).HasMaxLength(200);
            entity.Property(e => e.vcincome).HasMaxLength(200);
            entity.Property(e => e.btassessedForIncomeTax).IsRequired();
            entity.Property(e => e.vcpanNumber).HasMaxLength(100);
            entity.Property(e => e.vcsourceOfIncome).HasMaxLength(200);
            entity.Property(e => e.bthaveAgriculturalLand).IsRequired();
            entity.Property(e => e.vchaveAgriculturalLandDetails).HasMaxLength(200);
            entity.Property(e => e.vccashDepositesCertificates).HasMaxLength(200);
            entity.Property(e => e.vcnscUtiPpfPension).HasMaxLength(200);
            entity.Property(e => e.vccapitalInvestment).HasMaxLength(200);
            entity.Property(e => e.vcimmovableProperties).HasMaxLength(200);
            entity.Property(e => e.vcotherInvestmentsSavings).HasMaxLength(200);
            entity.Property(e => e.vctotal).HasMaxLength(200);
            entity.Property(e => e.vcnetWorth).HasMaxLength(200);
            entity.Property(e => e.vcyear2425).HasMaxLength(200);
            entity.Property(e => e.vcyear2324).HasMaxLength(200);
            entity.Property(e => e.vcyear2223).HasMaxLength(200);
            entity.Property(e => e.vcAgriyear2425).HasMaxLength(200);
            entity.Property(e => e.vcAgriyear2324).HasMaxLength(200);
            entity.Property(e => e.vcAgriyear2223).HasMaxLength(200);
            entity.Property(e => e.vcrentyear2425).HasMaxLength(200);
            entity.Property(e => e.vcrentyear2324).HasMaxLength(200);
            entity.Property(e => e.vcrentyear2223).HasMaxLength(200);
            entity.Property(e => e.vccapitalgainyear2425).HasMaxLength(200);
            entity.Property(e => e.vccapitalgainyear2324).HasMaxLength(200);
            entity.Property(e => e.vccapitalgainyear2223).HasMaxLength(200);
            entity.Property(e => e.vclongcapitalgainyear2425).HasMaxLength(200);
            entity.Property(e => e.vclongcapitalgainyear2324).HasMaxLength(200);
            entity.Property(e => e.vclongcapitalgainyear2223).HasMaxLength(200);
            entity.Property(e => e.vcinterestyear2425).HasMaxLength(200);
            entity.Property(e => e.vcinterestyear2324);
            entity.Property(e => e.vcinterestyear2223).HasMaxLength(200);
            entity.Property(e => e.vcotheryear2425).HasMaxLength(200);
            entity.Property(e => e.vcotheryear2324).HasMaxLength(200);
            entity.Property(e => e.vcotheryear2223).HasMaxLength(200);
            entity.Property(e => e.vctotalanualyear2425).HasMaxLength(200);
            entity.Property(e => e.vctotalanualyear2324).HasMaxLength(200);
            entity.Property(e => e.vctotalanualyear2223).HasMaxLength(200);
            entity.Property(e => e.vcApplicationNumber).HasMaxLength(200);
            entity.Property(e => e.intAssureType);
            entity.Property(e => e.vcotherLiabilities).HasMaxLength(100);
            entity.Property(e => e.vcliabilitiesTotal).HasMaxLength(100);
            entity.Property(e => e.vcloan).HasMaxLength(100);
            entity.Property(e => e.vcotherDetails).HasMaxLength(1000);
            entity.Property(e => e.vcLastAccessIP).HasMaxLength(100);
            entity.Property(e => e.vcCreatedBy).HasMaxLength(100);
            entity.Property(e => e.dtCreateDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.vcModifiedBy).HasMaxLength(100);
            entity.Property(e => e.dtModifiedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtDeletedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.bitIsDeleted);
                
            entity.Property(e => e.btinsuranceFromOtherCompanines)
                .IsRequired()
                .HasDefaultValue(false);
            entity.Property(e => e.vcinsuranceCompanyName).HasMaxLength(200);
            entity.Property(e => e.vcpolicyNumber).HasMaxLength(200);
            entity.Property(e => e.vcbasicSumAssured).HasMaxLength(200);
            entity.Property(e => e.vcridersOpted).HasMaxLength(200);
            entity.Property(e => e.vcmedicalOrNonMedical).HasMaxLength(200);
            entity.Property(e => e.vcannualizedPremium).HasMaxLength(200);
            entity.Property(e => e.vcaccept).HasMaxLength(200);
            entity.Property(e => e.vcnameOfLa).HasMaxLength(200);
            entity.Property(e => e.vcspecifyDetails).HasMaxLength(200);

            // Indexes
            entity.HasIndex(e => e.vcApplicationNumber)
                .HasDatabaseName("tblpf_financialQuestion_ApplicationNumber");

            entity.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                .HasDatabaseName("tblpf_financialQuestion_IsDeleted_DeletedDate");
        }
    }
}
