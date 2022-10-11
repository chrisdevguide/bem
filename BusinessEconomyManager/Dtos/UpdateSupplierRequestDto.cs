namespace BusinessEconomyManager.Dtos
{
    public class UpdateSupplierRequestDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? ServiceSuppliedTypeId { get; set; }
        public Guid UserBusinessId { get; set; }
    }
}
