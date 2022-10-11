using BusinessEconomyManager.Data;
using BusinessEconomyManager.Dtos;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BusinessEconomyManager.Services.Implementations
{
    public class RepositoryService : IRepositoryService
    {
        private readonly DataContext _dataContext;

        public RepositoryService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public User FindUserByUserLogin(SignInRequestDto request)
        {
            User user = _dataContext.Users.FirstOrDefault(x => x.EmailAddress.ToLower() == request.EmailAddress.ToLower() && x.Password == request.Password);
            return user;
        }

        public async Task<User> AddUser(User user)
        {
            await _dataContext.Users.AddAsync(user);
            _dataContext.SaveChanges();
            return user;
        }

        public async Task<bool> UserExistsAsync(User user)
        {
            return await _dataContext.Users.AnyAsync(x => x.EmailAddress == user.EmailAddress);
        }

        public async Task<bool> UserExistsAsync(Guid userId)
        {
            return await _dataContext.Users.AnyAsync(x => x.Id == userId);
        }

        public async Task<User> GetUser(Guid userId)
        {
            return await _dataContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
        }

        public async Task AddUserBusiness(UserBusiness userBusiness)
        {
            await _dataContext.UserBusinesses.AddAsync(userBusiness);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<UserBusiness>> GetCurrentUserBusinesses(Guid userId)
        {
            return await _dataContext.UserBusinesses.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task CreateUserBusinessPeriod(UserBusinessPeriod userBusinessPeriod)
        {
            await _dataContext.UserBusinessPeriods.AddAsync(userBusinessPeriod);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<UserBusinessPeriod>> GetUserBusinessPeriods(Guid userBusinessId)
        {
            return await _dataContext.UserBusinessPeriods.Where(x => x.UserBusiness.Id == userBusinessId).OrderBy(x => x.DateFrom).ToListAsync();
        }

        public async Task CreateSupplier(Supplier supplier)
        {
            await _dataContext.Suppliers.AddAsync(supplier);
            await _dataContext.SaveChangesAsync();
        }

        public async Task UpdateSupplier(Supplier supplier)
        {
            _dataContext.Suppliers.Update(supplier);
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteSupplier(Guid supplierId)
        {
            _dataContext.Suppliers.Remove(new Supplier() { Id = supplierId });
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<Supplier>> GetUserBusinessSuppliers(Guid userBusinessId)
        {
            return await _dataContext.Suppliers.Where(x => x.UserBusinessId == userBusinessId).ToListAsync();
        }

        public async Task CreateServiceSuppliedType(ServiceSuppliedType serviceSuppliedType)
        {
            await _dataContext.ServiceSuppliedTypes.AddAsync(serviceSuppliedType);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<ServiceSuppliedType>> GetUserBusinessServiceSuppliedTypes(Guid userBusinessId)
        {
            return await _dataContext.ServiceSuppliedTypes.Where(x => x.UserBusinessId == userBusinessId).ToListAsync();
        }

        public async Task CreateSupplierOperation(SupplierOperation supplierOperation)
        {
            await _dataContext.SupplierOperations.AddAsync(supplierOperation);
            await _dataContext.SaveChangesAsync();
        }

        public async Task<List<SupplierOperation>> GetSupplierOperations(Guid supplierId)
        {
            return await _dataContext.SupplierOperations.Where(x => x.SupplierId == supplierId).ToListAsync();

        }
    }
}
