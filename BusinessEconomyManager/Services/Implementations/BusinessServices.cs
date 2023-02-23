using AutoMapper;
using BusinessEconomyManager.Data.Repositories.Implementations;
using BusinessEconomyManager.Data.Repositories.Interfaces;
using BusinessEconomyManager.DTOs;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Models.Exceptions;

namespace BusinessEconomyManager.Services.Implementations
{
    public class BusinessServices : IBusinessServices
    {
        private readonly IBusinessRepository _businessRepository;
        private readonly IAppUserRepository _appUserRepository;
        private readonly IMapper _mapper;

        public BusinessServices(IBusinessRepository businessRepository, IAppUserRepository appUserRepository, IMapper mapper)
        {
            _businessRepository = businessRepository;
            _appUserRepository = appUserRepository;
            _mapper = mapper;
        }

        public async Task CreateBusiness(CreateBusinessRequestDto request, Guid appUserId)
        {
            if (!await _appUserRepository.AppUserExists(appUserId))
                throw new ApiException()
                {
                    ErrorMessage = "User not found.",
                    StatusCode = StatusCodes.Status404NotFound
                };

            if (await _businessRepository.AppUserHasBusiness(appUserId))
                throw new ApiException()
                {
                    ErrorMessage = "User has already a business.",
                    StatusCode = StatusCodes.Status400BadRequest
                };

            Business business = _mapper.Map<Business>(request);
            business.AppUserId = appUserId;
            await _businessRepository.CreateBusiness(business);
        }

        public async Task<Business> GetBusiness(Guid appUserId)
        {
            return await _businessRepository.GetBusiness(appUserId);
        }

