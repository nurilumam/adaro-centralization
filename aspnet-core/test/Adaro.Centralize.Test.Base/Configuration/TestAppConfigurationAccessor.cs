using Abp.Dependency;
using Abp.Reflection.Extensions;
using Microsoft.Extensions.Configuration;
using Adaro.Centralize.Configuration;

namespace Adaro.Centralize.Test.Base.Configuration
{
    public class TestAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }

        public TestAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(
                typeof(CentralizeTestBaseModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }
    }
}
