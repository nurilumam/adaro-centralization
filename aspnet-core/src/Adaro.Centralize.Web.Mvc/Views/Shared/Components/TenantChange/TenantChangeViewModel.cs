using Abp.AutoMapper;
using Adaro.Centralize.Sessions.Dto;

namespace Adaro.Centralize.Web.Views.Shared.Components.TenantChange
{
    [AutoMapFrom(typeof(GetCurrentLoginInformationsOutput))]
    public class TenantChangeViewModel
    {
        public TenantLoginInfoDto Tenant { get; set; }
    }
}