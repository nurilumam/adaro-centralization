using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.GeneralLedgerMappings;
using Adaro.Centralize.Web.Controllers;
using Adaro.Centralize.Authorization;
using Adaro.Centralize.MasterData;
using Adaro.Centralize.MasterData.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_GeneralLedgerMappings)]
    public class GeneralLedgerMappingsController : CentralizeControllerBase
    {
        private readonly IGeneralLedgerMappingsAppService _generalLedgerMappingsAppService;

        public GeneralLedgerMappingsController(IGeneralLedgerMappingsAppService generalLedgerMappingsAppService)
        {
            _generalLedgerMappingsAppService = generalLedgerMappingsAppService;

        }

        public ActionResult Index()
        {
            var model = new GeneralLedgerMappingsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_GeneralLedgerMappings_Create, AppPermissions.Pages_GeneralLedgerMappings_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(Guid? id)
        {
            GetGeneralLedgerMappingForEditOutput getGeneralLedgerMappingForEditOutput;

            if (id.HasValue)
            {
                getGeneralLedgerMappingForEditOutput = await _generalLedgerMappingsAppService.GetGeneralLedgerMappingForEdit(new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getGeneralLedgerMappingForEditOutput = new GetGeneralLedgerMappingForEditOutput
                {
                    GeneralLedgerMapping = new CreateOrEditGeneralLedgerMappingDto()
                };
            }

            var viewModel = new CreateOrEditGeneralLedgerMappingModalViewModel()
            {
                GeneralLedgerMapping = getGeneralLedgerMappingForEditOutput.GeneralLedgerMapping,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewGeneralLedgerMappingModal(Guid id)
        {
            var getGeneralLedgerMappingForViewDto = await _generalLedgerMappingsAppService.GetGeneralLedgerMappingForView(id);

            var model = new GeneralLedgerMappingViewModel()
            {
                GeneralLedgerMapping = getGeneralLedgerMappingForViewDto.GeneralLedgerMapping
            };

            return PartialView("_ViewGeneralLedgerMappingModal", model);
        }

    }
}