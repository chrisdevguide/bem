using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class CreateBusinessPeriodFromExcelRequestDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public IFormFile ExcelFile { get; set; }
        public bool CreateMissingSuppliers { get; set; }
    }
}