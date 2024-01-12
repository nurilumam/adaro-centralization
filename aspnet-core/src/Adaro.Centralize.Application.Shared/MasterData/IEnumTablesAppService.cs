using System;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;

namespace Adaro.Centralize.MasterData
{
    public interface IEnumTablesAppService : IApplicationService
    {
        Task<PagedResultDto<GetEnumTableForViewDto>> GetAll(GetAllEnumTablesInput input);

        Task<GetEnumTableForViewDto> GetEnumTableForView(Guid id);

        Task<GetEnumTableForEditOutput> GetEnumTableForEdit(EntityDto<Guid> input);

        Task CreateOrEdit(CreateOrEditEnumTableDto input);

        Task Delete(EntityDto<Guid> input);

        Task<FileDto> GetEnumTablesToExcel(GetAllEnumTablesForExcelInput input);

    }
}