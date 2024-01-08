using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Layout;
using Adaro.Centralize.Web.Session;
using Adaro.Centralize.Web.Views;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Views.Shared.Themes.Theme13.Components.AppAreaNameTheme13Footer
{
    public class AppAreaNameTheme13FooterViewComponent : CentralizeViewComponent
    {
        private readonly IPerRequestSessionCache _sessionCache;

        public AppAreaNameTheme13FooterViewComponent(IPerRequestSessionCache sessionCache)
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
