namespace Adaro.Centralize.Mobile.MAUI.Services.Tenants
{
    public interface ITenantCustomizationService
    {
        Task<string> GetTenantLogo();
    }
}