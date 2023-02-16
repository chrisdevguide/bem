using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    public class Business
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        public string Name { get; set; }
        [Required]
        public Guid AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public List<BusinessPeriod> BusinessPeriods { get; set; }
        public List<Supplier> Suppliers { get; set; }
    }
}
