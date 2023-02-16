using BusinessEconomyManager.DTOs;

namespace BusinessEconomyManager.Services.Interfaces
{
    public interface IAppUserServices
    {
        Task<AppUserDto> GetAppUserDto(string emailAddress);
        Task<LoginResponseDto> Login(string emailAddress, string password);
        Task<RegisterResponseDto> Register(string emailAddress, string password);
    }
}