using Microsoft.AspNetCore.Antiforgery;

namespace Adaro.Centralize.Web.Controllers
{
    public class AntiForgeryController : CentralizeControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
