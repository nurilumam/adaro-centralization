using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.SAPConnector.Dtos
{
    public class CreateOrEditCostCenterDto : EntityDto<Guid?>
    {

        [Required]
        public string ControllingArea { get; set; }

        [Required]
        public string CostCenterName { get; set; }

        public string Description { get; set; }

        public string ActState { get; set; }

        public bool IsActive { get; set; }

    }
}