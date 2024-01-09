using Adaro.Centralize.Authorization.Users;
using Adaro.Centralize.Travel;
using Adaro.Centralize.Travel;
using Adaro.Centralize.Authorization.Users;

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
    [AbpAuthorize(AppPermissions.Pages_TravelRequests)]
    public class TravelRequestsAppService : CentralizeAppServiceBase, ITravelRequestsAppService
    {
        private readonly IRepository<TravelRequest, Guid> _travelRequestRepository;
        private readonly ITravelRequestsExcelExporter _travelRequestsExcelExporter;
        private readonly IRepository<User, long> _lookup_userRepository;
        private readonly IRepository<Airport, Guid> _lookup_airportRepository;

        public TravelRequestsAppService(IRepository<TravelRequest, Guid> travelRequestRepository, ITravelRequestsExcelExporter travelRequestsExcelExporter, IRepository<User, long> lookup_userRepository, IRepository<Airport, Guid> lookup_airportRepository)
        {
            _travelRequestRepository = travelRequestRepository;
            _travelRequestsExcelExporter = travelRequestsExcelExporter;
            _lookup_userRepository = lookup_userRepository;
            _lookup_airportRepository = lookup_airportRepository;

        }

        public virtual async Task<PagedResultDto<GetTravelRequestForViewDto>> GetAll(GetAllTravelRequestsInput input)
        {
            var travelStatusFilter = input.TravelStatusFilter.HasValue
                        ? (TravelStatus)input.TravelStatusFilter
                        : default;
            var travelTypeFilter = input.TravelTypeFilter.HasValue
                ? (TravelType)input.TravelTypeFilter
                : default;

            var filteredTravelRequests = _travelRequestRepository.GetAll()
                        .Include(e => e.UserTravelFk)
                        .Include(e => e.AirportFromFk)
                        .Include(e => e.AirportToFk)
                        .Include(e => e.CreatedByFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.RequestNo.Contains(input.Filter) || e.Camp.Contains(input.Filter) || e.TransportBus.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RequestNoFilter), e => e.RequestNo.Contains(input.RequestNoFilter))
                        .WhereIf(input.TravelStatusFilter.HasValue && input.TravelStatusFilter > -1, e => e.TravelStatus == travelStatusFilter)
                        .WhereIf(input.TravelTypeFilter.HasValue && input.TravelTypeFilter > -1, e => e.TravelType == travelTypeFilter)
                        .WhereIf(input.MinRequestDateFilter != null, e => e.RequestDate >= input.MinRequestDateFilter)
                        .WhereIf(input.MaxRequestDateFilter != null, e => e.RequestDate <= input.MaxRequestDateFilter)
                        .WhereIf(input.MinRequestPlanDateFilter != null, e => e.RequestPlanDate >= input.MinRequestPlanDateFilter)
                        .WhereIf(input.MaxRequestPlanDateFilter != null, e => e.RequestPlanDate <= input.MaxRequestPlanDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CampFilter), e => e.Camp.Contains(input.CampFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TransportBusFilter), e => e.TransportBus.Contains(input.TransportBusFilter))
                        .WhereIf(input.MinCreatedDateFilter != null, e => e.CreatedDate >= input.MinCreatedDateFilter)
                        .WhereIf(input.MaxCreatedDateFilter != null, e => e.CreatedDate <= input.MaxCreatedDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserTravelFk != null && e.UserTravelFk.Name == input.UserNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AirportDisplayPropertyFilter), e => string.Format("{0} {1} {2} {3}", e.AirportFromFk == null || e.AirportFromFk.AirportName == null ? "" : e.AirportFromFk.AirportName.ToString()
, e.AirportFromFk == null || e.AirportFromFk.IATA == null ? "" : e.AirportFromFk.IATA.ToString()
, e.AirportFromFk == null || e.AirportFromFk.City == null ? "" : e.AirportFromFk.City.ToString()
, e.AirportFromFk == null || e.AirportFromFk.Category == null ? "" : e.AirportFromFk.Category.ToString()
) == input.AirportDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AirportDisplayProperty2Filter), e => string.Format("{0} {1} {2} {3}", e.AirportToFk == null || e.AirportToFk.AirportName == null ? "" : e.AirportToFk.AirportName.ToString()
, e.AirportToFk == null || e.AirportToFk.IATA == null ? "" : e.AirportToFk.IATA.ToString()
, e.AirportToFk == null || e.AirportToFk.City == null ? "" : e.AirportToFk.City.ToString()
, e.AirportToFk == null || e.AirportToFk.Category == null ? "" : e.AirportToFk.Category.ToString()
) == input.AirportDisplayProperty2Filter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserName2Filter), e => e.CreatedByFk != null && e.CreatedByFk.Name == input.UserName2Filter);

            var pagedAndFilteredTravelRequests = filteredTravelRequests
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var travelRequests = from o in pagedAndFilteredTravelRequests
                                 join o1 in _lookup_userRepository.GetAll() on o.UserTravel equals o1.Id into j1
                                 from s1 in j1.DefaultIfEmpty()

                                 join o2 in _lookup_airportRepository.GetAll() on o.AirportFrom equals o2.Id into j2
                                 from s2 in j2.DefaultIfEmpty()

                                 join o3 in _lookup_airportRepository.GetAll() on o.AirportTo equals o3.Id into j3
                                 from s3 in j3.DefaultIfEmpty()

                                 join o4 in _lookup_userRepository.GetAll() on o.CreatedBy equals o4.Id into j4
                                 from s4 in j4.DefaultIfEmpty()

                                 select new
                                 {

                                     o.RequestNo,
                                     o.TravelStatus,
                                     o.TravelType,
                                     o.RequestDate,
                                     o.Camp,
                                     o.TransportBus,
                                     Id = o.Id,
                                     UserName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                                     AirportDisplayProperty = string.Format("{0} {1} {2} {3}", s2 == null || s2.AirportName == null ? "" : s2.AirportName.ToString()
                 , s2 == null || s2.IATA == null ? "" : s2.IATA.ToString()
                 , s2 == null || s2.City == null ? "" : s2.City.ToString()
                 , s2 == null || s2.Category == null ? "" : s2.Category.ToString()
                 ),
                                     AirportDisplayProperty2 = string.Format("{0} {1} {2} {3}", s3 == null || s3.AirportName == null ? "" : s3.AirportName.ToString()
                 , s3 == null || s3.IATA == null ? "" : s3.IATA.ToString()
                 , s3 == null || s3.City == null ? "" : s3.City.ToString()
                 , s3 == null || s3.Category == null ? "" : s3.Category.ToString()
                 ),
                                     UserName2 = s4 == null || s4.Name == null ? "" : s4.Name.ToString()
                                 };

            var totalCount = await filteredTravelRequests.CountAsync();

            var dbList = await travelRequests.ToListAsync();
            var results = new List<GetTravelRequestForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetTravelRequestForViewDto()
                {
                    TravelRequest = new TravelRequestDto
                    {

                        RequestNo = o.RequestNo,
                        TravelStatus = o.TravelStatus,
                        TravelType = o.TravelType,
                        RequestDate = o.RequestDate,
                        Camp = o.Camp,
                        TransportBus = o.TransportBus,
                        Id = o.Id,
                    },
                    UserName = o.UserName,
                    AirportDisplayProperty = o.AirportDisplayProperty,
                    AirportDisplayProperty2 = o.AirportDisplayProperty2,
                    UserName2 = o.UserName2
                };

                results.Add(res);
            }

            return new PagedResultDto<GetTravelRequestForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetTravelRequestForViewDto> GetTravelRequestForView(Guid id)
        {
            var travelRequest = await _travelRequestRepository.GetAsync(id);

            var output = new GetTravelRequestForViewDto { TravelRequest = ObjectMapper.Map<TravelRequestDto>(travelRequest) };

            if (output.TravelRequest.UserTravel != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.TravelRequest.UserTravel);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            if (output.TravelRequest.AirportFrom != null)
            {
                var _lookupAirport = await _lookup_airportRepository.FirstOrDefaultAsync((Guid)output.TravelRequest.AirportFrom);
                output.AirportDisplayProperty = string.Format("{0} {1} {2} {3}", _lookupAirport.AirportName, _lookupAirport.IATA, _lookupAirport.City, _lookupAirport.Category);
            }

            if (output.TravelRequest.AirportTo != null)
            {
                var _lookupAirport = await _lookup_airportRepository.FirstOrDefaultAsync((Guid)output.TravelRequest.AirportTo);
                output.AirportDisplayProperty2 = string.Format("{0} {1} {2} {3}", _lookupAirport.AirportName, _lookupAirport.IATA, _lookupAirport.City, _lookupAirport.Category);
            }

            if (output.TravelRequest.CreatedBy != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.TravelRequest.CreatedBy);
                output.UserName2 = _lookupUser?.Name?.ToString();
            }

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_TravelRequests_Edit)]
        public virtual async Task<GetTravelRequestForEditOutput> GetTravelRequestForEdit(EntityDto<Guid> input)
        {
            var travelRequest = await _travelRequestRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetTravelRequestForEditOutput { TravelRequest = ObjectMapper.Map<CreateOrEditTravelRequestDto>(travelRequest) };

            if (output.TravelRequest.UserTravel != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.TravelRequest.UserTravel);
                output.UserName = _lookupUser?.Name?.ToString();
            }

            if (output.TravelRequest.AirportFrom != null)
            {
                var _lookupAirport = await _lookup_airportRepository.FirstOrDefaultAsync((Guid)output.TravelRequest.AirportFrom);
                output.AirportDisplayProperty = string.Format("{0} {1} {2} {3}", _lookupAirport.AirportName, _lookupAirport.IATA, _lookupAirport.City, _lookupAirport.Category);
            }

            if (output.TravelRequest.AirportTo != null)
            {
                var _lookupAirport = await _lookup_airportRepository.FirstOrDefaultAsync((Guid)output.TravelRequest.AirportTo);
                output.AirportDisplayProperty2 = string.Format("{0} {1} {2} {3}", _lookupAirport.AirportName, _lookupAirport.IATA, _lookupAirport.City, _lookupAirport.Category);
            }

            if (output.TravelRequest.CreatedBy != null)
            {
                var _lookupUser = await _lookup_userRepository.FirstOrDefaultAsync((long)output.TravelRequest.CreatedBy);
                output.UserName2 = _lookupUser?.Name?.ToString();
            }

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditTravelRequestDto input)
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

        [AbpAuthorize(AppPermissions.Pages_TravelRequests_Create)]
        protected virtual async Task Create(CreateOrEditTravelRequestDto input)
        {
            var travelRequest = ObjectMapper.Map<TravelRequest>(input);

            if (AbpSession.TenantId != null)
            {
                travelRequest.TenantId = (int?)AbpSession.TenantId;
            }

            await _travelRequestRepository.InsertAsync(travelRequest);

        }

        [AbpAuthorize(AppPermissions.Pages_TravelRequests_Edit)]
        protected virtual async Task Update(CreateOrEditTravelRequestDto input)
        {
            var travelRequest = await _travelRequestRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, travelRequest);

        }

        [AbpAuthorize(AppPermissions.Pages_TravelRequests_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _travelRequestRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetTravelRequestsToExcel(GetAllTravelRequestsForExcelInput input)
        {
            var travelStatusFilter = input.TravelStatusFilter.HasValue
                        ? (TravelStatus)input.TravelStatusFilter
                        : default;
            var travelTypeFilter = input.TravelTypeFilter.HasValue
                ? (TravelType)input.TravelTypeFilter
                : default;

            var filteredTravelRequests = _travelRequestRepository.GetAll()
                        .Include(e => e.UserTravelFk)
                        .Include(e => e.AirportFromFk)
                        .Include(e => e.AirportToFk)
                        .Include(e => e.CreatedByFk)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.RequestNo.Contains(input.Filter) || e.Camp.Contains(input.Filter) || e.TransportBus.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RequestNoFilter), e => e.RequestNo.Contains(input.RequestNoFilter))
                        .WhereIf(input.TravelStatusFilter.HasValue && input.TravelStatusFilter > -1, e => e.TravelStatus == travelStatusFilter)
                        .WhereIf(input.TravelTypeFilter.HasValue && input.TravelTypeFilter > -1, e => e.TravelType == travelTypeFilter)
                        .WhereIf(input.MinRequestDateFilter != null, e => e.RequestDate >= input.MinRequestDateFilter)
                        .WhereIf(input.MaxRequestDateFilter != null, e => e.RequestDate <= input.MaxRequestDateFilter)
                        .WhereIf(input.MinRequestPlanDateFilter != null, e => e.RequestPlanDate >= input.MinRequestPlanDateFilter)
                        .WhereIf(input.MaxRequestPlanDateFilter != null, e => e.RequestPlanDate <= input.MaxRequestPlanDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.CampFilter), e => e.Camp.Contains(input.CampFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TransportBusFilter), e => e.TransportBus.Contains(input.TransportBusFilter))
                        .WhereIf(input.MinCreatedDateFilter != null, e => e.CreatedDate >= input.MinCreatedDateFilter)
                        .WhereIf(input.MaxCreatedDateFilter != null, e => e.CreatedDate <= input.MaxCreatedDateFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserNameFilter), e => e.UserTravelFk != null && e.UserTravelFk.Name == input.UserNameFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AirportDisplayPropertyFilter), e => string.Format("{0} {1} {2} {3}", e.AirportFromFk == null || e.AirportFromFk.AirportName == null ? "" : e.AirportFromFk.AirportName.ToString()
, e.AirportFromFk == null || e.AirportFromFk.IATA == null ? "" : e.AirportFromFk.IATA.ToString()
, e.AirportFromFk == null || e.AirportFromFk.City == null ? "" : e.AirportFromFk.City.ToString()
, e.AirportFromFk == null || e.AirportFromFk.Category == null ? "" : e.AirportFromFk.Category.ToString()
) == input.AirportDisplayPropertyFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AirportDisplayProperty2Filter), e => string.Format("{0} {1} {2} {3}", e.AirportToFk == null || e.AirportToFk.AirportName == null ? "" : e.AirportToFk.AirportName.ToString()
, e.AirportToFk == null || e.AirportToFk.IATA == null ? "" : e.AirportToFk.IATA.ToString()
, e.AirportToFk == null || e.AirportToFk.City == null ? "" : e.AirportToFk.City.ToString()
, e.AirportToFk == null || e.AirportToFk.Category == null ? "" : e.AirportToFk.Category.ToString()
) == input.AirportDisplayProperty2Filter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UserName2Filter), e => e.CreatedByFk != null && e.CreatedByFk.Name == input.UserName2Filter);

            var query = (from o in filteredTravelRequests
                         join o1 in _lookup_userRepository.GetAll() on o.UserTravel equals o1.Id into j1
                         from s1 in j1.DefaultIfEmpty()

                         join o2 in _lookup_airportRepository.GetAll() on o.AirportFrom equals o2.Id into j2
                         from s2 in j2.DefaultIfEmpty()

                         join o3 in _lookup_airportRepository.GetAll() on o.AirportTo equals o3.Id into j3
                         from s3 in j3.DefaultIfEmpty()

                         join o4 in _lookup_userRepository.GetAll() on o.CreatedBy equals o4.Id into j4
                         from s4 in j4.DefaultIfEmpty()

                         select new GetTravelRequestForViewDto()
                         {
                             TravelRequest = new TravelRequestDto
                             {
                                 RequestNo = o.RequestNo,
                                 TravelStatus = o.TravelStatus,
                                 TravelType = o.TravelType,
                                 RequestDate = o.RequestDate,
                                 Camp = o.Camp,
                                 TransportBus = o.TransportBus,
                                 Id = o.Id
                             },
                             UserName = s1 == null || s1.Name == null ? "" : s1.Name.ToString(),
                             AirportDisplayProperty = string.Format("{0} {1} {2} {3}", s2 == null || s2.AirportName == null ? "" : s2.AirportName.ToString()
, s2 == null || s2.IATA == null ? "" : s2.IATA.ToString()
, s2 == null || s2.City == null ? "" : s2.City.ToString()
, s2 == null || s2.Category == null ? "" : s2.Category.ToString()
),
                             AirportDisplayProperty2 = string.Format("{0} {1} {2} {3}", s3 == null || s3.AirportName == null ? "" : s3.AirportName.ToString()
, s3 == null || s3.IATA == null ? "" : s3.IATA.ToString()
, s3 == null || s3.City == null ? "" : s3.City.ToString()
, s3 == null || s3.Category == null ? "" : s3.Category.ToString()
),
                             UserName2 = s4 == null || s4.Name == null ? "" : s4.Name.ToString()
                         });

            var travelRequestListDtos = await query.ToListAsync();

            return _travelRequestsExcelExporter.ExportToFile(travelRequestListDtos);
        }

        [AbpAuthorize(AppPermissions.Pages_TravelRequests)]
        public async Task<PagedResultDto<TravelRequestUserLookupTableDto>> GetAllUserForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_userRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => e.Name != null && e.Name.Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var userList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<TravelRequestUserLookupTableDto>();
            foreach (var user in userList)
            {
                lookupTableDtoList.Add(new TravelRequestUserLookupTableDto
                {
                    Id = user.Id,
                    DisplayName = user.Name?.ToString()
                });
            }

            return new PagedResultDto<TravelRequestUserLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

        [AbpAuthorize(AppPermissions.Pages_TravelRequests)]
        public async Task<PagedResultDto<TravelRequestAirportLookupTableDto>> GetAllAirportForLookupTable(GetAllForLookupTableInput input)
        {
            var query = _lookup_airportRepository.GetAll().WhereIf(
                   !string.IsNullOrWhiteSpace(input.Filter),
                  e => string.Format("{0} {1} {2} {3}", e.AirportName, e.IATA, e.City, e.Category).Contains(input.Filter)
               );

            var totalCount = await query.CountAsync();

            var airportList = await query
                .PageBy(input)
                .ToListAsync();

            var lookupTableDtoList = new List<TravelRequestAirportLookupTableDto>();
            foreach (var airport in airportList)
            {
                lookupTableDtoList.Add(new TravelRequestAirportLookupTableDto
                {
                    Id = airport.Id.ToString(),
                    DisplayName = string.Format("{0} {1} {2} {3}", airport.AirportName, airport.IATA, airport.City, airport.Category)
                });
            }

            return new PagedResultDto<TravelRequestAirportLookupTableDto>(
                totalCount,
                lookupTableDtoList
            );
        }

    }
}