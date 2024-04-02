using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Adaro.Centralize.JobScheduler.Exporting;
using Adaro.Centralize.JobScheduler.Dtos;
using Adaro.Centralize.Dto;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Authorization;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.BackgroundJobs;
using Adaro.Centralize.SAPConnector;
using AdaroConnect.Application.Core.Models;
using Adaro.Centralize.SAPConnector.Dtos;

namespace Adaro.Centralize.JobScheduler
{
    [AbpAuthorize(AppPermissions.Pages_JobSynchronizes)]
    public class JobSynchronizesAppService : CentralizeAppServiceBase, IJobSynchronizesAppService
    {
        private readonly IRepository<JobSynchronize, Guid> _jobSynchronizeRepository;
        private readonly IJobSynchronizesExcelExporter _jobSynchronizesExcelExporter;
        private readonly IBackgroundJobManager _backgroundJobManager;

        public JobSynchronizesAppService(
            IRepository<JobSynchronize, Guid> jobSynchronizeRepository, 
            IJobSynchronizesExcelExporter jobSynchronizesExcelExporter,
            IBackgroundJobManager backgroundJobManager)
        {
            _jobSynchronizeRepository = jobSynchronizeRepository;
            _jobSynchronizesExcelExporter = jobSynchronizesExcelExporter;
            _backgroundJobManager = backgroundJobManager;
        }

