﻿using Abp.Application.Services.Dto;

namespace Adaro.Centralize.JobScheduler.Dtos
{
    public class GetAllForLookupTableInput : PagedAndSortedResultRequestDto
    {
        public string Filter { get; set; }
    }
}