using Abp.Application.Services;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Logging.Dto;

namespace Adaro.Centralize.Logging
{
    public interface IWebLogAppService : IApplicationService
    {
        GetLatestWebLogsOutput GetLatestWebLogs();

        FileDto DownloadWebLogs();
    }
}
