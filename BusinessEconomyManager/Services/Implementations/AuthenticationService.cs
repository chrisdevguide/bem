using BusinessEconomyManager.Models;
using BusinessEconomyManager.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessEconomyManager.Services.Implementations
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IConfiguration _configuration;
        private readonly SecurityKey _securityKey;
        public static string appUserIdClaimName = "appUserId";

        public AuthenticationService(IConfiguration configuration)
        {
            _configuration = configuration;
            _securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
        }
        public string GenerateAuthenticationToken(AppUser appUser)
        {
            List<Claim> claims = new()
            {
                new(ClaimTypes.Email, appUser.EmailAddress),
                new(appUserIdClaimName, appUser.Id.ToString()),
            };

            SecurityTokenDescriptor securityTokenDescriptor = new()
            {
                Subject = new(claims),
                Expires = DateTime.UtcNow.AddYears(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new(_securityKey, SecurityAlgorithms.HmacSha512Signature)
            };

            JwtSecurityTokenHandler jwtSecurityTokenHandler = new();
            SecurityToken securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtSecurityTokenHandler.WriteToken(securityToken);
        }
    }
}
