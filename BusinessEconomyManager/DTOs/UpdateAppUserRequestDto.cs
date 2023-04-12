using System.ComponentModel.DataAnnotations;

namespace BusinessEconomyManager.Services.Implementations
{
    public class UpdateAppUserRequestDto
    {
        [EmailAddress]
        public string EmailAddress { get; set; }
        [StringLength(30, MinimumLength = 8)]
        public string OldPassword { get; set; }
        [StringLength(30, MinimumLength = 8)]
        public string NewPassword { get; set; }
        [StringLength(30, MinimumLength = 4)]
        public string BusinessName { get; set; }
    }
}