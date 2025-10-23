using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Entities
{
    public class tblPF_NomineeDetails
    {
        [Key]
        public int intNomineeDetailsId { get; set; }

        public int? intAssureType { get; set; }
        public int? intFamilyDetailsId { get; set; }

        [StringLength(100)]
        public string? vcRelation { get; set; }

        [StringLength(200)]
        public string vcApplicationNumber { get; set; } = null!;

        [StringLength(20)]
        public string? vcTitle { get; set; }

        [StringLength(100)]
        public string vcFirstName { get; set; } = null!;

        [StringLength(100)]
        public string? vcMiddleName { get; set; }

        [StringLength(100)]
        public string? vcLastName { get; set; }

        public DateTime? dtDOB { get; set; }

        [StringLength(2)]
        public string? chGender { get; set; }

        public decimal? dcAnnualIncome { get; set; }

        [StringLength(100)]
        public string? vcMobileNumber { get; set; }

        public double? ftNomineePercentage { get; set; }

        [StringLength(200)]
        public string? vcOccupation { get; set; }

        public bool? btIsAppointee { get; set; }

        [StringLength(1000)]
        public string? vcAppointeeRelation { get; set; }

        [StringLength(100)]
        public string? vcAppointeeTile { get; set; }

        [StringLength(1000)]
        public string? vcAppointeeFirstName { get; set; }

        [StringLength(1000)]
        public string? vcAppointeeMiddleName { get; set; }

        [StringLength(1000)]
        public string? vcAppointeeLastName { get; set; }

        public DateTime? vcAppointeeDob { get; set; }

        [StringLength(2)]
        public string? chAppointeeGender { get; set; }

        [StringLength(40)]
        public string? vcAppointeeContactNumber { get; set; }

        [StringLength(20)]
        public string? vcAppointeeCkycNumber { get; set; }

        [StringLength(1000)]
        public string? vcAppointeeAddress1 { get; set; }

        [StringLength(1000)]
        public string? vcAppointeeAddress2 { get; set; }

        [StringLength(1000)]
        public string? vcAppointeeLandmark { get; set; }

        [StringLength(1000)]
        public string? vcAppointeeCity { get; set; }

        [StringLength(1000)]
        public string? vcAppointeeState { get; set; }

        [StringLength(1000)]
        public string? vcAppointeeCountry { get; set; }

        [StringLength(100)]
        public string? vcAppointeePincode { get; set; }

        public bool? btAppointeeAddressSameAsNominee { get; set; }

        [StringLength(100)]
        public string? vcRelationshipWithNominee { get; set; }

        [StringLength(1000)]
        public string? vcAppointeeSignature { get; set; }

        public int? intAgeAtOnset { get; set; }

        [StringLength(100)]
        public string? vcLivingOrDeceased { get; set; }

        [StringLength(100)]
        public string? vcDiagnosis { get; set; }

        [StringLength(100)]
        public string vcLastAccessIP { get; set; } = null!;

        [StringLength(100)]
        public string vcCreatedBy { get; set; } = null!;

        public DateTime dtCreateDate { get; set; }

        [StringLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }
        public DateTime? dtDeletedDate { get; set; }

        public bool bitIsDeleted { get; set; }

        public int intDeDupeStatus { get; set; } = 0;
        public int intImageQCStatus { get; set; } = 0;
        public int intDocQCStatus { get; set; } = 0;
        public string? vcLifeAsiaClientId { get; set; }
        public DateTime? dtClientIDGeneratedOn { get; set; }
        public int intAppointeeDeDupeStatus { get; set; } = 0;
        public int intAppointeeImageQCStatus { get; set; } = 0;
        public int intAppointeeDocQCStatus { get; set; } = 0;
        public string? vcAppointeeLAClientID { get; set; }
        public DateTime? dtAppointeeClientIDGeneratedOn { get; set; }
        public string? vcBankAccountNumber { get; set; }
        public string? vcIFSCCODE { get; set; }
        public string? vcBANKNAME { get; set; }
        public string? vcBranchLocation { get; set; }
        public string? vcAppointeeDedupeMode { get; set; }
        public string? vcNomineeDedupeMode { get; set; }
    }
}
