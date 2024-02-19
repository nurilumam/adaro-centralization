using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.SAPConnector.Exporting
{
    public class EkkosExcelExporter : MiniExcelExcelExporterBase, IEkkosExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public EkkosExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetEkkoForViewDto> ekkos)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var ekko in ekkos)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("MANDT"), ekko.Ekko.MANDT},
                        {L("EBELN"), ekko.Ekko.EBELN},
                        {L("BUKRS"), ekko.Ekko.BUKRS},
                        {L("BSTYP"), ekko.Ekko.BSTYP},
                        {L("AEDAT"), ekko.Ekko.AEDAT},
                        {L("ZBD1T"), ekko.Ekko.ZBD1T},
                        {L("ZBD2T"), ekko.Ekko.ZBD2T},
                        {L("ZBD3T"), ekko.Ekko.ZBD3T},
                        {L("EKGRP"), ekko.Ekko.EKGRP},
                        {L("WKURS"), ekko.Ekko.WKURS},
                        {L("KUFIX"), ekko.Ekko.KUFIX},
                        {L("BEDAT"), ekko.Ekko.BEDAT},
                        {L("KDATB"), ekko.Ekko.KDATB},
                        {L("KDATE"), ekko.Ekko.KDATE},
                        {L("BWBDT"), ekko.Ekko.BWBDT},
                        {L("GWLDT"), ekko.Ekko.GWLDT},
                        {L("IHRAN"), ekko.Ekko.IHRAN},
                        {L("KUNNR"), ekko.Ekko.KUNNR},
                        {L("KONNR"), ekko.Ekko.KONNR},
                        {L("ABGRU"), ekko.Ekko.ABGRU},
                        {L("AUTLF"), ekko.Ekko.AUTLF},
                        {L("WEAKT"), ekko.Ekko.WEAKT},
                        {L("RESWK"), ekko.Ekko.RESWK},
                        {L("LBLIF"), ekko.Ekko.LBLIF},
                        {L("INCO1"), ekko.Ekko.INCO1},
                        {L("INCO2"), ekko.Ekko.INCO2},
                        {L("SUBMI"), ekko.Ekko.SUBMI},
                        {L("KNUMV"), ekko.Ekko.KNUMV},

                    });
            }

            return CreateExcelPackage("EkkosList.xlsx", items);

        }
    }
}