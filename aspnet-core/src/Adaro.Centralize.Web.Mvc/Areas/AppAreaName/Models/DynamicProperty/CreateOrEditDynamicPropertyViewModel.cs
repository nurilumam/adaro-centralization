using System.Collections.Generic;
using Adaro.Centralize.DynamicEntityProperties.Dto;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.DynamicProperty
{
    public class CreateOrEditDynamicPropertyViewModel
    {
        public DynamicPropertyDto DynamicPropertyDto { get; set; }

        public List<string> AllowedInputTypes { get; set; }
    }
}
