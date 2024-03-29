﻿using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class CreateSupplierRequestDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid SupplierCategoryId { get; set; }
    }
}