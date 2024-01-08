using System.Threading.Tasks;
using Abp.Application.Services;
using Adaro.Centralize.MultiTenancy.Payments.Dto;
using Adaro.Centralize.MultiTenancy.Payments.Stripe.Dto;

namespace Adaro.Centralize.MultiTenancy.Payments.Stripe
{
    public interface IStripePaymentAppService : IApplicationService
    {
        Task ConfirmPayment(StripeConfirmPaymentInput input);

        StripeConfigurationDto GetConfiguration();

        Task<SubscriptionPaymentDto> GetPaymentAsync(StripeGetPaymentInput input);

        Task<string> CreatePaymentSession(StripeCreatePaymentSessionInput input);
    }
}