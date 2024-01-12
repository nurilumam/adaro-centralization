using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.MasterData.Exporting
{
    public class EnumTablesExcelExporter : MiniExcelExcelExporterBase, IEnumTablesExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public EnumTablesExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetEnumTableForViewDto> enumTables)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var enumTable in enumTables)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("EnumCode"), enumTable.EnumTable.EnumCode},
                        {L("EnumValue"), enumTable.EnumTable.EnumValue},
                        {L("EnumLabel"), enumTable.EnumTable.EnumLabel},

                    });
            }

            return CreateExcelPackage("EnumTablesList.xlsx", items);

        }
    }
}