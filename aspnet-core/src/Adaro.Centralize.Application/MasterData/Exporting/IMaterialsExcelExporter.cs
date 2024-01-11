using System.Collections.Generic;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.MasterData.Exporting
{
    public interface IMaterialsExcelExporter
    {
        FileDto ExportToFile(List<GetMaterialForViewDto> materials);
    }
}