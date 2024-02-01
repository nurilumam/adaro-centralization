using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.JobScheduler.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.JobScheduler
{
    public interface IJobSynchronizesAppService : IApplicationService
    {
        Task<PagedResultDto<GetJobSynchronizeForViewDto>> GetAll(GetAllJobSynchronizesInput input);

        Task<GetJobSynchronizeForViewDto> GetJobSynchronizeForView(Guid id);

        Task<GetJobSynchronizeForEditOutput> GetJobSynchronizeForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditJobSynchronizeDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetJobSynchronizesToExcel(GetAllJobSynchronizesForExcelInput input);

        //Task<GetJobSynchronizeForViewDto> EnqueueJob(Guid id);

    }
}