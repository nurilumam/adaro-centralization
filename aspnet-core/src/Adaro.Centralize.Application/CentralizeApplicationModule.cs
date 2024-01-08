using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Adaro.Centralize.Authorization;

namespace Adaro.Centralize
{
    /// <summary>
    /// Application layer module of the application.
    /// </summary>
    [DependsOn(
        typeof(CentralizeApplicationSharedModule),
        typeof(CentralizeCoreModule)
        )]
    public class CentralizeApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            //Adding authorization providers
            Configuration.Authorization.Providers.Add<AppAuthorizationProvider>();

            //Adding custom AutoMapper configuration
            Configuration.Modules.AbpAutoMapper().Configurators.Add(CustomDtoMapper.CreateMappings);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CentralizeApplicationModule).GetAssembly());
        }
    }
}