using Adaro.Centralize.MasterDataRequest;

using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.MasterDataRequest.Dtos
{
    public class CreateOrEditMaterialRequestDto : EntityDto<Guid?>
    {

        [Required]
        public string RequestNo { get; set; }

        public MaterialRequestStatus RequestStatus { get; set; }

        [Required]
        public string MaterialName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Purpose { get; set; }

        public string MaterialType { get; set; }

        public string Category { get; set; }

        public string Size { get; set; }

        public string UoM { get; set; }

        public string PackageSize { get; set; }

        [Required]
        public string GeneralLedger { get; set; }

        public string Brand { get; set; }

        public string Weight { get; set; }

        public Guid? Picture { get; set; }

        public string PictureToken { get; set; }

        public string ImageColletion { get; set; }

        public Guid? UNSPSCId { get; set; }

        public Guid? ValuationClassId { get; set; }

    }
}