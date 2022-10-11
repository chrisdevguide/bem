using BusinessEconomyManager.Dtos;
using BusinessEconomyManager.Helpers;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Services.Interfaces;
using IcoCovid.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessEconomyManager.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly IIdentityServices _identityService;

        public IdentityController(IIdentityServices identityService)
        {
            _identityService = identityService;
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult SignIn([FromBody] SignInRequestDto request)
        {
            try
            {
                return Ok(_identityService.SignIn(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> SignUp([FromBody] SignUpRequestDto request)
        {
            try
            {
                return Ok(await _identityService.SignUp(request));
            }
            catch (Exception e)
            {
                return BadRequest(e.GetInnerExceptionMessage());
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            try
            {
                User currentUser = UserHelper.GetCurrentUser(HttpContext);
                return Ok(await _identityService.GetUser(currentUser.Id));
            }
            catch (Exception e)
            {
                return BadRequest(e.GetInnerExceptionMessage());
            }
        }
    }
}
