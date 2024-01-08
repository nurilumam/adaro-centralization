using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Views;
using Abp.Extensions;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.Extensions.Configuration;
using Adaro.Centralize.Configuration;
using Adaro.Centralize.Localization;
using Adaro.Centralize.UiCustomization;
using Adaro.Centralize.UiCustomization.Dto;

namespace Adaro.Centralize.Web.Views
{
    public abstract class CentralizeRazorPage<TModel> : AbpRazorPage<TModel>
    {
        [RazorInject] public IAbpSession AbpSession { get; set; }

        [RazorInject] public IUiThemeCustomizerFactory UiThemeCustomizerFactory { get; set; }

        [RazorInject] public IAppConfigurationAccessor AppConfigurationAccessor { get; set; }

        protected CentralizeRazorPage()
        {
            LocalizationSourceName = CentralizeConsts.LocalizationSourceName;
        }

        public async Task<UiCustomizationSettingsDto> GetTheme()
        {
            var themeCustomizer = await UiThemeCustomizerFactory.GetCurrentUiCustomizer();
            var settings = await themeCustomizer.GetUiSettings();
            return settings;
        }

        public async Task<string> GetContainerClass()
        {
            var theme = await GetTheme();
            if (theme.BaseSettings.Layout.LayoutType == "fluid")
            {
                return "app-container container-fluid";
            }

            return theme.BaseSettings.Layout.LayoutType.IsIn("fixed", "fluid-xxl")
                ? "app-container container-xxl"
                : "app-container container";
        }

        public async Task<string> GetLogoSkin()
        {
            var theme = await GetTheme();
            if (theme.IsTopMenuUsed || theme.IsTabMenuUsed)
            {
                return theme.BaseSettings.Layout.DarkMode ? "light" : "dark";
            }

            return theme.BaseSettings.Menu.AsideSkin;
        }
        
        public string GetMomentLocale()
        {
            if (CultureHelper.IsRtl)
            {
                return "en";
            }

            var momentLocaleMapping = AppConfigurationAccessor.Configuration.GetSection("LocaleMappings:Moment").Get<List<LocaleMappingInfo>>();
            if (momentLocaleMapping == null)
            {
                return CultureInfo.CurrentUICulture.Name;
            }

            var mapping = momentLocaleMapping.FirstOrDefault(e => e.From == CultureInfo.CurrentUICulture.Name);
            if (mapping == null)
            {
                return CultureInfo.CurrentUICulture.Name;
            }

            return mapping.To;
        }
    }
}
