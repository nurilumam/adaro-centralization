using System.Collections.Generic;
using Adaro.Centralize.LookupArea.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.LookupArea.Exporting
{
    public interface ILookupPagesExcelExporter
    {
        FileDto ExportToFile(List<GetLookupPageForViewDto> lookupPages);
    }
}