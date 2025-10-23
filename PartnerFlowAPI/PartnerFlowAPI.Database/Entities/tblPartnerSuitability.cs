using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Database.Entities
{
    public class tblPartnerSuitability
    {
        public Guid GdSuitabilityId { get; set; }
        public string VcLeadID { get; set; }
        public string VcSalutation { get; set; }
        public string VcName { get; set; }
        public string VcFirstName { get; set; }
        public string VcMiddleName { get; set; }
        public string VcLastName { get; set; }
        public string VcDOB { get; set; }
        public DateTime? DtDOB { get; set; }
        public int IntNationality { get; set; }
        public char ChGender { get; set; }
        public string VcMobile { get; set; }
        public string VcInternationalMobile { get; set; }
        public string VcEmail { get; set; }
        public string VcCountry { get; set; }
        public string VcCountryCode { get; set; }
        public bool BtIsSmoker { get; set; }
        public string VcLifeStage { get; set; }
        public string VcRiskProfile { get; set; }
        public decimal? DcAnnualIncome { get; set; }
        public int? IntExistingInsurance { get; set; }
        public decimal? DcExistingSumAssured { get; set; }
        public int? IntLifeGoals { get; set; }
        public int IntPolicyTerm { get; set; }
        public decimal? DcGoalCurrentValue { get; set; }
        public decimal? DcTimeToAchieveGoal { get; set; }
        public decimal? DcGoalFutureValue { get; set; }
        public string VcProductCategory { get; set; }
        public string VcScheme { get; set; }
        public string VcJourneyId { get; set; }
        public string VcLastAccessIP { get; set; }
        public string VcCreatedBy { get; set; }
        public DateTime DtCreateDate { get; set; }
        public string VcModifiedBy { get; set; }
        public DateTime? DtModifiedDate { get; set; }
        public DateTime? DtDeletedDate { get; set; }
        public bool BitIsDeleted { get; set; }
    }
}
