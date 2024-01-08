using System.Threading.Tasks;
using Adaro.Centralize.Sessions.Dto;

namespace Adaro.Centralize.Web.Session
{
    public interface IPerRequestSessionCache
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformationsAsync();
    }
}
