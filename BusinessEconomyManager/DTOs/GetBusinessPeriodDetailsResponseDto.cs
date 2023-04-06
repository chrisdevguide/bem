using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.DTOs
{
    public class GetBusinessPeriodDetailsResponseDto
    {
        public BusinessPeriod BusinessPeriod { get; set; }
        public double? TotalSaleTransactionsByCash { get; set; }
        public double? TotalSaleTransactionsByCreditCard { get; set; }
        public double? TotalExpenseTransactionsByCash { get; set; }
        public double? TotalExpenseTransactionsByCreditCard { get; set; }
        public double? TotalSales { get; set; }
        public double? TotalExpenses { get; set; }
        public double? CashBalance { get; set; }
        public double? CreditCardBalance { get; set; }

        public GetBusinessPeriodDetailsResponseDto(BusinessPeriod businessPeriod)
        {
            BusinessPeriod = businessPeriod;
            businessPeriod.AccountCashBalance = Math.Round(businessPeriod.AccountCashBalance, 2);
            businessPeriod.AccountCreditCardBalance = Math.Round(businessPeriod.AccountCreditCardBalance, 2);
            TotalSaleTransactionsByCash = businessPeriod?.BusinessSaleTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.Cash).Sum(x => x.Amount);
            TotalSaleTransactionsByCreditCard = businessPeriod?.BusinessSaleTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.CreditCard).Sum(x => x.Amount);
            TotalExpenseTransactionsByCash = businessPeriod?.BusinessExpenseTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.Cash).Sum(x => x.Amount);
            TotalExpenseTransactionsByCreditCard = businessPeriod?.BusinessExpenseTransactions.Where(x => x.TransactionPaymentType == TransactionPaymentType.CreditCard).Sum(x => x.Amount);
            TotalSales = TotalSaleTransactionsByCash + TotalSaleTransactionsByCreditCard;
            TotalExpenses = TotalExpenseTransactionsByCash + TotalExpenseTransactionsByCreditCard;
            CashBalance = TotalSaleTransactionsByCash - TotalExpenseTransactionsByCash;
            CreditCardBalance = TotalSaleTransactionsByCreditCard - TotalExpenseTransactionsByCreditCard;
        }
    }
}