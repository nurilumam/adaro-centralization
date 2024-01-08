using Abp.AutoMapper;
using Adaro.Centralize.MultiTenancy;
using Adaro.Centralize.MultiTenancy.Dto;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Common;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Tenants
{
    [AutoMapFrom(typeof (GetTenantFeaturesEditOutput))]
    public class TenantFeaturesEditViewModel : GetTenantFeaturesEditOutput, IFeatureEditViewModel
    {
        public Tenant Tenant { get; set; }
    }
}