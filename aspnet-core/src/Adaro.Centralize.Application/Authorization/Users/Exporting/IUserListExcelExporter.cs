using System.Collections.Generic;
using Adaro.Centralize.Authorization.Users.Dto;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Authorization.Users.Exporting
{
    public interface IUserListExcelExporter
    {
        FileDto ExportToFile(List<UserListDto> userListDtos);
    }
}