using BusinessEconomyManager.Data.Repositories.Implementations;
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
