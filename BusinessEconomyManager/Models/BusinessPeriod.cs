using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    public class BusinessPeriod
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }
        public List<BusinessSaleTransaction> BusinessSaleTransactions { get; set; }
        public List<BusinessExpenseTransaction> BusinessExpenseTransactions { get; set; }
        public double AccountCashBalance { get; set; }
        public double AccountCreditCardBalance { get; set; }
        [Required]
        public bool Closed { get; set; }
        [Required]
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }
    }
}