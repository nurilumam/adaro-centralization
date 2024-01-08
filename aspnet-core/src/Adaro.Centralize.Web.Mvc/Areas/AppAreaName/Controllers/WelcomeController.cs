using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Controllers;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize]
    public class WelcomeController : CentralizeControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}