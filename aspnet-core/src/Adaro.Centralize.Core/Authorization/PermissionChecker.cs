using Abp.Authorization;
using Adaro.Centralize.Authorization.Roles;
using Adaro.Centralize.Authorization.Users;

namespace Adaro.Centralize.Authorization
{
    public class PermissionChecker : PermissionChecker<Role, User>
    {
        public PermissionChecker(UserManager userManager)
            : base(userManager)
        {

        }
    }
}
