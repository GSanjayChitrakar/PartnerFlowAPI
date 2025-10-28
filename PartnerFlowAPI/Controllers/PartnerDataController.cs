using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using PartnerFlowAPI.Services.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Text.Json;

namespace PartnerFlowAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PartnerDataController : ControllerBase
    {
        private readonly IPartnerDataService _partnerDataService;

        public PartnerDataController(IPartnerDataService partnerDataService)
        {
            _partnerDataService = partnerDataService;
        }

        [HttpPost("SubmitData")]
        public async Task<IActionResult> ProcessPartnerData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";

                var result = await _partnerDataService.ProcessPartnerDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }


        [HttpPost("SubmitAgentData")]
        public async Task<IActionResult> AgentDetailsData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.AgentDetailsDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitBankAccountData")]
        public async Task<IActionResult> BankAccountData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.BankAccountDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitCommunicationDetailsData")]
        public async Task<IActionResult> CommunicationDetailstData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.CommunicationDetailstDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitEmploymentDetailsData")]
        public async Task<IActionResult> EmploymentDetailstData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.EmploymentDetailstDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitFamilyDetailsData")]
        public async Task<IActionResult> FamilyDetailstData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.FamilyDetailstDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitFATCAData")]
        public async Task<IActionResult> FATCADetailstData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.FATCADetailstDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitFinancialQuestionsData")]
        public async Task<IActionResult> FinancialQuestionData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.FinancialQuestionDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitForm60QuestionData")]
        public async Task<IActionResult> Form60QuestionData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.Form60QuestionDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitHealthConditionData")]
        public async Task<IActionResult> HealthConditionData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.HealthConditionDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitHealthConditionDetailsData")]
        public async Task<IActionResult> HealthConditionDetailsData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.HealthConditionDetailsDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitInsuranceHistoryData")]
        public async Task<IActionResult> InsuranceHistoryData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.InsuranceHistoryDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitLifeStyleData")]
        public async Task<IActionResult> LifeStyleDetailsData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.LifeStyleDetailsDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitMandateData")]
        public async Task<IActionResult> MandateDetailsData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.MandateDetailsDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitMinorData")]
        public async Task<IActionResult> MinorDetailsData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.MinorDetailsDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitNomineeData")]
        public async Task<IActionResult> NomineeDetailsData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.NomineeDetailsDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitNRIData")]
        public async Task<IActionResult> NRIDetailsData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.NRIDetailsDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitOtherInsuranceData")]
        public async Task<IActionResult> OtherInsuranceData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.OtherInsuranceDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitPartialWithdrawlData")]
        public async Task<IActionResult> PartialWithdrawalData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.PartialWithdrawalDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitPaymentDetailsData")]
        public async Task<IActionResult> PaymentDetailsData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.PaymentDetailsDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }
        [HttpPost("SubmitPersonalDetailsData")]
        public async Task<IActionResult> PersonalDetailsData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.PersonalDetailsDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }


        [HttpPost("SubmitProductDetailsData")]
        public async Task<IActionResult> ProductDetailsData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.ProductDetailsDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }

        [HttpPost("SubmitRiderDetailsData")]
        public async Task<IActionResult> RiderDetailsData([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.RiderDetailsDataAsync(partnerId, payload, partnerName);

                if (result.InvalidFields.Any())
                {
                    return BadRequest(new
                    {
                        Status = "Validation Failed",
                        InvalidFields = result.InvalidFields
                    });
                }

                return Ok(new { Status = "Success", Message = "Data inserted successfully." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Status = "Error", Message = ex.Message });
            }
        }


        [HttpPost("SubmitAllData")]
        public async Task<IActionResult> SubmitData([FromBody] Dictionary<string, object> payload)
        {
            int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");

            var response = await _partnerDataService.SubmitDataAsync(partnerId, payload);
            return Ok(new { message = response });
        }

    }
}
