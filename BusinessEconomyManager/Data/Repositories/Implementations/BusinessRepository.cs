using BusinessEconomyManager.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessEconomyManager.Data.Repositories.Implementations
{
    public class BusinessRepository : IBusinessRepository
    {
        private readonly DataContext _dataContext;

        public BusinessRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateBusiness(Business business)
        {
            _dataContext.Businesses.Add(business);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<Business> GetBusiness(Guid appUserId)
        {
            return await _dataContext.Businesses.Where(x => x.AppUserId == appUserId).SingleOrDefaultAsync();
        }

        public async Task<bool> BusinessExists(Guid businessId, Guid appUserId)
        {
            return await _dataContext.Businesses.AnyAsync(x => x.Id == businessId && x.AppUserId == appUserId);
        }

        public async Task CreateBusinessPeriod(BusinessPeriod businessPeriod)
        {
            _dataContext.BusinessPeriods.Add(businessPeriod);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<BusinessPeriod> GetBusinessPeriod(Guid businessPeriodId, Guid appUserId)
        {
            return await _dataContext.BusinessPeriods
                .Include(x => x.BusinessSaleTransactions)
                .Include(x => x.BusinessExpenseTransactions)
                .ThenInclude(x => x.Supplier)
                .SingleOrDefaultAsync(x => x.Id == businessPeriodId && x.Business.AppUserId == appUserId);
        }

        public async Task<List<BusinessPeriod>> GetAppUserBusinessPeriods(Guid appUserId)
        {
            return await _dataContext.BusinessPeriods.Where(x => x.Business.AppUserId == appUserId).ToListAsync();
        }

        public async Task<bool> AppUserHasBusiness(Guid appUserId)
        {
            return await _dataContext.Businesses.AnyAsync(x => x.AppUserId == appUserId);
        }

        public async Task CreateBusinessSaleTransaction(BusinessSaleTransaction businessSaleTransaction)
        {
            _dataContext.BusinessSaleTransactions.Add(businessSaleTransaction);
            await _dataContext.SaveChangesAsync();
        }

        public async Task CreateBusinessExpenseTransaction(BusinessExpenseTransaction businessExpenseTransaction)
        {
            _dataContext.BusinessExpenseTransactions.Add(businessExpenseTransaction);
            await _dataContext.SaveChangesAsync();
        }
        public async Task<BusinessSaleTransaction> GetBusinessSaleTransaction(Guid transactionId, Guid appUserId)
        {
            return await _dataContext.BusinessSaleTransactions.SingleOrDefaultAsync(x => x.Id == transactionId && x.BusinessPeriod.Business.AppUserId == appUserId);
        }
        public async Task<BusinessExpenseTransaction> GetBusinessExpenseTransaction(Guid transactionId, Guid appUserId)
        {
            return await _dataContext.BusinessExpenseTransactions.SingleOrDefaultAsync(x => x.Id == transactionId && x.BusinessPeriod.Business.AppUserId == appUserId);
        }

        public async Task CreateSupplier(Supplier supplier)
        {
            _dataContext.Suppliers.Add(supplier);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Supplier>> GetAppUserSuppliers(Guid appUserId)
        {
            return await _dataContext.Suppliers.Where(x => x.Business.AppUserId == appUserId).ToListAsync();
        }

    }
}
