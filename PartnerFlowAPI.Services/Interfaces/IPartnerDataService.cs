using Newtonsoft.Json.Linq;
using PartnerFlowAPI.Models;
using PartnerFlowAPI.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace PartnerFlowAPI.Services.Interfaces
{
    public interface IPartnerDataService
    {
        Task<ValidationResultModel> ProcessPartnerDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> AgentDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> BankAccountDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> CommunicationDetailstDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> EmploymentDetailstDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> FamilyDetailstDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> FATCADetailstDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> FinancialQuestionDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> Form60QuestionDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> HealthConditionDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> HealthConditionDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> InsuranceHistoryDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> LifeStyleDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> MandateDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> MinorDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> NomineeDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> NRIDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> OtherInsuranceDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> PartialWithdrawalDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> PaymentDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> PersonalDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> ProductDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> RiderDetailsDataAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<ValidationResultModel> UploadDocumentAsync(int partnerId, Dictionary<string, object> payload, string partnerName);
        Task<string> SubmitDataAsync(int partnerId, Dictionary<string, object> payload);
    }
}
