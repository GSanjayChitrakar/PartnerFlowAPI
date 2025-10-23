using System;
using System.ComponentModel.DataAnnotations;

namespace YourNamespace.Entities
{
    public class tblPF_OtherInsurances
    {
        [Key]
        public int intOtherInsuranceId { get; set; }

        [StringLength(100)]
        public string vcApplicationNumber { get; set; } = null!;

        public int intAssureType { get; set; }

        [StringLength(100)]
        public string? vcInsurerNumber { get; set; }

        [StringLength(100)]
        public string? vcToBeInsured { get; set; }

        [StringLength(100)]
        public string? vcDeathSumAssured { get; set; }

        [StringLength(100)]
        public string? vcPremiumPerAnnum { get; set; }

        public DateTime? dtMonthAndYearOfInssuance { get; set; }

        [StringLength(100)]
        public string? vcAcceptanceTerm { get; set; }

        [StringLength(100)]
        public string? vcCurrentStatus { get; set; }

        [StringLength(100)]
        public string? vcPolicyOrAppliactionNumber { get; set; }

        [StringLength(200)]
        public string? vcStandardAcceptance { get; set; }

        public DateTime? dtProposalDate { get; set; }

        [StringLength(100)]
        public string? vcTypeofpolicyterm { get; set; }

        [StringLength(100)]
        public string vcLastAccessIP { get; set; } = null!;

        [StringLength(100)]
        public string vcCreatedBy { get; set; } = null!;

        public DateTime dtCreateDate { get; set; }

        [StringLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }

        public DateTime? dtDeletedDate { get; set; }

        public bool bitIsDeleted { get; set; } = false;
    }
}
