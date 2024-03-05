using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetZMM021RForEditOutput
    {
        public CreateOrEditZMM021RDto ZMM021R { get; set; }

    }
}