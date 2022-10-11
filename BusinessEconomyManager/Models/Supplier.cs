using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    public class Supplier
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid UserBusinessId { get; set; }
        public UserBusiness UserBusiness { get; set; }
        public Guid? ServiceSuppliedTypeId { get; set; }
        public ServiceSuppliedType ServiceSuppliedType { get; set; }

        public List<SupplierOperation> SupplierOperations { get; set; }
    }
}
