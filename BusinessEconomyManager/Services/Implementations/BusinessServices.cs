﻿using AutoMapper;
using BusinessEconomyManager.Data.Repositories.Implementations;
using BusinessEconomyManager.Data.Repositories.Interfaces;
using BusinessEconomyManager.DTOs;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Models.Exceptions;
using ClosedXML.Excel;
using System.Collections;

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
            if (await _businessRepository.HasBusinessPeriodTransactions(businessPeriodId, appUserId)) throw new ApiException("Business period has business transactions and can't be deleted.");
            await _businessRepository.DeleteBusinessPeriod(businessPeriodId, appUserId);
        }

        public async Task<GetBusinessPeriodDetailsResponseDto> GetBusinessPeriodDetails(Guid businessPeriodId, Guid appUserId)
        {
            BusinessPeriod businessPeriod = await _businessRepository.GetBusinessPeriod(businessPeriodId, appUserId) ?? throw new ApiException()
            {
                ErrorMessage = "Business period not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            GetBusinessPeriodDetailsResponseDto response = new(businessPeriod);

            return response;
        }

        public async Task<List<BusinessPeriod>> GetAppUserBusinessPeriods(Guid appUserId)
        {
            return await _businessRepository.GetAppUserBusinessPeriods(appUserId);
        }

        public async Task CreateBusinessSaleDay(CreateBusinessSaleDayRequestDto request, Guid appUserId)
        {
            BusinessPeriod businessPeriod = await _businessRepository.GetBusinessPeriod(request.BusinessPeriodId, appUserId) ?? throw new ApiException()
            {
                ErrorMessage = "Business period not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (businessPeriod.Closed) throw new ApiException("Business period is closed.");

            if (!request.IsValid(businessPeriod, out string errorMessage)) throw new ApiException(errorMessage);

            BusinessSaleTransaction businessSaleTransaction = _mapper.Map<BusinessSaleTransaction>(request);
            businessSaleTransaction.Amount = request.CashAmount;
            businessSaleTransaction.TransactionPaymentType = TransactionPaymentType.Cash;
            await _businessRepository.CreateBusinessSaleTransaction(businessSaleTransaction);

            businessSaleTransaction = _mapper.Map<BusinessSaleTransaction>(request);
            businessSaleTransaction.Amount = request.CreditCardAmount;
            businessSaleTransaction.TransactionPaymentType = TransactionPaymentType.CreditCard;
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

            if (await _businessRepository.IsBusinessPeriodClosed(businessSaleTransaction.BusinessPeriodId, appUserId)) throw new ApiException("Business period is closed.");

            if (!request.IsValid(businessSaleTransaction.BusinessPeriod, out string errorMessage)) throw new ApiException(errorMessage);

            BusinessSaleTransaction businessSaleTransactionUpdated = _mapper.Map<BusinessSaleTransaction>(request);
            businessSaleTransactionUpdated.BusinessPeriodId = businessSaleTransaction.BusinessPeriodId;
            await _businessRepository.UpdateBusinessSaleTransaction(businessSaleTransactionUpdated);
        }

        public async Task DeleteBusinessSaleTransaction(Guid businessSaleTransactionId, Guid appUserId)
        {
            BusinessSaleTransaction businessSaleTransactionToDelete = await _businessRepository.GetBusinessSaleTransaction(businessSaleTransactionId, appUserId) ?? throw new ApiException()
            {
                ErrorMessage = "Transaction not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (await _businessRepository.IsBusinessPeriodClosed(businessSaleTransactionToDelete.BusinessPeriodId, appUserId)) throw new ApiException("Business period is closed.");

            await _businessRepository.DeleteBusinessSaleTransaction(businessSaleTransactionToDelete);
        }

        public async Task CreateBusinessExpenseTransaction(CreateBusinessExpenseTransactionRequestDto request, Guid appUserId)
        {
            BusinessPeriod businessPeriod = await _businessRepository.GetBusinessPeriod(request.BusinessPeriodId, appUserId);
            if (businessPeriod == null) throw new ApiException()
            {
                ErrorMessage = "Business period not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (businessPeriod.Closed) throw new ApiException("Business period is closed.");

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

            if (await _businessRepository.IsBusinessPeriodClosed(businessExpenseTransaction.BusinessPeriodId, appUserId)) throw new ApiException("Business period is closed.");

            if (!request.IsValid(businessExpenseTransaction.BusinessPeriod, out string errorMessage)) throw new ApiException(errorMessage);

            BusinessExpenseTransaction businessExpenseTransactionToUpdate = _mapper.Map<BusinessExpenseTransaction>(request);
            businessExpenseTransactionToUpdate.BusinessPeriodId = businessExpenseTransaction.BusinessPeriodId;
            await _businessRepository.UpdateBusinessExpenseTransaction(businessExpenseTransactionToUpdate);
        }

        public async Task DeleteBusinessExpenseTransaction(Guid businessExpenseTransactionId, Guid appUserId)
        {
            BusinessExpenseTransaction businessExpenseTransactionToDelete = await _businessRepository.GetBusinessExpenseTransaction(businessExpenseTransactionId, appUserId) ?? throw new ApiException()
            {
                ErrorMessage = "Transaction not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (await _businessRepository.IsBusinessPeriodClosed(businessExpenseTransactionToDelete.BusinessPeriodId, appUserId)) throw new ApiException("Business period is closed.");

            await _businessRepository.DeleteBusinessExpenseTransaction(businessExpenseTransactionToDelete);
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
            Business business = await _businessRepository.GetBusiness(appUserId) ?? throw new ApiException()
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

        public async Task CreateSupplierCategory(CreateSupplierCategoryRequestDto request, Guid appUserId)
        {
            Business business = await GetBusiness(appUserId);
            if (business is null) throw new ApiException()
            {
                ErrorMessage = "Business not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            SupplierCategory supplierCategory = _mapper.Map<SupplierCategory>(request);
            supplierCategory.BusinessId = business.Id;

            await _businessRepository.CreateSupplierCategory(supplierCategory);
        }

        public async Task<List<SupplierCategory>> GetSupplierCategories(Guid appUserId)
        {
            return await _businessRepository.GetSupplierCategories(appUserId);
        }

        public async Task UpdateSupplierCategory(SupplierCategory request, Guid appUserId)
        {
            await BusinessExists(request.BusinessId, appUserId);
            await _businessRepository.UpdateSupplierCategory(request);
        }

        public async Task DeleteSupplierCategory(Guid supplierCategoryId, Guid appUserId)
        {
            SupplierCategory supplierCategoryToDelete = await _businessRepository.GetSupplierCategory(supplierCategoryId, appUserId);
            if (supplierCategoryToDelete is null) throw new ApiException()
            {
                ErrorMessage = "Supplier category not found.",
                StatusCode = StatusCodes.Status404NotFound
            };
            await _businessRepository.DeleteSupplierCategory(supplierCategoryToDelete);
        }

        private async Task BusinessExists(Guid businessId, Guid appUserId)
        {
            if (!await _businessRepository.BusinessExists(businessId, appUserId)) throw new ApiException()
            {
                ErrorMessage = "Business not found.",
                StatusCode = StatusCodes.Status404NotFound
            };
        }

        public async Task CloseBusinessPeriod(CloseBusinessPeriodRequestDto request, Guid appUserId)
        {
            BusinessPeriod businessPeriod = await _businessRepository.GetBusinessPeriod(request.BusinessPeriodId, appUserId) ?? throw new ApiException()
            {
                ErrorMessage = "Business period not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            businessPeriod.Closed = request.Closed;
            businessPeriod.AccountCashBalance = request.CashBalance;
            businessPeriod.AccountCreditCardBalance = request.CreditCardBalance;
            await _businessRepository.UpdateBusinessPeriod(businessPeriod);
        }

        public async Task CalculateBalanceBusinessPeriod(Guid businessPeriodId, Guid appUserId)
        {
            BusinessPeriod businessPeriod = await _businessRepository.GetBusinessPeriod(businessPeriodId, appUserId) ?? throw new ApiException()
            {
                ErrorMessage = "Business period not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            if (await _businessRepository.IsBusinessPeriodClosed(businessPeriodId, appUserId)) throw new ApiException("Business period is closed.");

            BusinessPeriod lastClosedBusinessPeriod = await _businessRepository.GetLastClosedBusinessPeriod(appUserId);
            businessPeriod.AccountCashBalance = lastClosedBusinessPeriod.AccountCashBalance + DefineBusinessPeriodAccountCashBalance(businessPeriod);
            businessPeriod.AccountCreditCardBalance = lastClosedBusinessPeriod.AccountCreditCardBalance + DefineBusinessPeriodAccountCreditCardBalance(businessPeriod);

            await _businessRepository.UpdateBusinessPeriod(businessPeriod);
        }

        private static double DefineBusinessPeriodAccountCashBalance(BusinessPeriod businessPeriod)
        {
            return businessPeriod.BusinessSaleTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.Cash).Sum(x => x.Amount) -
                businessPeriod.BusinessExpenseTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.Cash).Sum(x => x.Amount);
        }
        private static double DefineBusinessPeriodAccountCreditCardBalance(BusinessPeriod businessPeriod)
        {
            return businessPeriod.BusinessSaleTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.CreditCard).Sum(x => x.Amount) -
            businessPeriod.BusinessExpenseTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.CreditCard).Sum(x => x.Amount);
        }

        public async Task<GetAccountBalanceResponseDto> GetAccountBalance(Guid appUserId)
        {
            BusinessPeriod lastClosedBusinessPeriod = await _businessRepository.GetLastClosedBusinessPeriod(appUserId);
            DateTime startDate = lastClosedBusinessPeriod is null ? DateTime.Now : lastClosedBusinessPeriod.DateTo;
            List<BusinessPeriod> lastBusinessPeriods = await _businessRepository.GetBusinessPeriods(startDate, appUserId);
            return new()
            {
                CashBalance = lastClosedBusinessPeriod.AccountCashBalance + lastBusinessPeriods.Sum(x => DefineBusinessPeriodAccountCashBalance(x)),
                CreditCardBalance = lastClosedBusinessPeriod.AccountCreditCardBalance + lastBusinessPeriods.Sum(x => DefineBusinessPeriodAccountCreditCardBalance(x)),
            };
        }

        public async Task<(byte[], string)> DownloadBusinessPeriod(Guid businessPeriodId, Guid appUserId)
        {
            BusinessPeriod businessPeriod = await _businessRepository.GetBusinessPeriod(businessPeriodId, appUserId) ?? throw new ApiException()
            {
                ErrorMessage = "Business period not found.",
                StatusCode = StatusCodes.Status404NotFound
            };

            List<string> columnNamesExpensesSheet = new() { "Fecha", "Proveedor", "Importe", "Forma de pago", "Descripción", "", "Total Gastos Efectivo", "Total Gastos Tarjeta", "Total Gastos" };

            using var workbook = new XLWorkbook();
            workbook.Style.Font.FontSize = 12;
            workbook.Style.Font.FontName = "Arial";
            IXLWorksheet worksheetExpenses = workbook.Worksheets.Add($"Gastos {businessPeriod.Name}");
            IEnumerable dataToAdd = businessPeriod.BusinessExpenseTransactions.Select(x => new
            {
                x.Date,
                x.Supplier.Name,
                x.Amount,
                TransactionPaymentType = (x.TransactionPaymentType == TransactionPaymentType.Cash) ? "Efectivo" : "Tarjeta",
                x.Description,
            });

            AddWorksheetDefaultsForBusinessPeriodDownload(worksheetExpenses, columnNamesExpensesSheet, dataToAdd, new() { 3, 7, 8, 9 });

            worksheetExpenses.Cell(2, 7).FormulaA1 = "=SUMIF(D:D, \"Efectivo\", C:C)";
            worksheetExpenses.Cell(2, 8).FormulaA1 = "=SUMIF(D:D, \"Tarjeta\", C:C)";
            worksheetExpenses.Cell(2, 9).FormulaA1 = "=G2+H2";


            List<string> columnNamesSalesSheet = new() { "Fecha", "Importe", "Forma de pago", "Descripción", "", "Total Ventas Efectivo", "Total Ventas Tarjeta", "Total Ventas" };
            IXLWorksheet worksheetSales = workbook.Worksheets.Add($"Ventas {businessPeriod.Name}");
            dataToAdd = businessPeriod.BusinessSaleTransactions.Select(x => new
            {
                x.Date,
                x.Amount,
                TransactionPaymentType = (x.TransactionPaymentType == TransactionPaymentType.Cash) ? "Efectivo" : "Tarjeta",
                x.Description,
            });

            AddWorksheetDefaultsForBusinessPeriodDownload(worksheetSales, columnNamesSalesSheet, dataToAdd, new() { 2, 6, 7, 8 });

            worksheetSales.Cell(2, 6).FormulaA1 = "=SUMIF(C:C, \"Efectivo\", B:B)";
            worksheetSales.Cell(2, 7).FormulaA1 = "=SUMIF(C:C, \"Tarjeta\", B:B)";
            worksheetSales.Cell(2, 8).FormulaA1 = "=F2+G2";


            using MemoryStream stream = new();
            workbook.SaveAs(stream);
            return (stream.ToArray(), businessPeriod.Name);
        }

        private static void AddWorksheetDefaultsForBusinessPeriodDownload(IXLWorksheet worksheet, List<string> columnNames, IEnumerable dataToAdd, List<int> columnToFormatToEuro)
        {
            IXLRow firstRow = worksheet.FirstRow();
            firstRow.Style.Fill.SetBackgroundColor(XLColor.FromHtml("#5b95f9"));
            firstRow.Style.Font.Bold = true;
            firstRow.Style.Font.FontColor = XLColor.White;


            columnNames.ForEach(x =>
            {
                IXLCell cellToModify = firstRow.Cell(columnNames.IndexOf(x) + 1);
                cellToModify.Value = x;
                IXLColumn columnToModify = cellToModify.WorksheetColumn();
                columnToModify.Width = 15;
                columnToModify.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                columnToModify.Style.Alignment.Vertical = XLAlignmentVerticalValues.Center;
                columnToModify.Style.Alignment.WrapText = true;
            });

            worksheet.Cell(2, 1).InsertData(dataToAdd);
            columnToFormatToEuro.ForEach(x => worksheet.Column(x).Style.NumberFormat.Format = "€ #,##0.00");
            worksheet.Rows().Height = 30;
            firstRow.Height = 35;

        }

    }
}
