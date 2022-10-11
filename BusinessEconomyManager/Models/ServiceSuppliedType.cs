using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    public class ServiceSuppliedType
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserBusinessId { get; set; }
        public UserBusiness UserBusiness { get; set; }
        public List<Supplier> Suppliers { get; set; }


    }
}
