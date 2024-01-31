using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetDataProductionForEditOutput
    {
        public CreateOrEditDataProductionDto DataProduction { get; set; }

    }
}