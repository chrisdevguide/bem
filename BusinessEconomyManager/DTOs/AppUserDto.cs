namespace BusinessEconomyManager.DTOs
{
    public class AppUserDto
    {
        public string EmailAddress { get; set; }
        public string Token { get; set; }
        public Guid AppUserId { get; set; }
    }
}