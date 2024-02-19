using System.Collections.Generic;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.SAPConnector.Exporting
{
    public interface IEkkosExcelExporter
    {
        FileDto ExportToFile(List<GetEkkoForViewDto> ekkos);
    }
}