﻿using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    public class Supplier
    {
        [Required]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        [Required]
        public Guid BusinessId { get; set; }
        public Business Business { get; set; }
        [Required]
        public Guid SupplierCategoryId { get; set; }
        public SupplierCategory SupplierCategory { get; set; }
    }
}