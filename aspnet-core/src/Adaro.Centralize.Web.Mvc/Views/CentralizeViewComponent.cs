using Abp.AspNetCore.Mvc.ViewComponents;

namespace Adaro.Centralize.Web.Views
{
    public abstract class CentralizeViewComponent : AbpViewComponent
    {
        protected CentralizeViewComponent()
        {
            LocalizationSourceName = CentralizeConsts.LocalizationSourceName;
        }
    }
}