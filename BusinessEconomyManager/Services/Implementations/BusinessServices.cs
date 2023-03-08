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
            Business business = await _businessRepository.GetBusiness(appUserId);
            if (business is null) throw new ApiException()
            {
                ErrorMessage = "The user doesn't have a business.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (!request.IsValid(out string errorMessage)) throw new ApiException()
            {
                ErrorMessage = errorMessage,
                StatusCode = StatusCodes.Status400BadRequest
            };

            BusinessPeriod businessPeriod = _mapper.Map<BusinessPeriod>(request);
            businessPeriod.BusinessId = business.Id;
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
            Business business = await _businessRepository.GetBusiness(appUserId);
            if (business is null) throw new ApiException()
            {
                ErrorMessage = "Business not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            Supplier supplier = _mapper.Map<Supplier>(request);
            supplier.BusinessId = business.Id;

            await _businessRepository.CreateSupplier(supplier);
        }

        public async Task UpdateSupplier(Supplier supplier, Guid appUserId)
        {
            if (!await _businessRepository.SupplierExists(supplier.Id, appUserId)) throw new ApiException()
            {
                ErrorMessage = "Supplier not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            Supplier supplierToUpdate = await _businessRepository.GetSupplier(supplier.Id, appUserId);

            if (supplierToUpdate.Name != supplier.Name) throw new ApiException("Updating the supplier name is not allowed.");

            await _businessRepository.UpdateSupplier(supplier, appUserId);
        }

        public async Task DeleteSupplier(Guid supplierId, Guid appUserId)
        {
            Supplier supplierToDelete = await _businessRepository.GetSupplier(supplierId, appUserId);
            if (supplierToDelete is null) throw new ApiException()
            {
                ErrorMessage = "Supplier not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (await _businessRepository.HasSupplierBusinessExpenseTransactions(supplierId, appUserId))
                throw new ApiException("Supplier has expense transactions and can't be deleted.");

            await _businessRepository.DeleteSupplier(supplierToDelete, appUserId);
        }

        public async Task<List<Supplier>> GetAppUserSuppliers(Guid appUserId)
        {
            return await _businessRepository.GetAppUserSuppliers(appUserId);
        }

        public async Task<GetBusinessStatisticsResponseDto> GetBusinessStatistics(GetBusinessStatisticsRequestDto request, Guid appUserId)
        {
            if (!request.IsValid(out string errorMessage)) throw new ApiException(errorMessage);

            List<BusinessSaleTransaction> businessSaleTransactions = await _businessRepository.GetBusinessSaleTransactions(request.DateFrom, request.DateTo, appUserId);
            List<BusinessExpenseTransaction> businessExpenseTransactions = await _businessRepository.GetBusinessExpenseTransactions(request.DateFrom, request.DateTo, appUserId);

            return new()
            {
                TotalSaleTransactions = businessSaleTransactions.Sum(x => x.Amount),
                TotalExpenseTransactions = businessExpenseTransactions.Sum(x => x.Amount),
                TotalSaleTransactionsByCash = businessSaleTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.Cash).Sum(x => x.Amount),
                TotalSaleTransactionsByCreditCard = businessSaleTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.CreditCard).Sum(x => x.Amount),
                TotalExpenseTransactionsByCash = businessExpenseTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.Cash).Sum(x => x.Amount),
                TotalExpenseTransactionsByCreditCard = businessExpenseTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.CreditCard).Sum(x => x.Amount),
            };
        }
    }
}
