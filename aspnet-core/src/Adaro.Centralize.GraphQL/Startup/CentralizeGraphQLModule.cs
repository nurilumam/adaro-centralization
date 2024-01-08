using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;

namespace Adaro.Centralize.Startup
{
    [DependsOn(typeof(CentralizeCoreModule))]
    public class CentralizeGraphQLModule : AbpModule
    {
        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CentralizeGraphQLModule).GetAssembly());
        }

        public override void PreInitialize()
        {
            base.PreInitialize();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }
    }
}