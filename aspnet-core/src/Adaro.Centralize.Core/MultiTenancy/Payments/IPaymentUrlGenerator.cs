namespace Adaro.Centralize.MultiTenancy.Payments
{
    public interface IPaymentUrlGenerator
    {
        string CreatePaymentRequestUrl(SubscriptionPayment subscriptionPayment);
    }
}