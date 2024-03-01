using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.LookupArea.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.LookupArea.Exporting
{
    public class LookupPagesExcelExporter : MiniExcelExcelExporterBase, ILookupPagesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public LookupPagesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetLookupPageForViewDto> lookupPages)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var lookupPage in lookupPages)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("LookupName"), lookupPage.LookupPage.LookupName},

                    });
            }

            return CreateExcelPackage("LookupPagesList.xlsx", items);

        }
    }
}