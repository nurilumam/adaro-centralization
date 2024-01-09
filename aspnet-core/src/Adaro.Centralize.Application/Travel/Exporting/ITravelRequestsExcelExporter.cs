using System.Collections.Generic;
using Adaro.Centralize.Travel.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Travel.Exporting
{
    public interface ITravelRequestsExcelExporter
    {
        FileDto ExportToFile(List<GetTravelRequestForViewDto> travelRequests);
    }
}