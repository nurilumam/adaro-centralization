using System.Threading.Tasks;
using Abp.Application.Services;
using Adaro.Centralize.MultiTenancy.Payments.PayPal.Dto;

namespace Adaro.Centralize.MultiTenancy.Payments.PayPal
{
    public interface IPayPalPaymentAppService : IApplicationService
    {
        Task ConfirmPayment(long paymentId, string paypalOrderId);

        PayPalConfigurationDto GetConfiguration();
    }
}
