using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Controllers;

namespace Adaro.Centralize.Web.Public.Controllers
{
    public class AboutController : CentralizeControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}