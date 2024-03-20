using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Adaro.Centralize.ReportArea.Exporting;
using Adaro.Centralize.ReportArea.Dtos;
using Adaro.Centralize.Dto;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.ReportArea
{
    [AbpAuthorize(AppPermissions.Pages_RptProcurementAdjusts)]
    public class RptProcurementAdjustsAppService : CentralizeAppServiceBase, IRptProcurementAdjustsAppService
    {
        private readonly IRepository<RptProcurementAdjust, Guid> _rptProcurementAdjustRepository;
        private readonly IRptProcurementAdjustsExcelExporter _rptProcurementAdjustsExcelExporter;

        public RptProcurementAdjustsAppService(IRepository<RptProcurementAdjust, Guid> rptProcurementAdjustRepository, IRptProcurementAdjustsExcelExporter rptProcurementAdjustsExcelExporter)
        {
            _rptProcurementAdjustRepository = rptProcurementAdjustRepository;
            _rptProcurementAdjustsExcelExporter = rptProcurementAdjustsExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetRptProcurementAdjustForViewDto>> GetAll(GetAllRptProcurementAdjustsInput input)
        {

            var filteredRptProcurementAdjusts = _rptProcurementAdjustRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PurchasingDocument.Contains(input.Filter) || e.Remark.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingDocumentFilter), e => e.PurchasingDocument.Contains(input.PurchasingDocumentFilter))
                        .WhereIf(input.IsContractFilter.HasValue && input.IsContractFilter > -1, e => (input.IsContractFilter == 1 && e.IsContract) || (input.IsContractFilter == 0 && !e.IsContract))
                        .WhereIf(input.IsAdjustFilter.HasValue && input.IsAdjustFilter > -1, e => (input.IsAdjustFilter == 1 && e.IsAdjust) || (input.IsAdjustFilter == 0 && !e.IsAdjust))
                        .WhereIf(input.MinDayAdjustFilter != null, e => e.DayAdjust >= input.MinDayAdjustFilter)
                        .WhereIf(input.MaxDayAdjustFilter != null, e => e.DayAdjust <= input.MaxDayAdjustFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RemarkFilter), e => e.Remark.Contains(input.RemarkFilter));

            var pagedAndFilteredRptProcurementAdjusts = filteredRptProcurementAdjusts
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var rptProcurementAdjusts = from o in pagedAndFilteredRptProcurementAdjusts
                                        select new
                                        {

                                            o.PurchasingDocument,
                                            o.IsContract,
                                            o.IsAdjust,
                                            o.DayAdjust,
                                            o.Remark,
                                            Id = o.Id
                                        };

            var totalCount = await filteredRptProcurementAdjusts.CountAsync();

            var dbList = await rptProcurementAdjusts.ToListAsync();
            var results = new List<GetRptProcurementAdjustForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetRptProcurementAdjustForViewDto()
                {
                    RptProcurementAdjust = new RptProcurementAdjustDto
                    {

                        PurchasingDocument = o.PurchasingDocument,
                        IsContract = o.IsContract,
                        IsAdjust = o.IsAdjust,
                        DayAdjust = o.DayAdjust,
                        Remark = o.Remark,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetRptProcurementAdjustForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetRptProcurementAdjustForViewDto> GetRptProcurementAdjustForView(Guid id)
        {
            var rptProcurementAdjust = await _rptProcurementAdjustRepository.GetAsync(id);

            var output = new GetRptProcurementAdjustForViewDto { RptProcurementAdjust = ObjectMapper.Map<RptProcurementAdjustDto>(rptProcurementAdjust) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_RptProcurementAdjusts_Edit)]
        public virtual async Task<GetRptProcurementAdjustForEditOutput> GetRptProcurementAdjustForEdit(EntityDto<Guid> input)
        {
            var rptProcurementAdjust = await _rptProcurementAdjustRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetRptProcurementAdjustForEditOutput { RptProcurementAdjust = ObjectMapper.Map<CreateOrEditRptProcurementAdjustDto>(rptProcurementAdjust) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditRptProcurementAdjustDto input)
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

        [AbpAuthorize(AppPermissions.Pages_RptProcurementAdjusts_Create)]
        protected virtual async Task Create(CreateOrEditRptProcurementAdjustDto input)
        {
            var rptProcurementAdjust = ObjectMapper.Map<RptProcurementAdjust>(input);

            if (AbpSession.TenantId != null)
            {
                rptProcurementAdjust.TenantId = (int?)AbpSession.TenantId;
            }

            await _rptProcurementAdjustRepository.InsertAsync(rptProcurementAdjust);

        }

        [AbpAuthorize(AppPermissions.Pages_RptProcurementAdjusts_Edit)]
        protected virtual async Task Update(CreateOrEditRptProcurementAdjustDto input)
        {
            var rptProcurementAdjust = await _rptProcurementAdjustRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, rptProcurementAdjust);

        }

        [AbpAuthorize(AppPermissions.Pages_RptProcurementAdjusts_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _rptProcurementAdjustRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetRptProcurementAdjustsToExcel(GetAllRptProcurementAdjustsForExcelInput input)
        {

            var filteredRptProcurementAdjusts = _rptProcurementAdjustRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.PurchasingDocument.Contains(input.Filter) || e.Remark.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PurchasingDocumentFilter), e => e.PurchasingDocument.Contains(input.PurchasingDocumentFilter))
                        .WhereIf(input.IsContractFilter.HasValue && input.IsContractFilter > -1, e => (input.IsContractFilter == 1 && e.IsContract) || (input.IsContractFilter == 0 && !e.IsContract))
                        .WhereIf(input.IsAdjustFilter.HasValue && input.IsAdjustFilter > -1, e => (input.IsAdjustFilter == 1 && e.IsAdjust) || (input.IsAdjustFilter == 0 && !e.IsAdjust))
                        .WhereIf(input.MinDayAdjustFilter != null, e => e.DayAdjust >= input.MinDayAdjustFilter)
                        .WhereIf(input.MaxDayAdjustFilter != null, e => e.DayAdjust <= input.MaxDayAdjustFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RemarkFilter), e => e.Remark.Contains(input.RemarkFilter));

            var query = (from o in filteredRptProcurementAdjusts
                         select new GetRptProcurementAdjustForViewDto()
                         {
                             RptProcurementAdjust = new RptProcurementAdjustDto
                             {
                                 PurchasingDocument = o.PurchasingDocument,
                                 IsContract = o.IsContract,
                                 IsAdjust = o.IsAdjust,
                                 DayAdjust = o.DayAdjust,
                                 Remark = o.Remark,
                                 Id = o.Id
                             }
                         });

            var rptProcurementAdjustListDtos = await query.ToListAsync();

            return _rptProcurementAdjustsExcelExporter.ExportToFile(rptProcurementAdjustListDtos);
        }

    }
}