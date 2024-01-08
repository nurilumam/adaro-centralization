using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}
