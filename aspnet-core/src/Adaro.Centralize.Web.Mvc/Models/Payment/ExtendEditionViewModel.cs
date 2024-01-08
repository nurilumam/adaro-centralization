using System.Collections.Generic;
using Adaro.Centralize.Editions.Dto;
using Adaro.Centralize.MultiTenancy.Payments;

namespace Adaro.Centralize.Web.Models.Payment
{
    public class ExtendEditionViewModel
    {
        public EditionSelectDto Edition { get; set; }

        public List<PaymentGatewayModel> PaymentGateways { get; set; }
    }
}