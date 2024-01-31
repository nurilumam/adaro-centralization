using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AdaroConnect.Core.Abstract;
using AdaroConnect.Wrapper.Extension;

namespace AdaroConnect.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdaroConnectCore(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddAdaroConnectWrapper();
            serviceCollection.TryAddTransient<IRfcConnection, RfcConnection>();
            serviceCollection.TryAddSingleton<IRfcNetWeaverLibrary, RfcNetWeaverLibrary>();
            
            return serviceCollection;
        }
    }
}
