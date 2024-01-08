using System.Threading.Tasks;
using Abp.Domain.Uow;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Authorization.Delegation;
using Adaro.Centralize.Authorization.Users.Delegation;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Layout;
using Adaro.Centralize.Web.Views;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Views.Shared.Components.
    AppAreaNameActiveUserDelegationsCombobox
{
    public class AppAreaNameActiveUserDelegationsComboboxViewComponent : CentralizeViewComponent
    {
        private readonly IUserDelegationAppService _userDelegationAppService;
        private readonly IUserDelegationConfiguration _userDelegationConfiguration;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public AppAreaNameActiveUserDelegationsComboboxViewComponent(
            IUserDelegationAppService userDelegationAppService,
            IUserDelegationConfiguration userDelegationConfiguration,
            IUnitOfWorkManager unitOfWorkManager)
        {
            _userDelegationAppService = userDelegationAppService;
            _userDelegationConfiguration = userDelegationConfiguration;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public async Task<IViewComponentResult> InvokeAsync(string logoSkin = null, string logoClass = "", string cssClass = "d-flex align-items-center ms-1 ms-lg-3 active-user-delegations me-2")
        {
            return await _unitOfWorkManager.WithUnitOfWorkAsync(async () =>
            {
                var activeUserDelegations = await _userDelegationAppService.GetActiveUserDelegations();
                var model = new ActiveUserDelegationsComboboxViewModel
                {
                    UserDelegations = activeUserDelegations,
                    UserDelegationConfiguration = _userDelegationConfiguration,
                    CssClass = cssClass
                };

                return View(model);
            });
        }
    }
}
