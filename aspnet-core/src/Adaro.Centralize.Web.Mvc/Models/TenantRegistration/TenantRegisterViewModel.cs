using Adaro.Centralize.Editions;
using Adaro.Centralize.Editions.Dto;
using Adaro.Centralize.MultiTenancy.Payments;
using Adaro.Centralize.Security;
using Adaro.Centralize.MultiTenancy.Payments.Dto;

namespace Adaro.Centralize.Web.Models.TenantRegistration
{
    public class TenantRegisterViewModel
    {
        public PasswordComplexitySetting PasswordComplexitySetting { get; set; }

        public int? EditionId { get; set; }

        public SubscriptionStartType? SubscriptionStartType { get; set; }

        public EditionSelectDto Edition { get; set; }

        public EditionPaymentType EditionPaymentType { get; set; }
    }
}
