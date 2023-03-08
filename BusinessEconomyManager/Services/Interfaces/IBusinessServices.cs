using BusinessEconomyManager.DTOs;
using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.Services.Implementations
{
    public interface IBusinessServices
    {
        Task CreateBusiness(CreateBusinessRequestDto request, Guid appUserId);
        Task CreateBusinessExpenseTransaction(CreateBusinessExpenseTransactionRequestDto request, Guid appUserId);
        Task CreateBusinessPeriod(CreateBusinessPeriodRequestDto request, Guid appUserId);
        Task CreateBusinessSaleTransaction(CreateBusinessSaleTransactionRequestDto request, Guid appUserId);
        Task CreateSupplier(CreateSupplierRequestDto request, Guid appUserId);
        Task DeleteBusinessExpenseTransaction(Guid businessSaleTransactionId, Guid appUserId);
        Task DeleteBusinessPeriod(Guid businessPeriodId, Guid appUserId);
        Task DeleteBusinessSaleTransaction(Guid businessExpenseTransactionId, Guid appUserId);
        Task DeleteSupplier(Guid supplierId, Guid appUserId);
        Task<List<BusinessPeriod>> GetAppUserBusinessPeriods(Guid appUserId);
        Task<List<Supplier>> GetAppUserSuppliers(Guid appUserId);
        Task<Business> GetBusiness(Guid appUserId);
        Task<BusinessExpenseTransaction> GetBusinessExpenseTransaction(Guid transactionId, Guid appUserId);
        Task<BusinessPeriod> GetBusinessPeriod(Guid businessPeriodId, Guid appUserId);
        Task<GetBusinessPeriodDetailsResponseDto> GetBusinessPeriodDetails(Guid businessPeriodId, Guid appUserId);
        Task<BusinessSaleTransaction> GetBusinessSaleTransaction(Guid transactionId, Guid appUserId);
        Task<GetBusinessStatisticsResponseDto> GetBusinessStatistics(GetBusinessStatisticsRequestDto request, Guid appUserId);
        Task UpdateBusinessExpenseTransaction(UpdateBusinessExpenseTransactionRequestDto request, Guid appUserId);
        Task UpdateBusinessSaleTransaction(UpdateBusinessSaleTransactionRequestDto request, Guid appUserId);
        Task UpdateSupplier(Supplier supplier, Guid appUserId);
    }
}