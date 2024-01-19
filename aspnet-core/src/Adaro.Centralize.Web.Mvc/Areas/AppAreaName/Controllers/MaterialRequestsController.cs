using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.MaterialRequests;
using Adaro.Centralize.Web.Controllers;
using Adaro.Centralize.Authorization;
using Adaro.Centralize.MasterDataRequest;
using Adaro.Centralize.MasterDataRequest.Dtos;
using Abp.Application.Services.Dto;
using Abp.Extensions;

using System.IO;
using System.Linq;
using Abp.Web.Models;
using Abp.UI;
using Abp.IO.Extensions;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Controllers
{
    [Area("AppAreaName")]
    [AbpMvcAuthorize(AppPermissions.Pages_MaterialRequests)]
    public class MaterialRequestsController : CentralizeControllerBase
    {
        private readonly IMaterialRequestsAppService _materialRequestsAppService;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const long MaxPictureLength = 5242880; //5MB
        private const string MaxPictureLengthUserFriendlyValue = "5MB"; //5MB
        private readonly string[] PictureAllowedFileTypes = { "jpeg", "jpg", "png" };

        public MaterialRequestsController(IMaterialRequestsAppService materialRequestsAppService, ITempFileCacheManager tempFileCacheManager)
        {
            _materialRequestsAppService = materialRequestsAppService;
            _tempFileCacheManager = tempFileCacheManager;
        }

        public ActionResult Index()
        {
            var model = new MaterialRequestsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_MaterialRequests_Create, AppPermissions.Pages_MaterialRequests_Edit)]
        public async Task<ActionResult> CreateOrEdit(Guid? id)
        {
            GetMaterialRequestForEditOutput getMaterialRequestForEditOutput;

            if (id.HasValue)
            {
                getMaterialRequestForEditOutput = await _materialRequestsAppService.GetMaterialRequestForEdit(new EntityDto<Guid> { Id = (Guid)id });
            }
            else
            {
                getMaterialRequestForEditOutput = new GetMaterialRequestForEditOutput
                {
                    MaterialRequest = new CreateOrEditMaterialRequestDto()
                };
            }

            var viewModel = new CreateOrEditMaterialRequestViewModel()
            {
                MaterialRequest = getMaterialRequestForEditOutput.MaterialRequest,
                UNSPSCDisplayProperty = getMaterialRequestForEditOutput.UNSPSCDisplayProperty,
                GeneralLedgerMappingDisplayProperty = getMaterialRequestForEditOutput.GeneralLedgerMappingDisplayProperty,
            };

            return View(viewModel);
        }

        public async Task<ActionResult> ViewMaterialRequest(Guid id)
        {
            var getMaterialRequestForViewDto = await _materialRequestsAppService.GetMaterialRequestForView(id);

            var model = new MaterialRequestViewModel()
            {
                MaterialRequest = getMaterialRequestForViewDto.MaterialRequest
                ,
                UNSPSCDisplayProperty = getMaterialRequestForViewDto.UNSPSCDisplayProperty

                ,
                GeneralLedgerMappingDisplayProperty = getMaterialRequestForViewDto.GeneralLedgerMappingDisplayProperty

            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_MaterialRequests_Create, AppPermissions.Pages_MaterialRequests_Edit)]
        public PartialViewResult UNSPSCLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new MaterialRequestUNSPSCLookupTableViewModel()
            {
                Id = id.ToString(),
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_MaterialRequestUNSPSCLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_MaterialRequests_Create, AppPermissions.Pages_MaterialRequests_Edit)]
        public PartialViewResult GeneralLedgerMappingLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new MaterialRequestGeneralLedgerMappingLookupTableViewModel()
            {
                Id = id.ToString(),
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_MaterialRequestGeneralLedgerMappingLookupTableModal", viewModel);
        }

        public FileUploadCacheOutput UploadPictureFile()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count == 0)
                {
                    throw new UserFriendlyException(L("NoFileFoundError"));
                }

                var file = Request.Form.Files.First();
                if (file.Length > MaxPictureLength)
                {
                    throw new UserFriendlyException(L("Warn_File_SizeLimit", MaxPictureLengthUserFriendlyValue));
                }

                var fileType = Path.GetExtension(file.FileName).Substring(1);
                if (PictureAllowedFileTypes != null && PictureAllowedFileTypes.Length > 0 && !PictureAllowedFileTypes.Contains(fileType))
                {
                    throw new UserFriendlyException(L("FileNotInAllowedFileTypes", PictureAllowedFileTypes));
                }

                byte[] fileBytes;
                using (var stream = file.OpenReadStream())
                {
                    fileBytes = stream.GetAllBytes();
                }

                var fileToken = Guid.NewGuid().ToString("N");
                _tempFileCacheManager.SetFile(fileToken, new TempFileInfo(file.FileName, fileType, fileBytes));

                return new FileUploadCacheOutput(fileToken);
            }
            catch (UserFriendlyException ex)
            {
                return new FileUploadCacheOutput(new ErrorInfo(ex.Message));
            }
        }

    }
}