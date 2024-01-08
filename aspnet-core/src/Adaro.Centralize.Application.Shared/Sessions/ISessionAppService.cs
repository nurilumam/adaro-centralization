using System.Threading.Tasks;
using Abp.Application.Services;
using Adaro.Centralize.Sessions.Dto;

namespace Adaro.Centralize.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();

        Task<UpdateUserSignInTokenOutput> UpdateUserSignInToken();
    }
}
