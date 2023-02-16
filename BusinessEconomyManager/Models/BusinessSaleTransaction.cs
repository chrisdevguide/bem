using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    public class BusinessSaleTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public TransactionPaymentType TransactionPaymentType { get; set; }
        [Required]
        public Guid BusinessPeriodId { get; set; }
        public BusinessPeriod BusinessPeriod { get; set; }
    }
}