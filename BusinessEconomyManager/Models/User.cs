using BusinessEconomyManager.Models.Enums;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Models
{
    [Index("EmailAddress", IsUnique = true)]
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string Password { get; set; }
        public string EmailAddress { get; set; }
        public UserRoleType Role { get; set; }
        public string GivenName { get; set; }
        public string Surname { get; set; }

        public List<UserBusiness> UserBusinesses { get; set; }

        public User(string password, string emailAddress, UserRoleType role, string givenName, string surname)
        {
            Id = Guid.NewGuid();
            Password = password;
            EmailAddress = emailAddress;
            Role = role;
            Surname = surname;
            GivenName = givenName;
        }

        public User()
        {
            Id = Guid.NewGuid();
        }
    }
}
