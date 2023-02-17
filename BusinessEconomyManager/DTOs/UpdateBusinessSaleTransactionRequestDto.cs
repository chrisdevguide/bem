using BusinessEconomyManager.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class UpdateBusinessSaleTransactionRequestDto
    {
        [Required]
        public DateTime Date { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public TransactionPaymentType TransactionPaymentType { get; set; }
        [Required]
        public Guid BusinessSaleTransactionId { get; set; }

        public bool IsValid(BusinessPeriod businessPeriod, out string errorMessage)
        {
            errorMessage = string.Empty;
            if (!Enum.IsDefined(TransactionPaymentType))
            {
                errorMessage = "Payment type is not valid.";
                return false;
            }

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