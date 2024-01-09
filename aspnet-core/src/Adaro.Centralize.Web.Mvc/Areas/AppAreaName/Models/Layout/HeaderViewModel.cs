using Adaro.Centralize.Sessions.Dto;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Layout
{
    public class HeaderViewModel
    {
        public int SubscriptionExpireNotifyDayCount { get; set; }

        public GetCurrentLoginInformationsOutput LoginInformations { get; set; }

        public string GetLogoUrl(string appPath, string logoSkin)
        {
            if (LoginInformations?.Tenant == null || !LoginInformations.Tenant.HasLogo())
            {
                return appPath + "Common/Images/app-logo-on-" + logoSkin + ".png";
            }

            //id parameter is used to prevent caching only.
            return appPath + "TenantCustomization/GetTenantLogo?tenantId=" + LoginInformations?.Tenant?.Id + "&skin=" + logoSkin;
        }
    }
}
