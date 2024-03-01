using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.LookupArea.Dtos
{
    public class CreateOrEditLookupPageDto : EntityDto<Guid?>
    {

        [Required]
        public string LookupName { get; set; }

        public Guid? CostCenterId { get; set; }

    }
}