using AutoMapper;
using BusinessEconomyManager.Dtos;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Services.Interfaces;

namespace BusinessEconomyManager.Services.Implementations
{
    public class UserBusinessService : IUserBusinessService
    {
        private readonly IRepositoryService _repositoryService;
        private readonly IMapper _mapper;
        private readonly IValidationService _validationService;

        public UserBusinessService(IRepositoryService repositoryService, IMapper mapper, IValidationService validationService)
        {
            _repositoryService = repositoryService;
            _mapper = mapper;
            _validationService = validationService;
        }

        public async Task<UserBusiness> CreateUserBusiness(CreateUserBusinessRequestDto request, Guid userId)
        {
            if (!await _repositoryService.UserExistsAsync(userId)) throw new Exception($"User with Id '{userId}' does not exist.");
            UserBusiness userBusinessToAdd = _mapper.Map<UserBusiness>(request);
            userBusinessToAdd.UserId = userId;
            await _repositoryService.AddUserBusiness(userBusinessToAdd);
            return userBusinessToAdd;
        }

        public async Task<List<UserBusiness>> GetCurrentUserBusinesses(Guid userId)
        {
            return await _repositoryService.GetCurrentUserBusinesses(userId) ?? throw new Exception($"User with Id {userId} not found.");
        }

        public async Task<UserBusinessPeriod> CreateUserBusinessPeriod(CreateUserBusinessPeriodRequestDto request)
        {
            if (!_validationService.ValidateCreateUserBusinessPeriodRequest(request)) throw new Exception("Request is not valid.");
            UserBusinessPeriod userBusinessPeriod = _mapper.Map<UserBusinessPeriod>(request);
            await _repositoryService.CreateUserBusinessPeriod(userBusinessPeriod);
            return userBusinessPeriod;
        }

        public async Task<List<UserBusinessPeriod>> GetUserBusinessPeriods(Guid userBusinessId)
        {
            return await _repositoryService.GetUserBusinessPeriods(userBusinessId);
        }

        public async Task<Supplier> CreateSupplier(CreateSupplierRequestDto request)
        {
            if (!_validationService.ValidateCreateSupplierRequest(request)) throw new Exception("Request is not valid.");
            Supplier supplier = _mapper.Map<Supplier>(request);
            await _repositoryService.CreateSupplier(supplier);
            return supplier;
        }

        public async Task<Supplier> UpdateSupplier(UpdateSupplierRequestDto request)
        {
            if (!_validationService.ValidateUpdateSupplierRequest(request)) throw new Exception("Request is not valid.");
            Supplier supplier = _mapper.Map<Supplier>(request);
            await _repositoryService.UpdateSupplier(supplier);
            return supplier;
        }

        public async Task DeleteSupplier(Guid supplierId)
        {
            await _repositoryService.DeleteSupplier(supplierId);
        }

        public async Task<List<Supplier>> GetUserBusinessSuppliers(Guid userBusinessId)
        {
            return await _repositoryService.GetUserBusinessSuppliers(userBusinessId);
        }

        public async Task<ServiceSuppliedType> CreateServiceSuppliedType(CreateServiceSuppliedTypeRequestDto request)
        {
            if (!_validationService.ValidateCreateServiceSuppliedTypeRequest(request)) throw new Exception("Request is not valid.");
            ServiceSuppliedType serviceSuppliedType = _mapper.Map<ServiceSuppliedType>(request);
            await _repositoryService.CreateServiceSuppliedType(serviceSuppliedType);
            return serviceSuppliedType;
        }

        public async Task<List<ServiceSuppliedType>> GetUserBusinessServiceSuppliedTypes(Guid userBusinessId)
        {
            return await _repositoryService.GetUserBusinessServiceSuppliedTypes(userBusinessId);
        }

        public async Task<SupplierOperation> CreateSupplierOperation(CreateSupplierOperationRequestDto request)
        {
            if (!_validationService.ValidateCreateSupplierOperationRequest(request)) throw new Exception("Request is not valid.");
            SupplierOperation supplierOperation = _mapper.Map<SupplierOperation>(request);
            await _repositoryService.CreateSupplierOperation(supplierOperation);
            return supplierOperation;
        }

        public async Task<List<SupplierOperation>> GetSupplierOperations(Guid supplierId)
        {
            return await _repositoryService.GetSupplierOperations(supplierId);
        }
    }
}
