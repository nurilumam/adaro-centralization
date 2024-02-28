using System.Collections.Generic;
using Adaro.Centralize.Finance.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Finance.Exporting
{
    public interface ITransferBudgetItemsExcelExporter
    {
        FileDto ExportToFile(List<GetTransferBudgetItemForViewDto> transferBudgetItems);
    }
}