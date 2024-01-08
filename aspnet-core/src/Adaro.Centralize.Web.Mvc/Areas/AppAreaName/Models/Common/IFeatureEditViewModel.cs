using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Editions.Dto;

namespace Adaro.Centralize.Web.Areas.AppAreaName.Models.Common
{
    public interface IFeatureEditViewModel
    {
        List<NameValueDto> FeatureValues { get; set; }

        List<FlatFeatureDto> Features { get; set; }
    }
}