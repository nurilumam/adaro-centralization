using Adaro.Centralize.MasterData.Dtos;

using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.MaterialGroups
{
    public class CreateOrEditMaterialGroupModalViewModel
    {
        public CreateOrEditMaterialGroupDto MaterialGroup { get; set; }

        public bool IsEditMode => MaterialGroup.Id.HasValue;
    }
}