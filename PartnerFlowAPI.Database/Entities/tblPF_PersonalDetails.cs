using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YourNamespace.Entities
{
    public class tblPF_PersonalDetails
    {
        [Key]
        public int intPersonalId { get; set; }

        [StringLength(200)]
        public string vcApplicationNumber { get; set; } = null!;

        public int intAssureType { get; set; }

        [StringLength(20)]
        public string? vcTitle { get; set; }

        [StringLength(200)]
        public string? vcCompanyName { get; set; }

        [StringLength(510)]
        public string? vcFirstName { get; set; }

        [StringLength(510)]
        public string? vcMiddleName { get; set; }

        [StringLength(510)]
        public string? vcLastName { get; set; }

        [StringLength(20)]
        public string? vcNameBeforeMarriageTitle { get; set; }

        [StringLength(510)]
        public string? vcNameBeforeMarriageFirstName { get; set; }

        [StringLength(510)]
        public string? vcNameBeforeMarriageMiddleName { get; set; }

        [StringLength(510)]
        public string? vcNameBeforeMarriageLastName { get; set; }

        [StringLength(100)]
        public string? vcAdharNumber { get; set; }

        [StringLength(20)]
        public string? vcPANNumber { get; set; }

        public int? intMaritalStatus { get; set; }

        public int? intNumberOfChildren { get; set; }

        [StringLength(100)]
        public string? vcCKYCNumber { get; set; }

        [StringLength(200)]
        public string? vcEducationQualification { get; set; }

        public decimal? dcAnnualIncome { get; set; }

        [StringLength(200)]
        public string? vcAUCustomerId { get; set; }

        [StringLength(200)]
        public string? vcAUSavingsOrLoanAccountNumber { get; set; }

        public char? chGender { get; set; }

        public DateTime? dtDOB { get; set; }

        [StringLength(200)]
        public string? vcCountryOfBirth { get; set; }

        public int? intNationality { get; set; }

        public bool? btIsCriminalRecord { get; set; }

        [StringLength(2000)]
        public string? vcCriminalCaseDescription { get; set; }

        public bool? btRelatedToPoliticalParty { get; set; }

        [StringLength(2000)]
        public string? vcPoliticalDescription { get; set; }

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

        [StringLength(20)]
        public string? vcLifeAsiaClientId { get; set; }

        public DateTime? dtClientIDGeneratedOn { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime SysStartTime { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime SysEndTime { get; set; } = DateTime.Parse("9999-12-31 23:59:59");

        [StringLength(100)]
        public string? vcRelationWithLA { get; set; }

        [StringLength(100)]
        public string? vcInterNationalNumber { get; set; }

        [StringLength(100)]
        public string? vcCountryOfResidence { get; set; }

        public bool? btPanVerified { get; set; } = false;

        public bool? btPanDOBMatch { get; set; } = false;

        public bool? btPanNameMatch { get; set; } = false;

        [StringLength(200)]
        public string? vcPanDublicate { get; set; }

        [StringLength(400)]
        public string? vcRequestId { get; set; }

        public bool? btisPanVerify { get; set; }

        public int? intAMLStatus { get; set; }

        [StringLength(100)]
        public string? vcCibilScore { get; set; }

        [StringLength(100)]
        public string? vcIncomeEstimator { get; set; }

        [StringLength(100)]
        public string? vcIIBScore { get; set; }

        [StringLength(200)]
        public string? vcDedupeMode { get; set; }
    }
}
