using System.Threading.Tasks;
using Abp.Application.Services;
using Adaro.Centralize.Install.Dto;

namespace Adaro.Centralize.Install
{
    public interface IInstallAppService : IApplicationService
    {
        Task Setup(InstallDto input);

        AppSettingsJsonDto GetAppSettingsJson();

        CheckDatabaseOutput CheckDatabase();
    }
}