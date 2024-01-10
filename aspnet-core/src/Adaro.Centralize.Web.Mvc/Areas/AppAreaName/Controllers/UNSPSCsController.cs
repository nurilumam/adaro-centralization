using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.UNSPSCs;
using Adaro.Centralize.Web.Controllers;
using Adaro.Centralize.Authorization;
using Adaro.Centralize.MasterData;
using Adaro.Centralize.MasterData.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_UNSPSCs)]
    public class UNSPSCsController : CentralizeControllerBase
    {
        private readonly IUNSPSCsAppService _unspsCsAppService;

        public UNSPSCsController(IUNSPSCsAppService unspsCsAppService)
        {
            _unspsCsAppService = unspsCsAppService;

        }

        public ActionResult Index()
        {
            var model = new UNSPSCsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_UNSPSCs_Create, AppPermissions.Pages_UNSPSCs_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(Guid? id)
        {
            GetUNSPSCForEditOutput getUNSPSCForEditOutput;

            if (id.HasValue)
            {
                getUNSPSCForEditOutput = await _unspsCsAppService.GetUNSPSCForEdit(new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getUNSPSCForEditOutput = new GetUNSPSCForEditOutput
                {
                    UNSPSC = new CreateOrEditUNSPSCDto()
                };
            }

            var viewModel = new CreateOrEditUNSPSCModalViewModel()
            {
                UNSPSC = getUNSPSCForEditOutput.UNSPSC,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewUNSPSCModal(Guid id)
        {
            var getUNSPSCForViewDto = await _unspsCsAppService.GetUNSPSCForView(id);

            var model = new UNSPSCViewModel()
            {
                UNSPSC = getUNSPSCForViewDto.UNSPSC
            };

            return PartialView("_ViewUNSPSCModal", model);
        }

    }
}