using System.Collections.Generic;
using Adaro.Centralize.JobScheduler.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.JobScheduler.Exporting
{
    public interface IJobSynchronizesExcelExporter
    {
        FileDto ExportToFile(List<GetJobSynchronizeForViewDto> jobSynchronizes);
    }
}