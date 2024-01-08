using System.Threading.Tasks;
using Abp.Application.Services;
using Adaro.Centralize.Configuration.Tenants.Dto;

namespace Adaro.Centralize.Configuration.Tenants
{
    public interface ITenantSettingsAppService : IApplicationService
    {
        Task<TenantSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(TenantSettingsEditDto input);

        Task ClearDarkLogo();
        
        Task ClearLightLogo();

        Task ClearCustomCss();
    }
}
