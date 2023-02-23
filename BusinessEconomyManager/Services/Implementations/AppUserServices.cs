using BusinessEconomyManager.Data.Repositories.Interfaces;
using BusinessEconomyManager.DTOs;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Models.Exceptions;
using BusinessEconomyManager.Services.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace BusinessEconomyManager.Services.Implementations
{
    public class AppUserServices : IAppUserServices
    {
        private readonly IAppUserRepository _appUserRepository;
        private readonly IAuthenticationService _authenticationService;

        public AppUserServices(IAppUserRepository appUserRepository, IAuthenticationService authenticationService)
        {
            _appUserRepository = appUserRepository;
            _authenticationService = authenticationService;
        }

        public async Task<AppUserDto> Register(string emailAddress, string password)
        {
            if (await _appUserRepository.AppUserExists(emailAddress))
                throw new ApiException("User already exists.");

            using HMACSHA512 hmac = new();

            AppUser appUser = new()
            {
                EmailAddress = emailAddress.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
                PasswordSalt = hmac.Key
            };

            await _appUserRepository.AddAppUser(appUser);

            return new()
            {
                EmailAddress = emailAddress,
                Token = _authenticationService.GenerateAuthenticationToken(appUser),
                AppUserId = appUser.Id
            };
        }

        public async Task<AppUserDto> Login(string emailAddress, string password)
        {
            AppUser appUser = await _appUserRepository.GetAppUser(emailAddress);
            if (appUser == null) throw new ApiException()
            {
                ErrorMessage = "User not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            using HMACSHA512 hmac = new(appUser.PasswordSalt);

            if (!hmac.ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(appUser.PasswordHash))
                throw new ApiException()
                {
                    ErrorMessage = "Password was incorrect.",
                    StatusCode = StatusCodes.Status401Unauthorized
                };

            return new()
            {
                EmailAddress = emailAddress,
                Token = _authenticationService.GenerateAuthenticationToken(appUser),
                AppUserId = appUser.Id
            };
        }

        public async Task<AppUserDto> GetAppUserDto(string emailAddress)
        {
            return await _appUserRepository.GetAppUserDto(emailAddress) ??
                throw new ApiException()
                {
                    ErrorMessage = "User not found.",
                    StatusCode = StatusCodes.Status404NotFound
                };
        }
    }
}
