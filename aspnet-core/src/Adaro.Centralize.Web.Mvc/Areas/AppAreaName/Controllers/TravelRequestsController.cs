using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.TravelRequests;
using Adaro.Centralize.Web.Controllers;
using Adaro.Centralize.Authorization;
using Adaro.Centralize.Travel;
using Adaro.Centralize.Travel.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_TravelRequests)]
    public class TravelRequestsController : CentralizeControllerBase
    {
        private readonly ITravelRequestsAppService _travelRequestsAppService;

        public TravelRequestsController(ITravelRequestsAppService travelRequestsAppService)
        {
            _travelRequestsAppService = travelRequestsAppService;

        }

        public ActionResult Index()
        {
            var model = new TravelRequestsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_TravelRequests_Create, AppPermissions.Pages_TravelRequests_Edit)]
        public async Task<ActionResult> CreateOrEdit(Guid? id)
        {
            GetTravelRequestForEditOutput getTravelRequestForEditOutput;

            if (id.HasValue)
            {
                getTravelRequestForEditOutput = await _travelRequestsAppService.GetTravelRequestForEdit(new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getTravelRequestForEditOutput = new GetTravelRequestForEditOutput
                {
                    TravelRequest = new CreateOrEditTravelRequestDto()
                };
                getTravelRequestForEditOutput.TravelRequest.RequestDate = DateTime.Now;
                getTravelRequestForEditOutput.TravelRequest.RequestPlanDate = DateTime.Now;
                getTravelRequestForEditOutput.TravelRequest.CreatedDate = DateTime.Now;
            }

            var viewModel = new CreateOrEditTravelRequestViewModel()
            {
                TravelRequest = getTravelRequestForEditOutput.TravelRequest,
                UserName = getTravelRequestForEditOutput.UserName,
                AirportDisplayProperty = getTravelRequestForEditOutput.AirportDisplayProperty,
                AirportDisplayProperty2 = getTravelRequestForEditOutput.AirportDisplayProperty2,
                UserName2 = getTravelRequestForEditOutput.UserName2,
            };

            return View(viewModel);
        }

        public async Task<ActionResult> ViewTravelRequest(Guid id)
        {
            var getTravelRequestForViewDto = await _travelRequestsAppService.GetTravelRequestForView(id);

            var model = new TravelRequestViewModel()
            {
                TravelRequest = getTravelRequestForViewDto.TravelRequest
                ,
                UserName = getTravelRequestForViewDto.UserName

                ,
                AirportDisplayProperty = getTravelRequestForViewDto.AirportDisplayProperty

                ,
                AirportDisplayProperty2 = getTravelRequestForViewDto.AirportDisplayProperty2

                ,
                UserName2 = getTravelRequestForViewDto.UserName2

            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_TravelRequests_Create, AppPermissions.Pages_TravelRequests_Edit)]
        public PartialViewResult UserLookupTableModal(long? id, string displayName)
        {
            var viewModel = new TravelRequestUserLookupTableViewModel()
            {
                Id = id,
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_TravelRequestUserLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_TravelRequests_Create, AppPermissions.Pages_TravelRequests_Edit)]
        public PartialViewResult AirportLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new TravelRequestAirportLookupTableViewModel()
            {
                Id = id.ToString(),
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_TravelRequestAirportLookupTableModal", viewModel);
        }

    }
}