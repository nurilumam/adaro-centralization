using System.Collections.Generic;
using Adaro.Centralize.ReportArea.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.ReportArea.Exporting
{
    public interface IRptProcurementAdjustsExcelExporter
    {
        FileDto ExportToFile(List<GetRptProcurementAdjustForViewDto> rptProcurementAdjusts);
    }
}