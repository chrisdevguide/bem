namespace BusinessEconomyManager.DTOs
{
    public class GetBusinessStatisticsRequestDto
    {
        public DateTimeOffset DateFrom { get; set; }
        public DateTimeOffset DateTo { get; set; }

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