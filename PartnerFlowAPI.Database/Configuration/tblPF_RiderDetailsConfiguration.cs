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
    public class tblPF_RiderDetailsConfiguration : IEntityTypeConfiguration<tblPF_RiderDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_RiderDetails> entity)
        {
            entity.ToTable("tblPF_RiderDetails");

            entity.HasKey(e => e.intRiderDetailId);

            entity.Property(e => e.intRiderDetailId)
                .ValueGeneratedOnAdd();

            entity.Property(e => e.vcApplicationNumber)
                .IsRequired()
                .HasMaxLength(200)
                .IsUnicode(true);

            entity.Property(e => e.vcQuotationId).HasMaxLength(200);
            entity.Property(e => e.vcRiderType).HasMaxLength(100);
            entity.Property(e => e.vcProductCode).HasMaxLength(100);
            entity.Property(e => e.vcRiderCode).HasMaxLength(100);
            entity.Property(e => e.vcProductName).HasMaxLength(510);
            entity.Property(e => e.vcRiderName).HasMaxLength(510);
            entity.Property(e => e.vcMode).HasMaxLength(200);
            entity.Property(e => e.vcUIN).HasMaxLength(200);
            entity.Property(e => e.vcReturnOfPremium).HasMaxLength(100);
            entity.Property(e => e.vcBenefitTypePayout).HasMaxLength(100);
            entity.Property(e => e.vcIncomeFrequency).HasMaxLength(100);
            entity.Property(e => e.vcIncomeDuration).HasMaxLength(100);
            entity.Property(e => e.vcLumpSumBenefit).HasMaxLength(100);
            entity.Property(e => e.vcRiderOption).HasMaxLength(100);
            entity.Property(e => e.vcLastAccessIP).IsRequired().HasMaxLength(100);
            entity.Property(e => e.vcCreatedBy).IsRequired().HasMaxLength(100);
            entity.Property(e => e.vcModifiedBy).HasMaxLength(100);

            entity.Property(e => e.dtCreateDate).IsRequired();
            entity.Property(e => e.bitIsDeleted);

            entity.Property(e => e.dcModalPremium).HasPrecision(18, 2);
            entity.Property(e => e.dcSumAssured).HasPrecision(18, 2);
            entity.Property(e => e.dcTotalPremium).HasPrecision(18, 2);
            entity.Property(e => e.dcAnnualizedPremium).HasPrecision(18, 2);
            entity.Property(e => e.dcTax).HasPrecision(18, 2);

            entity.Property(e => e.intPT).IsRequired();
            entity.Property(e => e.intPPT).IsRequired();
            entity.Property(e => e.intRiderCreateStatus).IsRequired();
        }
    }
}
