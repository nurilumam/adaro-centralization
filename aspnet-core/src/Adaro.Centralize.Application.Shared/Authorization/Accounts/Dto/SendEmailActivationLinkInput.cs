using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Authorization.Accounts.Dto
{
    public class SendEmailActivationLinkInput
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}