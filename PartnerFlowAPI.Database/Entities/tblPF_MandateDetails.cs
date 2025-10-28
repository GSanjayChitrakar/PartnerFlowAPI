using System;

namespace PartnerFlowAPI.Entities
{
    public class tblPF_MandateDetails
    {
        public int intMandateID { get; set; }
        public string vcApplicationNumber { get; set; } = null!;
        public int? intPersonalID { get; set; }
        public int intAssureType { get; set; }
        public string? vcTransactionId { get; set; }
        public int intPaymentStatus { get; set; }
        public int? intMandateStatus { get; set; }
        public bool? btIsMandateCase { get; set; }
        public DateTime? dtMandateStartDate { get; set; }
        public DateTime? dtMandateEndDate { get; set; }
        public string? vcPaymentType { get; set; }
        public string? vcRenewalMode { get; set; }
        public decimal? dcAmount { get; set; }
        public string? vcMandateOption { get; set; }
        public string? vcMandateLink { get; set; }
        public string? vcChequeDDNumber { get; set; }
        public DateTime? vcChequeDDDate { get; set; }
        public decimal? ftChequeDDAmount { get; set; }
        public string? vcBankName { get; set; }
        public string? vcBankBranch { get; set; }
        public string? vcBankAccountType { get; set; }
        public string? vcBankAccountNumber { get; set; }
        public string? vcBankIFSCCode { get; set; }
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
        public string? vcAccountNumber { get; set; }
        public string? vcBranch { get; set; }
        public string? vcCardNumber { get; set; }
        public string? vcChequeNumber { get; set; }
        public string? vcDemandDraft { get; set; }
        public string? vcDrownOn { get; set; }
        public string? vcEInsuranceAccountNumber { get; set; }
        public string? vcIfscCode { get; set; }
        public string? vcInsuranceRepositoryName { get; set; }
        public string? vcPaymentCardNetwork { get; set; }
        public bool? btIsRecieveEInsurance { get; set; }
        public string vcLastAccessIP { get; set; } = null!;
        public string vcCreatedBy { get; set; } = null!;
        public DateTime dtCreateDate { get; set; }
        public string? vcModifiedBy { get; set; }
        public DateTime? dtModifiedDate { get; set; }
        public DateTime? dtDeletedDate { get; set; }
        public bool bitIsDeleted { get; set; }
        public string? vcEmail { get; set; }
        public string? vcDemanddraftno { get; set; }
        public DateTime? dtDemaddraftDate { get; set; }
        public string? vcAgentTitle { get; set; }
        public string? vcAgentFirstName { get; set; }
        public string? vcAgentMiddleName { get; set; }
        public string? vcAgentLastName { get; set; }
        public string? vcAgentPhoneNo { get; set; }
        public string? vcAgentEmailID { get; set; }
        public string? vcPolicyNumber { get; set; }
        public string? vcPGMandateNo { get; set; }
        public string? vcMandateMode { get; set; }
        public string? vcLifeAsiaMandateNumber { get; set; }
        public string? vcMandateStatus { get; set; }
        public string? vcReferenceNumber { get; set; }
        public string? vcPGStatus { get; set; }
        public string? vcPGReferenceNo { get; set; }
        public string? vcPGMsg { get; set; }
        public string? vcInternalBankId { get; set; }
        public string? vcInternalTransactionID { get; set; }
        public string? vcToken { get; set; }
        public string? vcFactHouse { get; set; }
    }
}
