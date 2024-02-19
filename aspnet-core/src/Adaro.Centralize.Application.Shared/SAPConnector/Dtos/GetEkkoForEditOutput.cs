using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetEkkoForEditOutput
    {
        public CreateOrEditEkkoDto Ekko { get; set; }

    }
}