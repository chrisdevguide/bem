using BusinessEconomyManager.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    public class SupplierOperation
    {
        [Key]
        public Guid Id { get; set; }
        public DateTime PaymentTime { get; set; }
        public double PaidAmount { get; set; }
        public PaymentType PaymentType { get; set; }
        public Guid UserBusinessPeriodId { get; set; }
        public Guid SupplierId { get; set; }

        public UserBusinessPeriod UserBusinessPeriod { get; set; }
        public Supplier Supplier { get; set; }
    }
}
