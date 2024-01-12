using Adaro.Centralize.MasterData.Dtos;

using Abp.Extensions;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.EnumTables
{
    public class CreateOrEditEnumTableModalViewModel
    {
        public CreateOrEditEnumTableDto EnumTable { get; set; }

        public bool IsEditMode => EnumTable.Id.HasValue;
    }
}