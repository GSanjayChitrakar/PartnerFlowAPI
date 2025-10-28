using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartnerFlowAPI.Entities;

namespace PartnerFlowAPI.Configurations
{
    public class tblPF_LifeStyleDetailsConfiguration : IEntityTypeConfiguration<tblPF_LifeStyleDetails>
    {
        public void Configure(EntityTypeBuilder<tblPF_LifeStyleDetails> entity)
        {
            entity.ToTable("tblPF_LifeStyleDetails");

            entity.HasKey(e => e.intLifeStyleId)
                  .HasName("PK_tblPF_LifeStyleDetails");

            entity.HasIndex(e => e.vcApplicationNumber)
                  .HasDatabaseName("tblPF_LifeStyleDetails_ApplicationNumber");

            entity.Property(e => e.intLifeStyleId);
            entity.Property(e => e.vcApplicationNumber).IsRequired().HasMaxLength(200);
            entity.Property(e => e.intAssureType);
            entity.Property(e => e.btConsumeTobacco).HasMaxLength(100);
            entity.Property(e => e.intDurationTobaccoConsumption);
            entity.Property(e => e.vcTobaccoConsumptionWhatForm).HasMaxLength(510);
            entity.Property(e => e.dtTobaccoConsumptionStopOn).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtCigarettesIfstoppedconsuming).HasColumnType("datetime2(7)");
            entity.Property(e => e.intNumberPerDay);
            entity.Property(e => e.intNumberOfMonths);
            entity.Property(e => e.btFormOfCigarettes);
            entity.Property(e => e.intNumberOfCigarettesPerDay);
            entity.Property(e => e.intNumberOfCigarettesPerMonth);
            entity.Property(e => e.btFormOfBidi);
            entity.Property(e => e.intNumberOfBidiPerDay);
            entity.Property(e => e.intNumberOfBidiPerMonth);
            entity.Property(e => e.vcBidiStoppedYear).HasMaxLength(20);
            entity.Property(e => e.vcBidiStoppedMonth).HasMaxLength(30);
            entity.Property(e => e.btFormOfGutka);
            entity.Property(e => e.intNumberOfGutkaPerDay);
            entity.Property(e => e.intNumberOfGutkaPerMonths);
            entity.Property(e => e.vcGutkaStoppedYear).HasMaxLength(20);
            entity.Property(e => e.vcGutkaStoppedMonths).HasMaxLength(30);
            entity.Property(e => e.btNeverConsume);
            entity.Property(e => e.btStoppedConsumption);
            entity.Property(e => e.vcStoppedOn).HasMaxLength(510);
            entity.Property(e => e.btConsumeAlchohol);
            entity.Property(e => e.intConsumeAlchoholFrequency);
            entity.Property(e => e.btHardLiquorConsume);
            entity.Property(e => e.intHardLiquorPerWeek);
            entity.Property(e => e.intBeerBottlesPerWeek);
            entity.Property(e => e.intBeerBottlesPerMonths);
            entity.Property(e => e.vcBeerStoppedYears).HasMaxLength(20);
            entity.Property(e => e.vcBeerStoppedMonths).HasMaxLength(30);
            entity.Property(e => e.btWineConsume);
            entity.Property(e => e.intWineGlassPerWeek);
            entity.Property(e => e.intWineBottlesPerMonths);
            entity.Property(e => e.vcWineStoppedYear).HasMaxLength(20);
            entity.Property(e => e.vcWineStoppedMonths).HasMaxLength(30);
            entity.Property(e => e.btNarcoticOrDrug);
            entity.Property(e => e.btNarcoticOrDrugStopped);
            entity.Property(e => e.intQuantityOfDrugConsumePerDay);
            entity.Property(e => e.btDrugNeverConsume);
            entity.Property(e => e.btDrugStopConsumption);
            entity.Property(e => e.vcDrugStopppedOn).HasMaxLength(1000);
            entity.Property(e => e.btDrugMarijuanaConsumed);
            entity.Property(e => e.intMarijuanaPerDay);
            entity.Property(e => e.intMarijuanaPerMonth);
            entity.Property(e => e.vcMarijuanaStoppedYear).HasMaxLength(20);
            entity.Property(e => e.vcMarijuanaStoppedMonths).HasMaxLength(30);
            entity.Property(e => e.btDrugCocaineConsumed);
            entity.Property(e => e.intCocainePerDay);
            entity.Property(e => e.intCocainePerMonth);
            entity.Property(e => e.vcCocaineStoppedYear).HasMaxLength(20);
            entity.Property(e => e.vcCocaineStoppedMonths).HasMaxLength(30);
            entity.Property(e => e.btDrugAddictiveConsumed);
            entity.Property(e => e.vcNameOfDrug).HasMaxLength(200);
            entity.Property(e => e.dtDrugConsumptionStopOn).HasColumnType("datetime2(7)");
            entity.Property(e => e.btHazardousHobbiesSports);
            entity.Property(e => e.vcHobbiesOrSportsRisk).HasMaxLength(1000);
            entity.Property(e => e.intAddictiveDrugPerDay);
            entity.Property(e => e.intAddictiveDrugPerMonth);
            entity.Property(e => e.vcAddictiveDrugStoppedYear).HasMaxLength(20);
            entity.Property(e => e.vcAddictiveDrugStoppedMonths).HasMaxLength(30);
            entity.Property(e => e.btHazardousHobbies);
            entity.Property(e => e.vcHazardousHobbies).HasMaxLength(1000);
            entity.Property(e => e.intHazardousHobbies);
            entity.Property(e => e.vcOwnAsset).HasMaxLength(1000);
            entity.Property(e => e.btOutSideIndiaLast30Days);
            entity.Property(e => e.btIsHospitalizedForCovid);
            entity.Property(e => e.dtDateOnHospitalizedForCovid).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtDateOnReleaseFromHospitalForCovid).HasColumnType("datetime2(7)");
            entity.Property(e => e.btIcuRequirement);
            entity.Property(e => e.btIscomplicationSuffered);
            entity.Property(e => e.vcComplicationDetails).HasMaxLength(1000);
            entity.Property(e => e.vcOutSideIndiaReason).HasMaxLength(1000);
            entity.Property(e => e.VcWhatForm).HasMaxLength(1000);
            entity.Property(e => e.intLiquorPegsPerFrequency);
            entity.Property(e => e.intBeerPintPerFrequency);
            entity.Property(e => e.intWineGlassesPerFrequency);
            entity.Property(e => e.btAlchoholWithdrawal);
            entity.Property(e => e.vcHazardousReflexTypeIds).HasMaxLength(2000);
            entity.Property(e => e.bitIsDeleted);
            entity.Property(e => e.btCovid19);
            entity.Property(e => e.btConsumeAlcohol);
            entity.Property(e => e.vcConsumeNarcotics).HasMaxLength(500);
            entity.Property(e => e.intQuantityOfNarcotics);
            entity.Property(e => e.dtAdmission).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtDischarge).HasColumnType("datetime2(7)");
            entity.Property(e => e.btConsumeTobaccoAlchoholNarcoticSubstance).HasDefaultValue(false);
            entity.Property(e => e.btConsumeTobaccoAlchoholNarcoticSubstanceConsumptionStopped).HasDefaultValue(false);
            entity.Property(e => e.vcDurationTobacco).HasMaxLength(200);
            entity.Property(e => e.vcDurationAlcohol).HasMaxLength(200);
            entity.Property(e => e.vcDurationNarcotics).HasMaxLength(200);
            entity.Property(e => e.vcDurationSinceStopped).HasMaxLength(200);
            entity.Property(e => e.vcChooseHabbitSubstance).HasMaxLength(200);
            entity.Property(e => e.vcReasonForDiscontinuation).HasMaxLength(200);
            entity.Property(e => e.intQuantityCigarCigarettesBeediPaan);
            entity.Property(e => e.intQuantityBeerWineHardLiquor);
            entity.Property(e => e.intQuantityAnyNarcotics);
            entity.Property(e => e.vcLastAccessIP).IsRequired().HasMaxLength(100);
            entity.Property(e => e.vcCreatedBy).IsRequired().HasMaxLength(100);
            entity.Property(e => e.dtCreateDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.vcModifiedBy).HasMaxLength(100);
            entity.Property(e => e.dtModifiedDate).HasColumnType("datetime2(7)");
            entity.Property(e => e.dtDeletedDate).HasColumnType("datetime2(7)");
        }
    }
}
