namespace BusinessEconomyManager.DTOs
{
    public class SearchBusinessSaleTransactionsRequestDto
    {
        public Guid? BusinessPeriodId { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
        public double? AmountFrom { get; set; }
        public double? AmountTo { get; set; }
        public string Description { get; set; }
        public TransactionPaymentType? TransactionPaymentType { get; set; }
    }
}