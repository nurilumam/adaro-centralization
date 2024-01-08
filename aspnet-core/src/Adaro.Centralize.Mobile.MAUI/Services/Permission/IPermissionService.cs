namespace Adaro.Centralize.Services.Permission
{
    public interface IPermissionService
    {
        bool HasPermission(string key);
    }
}