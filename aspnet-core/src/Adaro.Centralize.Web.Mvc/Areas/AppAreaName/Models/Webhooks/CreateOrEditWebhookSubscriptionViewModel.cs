using Abp.Application.Services.Dto;
using Abp.Webhooks;
using Adaro.Centralize.WebHooks.Dto;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Webhooks
{
    public class CreateOrEditWebhookSubscriptionViewModel
    {
        public WebhookSubscription WebhookSubscription { get; set; }

        public ListResultDto<GetAllAvailableWebhooksOutput> AvailableWebhookEvents { get; set; }
    }
}