        public virtual async Task<PagedResultDto<GetJobSynchronizeForViewDto>> GetAll(GetAllJobSynchronizesInput input)
        {
            var jobTypeFilter = input.JobTypeFilter.HasValue
                        ? (JobSchedulerType)input.JobTypeFilter
                        : default;
            var lastStatusFilter = input.LastStatusFilter.HasValue
                ? (JobSchedulerStatus)input.LastStatusFilter
                : default;

            var filteredJobSynchronizes = _jobSynchronizeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.JobName.Contains(input.Filter) || e.DataSource.Contains(input.Filter) || e.Uri.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.JobNameFilter), e => e.JobName.Contains(input.JobNameFilter))
                        .WhereIf(input.JobTypeFilter.HasValue && input.JobTypeFilter > -1, e => e.JobType == jobTypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DataSourceFilter), e => e.DataSource.Contains(input.DataSourceFilter))
                        .WhereIf(input.LastStatusFilter.HasValue && input.LastStatusFilter > -1, e => e.LastStatus == lastStatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UriFilter), e => e.Uri.Contains(input.UriFilter))
                        .WhereIf(input.MinLastUpdateFilter != null, e => e.LastUpdate >= input.MinLastUpdateFilter)
                        .WhereIf(input.MaxLastUpdateFilter != null, e => e.LastUpdate <= input.MaxLastUpdateFilter)
                        .WhereIf(input.MinTotalInsertFilter != null, e => e.TotalInsert >= input.MinTotalInsertFilter)
                        .WhereIf(input.MaxTotalInsertFilter != null, e => e.TotalInsert <= input.MaxTotalInsertFilter)
                        .WhereIf(input.MinTotalUpdateFilter != null, e => e.TotalUpdate >= input.MinTotalUpdateFilter)
                        .WhereIf(input.MaxTotalUpdateFilter != null, e => e.TotalUpdate <= input.MaxTotalUpdateFilter)
                        .WhereIf(input.MinTotalDeleteFilter != null, e => e.TotalDelete >= input.MinTotalDeleteFilter)
                        .WhereIf(input.MaxTotalDeleteFilter != null, e => e.TotalDelete <= input.MaxTotalDeleteFilter);

            var pagedAndFilteredJobSynchronizes = filteredJobSynchronizes
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var jobSynchronizes = from o in pagedAndFilteredJobSynchronizes
                                  select new
                                  {

                                      o.JobName,
                                      o.JobType,
                                      o.DataSource,
                                      o.LastStatus,
                                      o.LastUpdate,
                                      Id = o.Id
                                  };

            var totalCount = await filteredJobSynchronizes.CountAsync();

            var dbList = await jobSynchronizes.ToListAsync();
            var results = new List<GetJobSynchronizeForViewDto>();

            //await _backgroundJobManager.EnqueueAsync<SAPConnector.JobScheduler.CostCenterJob, ImportCostCenterArgs>(
            //                new ImportCostCenterArgs()
            //                {
            //                    Year = 2024
            //                }
            //            );

            foreach (var o in dbList)
            {
                var res = new GetJobSynchronizeForViewDto()
                {
                    JobSynchronize = new JobSynchronizeDto
                    {

                        JobName = o.JobName,
                        JobType = o.JobType,
                        DataSource = o.DataSource,
                        LastStatus = o.LastStatus,
                        LastUpdate = o.LastUpdate,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetJobSynchronizeForViewDto>(
                totalCount,
                results
            );

        }


        [AbpAuthorize(AppPermissions.Pages_JobSynchronizes_Edit)]
        public virtual async Task<GetJobSynchronizeForEditOutput> GetJobSynchronizeForEdit(EntityDto<Guid> input)
        {
            var jobSynchronize = await _jobSynchronizeRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetJobSynchronizeForEditOutput { JobSynchronize = ObjectMapper.Map<CreateOrEditJobSynchronizeDto>(jobSynchronize) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditJobSynchronizeDto input)
        {
            if (input.Id == null)
            {
                await Create(input);
            }
            else
            {
                await Update(input);
            }
        }

        [AbpAuthorize(AppPermissions.Pages_JobSynchronizes_Create)]
        protected virtual async Task Create(CreateOrEditJobSynchronizeDto input)
        {
            var jobSynchronize = ObjectMapper.Map<JobSynchronize>(input);

            if (AbpSession.TenantId != null)
            {
                jobSynchronize.TenantId = (int?)AbpSession.TenantId;
            }

            await _jobSynchronizeRepository.InsertAsync(jobSynchronize);

        }

        [AbpAuthorize(AppPermissions.Pages_JobSynchronizes_Edit)]
        protected virtual async Task Update(CreateOrEditJobSynchronizeDto input)
        {
            var jobSynchronize = await _jobSynchronizeRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, jobSynchronize);


            //_SAPSynchService.PurchasingDocumentHeader();

        }

        [AbpAuthorize(AppPermissions.Pages_JobSynchronizes_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _jobSynchronizeRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetJobSynchronizesToExcel(GetAllJobSynchronizesForExcelInput input)
        {
            var jobTypeFilter = input.JobTypeFilter.HasValue
                        ? (JobSchedulerType)input.JobTypeFilter
                        : default;
            var lastStatusFilter = input.LastStatusFilter.HasValue
                ? (JobSchedulerStatus)input.LastStatusFilter
                : default;

            var filteredJobSynchronizes = _jobSynchronizeRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.JobName.Contains(input.Filter) || e.DataSource.Contains(input.Filter) || e.Uri.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.JobNameFilter), e => e.JobName.Contains(input.JobNameFilter))
                        .WhereIf(input.JobTypeFilter.HasValue && input.JobTypeFilter > -1, e => e.JobType == jobTypeFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.DataSourceFilter), e => e.DataSource.Contains(input.DataSourceFilter))
                        .WhereIf(input.LastStatusFilter.HasValue && input.LastStatusFilter > -1, e => e.LastStatus == lastStatusFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UriFilter), e => e.Uri.Contains(input.UriFilter))
                        .WhereIf(input.MinLastUpdateFilter != null, e => e.LastUpdate >= input.MinLastUpdateFilter)
                        .WhereIf(input.MaxLastUpdateFilter != null, e => e.LastUpdate <= input.MaxLastUpdateFilter)
                        .WhereIf(input.MinTotalInsertFilter != null, e => e.TotalInsert >= input.MinTotalInsertFilter)
                        .WhereIf(input.MaxTotalInsertFilter != null, e => e.TotalInsert <= input.MaxTotalInsertFilter)
                        .WhereIf(input.MinTotalUpdateFilter != null, e => e.TotalUpdate >= input.MinTotalUpdateFilter)
                        .WhereIf(input.MaxTotalUpdateFilter != null, e => e.TotalUpdate <= input.MaxTotalUpdateFilter)
                        .WhereIf(input.MinTotalDeleteFilter != null, e => e.TotalDelete >= input.MinTotalDeleteFilter)
                        .WhereIf(input.MaxTotalDeleteFilter != null, e => e.TotalDelete <= input.MaxTotalDeleteFilter);

            var query = (from o in filteredJobSynchronizes
                         select new GetJobSynchronizeForViewDto()
                         {
                             JobSynchronize = new JobSynchronizeDto
                             {
                                 JobName = o.JobName,
                                 JobType = o.JobType,
                                 DataSource = o.DataSource,
                                 LastStatus = o.LastStatus,
                                 LastUpdate = o.LastUpdate,
                                 Id = o.Id
                             }
                         });

            var jobSynchronizeListDtos = await query.ToListAsync();

            return _jobSynchronizesExcelExporter.ExportToFile(jobSynchronizeListDtos);
        }

        //public virtual async Task<GetJobSynchronizeForViewDto> EnqueueJob(Guid id)
        //{
        //    var jobSynchronize = await _jobSynchronizeRepository.GetAsync(id);

        //    var output = new GetJobSynchronizeForViewDto { JobSynchronize = ObjectMapper.Map<JobSynchronizeDto>(jobSynchronize) };

        //    return output;
        //}
    }
}