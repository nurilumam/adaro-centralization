using System.Threading.Tasks;

namespace Adaro.Centralize.Security
{
    public interface IPasswordComplexitySettingStore
    {
        Task<PasswordComplexitySetting> GetSettingsAsync();
    }
}
