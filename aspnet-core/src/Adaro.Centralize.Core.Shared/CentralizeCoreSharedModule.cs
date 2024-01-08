using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Adaro.Centralize
{
    public class CentralizeCoreSharedModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CentralizeCoreSharedModule).GetAssembly());
        }
    }
}