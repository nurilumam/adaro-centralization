using Abp.AspNetCore.Mvc.Views;

namespace Adaro.Centralize.Web.Views
{
    public abstract class CentralizeRazorPage<TModel> : AbpRazorPage<TModel>
    {
        protected CentralizeRazorPage()
        {
            LocalizationSourceName = CentralizeConsts.LocalizationSourceName;
        }
    }
}
