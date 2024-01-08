using System.Threading.Tasks;
using Abp.Application.Services;
using Adaro.Centralize.Configuration.Host.Dto;

namespace Adaro.Centralize.Configuration.Host
{
    public interface IHostSettingsAppService : IApplicationService
    {
        Task<HostSettingsEditDto> GetAllSettings();

        Task UpdateAllSettings(HostSettingsEditDto input);

        Task SendTestEmail(SendTestEmailInput input);
    }
}
