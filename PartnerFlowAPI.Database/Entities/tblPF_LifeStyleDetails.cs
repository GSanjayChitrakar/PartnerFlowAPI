using System;

namespace YourNamespace.Entities
{
    public class tblPF_LifeStyleDetails
    {
        public int intLifeStyleId { get; set; }
        public string vcApplicationNumber { get; set; } = null!;
        public int intAssureType { get; set; }
        public string? btConsumeTobacco { get; set; }
        public int? intDurationTobaccoConsumption { get; set; }
        public string? vcTobaccoConsumptionWhatForm { get; set; }
        public DateTime? dtTobaccoConsumptionStopOn { get; set; }
        public DateTime? dtCigarettesIfstoppedconsuming { get; set; }
        public int? intNumberPerDay { get; set; }
        public int? intNumberOfMonths { get; set; }
        public bool? btFormOfCigarettes { get; set; }
        public int? intNumberOfCigarettesPerDay { get; set; }
        public int? intNumberOfCigarettesPerMonth { get; set; }
        public bool? btFormOfBidi { get; set; }
        public int? intNumberOfBidiPerDay { get; set; }
        public int? intNumberOfBidiPerMonth { get; set; }
        public string? vcBidiStoppedYear { get; set; }
        public string? vcBidiStoppedMonth { get; set; }
        public bool? btFormOfGutka { get; set; }
        public int? intNumberOfGutkaPerDay { get; set; }
        public int? intNumberOfGutkaPerMonths { get; set; }
        public string? vcGutkaStoppedYear { get; set; }
        public string? vcGutkaStoppedMonths { get; set; }
        public bool? btNeverConsume { get; set; }
        public bool? btStoppedConsumption { get; set; }
        public string? vcStoppedOn { get; set; }
        public bool? btConsumeAlchohol { get; set; }
        public int? intConsumeAlchoholFrequency { get; set; }
        public bool? btHardLiquorConsume { get; set; }
        public int? intHardLiquorPerWeek { get; set; }
        public int? intBeerBottlesPerWeek { get; set; }
        public int? intBeerBottlesPerMonths { get; set; }
        public string? vcBeerStoppedYears { get; set; }
        public string? vcBeerStoppedMonths { get; set; }
        public bool? btWineConsume { get; set; }
        public int? intWineGlassPerWeek { get; set; }
        public int? intWineBottlesPerMonths { get; set; }
        public string? vcWineStoppedYear { get; set; }
        public string? vcWineStoppedMonths { get; set; }
        public bool? btNarcoticOrDrug { get; set; }
        public bool? btNarcoticOrDrugStopped { get; set; }
        public int? intQuantityOfDrugConsumePerDay { get; set; }
        public bool? btDrugNeverConsume { get; set; }
        public bool? btDrugStopConsumption { get; set; }
        public string? vcDrugStopppedOn { get; set; }
        public bool? btDrugMarijuanaConsumed { get; set; }
        public int? intMarijuanaPerDay { get; set; }
        public int? intMarijuanaPerMonth { get; set; }
        public string? vcMarijuanaStoppedYear { get; set; }
        public string? vcMarijuanaStoppedMonths { get; set; }
        public bool? btDrugCocaineConsumed { get; set; }
        public int? intCocainePerDay { get; set; }
        public int? intCocainePerMonth { get; set; }
        public string? vcCocaineStoppedYear { get; set; }
        public string? vcCocaineStoppedMonths { get; set; }
        public bool? btDrugAddictiveConsumed { get; set; }
        public string? vcNameOfDrug { get; set; }
        public DateTime? dtDrugConsumptionStopOn { get; set; }
        public bool? btHazardousHobbiesSports { get; set; }
        public string? vcHobbiesOrSportsRisk { get; set; }
        public int? intAddictiveDrugPerDay { get; set; }
        public int? intAddictiveDrugPerMonth { get; set; }
        public string? vcAddictiveDrugStoppedYear { get; set; }
        public string? vcAddictiveDrugStoppedMonths { get; set; }
        public bool? btHazardousHobbies { get; set; }
        public string? vcHazardousHobbies { get; set; }
        public int? intHazardousHobbies { get; set; }
        public string? vcOwnAsset { get; set; }
        public bool? btOutSideIndiaLast30Days { get; set; }
        public bool? btIsHospitalizedForCovid { get; set; }
        public DateTime? dtDateOnHospitalizedForCovid { get; set; }
        public DateTime? dtDateOnReleaseFromHospitalForCovid { get; set; }
        public bool? btIcuRequirement { get; set; }
        public bool? btIscomplicationSuffered { get; set; }
        public string? vcComplicationDetails { get; set; }
        public string? vcOutSideIndiaReason { get; set; }
        public string? VcWhatForm { get; set; }
        public int? intLiquorPegsPerFrequency { get; set; }
        public int? intBeerPintPerFrequency { get; set; }
        public int? intWineGlassesPerFrequency { get; set; }
        public bool? btAlchoholWithdrawal { get; set; }
        public string? vcHazardousReflexTypeIds { get; set; }
        public bool bitIsDeleted { get; set; }
        public bool? btCovid19 { get; set; }
        public bool? btConsumeAlcohol { get; set; }
        public string? vcConsumeNarcotics { get; set; }
        public int? intQuantityOfNarcotics { get; set; }
        public DateTime? dtAdmission { get; set; }
        public DateTime? dtDischarge { get; set; }
        public bool? btConsumeTobaccoAlchoholNarcoticSubstance { get; set; }
        public bool? btConsumeTobaccoAlchoholNarcoticSubstanceConsumptionStopped { get; set; }
        public string? vcDurationTobacco { get; set; }
        public string? vcDurationAlcohol { get; set; }
        public string? vcDurationNarcotics { get; set; }
        public string? vcDurationSinceStopped { get; set; }
        public string? vcChooseHabbitSubstance { get; set; }
        public string? vcReasonForDiscontinuation { get; set; }
        public int? intQuantityCigarCigarettesBeediPaan { get; set; }
        public int? intQuantityBeerWineHardLiquor { get; set; }
        public int? intQuantityAnyNarcotics { get; set; }
        public string vcLastAccessIP { get; set; } = null!;
        public string vcCreatedBy { get; set; } = null!;
        public DateTime dtCreateDate { get; set; }
        public string? vcModifiedBy { get; set; }
        public DateTime? dtModifiedDate { get; set; }
        public DateTime? dtDeletedDate { get; set; }
    }
}
