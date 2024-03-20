using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.ReportArea.Dtos
{
    public class CreateOrEditRptProcurementAdjustDto : EntityDto<Guid?>
    {

        [Required]
        public string PurchasingDocument { get; set; }

        public bool IsContract { get; set; }

        public bool IsAdjust { get; set; }

        public int DayAdjust { get; set; }

        public string Remark { get; set; }

    }
}