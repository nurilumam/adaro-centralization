using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.MasterData
{
    public interface IGeneralLedgerMappingsAppService : IApplicationService
    {
        Task<PagedResultDto<GetGeneralLedgerMappingForViewDto>> GetAll(GetAllGeneralLedgerMappingsInput input);

        Task<GetGeneralLedgerMappingForViewDto> GetGeneralLedgerMappingForView(Guid id);

        Task<GetGeneralLedgerMappingForEditOutput> GetGeneralLedgerMappingForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditGeneralLedgerMappingDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetGeneralLedgerMappingsToExcel(GetAllGeneralLedgerMappingsForExcelInput input);

    }
}