using System.Collections.Generic;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.SAPConnector.Exporting
{
    public interface IZMM020RExcelExporter
    {
        FileDto ExportToFile(List<GetZMM020RForViewDto> zmM020R);
    }
}