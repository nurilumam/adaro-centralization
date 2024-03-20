using System;
using Abp.Application.Services.Dto;

namespace Adaro.Centralize.ReportArea.Dtos
{
    public class RptProcurementAdjustDto : EntityDto<Guid>
    {
        public string PurchasingDocument { get; set; }

        public bool IsContract { get; set; }

        public bool IsAdjust { get; set; }

        public int DayAdjust { get; set; }

        public string Remark { get; set; }

    }
}