using BusinessEconomyManager.DTOs;

namespace BusinessEconomyManager.Services.Implementations
{
    public interface IAppUserServices
    {
        Task<AppUserDto> GetAppUserDto(string emailAddress);
        Task<AppUserDto> Login(string emailAddress, string password);
        Task<AppUserDto> Register(RegisterRequestDto request);
        Task UpdateAppUser(UpdateAppUserRequestDto request, Guid appUserId);
    }
}