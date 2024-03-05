using System.Collections.Generic;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.SAPConnector.Exporting
{
    public interface IZMM021RExcelExporter
    {
        FileDto ExportToFile(List<GetZMM021RForViewDto> zmM021R);
    }
}