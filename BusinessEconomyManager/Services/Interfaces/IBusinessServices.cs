using BusinessEconomyManager.DTOs;
using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.Services.Implementations
{
    public interface IBusinessServices
    {
        Task CalculateBalanceBusinessPeriod(Guid businessPeriodId, Guid appUserId);
        Task CloseBusinessPeriod(CloseBusinessPeriodRequestDto request, Guid appUserId);
        Task CreateBusiness(CreateBusinessRequestDto request, Guid appUserId);
        Task CreateBusinessExpenseTransaction(CreateBusinessExpenseTransactionRequestDto request, Guid appUserId);
        Task CreateBusinessPeriod(CreateBusinessPeriodRequestDto request, Guid appUserId);
        Task CreateBusinessSaleDay(CreateBusinessSaleDayRequestDto request, Guid appUserId);
        Task CreateSupplier(CreateSupplierRequestDto request, Guid appUserId);
        Task CreateSupplierCategory(CreateSupplierCategoryRequestDto request, Guid appUserId);
        Task DeleteBusinessExpenseTransaction(Guid businessExpenseTransactionId, Guid appUserId);
        Task DeleteBusinessPeriod(Guid businessPeriodId, Guid appUserId);
        Task DeleteBusinessSaleTransaction(Guid businessSaleTransactionId, Guid appUserId);
        Task DeleteSupplier(Guid supplierId, Guid appUserId);
        Task DeleteSupplierCategory(Guid supplierCategoryId, Guid appUserId);
        Task<(byte[], string)> DownloadBusinessPeriod(Guid businessPeriodId, Guid appUserId);
        Task<GetAccountBalanceResponseDto> GetAccountBalance(Guid appUserId);
        Task<List<BusinessPeriod>> GetAppUserBusinessPeriods(Guid appUserId);
        Task<List<Supplier>> GetAppUserSuppliers(Guid appUserId);
        Task<Business> GetBusiness(Guid appUserId);
        Task<BusinessExpenseTransaction> GetBusinessExpenseTransaction(Guid transactionId, Guid appUserId);
        Task<BusinessPeriod> GetBusinessPeriod(Guid businessPeriodId, Guid appUserId);
        Task<GetBusinessPeriodDetailsResponseDto> GetBusinessPeriodDetails(Guid businessPeriodId, Guid appUserId);
        Task<BusinessSaleTransaction> GetBusinessSaleTransaction(Guid transactionId, Guid appUserId);
        Task<GetBusinessStatisticsResponseDto> GetBusinessStatistics(GetBusinessStatisticsRequestDto request, Guid appUserId);
        Task<List<SupplierCategory>> GetSupplierCategories(Guid appUserId);
        Task UpdateBusinessExpenseTransaction(UpdateBusinessExpenseTransactionRequestDto request, Guid appUserId);
        Task UpdateBusinessSaleTransaction(UpdateBusinessSaleTransactionRequestDto request, Guid appUserId);
        Task UpdateSupplier(Supplier supplier, Guid appUserId);
        Task UpdateSupplierCategory(SupplierCategory request, Guid appUserId);
    }
}