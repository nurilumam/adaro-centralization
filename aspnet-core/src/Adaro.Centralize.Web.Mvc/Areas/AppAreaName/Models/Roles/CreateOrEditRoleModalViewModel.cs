using Abp.AutoMapper;
using Adaro.Centralize.Authorization.Roles.Dto;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Common;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Roles
{
    [AutoMapFrom(typeof(GetRoleForEditOutput))]
    public class CreateOrEditRoleModalViewModel : GetRoleForEditOutput, IPermissionsEditViewModel
    {
        public bool IsEditMode => Role.Id.HasValue;
    }
}