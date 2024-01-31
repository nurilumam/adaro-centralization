using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AdaroConnect.Wrapper.Abstract;
using AdaroConnect.Wrapper.Interop;

namespace AdaroConnect.Wrapper.Extension
{
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAdaroConnectWrapper(this IServiceCollection serviceCollection)
        {
            serviceCollection.TryAddSingleton<IRfcInterop,RfcInterop>();
            serviceCollection.BuildServiceProvider();
            return serviceCollection;
        }
    }
}
