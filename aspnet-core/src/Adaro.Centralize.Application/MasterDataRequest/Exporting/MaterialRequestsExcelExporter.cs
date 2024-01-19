using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.MasterDataRequest.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.MasterDataRequest.Exporting
{
    public class MaterialRequestsExcelExporter : MiniExcelExcelExporterBase, IMaterialRequestsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public MaterialRequestsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetMaterialRequestForViewDto> materialRequests)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var materialRequest in materialRequests)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("RequestNo"), materialRequest.MaterialRequest.RequestNo},
                        {L("RequestStatus"), materialRequest.MaterialRequest.RequestStatus},
                        {L("MaterialName"), materialRequest.MaterialRequest.MaterialName},
                        {L("Description"), materialRequest.MaterialRequest.Description},
                        {L("GeneralLedger"), materialRequest.MaterialRequest.GeneralLedger},
                        {L("Picture"), materialRequest.MaterialRequest.Picture},

                    });
            }

            return CreateExcelPackage("MaterialRequestsList.xlsx", items);

        }
    }
}