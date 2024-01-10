using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.MasterData.Exporting
{
    public class MaterialGroupsExcelExporter : MiniExcelExcelExporterBase, IMaterialGroupsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MaterialGroupsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMaterialGroupForViewDto> materialGroups)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var materialGroup in materialGroups)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("MaterialGroupCode"), materialGroup.MaterialGroup.MaterialGroupCode},
                        {L("MaterialGroupName"), materialGroup.MaterialGroup.MaterialGroupName},
                        {L("MaterialGroupDescription"), materialGroup.MaterialGroup.MaterialGroupDescription},
                        {L("Language"), materialGroup.MaterialGroup.Language},

                    });
            }

            return CreateExcelPackage("MaterialGroupsList.xlsx", items);

        }
    }
}