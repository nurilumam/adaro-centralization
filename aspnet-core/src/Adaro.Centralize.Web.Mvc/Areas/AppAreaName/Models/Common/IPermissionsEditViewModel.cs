using System.Collections.Generic;
using Adaro.Centralize.Authorization.Permissions.Dto;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Common
{
    public interface IPermissionsEditViewModel
    {
        List<FlatPermissionDto> Permissions { get; set; }

        List<string> GrantedPermissionNames { get; set; }
    }
}