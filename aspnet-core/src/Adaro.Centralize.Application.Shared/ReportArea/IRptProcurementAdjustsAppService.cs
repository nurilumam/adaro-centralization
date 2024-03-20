using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.ReportArea.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.ReportArea
{
    public interface IRptProcurementAdjustsAppService : IApplicationService
    {
        Task<PagedResultDto<GetRptProcurementAdjustForViewDto>> GetAll(GetAllRptProcurementAdjustsInput input);

        Task<GetRptProcurementAdjustForViewDto> GetRptProcurementAdjustForView(Guid id);

        Task<GetRptProcurementAdjustForEditOutput> GetRptProcurementAdjustForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditRptProcurementAdjustDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetRptProcurementAdjustsToExcel(GetAllRptProcurementAdjustsForExcelInput input);

    }
}