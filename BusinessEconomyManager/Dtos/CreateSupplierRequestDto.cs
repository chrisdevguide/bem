﻿using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.DTOs
{
    public class CreateSupplierRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}