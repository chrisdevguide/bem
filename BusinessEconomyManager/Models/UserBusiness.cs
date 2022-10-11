using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    public class UserBusiness
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreationDate { get; set; }
        public Guid UserId { get; set; }

        public User User { get; set; }
        public List<UserBusinessPeriod> UserBusinessPeriods { get; set; }
        public List<Supplier> Suppliers { get; set; }
        public List<ServiceSuppliedType> ServiceSuppliedTypes { get; set; }

        public UserBusiness(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
            CreationDate = DateTime.Now;
        }
    }
}
