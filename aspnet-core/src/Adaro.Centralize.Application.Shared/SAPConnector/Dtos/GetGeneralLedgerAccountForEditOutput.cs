using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetGeneralLedgerAccountForEditOutput
    {
        public CreateOrEditGeneralLedgerAccountDto GeneralLedgerAccount { get; set; }

        public string CostCenterCostCenterName { get; set; }

    }
}