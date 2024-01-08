using Microsoft.Extensions.Configuration;

namespace Adaro.Centralize.Configuration
{
    public interface IAppConfigurationAccessor
    {
        IConfigurationRoot Configuration { get; }
    }
}
