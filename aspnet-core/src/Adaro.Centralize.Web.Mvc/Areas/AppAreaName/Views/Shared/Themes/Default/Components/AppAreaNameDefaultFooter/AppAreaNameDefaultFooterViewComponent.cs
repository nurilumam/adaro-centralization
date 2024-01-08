using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Layout;
using Adaro.Centralize.Web.Session;
using Adaro.Centralize.Web.Views;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Views.Shared.Themes.Default.Components.AppAreaNameDefaultFooter
{
    public class AppAreaNameDefaultFooterViewComponent : CentralizeViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameDefaultFooterViewComponent(IPerRequestSessionCache sessionCache)
        {
            _sessionCache = sessionCache;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var footerModel = new FooterViewModel
            {
                LoginInformations = await _sessionCache.GetCurrentLoginInformationsAsync()
            };

            return View(footerModel);
        }
    }
}
