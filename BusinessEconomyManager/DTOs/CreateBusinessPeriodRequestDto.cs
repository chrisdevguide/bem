using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class CreateBusinessPeriodRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public DateTime DateFrom { get; set; }
        [Required]
        public DateTime DateTo { get; set; }

        public bool IsValid(out string errorMessage)
        {
            errorMessage = string.Empty;

            if (DateFrom > DateTo)
            {
                errorMessage = "Starting date must be greater than the ending date.";
                return false;
            }

            return true;
        }
    }
}