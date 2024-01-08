using Adaro.Centralize.Dto;

namespace Adaro.Centralize.WebHooks.Dto
{
    public class GetAllSendAttemptsInput : PagedInputDto
    {
        public string SubscriptionId { get; set; }
    }
}
