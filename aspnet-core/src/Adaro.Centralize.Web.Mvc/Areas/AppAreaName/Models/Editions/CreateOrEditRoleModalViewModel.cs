using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Adaro.Centralize.Editions.Dto;
using Adaro.Centralize.Web.Areas.AppAreaName.Models.Common;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Editions
{
    [AutoMapFrom(typeof(GetEditionEditOutput))]
    public class CreateEditionModalViewModel : GetEditionEditOutput, IFeatureEditViewModel
    {
        public IReadOnlyList<ComboboxItemDto> EditionItems { get; set; }

        public IReadOnlyList<ComboboxItemDto> FreeEditionItems { get; set; }
    }
}