using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.LookupArea.Dtos
{
    public class GetLookupPageForEditOutput
    {
        public CreateOrEditLookupPageDto LookupPage { get; set; }

        public string CostCenterDisplayProperty { get; set; }

    }
}