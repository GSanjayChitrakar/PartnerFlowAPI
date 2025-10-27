using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Entities
{
    public class tblPF_RiderDetails
    {
        [Key]
        public int intRiderDetailId { get; set; }

        [Required, MaxLength(200)]
        public string vcApplicationNumber { get; set; }

        [MaxLength(200)]
        public string? vcQuotationId { get; set; }

        [MaxLength(100)]
        public string? vcRiderType { get; set; }

        [MaxLength(100)]
        public string? vcProductCode { get; set; }

        [MaxLength(100)]
        public string? vcRiderCode { get; set; }

        [MaxLength(510)]
        public string? vcProductName { get; set; }

        [MaxLength(510)]
        public string? vcRiderName { get; set; }

        public decimal? dcModalPremium { get; set; }
        public decimal? dcSumAssured { get; set; }
        public decimal? dcTotalPremium { get; set; }
        public decimal? dcAnnualizedPremium { get; set; }
        public decimal? dcTax { get; set; }

        public int intPT { get; set; }
        public int intPPT { get; set; }

        [MaxLength(200)]
        public string? vcMode { get; set; }

        [MaxLength(200)]
        public string? vcUIN { get; set; }

        [MaxLength(100)]
        public string? vcReturnOfPremium { get; set; }

        [MaxLength(100)]
        public string? vcBenefitTypePayout { get; set; }

        [MaxLength(100)]
        public string? vcIncomeFrequency { get; set; }

        [MaxLength(100)]
        public string? vcIncomeDuration { get; set; }

        [MaxLength(100)]
        public string? vcLumpSumBenefit { get; set; }

        [MaxLength(100)]
        public string? vcRiderOption { get; set; }

        [Required, MaxLength(100)]
        public string vcLastAccessIP { get; set; }

        [Required, MaxLength(100)]
        public string vcCreatedBy { get; set; }

        [Required]
        public DateTime dtCreateDate { get; set; }

        [MaxLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }
        public DateTime? dtDeletedDate { get; set; }

        [Required]
        public bool bitIsDeleted { get; set; }

        public int intRiderCreateStatus { get; set; }
    }
}
