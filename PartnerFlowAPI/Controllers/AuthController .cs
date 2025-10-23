using Microsoft.AspNetCore.Mvc;
using PartnerFlowAPI.Models.Dtos;
using PartnerFlowAPI.Services.Interfaces;

namespace PartnerFlowAPI.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IPartnerAuthService _authService;

        public AuthController(IPartnerAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("token")]
        public async Task<IActionResult> GenerateToken([FromBody] PartnerAuthRequestDto request)
        {
            var response = await _authService.AuthenticateAsync(request);

            if (!response.Success)
                return Unauthorized(response);

            return Ok(response);
        }
    }
}
