using BusinessEconomyManager.Data.Repositories.Implementations;
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
        private readonly IBusinessRepository _businessRepository;

        public AppUserServices(IAppUserRepository appUserRepository, IAuthenticationService authenticationService, IBusinessRepository businessRepository)
        {
            _appUserRepository = appUserRepository;
            _authenticationService = authenticationService;
            _businessRepository = businessRepository;
        }

        public async Task<AppUserDto> Register(RegisterRequestDto request)
        {
            if (await _appUserRepository.AppUserExists(request.EmailAddress))
                throw new ApiException("User already exists.");

            using HMACSHA512 hmac = new();

            AppUser appUser = new()
            {
                EmailAddress = request.EmailAddress.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.Password)),
                PasswordSalt = hmac.Key
            };

            await _appUserRepository.AddAppUser(appUser);

            Business business = new()
            {
                AppUserId = appUser.Id,
                Name = request.BusinessName
            };

            await _businessRepository.CreateBusiness(business);

            return new()
            {
                EmailAddress = request.EmailAddress,
                Token = _authenticationService.GenerateAuthenticationToken(appUser),
                AppUserId = appUser.Id
            };
        }

        public async Task<AppUserDto> Login(string emailAddress, string password)
        {
            AppUser appUser = await _appUserRepository.GetAppUser(emailAddress) ?? throw new ApiException()
            {
                ErrorMessage = "User not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (!CheckAppUserPassword(appUser, password))
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

        public async Task UpdateAppUser(UpdateAppUserRequestDto request, Guid appUserId)
        {
            AppUser appUser = await _appUserRepository.GetAppUser(appUserId) ?? throw new ApiException()
            {
                ErrorMessage = "User not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (!string.IsNullOrEmpty(request.EmailAddress))
            {
                appUser.EmailAddress = request.EmailAddress;
            }

            if (!string.IsNullOrEmpty(request.OldPassword))
            {
                if (!CheckAppUserPassword(appUser, request.OldPassword))
                    throw new ApiException()
                    {
                        ErrorMessage = "Password was incorrect.",
                        StatusCode = StatusCodes.Status400BadRequest
                    };
                if (string.IsNullOrEmpty(request.NewPassword) || request.NewPassword.Length < 8) throw new ApiException("New password is not valid.");

                using HMACSHA512 hmac = new();
                appUser.PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(request.NewPassword));
                appUser.PasswordSalt = hmac.Key;
            }

            await _appUserRepository.UpdateAppUser(appUser);

            if (!string.IsNullOrEmpty(request.BusinessName))
            {
                appUser.Business.Name = request.BusinessName;
                await _businessRepository.UpdateBusiness(appUser.Business);
            }
        }

        private static bool CheckAppUserPassword(AppUser appUser, string password)
        {
            using HMACSHA512 hmac = new(appUser.PasswordSalt);
            return hmac.ComputeHash(Encoding.UTF8.GetBytes(password)).SequenceEqual(appUser.PasswordHash);
        }
    }
}
