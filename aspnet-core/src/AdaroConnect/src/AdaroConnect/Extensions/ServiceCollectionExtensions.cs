using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Options;
using AdaroConnect.Abstract;
using AdaroConnect.Core;
using AdaroConnect.Core.Abstract;
using AdaroConnect.Core.Extensions;
using AdaroConnect.Core.Models;
using AdaroConnect.Utility;

namespace AdaroConnect.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdaroConnect(this IServiceCollection services, Action<RfcConfiguration> configurationAction)
        {
            services.AddOptions();
            services.Configure(configurationAction);
            services.AddAdaroConnectCore();
            services.TryAddTransient<IRfcClient, RfcClient>();
            services.TryAddTransient<IRfcConnectionPoolServiceFactory, RfcConnectionPoolServiceFactory>();
            services.TryAddSingleton<IPropertyCache, PropertyCache>();

            RfcConfiguration sapConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<RfcConfiguration>>().Value;

            GenerateConnectionPools(services, sapConfiguration);
            return services;
        }
        private static void GenerateConnectionPools(IServiceCollection services, RfcConfiguration rfcConfiguration)
        {
            foreach (RfcServer sapServerConnection in rfcConfiguration.RfcServers)
            {
                if (sapServerConnection.ConnectionPooling.Enabled)
                {
                    services.AddSingleton<IRfcConnectionPool>(s => new RfcConnectionPool(s, sapServerConnection.Alias, rfcConfiguration));
                }
            }
        }
    }
}
