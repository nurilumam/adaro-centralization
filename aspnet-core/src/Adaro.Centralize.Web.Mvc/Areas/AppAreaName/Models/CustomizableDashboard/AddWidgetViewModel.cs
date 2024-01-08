using System.Collections.Generic;
using Adaro.Centralize.DashboardCustomization.Dto;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.CustomizableDashboard
{
    public class AddWidgetViewModel
    {
        public List<WidgetOutput> Widgets { get; set; }

        public string DashboardName { get; set; }

        public string PageId { get; set; }
    }
}
