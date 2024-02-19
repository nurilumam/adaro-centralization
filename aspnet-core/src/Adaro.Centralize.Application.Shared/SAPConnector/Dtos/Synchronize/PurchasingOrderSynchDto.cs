using System;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class PurchasingOrderSynchDto
    {
        public string CompanyCode { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public bool IsActive { get; set; }

    }
}