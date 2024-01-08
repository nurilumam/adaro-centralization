using Adaro.Centralize.MultiTenancy.Payments;

namespace Adaro.Centralize.Web.Models.Payment
{
    public class CancelPaymentModel
    {
        public string PaymentId { get; set; }

        public SubscriptionPaymentGatewayType Gateway { get; set; }
    }
}