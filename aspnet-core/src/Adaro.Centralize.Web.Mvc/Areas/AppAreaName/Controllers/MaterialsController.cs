using System;
using System.Threading.Tasks;
using Abp.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Materials;
using Adaro.Centralize.Web.Controllers;
using Adaro.Centralize.Authorization;
using Adaro.Centralize.MasterData;
using Adaro.Centralize.MasterData.Dtos;
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
    [AbpMvcAuthorize(AppPermissions.Pages_Materials)]
    public class MaterialsController : CentralizeControllerBase
    {
        private readonly IMaterialsAppService _materialsAppService;
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const long MaxImageMainLength = 5242880; //5MB
        private const string MaxImageMainLengthUserFriendlyValue = "5MB"; //5MB
        private readonly string[] ImageMainAllowedFileTypes = { "jpeg", "jpg", "png" };

        public MaterialsController(IMaterialsAppService materialsAppService, ITempFileCacheManager tempFileCacheManager)
        {
            _materialsAppService = materialsAppService;
            _tempFileCacheManager = tempFileCacheManager;
        }

        public ActionResult Index()
        {
            var model = new MaterialsViewModel
            {
                FilterText = ""
            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Materials_Create, AppPermissions.Pages_Materials_Edit)]
        public async Task<ActionResult> CreateOrEdit(int? id)
        {
            GetMaterialForEditOutput getMaterialForEditOutput;

            if (id.HasValue)
            {
                getMaterialForEditOutput = await _materialsAppService.GetMaterialForEdit(new EntityDto { Id = (int)id });
            }
            else
            {
                getMaterialForEditOutput = new GetMaterialForEditOutput
                {
                    Material = new CreateOrEditMaterialDto()
                };
            }

            var viewModel = new CreateOrEditMaterialViewModel()
            {
                Material = getMaterialForEditOutput.Material,
                MaterialGroupDisplayProperty = getMaterialForEditOutput.MaterialGroupDisplayProperty,
                UNSPSCDisplayProperty = getMaterialForEditOutput.UNSPSCDisplayProperty,
                GeneralLedgerMappingDisplayProperty = getMaterialForEditOutput.GeneralLedgerMappingDisplayProperty,
            };

            return View(viewModel);
        }

        public async Task<ActionResult> ViewMaterial(int id)
        {
            var getMaterialForViewDto = await _materialsAppService.GetMaterialForView(id);

            var model = new MaterialViewModel()
            {
                Material = getMaterialForViewDto.Material
                ,
                MaterialGroupDisplayProperty = getMaterialForViewDto.MaterialGroupDisplayProperty

                ,
                UNSPSCDisplayProperty = getMaterialForViewDto.UNSPSCDisplayProperty

                ,
                GeneralLedgerMappingDisplayProperty = getMaterialForViewDto.GeneralLedgerMappingDisplayProperty

            };

            return View(model);
        }

        [AbpMvcAuthorize(AppPermissions.Pages_Materials_Create, AppPermissions.Pages_Materials_Edit)]
        public PartialViewResult MaterialGroupLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new MaterialMaterialGroupLookupTableViewModel()
            {
                Id = id.ToString(),
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_MaterialMaterialGroupLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Materials_Create, AppPermissions.Pages_Materials_Edit)]
        public PartialViewResult UNSPSCLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new MaterialUNSPSCLookupTableViewModel()
            {
                Id = id.ToString(),
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_MaterialUNSPSCLookupTableModal", viewModel);
        }
        [AbpMvcAuthorize(AppPermissions.Pages_Materials_Create, AppPermissions.Pages_Materials_Edit)]
        public PartialViewResult GeneralLedgerMappingLookupTableModal(Guid? id, string displayName)
        {
            var viewModel = new MaterialGeneralLedgerMappingLookupTableViewModel()
            {
                Id = id.ToString(),
                DisplayName = displayName,
                FilterText = ""
            };

            return PartialView("_MaterialGeneralLedgerMappingLookupTableModal", viewModel);
        }

        public FileUploadCacheOutput UploadImageMainFile()
        {
            try
            {
                //Check input
                if (Request.Form.Files.Count == 0)
                {
                    throw new UserFriendlyException(L("NoFileFoundError"));
                }

                var file = Request.Form.Files.First();
                if (file.Length > MaxImageMainLength)
                {
                    throw new UserFriendlyException(L("Warn_File_SizeLimit", MaxImageMainLengthUserFriendlyValue));
                }

                var fileType = Path.GetExtension(file.FileName).Substring(1);
                if (ImageMainAllowedFileTypes != null && ImageMainAllowedFileTypes.Length > 0 && !ImageMainAllowedFileTypes.Contains(fileType))
                {
                    throw new UserFriendlyException(L("FileNotInAllowedFileTypes", ImageMainAllowedFileTypes));
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