using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    public class BusinessExpenseTransaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public DateTimeOffset Date { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public TransactionPaymentType TransactionPaymentType { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid SupplierId { get; set; }
        public Supplier Supplier { get; set; }
        [Required]
        public Guid BusinessPeriodId { get; set; }
        public BusinessPeriod BusinessPeriod { get; set; }

    }
}