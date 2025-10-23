using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using PartnerFlowAPI.Services.Interfaces;
using System.Security.Claims;

namespace PartnerFlowAPI.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FieldsController : ControllerBase
    {
        private readonly IFieldService _fieldService;

        public FieldsController(IFieldService fieldService)
        {
            _fieldService = fieldService;
        }

        [HttpGet("FieldsByDataType")]
        [OutputCache(Duration = 120)]/*, VaryByClaims = new[] { "PartnerId" })]*/ // ✅ fixed here
        public async Task<IActionResult> GetByDataType([FromQuery] string dataType)
        {
            if (string.IsNullOrWhiteSpace(dataType))
                return BadRequest("DataType parameter is required.");

            var fields = await _fieldService.GetFieldsByDataTypeAsync(dataType);
            return Ok(fields);
        }
    }
}
