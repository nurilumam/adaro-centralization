using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}