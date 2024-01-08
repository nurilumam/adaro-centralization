using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Adaro.Centralize
{
    public class CentralizeClientModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CentralizeClientModule).GetAssembly());
        }
    }
}