        public async Task CreateBusinessPeriod(CreateBusinessPeriodRequestDto request, Guid appUserId)
        {
            if (!await _businessRepository.BusinessExists(request.BusinessId, appUserId)) throw new ApiException()
            {
                ErrorMessage = "Business not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (!request.IsValid(out string errorMessage)) throw new ApiException()
            {
                ErrorMessage = errorMessage,
                StatusCode = StatusCodes.Status400BadRequest
            };

            BusinessPeriod businessPeriod = _mapper.Map<BusinessPeriod>(request);
            await _businessRepository.CreateBusinessPeriod(businessPeriod);
        }

        public async Task<BusinessPeriod> GetBusinessPeriod(Guid businessPeriodId, Guid appUserId)
        {
            return await _businessRepository.GetBusinessPeriod(businessPeriodId, appUserId);
        }

        public async Task DeleteBusinessPeriod(Guid businessPeriodId, Guid appUserId)
        {
            await _businessRepository.DeleteBusinessPeriod(businessPeriodId, appUserId);
        }

        public async Task<GetBusinessPeriodDetailsResponseDto> GetBusinessPeriodDetails(Guid businessPeriodId, Guid appUserId)
        {
            BusinessPeriod businessPeriod = await _businessRepository.GetBusinessPeriod(businessPeriodId, appUserId);
            GetBusinessPeriodDetailsResponseDto response = new()
            {
                BusinessPeriod = businessPeriod,
                TotalSaleTransactionsByCash = businessPeriod?.BusinessSaleTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.Cash).Sum(x => x.Amount),
                TotalSaleTransactionsByCreditCard = businessPeriod?.BusinessSaleTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.CreditCard).Sum(x => x.Amount),
                TotalExpenseTransactionsByCash = businessPeriod?.BusinessExpenseTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.Cash).Sum(x => x.Amount),
                TotalExpenseTransactionsByCreditCard = businessPeriod?.BusinessExpenseTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.CreditCard).Sum(x => x.Amount),
            };
            return response;
        }

        public async Task<List<BusinessPeriod>> GetAppUserBusinessPeriods(Guid appUserId)
        {
            return await _businessRepository.GetAppUserBusinessPeriods(appUserId);
        }

        public async Task CreateBusinessSaleTransaction(CreateBusinessSaleTransactionRequestDto request, Guid appUserId)
        {
            BusinessPeriod businessPeriod = await _businessRepository.GetBusinessPeriod(request.BusinessPeriodId, appUserId);
            if (businessPeriod == null) throw new ApiException()
            {
                ErrorMessage = "Business period not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (!request.IsValid(businessPeriod, out string errorMessage)) throw new ApiException(errorMessage);

            BusinessSaleTransaction businessSaleTransaction = _mapper.Map<BusinessSaleTransaction>(request);
            await _businessRepository.CreateBusinessSaleTransaction(businessSaleTransaction);
        }

        public async Task UpdateBusinessSaleTransaction(UpdateBusinessSaleTransactionRequestDto request, Guid appUserId)
        {
            BusinessSaleTransaction businessSaleTransaction = await _businessRepository.GetBusinessSaleTransaction(request.BusinessSaleTransactionId, appUserId);
            if (businessSaleTransaction == null) throw new ApiException()
            {
                ErrorMessage = "Transaction not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (!request.IsValid(businessSaleTransaction.BusinessPeriod, out string errorMessage)) throw new ApiException(errorMessage);

            BusinessSaleTransaction businessSaleTransactionUpdated = _mapper.Map<BusinessSaleTransaction>(request);
            businessSaleTransactionUpdated.BusinessPeriodId = businessSaleTransaction.BusinessPeriodId;
            await _businessRepository.UpdateBusinessSaleTransaction(businessSaleTransactionUpdated);
        }

        public async Task DeleteBusinessSaleTransaction(Guid businessExpenseTransactionId, Guid appUserId)
        {
            await _businessRepository.DeleteBusinessSaleTransaction(businessExpenseTransactionId, appUserId);
        }

        public async Task CreateBusinessExpenseTransaction(CreateBusinessExpenseTransactionRequestDto request, Guid appUserId)
        {
            BusinessPeriod businessPeriod = await _businessRepository.GetBusinessPeriod(request.BusinessPeriodId, appUserId);
            if (businessPeriod == null) throw new ApiException()
            {
                ErrorMessage = "Business period not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (!request.IsValid(businessPeriod, out string errorMessage)) throw new ApiException(errorMessage);

            BusinessExpenseTransaction businessExpenseTransaction = _mapper.Map<BusinessExpenseTransaction>(request);
            await _businessRepository.CreateBusinessExpenseTransaction(businessExpenseTransaction);
        }

        public async Task UpdateBusinessExpenseTransaction(UpdateBusinessExpenseTransactionRequestDto request, Guid appUserId)
        {
            BusinessExpenseTransaction businessExpenseTransaction = await _businessRepository.GetBusinessExpenseTransaction(request.BusinessExpenseTransactionId, appUserId);
            if (businessExpenseTransaction == null) throw new ApiException()
            {
                ErrorMessage = "Transaction not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (!request.IsValid(businessExpenseTransaction.BusinessPeriod, out string errorMessage)) throw new ApiException(errorMessage);

            BusinessExpenseTransaction businessExpenseTransactionToUpdate = _mapper.Map<BusinessExpenseTransaction>(request);
            businessExpenseTransactionToUpdate.BusinessPeriodId = businessExpenseTransaction.BusinessPeriodId;
            await _businessRepository.UpdateBusinessExpenseTransaction(businessExpenseTransactionToUpdate);
        }

        public async Task DeleteBusinessExpenseTransaction(Guid businessSaleTransactionId, Guid appUserId)
        {
            await _businessRepository.DeleteBusinessExpenseTransaction(businessSaleTransactionId, appUserId);
        }

        public async Task<BusinessSaleTransaction> GetBusinessSaleTransaction(Guid transactionId, Guid appUserId)
        {
            return await _businessRepository.GetBusinessSaleTransaction(transactionId, appUserId);
        }

        public async Task<BusinessExpenseTransaction> GetBusinessExpenseTransaction(Guid transactionId, Guid appUserId)
        {
            return await _businessRepository.GetBusinessExpenseTransaction(transactionId, appUserId);
        }

        public async Task CreateSupplier(CreateSupplierRequestDto request, Guid appUserId)
        {
            if (!await _businessRepository.BusinessExists(request.BusinessId, appUserId)) throw new ApiException()
            {
                ErrorMessage = "Business not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            Supplier supplier = _mapper.Map<Supplier>(request);
            await _businessRepository.CreateSupplier(supplier);
        }

        public async Task<List<Supplier>> GetAppUserSuppliers(Guid appUserId)
        {
            return await _businessRepository.GetAppUserSuppliers(appUserId);
        }
    }
}
