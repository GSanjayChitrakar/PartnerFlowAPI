using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Entities
{
    [Table("tblPartnerData")]
    public class tblPartnerData
    {
        [Key]
        [Column("intPartnerID")]
        public int PartnerID { get; set; }

        [MaxLength(200)]
        public string? GUID { get; set; }

        [MaxLength(200)]
        public string? ApplicationNumber { get; set; }

        [MaxLength(100)]
        public string? CustId { get; set; }

        [MaxLength(200)]
        public string? AdvisorCode { get; set; }

        [MaxLength(200)]
        public string? Source { get; set; }

        [MaxLength(200)]
        public string? SourceKey { get; set; }

        public bool IsLAPrposerSame { get; set; }

        [MaxLength(200)]
        public string? SalesDataReqd { get; set; }

        [MaxLength(200)]
        public string? DependentFlag { get; set; }

        public DateTime? ProposerDOB { get; set; }

        [MaxLength(200)] public string? ProposerMobileNumber { get; set; }
        [MaxLength(200)] public string? ProposerPanNumber { get; set; }
        [MaxLength(200)] public string? ProposerEmail { get; set; }
        [MaxLength(200)] public string? ProposerGender { get; set; }
        [MaxLength(200)] public string? ProposerFirstName { get; set; }
        [MaxLength(200)] public string? ProposerMiddleName { get; set; }
        [MaxLength(200)] public string? ProposerLastName { get; set; }
        [MaxLength(200)] public string? ProposerCKYCNumber { get; set; }
        [MaxLength(200)] public string? ProposerAadhaarNumber { get; set; }
        [MaxLength(200)] public string? ProposerBankAccountNumber { get; set; }
        [MaxLength(100)] public string? ProposerIFSC { get; set; }
        [MaxLength(200)] public string? ProposerPincode { get; set; }
        [MaxLength(200)] public string? ProposerLandMark { get; set; }
        [MaxLength(200)] public string? ProposerState { get; set; }
        [MaxLength(200)] public string? ProposerAddressLine1 { get; set; }
        [MaxLength(200)] public string? ProposerAddressLine2 { get; set; }
        [MaxLength(200)] public string? ProposerAddressLine3 { get; set; }
        [MaxLength(200)] public string? ProposerCity { get; set; }
        [MaxLength(200)] public string? ProposerCountry { get; set; }

        public decimal? ProposerAnnualIncome { get; set; }
        public int? ProposerMaritalStatus { get; set; }
        public int? LifeAssureMaritalStatus { get; set; }

        public DateTime? LifeAssureDOB { get; set; }

        [MaxLength(200)] public string? LifeAssureMobileNumber { get; set; }
        [MaxLength(200)] public string? LifeAssurePanNumber { get; set; }
        [MaxLength(200)] public string? LifeAssureEmail { get; set; }
        [MaxLength(200)] public string? LifeAssureGender { get; set; }
        [MaxLength(200)] public string? LifeAssureFirstName { get; set; }
        [MaxLength(200)] public string? LifeAssureMiddleName { get; set; }
        [MaxLength(200)] public string? LifeAssureLastName { get; set; }
        [MaxLength(200)] public string? LifeAssureCKYCNumber { get; set; }
        [MaxLength(200)] public string? LifeAssureAadhaarNumber { get; set; }
        [MaxLength(200)] public string? LifeAssureBankAccountNumber { get; set; }
        [MaxLength(100)] public string? LifeAssureIFSC { get; set; }
        [MaxLength(200)] public string? LifeAssurePincode { get; set; }
        [MaxLength(200)] public string? LifeAssureLandMark { get; set; }
        [MaxLength(200)] public string? LifeAssureState { get; set; }
        [MaxLength(200)] public string? LifeAssureAddressLine1 { get; set; }
        [MaxLength(200)] public string? LifeAssureAddressLine2 { get; set; }
        [MaxLength(200)] public string? LifeAssureAddressLine3 { get; set; }
        [MaxLength(200)] public string? LifeAssureCity { get; set; }
        [MaxLength(200)] public string? LifeAssureCountry { get; set; }
        public decimal? LifeAssureAnnualIncome { get; set; }

        [MaxLength(200)] public string? ChannelType { get; set; }
        [MaxLength(200)] public string? CustomerBankAccount { get; set; }
        [MaxLength(200)] public string? PartnerUniqueId1 { get; set; }
        [MaxLength(200)] public string? BankName { get; set; }
        [MaxLength(200)] public string? NeedRiskProfile { get; set; }
        [MaxLength(200)] public string? CSRLimCode { get; set; }
        [MaxLength(200)] public string? CafosCode { get; set; }
        [MaxLength(200)] public string? OppId { get; set; }
        [MaxLength(200)] public string? IFSCCode { get; set; }
        [MaxLength(200)] public string? SPCode { get; set; }
        [MaxLength(200)] public string? BankBranch { get; set; }
        [MaxLength(200)] public string? SubChannel { get; set; }

        [MaxLength(200)] public string? LifeAssurePermanentAddressPincode { get; set; }
        [MaxLength(200)] public string? LifeAssurePermanentAddressLandMark { get; set; }
        [MaxLength(200)] public string? LifeAssurePermanentAddressState { get; set; }
        [MaxLength(200)] public string? LifeAssurePermanentAddressAddressLine1 { get; set; }
        [MaxLength(200)] public string? LifeAssurePermanentAddressAddressLine2 { get; set; }
        [MaxLength(200)] public string? LifeAssurePermanentAddressAddressLine3 { get; set; }
        [MaxLength(200)] public string? LifeAssurePermanentAddressCity { get; set; }
        [MaxLength(200)] public string? LifeAssurePermanentCountry { get; set; }

        [MaxLength(200)] public string? ProposerPermanentPincode { get; set; }
        [MaxLength(200)] public string? ProposerPermanentLandMark { get; set; }
        [MaxLength(200)] public string? ProposerPermanentState { get; set; }
        [MaxLength(200)] public string? ProposerPermanentAddressLine1 { get; set; }
        [MaxLength(200)] public string? ProposerPermanentAddressLine2 { get; set; }
        [MaxLength(200)] public string? ProposerPermanentAddressLine3 { get; set; }
        [MaxLength(200)] public string? ProposerPermanentCity { get; set; }
        [MaxLength(200)] public string? ProposerPermanentCountry { get; set; }

        [MaxLength(100)] public string? RMID { get; set; }
        [MaxLength(200)] public string? AgentCode { get; set; }
        [MaxLength(200)] public string? LastAccessIP { get; set; }
        [MaxLength(200)] public string? CreatedBy { get; set; }
        public DateTime? CreateDate { get; set; }
        [MaxLength(200)] public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public bool IsDeleted { get; set; }

        [MaxLength(100)] public string? AlternateNumber { get; set; }
        [MaxLength(100)] public string? SPName { get; set; }
        [MaxLength(100)] public string? SPLocation { get; set; }

        [MaxLength(20)] public string? ProposerTitle { get; set; }
        [MaxLength(100)] public string? ProposerOccupation { get; set; }
        [MaxLength(200)] public string? ProposerEducationQualification { get; set; }
        [MaxLength(20)] public string? LifeAssureTitle { get; set; }
        [MaxLength(100)] public string? LifeAssureOccupation { get; set; }
        [MaxLength(200)] public string? LifeAssureEducationQualification { get; set; }

        [MaxLength(200)] public string? QuotationID { get; set; }
    }
}
