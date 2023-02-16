using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string GenerateAuthenticationToken(AppUser appUser);
    }
}