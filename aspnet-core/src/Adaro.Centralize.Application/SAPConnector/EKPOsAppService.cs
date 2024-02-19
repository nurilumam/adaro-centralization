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
    [AbpAuthorize(AppPermissions.Pages_EKPOs)]
    public class EKPOsAppService : CentralizeAppServiceBase, IEKPOsAppService
    {
        private readonly IRepository<EKPO, Guid> _ekpoRepository;
        private readonly IEKPOsExcelExporter _ekpOsExcelExporter;

        public EKPOsAppService(IRepository<EKPO, Guid> ekpoRepository, IEKPOsExcelExporter ekpOsExcelExporter)
        {
            _ekpoRepository = ekpoRepository;
            _ekpOsExcelExporter = ekpOsExcelExporter;

        }

        public virtual async Task<PagedResultDto<GetEKPOForViewDto>> GetAll(GetAllEKPOsInput input)
        {

            var filteredEKPOs = _ekpoRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MANDT.Contains(input.Filter) || e.EBELN.Contains(input.Filter) || e.UNIQUEID.Contains(input.Filter) || e.LOEKZ.Contains(input.Filter) || e.STATU.Contains(input.Filter) || e.TXZ01.Contains(input.Filter) || e.MATNR.Contains(input.Filter) || e.EMATN.Contains(input.Filter) || e.BUKRS.Contains(input.Filter) || e.WERKS.Contains(input.Filter) || e.LGORT.Contains(input.Filter) || e.BEDNR.Contains(input.Filter) || e.MATKL.Contains(input.Filter) || e.INFNR.Contains(input.Filter) || e.IDNLF.Contains(input.Filter) || e.MEINS.Contains(input.Filter) || e.BPRME.Contains(input.Filter) || e.MWSKZ.Contains(input.Filter) || e.BONUS.Contains(input.Filter) || e.INSMK.Contains(input.Filter) || e.SPINF.Contains(input.Filter) || e.PRSDR.Contains(input.Filter) || e.BWTAR.Contains(input.Filter) || e.BWTTY.Contains(input.Filter) || e.ABSKZ.Contains(input.Filter) || e.PSTYP.Contains(input.Filter) || e.KNTTP.Contains(input.Filter) || e.KONNR.Contains(input.Filter) || e.ANFNR.Contains(input.Filter) || e.BANFN.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MANDTFilter), e => e.MANDT.Contains(input.MANDTFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EBELNFilter), e => e.EBELN.Contains(input.EBELNFilter))
                        .WhereIf(input.MinEBELPFilter != null, e => e.EBELP >= input.MinEBELPFilter)
                        .WhereIf(input.MaxEBELPFilter != null, e => e.EBELP <= input.MaxEBELPFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UNIQUEIDFilter), e => e.UNIQUEID.Contains(input.UNIQUEIDFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LOEKZFilter), e => e.LOEKZ.Contains(input.LOEKZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.STATUFilter), e => e.STATU.Contains(input.STATUFilter))
                        .WhereIf(input.MinAEDATFilter != null, e => e.AEDAT >= input.MinAEDATFilter)
                        .WhereIf(input.MaxAEDATFilter != null, e => e.AEDAT <= input.MaxAEDATFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TXZ01Filter), e => e.TXZ01.Contains(input.TXZ01Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MATNRFilter), e => e.MATNR.Contains(input.MATNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EMATNFilter), e => e.EMATN.Contains(input.EMATNFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BUKRSFilter), e => e.BUKRS.Contains(input.BUKRSFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WERKSFilter), e => e.WERKS.Contains(input.WERKSFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LGORTFilter), e => e.LGORT.Contains(input.LGORTFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BEDNRFilter), e => e.BEDNR.Contains(input.BEDNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MATKLFilter), e => e.MATKL.Contains(input.MATKLFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.INFNRFilter), e => e.INFNR.Contains(input.INFNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IDNLFFilter), e => e.IDNLF.Contains(input.IDNLFFilter))
                        .WhereIf(input.MinKTMNGFilter != null, e => e.KTMNG >= input.MinKTMNGFilter)
                        .WhereIf(input.MaxKTMNGFilter != null, e => e.KTMNG <= input.MaxKTMNGFilter)
                        .WhereIf(input.MinMENGEFilter != null, e => e.MENGE >= input.MinMENGEFilter)
                        .WhereIf(input.MaxMENGEFilter != null, e => e.MENGE <= input.MaxMENGEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MEINSFilter), e => e.MEINS.Contains(input.MEINSFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BPRMEFilter), e => e.BPRME.Contains(input.BPRMEFilter))
                        .WhereIf(input.MinBPUMZFilter != null, e => e.BPUMZ >= input.MinBPUMZFilter)
                        .WhereIf(input.MaxBPUMZFilter != null, e => e.BPUMZ <= input.MaxBPUMZFilter)
                        .WhereIf(input.MinBPUMNFilter != null, e => e.BPUMN >= input.MinBPUMNFilter)
                        .WhereIf(input.MaxBPUMNFilter != null, e => e.BPUMN <= input.MaxBPUMNFilter)
                        .WhereIf(input.MinUMREZFilter != null, e => e.UMREZ >= input.MinUMREZFilter)
                        .WhereIf(input.MaxUMREZFilter != null, e => e.UMREZ <= input.MaxUMREZFilter)
                        .WhereIf(input.MinUMRENFilter != null, e => e.UMREN >= input.MinUMRENFilter)
                        .WhereIf(input.MaxUMRENFilter != null, e => e.UMREN <= input.MaxUMRENFilter)
                        .WhereIf(input.MinNETPRFilter != null, e => e.NETPR >= input.MinNETPRFilter)
                        .WhereIf(input.MaxNETPRFilter != null, e => e.NETPR <= input.MaxNETPRFilter)
                        .WhereIf(input.MinPEINHFilter != null, e => e.PEINH >= input.MinPEINHFilter)
                        .WhereIf(input.MaxPEINHFilter != null, e => e.PEINH <= input.MaxPEINHFilter)
                        .WhereIf(input.MinNETWRFilter != null, e => e.NETWR >= input.MinNETWRFilter)
                        .WhereIf(input.MaxNETWRFilter != null, e => e.NETWR <= input.MaxNETWRFilter)
                        .WhereIf(input.MinBRTWRFilter != null, e => e.BRTWR >= input.MinBRTWRFilter)
                        .WhereIf(input.MaxBRTWRFilter != null, e => e.BRTWR <= input.MaxBRTWRFilter)
                        .WhereIf(input.MinAGDATFilter != null, e => e.AGDAT >= input.MinAGDATFilter)
                        .WhereIf(input.MaxAGDATFilter != null, e => e.AGDAT <= input.MaxAGDATFilter)
                        .WhereIf(input.MinWEBAZFilter != null, e => e.WEBAZ >= input.MinWEBAZFilter)
                        .WhereIf(input.MaxWEBAZFilter != null, e => e.WEBAZ <= input.MaxWEBAZFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MWSKZFilter), e => e.MWSKZ.Contains(input.MWSKZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BONUSFilter), e => e.BONUS.Contains(input.BONUSFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.INSMKFilter), e => e.INSMK.Contains(input.INSMKFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SPINFFilter), e => e.SPINF.Contains(input.SPINFFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PRSDRFilter), e => e.PRSDR.Contains(input.PRSDRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BWTARFilter), e => e.BWTAR.Contains(input.BWTARFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BWTTYFilter), e => e.BWTTY.Contains(input.BWTTYFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ABSKZFilter), e => e.ABSKZ.Contains(input.ABSKZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PSTYPFilter), e => e.PSTYP.Contains(input.PSTYPFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KNTTPFilter), e => e.KNTTP.Contains(input.KNTTPFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KONNRFilter), e => e.KONNR.Contains(input.KONNRFilter))
                        .WhereIf(input.MinKTPNRFilter != null, e => e.KTPNR >= input.MinKTPNRFilter)
                        .WhereIf(input.MaxKTPNRFilter != null, e => e.KTPNR <= input.MaxKTPNRFilter)
                        .WhereIf(input.MinPACKNOFilter != null, e => e.PACKNO >= input.MinPACKNOFilter)
                        .WhereIf(input.MaxPACKNOFilter != null, e => e.PACKNO <= input.MaxPACKNOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ANFNRFilter), e => e.ANFNR.Contains(input.ANFNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BANFNFilter), e => e.BANFN.Contains(input.BANFNFilter))
                        .WhereIf(input.MinBNFPOFilter != null, e => e.BNFPO >= input.MinBNFPOFilter)
                        .WhereIf(input.MaxBNFPOFilter != null, e => e.BNFPO <= input.MaxBNFPOFilter);

            var pagedAndFilteredEKPOs = filteredEKPOs
                .OrderBy(input.Sorting ?? "id asc")
                .PageBy(input);

            var ekpOs = from o in pagedAndFilteredEKPOs
                        select new
                        {

                            o.MANDT,
                            o.EBELN,
                            o.EBELP,
                            o.UNIQUEID,
                            o.LOEKZ,
                            o.AEDAT,
                            o.TXZ01,
                            o.MATNR,
                            o.BUKRS,
                            o.BEDNR,
                            o.MATKL,
                            o.INFNR,
                            o.IDNLF,
                            o.KTMNG,
                            o.MENGE,
                            o.MEINS,
                            o.BPRME,
                            o.BPUMZ,
                            o.BPUMN,
                            o.UMREZ,
                            o.UMREN,
                            o.NETPR,
                            o.PEINH,
                            o.NETWR,
                            o.BRTWR,
                            o.AGDAT,
                            o.WEBAZ,
                            o.MWSKZ,
                            o.BONUS,
                            o.INSMK,
                            o.SPINF,
                            o.PRSDR,
                            o.BWTAR,
                            o.BWTTY,
                            o.ABSKZ,
                            o.PSTYP,
                            o.KNTTP,
                            o.KONNR,
                            o.KTPNR,
                            o.PACKNO,
                            o.ANFNR,
                            o.BANFN,
                            o.BNFPO,
                            Id = o.Id
                        };

            var totalCount = await filteredEKPOs.CountAsync();

            var dbList = await ekpOs.ToListAsync();
            var results = new List<GetEKPOForViewDto>();

            foreach (var o in dbList)
            {
                var res = new GetEKPOForViewDto()
                {
                    EKPO = new EKPODto
                    {

                        MANDT = o.MANDT,
                        EBELN = o.EBELN,
                        EBELP = o.EBELP,
                        UNIQUEID = o.UNIQUEID,
                        LOEKZ = o.LOEKZ,
                        AEDAT = o.AEDAT,
                        TXZ01 = o.TXZ01,
                        MATNR = o.MATNR,
                        BUKRS = o.BUKRS,
                        BEDNR = o.BEDNR,
                        MATKL = o.MATKL,
                        INFNR = o.INFNR,
                        IDNLF = o.IDNLF,
                        KTMNG = o.KTMNG,
                        MENGE = o.MENGE,
                        MEINS = o.MEINS,
                        BPRME = o.BPRME,
                        BPUMZ = o.BPUMZ,
                        BPUMN = o.BPUMN,
                        UMREZ = o.UMREZ,
                        UMREN = o.UMREN,
                        NETPR = o.NETPR,
                        PEINH = o.PEINH,
                        NETWR = o.NETWR,
                        BRTWR = o.BRTWR,
                        AGDAT = o.AGDAT,
                        WEBAZ = o.WEBAZ,
                        MWSKZ = o.MWSKZ,
                        BONUS = o.BONUS,
                        INSMK = o.INSMK,
                        SPINF = o.SPINF,
                        PRSDR = o.PRSDR,
                        BWTAR = o.BWTAR,
                        BWTTY = o.BWTTY,
                        ABSKZ = o.ABSKZ,
                        PSTYP = o.PSTYP,
                        KNTTP = o.KNTTP,
                        KONNR = o.KONNR,
                        KTPNR = o.KTPNR,
                        PACKNO = o.PACKNO,
                        ANFNR = o.ANFNR,
                        BANFN = o.BANFN,
                        BNFPO = o.BNFPO,
                        Id = o.Id,
                    }
                };

                results.Add(res);
            }

            return new PagedResultDto<GetEKPOForViewDto>(
                totalCount,
                results
            );

        }

        public virtual async Task<GetEKPOForViewDto> GetEKPOForView(Guid id)
        {
            var ekpo = await _ekpoRepository.GetAsync(id);

            var output = new GetEKPOForViewDto { EKPO = ObjectMapper.Map<EKPODto>(ekpo) };

            return output;
        }

        [AbpAuthorize(AppPermissions.Pages_EKPOs_Edit)]
        public virtual async Task<GetEKPOForEditOutput> GetEKPOForEdit(EntityDto<Guid> input)
        {
            var ekpo = await _ekpoRepository.FirstOrDefaultAsync(input.Id);

            var output = new GetEKPOForEditOutput { EKPO = ObjectMapper.Map<CreateOrEditEKPODto>(ekpo) };

            return output;
        }

        public virtual async Task CreateOrEdit(CreateOrEditEKPODto input)
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

        [AbpAuthorize(AppPermissions.Pages_EKPOs_Create)]
        protected virtual async Task Create(CreateOrEditEKPODto input)
        {
            var ekpo = ObjectMapper.Map<EKPO>(input);

            if (AbpSession.TenantId != null)
            {
                ekpo.TenantId = (int?)AbpSession.TenantId;
            }

            await _ekpoRepository.InsertAsync(ekpo);

        }

        [AbpAuthorize(AppPermissions.Pages_EKPOs_Edit)]
        protected virtual async Task Update(CreateOrEditEKPODto input)
        {
            var ekpo = await _ekpoRepository.FirstOrDefaultAsync((Guid)input.Id);
            ObjectMapper.Map(input, ekpo);

        }

        [AbpAuthorize(AppPermissions.Pages_EKPOs_Delete)]
        public virtual async Task Delete(EntityDto<Guid> input)
        {
            await _ekpoRepository.DeleteAsync(input.Id);
        }

        public virtual async Task<FileDto> GetEKPOsToExcel(GetAllEKPOsForExcelInput input)
        {

            var filteredEKPOs = _ekpoRepository.GetAll()
                        .WhereIf(!string.IsNullOrWhiteSpace(input.Filter), e => false || e.MANDT.Contains(input.Filter) || e.EBELN.Contains(input.Filter) || e.UNIQUEID.Contains(input.Filter) || e.LOEKZ.Contains(input.Filter) || e.STATU.Contains(input.Filter) || e.TXZ01.Contains(input.Filter) || e.MATNR.Contains(input.Filter) || e.EMATN.Contains(input.Filter) || e.BUKRS.Contains(input.Filter) || e.WERKS.Contains(input.Filter) || e.LGORT.Contains(input.Filter) || e.BEDNR.Contains(input.Filter) || e.MATKL.Contains(input.Filter) || e.INFNR.Contains(input.Filter) || e.IDNLF.Contains(input.Filter) || e.MEINS.Contains(input.Filter) || e.BPRME.Contains(input.Filter) || e.MWSKZ.Contains(input.Filter) || e.BONUS.Contains(input.Filter) || e.INSMK.Contains(input.Filter) || e.SPINF.Contains(input.Filter) || e.PRSDR.Contains(input.Filter) || e.BWTAR.Contains(input.Filter) || e.BWTTY.Contains(input.Filter) || e.ABSKZ.Contains(input.Filter) || e.PSTYP.Contains(input.Filter) || e.KNTTP.Contains(input.Filter) || e.KONNR.Contains(input.Filter) || e.ANFNR.Contains(input.Filter) || e.BANFN.Contains(input.Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MANDTFilter), e => e.MANDT.Contains(input.MANDTFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EBELNFilter), e => e.EBELN.Contains(input.EBELNFilter))
                        .WhereIf(input.MinEBELPFilter != null, e => e.EBELP >= input.MinEBELPFilter)
                        .WhereIf(input.MaxEBELPFilter != null, e => e.EBELP <= input.MaxEBELPFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.UNIQUEIDFilter), e => e.UNIQUEID.Contains(input.UNIQUEIDFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LOEKZFilter), e => e.LOEKZ.Contains(input.LOEKZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.STATUFilter), e => e.STATU.Contains(input.STATUFilter))
                        .WhereIf(input.MinAEDATFilter != null, e => e.AEDAT >= input.MinAEDATFilter)
                        .WhereIf(input.MaxAEDATFilter != null, e => e.AEDAT <= input.MaxAEDATFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.TXZ01Filter), e => e.TXZ01.Contains(input.TXZ01Filter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MATNRFilter), e => e.MATNR.Contains(input.MATNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.EMATNFilter), e => e.EMATN.Contains(input.EMATNFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BUKRSFilter), e => e.BUKRS.Contains(input.BUKRSFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.WERKSFilter), e => e.WERKS.Contains(input.WERKSFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.LGORTFilter), e => e.LGORT.Contains(input.LGORTFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BEDNRFilter), e => e.BEDNR.Contains(input.BEDNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MATKLFilter), e => e.MATKL.Contains(input.MATKLFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.INFNRFilter), e => e.INFNR.Contains(input.INFNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.IDNLFFilter), e => e.IDNLF.Contains(input.IDNLFFilter))
                        .WhereIf(input.MinKTMNGFilter != null, e => e.KTMNG >= input.MinKTMNGFilter)
                        .WhereIf(input.MaxKTMNGFilter != null, e => e.KTMNG <= input.MaxKTMNGFilter)
                        .WhereIf(input.MinMENGEFilter != null, e => e.MENGE >= input.MinMENGEFilter)
                        .WhereIf(input.MaxMENGEFilter != null, e => e.MENGE <= input.MaxMENGEFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MEINSFilter), e => e.MEINS.Contains(input.MEINSFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BPRMEFilter), e => e.BPRME.Contains(input.BPRMEFilter))
                        .WhereIf(input.MinBPUMZFilter != null, e => e.BPUMZ >= input.MinBPUMZFilter)
                        .WhereIf(input.MaxBPUMZFilter != null, e => e.BPUMZ <= input.MaxBPUMZFilter)
                        .WhereIf(input.MinBPUMNFilter != null, e => e.BPUMN >= input.MinBPUMNFilter)
                        .WhereIf(input.MaxBPUMNFilter != null, e => e.BPUMN <= input.MaxBPUMNFilter)
                        .WhereIf(input.MinUMREZFilter != null, e => e.UMREZ >= input.MinUMREZFilter)
                        .WhereIf(input.MaxUMREZFilter != null, e => e.UMREZ <= input.MaxUMREZFilter)
                        .WhereIf(input.MinUMRENFilter != null, e => e.UMREN >= input.MinUMRENFilter)
                        .WhereIf(input.MaxUMRENFilter != null, e => e.UMREN <= input.MaxUMRENFilter)
                        .WhereIf(input.MinNETPRFilter != null, e => e.NETPR >= input.MinNETPRFilter)
                        .WhereIf(input.MaxNETPRFilter != null, e => e.NETPR <= input.MaxNETPRFilter)
                        .WhereIf(input.MinPEINHFilter != null, e => e.PEINH >= input.MinPEINHFilter)
                        .WhereIf(input.MaxPEINHFilter != null, e => e.PEINH <= input.MaxPEINHFilter)
                        .WhereIf(input.MinNETWRFilter != null, e => e.NETWR >= input.MinNETWRFilter)
                        .WhereIf(input.MaxNETWRFilter != null, e => e.NETWR <= input.MaxNETWRFilter)
                        .WhereIf(input.MinBRTWRFilter != null, e => e.BRTWR >= input.MinBRTWRFilter)
                        .WhereIf(input.MaxBRTWRFilter != null, e => e.BRTWR <= input.MaxBRTWRFilter)
                        .WhereIf(input.MinAGDATFilter != null, e => e.AGDAT >= input.MinAGDATFilter)
                        .WhereIf(input.MaxAGDATFilter != null, e => e.AGDAT <= input.MaxAGDATFilter)
                        .WhereIf(input.MinWEBAZFilter != null, e => e.WEBAZ >= input.MinWEBAZFilter)
                        .WhereIf(input.MaxWEBAZFilter != null, e => e.WEBAZ <= input.MaxWEBAZFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.MWSKZFilter), e => e.MWSKZ.Contains(input.MWSKZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BONUSFilter), e => e.BONUS.Contains(input.BONUSFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.INSMKFilter), e => e.INSMK.Contains(input.INSMKFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.SPINFFilter), e => e.SPINF.Contains(input.SPINFFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PRSDRFilter), e => e.PRSDR.Contains(input.PRSDRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BWTARFilter), e => e.BWTAR.Contains(input.BWTARFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BWTTYFilter), e => e.BWTTY.Contains(input.BWTTYFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ABSKZFilter), e => e.ABSKZ.Contains(input.ABSKZFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.PSTYPFilter), e => e.PSTYP.Contains(input.PSTYPFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KNTTPFilter), e => e.KNTTP.Contains(input.KNTTPFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.KONNRFilter), e => e.KONNR.Contains(input.KONNRFilter))
                        .WhereIf(input.MinKTPNRFilter != null, e => e.KTPNR >= input.MinKTPNRFilter)
                        .WhereIf(input.MaxKTPNRFilter != null, e => e.KTPNR <= input.MaxKTPNRFilter)
                        .WhereIf(input.MinPACKNOFilter != null, e => e.PACKNO >= input.MinPACKNOFilter)
                        .WhereIf(input.MaxPACKNOFilter != null, e => e.PACKNO <= input.MaxPACKNOFilter)
                        .WhereIf(!string.IsNullOrWhiteSpace(input.ANFNRFilter), e => e.ANFNR.Contains(input.ANFNRFilter))
                        .WhereIf(!string.IsNullOrWhiteSpace(input.BANFNFilter), e => e.BANFN.Contains(input.BANFNFilter))
                        .WhereIf(input.MinBNFPOFilter != null, e => e.BNFPO >= input.MinBNFPOFilter)
                        .WhereIf(input.MaxBNFPOFilter != null, e => e.BNFPO <= input.MaxBNFPOFilter);

            var query = (from o in filteredEKPOs
                         select new GetEKPOForViewDto()
                         {
                             EKPO = new EKPODto
                             {
                                 MANDT = o.MANDT,
                                 EBELN = o.EBELN,
                                 EBELP = o.EBELP,
                                 UNIQUEID = o.UNIQUEID,
                                 LOEKZ = o.LOEKZ,
                                 AEDAT = o.AEDAT,
                                 TXZ01 = o.TXZ01,
                                 MATNR = o.MATNR,
                                 BUKRS = o.BUKRS,
                                 BEDNR = o.BEDNR,
                                 MATKL = o.MATKL,
                                 INFNR = o.INFNR,
                                 IDNLF = o.IDNLF,
                                 KTMNG = o.KTMNG,
                                 MENGE = o.MENGE,
                                 MEINS = o.MEINS,
                                 BPRME = o.BPRME,
                                 BPUMZ = o.BPUMZ,
                                 BPUMN = o.BPUMN,
                                 UMREZ = o.UMREZ,
                                 UMREN = o.UMREN,
                                 NETPR = o.NETPR,
                                 PEINH = o.PEINH,
                                 NETWR = o.NETWR,
                                 BRTWR = o.BRTWR,
                                 AGDAT = o.AGDAT,
                                 WEBAZ = o.WEBAZ,
                                 MWSKZ = o.MWSKZ,
                                 BONUS = o.BONUS,
                                 INSMK = o.INSMK,
                                 SPINF = o.SPINF,
                                 PRSDR = o.PRSDR,
                                 BWTAR = o.BWTAR,
                                 BWTTY = o.BWTTY,
                                 ABSKZ = o.ABSKZ,
                                 PSTYP = o.PSTYP,
                                 KNTTP = o.KNTTP,
                                 KONNR = o.KONNR,
                                 KTPNR = o.KTPNR,
                                 PACKNO = o.PACKNO,
                                 ANFNR = o.ANFNR,
                                 BANFN = o.BANFN,
                                 BNFPO = o.BNFPO,
                                 Id = o.Id
                             }
                         });

            var ekpoListDtos = await query.ToListAsync();

            return _ekpOsExcelExporter.ExportToFile(ekpoListDtos);
        }

    }
}