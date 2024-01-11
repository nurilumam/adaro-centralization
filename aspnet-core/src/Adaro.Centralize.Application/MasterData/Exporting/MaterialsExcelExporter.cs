using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.MasterData.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.MasterData.Exporting
{
    public class MaterialsExcelExporter : MiniExcelExcelExporterBase, IMaterialsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MaterialsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMaterialForViewDto> materials)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var material in materials)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("MaterialNo"), material.Material.MaterialNo},
                        {L("MaterialName"), material.Material.MaterialName},
                        {L("Description"), material.Material.Description},
                        {L("UoM"), material.Material.UoM},
                        {L("ImageMain"), material.Material.ImageMain},

                    });
            }

            return CreateExcelPackage("MaterialsList.xlsx", items);

        }
    }
}