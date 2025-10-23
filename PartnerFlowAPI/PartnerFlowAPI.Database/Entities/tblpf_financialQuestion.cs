using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartnerFlowAPI.Domain.Entities
{
    [Table("tblpf_financialQuestion")]
    public class tblpf_financialQuestion
    {
        [Key]
        public int FinancialID { get; set; }

        [MaxLength(200)]
        public string? vcpremiumFinancedBy { get; set; }

        [MaxLength(200)]
        public string? vcoccupation { get; set; }

        [MaxLength(200)]
        public string? vcannualIncome { get; set; }

        [MaxLength(200)]
        public string? vcincome { get; set; }

        [Required]
        public bool btassessedForIncomeTax { get; set; }

        [MaxLength(100)]
        public string? vcpanNumber { get; set; }

        [MaxLength(200)]
        public string? vcsourceOfIncome { get; set; }

        [Required]
        public bool bthaveAgriculturalLand { get; set; }

        [MaxLength(200)]
        public string? vchaveAgriculturalLandDetails { get; set; }

        [MaxLength(200)]
        public string? vccashDepositesCertificates { get; set; }

        [MaxLength(200)]
        public string? vcnscUtiPpfPension { get; set; }

        [MaxLength(200)]
        public string? vccapitalInvestment { get; set; }

        [MaxLength(200)]
        public string? vcimmovableProperties { get; set; }

        [MaxLength(200)]
        public string? vcotherInvestmentsSavings { get; set; }

        [MaxLength(200)]
        public string? vctotal { get; set; }

        [MaxLength(200)]
        public string? vcnetWorth { get; set; }

        [MaxLength(200)]
        public string? vcyear2425 { get; set; }

        [MaxLength(200)]
        public string? vcyear2324 { get; set; }

        [MaxLength(200)]
        public string? vcyear2223 { get; set; }

        [MaxLength(200)]
        public string? vcAgriyear2425 { get; set; }

        [MaxLength(200)]
        public string? vcAgriyear2324 { get; set; }

        [MaxLength(200)]
        public string? vcAgriyear2223 { get; set; }

        [MaxLength(200)]
        public string? vcrentyear2425 { get; set; }

        [MaxLength(200)]
        public string? vcrentyear2324 { get; set; }

        [MaxLength(200)]
        public string? vcrentyear2223 { get; set; }

        [MaxLength(200)]
        public string? vccapitalgainyear2425 { get; set; }

        [MaxLength(200)]
        public string? vccapitalgainyear2324 { get; set; }

        [MaxLength(200)]
        public string? vccapitalgainyear2223 { get; set; }

        [MaxLength(200)]
        public string? vclongcapitalgainyear2425 { get; set; }

        [MaxLength(200)]
        public string? vclongcapitalgainyear2324 { get; set; }

        [MaxLength(200)]
        public string? vclongcapitalgainyear2223 { get; set; }

        [MaxLength(200)]
        public string? vcinterestyear2425 { get; set; }

        [MaxLength(200)]
        public string? vcinterestyear2324 { get; set; }

        [MaxLength(200)]
        public string? vcinterestyear2223 { get; set; }

        [MaxLength(200)]
        public string? vcotheryear2425 { get; set; }

        [MaxLength(200)]
        public string? vcotheryear2324 { get; set; }

        [MaxLength(200)]
        public string? vcotheryear2223 { get; set; }

        [MaxLength(200)]
        public string? vctotalanualyear2425 { get; set; }

        [MaxLength(200)]
        public string? vctotalanualyear2324 { get; set; }

        [MaxLength(200)]
        public string? vctotalanualyear2223 { get; set; }

        [MaxLength(200)]
        public string? vcApplicationNumber { get; set; }

        public int? intAssureType { get; set; }

        [MaxLength(100)]
        public string? vcotherLiabilities { get; set; }

        [MaxLength(100)]
        public string? vcliabilitiesTotal { get; set; }

        [MaxLength(100)]
        public string? vcloan { get; set; }

        [MaxLength(1000)]
        public string? vcotherDetails { get; set; }

        [MaxLength(100)]
        public string? vcLastAccessIP { get; set; }

        [MaxLength(100)]
        public string? vcCreatedBy { get; set; }

        public DateTime? dtCreateDate { get; set; }

        [MaxLength(100)]
        public string? vcModifiedBy { get; set; }

        public DateTime? dtModifiedDate { get; set; }

        public DateTime? dtDeletedDate { get; set; }

        [Required]
        public bool bitIsDeleted { get; set; }

        [Required]
        public bool btinsuranceFromOtherCompanines { get; set; }

        [MaxLength(200)]
        public string? vcinsuranceCompanyName { get; set; }

        [MaxLength(200)]
        public string? vcpolicyNumber { get; set; }

        [MaxLength(200)]
        public string? vcbasicSumAssured { get; set; }

        [MaxLength(200)]
        public string? vcridersOpted { get; set; }

        [MaxLength(200)]
        public string? vcmedicalOrNonMedical { get; set; }

        [MaxLength(200)]
        public string? vcannualizedPremium { get; set; }

        [MaxLength(200)]
        public string? vcaccept { get; set; }

        [MaxLength(200)]
        public string? vcnameOfLa { get; set; }

        [MaxLength(200)]
        public string? vcspecifyDetails { get; set; }
    }
}
