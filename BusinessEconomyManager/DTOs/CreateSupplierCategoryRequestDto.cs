using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class CreateSupplierCategoryRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}