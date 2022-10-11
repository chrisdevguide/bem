using BusinessEconomyManager.Models.Enums;

namespace BusinessEconomyManager.Dtos
{
    public class CreateSupplierOperationRequestDto
    {
        public DateTime PaymentTime { get; set; }
        public double PaidAmount { get; set; }
        public PaymentType PaymentType { get; set; }
        public Guid UserBusinessPeriodId { get; set; }
        public Guid SupplierId { get; set; }
    }
}
