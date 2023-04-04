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
        Task CreateSupplierCategory(SupplierCategory supplierCategory);
        Task DeleteBusinessExpenseTransaction(Guid businessExpenseTransaction, Guid appUserId);
        Task DeleteBusinessPeriod(Guid businessPeriodId, Guid appUserId);
        Task DeleteBusinessSaleTransaction(Guid businessSaleTransactionId, Guid appUserId);
        Task DeleteSupplier(Supplier supplier, Guid appUserId);
        Task DeleteSupplierCategory(SupplierCategory supplierCategory);
        Task<List<BusinessPeriod>> GetAppUserBusinessPeriods(Guid appUserId);
        Task<List<Supplier>> GetAppUserSuppliers(Guid appUserId);
        Task<Business> GetBusiness(Guid appUserId);
        Task<BusinessExpenseTransaction> GetBusinessExpenseTransaction(Guid transactionId, Guid appUserId);
        Task<List<BusinessExpenseTransaction>> GetBusinessExpenseTransactions(DateTime dateFrom, DateTime dateTo, Guid appUserId);
        Task<BusinessPeriod> GetBusinessPeriod(Guid businessPeriodId, Guid appUserId);
        Task<BusinessSaleTransaction> GetBusinessSaleTransaction(Guid transactionId, Guid appUserId);
        Task<List<BusinessSaleTransaction>> GetBusinessSaleTransactions(DateTime dateFrom, DateTime dateTo, Guid appUserId);
        Task<Supplier> GetSupplier(Guid supplierId, Guid appUserId);
        Task<List<SupplierCategory>> GetSupplierCategories(Guid appUserId);
        Task<SupplierCategory> GetSupplierCategory(Guid supplierCategoryId, Guid appUserId);
        Task<bool> HasSupplierBusinessExpenseTransactions(Guid supplierId, Guid appUserId);
        Task<bool> SupplierExists(Guid supplierId, Guid appUserId);
        Task UpdateBusinessExpenseTransaction(BusinessExpenseTransaction BusinessExpenseTransaction);
        Task UpdateBusinessSaleTransaction(BusinessSaleTransaction businessSaleTransaction);
        Task UpdateSupplier(Supplier supplier, Guid appUserId);
        Task UpdateSupplierCategory(SupplierCategory supplierCategory);
        Task<bool> HasBusinessPeriodTransactions(Guid businessPeriodId, Guid appUserId);
    }
}