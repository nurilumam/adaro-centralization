using System.Collections.Generic;
using Adaro.Centralize.Authorization.Permissions.Dto;

namespace Adaro.Centralize.Authorization.Users.Dto
{
    public class GetUserPermissionsForEditOutput
    {
        public List<FlatPermissionDto> Permissions { get; set; }

        public List<string> GrantedPermissionNames { get; set; }
    }
}