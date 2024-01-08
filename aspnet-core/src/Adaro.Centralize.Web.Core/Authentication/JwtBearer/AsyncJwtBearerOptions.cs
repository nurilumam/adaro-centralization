using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Adaro.Centralize.Web.Authentication.JwtBearer
{
    public class AsyncJwtBearerOptions : JwtBearerOptions
    {
        public readonly List<IAsyncSecurityTokenValidator> AsyncSecurityTokenValidators;
        
        private readonly CentralizeAsyncJwtSecurityTokenHandler _defaultAsyncHandler = new CentralizeAsyncJwtSecurityTokenHandler();

        public AsyncJwtBearerOptions()
        {
            AsyncSecurityTokenValidators = new List<IAsyncSecurityTokenValidator>() {_defaultAsyncHandler};
        }
    }

}
