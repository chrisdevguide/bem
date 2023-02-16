using BusinessEconomyManager.Models.Exceptions;
using System.Net.Mail;
using System.Security.Claims;

namespace BusinessEconomyManager.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetClaim(this ClaimsPrincipal claimsPrincipal, string type, bool allowNullable = true)
        {
            string claim = claimsPrincipal?.Claims?.FirstOrDefault(x => x.Type == type)?.Value;
            if (allowNullable) return claim;
            if (string.IsNullOrEmpty(claim)) throw new ApiException($"Claim of type '{type}' is required.");

            return claim;
        }

        public static string GetEmailClaim(this ClaimsPrincipal claimsPrincipal, bool allowNullable = true)
        {
            string emailAddress = claimsPrincipal.GetClaim(ClaimTypes.Email, allowNullable);

            if (!MailAddress.TryCreate(emailAddress, out _))
                throw new ApiException($"Claim of type 'Email' is not valid.");

            return emailAddress;

        }
    }
}
