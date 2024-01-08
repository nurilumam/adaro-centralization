using Abp.Domain.Services;

namespace Adaro.Centralize
{
    public abstract class CentralizeDomainServiceBase : DomainService
    {
        /* Add your common members for all your domain services. */

        protected CentralizeDomainServiceBase()
        {
            LocalizationSourceName = CentralizeConsts.LocalizationSourceName;
        }
    }
}
