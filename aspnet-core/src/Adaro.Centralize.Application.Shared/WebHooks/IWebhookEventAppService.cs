using System.Threading.Tasks;
using Abp.Webhooks;

namespace Adaro.Centralize.WebHooks
{
    public interface IWebhookEventAppService
    {
        Task<WebhookEvent> Get(string id);
    }
}
