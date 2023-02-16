namespace BusinessEconomyManager.DTOs
{
    public class RegisterResponseDto
    {
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public Guid AppUserId { get; set; }
    }
}
