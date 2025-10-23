using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using YourNamespace.Entities;

namespace YourNamespace.Configurations
{
    public class tblPF_OtherInsurancesConfiguration : IEntityTypeConfiguration<tblPF_OtherInsurances>
    {
        public void Configure(EntityTypeBuilder<tblPF_OtherInsurances> builder)
        {
            builder.ToTable("tblPF_OtherInsurances");

            builder.HasKey(e => e.intOtherInsuranceId)
                   .HasName("PK_tblPF_OtherInsurances");

            builder.HasIndex(e => e.vcApplicationNumber)
                   .HasDatabaseName("tblPF_OtherInsurances_ApplicationNumber");

            builder.HasIndex(e => new { e.bitIsDeleted, e.dtDeletedDate })
                   .HasDatabaseName("tblPF_OtherInsurances_IsDeleted_DeletedDate");

            builder.Property(e => e.intOtherInsuranceId).IsRequired().ValueGeneratedOnAdd();
            builder.Property(e => e.vcApplicationNumber).HasMaxLength(100).IsRequired();
            builder.Property(e => e.intAssureType).IsRequired();
            builder.Property(e => e.vcInsurerNumber).HasMaxLength(100);
            builder.Property(e => e.vcToBeInsured).HasMaxLength(100);
            builder.Property(e => e.vcDeathSumAssured).HasMaxLength(100);
            builder.Property(e => e.vcPremiumPerAnnum).HasMaxLength(100);
            builder.Property(e => e.dtMonthAndYearOfInssuance);
            builder.Property(e => e.vcAcceptanceTerm).HasMaxLength(100);
            builder.Property(e => e.vcCurrentStatus).HasMaxLength(100);
            builder.Property(e => e.vcPolicyOrAppliactionNumber).HasMaxLength(100);
            builder.Property(e => e.vcStandardAcceptance).HasMaxLength(200);
            builder.Property(e => e.dtProposalDate);
            builder.Property(e => e.vcTypeofpolicyterm).HasMaxLength(100);
            builder.Property(e => e.vcLastAccessIP).HasMaxLength(100).IsRequired();
            builder.Property(e => e.vcCreatedBy).HasMaxLength(100).IsRequired();
            builder.Property(e => e.dtCreateDate).HasDefaultValueSql("getdate()").IsRequired();
            builder.Property(e => e.vcModifiedBy).HasMaxLength(100);
            builder.Property(e => e.dtModifiedDate);
            builder.Property(e => e.dtDeletedDate);
            builder.Property(e => e.bitIsDeleted);
        }
    }
}
