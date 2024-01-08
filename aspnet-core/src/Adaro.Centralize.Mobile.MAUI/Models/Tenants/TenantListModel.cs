using Abp.AutoMapper;
using Adaro.Centralize.MultiTenancy.Dto;

namespace Adaro.Centralize.Mobile.MAUI.Models.Tenants
{
    [AutoMapFrom(typeof(TenantListDto))]
    [AutoMapTo(typeof(TenantEditDto), typeof(CreateTenantInput))]
    public class TenantListModel : TenantListDto
    {
 
    }
}
