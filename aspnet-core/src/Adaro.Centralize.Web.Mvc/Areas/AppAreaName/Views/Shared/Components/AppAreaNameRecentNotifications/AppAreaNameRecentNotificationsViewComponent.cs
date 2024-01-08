using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Layout;
using Adaro.Centralize.Web.Views;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Views.Shared.Components.AppAreaNameRecentNotifications
{
    public class AppAreaNameRecentNotificationsViewComponent : CentralizeViewComponent
    {
        public Task<IViewComponentResult> InvokeAsync(string cssClass, string iconClass = "flaticon-alert-2 unread-notification fs-2")
        {
            var model = new RecentNotificationsViewModel
            {
                CssClass = cssClass,
                IconClass = iconClass
            };
            
            return Task.FromResult<IViewComponentResult>(View(model));
        }
    }
}
