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
    }
}