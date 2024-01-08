using System.Threading.Tasks;
using Abp.Application.Services;
using Adaro.Centralize.Editions.Dto;
using Adaro.Centralize.MultiTenancy.Dto;

namespace Adaro.Centralize.MultiTenancy
{
    public interface ITenantRegistrationAppService: IApplicationService
    {
        Task<RegisterTenantOutput> RegisterTenant(RegisterTenantInput input);

        Task<EditionsSelectOutput> GetEditionsForSelect();

        Task<EditionSelectDto> GetEdition(int editionId);
    }
}