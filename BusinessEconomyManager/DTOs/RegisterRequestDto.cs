using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class RegisterRequestDto
    {
        [Required]
        [EmailAddress]
        public string EmailAddress { get; set; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
        [Required]
        [MinLength(4)]
        public string BusinessName { get; set; }
    }
}