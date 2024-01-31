using System.Collections.Generic;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using AdaroConnect.Core.Extensions;
using AdaroConnect.Core.Models;
using AdaroConnect.Extensions;
using AdaroConnect.Application.Core.Abstracts;
using AdaroConnect.Application.Core.Managers;

namespace AdaroConnect.Application.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdaroConnectSampleCore(this IServiceCollection services,IConfiguration configuration)
        {
            services.AddOptions<IConfiguration>();
            services.Configure<IOptions<IConfiguration>>(configuration);
            services.AddAdaroConnect(s=>s.ReadFromConfiguration(configuration));
            //services.AddAdaroConnect(s =>
            //{
            //    s.DefaultServer = "LIVE";
            //    s.RfcServers = new List<RfcServer>()
            //    {
            //        new RfcServer()
            //        {
            //            Alias = "LIVE",
            //            ConnectionString =
            //                "Name=LIVE;User=USER;Password=PASSWORD;Client=CLIENT_CODE;SystemId:xxx;Language=EN;AppServerHost=HOST_NAME;SystemNumber=00;MaxPoolSize:100;PoolSize=50;IdleTimeout:600;Trace=0;",
            //            ConnectionPooling = new RfcConnectionPoolingOption() {Enabled = true, PoolSize = 10}
            //        }
            //    };
            //});
            services.TryAddTransient<IJobManager, JobManager>();
            services.TryAddTransient<IVendorManager, VendorManager>();
            services.TryAddSingleton<IMaterialManager, MaterialManager>();
            services.TryAddSingleton<IBillOfMaterialManager, BillOfMaterialManager>();
            services.TryAddSingleton<IFunctionMetaDataManager, FunctionMetaDataManager>();
            services.TryAddSingleton<IMaterialSaveDataManager, MaterialSaveDataManager>();
            services.TryAddSingleton<IGoodsMovementManager, GoodsMovementManager>();
            services.TryAddSingleton<ICostCenterManager, CostCenterManager>();
            services.BuildServiceProvider();

            return services;
        }
    }
}
