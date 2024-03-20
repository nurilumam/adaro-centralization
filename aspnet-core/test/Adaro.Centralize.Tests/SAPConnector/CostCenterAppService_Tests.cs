using System;
using System.Linq;
using System.Threading.Tasks;
using Adaro.Centralize.SAPConnector.Dtos;
using Abp.Application.Services.Dto;
using Microsoft.EntityFrameworkCore;
using Adaro.Centralize.SAPConnector;
using Shouldly;
using Xunit;
using Abp.Timing;

namespace Adaro.Centralize.Tests.SAPConnector
{
    public class CostCentersAppService_Tests : AppTestBase
    {
        private readonly ICostCentersAppService _costCentersAppService;

        private readonly Guid _costCenterTestId;

        public CostCentersAppService_Tests()
        {
            _costCentersAppService = Resolve<ICostCentersAppService>();
            _costCenterTestId = Guid.NewGuid();
            SeedTestData();
        }

        private void SeedTestData()
        {
            var currentTenant = GetCurrentTenant();

            var costCenter = new CostCenter
            {
                ControllingArea = "Test value",
                CostCenterName = "Test value",
                Description = "Test value",
                IsActive = false,
                CostCenterCode = "Test value",
                DepartmentName = "Test value",
                Period = "Test value",
                Id = _costCenterTestId,
                TenantId = currentTenant.Id
            };

            UsingDbContext(context =>
            {
                context.CostCenters.Add(costCenter);
            });
        }

        [Fact]
        public async Task Should_Get_All_CostCenters()
        {
            var costCenters = await _costCentersAppService.GetAll(new GetAllCostCentersInput());

            costCenters.TotalCount.ShouldBe(1);
            costCenters.Items.Count.ShouldBe(1);

            costCenters.Items.Any(x => x.CostCenter.Id == _costCenterTestId).ShouldBe(true);
        }

        [Fact]
        public async Task Should_Get_CostCenter_For_View()
        {
            var costCenter = await _costCentersAppService.GetCostCenterForView(_costCenterTestId);

            costCenter.ShouldNotBe(null);
        }

        [Fact]
        public async Task Should_Get_CostCenter_For_Edit()
        {
            var costCenter = await _costCentersAppService.GetCostCenterForEdit(new EntityDto<Guid> { Id = _costCenterTestId });

            costCenter.ShouldNotBe(null);
        }

        [Fact]
        protected virtual async Task Should_Create_CostCenter()
        {
            var costCenter = new CreateOrEditCostCenterDto
            {
                ControllingArea = "Test value",
                CostCenterName = "Test value",
                Description = "Test value",
                IsActive = false,
                CostCenterCode = "Test value",
                DepartmentName = "Test value",
                Period = "Test value",
                Id = _costCenterTestId
            };

            await _costCentersAppService.CreateOrEdit(costCenter);

            await UsingDbContextAsync(async context =>
            {
                var entity = await context.CostCenters.FirstOrDefaultAsync(e => e.Id == _costCenterTestId);
                entity.ShouldNotBe(null);
            });
        }

        [Fact]
        protected virtual async Task Should_Update_CostCenter()
        {
            var costCenter = new CreateOrEditCostCenterDto
            {
                ControllingArea = "Updated test value",
                CostCenterName = "Updated test value",
                Description = "Updated test value",
                IsActive = true,
                CostCenterCode = "Updated test value",
                DepartmentName = "Updated test value",
                Period = "Updated test value",
                Id = _costCenterTestId
            };

            await _costCentersAppService.CreateOrEdit(costCenter);

            await UsingDbContextAsync(async context =>
            {
                var entity = await context.CostCenters.FirstOrDefaultAsync(e => e.Id == costCenter.Id);
                entity.ShouldNotBeNull();

                entity.ControllingArea.ShouldBe("Updated test value");
                entity.CostCenterName.ShouldBe("Updated test value");
                entity.Description.ShouldBe("Updated test value");
                entity.IsActive.ShouldBe(true);
                entity.CostCenterCode.ShouldBe("Updated test value");
                entity.DepartmentName.ShouldBe("Updated test value");
                entity.Period.ShouldBe("Updated test value");
            });
        }

        [Fact]
        public async Task Should_Delete_CostCenter()
        {
            await _costCentersAppService.Delete(new EntityDto<Guid> { Id = _costCenterTestId });

            await UsingDbContextAsync(async context =>
            {
                var entity = await context.CostCenters.FirstOrDefaultAsync(e => e.Id == _costCenterTestId);
                entity.ShouldBeNull();
            });
        }
    }
}