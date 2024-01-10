using System.Collections.Generic;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.MasterData.Exporting
{
    public interface IMaterialGroupsExcelExporter
    {
        FileDto ExportToFile(List<GetMaterialGroupForViewDto> materialGroups);
    }
}