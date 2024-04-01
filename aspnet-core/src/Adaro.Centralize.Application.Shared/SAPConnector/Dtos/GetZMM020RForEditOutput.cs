using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class GetZMM020RForEditOutput
    {
        public CreateOrEditZMM020RDto ZMM020R { get; set; }

    }
}