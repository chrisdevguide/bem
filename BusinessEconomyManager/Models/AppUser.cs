using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    [Index("EmailAddress", IsUnique = true)]
    public class AppUser
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public byte[] PasswordHash { get; set; }
        [Required]
        public byte[] PasswordSalt { get; set; }

        public Business Business { get; set; }
    }
}
