using System.Collections.Generic;
using Abp;
using Adaro.Centralize.Chat.Dto;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Chat.Exporting
{
    public interface IChatMessageListExcelExporter
    {
        FileDto ExportToFile(UserIdentifier user, List<ChatMessageExportDto> messages);
    }
}
