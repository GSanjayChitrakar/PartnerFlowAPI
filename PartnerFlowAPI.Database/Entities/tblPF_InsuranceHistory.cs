using System;

namespace PartnerFlowAPI.Entities
{
    public class tblPF_InsuranceHistory
    {
        public int intInsuranceHistoryId { get; set; }
        public string vcApplicationNumber { get; set; } = null!;
        public int intAssureType { get; set; }
        public string? vcTitle { get; set; }
        public string? vcFirstName { get; set; }
        public string? vcMiddleName { get; set; }
        public string? vcLastName { get; set; }
        public string? vcPolicyNumber { get; set; }
        public DateTime? dtDateOfProposal { get; set; }
        public int? intTypeOfPolicy { get; set; }
        public decimal? dcBaseSumAssured { get; set; }
        public decimal? dcPremiumPerAnnum { get; set; }
        public string? vcMonthOfInsurance { get; set; }
        public string? vcYearOfInsurance { get; set; }
        public int? intCurrentStatus { get; set; }
        public int? intAcceptanceTerms { get; set; }
        public string vcLastAccessIP { get; set; } = null!;
        public string vcCreatedBy { get; set; } = null!;
        public DateTime dtCreateDate { get; set; }
        public string? vcModifiedBy { get; set; }
        public DateTime? dtModifiedDate { get; set; }
        public DateTime? dtDeletedDate { get; set; }
    }
}

   
