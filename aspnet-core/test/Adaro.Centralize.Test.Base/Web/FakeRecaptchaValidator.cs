using System.Threading.Tasks;
using Adaro.Centralize.Security.Recaptcha;

namespace Adaro.Centralize.Test.Base.Web
{
    public class FakeRecaptchaValidator : IRecaptchaValidator
    {
        public Task ValidateAsync(string captchaResponse)
        {
            return Task.CompletedTask;
        }
    }
}
