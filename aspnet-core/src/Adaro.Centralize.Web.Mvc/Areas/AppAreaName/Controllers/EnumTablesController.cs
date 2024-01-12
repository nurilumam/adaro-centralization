using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.EnumTables;
using Adaro.Centralize.Web.Controllers;
using Adaro.Centralize.Authorization;
using Adaro.Centralize.MasterData;
using Adaro.Centralize.MasterData.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_EnumTables)]
    public class EnumTablesController : CentralizeControllerBase
    {
        private readonly IEnumTablesAppService _enumTablesAppService;

        public EnumTablesController(IEnumTablesAppService enumTablesAppService)
        {
            _enumTablesAppService = enumTablesAppService;

        }

        public ActionResult Index()
        {
            var model = new EnumTablesViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_EnumTables_Create, AppPermissions.Pages_EnumTables_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(Guid? id)
        {
            GetEnumTableForEditOutput getEnumTableForEditOutput;

            if (id.HasValue)
            {
                getEnumTableForEditOutput = await _enumTablesAppService.GetEnumTableForEdit(new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getEnumTableForEditOutput = new GetEnumTableForEditOutput
                {
                    EnumTable = new CreateOrEditEnumTableDto()
                };
            }

            var viewModel = new CreateOrEditEnumTableModalViewModel()
            {
                EnumTable = getEnumTableForEditOutput.EnumTable,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewEnumTableModal(Guid id)
        {
            var getEnumTableForViewDto = await _enumTablesAppService.GetEnumTableForView(id);

            var model = new EnumTableViewModel()
            {
                EnumTable = getEnumTableForViewDto.EnumTable
            };

            return PartialView("_ViewEnumTableModal", model);
        }

    }
}