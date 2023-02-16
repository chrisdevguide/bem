using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class CreateBusinessRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
