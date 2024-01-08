using System.Collections.Generic;
using Adaro.Centralize.Editions;
using Adaro.Centralize.Editions.Dto;
using Adaro.Centralize.MultiTenancy.Payments;
using Adaro.Centralize.MultiTenancy.Payments.Dto;

namespace Adaro.Centralize.Web.Models.Payment
{
    public class BuyEditionViewModel
    {
        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public decimal? AdditionalPrice { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}
