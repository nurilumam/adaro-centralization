using Abp.Auditing;
using Adaro.Centralize.Configuration.Dto;

namespace Adaro.Centralize.Configuration.Tenants.Dto
{
    public class TenantEmailSettingsEditDto : EmailSettingsEditDto
    {
        public bool UseHostDefaultEmailSettings { get; set; }
    }
}