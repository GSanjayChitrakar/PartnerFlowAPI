using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Configuration
{
    public class ProductDetailsConfiguration : IEntityTypeConfiguration<tblPF_ProductDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_ProductDetails> entity)
        {
            entity.HasKey(e => e.intProductDetailId);

            entity.Property(e => e.intProductDetailId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.vcApplicationNumber)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            entity.Property(e => e.vcProductCode).HasMaxLength(100);
            entity.Property(e => e.vcRiderProductId).HasMaxLength(100);
            entity.Property(e => e.vcFGProductCode).HasMaxLength(600);
            entity.Property(e => e.vcProductType).HasMaxLength(100);
            entity.Property(e => e.vcProductName).HasMaxLength(510);
            entity.Property(e => e.vcOptionWithinProductRider).HasMaxLength(1000);
            entity.Property(e => e.vcBIPdfPath).HasMaxLength(1600);
            entity.Property(e => e.vcMode).HasMaxLength(200);
            entity.Property(e => e.vcSourceChannel).HasMaxLength(100);
            entity.Property(e => e.vcGroupStaff).HasMaxLength(100);
            entity.Property(e => e.vcOptions).HasMaxLength(510);
            entity.Property(e => e.vcAnnuityThrough).HasMaxLength(510);
            entity.Property(e => e.vcPensionFrequency).HasMaxLength(510);
            entity.Property(e => e.vcSurrenderAnnuityThrough).HasMaxLength(510);
            entity.Property(e => e.vcPensionSurrenderFrequency).HasMaxLength(510);
            entity.Property(e => e.vcNoOfYears).HasMaxLength(100);
            entity.Property(e => e.vcAssignmentType).HasMaxLength(100);
            entity.Property(e => e.vcUIN).HasMaxLength(100);
            entity.Property(e => e.vcPayoutFrequency).HasMaxLength(100);
            entity.Property(e => e.vcProductCategory).HasMaxLength(100);
            entity.Property(e => e.vcPayOutOptions).HasMaxLength(500);
            entity.Property(e => e.VcAccidentalDeathSumAssured).HasMaxLength(100);
            entity.Property(e => e.VcIncomeOption).HasMaxLength(100);
            entity.Property(e => e.vcDeathBenefit).HasMaxLength(100);
            entity.Property(e => e.vcInBuildRider).HasMaxLength(1000);
            entity.Property(e => e.vcFundStrategy).HasMaxLength(400);
            entity.Property(e => e.vcLumpsumMaturityBenefit).HasMaxLength(100);
            entity.Property(e => e.vcDeathBenifitPayout).HasMaxLength(100);
            entity.Property(e => e.vcLastAccessIP).IsRequired().HasMaxLength(100);
            entity.Property(e => e.vcCreatedBy).IsRequired().HasMaxLength(100);
            entity.Property(e => e.vcModifiedBy).HasMaxLength(100);
            entity.Property(e => e.vcEMRLoading).HasMaxLength(10);

            entity.Property(e => e.dtCreateDate).IsRequired();
            entity.Property(e => e.bitIsDeleted);

            // Numeric precision
            entity.Property(e => e.dcModalPremium).HasPrecision(18, 2);
            entity.Property(e => e.dcSumAssured).HasPrecision(18, 2);
            entity.Property(e => e.dcTotalPremium).HasPrecision(18, 2);
            entity.Property(e => e.dcAnnualizedPremium).HasPrecision(18, 2);
            entity.Property(e => e.dcTax).HasPrecision(18, 2);
            entity.Property(e => e.dcTax1).HasPrecision(18, 2);
            entity.Property(e => e.dcInstallmentPremiumWithTaxes).HasPrecision(18, 2);
            entity.Property(e => e.dcMonthlyIncome).HasPrecision(18, 2);
            entity.Property(e => e.dcBaseModalPremium).HasPrecision(18, 2);
            entity.Property(e => e.dcBaseModalPremiumWGst).HasPrecision(18, 2);
            entity.Property(e => e.dcRiderModalPremium).HasPrecision(18, 2);
            entity.Property(e => e.dcRiderModalPremiumWGst).HasPrecision(18, 2);
            entity.Property(e => e.dcDeathBenefitAmount).HasPrecision(18, 2);
            entity.Property(e => e.dcEMRMortality).HasPrecision(18, 2);
            entity.Property(e => e.dcRateAdjustment).HasPrecision(18, 2);
            entity.Property(e => e.dcInstPrem).HasPrecision(18, 2);
            entity.Property(e => e.dcZlinstPrem).HasPrecision(18, 2);

            entity.ToTable("tblPF_ProductDetails");
        }
    }
}
