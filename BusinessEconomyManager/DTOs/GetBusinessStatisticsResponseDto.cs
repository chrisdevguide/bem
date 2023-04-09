using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.DTOs
{
    public class GetBusinessStatisticsResponseDto
    {
        public double? TotalSaleTransactions { get; set; }
        public double? TotalExpenseTransactions { get; set; }
        public double? TotalSaleTransactionsByCash { get; set; }
        public double? TotalSaleTransactionsByCreditCard { get; set; }
        public double? TotalExpenseTransactionsByCash { get; set; }
        public double? TotalExpenseTransactionsByCreditCard { get; set; }
        public List<SupplierReport> SupplierReports { get; set; }
        public List<SupplierCategoryReport> SupplierCategoryReports { get; set; }
        public List<BusinessSaleDayReport> BusinessSaleDayReports { get; set; }
        public List<BusinessPeriodReport> BusinessPeriodReports { get; set; }

    }
}