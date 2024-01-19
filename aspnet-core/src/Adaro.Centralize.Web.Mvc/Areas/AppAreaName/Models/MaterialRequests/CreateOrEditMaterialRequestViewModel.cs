using Adaro.Centralize.MasterDataRequest.Dtos;

using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.MaterialRequests
{
    public class CreateOrEditMaterialRequestViewModel
    {
        public CreateOrEditMaterialRequestDto MaterialRequest { get; set; }

        public string UNSPSCDisplayProperty { get; set; }

        public string GeneralLedgerMappingDisplayProperty { get; set; }

        public string PictureFileName { get; set; }
        public string PictureFileAcceptedTypes { get; set; }

        public bool IsEditMode => MaterialRequest.Id.HasValue;
    }
}