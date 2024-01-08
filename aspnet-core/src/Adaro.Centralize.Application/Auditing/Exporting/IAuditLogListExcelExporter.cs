using System.Collections.Generic;
using Adaro.Centralize.Auditing.Dto;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Auditing.Exporting
{
    public interface IAuditLogListExcelExporter
    {
        FileDto ExportToFile(List<AuditLogListDto> auditLogListDtos);

        FileDto ExportToFile(List<EntityChangeListDto> entityChangeListDtos);
    }
}
