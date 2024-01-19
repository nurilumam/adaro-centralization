using System;
using System.IO;
using System.Linq;
using Abp.IO.Extensions;
using Abp.UI;
using Abp.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.Web.Controllers
{
    [Authorize]
    public class MaterialRequestsController : CentralizeControllerBase
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const long MaxPictureLength = 5242880; //5MB
        private const string MaxPictureLengthUserFriendlyValue = "5MB"; //5MB
        private readonly string[] PictureAllowedFileTypes = { "jpeg", "jpg", "png" };

        public MaterialRequestsController(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
            //var hasFileProp = True;
            //var projetcType = Angular;
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

        public string[] GetPictureFileAllowedTypes()
        {
            return PictureAllowedFileTypes;
        }

    }
}