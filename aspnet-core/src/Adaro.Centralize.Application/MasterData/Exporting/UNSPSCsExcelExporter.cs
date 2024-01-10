using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.MasterData.Exporting
{
    public class UNSPSCsExcelExporter : MiniExcelExcelExporterBase, IUNSPSCsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public UNSPSCsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetUNSPSCForViewDto> unspsCs)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var unspsc in unspsCs)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("UNSPSC_Code"), unspsc.UNSPSC.UNSPSC_Code},
                        {L("Description"), unspsc.UNSPSC.Description},
                        {L("AccountCode"), unspsc.UNSPSC.AccountCode},

                    });
            }

            return CreateExcelPackage("UNSPSCsList.xlsx", items);

        }
    }
}