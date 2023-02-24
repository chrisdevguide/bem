using BusinessEconomyManager.Models;
using BusinessEconomyManager.Models.Exceptions;
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
                .Include(x => x.Business)
                .Include(x => x.BusinessSaleTransactions)
                .Include(x => x.BusinessExpenseTransactions)
                .ThenInclude(x => x.Supplier)
                .SingleOrDefaultAsync(x => x.Id == businessPeriodId && x.Business.AppUserId == appUserId);
        }

        public async Task DeleteBusinessPeriod(Guid businessPeriodId, Guid appUserId)
        {
            BusinessPeriod businessPeriodToDelete = await GetBusinessPeriod(businessPeriodId, appUserId);
            if (businessPeriodToDelete is null) throw new ApiException()
            {
                ErrorMessage = "Business period not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            _dataContext.BusinessSaleTransactions.RemoveRange(businessPeriodToDelete.BusinessSaleTransactions);
            _dataContext.BusinessExpenseTransactions.RemoveRange(businessPeriodToDelete.BusinessExpenseTransactions);
            _dataContext.BusinessPeriods.Remove(businessPeriodToDelete);
            await _dataContext.SaveChangesAsync();
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

        public async Task UpdateBusinessSaleTransaction(BusinessSaleTransaction businessSaleTransaction)
        {
            _dataContext.BusinessSaleTransactions.Update(businessSaleTransaction);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteBusinessSaleTransaction(Guid businessSaleTransactionId, Guid appUserId)
        {
            BusinessSaleTransaction businessSaleTransactionToDelete = await _dataContext.BusinessSaleTransactions.SingleOrDefaultAsync(x => x.Id == businessSaleTransactionId && x.BusinessPeriod.Business.AppUserId == appUserId);
            if (businessSaleTransactionToDelete is null) throw new ApiException()
            {
                ErrorMessage = "Transaction not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            _dataContext.BusinessSaleTransactions.Remove(businessSaleTransactionToDelete);
            await _dataContext.SaveChangesAsync();
        }

        public async Task CreateBusinessExpenseTransaction(BusinessExpenseTransaction businessExpenseTransaction)
        {
            _dataContext.BusinessExpenseTransactions.Add(businessExpenseTransaction);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateBusinessExpenseTransaction(BusinessExpenseTransaction BusinessExpenseTransaction)
        {
            _dataContext.BusinessExpenseTransactions.Update(BusinessExpenseTransaction);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteBusinessExpenseTransaction(Guid businessExpenseTransaction, Guid appUserId)
        {
            BusinessExpenseTransaction businessExpenseTransactionToDelete = await _dataContext.BusinessExpenseTransactions.SingleOrDefaultAsync(x => x.Id == businessExpenseTransaction && x.BusinessPeriod.Business.AppUserId == appUserId);
            if (businessExpenseTransactionToDelete is null) throw new ApiException()
            {
                ErrorMessage = "Transaction not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            _dataContext.BusinessExpenseTransactions.Remove(businessExpenseTransactionToDelete);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<BusinessSaleTransaction> GetBusinessSaleTransaction(Guid transactionId, Guid appUserId)
        {
            return await _dataContext.BusinessSaleTransactions
                .AsNoTracking()
                .Include(x => x.BusinessPeriod)
                .ThenInclude(x => x.Business)
                .SingleOrDefaultAsync(x => x.Id == transactionId && x.BusinessPeriod.Business.AppUserId == appUserId);
        }

        public async Task<BusinessExpenseTransaction> GetBusinessExpenseTransaction(Guid transactionId, Guid appUserId)
        {
            return await _dataContext.BusinessExpenseTransactions
                .AsNoTracking()
                .Include(x => x.BusinessPeriod)
                .SingleOrDefaultAsync(x => x.Id == transactionId && x.BusinessPeriod.Business.AppUserId == appUserId);
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

        public async Task<List<BusinessSaleTransaction>> GetBusinessSaleTransactions(DateTime dateFrom, DateTime dateTo, Guid appUserId)
        {
            return await _dataContext.BusinessSaleTransactions
                .Where(x => x.BusinessPeriod.Business.AppUserId == appUserId && x.Date >= dateFrom && x.Date <= dateTo)
                .ToListAsync();
        }

        public async Task<List<BusinessExpenseTransaction>> GetBusinessExpenseTransactions(DateTime dateFrom, DateTime dateTo, Guid appUserId)
        {
            return await _dataContext.BusinessExpenseTransactions
                .Where(x => x.BusinessPeriod.Business.AppUserId == appUserId && x.Date >= dateFrom && x.Date <= dateTo)
                .ToListAsync();
        }
    }
}
