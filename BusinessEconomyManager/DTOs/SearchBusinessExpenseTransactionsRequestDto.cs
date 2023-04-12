namespace BusinessEconomyManager.DTOs
{
    public class SearchBusinessExpenseTransactionsRequestDto
    {
        public Guid? BusinessPeriodId { get; set; }
        public DateTimeOffset? DateFrom { get; set; }
        public DateTimeOffset? DateTo { get; set; }
        public double? AmountFrom { get; set; }
        public double? AmountTo { get; set; }
        public string Description { get; set; }
        public TransactionPaymentType? TransactionPaymentType { get; set; }
        public List<Guid> SuppliersId { get; set; }
        public List<Guid> SupplierCategoriesId { get; set; }
    }
}