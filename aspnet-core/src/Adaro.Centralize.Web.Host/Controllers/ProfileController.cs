using Abp.AspNetCore.Mvc.Authorization;
using Adaro.Centralize.Authorization.Users.Profile;
using Adaro.Centralize.Graphics;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.Web.Controllers
{
    [AbpMvcAuthorize]
    public class ProfileController : ProfileControllerBase
    {
        public ProfileController(
            ITempFileCacheManager tempFileCacheManager,
            IProfileAppService profileAppService,
            IImageValidator imageValidator) :
            base(tempFileCacheManager, profileAppService, imageValidator)
        {
        }
    }
}