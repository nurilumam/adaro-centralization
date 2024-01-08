using Adaro.Centralize.Models.NavigationMenu;

namespace Adaro.Centralize.Services.Navigation
{
    public interface IMenuProvider
    {
        List<NavigationMenuItem> GetAuthorizedMenuItems(Dictionary<string, string> grantedPermissions);
    }
}