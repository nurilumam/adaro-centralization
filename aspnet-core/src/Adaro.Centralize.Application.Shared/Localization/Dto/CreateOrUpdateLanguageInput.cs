using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.Localization.Dto
{
    public class CreateOrUpdateLanguageInput
    {
        [Required]
        public ApplicationLanguageEditDto Language { get; set; }
    }
}