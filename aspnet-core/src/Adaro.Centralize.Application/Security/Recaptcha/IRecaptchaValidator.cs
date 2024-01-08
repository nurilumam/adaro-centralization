using System.Threading.Tasks;

namespace Adaro.Centralize.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}