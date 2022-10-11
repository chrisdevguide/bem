using BusinessEconomyManager.Dtos;
using BusinessEconomyManager.Models;

namespace BusinessEconomyManager.Services.Interfaces
{
    public interface IIdentityServices
    {
        public string SignIn(SignInRequestDto userLogin);
        public Task<User> SignUp(SignUpRequestDto request);
        public Task<User> GetUser(Guid userId);
    }
}
