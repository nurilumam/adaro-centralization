using Abp.AutoMapper;
using Abp.Configuration.Startup;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Adaro.Centralize.ApiClient;
using Adaro.Centralize.Mobile.MAUI.Core.ApiClient;

namespace Adaro.Centralize
{
    [DependsOn(typeof(CentralizeClientModule), typeof(AbpAutoMapperModule))]

    public class CentralizeMobileMAUIModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Localization.IsEnabled = false;
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;

            Configuration.ReplaceService<IApplicationContext, MAUIApplicationContext>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CentralizeMobileMAUIModule).GetAssembly());
        }
    }
}