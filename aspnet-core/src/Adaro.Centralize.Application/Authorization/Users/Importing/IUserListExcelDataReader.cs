using System.Collections.Generic;
using Adaro.Centralize.Authorization.Users.Importing.Dto;
using Abp.Dependency;

namespace Adaro.Centralize.Authorization.Users.Importing
{
    public interface IUserListExcelDataReader: ITransientDependency
    {
        List<ImportUserDto> GetUsersFromExcel(byte[] fileBytes);
    }
}
