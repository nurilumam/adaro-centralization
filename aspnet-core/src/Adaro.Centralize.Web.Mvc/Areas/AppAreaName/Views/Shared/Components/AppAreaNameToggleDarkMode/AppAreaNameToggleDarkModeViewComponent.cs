using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Layout;
using Adaro.Centralize.Web.Views;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameToggleDarkMode
{
    public class AppAreaNameToggleDarkModeViewComponent : CentralizeViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string cssClass, bool isDarkModeActive)
        {
            return Task.FromResult<IViewComponentResult>(View(new ToggleDarkModeViewModel(cssClass, isDarkModeActive)));
        }
    }
}