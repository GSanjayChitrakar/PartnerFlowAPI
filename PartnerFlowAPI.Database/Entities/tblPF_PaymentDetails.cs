using System;
using System.ComponentModel.DataAnnotations;

namespace PartnerFlowAPI.Entities
{
    public class tblPF_PaymentDetails
    {
        [Key]
        public int intPaymentID { get; set; }

        [StringLength(200)]
        public string vcApplicationNumber { get; set; } = null!;

        public int? intPersonalID { get; set; }

        public int intAssureType { get; set; }

        [StringLength(100)]
        public string? vcTransactionId { get; set; }

        public int intPaymentStatus { get; set; }

        public int? intMandateStatus { get; set; }

        public bool? btIsMandateCase { get; set; } = false;

        public DateTime? dtMandateStartDate { get; set; }

        public DateTime? dtMandateEndDate { get; set; }

        [StringLength(100)]
        public string? vcPaymentType { get; set; }

        [StringLength(100)]
        public string? vcRenewalMode { get; set; }

        public decimal? dcAmount { get; set; }

        [StringLength(100)]
        public string? vcMandateOption { get; set; }

        [StringLength(300)]
        public string? vcMandateLink { get; set; }

        [StringLength(100)]
        public string? vcChequeDDNumber { get; set; }

        public DateTime? vcChequeDDDate { get; set; }

        public decimal? ftChequeDDAmount { get; set; }

        [StringLength(300)]
        public string? vcBankName { get; set; }

        [StringLength(300)]
        public string? vcBankBranch { get; set; }

        [StringLength(100)]
        public string? vcBankAccountType { get; set; }

        [StringLength(100)]
        public string? vcBankAccountNumber { get; set; }

        [StringLength(100)]
        public string? vcBankIFSCCode { get; set; }

        [StringLength(100)]
        public string? vcBankMICRCode { get; set; }

        public bool? btIsAlreadyEInsurance { get; set; }

        public bool? btOpenEInsuranceAccount { get; set; }

        public decimal? dcTotalInstalmentPremiumWithTaxes { get; set; }

        public DateTime? dtChequeDate { get; set; }

        public float? ftFixedIncomePayoutPercentage { get; set; }

        public float? ftLumpsumPayoutPercentage { get; set; }

        public int? intFirstYearPremiumMode { get; set; }

        public int? intPaymentFrequency { get; set; }

        public int? intPayoutFrequency { get; set; }

        public int? intPayoutOption { get; set; }

        public int? intPayoutTerm { get; set; }

        public int? intRenewalPremiumMode { get; set; }

        public int? intRepository { get; set; }

        [StringLength(1000)]
        public string? vcAccountNumber { get; set; }

        [StringLength(1000)]
        public string? vcBranch { get; set; }

        [StringLength(1000)]
        public string? vcCardNumber { get; set; }

        [StringLength(1000)]
        public string? vcChequeNumber { get; set; }

        [StringLength(1000)]
        public string? vcDemandDraft { get; set; }

        [StringLength(1000)]
        public string? vcDrownOn { get; set; }

        [StringLength(1000)]
        public string? vcEInsuranceAccountNumber { get; set; }

        [StringLength(1000)]
        public string? vcIfscCode { get; set; }

        [StringLength(1000)]
        public string? vcInsuranceRepositoryName { get; set; }

        [StringLength(1000)]
        public string? vcPaymentCardNetwork { get; set; }

        public bool? btIsRecieveEInsurance { get; set; }

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

        [StringLength(400)]
        public string? vcEmail { get; set; }

        [StringLength(100)]
        public string? vcDemanddraftno { get; set; }

        public DateTime? dtDemaddraftDate { get; set; }

        [StringLength(100)]
        public string? vcAgentTitle { get; set; }

        [StringLength(100)]
        public string? vcAgentFirstName { get; set; }

        [StringLength(100)]
        public string? vcAgentMiddleName { get; set; }

        [StringLength(100)]
        public string? vcAgentLastName { get; set; }

        [StringLength(40)]
        public string? vcAgentPhoneNo { get; set; }

        [StringLength(400)]
        public string? vcAgentEmailID { get; set; }

        [StringLength(100)]
        public string? vcPolicyNumber { get; set; }

        [StringLength(200)]
        public string? vcReferenceNumber { get; set; }

        [StringLength(200)]
        public string? vcPGReferenceNo { get; set; }

        [StringLength(200)]
        public string? vcInternalTransactionID { get; set; }
    }
}
