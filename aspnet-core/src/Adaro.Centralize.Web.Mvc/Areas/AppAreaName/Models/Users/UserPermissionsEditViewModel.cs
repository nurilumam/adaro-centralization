using Abp.AutoMapper;
using Adaro.Centralize.Authorization.Users;
using Adaro.Centralize.Authorization.Users.Dto;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Common;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Users
{
    [AutoMapFrom(typeof(GetUserPermissionsForEditOutput))]
    public class UserPermissionsEditViewModel : GetUserPermissionsForEditOutput, IPermissionsEditViewModel
    {
        public User User { get; set; }
    }
}