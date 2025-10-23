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
    public class tblPartnerSuitabilityConfiguration : IEntityTypeConfiguration<tblPartnerSuitability>
    {
        public void Configure(EntityTypeBuilder<tblPartnerSuitability> builder)
        {
            builder.ToTable("tblPartnerSuitabilities");

            // Primary Key
            builder.HasKey(x => x.GdSuitabilityId)
                   .HasName("PK_tblPartnerSuitabilities");

            // Index
            builder.HasIndex(x => new { x.BitIsDeleted, x.DtDeletedDate })
                   .HasDatabaseName("tblPartnerSuitabilities_IsDeleted_DeletedDate");

            // Properties
            builder.Property(x => x.GdSuitabilityId)
                   .HasColumnName("gdSuitabilityId")
                   .IsRequired();

            builder.Property(x => x.VcLeadID)
                   .HasColumnName("vcLeadID")
                   .HasMaxLength(200);

            builder.Property(x => x.VcSalutation)
                   .HasColumnName("vcSalutation")
                   .HasMaxLength(20);

            builder.Property(x => x.VcName)
                   .HasColumnName("vcName")
                   .HasMaxLength(510)
                   .IsRequired();

            builder.Property(x => x.VcFirstName)
                   .HasColumnName("vcFirstName")
                   .HasMaxLength(200);

            builder.Property(x => x.VcMiddleName)
                   .HasColumnName("vcMiddleName")
                   .HasMaxLength(200);

            builder.Property(x => x.VcLastName)
                   .HasColumnName("vcLastName")
                   .HasMaxLength(200);

            builder.Property(x => x.VcDOB)
                   .HasColumnName("vcDOB")
                   .HasMaxLength(100);

            builder.Property(x => x.DtDOB)
                   .HasColumnName("dtDOB");

            builder.Property(x => x.IntNationality)
                   .HasColumnName("intNationality")
                   .IsRequired();

            builder.Property(x => x.ChGender)
                   .HasColumnName("chGender")
                   .HasColumnType("char(1)")
                   .IsRequired();

            builder.Property(x => x.VcMobile)
                   .HasColumnName("vcMobile")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.VcInternationalMobile)
                   .HasColumnName("vcInternationalMobile")
                   .HasMaxLength(100);

            builder.Property(x => x.VcEmail)
                   .HasColumnName("vcEmail")
                   .HasMaxLength(400)
                   .IsRequired();

            builder.Property(x => x.VcCountry)
                   .HasColumnName("vcCountry")
                   .HasMaxLength(100);

            builder.Property(x => x.VcCountryCode)
                   .HasColumnName("vcCountryCode")
                   .HasMaxLength(100);

            builder.Property(x => x.BtIsSmoker)
                   .HasColumnName("btIsSmoker")
                   .IsRequired();

            builder.Property(x => x.VcLifeStage)
                   .HasColumnName("vcLifeStage")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.VcRiskProfile)
                   .HasColumnName("vcRiskProfile")
                   .HasMaxLength(100);

            builder.Property(x => x.DcAnnualIncome)
                   .HasColumnName("dcAnnualIncome")
                   .HasColumnType("decimal(18,7)");

            builder.Property(x => x.IntExistingInsurance)
                   .HasColumnName("intExistingInsurance");

            builder.Property(x => x.DcExistingSumAssured)
                   .HasColumnName("dcExistingSumAssured")
                   .HasColumnType("decimal(18,7)");

            builder.Property(x => x.IntLifeGoals)
                   .HasColumnName("intLifeGoals");

            builder.Property(x => x.IntPolicyTerm)
                   .HasColumnName("intPolicyTerm")
                   .IsRequired();

            builder.Property(x => x.DcGoalCurrentValue)
                   .HasColumnName("dcGoalCurrentValue")
                   .HasColumnType("decimal(18,7)");

            builder.Property(x => x.DcTimeToAchieveGoal)
                   .HasColumnName("dcTimeToAchieveGoal")
                   .HasColumnType("decimal(18,7)");

            builder.Property(x => x.DcGoalFutureValue)
                   .HasColumnName("dcGoalFutureValue")
                   .HasColumnType("decimal(18,7)");

            builder.Property(x => x.VcProductCategory)
                   .HasColumnName("vcProductCategory")
                   .HasMaxLength(510);

            builder.Property(x => x.VcScheme)
                   .HasColumnName("vcScheme")
                   .HasMaxLength(510);

            builder.Property(x => x.VcJourneyId)
                   .HasColumnName("vcJourneyId")
                   .HasMaxLength(200);

            builder.Property(x => x.VcLastAccessIP)
                   .HasColumnName("vcLastAccessIP")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.VcCreatedBy)
                   .HasColumnName("vcCreatedBy")
                   .HasMaxLength(100)
                   .IsRequired();

            builder.Property(x => x.DtCreateDate)
                   .HasColumnName("dtCreateDate")
                   .IsRequired();

            builder.Property(x => x.VcModifiedBy)
                   .HasColumnName("vcModifiedBy")
                   .HasMaxLength(100);

            builder.Property(x => x.DtModifiedDate)
                   .HasColumnName("dtModifiedDate");

            builder.Property(x => x.DtDeletedDate)
                   .HasColumnName("dtDeletedDate");

            builder.Property(x => x.BitIsDeleted)
                   .HasColumnName("bitIsDeleted")
                   .IsRequired();


        }
    }
}
