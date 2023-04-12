using BusinessEconomyManager.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class CreateBusinessSaleDayRequestDto
    {
        [Required]
        public DateTimeOffset Date { get; set; }
        [Required]
        public double CashAmount { get; set; }
        [Required]
        public double CreditCardAmount { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid BusinessId { get; set; }
        [Required]
        public Guid BusinessPeriodId { get; set; }

        public bool IsValid(BusinessPeriod businessPeriod, out string errorMessage)
        {
            errorMessage = string.Empty;

            if (Date < businessPeriod.DateFrom)
            {
                errorMessage = "Date must be greater than the business period starting date.";
                return false;
            }

            if (Date > businessPeriod.DateTo)
            {
                errorMessage = "Date must be before than the business period ending date.";
                return false;
            }

            return true;
        }
    }
}