using Abp.AutoMapper;
using Adaro.Centralize.MultiTenancy.Dto;

namespace Adaro.Centralize.Web.Models.TenantRegistration
{
    [AutoMapFrom(typeof(EditionsSelectOutput))]
    public class EditionsSelectViewModel : EditionsSelectOutput
    {
    }
}
