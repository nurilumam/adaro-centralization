using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.MaterialGroups;
using Adaro.Centralize.Web.Controllers;
using Adaro.Centralize.Authorization;
using Adaro.Centralize.MasterData;
using Adaro.Centralize.MasterData.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_MaterialGroups)]
    public class MaterialGroupsController : CentralizeControllerBase
    {
        private readonly IMaterialGroupsAppService _materialGroupsAppService;

        public MaterialGroupsController(IMaterialGroupsAppService materialGroupsAppService)
        {
            _materialGroupsAppService = materialGroupsAppService;

        }

        public ActionResult Index()
        {
            var model = new MaterialGroupsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_MaterialGroups_Create, AppPermissions.Pages_MaterialGroups_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(Guid? id)
        {
            GetMaterialGroupForEditOutput getMaterialGroupForEditOutput;

            if (id.HasValue)
            {
                getMaterialGroupForEditOutput = await _materialGroupsAppService.GetMaterialGroupForEdit(new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getMaterialGroupForEditOutput = new GetMaterialGroupForEditOutput
                {
                    MaterialGroup = new CreateOrEditMaterialGroupDto()
                };
            }

            var viewModel = new CreateOrEditMaterialGroupModalViewModel()
            {
                MaterialGroup = getMaterialGroupForEditOutput.MaterialGroup,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewMaterialGroupModal(Guid id)
        {
            var getMaterialGroupForViewDto = await _materialGroupsAppService.GetMaterialGroupForView(id);

            var model = new MaterialGroupViewModel()
            {
                MaterialGroup = getMaterialGroupForViewDto.MaterialGroup
            };

            return PartialView("_ViewMaterialGroupModal", model);
        }

    }
}