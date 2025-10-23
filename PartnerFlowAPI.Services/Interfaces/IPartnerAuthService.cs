using PartnerFlowAPI.Models.Dtos;

namespace PartnerFlowAPI.Services.Interfaces
{
    public interface IPartnerAuthService
    {
        Task<PartnerAuthResponseDto> AuthenticateAsync(PartnerAuthRequestDto request);
    }
}
