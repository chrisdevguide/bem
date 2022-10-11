using BusinessEconomyManager.Dtos;
using BusinessEconomyManager.Helpers;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BusinessEconomyManager.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserBusinessController : ControllerBase
    {
        private readonly IUserBusinessService _userBusinessService;

        public UserBusinessController(IUserBusinessService userBusinessService)
        {
            _userBusinessService = userBusinessService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserBusiness([FromBody] CreateUserBusinessRequestDto request)
        {
            try
            {
                User currentUser = UserHelper.GetCurrentUser(HttpContext);
                return Ok(await _userBusinessService.CreateUserBusiness(request, currentUser.Id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetCurrentUserBusinesses()
        {
            try
            {
                User currentUser = UserHelper.GetCurrentUser(HttpContext);
                return Ok(await _userBusinessService.GetCurrentUserBusinesses(currentUser.Id));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateUserBusinessPeriod(CreateUserBusinessPeriodRequestDto request)
        {
            try
            {
                return Ok(await _userBusinessService.CreateUserBusinessPeriod(request));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBusinessPeriods(Guid userBusinessId)
        {
            try
            {
                return Ok(await _userBusinessService.GetUserBusinessPeriods(userBusinessId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplier(CreateSupplierRequestDto request)
        {
            try
            {
                return Ok(await _userBusinessService.CreateSupplier(request));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateSupplier(UpdateSupplierRequestDto request)
        {
            try
            {
                return Ok(await _userBusinessService.UpdateSupplier(request));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteSupplier(Guid supplierId)
        {
            try
            {
                await _userBusinessService.DeleteSupplier(supplierId);
                return Ok();
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBusinessSuppliers(Guid userBusinessId)
        {
            try
            {
                return Ok(await _userBusinessService.GetUserBusinessSuppliers(userBusinessId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateServiceSuppliedType(CreateServiceSuppliedTypeRequestDto request)
        {
            try
            {
                return Ok(await _userBusinessService.CreateServiceSuppliedType(request));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetUserBusinessServiceSuppliedTypes(Guid userBusinessId)
        {
            try
            {
                return Ok(await _userBusinessService.GetUserBusinessServiceSuppliedTypes(userBusinessId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateSupplierOperation(CreateSupplierOperationRequestDto request)
        {
            try
            {
                return Ok(await _userBusinessService.CreateSupplierOperation(request));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetSupplierOperations(Guid supplierId)
        {
            try
            {
                return Ok(await _userBusinessService.GetSupplierOperations(supplierId));
            }
            catch (Exception e)
            {

                return BadRequest(e.Message);
            }
        }

    }
}
