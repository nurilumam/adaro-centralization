using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Authorization.Permissions.Dto;

namespace Adaro.Centralize.Authorization.Permissions
{
    public interface IPermissionAppService : IApplicationService
    {
        ListResultDto<FlatPermissionWithLevelDto> GetAllPermissions();
    }
}
