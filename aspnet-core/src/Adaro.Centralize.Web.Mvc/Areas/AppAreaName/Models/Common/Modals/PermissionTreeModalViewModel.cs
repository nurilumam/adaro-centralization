using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Adaro.Centralize.Authorization.Permissions.Dto;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Common.Modals
{
    public class PermissionTreeModalViewModel : IPermissionsEditViewModel
    {
        public List<FlatPermissionDto> Permissions { get; set; }
        public List<string> GrantedPermissionNames { get; set; }
    }
}
