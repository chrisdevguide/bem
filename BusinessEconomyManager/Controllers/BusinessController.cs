using BusinessEconomyManager.DTOs;
using BusinessEconomyManager.Extensions;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Services.Implementations;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Controllers
{
    public class BusinessController : BaseController
    {
        private readonly IBusinessServices _businessServices;

        public BusinessController(IBusinessServices businessServices)
        {
            _businessServices = businessServices;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBusiness(CreateBusinessRequestDto createBusinessRequestDto)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CreateBusiness(createBusinessRequestDto, appUserId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<Business>> GetAppUserBusiness()
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetBusiness(appUserId));
        }

        [HttpPost]
        public async Task<ActionResult> CreateBusinessPeriod(CreateBusinessPeriodRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CreateBusinessPeriod(request, appUserId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<BusinessPeriod>> GetBusinessPeriod(Guid businessPeriodId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetBusinessPeriod(businessPeriodId, appUserId));
        }

        [HttpGet]
        public async Task<ActionResult<List<BusinessPeriod>>> GetAppUserBusinessPeriods()
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetAppUserBusinessPeriods(appUserId));
        }

        [HttpPost]
        public async Task<ActionResult> CreateBusinessSaleTransaction(CreateBusinessSaleTransactionRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CreateBusinessSaleTransaction(request, appUserId);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateBusinessExpenseTransaction(CreateBusinessExpenseTransactionRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CreateBusinessExpenseTransaction(request, appUserId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<BusinessSaleTransaction>> GetBusinessSaleTransaction([Required] Guid transactionId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetBusinessSaleTransaction(transactionId, appUserId));
        }

        [HttpGet]
        public async Task<ActionResult<BusinessExpenseTransaction>> GetBusinessExpenseTransaction([Required] Guid transactionId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetBusinessExpenseTransaction(transactionId, appUserId));
        }

        [HttpPost]
        public async Task<ActionResult> CreateSupplier(CreateSupplierRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CreateSupplier(request, appUserId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetAppUserSuppliers()
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetAppUserSuppliers(appUserId));
        }
    }
}
