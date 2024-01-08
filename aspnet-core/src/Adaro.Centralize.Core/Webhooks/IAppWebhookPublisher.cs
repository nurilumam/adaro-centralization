using System.Threading.Tasks;
using Adaro.Centralize.Authorization.Users;

namespace Adaro.Centralize.WebHooks
{
    public interface IAppWebhookPublisher
    {
        Task PublishTestWebhook();
    }
}
