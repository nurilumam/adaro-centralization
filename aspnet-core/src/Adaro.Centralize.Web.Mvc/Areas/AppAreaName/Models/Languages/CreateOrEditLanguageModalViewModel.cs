using Abp.AutoMapper;
using Adaro.Centralize.Localization.Dto;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Languages
{
    [AutoMapFrom(typeof(GetLanguageForEditOutput))]
    public class CreateOrEditLanguageModalViewModel : GetLanguageForEditOutput
    {
        public bool IsEditMode => Language.Id.HasValue;
    }
}