using Abp.AutoMapper;
using Adaro.Centralize.MultiTenancy.Dto;

namespace Adaro.Centralize.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(RegisterTenantOutput))]
    public class TenantRegisterResultViewModel : RegisterTenantOutput
    {
        public string TenantLoginAddress { get; set; }
    }
}