using BusinessEconomyManager.DTOs;
using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.Data.Repositories.Implementations
{
    public interface IAppUserRepository
    {
        Task AddAppUser(AppUser appUser);
        Task<bool> AppUserExists(Guid id);
        Task<bool> AppUserExists(string emailAddress);
        Task<AppUser> GetAppUser(Guid appUserId);
        Task<AppUser> GetAppUser(string emailAddress);
        Task<AppUserDto> GetAppUserDto(string emailAddress);
        Task UpdateAppUser(AppUser appUser);
    }
}