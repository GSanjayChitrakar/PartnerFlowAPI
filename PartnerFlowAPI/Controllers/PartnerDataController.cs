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

        [HttpPost("Basic")]
        public async Task<IActionResult> ProcessPartner([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("Agent")]
        public async Task<IActionResult> AgentDetails([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("BankAccount")]
        public async Task<IActionResult> BankAccount([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("Communication")]
        public async Task<IActionResult> CommunicationDetailst([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("Employment")]
        public async Task<IActionResult> EmploymentDetailst([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("Family")]
        public async Task<IActionResult> FamilyDetailst([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("FATCA")]
        public async Task<IActionResult> FATCADetailst([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("FinancialQuestions")]
        public async Task<IActionResult> FinancialQuestion([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("Form60Question")]
        public async Task<IActionResult> Form60Question([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("HealthCondition")]
        public async Task<IActionResult> HealthCondition([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("HealthCondition")]
        public async Task<IActionResult> HealthConditionDetails([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("InsuranceHistory")]
        public async Task<IActionResult> InsuranceHistory([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("LifeStyle")]
        public async Task<IActionResult> LifeStyleDetails([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("Mandate")]
        public async Task<IActionResult> MandateDetails([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("Minor")]
        public async Task<IActionResult> MinorDetails([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("Nominee")]
        public async Task<IActionResult> NomineeDetails([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("NRI")]
        public async Task<IActionResult> NRIDetails([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("OtherInsurance")]
        public async Task<IActionResult> OtherInsurance([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("PartialWithdrawl")]
        public async Task<IActionResult> PartialWithdrawal([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("Payment")]
        public async Task<IActionResult> PaymentDetails([FromBody] Dictionary<string, object> payload)
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
        [HttpPost("Personal")]
        public async Task<IActionResult> PersonalDetails([FromBody] Dictionary<string, object> payload)
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


        [HttpPost("Product")]
        public async Task<IActionResult> ProductDetails([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("Rider")]
        public async Task<IActionResult> RiderDetails([FromBody] Dictionary<string, object> payload)
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

        [HttpPost("UploadDocument")]
        public async Task<IActionResult> UploadDocument([FromBody] Dictionary<string, object> payload)
        {
            try
            {

                int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");
                string partnerName = User.FindFirst("PartnerName")?.Value ?? "system";
                var result = await _partnerDataService.UploadDocumentAsync(partnerId, payload, partnerName);

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

        [HttpPost("SubmitAll")]
        public async Task<IActionResult> Submit([FromBody] Dictionary<string, object> payload)
        {
            int partnerId = Convert.ToInt32(User.FindFirst("PartnerId")?.Value ?? "0");

            var response = await _partnerDataService.SubmitDataAsync(partnerId, payload);
            return Ok(new { message = response });
        }

    }
}
