﻿using BusinessEconomyManager.DTOs;
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
        public async Task<ActionResult> CreateBusiness([Required] CreateBusinessRequestDto createBusinessRequestDto)
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
        public async Task<ActionResult> CreateBusinessPeriod([Required] CreateBusinessPeriodRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CreateBusinessPeriod(request, appUserId);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateBusinessPeriodFromExcel([FromForm] CreateBusinessPeriodFromExcelRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CreateBusinessPeriodFromExcel(request, appUserId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<BusinessPeriod>> GetBusinessPeriod([Required] Guid businessPeriodId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetBusinessPeriod(businessPeriodId, appUserId));
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBusinessPeriod([Required] Guid businessPeriodId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.DeleteBusinessPeriod(businessPeriodId, appUserId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<GetBusinessPeriodDetailsResponseDto>> GetBusinessPeriodDetails([Required] Guid businessPeriodId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetBusinessPeriodDetails(businessPeriodId, appUserId));
        }

        [HttpGet]
        public async Task<ActionResult<List<BusinessPeriod>>> GetAppUserBusinessPeriods()
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetAppUserBusinessPeriods(appUserId));
        }

        [HttpPost]
        public async Task<ActionResult> CreateBusinessSaleDay([Required] CreateBusinessSaleDayRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CreateBusinessSaleDay(request, appUserId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBusinessSaleTransaction([Required] UpdateBusinessSaleTransactionRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.UpdateBusinessSaleTransaction(request, appUserId);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBusinessSaleTransaction([Required] Guid businessSaleTransactionId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.DeleteBusinessSaleTransaction(businessSaleTransactionId, appUserId);
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult> CreateBusinessExpenseTransaction([Required] CreateBusinessExpenseTransactionRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CreateBusinessExpenseTransaction(request, appUserId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateBusinessExpenseTransaction([Required] UpdateBusinessExpenseTransactionRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.UpdateBusinessExpenseTransaction(request, appUserId);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteBusinessExpenseTransaction([Required] Guid businessExpenseTransactionId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.DeleteBusinessExpenseTransaction(businessExpenseTransactionId, appUserId);
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
        public async Task<ActionResult> CreateSupplier([Required] CreateSupplierRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CreateSupplier(request, appUserId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSupplier([Required] Supplier supplier)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.UpdateSupplier(supplier, appUserId);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSupplier([Required] Guid supplierId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.DeleteSupplier(supplierId, appUserId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<Supplier>>> GetAppUserSuppliers()
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetAppUserSuppliers(appUserId));
        }

        [HttpGet]
        public async Task<ActionResult<GetBusinessStatisticsResponseDto>> GetBusinessStatistics([Required] DateTime dateFrom, [Required] DateTime dateTo)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            GetBusinessStatisticsRequestDto request = new()
            {
                DateFrom = dateFrom,
                DateTo = dateTo
            };
            return Ok(await _businessServices.GetBusinessStatistics(request, appUserId));
        }

        [HttpPost]
        public async Task<ActionResult> CreateSupplierCategory(CreateSupplierCategoryRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CreateSupplierCategory(request, appUserId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<SupplierCategory>>> GetSupplierCategories()
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetSupplierCategories(appUserId));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateSupplierCategory(SupplierCategory request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.UpdateSupplierCategory(request, appUserId);
            return Ok();
        }

        [HttpDelete]
        public async Task<ActionResult> DeleteSupplierCategory(Guid supplierCategoryId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.DeleteSupplierCategory(supplierCategoryId, appUserId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> CloseBusinessPeriod(CloseBusinessPeriodRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CloseBusinessPeriod(request, appUserId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> CalculateBalanceBusinessPeriod(Guid businessPeriodId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            await _businessServices.CalculateBalanceBusinessPeriod(businessPeriodId, appUserId);
            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<GetAccountBalanceResponseDto>>> GetAccountBalance()
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.GetAccountBalance(appUserId));
        }

        [HttpGet]
        public async Task<FileContentResult> DownloadBusinessPeriod(Guid businessPeriodId)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            var result = await _businessServices.DownloadBusinessPeriod(businessPeriodId, appUserId);
            return File(result.Item1, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", result.Item2);
        }

        [HttpPost]
        public async Task<ActionResult<List<BusinessSaleTransaction>>> SearchBusinessSaleTransactions([FromBody] SearchBusinessSaleTransactionsRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.SearchBusinessSaleTransactions(request, appUserId));
        }

        [HttpPost]
        public async Task<ActionResult<List<BusinessExpenseTransaction>>> SearchBusinessExpenseTransactions([FromBody] SearchBusinessExpenseTransactionsRequestDto request)
        {
            Guid appUserId = Guid.Parse(User.GetClaim(AuthenticationService.appUserIdClaimName, false));
            return Ok(await _businessServices.SearchBusinessExpenseTransactions(request, appUserId));
        }


    }
}