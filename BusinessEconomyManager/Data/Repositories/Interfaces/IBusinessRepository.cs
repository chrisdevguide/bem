using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.Data.Repositories.Implementations
{
    public interface IBusinessRepository
    {
        Task<bool> AppUserHasBusiness(Guid appUserId);
        Task<bool> BusinessExists(Guid businessId, Guid appUserId);
        Task CreateBusiness(Business business);
        Task CreateBusinessExpenseTransaction(BusinessExpenseTransaction businessExpenseTransaction);
        Task CreateBusinessPeriod(BusinessPeriod businessPeriod);
        Task CreateBusinessSaleTransaction(BusinessSaleTransaction businessSaleTransaction);
        Task CreateSupplier(Supplier supplier);
        Task DeleteBusinessSaleTransaction(Guid businessSaleTransactionId, Guid appUserId);
        Task<List<BusinessPeriod>> GetAppUserBusinessPeriods(Guid appUserId);
        Task<List<Supplier>> GetAppUserSuppliers(Guid appUserId);
        Task<Business> GetBusiness(Guid appUserId);
        Task<BusinessExpenseTransaction> GetBusinessExpenseTransaction(Guid transactionId, Guid appUserId);
        Task<BusinessPeriod> GetBusinessPeriod(Guid businessPeriodId, Guid appUserId);
        Task<BusinessSaleTransaction> GetBusinessSaleTransaction(Guid transactionId, Guid appUserId);
        Task UpdateBusinessSaleTransaction(BusinessSaleTransaction businessSaleTransaction);
    }
}