using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class GetBusinessRequestDto
    {
        [Required]
        public Guid BusinessId { get; set; }
        public Guid AppUserId { get; set; }
    }
}