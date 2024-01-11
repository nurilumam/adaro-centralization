using Adaro.Centralize.MasterData.Dtos;

using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.GeneralLedgerMappings
{
    public class CreateOrEditGeneralLedgerMappingModalViewModel
    {
        public CreateOrEditGeneralLedgerMappingDto GeneralLedgerMapping { get; set; }

        public bool IsEditMode => GeneralLedgerMapping.Id.HasValue;
    }
}