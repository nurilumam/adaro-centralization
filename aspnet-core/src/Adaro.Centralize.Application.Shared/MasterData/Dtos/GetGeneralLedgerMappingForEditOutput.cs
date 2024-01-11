using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class GetGeneralLedgerMappingForEditOutput
    {
        public CreateOrEditGeneralLedgerMappingDto GeneralLedgerMapping { get; set; }

    }
}