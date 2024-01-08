using System.Threading.Tasks;
using Abp.Domain.Policies;

namespace Adaro.Centralize.Authorization.Users
{
    public interface IUserPolicy : IPolicy
    {
        Task CheckMaxUserCountAsync(int tenantId);
    }
}
