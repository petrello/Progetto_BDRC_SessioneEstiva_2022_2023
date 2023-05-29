using System.ComponentModel.DataAnnotations;

namespace IdentityServer.Quickstart.Account
{
    public class TwoStepModel
    {
        [Required]
        public string? TwoFactorCode { get; set; }
        public bool RememberLogin { get; set; }
    }
}