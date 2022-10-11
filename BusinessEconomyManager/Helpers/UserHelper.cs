using BusinessEconomyManager.Constants;
using BusinessEconomyManager.Extensions;
using BusinessEconomyManager.Models;
using BusinessEconomyManager.Models.Enums;
using System.Security.Claims;

namespace BusinessEconomyManager.Helpers
{
    public class UserHelper
    {
        public static User GetCurrentUser(HttpContext httpContext)
        {
            if (httpContext.User.Identity is not ClaimsIdentity claimsIdentity) return null;
            return new User()
            {
                Id = claimsIdentity.Claims.FirstOrDefault(x => x.Type == IdentityConstants.UserIdClaimType).Value.ToGuid() ?? throw new Exception("Identity is not valid."),
                EmailAddress = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value,
                GivenName = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value,
                Surname = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName).Value,
                Role = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role).Value.ToEnum<UserRoleType>() ?? throw new Exception("Identity is not valid."),
            };
        }
    }
}
