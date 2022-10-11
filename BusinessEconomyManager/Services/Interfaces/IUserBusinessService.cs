using BusinessEconomyManager.Dtos;
using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.Services.Interfaces
{
    public interface IUserBusinessService
    {
        public Task<UserBusiness> CreateUserBusiness(CreateUserBusinessRequestDto request, Guid userId);
        public Task<List<UserBusiness>> GetCurrentUserBusinesses(Guid userId);
        public Task<UserBusinessPeriod> CreateUserBusinessPeriod(CreateUserBusinessPeriodRequestDto request);
        public Task<List<UserBusinessPeriod>> GetUserBusinessPeriods(Guid userId);
        public Task<Supplier> CreateSupplier(CreateSupplierRequestDto request);
        public Task<List<Supplier>> GetUserBusinessSuppliers(Guid userBusinessId);
        public Task<ServiceSuppliedType> CreateServiceSuppliedType(CreateServiceSuppliedTypeRequestDto request);
        public Task<List<ServiceSuppliedType>> GetUserBusinessServiceSuppliedTypes(Guid userId);
        public Task<SupplierOperation> CreateSupplierOperation(CreateSupplierOperationRequestDto request);
        public Task<List<SupplierOperation>> GetSupplierOperations(Guid supplierId);
        public Task<Supplier> UpdateSupplier(UpdateSupplierRequestDto request);
        public Task DeleteSupplier(Guid supplierId);
    }
}
