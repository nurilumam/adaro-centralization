using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Common;

namespace Adaro.Centralize.SAPConnector
{
    public interface ICostCentersAppService : IApplicationService
    {
        Task<PagedResultDto<GetCostCenterForViewDto>> GetAll(GetAllCostCentersInput input);

        Task<GetCostCenterForViewDto> GetCostCenterForView(Guid id);

        Task<GetCostCenterForEditOutput> GetCostCenterForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditCostCenterDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetCostCentersToExcel(GetAllCostCentersForExcelInput input);

        Task<DtoResponseModel> GetFromSAP();

    }
}