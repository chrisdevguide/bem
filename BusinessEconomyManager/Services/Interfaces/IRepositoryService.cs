using BusinessEconomyManager.Dtos;
using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.Services.Interfaces
{
    public interface IRepositoryService
    {
        public User FindUserByUserLogin(SignInRequestDto userLogin);
        public Task<User> AddUser(User user);
        public Task<bool> UserExistsAsync(Guid UserId);
        public Task AddUserBusiness(UserBusiness userBusiness);
        public Task<List<UserBusiness>> GetCurrentUserBusinesses(Guid userId);
        public Task<User> GetUser(Guid userId);
        public Task CreateUserBusinessPeriod(UserBusinessPeriod userBusinessPeriod);
        public Task<List<UserBusinessPeriod>> GetUserBusinessPeriods(Guid userId);
        public Task CreateSupplier(Supplier supplier);
        public Task<List<Supplier>> GetUserBusinessSuppliers(Guid userBusinessPeriodId);
        public Task CreateServiceSuppliedType(ServiceSuppliedType serviceSuppliedType);
        public Task<List<ServiceSuppliedType>> GetUserBusinessServiceSuppliedTypes(Guid userBusinessId);
        public Task CreateSupplierOperation(SupplierOperation supplierOperation);
        public Task<List<SupplierOperation>> GetSupplierOperations(Guid supplierId);
        public Task UpdateSupplier(Supplier supplier);
        public Task DeleteSupplier(Guid supplierId);
    }
}
