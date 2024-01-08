using System.Collections.Generic;
using Adaro.Centralize.Authorization.Users.Importing.Dto;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.Authorization.Users.Importing
{
    public interface IInvalidUserExporter
    {
        FileDto ExportToFile(List<ImportUserDto> userListDtos);
    }
}
