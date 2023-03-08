using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class UpdateSupplierRequestDto
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
    }
}