﻿using System;
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
    public class MaterialsController : CentralizeControllerBase
    {
        private readonly ITempFileCacheManager _tempFileCacheManager;

        private const long MaxImageMainLength = 5242880; //5MB
        private const string MaxImageMainLengthUserFriendlyValue = "5MB"; //5MB
        private readonly string[] ImageMainAllowedFileTypes = { "jpeg", "jpg", "png" };

        public MaterialsController(ITempFileCacheManager tempFileCacheManager)
        {
            _tempFileCacheManager = tempFileCacheManager;
            //var hasFileProp = True;
            //var projetcType = Angular;
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

        public string[] GetImageMainFileAllowedTypes()
        {
            return ImageMainAllowedFileTypes;
        }

    }
}