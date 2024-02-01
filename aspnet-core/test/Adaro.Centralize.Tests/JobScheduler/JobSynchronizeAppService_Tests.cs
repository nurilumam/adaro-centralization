using System;
using System.Linq;
using System.Threading.Tasks;
using Adaro.Centralize.JobScheduler.Dtos;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Adaro.Centralize.JobScheduler;
using Shouldly;
using Xunit;
using Abp.Timing;

namespace Adaro.Centralize.Tests.JobScheduler
{
    public class JobSynchronizesAppService_Tests : AppTestBase
    {
        private readonly IJobSynchronizesAppService _jobSynchronizesAppService;

        private readonly Guid _jobSynchronizeTestId;

        public JobSynchronizesAppService_Tests()
        {
            _jobSynchronizesAppService = Resolve<IJobSynchronizesAppService>();
            _jobSynchronizeTestId = Guid.NewGuid();
            SeedTestData();
        }

        private void SeedTestData()
        {
            var currentTenant = GetCurrentTenant();

            var jobSynchronize = new JobSynchronize
            {
                JobName = "Test value",
                JobType = 0,
                DataSource = "Test value",
                LastStatus = 0,
                LastUpdate = DateTime.Now,
                Id = _jobSynchronizeTestId,
                TenantId = currentTenant.Id
            };

            UsingDbContext(context =>
            {
                context.JobSynchronizes.Add(jobSynchronize);
            });
        }

        [Fact]
        public async Task Should_Get_All_JobSynchronizes()
        {
            var jobSynchronizes = await _jobSynchronizesAppService.GetAll(new GetAllJobSynchronizesInput());

            jobSynchronizes.TotalCount.ShouldBe(1);
            jobSynchronizes.Items.Count.ShouldBe(1);

            jobSynchronizes.Items.Any(x => x.JobSynchronize.Id == _jobSynchronizeTestId).ShouldBe(true);
        }

        [Fact]
        public async Task Should_Get_JobSynchronize_For_View()
        {
            var jobSynchronize = await _jobSynchronizesAppService.GetJobSynchronizeForView(_jobSynchronizeTestId);

            jobSynchronize.ShouldNotBe(null);
        }

        [Fact]
        public async Task Should_Get_JobSynchronize_For_Edit()
        {
            var jobSynchronize = await _jobSynchronizesAppService.GetJobSynchronizeForEdit(new EntityDto<Guid> { Id = _jobSynchronizeTestId });

            jobSynchronize.ShouldNotBe(null);
        }

        [Fact]
        protected virtual async Task Should_Create_JobSynchronize()
        {
            var jobSynchronize = new CreateOrEditJobSynchronizeDto
            {
                JobName = "Test value",
                JobType = 0,
                DataSource = "Test value",
                LastStatus = 0,
                LastUpdate = DateTime.Now,
                Id = _jobSynchronizeTestId
            };

            await _jobSynchronizesAppService.CreateOrEdit(jobSynchronize);

            await UsingDbContextAsync(async context =>
            {
                var entity = await context.JobSynchronizes.FirstOrDefaultAsync(e => e.Id == _jobSynchronizeTestId);
                entity.ShouldNotBe(null);
            });
        }

        [Fact]
        protected virtual async Task Should_Update_JobSynchronize()
        {
            var jobSynchronize = new CreateOrEditJobSynchronizeDto
            {
                JobName = "Updated test value",
                JobType = 0,
                DataSource = "Updated test value",
                LastStatus = 0,
                LastUpdate = Clock.Now.AddDays(3).Date,
                Id = _jobSynchronizeTestId
            };

            await _jobSynchronizesAppService.CreateOrEdit(jobSynchronize);

            await UsingDbContextAsync(async context =>
            {
                var entity = await context.JobSynchronizes.FirstOrDefaultAsync(e => e.Id == jobSynchronize.Id);
                entity.ShouldNotBeNull();

                entity.JobName.ShouldBe("Updated test value");
                entity.JobType.ShouldBe((JobSchedulerType)0);
                entity.DataSource.ShouldBe("Updated test value");
                entity.LastStatus.ShouldBe((JobSchedulerStatus)0);
                entity.LastUpdate.ShouldBe(Clock.Now.AddDays(3).Date);
            });
        }

        [Fact]
        public async Task Should_Delete_JobSynchronize()
        {
            await _jobSynchronizesAppService.Delete(new EntityDto<Guid> { Id = _jobSynchronizeTestId });

            await UsingDbContextAsync(async context =>
            {
                var entity = await context.JobSynchronizes.FirstOrDefaultAsync(e => e.Id == _jobSynchronizeTestId);
                entity.ShouldBeNull();
            });
        }
    }
}