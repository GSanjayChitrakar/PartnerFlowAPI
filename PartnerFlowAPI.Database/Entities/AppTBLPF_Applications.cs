using PartnerFlowAPI.Database.Common;
using PartnerFlowAPI.Database.Enums;


namespace PartnerFlowAPI.Database.Enums
{
    public class AppTBLPF_Applications : AuditEntity
    {
        public string vcApplicationNumber { get; set; }
        public string vcQuotationId { get; set; }
        public int intFormSeriesId { get; set; }
        public string? vcApplicationType { get; set; }
        public string? vcPartnerAppNo { get; set; }
        public string? vcParentApplicationNumber { get; set; }
        public string? vcPolicyNumber { get; set; }
        public string? vcLeadID { get; set; }
        public string? vcSourcingChannel { get; set; }
        public string? vcChannel { get; set; }
        public string? vcLAKYCType { get; set; }
        public string? vcProposerKYCType { get; set; }
        public string? vcPayorKYCType { get; set; }
        public bool? btIsLAProposerSame { get; set; }
        public bool btVerifiedFormSubmission { get; set; }
        public bool btIsMDRTCase { get; set; }
        public bool btIsHNICase { get; set; }
        public DateTime? dtVerifiedFormSubmissionOn { get; set; }
        public string? vcVerifiedFormSubmissionIP { get; set; }
        public Guid? gdSuitabilityId { get; set; }
        public ApplicationStatus intApplicationStatus { get; set; }
        public FormStage intFormStepNo { get; set; }
        public ApplicationDataStatus intDataStatus { get; set; }
        public ApplicationPaymentStatus intPaymentStatus { get; set; }
        public ApplicationMandateStatus? intMandatStatus { get; set; }

        public CompleteStatus intCompleteStatus { get; set; }
        public CdfType? intCdfType { get; set; }
        public ScrType? intScrType { get; set; }
        public string? vcLACdfLink { get; set; }
        public string? vcProposerCdfLink { get; set; }
        public string? vcPaymentLink { get; set; }
        public string? vcScheme { get; set; }
        public string? vcProductCategory { get; set; }
        public bool btPayor { get; set; }
        public bool btIsSpecialInsurance { get; set; }
        public bool btLACdfConfirm { get; set; }
        public bool btProposerCdfConfirm { get; set; }
        public bool btIsLaMinor { get; set; }
        public bool LACDFEligible { get; set; }
        public bool ProposalEligible { get; set; }
        //public bool btisfinancialeligible {  get; set; }
        public ApplicationDeDupeStatus intDeDupeStatus { get; set; }
        public ApplicationReceiptingStatus intReceiptingStatus { get; set; }
        public ApplicationImageQCStatus intImageQCStatus { get; set; }
        public ApplicationSplitPolicyStatus intSplitPolicyStatus { get; set; }
        public ApplicationDocQCStatus intDocQCStatus { get; set; }
        public ApplicationResubmissionStatus? intResubmissionStatus { get; set; }

        public bool btRejectedBySplit { get; set; }
        public DateTime? dtNBCompletedOn { get; set; }
        public string? vcNBCompletedBy { get; set; }
        public string? vcNBCompletedIP { get; set; }
        public string? vcRMID { get; set; }
        public bool? btIsSyncComplete { get; set; }
        public string? vcAgentCode { get; set; }
        public string? vcPaymentMode { get; set; }
        public string? vcRenewalMode { get; set; }
        public string? vcAgentPartnerLocation { get; set; }

        public string? vcAssignedTo { get; set; }
        public string? vcUWUserID { get; set; }
        public string? vcPolicyStatus { get; set; }
        public string? vcSTPDecision { get; set; }
        public bool btLAOTPVerified { get; set; }
        public bool btPROTPVerified { get; set; }
        public string? vcPLVCFlowID { get; set; }
        public string? vcIRDABranchLocation { get; set; }
        public bool? btBimaAsba { get; set; }
        public bool? btScrQueue { get; set; }
        public bool? btIsMandateViaLink { get; set; }
        public ApplicationJourneyType intApplicationJourneyType { get; set; }
    }
}
