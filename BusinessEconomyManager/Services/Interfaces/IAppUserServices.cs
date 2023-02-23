using BusinessEconomyManager.DTOs;

namespace BusinessEconomyManager.Services.Interfaces
{
    public interface IAppUserServices
    {
        Task<AppUserDto> GetAppUserDto(string emailAddress);
        Task<AppUserDto> Login(string emailAddress, string password);
        Task<AppUserDto> Register(string emailAddress, string password);
    }
}