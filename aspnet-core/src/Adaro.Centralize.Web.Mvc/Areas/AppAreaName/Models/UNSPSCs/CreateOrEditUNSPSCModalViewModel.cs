using Adaro.Centralize.MasterData.Dtos;

using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.UNSPSCs
{
    public class CreateOrEditUNSPSCModalViewModel
    {
        public CreateOrEditUNSPSCDto UNSPSC { get; set; }

        public bool IsEditMode => UNSPSC.Id.HasValue;
    }
}