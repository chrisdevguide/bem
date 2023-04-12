namespace BusinessEconomyManager.DTOs
{
    public class GetBusinessStatisticsRequestDto
    {
        public DateTime DateFrom { get; set; }
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