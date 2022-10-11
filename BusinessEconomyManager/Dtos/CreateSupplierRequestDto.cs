namespace BusinessEconomyManager.Dtos
{
    public class CreateSupplierRequestDto
    {
        public string Name { get; set; }
        public Guid? ServiceSuppliedTypeId { get; set; }
        public Guid UserBusinessId { get; set; }
    }
}
