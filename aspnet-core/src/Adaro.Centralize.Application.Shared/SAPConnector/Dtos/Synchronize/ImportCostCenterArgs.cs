using System;
using Abp;

namespace Adaro.Centralize.SAPConnector.Dto.Synchronize
{
    public class ImportCostCenterArgs
    {
        public int? TenantId { get; set; }

        public string ControllingArea { get; set; }

        public int Year { get; set; }
    }
}
