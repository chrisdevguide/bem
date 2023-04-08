using BusinessEconomyManager.DTOs;
using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.Data.Repositories.Implementations
{
    public interface IBusinessRepository
    {
        Task<bool> AppUserHasBusiness(Guid appUserId);
        Task<bool> BusinessExists(Guid businessId, Guid appUserId);
        Task CreateBusiness(Business business);
        Task CreateBusinessExpenseTransaction(BusinessExpenseTransaction businessExpenseTransaction);
        Task CreateBusinessExpenseTransactions(List<BusinessExpenseTransaction> businessExpenseTransactions);
        Task CreateBusinessPeriod(BusinessPeriod businessPeriod);
        Task CreateBusinessSaleTransaction(BusinessSaleTransaction businessSaleTransaction);
        Task CreateBusinessSaleTransactions(List<BusinessSaleTransaction> businessSaleTransactions);
        Task CreateSupplier(Supplier supplier);
        Task CreateSupplierCategory(SupplierCategory supplierCategory);
        Task DeleteBusinessExpenseTransaction(BusinessExpenseTransaction businessExpenseTransaction);
        Task DeleteBusinessPeriod(Guid businessPeriodId, Guid appUserId);
        Task DeleteBusinessSaleTransaction(BusinessSaleTransaction businessSaleTransaction);
        Task DeleteSupplier(Supplier supplier, Guid appUserId);
        Task DeleteSupplierCategory(SupplierCategory supplierCategory);
        Task<List<BusinessPeriod>> GetAppUserBusinessPeriods(Guid appUserId);
        Task<List<Supplier>> GetAppUserSuppliers(Guid appUserId);
        Task<Business> GetBusiness(Guid appUserId);
        Task<BusinessExpenseTransaction> GetBusinessExpenseTransaction(Guid transactionId, Guid appUserId);
        Task<List<BusinessExpenseTransaction>> GetBusinessExpenseTransactions(DateTime dateFrom, DateTime dateTo, Guid appUserId);
        Task<BusinessPeriod> GetBusinessPeriod(Guid businessPeriodId, Guid appUserId);
        Task<List<BusinessPeriod>> GetBusinessPeriods(DateTime startDate, Guid appUserId);
        Task<BusinessSaleTransaction> GetBusinessSaleTransaction(Guid transactionId, Guid appUserId);
        Task<List<BusinessSaleTransaction>> GetBusinessSaleTransactions(DateTime dateFrom, DateTime dateTo, Guid appUserId);
        Task<BusinessPeriod> GetLastClosedBusinessPeriod(Guid appUserId);
        Task<Supplier> GetSupplier(Guid supplierId, Guid appUserId);
        Task<List<Supplier>> GetSupplierByNames(List<string> names, Guid appUserId);
        Task<List<SupplierCategory>> GetSupplierCategories(Guid appUserId);
        Task<SupplierCategory> GetSupplierCategory(Guid supplierCategoryId, Guid appUserId);
        Task<bool> HasBusinessPeriodTransactions(Guid businessPeriodId, Guid appUserId);
        Task<bool> HasSupplierBusinessExpenseTransactions(Guid supplierId, Guid appUserId);
        Task<bool> IsBusinessPeriodClosed(Guid businessPeriodId, Guid appUserId);
        Task<List<BusinessExpenseTransaction>> SearchBusinessExpenseTransactions(SearchBusinessExpenseTransactionsRequestDto request, Guid appUserId);
        Task<List<BusinessSaleTransaction>> SearchBusinessSaleTransactions(SearchBusinessSaleTransactionsRequestDto request, Guid appUserId);
        Task<bool> SupplierExists(Guid supplierId, Guid appUserId);
        Task UpdateBusinessExpenseTransaction(BusinessExpenseTransaction BusinessExpenseTransaction);
        Task UpdateBusinessPeriod(BusinessPeriod businessPeriod);
        Task UpdateBusinessSaleTransaction(BusinessSaleTransaction businessSaleTransaction);
        Task UpdateSupplier(Supplier supplier, Guid appUserId);
        Task UpdateSupplierCategory(SupplierCategory supplierCategory);
    }
}