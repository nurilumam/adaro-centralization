using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.MasterData.Exporting
{
    public class GeneralLedgerMappingsExcelExporter : MiniExcelExcelExporterBase, IGeneralLedgerMappingsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public GeneralLedgerMappingsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetGeneralLedgerMappingForViewDto> generalLedgerMappings)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var generalLedgerMapping in generalLedgerMappings)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("GLAccount"), generalLedgerMapping.GeneralLedgerMapping.GLAccount},
                        {L("GLAccountDescription"), generalLedgerMapping.GeneralLedgerMapping.GLAccountDescription},
                        {L("MappingType"), generalLedgerMapping.GeneralLedgerMapping.MappingType},
                        {L("ValuationClass"), generalLedgerMapping.GeneralLedgerMapping.ValuationClass},
                        {L("ValuationClassDescription"), generalLedgerMapping.GeneralLedgerMapping.ValuationClassDescription},

                    });
            }

            return CreateExcelPackage("GeneralLedgerMappingsList.xlsx", items);

        }
    }
}