using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartnerFlowAPI.Entities
{
    [Table("tblPF_FamilyDetails")]
    public class tblPF_FamilyDetails
    {
        [Key]
        public int intFamilyDetailsId { get; set; }

        [MaxLength(100)]
        public string? vcRelation { get; set; }

        [Required, MaxLength(200)]
        public string vcApplicationNumber { get; set; } = string.Empty;

        public int intAssureType { get; set; }

        [MaxLength(20)]
        public string? vcTitle { get; set; }

        [Required, MaxLength(100)]
        public string vcFirstName { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? vcMiddleName { get; set; }

        [Required, MaxLength(100)]
        public string vcLastName { get; set; } = string.Empty;

        public DateTime? dtDOB { get; set; }

        [MaxLength(2)]
        public string? chGender { get; set; }

        [Column(TypeName = "decimal(18,0)")]
        public decimal? dcAnnualIncome { get; set; }

        [MaxLength(100)]
        public string? vcMobileNumber { get; set; }

        public int? intTotalLIfeSA { get; set; }

        public int? intVitalStatus { get; set; }

        [MaxLength(200)]
        public string? vcCauseOfDeath { get; set; }

        public int? intAgeAtDeath { get; set; }

        [MaxLength(20)]
        public string? vcHealthStatus { get; set; }

        [MaxLength(200)]
        public string? vcOccupation { get; set; }

        [Required, MaxLength(100)]
        public string vcLastAccessIP { get; set; } = string.Empty;

        [Required, MaxLength(100)]
        public string vcCreatedBy { get; set; } = string.Empty;

        public DateTime dtCreateDate { get; set; }

        [MaxLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }

        public DateTime? dtDeletedDate { get; set; }

        public bool bitIsDeleted { get; set; }

        public bool? btIsAppointee { get; set; }

        [MaxLength(1000)]
        public string? vcNameOfAppointee { get; set; }

        public DateTime? vcAppointeeDob { get; set; }

        [MaxLength(2)]
        public string? chAppointeeGender { get; set; }

        [MaxLength(40)]
        public string? vcAppointeeContactNumber { get; set; }

        [MaxLength(20)]
        public string? vcAppointeeCkycNumber { get; set; }

        [MaxLength(1000)]
        public string? vcAppointeeAddress1 { get; set; }

        [MaxLength(1000)]
        public string? vcAppointeeAddress2 { get; set; }

        [MaxLength(1000)]
        public string? vcAppointeeLandmark { get; set; }

        [MaxLength(1000)]
        public string? vcAppointeeCity { get; set; }

        [MaxLength(1000)]
        public string? vcAppointeeState { get; set; }

        [MaxLength(1000)]
        public string? vcAppointeeCountry { get; set; }

        [MaxLength(100)]
        public string? vcAppointeePincode { get; set; }

        public bool? btAppointeeAddressSameAsNominee { get; set; }

        [MaxLength(100)]
        public string? vcRelationshipWithNominee { get; set; }

        [MaxLength(1000)]
        public string? vcAppointeeSignature { get; set; }

        public int? intAgeAtOnset { get; set; }

        [MaxLength(100)]
        public string? vcLivingOrDeceased { get; set; }

        [MaxLength(100)]
        public string? vcDiagnosis { get; set; }

        public bool? btIsNominee { get; set; }

        public double? ftNomineePercentage { get; set; }
    }
}
