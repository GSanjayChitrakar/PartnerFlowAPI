using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourNamespace.Entities;

namespace YourNamespace.Configurations
{
    public class tblPF_InsuranceHistoryConfiguration : IEntityTypeConfiguration<tblPF_InsuranceHistory>
    {
        public void Configure(EntityTypeBuilder<tblPF_InsuranceHistory> entity)
        {
            entity.ToTable("tblPF_InsuranceHistory");

            entity.HasKey(e => e.intInsuranceHistoryId)
                  .HasName("PK_tblPF_InsuranceHistory");

            entity.HasIndex(e => e.vcApplicationNumber)
                  .HasDatabaseName("tblPF_InsuranceHistory_ApplicationNumber");

            // Column properties
            entity.Property(e => e.intInsuranceHistoryId).HasColumnName("intInsuranceHistoryId");
            entity.Property(e => e.vcApplicationNumber).IsRequired().HasMaxLength(200);
            entity.Property(e => e.intAssureType);
            entity.Property(e => e.vcTitle).HasMaxLength(510);
            entity.Property(e => e.vcFirstName).HasMaxLength(510);
            entity.Property(e => e.vcMiddleName).HasMaxLength(510);
            entity.Property(e => e.vcLastName).HasMaxLength(510);
            entity.Property(e => e.vcPolicyNumber).HasMaxLength(200);
            entity.Property(e => e.dtDateOfProposal).HasColumnType("datetime2(7)");
            entity.Property(e => e.intTypeOfPolicy);
            entity.Property(e => e.dcBaseSumAssured).HasColumnType("decimal(18,7)");
            entity.Property(e => e.dcPremiumPerAnnum).HasColumnType("decimal(18,7)");
            entity.Property(e => e.vcMonthOfInsurance).HasMaxLength(100);
            entity.Property(e => e.vcYearOfInsurance).HasMaxLength(100);
            entity.Property(e => e.intCurrentStatus);
            entity.Property(e => e.intAcceptanceTerms);
            entity.Property(e => e.vcLastAccessIP).IsRequired().HasMaxLength(100);
            entity.Property(e => e.vcCreatedBy).IsRequired().HasMaxLength(100);
            entity.Property(e => e.dtCreateDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.vcModifiedBy).HasMaxLength(100);
            entity.Property(e => e.dtModifiedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtDeletedDate).HasColumnType("datetime2(7)");
        }
    }
}
