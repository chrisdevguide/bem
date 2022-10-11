namespace BusinessEconomyManager.Dtos
{
    public class CreateUserBusinessPeriodRequestDto
    {
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Guid UserBusinessId { get; set; }
    }
}
