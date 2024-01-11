using Adaro.Centralize.MasterData.Dtos;

using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Materials
{
    public class CreateOrEditMaterialViewModel
    {
        public CreateOrEditMaterialDto Material { get; set; }

        public string MaterialGroupDisplayProperty { get; set; }

        public string UNSPSCDisplayProperty { get; set; }

        public string GeneralLedgerMappingDisplayProperty { get; set; }

        public string ImageMainFileName { get; set; }
        public string ImageMainFileAcceptedTypes { get; set; }

        public bool IsEditMode => Material.Id.HasValue;
    }
}