using System;
using System.Linq;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Adaro.Centralize.SAPConnector.Exporting;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Abp.Application.Services.Dto;
using Adaro.Centralize.Authorization;
using Abp.Extensions;
using Abp.Authorization;
using Microsoft.EntityFrameworkCore;
using Abp.UI;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.SAPConnector
{
    [AbpAuthorize(AppPermissions.Pages_Ekkos)]
    public class EKKOsAppService : CentralizeAppServiceBase, IEkkosAppService
    {
        private readonly IRepository<EKKO, Guid> _ekkoRepository;
        private readonly IEkkosExcelExporter _ekkosExcelExporter;

        public EKKOsAppService(IRepository<EKKO, Guid> ekkoRepository, IEkkosExcelExporter ekkosExcelExporter)
        {
            _ekkoRepository = ekkoRepository;
            _ekkosExcelExporter = ekkosExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetEkkoForViewDto>> GetAll(GetAllEkkosInput input)
        {

            var filteredEkkos = _ekkoRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MANDT.Contains(input.Filter) || e.EBELN.Contains(input.Filter) || e.BUKRS.Contains(input.Filter) || e.BSTYP.Contains(input.Filter) || e.BSART.Contains(input.Filter) || e.BSAKZ.Contains(input.Filter) || e.LOEKZ.Contains(input.Filter) || e.STATU.Contains(input.Filter) || e.ERNAM.Contains(input.Filter) || e.LIFNR.Contains(input.Filter) || e.ZTERM.Contains(input.Filter) || e.EKORG.Contains(input.Filter) || e.EKGRP.Contains(input.Filter) || e.WAERS.Contains(input.Filter) || e.KUFIX.Contains(input.Filter) || e.KUNNR.Contains(input.Filter) || e.KONNR.Contains(input.Filter) || e.ABGRU.Contains(input.Filter) || e.AUTLF.Contains(input.Filter) || e.WEAKT.Contains(input.Filter) || e.RESWK.Contains(input.Filter) || e.LBLIF.Contains(input.Filter) || e.INCO1.Contains(input.Filter) || e.INCO2.Contains(input.Filter) || e.SUBMI.Contains(input.Filter) || e.KNUMV.Contains(input.Filter) || e.KALSM.Contains(input.Filter) || e.PROCSTAT.Contains(input.Filter) || e.UNSEZ.Contains(input.Filter) || e.FRGGR.Contains(input.Filter) || e.FRGSX.Contains(input.Filter) || e.FRGKE.Contains(input.Filter) || e.FRGZU.Contains(input.Filter) || e.ADRNR.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MANDTFilter), e => e.MANDT.Contains(input.MANDTFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EBELNFilter), e => e.EBELN.Contains(input.EBELNFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BUKRSFilter), e => e.BUKRS.Contains(input.BUKRSFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BSTYPFilter), e => e.BSTYP.Contains(input.BSTYPFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BSARTFilter), e => e.BSART.Contains(input.BSARTFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BSAKZFilter), e => e.BSAKZ.Contains(input.BSAKZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LOEKZFilter), e => e.LOEKZ.Contains(input.LOEKZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.STATUFilter), e => e.STATU.Contains(input.STATUFilter))
                        .WhereIf(input.MinAEDATFilter != null, e => e.AEDAT >= input.MinAEDATFilter)
                        .WhereIf(input.MaxAEDATFilter != null, e => e.AEDAT <= input.MaxAEDATFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ERNAMFilter), e => e.ERNAM.Contains(input.ERNAMFilter))
                        .WhereIf(input.MinPINCRFilter != null, e => e.PINCR >= input.MinPINCRFilter)
                        .WhereIf(input.MaxPINCRFilter != null, e => e.PINCR <= input.MaxPINCRFilter)
                        .WhereIf(input.MinLPONRFilter != null, e => e.LPONR >= input.MinLPONRFilter)
                        .WhereIf(input.MaxLPONRFilter != null, e => e.LPONR <= input.MaxLPONRFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LIFNRFilter), e => e.LIFNR.Contains(input.LIFNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ZTERMFilter), e => e.ZTERM.Contains(input.ZTERMFilter))
                        .WhereIf(input.MinZBD1TFilter != null, e => e.ZBD1T >= input.MinZBD1TFilter)
                        .WhereIf(input.MaxZBD1TFilter != null, e => e.ZBD1T <= input.MaxZBD1TFilter)
                        .WhereIf(input.MinZBD2TFilter != null, e => e.ZBD2T >= input.MinZBD2TFilter)
                        .WhereIf(input.MaxZBD2TFilter != null, e => e.ZBD2T <= input.MaxZBD2TFilter)
                        .WhereIf(input.MinZBD3TFilter != null, e => e.ZBD3T >= input.MinZBD3TFilter)
                        .WhereIf(input.MaxZBD3TFilter != null, e => e.ZBD3T <= input.MaxZBD3TFilter)
                        .WhereIf(input.MinZBD1PFilter != null, e => e.ZBD1P >= input.MinZBD1PFilter)
                        .WhereIf(input.MaxZBD1PFilter != null, e => e.ZBD1P <= input.MaxZBD1PFilter)
                        .WhereIf(input.MinZBD2PFilter != null, e => e.ZBD2P >= input.MinZBD2PFilter)
                        .WhereIf(input.MaxZBD2PFilter != null, e => e.ZBD2P <= input.MaxZBD2PFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EKORGFilter), e => e.EKORG.Contains(input.EKORGFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EKGRPFilter), e => e.EKGRP.Contains(input.EKGRPFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WAERSFilter), e => e.WAERS.Contains(input.WAERSFilter))
                        .WhereIf(input.MinWKURSFilter != null, e => e.WKURS >= input.MinWKURSFilter)
                        .WhereIf(input.MaxWKURSFilter != null, e => e.WKURS <= input.MaxWKURSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KUFIXFilter), e => e.KUFIX.Contains(input.KUFIXFilter))
                        .WhereIf(input.MinBEDATFilter != null, e => e.BEDAT >= input.MinBEDATFilter)
                        .WhereIf(input.MaxBEDATFilter != null, e => e.BEDAT <= input.MaxBEDATFilter)
                        .WhereIf(input.MinKDATBFilter != null, e => e.KDATB >= input.MinKDATBFilter)
                        .WhereIf(input.MaxKDATBFilter != null, e => e.KDATB <= input.MaxKDATBFilter)
                        .WhereIf(input.MinKDATEFilter != null, e => e.KDATE >= input.MinKDATEFilter)
                        .WhereIf(input.MaxKDATEFilter != null, e => e.KDATE <= input.MaxKDATEFilter)
                        .WhereIf(input.MinBWBDTFilter != null, e => e.BWBDT >= input.MinBWBDTFilter)
                        .WhereIf(input.MaxBWBDTFilter != null, e => e.BWBDT <= input.MaxBWBDTFilter)
                        .WhereIf(input.MinGWLDTFilter != null, e => e.GWLDT >= input.MinGWLDTFilter)
                        .WhereIf(input.MaxGWLDTFilter != null, e => e.GWLDT <= input.MaxGWLDTFilter)
                        .WhereIf(input.MinIHRANFilter != null, e => e.IHRAN >= input.MinIHRANFilter)
                        .WhereIf(input.MaxIHRANFilter != null, e => e.IHRAN <= input.MaxIHRANFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KUNNRFilter), e => e.KUNNR.Contains(input.KUNNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KONNRFilter), e => e.KONNR.Contains(input.KONNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ABGRUFilter), e => e.ABGRU.Contains(input.ABGRUFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AUTLFFilter), e => e.AUTLF.Contains(input.AUTLFFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WEAKTFilter), e => e.WEAKT.Contains(input.WEAKTFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RESWKFilter), e => e.RESWK.Contains(input.RESWKFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LBLIFFilter), e => e.LBLIF.Contains(input.LBLIFFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.INCO1Filter), e => e.INCO1.Contains(input.INCO1Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.INCO2Filter), e => e.INCO2.Contains(input.INCO2Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SUBMIFilter), e => e.SUBMI.Contains(input.SUBMIFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KNUMVFilter), e => e.KNUMV.Contains(input.KNUMVFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KALSMFilter), e => e.KALSM.Contains(input.KALSMFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PROCSTATFilter), e => e.PROCSTAT.Contains(input.PROCSTATFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UNSEZFilter), e => e.UNSEZ.Contains(input.UNSEZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FRGGRFilter), e => e.FRGGR.Contains(input.FRGGRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FRGSXFilter), e => e.FRGSX.Contains(input.FRGSXFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FRGKEFilter), e => e.FRGKE.Contains(input.FRGKEFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FRGZUFilter), e => e.FRGZU.Contains(input.FRGZUFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ADRNRFilter), e => e.ADRNR.Contains(input.ADRNRFilter));

            var pagedAndFilteredEkkos = filteredEkkos
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var ekkos = from o in pagedAndFilteredEkkos
                        select new
                        {

                            o.MANDT,
                            o.EBELN,
                            o.BUKRS,
                            o.BSTYP,
                            o.AEDAT,
                            o.ZBD1T,
                            o.ZBD2T,
                            o.ZBD3T,
                            o.EKGRP,
                            o.WKURS,
                            o.KUFIX,
                            o.BEDAT,
                            o.KDATB,
                            o.KDATE,
                            o.BWBDT,
                            o.GWLDT,
                            o.IHRAN,
                            o.KUNNR,
                            o.KONNR,
                            o.ABGRU,
                            o.AUTLF,
                            o.WEAKT,
                            o.RESWK,
                            o.LBLIF,
                            o.INCO1,
                            o.INCO2,
                            o.SUBMI,
                            o.KNUMV,
                            Id = o.Id
                        };

            var totalCount = await filteredEkkos.CountAsync();

            var dbList = await ekkos.ToListAsync();
            var results = new List<GetEkkoForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEkkoForViewDto()
                {
                    Ekko = new EkkoDto
                    {

                        MANDT = o.MANDT,
                        EBELN = o.EBELN,
                        BUKRS = o.BUKRS,
                        BSTYP = o.BSTYP,
                        AEDAT = o.AEDAT,
                        ZBD1T = o.ZBD1T,
                        ZBD2T = o.ZBD2T,
                        ZBD3T = o.ZBD3T,
                        EKGRP = o.EKGRP,
                        WKURS = o.WKURS,
                        KUFIX = o.KUFIX,
                        BEDAT = o.BEDAT,
                        KDATB = o.KDATB,
                        KDATE = o.KDATE,
                        BWBDT = o.BWBDT,
                        GWLDT = o.GWLDT,
                        IHRAN = o.IHRAN,
                        KUNNR = o.KUNNR,
                        KONNR = o.KONNR,
                        ABGRU = o.ABGRU,
                        AUTLF = o.AUTLF,
                        WEAKT = o.WEAKT,
                        RESWK = o.RESWK,
                        LBLIF = o.LBLIF,
                        INCO1 = o.INCO1,
                        INCO2 = o.INCO2,
                        SUBMI = o.SUBMI,
                        KNUMV = o.KNUMV,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetEkkoForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetEkkoForViewDto> GetEkkoForView(Guid id)
        {
            var ekko = await _ekkoRepository.GetAsync(id);

            var output = new GetEkkoForViewDto { Ekko = ObjectMapper.Map<EkkoDto>(ekko) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_Ekkos_Edit)]
        public virtual async Task<GetEkkoForEditOutput> GetEkkoForEdit(EntityDto<Guid> input)
        {
            var ekko = await _ekkoRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEkkoForEditOutput { Ekko = ObjectMapper.Map<CreateOrEditEkkoDto>(ekko) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditEkkoDto input)
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

        [AbpAuthorize(AppPermissions.Pages_Ekkos_Create)]
        protected virtual async Task Create(CreateOrEditEkkoDto input)
        {
            var ekko = ObjectMapper.Map<EKKO>(input);

            if (AbpSession.TenantId != null)
            {
                ekko.TenantId = (int?)AbpSession.TenantId;
            }

            await _ekkoRepository.InsertAsync(ekko);

        }

        [AbpAuthorize(AppPermissions.Pages_Ekkos_Edit)]
        protected virtual async Task Update(CreateOrEditEkkoDto input)
        {
            var ekko = await _ekkoRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, ekko);

        }

        [AbpAuthorize(AppPermissions.Pages_Ekkos_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _ekkoRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetEkkosToExcel(GetAllEkkosForExcelInput input)
        {

            var filteredEkkos = _ekkoRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MANDT.Contains(input.Filter) || e.EBELN.Contains(input.Filter) || e.BUKRS.Contains(input.Filter) || e.BSTYP.Contains(input.Filter) || e.BSART.Contains(input.Filter) || e.BSAKZ.Contains(input.Filter) || e.LOEKZ.Contains(input.Filter) || e.STATU.Contains(input.Filter) || e.ERNAM.Contains(input.Filter) || e.LIFNR.Contains(input.Filter) || e.ZTERM.Contains(input.Filter) || e.EKORG.Contains(input.Filter) || e.EKGRP.Contains(input.Filter) || e.WAERS.Contains(input.Filter) || e.KUFIX.Contains(input.Filter) || e.KUNNR.Contains(input.Filter) || e.KONNR.Contains(input.Filter) || e.ABGRU.Contains(input.Filter) || e.AUTLF.Contains(input.Filter) || e.WEAKT.Contains(input.Filter) || e.RESWK.Contains(input.Filter) || e.LBLIF.Contains(input.Filter) || e.INCO1.Contains(input.Filter) || e.INCO2.Contains(input.Filter) || e.SUBMI.Contains(input.Filter) || e.KNUMV.Contains(input.Filter) || e.KALSM.Contains(input.Filter) || e.PROCSTAT.Contains(input.Filter) || e.UNSEZ.Contains(input.Filter) || e.FRGGR.Contains(input.Filter) || e.FRGSX.Contains(input.Filter) || e.FRGKE.Contains(input.Filter) || e.FRGZU.Contains(input.Filter) || e.ADRNR.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MANDTFilter), e => e.MANDT.Contains(input.MANDTFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EBELNFilter), e => e.EBELN.Contains(input.EBELNFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BUKRSFilter), e => e.BUKRS.Contains(input.BUKRSFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BSTYPFilter), e => e.BSTYP.Contains(input.BSTYPFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BSARTFilter), e => e.BSART.Contains(input.BSARTFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BSAKZFilter), e => e.BSAKZ.Contains(input.BSAKZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LOEKZFilter), e => e.LOEKZ.Contains(input.LOEKZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.STATUFilter), e => e.STATU.Contains(input.STATUFilter))
                        .WhereIf(input.MinAEDATFilter != null, e => e.AEDAT >= input.MinAEDATFilter)
                        .WhereIf(input.MaxAEDATFilter != null, e => e.AEDAT <= input.MaxAEDATFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ERNAMFilter), e => e.ERNAM.Contains(input.ERNAMFilter))
                        .WhereIf(input.MinPINCRFilter != null, e => e.PINCR >= input.MinPINCRFilter)
                        .WhereIf(input.MaxPINCRFilter != null, e => e.PINCR <= input.MaxPINCRFilter)
                        .WhereIf(input.MinLPONRFilter != null, e => e.LPONR >= input.MinLPONRFilter)
                        .WhereIf(input.MaxLPONRFilter != null, e => e.LPONR <= input.MaxLPONRFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LIFNRFilter), e => e.LIFNR.Contains(input.LIFNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ZTERMFilter), e => e.ZTERM.Contains(input.ZTERMFilter))
                        .WhereIf(input.MinZBD1TFilter != null, e => e.ZBD1T >= input.MinZBD1TFilter)
                        .WhereIf(input.MaxZBD1TFilter != null, e => e.ZBD1T <= input.MaxZBD1TFilter)
                        .WhereIf(input.MinZBD2TFilter != null, e => e.ZBD2T >= input.MinZBD2TFilter)
                        .WhereIf(input.MaxZBD2TFilter != null, e => e.ZBD2T <= input.MaxZBD2TFilter)
                        .WhereIf(input.MinZBD3TFilter != null, e => e.ZBD3T >= input.MinZBD3TFilter)
                        .WhereIf(input.MaxZBD3TFilter != null, e => e.ZBD3T <= input.MaxZBD3TFilter)
                        .WhereIf(input.MinZBD1PFilter != null, e => e.ZBD1P >= input.MinZBD1PFilter)
                        .WhereIf(input.MaxZBD1PFilter != null, e => e.ZBD1P <= input.MaxZBD1PFilter)
                        .WhereIf(input.MinZBD2PFilter != null, e => e.ZBD2P >= input.MinZBD2PFilter)
                        .WhereIf(input.MaxZBD2PFilter != null, e => e.ZBD2P <= input.MaxZBD2PFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EKORGFilter), e => e.EKORG.Contains(input.EKORGFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EKGRPFilter), e => e.EKGRP.Contains(input.EKGRPFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WAERSFilter), e => e.WAERS.Contains(input.WAERSFilter))
                        .WhereIf(input.MinWKURSFilter != null, e => e.WKURS >= input.MinWKURSFilter)
                        .WhereIf(input.MaxWKURSFilter != null, e => e.WKURS <= input.MaxWKURSFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KUFIXFilter), e => e.KUFIX.Contains(input.KUFIXFilter))
                        .WhereIf(input.MinBEDATFilter != null, e => e.BEDAT >= input.MinBEDATFilter)
                        .WhereIf(input.MaxBEDATFilter != null, e => e.BEDAT <= input.MaxBEDATFilter)
                        .WhereIf(input.MinKDATBFilter != null, e => e.KDATB >= input.MinKDATBFilter)
                        .WhereIf(input.MaxKDATBFilter != null, e => e.KDATB <= input.MaxKDATBFilter)
                        .WhereIf(input.MinKDATEFilter != null, e => e.KDATE >= input.MinKDATEFilter)
                        .WhereIf(input.MaxKDATEFilter != null, e => e.KDATE <= input.MaxKDATEFilter)
                        .WhereIf(input.MinBWBDTFilter != null, e => e.BWBDT >= input.MinBWBDTFilter)
                        .WhereIf(input.MaxBWBDTFilter != null, e => e.BWBDT <= input.MaxBWBDTFilter)
                        .WhereIf(input.MinGWLDTFilter != null, e => e.GWLDT >= input.MinGWLDTFilter)
                        .WhereIf(input.MaxGWLDTFilter != null, e => e.GWLDT <= input.MaxGWLDTFilter)
                        .WhereIf(input.MinIHRANFilter != null, e => e.IHRAN >= input.MinIHRANFilter)
                        .WhereIf(input.MaxIHRANFilter != null, e => e.IHRAN <= input.MaxIHRANFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KUNNRFilter), e => e.KUNNR.Contains(input.KUNNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KONNRFilter), e => e.KONNR.Contains(input.KONNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ABGRUFilter), e => e.ABGRU.Contains(input.ABGRUFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.AUTLFFilter), e => e.AUTLF.Contains(input.AUTLFFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WEAKTFilter), e => e.WEAKT.Contains(input.WEAKTFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.RESWKFilter), e => e.RESWK.Contains(input.RESWKFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LBLIFFilter), e => e.LBLIF.Contains(input.LBLIFFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.INCO1Filter), e => e.INCO1.Contains(input.INCO1Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.INCO2Filter), e => e.INCO2.Contains(input.INCO2Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SUBMIFilter), e => e.SUBMI.Contains(input.SUBMIFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KNUMVFilter), e => e.KNUMV.Contains(input.KNUMVFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KALSMFilter), e => e.KALSM.Contains(input.KALSMFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PROCSTATFilter), e => e.PROCSTAT.Contains(input.PROCSTATFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UNSEZFilter), e => e.UNSEZ.Contains(input.UNSEZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FRGGRFilter), e => e.FRGGR.Contains(input.FRGGRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FRGSXFilter), e => e.FRGSX.Contains(input.FRGSXFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FRGKEFilter), e => e.FRGKE.Contains(input.FRGKEFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.FRGZUFilter), e => e.FRGZU.Contains(input.FRGZUFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ADRNRFilter), e => e.ADRNR.Contains(input.ADRNRFilter));

            var query = (from o in filteredEkkos
                         select new GetEkkoForViewDto()
                         {
                             Ekko = new EkkoDto
                             {
                                 MANDT = o.MANDT,
                                 EBELN = o.EBELN,
                                 BUKRS = o.BUKRS,
                                 BSTYP = o.BSTYP,
                                 AEDAT = o.AEDAT,
                                 ZBD1T = o.ZBD1T,
                                 ZBD2T = o.ZBD2T,
                                 ZBD3T = o.ZBD3T,
                                 EKGRP = o.EKGRP,
                                 WKURS = o.WKURS,
                                 KUFIX = o.KUFIX,
                                 BEDAT = o.BEDAT,
                                 KDATB = o.KDATB,
                                 KDATE = o.KDATE,
                                 BWBDT = o.BWBDT,
                                 GWLDT = o.GWLDT,
                                 IHRAN = o.IHRAN,
                                 KUNNR = o.KUNNR,
                                 KONNR = o.KONNR,
                                 ABGRU = o.ABGRU,
                                 AUTLF = o.AUTLF,
                                 WEAKT = o.WEAKT,
                                 RESWK = o.RESWK,
                                 LBLIF = o.LBLIF,
                                 INCO1 = o.INCO1,
                                 INCO2 = o.INCO2,
                                 SUBMI = o.SUBMI,
                                 KNUMV = o.KNUMV,
                                 Id = o.Id
                             }
                         });

            var ekkoListDtos = await query.ToListAsync();

            return _ekkosExcelExporter.ExportToFile(ekkoListDtos);
        }

    }
}