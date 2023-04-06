using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class CloseBusinessPeriodRequestDto
    {
        [Required]
        public Guid BusinessPeriodId { get; set; }
        [Required]
        public bool Closed { get; set; }
        [Required]
        public double CashBalance { get; set; }
        [Required]
        public double CreditCardBalance { get; set; }
    }
}