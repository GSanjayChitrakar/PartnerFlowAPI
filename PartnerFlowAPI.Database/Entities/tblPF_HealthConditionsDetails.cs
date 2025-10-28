using System;

namespace PartnerFlowAPI.Entities
{
    public class tblPF_HealthConditionsDetails
    {
        public int intHealthConditionDetailsId { get; set; }
        public string vcApplicationNumber { get; set; } = null!;
        public int? intReflexQuestionTypeID { get; set; }
        public string? vcParentFieldName { get; set; }
        public string? vcNameOfIllness { get; set; }
        public DateTime? dtFirstDiagnosis { get; set; }
        public string? vcTreatmentDetails { get; set; }
        public string? vcCurrentStatus { get; set; }
        public bool? btSameTreatment { get; set; }
        public bool? btAnyCOmplecationConditions { get; set; }
        public string? vcFollowUpAdvise { get; set; }
        public string? vcAdditionalRemarks { get; set; }
        public string? vcMedicineDosageDetails { get; set; }
        public string? vcnameOfTreatingDoctor { get; set; }
        public string vcLastAccessIP { get; set; } = null!;
        public string vcCreatedBy { get; set; } = null!;
        public DateTime dtCreateDate { get; set; }
        public string? vcModifiedBy { get; set; }
        public DateTime? dtModifiedDate { get; set; }
        public DateTime? dtDeletedDate { get; set; }
        public bool bitIsDeleted { get; set; }
        public bool? btPregnancyDetailsDisorder { get; set; }
    }
}
