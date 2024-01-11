using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.MasterData.Dtos
{
    public class CreateOrEditMaterialDto : EntityDto<int?>
    {

        [Required]
        public string MaterialNo { get; set; }

        [Required]
        public string MaterialName { get; set; }

        public string MaterialNameSAP { get; set; }

        [Required]
        public string Description { get; set; }

        public string Size { get; set; }

        [Required]
        public string UoM { get; set; }

        public string Brand { get; set; }

        public Guid? ImageMain { get; set; }

        public string ImageMainToken { get; set; }

        public Guid? MaterialGroupId { get; set; }

        public Guid? UNSPSCId { get; set; }

        public Guid? GeneralLedgerMappingId { get; set; }

    }
}