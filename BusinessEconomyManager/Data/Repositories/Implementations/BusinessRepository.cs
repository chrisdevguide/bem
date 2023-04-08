using BusinessEconomyManager.DTOs;
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
            return await _dataContext.BusinessPeriods
                .Where(x => x.Business.AppUserId == appUserId)
                .OrderByDescending(x => x.DateFrom)
                .ToListAsync();
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

        public async Task CreateBusinessSaleTransactions(List<BusinessSaleTransaction> businessSaleTransactions)
        {
            _dataContext.BusinessSaleTransactions.AddRange(businessSaleTransactions);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateBusinessSaleTransaction(BusinessSaleTransaction businessSaleTransaction)
        {
            _dataContext.BusinessSaleTransactions.Update(businessSaleTransaction);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteBusinessSaleTransaction(BusinessSaleTransaction businessSaleTransaction)
        {
            _dataContext.BusinessSaleTransactions.Remove(businessSaleTransaction);
            await _dataContext.SaveChangesAsync();
        }

        public async Task CreateBusinessExpenseTransaction(BusinessExpenseTransaction businessExpenseTransaction)
        {
            _dataContext.BusinessExpenseTransactions.Add(businessExpenseTransaction);
            await _dataContext.SaveChangesAsync();
        }

        public async Task CreateBusinessExpenseTransactions(List<BusinessExpenseTransaction> businessExpenseTransactions)
        {
            _dataContext.BusinessExpenseTransactions.AddRange(businessExpenseTransactions);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateBusinessExpenseTransaction(BusinessExpenseTransaction BusinessExpenseTransaction)
        {
            _dataContext.BusinessExpenseTransactions.Update(BusinessExpenseTransaction);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteBusinessExpenseTransaction(BusinessExpenseTransaction businessExpenseTransaction)
        {
            _dataContext.BusinessExpenseTransactions.Remove(businessExpenseTransaction);
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

        public async Task<List<Supplier>> GetSupplierByNames(List<string> names, Guid appUserId)
        {
            return await _dataContext.Suppliers
                .Where(x => x.Business.AppUserId == appUserId && names.Any(y => y == x.Name))
                .ToListAsync();
        }

        public async Task CreateSupplier(Supplier supplier)
        {
            _dataContext.Suppliers.Add(supplier);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateSupplier(Supplier supplier, Guid appUserId)
        {
            _dataContext.ChangeTracker.Clear();
            _dataContext.Suppliers.Update(supplier);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteSupplier(Supplier supplier, Guid appUserId)
        {
            _dataContext.Suppliers.Remove(supplier);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Supplier>> GetAppUserSuppliers(Guid appUserId)
        {
            return await _dataContext.Suppliers.Where(x => x.Business.AppUserId == appUserId).ToListAsync();
        }

        public async Task<bool> SupplierExists(Guid supplierId, Guid appUserId)
        {
            return await _dataContext.Suppliers.AnyAsync(x => x.Id == supplierId && x.Business.AppUserId == appUserId);
        }

        public async Task<Supplier> GetSupplier(Guid supplierId, Guid appUserId)
        {
            return await _dataContext.Suppliers.SingleOrDefaultAsync(x => x.Id == supplierId && x.Business.AppUserId == appUserId);
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

        public async Task<bool> HasSupplierBusinessExpenseTransactions(Guid supplierId, Guid appUserId)
        {
            return await _dataContext.BusinessExpenseTransactions
                .AnyAsync(x => x.BusinessPeriod.Business.AppUserId == appUserId && x.SupplierId == supplierId);
        }

        public async Task CreateSupplierCategory(SupplierCategory supplierCategory)
        {
            _dataContext.SupplierCategories.Add(supplierCategory);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<SupplierCategory> GetSupplierCategory(Guid supplierCategoryId, Guid appUserId)
        {
            return await _dataContext.SupplierCategories.SingleOrDefaultAsync(x => x.Id == supplierCategoryId && x.Business.AppUserId == appUserId);
        }

        public async Task<List<SupplierCategory>> GetSupplierCategories(Guid appUserId)
        {
            return await _dataContext.SupplierCategories.Where(x => x.Business.AppUserId == appUserId).ToListAsync();
        }

        public async Task UpdateSupplierCategory(SupplierCategory supplierCategory)
        {
            _dataContext.SupplierCategories.Update(supplierCategory);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteSupplierCategory(SupplierCategory supplierCategory)
        {
            _dataContext.SupplierCategories.Remove(supplierCategory);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> HasBusinessPeriodTransactions(Guid businessPeriodId, Guid appUserId)
        {
            BusinessPeriod businessPeriod = await GetBusinessPeriod(businessPeriodId, appUserId);
            return businessPeriod.BusinessExpenseTransactions.Any() || businessPeriod.BusinessSaleTransactions.Any();
        }

        public async Task UpdateBusinessPeriod(BusinessPeriod businessPeriod)
        {
            _dataContext.BusinessPeriods.Update(businessPeriod);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<bool> IsBusinessPeriodClosed(Guid businessPeriodId, Guid appUserId)
        {
            return await _dataContext.BusinessPeriods.AnyAsync(x => x.Id == businessPeriodId && x.Business.AppUserId == appUserId && x.Closed);
        }

        public async Task<List<BusinessPeriod>> GetBusinessPeriods(DateTime startDate, Guid appUserId)
        {
            return await _dataContext.BusinessPeriods
                .Include(x => x.BusinessExpenseTransactions)
                .Include(x => x.BusinessSaleTransactions)
                .Where(x => x.DateFrom >= startDate && x.Business.AppUserId == appUserId)
                .ToListAsync();
        }

        public async Task<BusinessPeriod> GetLastClosedBusinessPeriod(Guid appUserId)
        {
            return await _dataContext.BusinessPeriods
                .Include(x => x.BusinessExpenseTransactions)
                .Include(x => x.BusinessSaleTransactions)
                .Where(x => x.Business.AppUserId == appUserId && x.Closed)
                .OrderByDescending(x => x.DateFrom)
                .FirstOrDefaultAsync();
        }

        public async Task<List<BusinessSaleTransaction>> SearchBusinessSaleTransactions(SearchBusinessSaleTransactionsRequestDto request, Guid appUserId)
        {
            var query = _dataContext.BusinessSaleTransactions
                .Include(x => x.BusinessPeriod)
                .Where(x => x.BusinessPeriod.Business.AppUserId == appUserId);

            if (request.BusinessPeriodId is not null)
            {
                query = query.Where(x => x.BusinessPeriodId == request.BusinessPeriodId);
            }

            if (request.DateFrom is not null)
            {
                query = query.Where(x => x.Date >= request.DateFrom);
            }

            if (request.DateTo is not null)
            {
                query = query.Where(x => x.Date <= request.DateTo);
            }

            if (request.AmountFrom is not null)
            {
                query = query.Where(x => x.Amount >= request.AmountFrom);
            }

            if (request.AmountTo is not null)
            {
                query = query.Where(x => x.Amount <= request.AmountTo);
            }

            if (request.Description is not null)
            {
                query = query.Where(x => x.Description.Contains(request.Description));
            }

            if (request.TransactionPaymentType is not null)
            {
                query = query.Where(x => x.TransactionPaymentType == request.TransactionPaymentType);
            }

            return await query.ToListAsync();
        }

        public async Task<List<BusinessExpenseTransaction>> SearchBusinessExpenseTransactions(SearchBusinessExpenseTransactionsRequestDto request, Guid appUserId)
        {
            var query = _dataContext.BusinessExpenseTransactions
                .Include(x => x.BusinessPeriod)
                .Include(x => x.Supplier)
                .Where(x => x.BusinessPeriod.Business.AppUserId == appUserId);

            if (request.BusinessPeriodId is not null)
            {
                query = query.Where(x => x.BusinessPeriodId == request.BusinessPeriodId);
            }

            if (request.DateFrom is not null)
            {
                query = query.Where(x => x.Date >= request.DateFrom);
            }

            if (request.DateTo is not null)
            {
                query = query.Where(x => x.Date <= request.DateTo);
            }

            if (request.AmountFrom is not null)
            {
                query = query.Where(x => x.Amount >= request.AmountFrom);
            }

            if (request.AmountTo is not null)
            {
                query = query.Where(x => x.Amount <= request.AmountTo);
            }

            if (request.Description is not null)
            {
                query = query.Where(x => x.Description.Contains(request.Description));
            }

            if (request.TransactionPaymentType is not null)
            {
                query = query.Where(x => x.TransactionPaymentType == request.TransactionPaymentType);
            }

            if (request.SuppliersId is not null)
            {
                query = query.Where(x => request.SuppliersId.Contains(x.SupplierId));
            }

            return await query.ToListAsync();
        }

    }
}
