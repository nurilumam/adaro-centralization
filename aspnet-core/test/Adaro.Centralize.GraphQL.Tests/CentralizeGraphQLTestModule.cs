using Abp.Modules;
using Abp.Reflection.Extensions;
using Castle.Windsor.MsDependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Adaro.Centralize.Configure;
using Adaro.Centralize.Startup;
using Adaro.Centralize.Test.Base;

namespace Adaro.Centralize.GraphQL.Tests
{
    [DependsOn(
        typeof(CentralizeGraphQLModule),
        typeof(CentralizeTestBaseModule))]
    public class CentralizeGraphQLTestModule : AbpModule
    {
        public override void PreInitialize()
        {
            IServiceCollection services = new ServiceCollection();
            
            services.AddAndConfigureGraphQL();

            WindsorRegistrationHelper.CreateServiceProvider(IocManager.IocContainer, services);
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(CentralizeGraphQLTestModule).GetAssembly());
        }
    }
}