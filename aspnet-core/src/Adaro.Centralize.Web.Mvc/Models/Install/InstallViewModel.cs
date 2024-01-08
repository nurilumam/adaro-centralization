using System.Collections.Generic;
using Abp.Localization;
using Adaro.Centralize.Install.Dto;

namespace Adaro.Centralize.Web.Models.Install
{
    public class InstallViewModel
    {
        public List<ApplicationLanguage> Languages { get; set; }

        public AppSettingsJsonDto AppSettingsJson { get; set; }
    }
}
