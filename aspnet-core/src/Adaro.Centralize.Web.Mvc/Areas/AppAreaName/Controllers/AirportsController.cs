using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Airports;
using Adaro.Centralize.Web.Controllers;
using Adaro.Centralize.Authorization;
using Adaro.Centralize.Travel;
using Adaro.Centralize.Travel.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_Airports)]
    public class AirportsController : CentralizeControllerBase
    {
        private readonly IAirportsAppService _airportsAppService;

        public AirportsController(IAirportsAppService airportsAppService)
        {
            _airportsAppService = airportsAppService;

        }

        public ActionResult Index()
        {
            var model = new AirportsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Airports_Create, AppPermissions.Pages_Airports_Edit)]
        public async Task<PartialViewResult> CreateOrEditModal(Guid? id)
        {
            GetAirportForEditOutput getAirportForEditOutput;

            if (id.HasValue)
            {
                getAirportForEditOutput = await _airportsAppService.GetAirportForEdit(new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getAirportForEditOutput = new GetAirportForEditOutput
                {
                    Airport = new CreateOrEditAirportDto()
                };
            }

            var viewModel = new CreateOrEditAirportModalViewModel()
            {
                Airport = getAirportForEditOutput.Airport,

            };

            return PartialView("_CreateOrEditModal", viewModel);
        }

        public async Task<PartialViewResult> ViewAirportModal(Guid id)
        {
            var getAirportForViewDto = await _airportsAppService.GetAirportForView(id);

            var model = new AirportViewModel()
            {
                Airport = getAirportForViewDto.Airport
            };

            return PartialView("_ViewAirportModal", model);
        }

    }
}