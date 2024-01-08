using System.Collections.Generic;
using Adaro.Centralize.Web.DashboardCustomization;


namespace Adaro.Centralize.Web.Areas.AppAreaName.Startup
{
    public class DashboardViewConfiguration
    {
        public Dictionary<string, WidgetViewDefinition> WidgetViewDefinitions { get; } = new Dictionary<string, WidgetViewDefinition>();

        public Dictionary<string, WidgetFilterViewDefinition> WidgetFilterViewDefinitions { get; } = new Dictionary<string, WidgetFilterViewDefinition>();

        public DashboardViewConfiguration()
        {
            var jsAndCssFileRoot = "/Areas/AppAreaName/Views/CustomizableDashboard/Widgets/";
            var viewFileRoot = "AppAreaName/Widgets/";

            #region FilterViewDefinitions

            WidgetFilterViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Filters.FilterDateRangePicker,
                new WidgetFilterViewDefinition(
                    CentralizeDashboardCustomizationConsts.Filters.FilterDateRangePicker,
                    "~/Areas/AppAreaName/Views/Shared/Components/CustomizableDashboard/Widgets/DateRangeFilter.cshtml",
                    jsAndCssFileRoot + "DateRangeFilter/DateRangeFilter.min.js",
                    jsAndCssFileRoot + "DateRangeFilter/DateRangeFilter.min.css")
            );
            
            //add your filters iew definitions here
            #endregion

            #region WidgetViewDefinitions

            #region TenantWidgets

            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Tenant.DailySales,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Tenant.DailySales,
                    viewFileRoot + "DailySales",
                    jsAndCssFileRoot + "DailySales/DailySales.min.js",
                    jsAndCssFileRoot + "DailySales/DailySales.min.css"));

            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Tenant.GeneralStats,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Tenant.GeneralStats,
                    viewFileRoot + "GeneralStats",
                    jsAndCssFileRoot + "GeneralStats/GeneralStats.min.js",
                    jsAndCssFileRoot + "GeneralStats/GeneralStats.min.css"));

            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Tenant.ProfitShare,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Tenant.ProfitShare,
                    viewFileRoot + "ProfitShare",
                    jsAndCssFileRoot + "ProfitShare/ProfitShare.min.js",
                    jsAndCssFileRoot + "ProfitShare/ProfitShare.min.css"));
  
            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Tenant.MemberActivity,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Tenant.MemberActivity,
                    viewFileRoot + "MemberActivity",
                    jsAndCssFileRoot + "MemberActivity/MemberActivity.min.js",
                    jsAndCssFileRoot + "MemberActivity/MemberActivity.min.css"));

            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Tenant.RegionalStats,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Tenant.RegionalStats,
                    viewFileRoot + "RegionalStats",
                    jsAndCssFileRoot + "RegionalStats/RegionalStats.min.js",
                    jsAndCssFileRoot + "RegionalStats/RegionalStats.min.css",
                    12,
                    10));

            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Tenant.SalesSummary,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Tenant.SalesSummary,
                    viewFileRoot + "SalesSummary",
                    jsAndCssFileRoot + "SalesSummary/SalesSummary.min.js",
                    jsAndCssFileRoot + "SalesSummary/SalesSummary.min.css",
                    6,
                    10));

            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Tenant.TopStats,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Tenant.TopStats,
                    viewFileRoot + "TopStats",
                    jsAndCssFileRoot + "TopStats/TopStats.min.js",
                    jsAndCssFileRoot + "TopStats/TopStats.min.css",
                    12,
                    10));

            //add your tenant side widget definitions here
            #endregion

            #region HostWidgets

            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Host.IncomeStatistics,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Host.IncomeStatistics,
                    viewFileRoot + "IncomeStatistics",
                    jsAndCssFileRoot + "IncomeStatistics/IncomeStatistics.min.js",
                    jsAndCssFileRoot + "IncomeStatistics/IncomeStatistics.min.css"));

            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Host.TopStats,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Host.TopStats,
                    viewFileRoot + "HostTopStats",
                    jsAndCssFileRoot + "HostTopStats/HostTopStats.min.js",
                    jsAndCssFileRoot + "HostTopStats/HostTopStats.min.css"));

            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Host.EditionStatistics,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Host.EditionStatistics,
                    viewFileRoot + "EditionStatistics",
                    jsAndCssFileRoot + "EditionStatistics/EditionStatistics.min.js",
                    jsAndCssFileRoot + "EditionStatistics/EditionStatistics.min.css"));

            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Host.SubscriptionExpiringTenants,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Host.SubscriptionExpiringTenants,
                    viewFileRoot + "SubscriptionExpiringTenants",
                    jsAndCssFileRoot + "SubscriptionExpiringTenants/SubscriptionExpiringTenants.min.js",
                    jsAndCssFileRoot + "SubscriptionExpiringTenants/SubscriptionExpiringTenants.min.css",
                    6,
                    10));

            WidgetViewDefinitions.Add(CentralizeDashboardCustomizationConsts.Widgets.Host.RecentTenants,
                new WidgetViewDefinition(
                    CentralizeDashboardCustomizationConsts.Widgets.Host.RecentTenants,
                    viewFileRoot + "RecentTenants",
                    jsAndCssFileRoot + "RecentTenants/RecentTenants.min.js",
                    jsAndCssFileRoot + "RecentTenants/RecentTenants.min.css"));

            //add your host side widgets definitions here
            #endregion

            #endregion
        }
    }
}
