using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.DTOs
{
    public class BusinessPeriodReport
    {
        public BusinessPeriod BusinessPeriod { get; set; }
        public double TotalSales { get; set; }
        public double TotalExpenses { get; set; }
    }
}