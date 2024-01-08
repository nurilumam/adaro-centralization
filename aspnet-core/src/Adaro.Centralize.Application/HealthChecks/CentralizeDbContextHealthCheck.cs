using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Adaro.Centralize.EntityFrameworkCore;

namespace Adaro.Centralize.HealthChecks
{
    public class CentralizeDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;

        public CentralizeDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("CentralizeDbContext connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("CentralizeDbContext could not connect to database"));
        }
    }
}
