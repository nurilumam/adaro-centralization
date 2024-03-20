using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.ReportArea.Dtos
{
    public class GetRptProcurementAdjustForEditOutput
    {
        public CreateOrEditRptProcurementAdjustDto RptProcurementAdjust { get; set; }

    }
}