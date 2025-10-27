using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Entities
{
    public class tblPF_ProductDetails
    {
        [Key]
        public int intProductDetailId { get; set; }

        [Required, MaxLength(200)]
        public string vcApplicationNumber { get; set; }

        [MaxLength(100)]
        public string? vcProductCode { get; set; }

        [MaxLength(100)]
        public string? vcRiderProductId { get; set; }

        [MaxLength(600)]
        public string? vcFGProductCode { get; set; }

        [MaxLength(100)]
        public string? vcProductType { get; set; }

        [MaxLength(510)]
        public string? vcProductName { get; set; }

        [MaxLength(1000)]
        public string? vcOptionWithinProductRider { get; set; }

        [MaxLength(1600)]
        public string? vcBIPdfPath { get; set; }

        public decimal? dcModalPremium { get; set; }
        public decimal? dcSumAssured { get; set; }
        public decimal? dcTotalPremium { get; set; }
        public decimal? dcAnnualizedPremium { get; set; }
        public decimal? dcTax { get; set; }
        public decimal? dcTax1 { get; set; }

        public int intPT { get; set; }
        public int intPPT { get; set; }

        [MaxLength(200)]
        public string? vcMode { get; set; }

        [MaxLength(100)]
        public string? vcSourceChannel { get; set; }

        [MaxLength(100)]
        public string? vcGroupStaff { get; set; }

        [MaxLength(510)]
        public string? vcOptions { get; set; }

        public bool? btPensionProduct { get; set; }
        public double? ftPercentageToRecieve { get; set; }

        [MaxLength(510)]
        public string? vcAnnuityThrough { get; set; }

        [MaxLength(510)]
        public string? vcPensionFrequency { get; set; }

        public bool? btWishToSurrender { get; set; }

        [MaxLength(510)]
        public string? vcSurrenderAnnuityThrough { get; set; }

        [MaxLength(510)]
        public string? vcPensionSurrenderFrequency { get; set; }

        public bool? btIsTraditionalPlan { get; set; }
        public DateTime? dtTraditionalDesireDate { get; set; }
        public bool? btIsUnitLinkedIndurance { get; set; }

        public double? ftFutureSecurePercentage { get; set; }
        public double? ftFutureIncomePercentage { get; set; }
        public double? ftFutureMidcapPercentage { get; set; }
        public double? ftFutureBalancePercentage { get; set; }
        public double? ftFutureApexPercentage { get; set; }
        public double? ftFutureOpportunityPercentage { get; set; }
        public double? ftFutureMaximisePercentage { get; set; }

        public int? intPayoutTerm { get; set; }
        public int? intPremiumPayingTerm { get; set; }
        public decimal? dcInstallmentPremiumWithTaxes { get; set; }
        public int? intPolicyTerm { get; set; }
        public decimal? dcMonthlyIncome { get; set; }

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

        public int? intLifeGoal { get; set; }

        [MaxLength(100)]
        public string? vcNoOfYears { get; set; }

        [MaxLength(100)]
        public string? vcAssignmentType { get; set; }

        [MaxLength(100)]
        public string? vcUIN { get; set; }

        [MaxLength(100)]
        public string? vcPayoutFrequency { get; set; }

        [MaxLength(100)]
        public string? vcProductCategory { get; set; }

        public double? ftLumpsumPercentage { get; set; }

        [MaxLength(500)]
        public string? vcPayOutOptions { get; set; }

        [MaxLength(100)]
        public string? VcAccidentalDeathSumAssured { get; set; }

        [MaxLength(100)]
        public string? VcIncomeOption { get; set; }

        [MaxLength(100)]
        public string? vcDeathBenefit { get; set; }

        public double? ftIncomeSpackFund { get; set; }
        public double? ftIncomePlusFund { get; set; }
        public double? ftMultiCapEquityFund { get; set; }

        [MaxLength(1000)]
        public string? vcInBuildRider { get; set; }

        [MaxLength(400)]
        public string? vcFundStrategy { get; set; }

        public decimal? dcBaseModalPremium { get; set; }
        public decimal? dcBaseModalPremiumWGst { get; set; }
        public decimal? dcRiderModalPremium { get; set; }
        public decimal? dcRiderModalPremiumWGst { get; set; }

        [MaxLength(100)]
        public string? vcLumpsumMaturityBenefit { get; set; }

        [MaxLength(100)]
        public string? vcDeathBenifitPayout { get; set; }

        public decimal? dcDeathBenefitAmount { get; set; }
        public decimal? dcEMRMortality { get; set; }
        public decimal? dcRateAdjustment { get; set; }
        public decimal? dcInstPrem { get; set; }
        public decimal? dcZlinstPrem { get; set; }

        public int? intOPTCREATE { get; set; }
        public int? intStatisticalCodeCreateStatus { get; set; }
        public int? intSCPCREATEStatus { get; set; }
        public int? intProposalAdditionalDetailsCreateStatus { get; set; }
        public int? intCWDCreateStatus { get; set; }

        public double? ftIncomeSparkFund { get; set; }

        [MaxLength(10)]
        public string? vcEMRLoading { get; set; }
    }
}
