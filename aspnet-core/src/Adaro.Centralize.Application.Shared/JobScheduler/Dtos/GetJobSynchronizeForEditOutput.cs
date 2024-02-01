using System;
using Abp.Application.Services.Dto;
using System.ComponentModel.DataAnnotations;

namespace Adaro.Centralize.JobScheduler.Dtos
{
    public class GetJobSynchronizeForEditOutput
    {
        public CreateOrEditJobSynchronizeDto JobSynchronize { get; set; }

    }
}