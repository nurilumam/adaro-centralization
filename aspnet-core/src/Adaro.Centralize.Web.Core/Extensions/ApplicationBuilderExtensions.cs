using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.HttpOverrides;

namespace Adaro.Centralize.Web.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCentralizeForwardedHeaders(this IApplicationBuilder builder)
        {
            var options = new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
            };

            options.KnownNetworks.Clear();
            options.KnownProxies.Clear();

            return builder.UseForwardedHeaders(options);
        }
    }
}
