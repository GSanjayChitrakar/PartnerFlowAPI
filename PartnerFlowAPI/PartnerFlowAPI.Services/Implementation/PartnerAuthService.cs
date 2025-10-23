using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using PartnerFlowAPI.Database.Context;
using PartnerFlowAPI.Models.Dtos;
using PartnerFlowAPI.Services.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PartnerFlowAPI.Services.Implementation
{
    public class PartnerAuthService : IPartnerAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PartnerAuthService> _logger;

        public PartnerAuthService(ApplicationDbContext context, IConfiguration configuration, ILogger<PartnerAuthService> logger)
        {
            _context = context;
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<PartnerAuthResponseDto> AuthenticateAsync(PartnerAuthRequestDto request)
        {
            try
            {
                _logger.LogInformation("Authentication attempt started for Code: {Code}", request.Code);

                var partner = await _context.Partners
                    .FirstOrDefaultAsync(p => p.Code == request.Code
                                           && p.APIKey == request.APIKey
                                           && p.IsActive
                                           && !p.IsDeleted);

                if (partner == null)
                {
                    _logger.LogWarning("Authentication failed for Code: {Code} - Invalid credentials", request.Code);

                    return new PartnerAuthResponseDto
                    {
                        Success = false,
                        Message = "Invalid Code or API Key"
                    };
                }

                // Read JWT settings
                var jwtSettings = _configuration.GetSection("JwtSettings");
                var secretKey = jwtSettings["SecretKey"] ?? throw new InvalidOperationException("SecretKey is missing");
                var issuer = jwtSettings["Issuer"];
                var audience = jwtSettings["Audience"];
                var expiryMinutes = int.TryParse(jwtSettings["ExpiryMinutes"], out var minutes) ? minutes : 60;

                // Claims for JWT
                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, partner.Code),
                    new Claim("PartnerId", partner.PartnerID.ToString()),
                    new Claim("PartnerName", partner.Name),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken(
                    issuer,
                    audience,
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(expiryMinutes),
                    signingCredentials: creds);

                var response = new PartnerAuthResponseDto
                {
                    Success = true,
                    Message = "Authentication successful",
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    Expiration = token.ValidTo,
                    PartnerCode = partner.Code,
                    PartnerName = partner.Name
                };

                _logger.LogInformation("Authentication successful for Code: {Code}", request.Code);

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during authentication for Code: {Code}", request.Code);

                throw; 
               
            }
        }
    }
}
