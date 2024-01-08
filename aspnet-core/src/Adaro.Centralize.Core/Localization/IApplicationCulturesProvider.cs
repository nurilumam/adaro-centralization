using System.Globalization;

namespace Adaro.Centralize.Localization
{
    public interface IApplicationCulturesProvider
    {
        CultureInfo[] GetAllCultures();
    }
}