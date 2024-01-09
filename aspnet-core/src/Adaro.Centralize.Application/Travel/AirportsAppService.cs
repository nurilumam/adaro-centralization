using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Adaro.Centralize.Travel.Exporting;
using Adaro.Centralize.Travel.Dtos;
using Adaro.Centralize.Dto;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.Travel
{
    [AbpAuthorize(AppPermissions.Pages_Airports)]
    public class AirportsAppService : CentralizeAppServiceBase, IAirportsAppService
    {
        private readonly IRepository<Airport, Guid> _airportRepository;
        private readonly IAirportsExcelExporter _airportsExcelExporter;

        public AirportsAppService(IRepository<Airport, Guid> airportRepository, IAirportsExcelExporter airportsExcelExporter)
        {
            _airportRepository = airportRepository;
            _airportsExcelExporter = airportsExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetAirportForViewDto>> GetAll(GetAllAirportsInput input)
        {

            var filteredAirports = _airportRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.AirportName.Contains(input.Filter) || e.IATA.Contains(input.Filter) || e.City.Contains(input.Filter) || e.Category.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AirportNameFilter), e => e.AirportName.Contains(input.AirportNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IATAFilter), e => e.IATA.Contains(input.IATAFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CityFilter), e => e.City.Contains(input.CityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CategoryFilter), e => e.Category.Contains(input.CategoryFilter));

            var pagedAndFilteredAirports = filteredAirports
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var airports = from o in pagedAndFilteredAirports
                           select new
                           {

                               o.AirportName,
                               o.IATA,
                               o.City,
                               o.Category,
                               Id = o.Id
                           };

            var totalCount = await filteredAirports.CountAsync();

            var dbList = await airports.ToListAsync();
            var results = new List<GetAirportForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetAirportForViewDto()
                {
                    Airport = new AirportDto
                    {

                        AirportName = o.AirportName,
                        IATA = o.IATA,
                        City = o.City,
                        Category = o.Category,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetAirportForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetAirportForViewDto> GetAirportForView(Guid id)
        {
            var airport = await _airportRepository.GetAsync(id);

            var output = new GetAirportForViewDto { Airport = ObjectMapper.Map<AirportDto>(airport) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Airports_Edit)]
        public virtual async Task<GetAirportForEditOutput> GetAirportForEdit(EntityDto<Guid> input)
        {
            var airport = await _airportRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetAirportForEditOutput { Airport = ObjectMapper.Map<CreateOrEditAirportDto>(airport) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditAirportDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Airports_Create)]
        protected virtual async Task Create(CreateOrEditAirportDto input)
        {
            var airport = ObjectMapper.Map<Airport>(input);

            if (AbpSession.TenantId != null)
            {
                airport.TenantId = (int?)AbpSession.TenantId;
            }

            await _airportRepository.InsertAsync(airport);

        }

        [AbpAuthorize(AppPermissions.Pages_Airports_Edit)]
        protected virtual async Task Update(CreateOrEditAirportDto input)
        {
            var airport = await _airportRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, airport);

        }

        [AbpAuthorize(AppPermissions.Pages_Airports_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _airportRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetAirportsToExcel(GetAllAirportsForExcelInput input)
        {

            var filteredAirports = _airportRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.AirportName.Contains(input.Filter) || e.IATA.Contains(input.Filter) || e.City.Contains(input.Filter) || e.Category.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AirportNameFilter), e => e.AirportName.Contains(input.AirportNameFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IATAFilter), e => e.IATA.Contains(input.IATAFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CityFilter), e => e.City.Contains(input.CityFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CategoryFilter), e => e.Category.Contains(input.CategoryFilter));

            var query = (from o in filteredAirports
                         select new GetAirportForViewDto()
                         {
                             Airport = new AirportDto
                             {
                                 AirportName = o.AirportName,
                                 IATA = o.IATA,
                                 City = o.City,
                                 Category = o.Category,
                                 Id = o.Id
                             }
                         });

            var airportListDtos = await query.ToListAsync();

            return _airportsExcelExporter.ExportToFile(airportListDtos);
        }

    }
}