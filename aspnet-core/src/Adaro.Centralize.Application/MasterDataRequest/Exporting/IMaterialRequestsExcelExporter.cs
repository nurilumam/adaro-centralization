using System.Collections.Generic;
using Adaro.Centralize.MasterDataRequest.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.MasterDataRequest.Exporting
{
    public interface IMaterialRequestsExcelExporter
    {
        FileDto ExportToFile(List<GetMaterialRequestForViewDto> materialRequests);
    }
}