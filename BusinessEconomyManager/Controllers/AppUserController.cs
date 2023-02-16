using BusinessEconomyManager.DTOs;
using BusinessEconomyManager.Extensions;
using BusinessEconomyManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Controllers
{

    public class AppUserController : BaseController
    {
        private readonly IAppUserServices _appUserServices;

        public AppUserController(IAppUserServices appUserServices)
        {
            _appUserServices = appUserServices;
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<RegisterResponseDto>> Register([Required][EmailAddress] string emailAddress, [Required][MinLength(8)] string password)
        {
            return Ok(await _appUserServices.Register(emailAddress, password));
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<LoginResponseDto>> Login([Required][EmailAddress] string emailAddress, [Required][MinLength(8)] string password)
        {
            return Ok(await _appUserServices.Login(emailAddress, password));
        }

        [HttpGet]
        public async Task<ActionResult<AppUserDto>> GetAppUserDto()
        {
            string emailAddress = User.GetEmailClaim(false);
            return Ok(await _appUserServices.GetAppUserDto(emailAddress));
        }

    }
}
