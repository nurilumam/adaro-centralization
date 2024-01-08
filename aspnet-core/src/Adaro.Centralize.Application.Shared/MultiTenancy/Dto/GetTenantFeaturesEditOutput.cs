using System.Collections.Generic;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Editions.Dto;

namespace Adaro.Centralize.MultiTenancy.Dto
{
    public class GetTenantFeaturesEditOutput
    {
        public List<NameValueDto> FeatureValues { get; set; }

        public List<FlatFeatureDto> Features { get; set; }
    }
}