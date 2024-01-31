using System.Collections.Generic;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.SAPConnector.Exporting
{
    public interface IDataProductionsExcelExporter
    {
        FileDto ExportToFile(List<GetDataProductionForViewDto> dataProductions);
    }
}