using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    public class UserBusinessPeriod
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Guid UserBusinessId { get; set; }

        public UserBusiness UserBusiness { get; set; }
        public List<SupplierOperation> SupplierOperations { get; set; }


        public UserBusinessPeriod()
        {
            Id = Guid.NewGuid();
        }
    }
}
