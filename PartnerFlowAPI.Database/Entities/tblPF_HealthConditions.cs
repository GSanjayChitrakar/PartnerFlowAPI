using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerFlowAPI.Entities
{
    public class tblPF_HealthConditions
    {
        [Key]
        public int intHealthConditionId { get; set; }

        [Required]
        [MaxLength(200)]
        public string vcApplicationNumber { get; set; } = null!;

        public int intAssureType { get; set; }

        public float? ftHeightInCms { get; set; }
        public float? ftHeightInInches { get; set; }
        public float? ftHeightInFeet { get; set; }
        public float? ftWeightinKgs { get; set; }

        public bool? btWeightChange6M { get; set; }
        public bool? btWeightChange1Year { get; set; }

        [MaxLength(100)]
        public string? vcWeightGainOrLost { get; set; }

        public float? ftWeightGainOrLostInKgs { get; set; }

        [MaxLength(510)]
        public string? vcCauseOfWeightIncrease { get; set; }

        public bool? btIsPregnent { get; set; }
        public int? intDurationOfPregnantInWeek { get; set; }
        public bool? btComplicationInPregnency { get; set; }
        public bool? btGynaecologicalProblem { get; set; }

        [MaxLength(1000)]
        public string? vcGynaecologicalProblemDetail { get; set; }

        public DateTime? dtDateOfLastDelivery { get; set; }
        public bool? btGynaecologicalComplications { get; set; }
        public DateTime? dtApproxDueDateOfDelivery { get; set; }
        public bool? btDiagnosedMedicalTreatment { get; set; }
        public bool? btSpouseSufferingHivOrHepatitis { get; set; }

        [MaxLength(1000)]
        public string? vcSpouseSufferingHivOrHepatitisDetail { get; set; }

        public DateTime? dtDateOfFirstDiagnosisForHivOrHepatitis { get; set; }
        public bool? btLaTreatmentSame { get; set; }

        [MaxLength(1000)]
        public string? vcExactTreatmentMedicalDetail { get; set; }

        public bool? btIsFamilySufferAnyNeurologicalDisorder { get; set; }

        [MaxLength(1000)]
        public string? vcFamilySufferAnyNeurologicalDisorderDetail { get; set; }

        public bool? btIsAnyChestRelatedCheckups { get; set; }
        public DateTime? dtFirstDiagnosisOfAnyChestRelatedCheckupsDate { get; set; }

        [MaxLength(1000)]
        public string? vcDiagnosisOfAnyChestRelatedCheckupsDetail { get; set; }

        [MaxLength(1000)]
        public string? vcDiagnosisOfAnyChestRelatedCheckupsOngoing { get; set; }

        public bool? btAsthmaOrTuberculosis { get; set; }
        public bool? btUlcerOrPencreatic { get; set; }
        public bool? btArthritisOrBone { get; set; }

        [MaxLength(1000)]
        public string? vcArthritisDiagnosis { get; set; }

        [MaxLength(1000)]
        public string? vcAffectedjoints { get; set; }

        public DateTime? dtDiagnosisDate { get; set; }
        public DateTime? vcStillHaveSymptoms { get; set; }

        public bool? btBloodDisorder { get; set; }
        public bool? btCancerOrTumour { get; set; }
        public bool? btChestPainOrHeartAttack { get; set; }
        public bool? btCongenitalOrHereditary { get; set; }
        public bool? btDiabetesOrsugarInUrine { get; set; }
        public bool? btEyeNoseEarSkinDisorder { get; set; }
        public bool? btHypertension { get; set; }
        public bool? btHivTesting { get; set; }
        public bool? btLiverGallbladderJaundice { get; set; }
        public bool? btMentalHealthDisorders { get; set; }
        public bool? btPhysicalImpairment { get; set; }
        public bool? btNeurologicalDisorders { get; set; }
        public bool? btHormonalDisorders { get; set; }
        public bool? btUrologicalReproductiveDisorders { get; set; }
        public bool? btMedicalTreatmentHistoryLast5Years { get; set; }
        public bool? btCirculatorySystemDisorder { get; set; }
        public bool? btCurrentlySufferingOtherThanAboveDetails { get; set; }
        public bool? btDigestiveDisorder { get; set; }
        public bool? btKidneyStoneDisorder { get; set; }
        public bool? btMedicalHistory { get; set; }
        public bool? btPsychiatricDisorder { get; set; }
        public bool? btSpouseAdvisedTest { get; set; }
        public bool? btSurgeryOrInvetigations { get; set; }
        public bool? btThyroidDisorder { get; set; }

        [MaxLength(100)]
        public string? vcInvestigationsTreatment { get; set; }

        [MaxLength(510)]
        public string? vcTreatmentDetails { get; set; }

        [MaxLength(100)]
        [Required]
        public string vcLastAccessIP { get; set; } = null!;

        [MaxLength(100)]
        [Required]
        public string vcCreatedBy { get; set; } = null!;

        [Required]
        public DateTime dtCreateDate { get; set; }

        [MaxLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }
        public DateTime? dtDeletedDate { get; set; }

        [Required]
        public bool bitIsDeleted { get; set; }

        // Remaining medical flags
        public bool? GyneacologicalDiagnosis { get; set; }
        public bool? HealthDisorder { get; set; }
        public bool? HospitalizedOrUndergoneAnySurgery { get; set; }
        public bool? AccidentInsuranceBeenDeclinedEver { get; set; }
        public bool? AnyFamilyMemberUndergoneForCovid19Test { get; set; }
        public bool? HeridetoryDisorder { get; set; }
        public bool? ThroatSkinDisorder { get; set; }
        public bool? HypertensionHighBloodPressure { get; set; }
        public bool? HivAidsInfection { get; set; }
        public bool? NervousMentalDisorder { get; set; }
        public bool? PhysicalImpairmentDisabilityHandicap { get; set; }
        public bool? BrainDisorderDetails { get; set; }
        public bool? ThyroidOrHormonalDisorder { get; set; }
        public bool? ReproductiveOrganDisorder { get; set; }
        public bool? UndergoneAnyTreatmentInLast5Years { get; set; }
        public bool? MammographyBiopsyDetails { get; set; }
        public bool? PregnancyDetailsDisorder { get; set; }

        [MaxLength(1000)]
        public string? CauseOfChange { get; set; }

        public DateTime? DueDateOfDelivery { get; set; }
        public bool? SpousePartnerHIVAIDS { get; set; }
        public bool? HaveUnderGoAnyTreatment { get; set; }

        [MaxLength(1000)]
        public string? ProvideDetails { get; set; }

        [MaxLength(1000)]
        public string? ProvidePregencyDetails { get; set; }

        [MaxLength(100)]
        public string? DurationInWeek { get; set; }

        public bool? btAnyOtherIllness { get; set; }
        public bool? btHypertensionHeartattack { get; set; }
        public bool? btParalysisStroke { get; set; }
        public bool? btMoreInformation { get; set; }
        public bool? btBrainEyeEar { get; set; }
    }
}
