using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.Travel.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.Travel.Exporting
{
    public class TravelRequestsExcelExporter : MiniExcelExcelExporterBase, ITravelRequestsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TravelRequestsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTravelRequestForViewDto> travelRequests)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var travelRequest in travelRequests)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("RequestNo"), travelRequest.TravelRequest.RequestNo},
                        {L("TravelStatus"), travelRequest.TravelRequest.TravelStatus},
                        {L("TravelType"), travelRequest.TravelRequest.TravelType},
                        {L("RequestDate"), travelRequest.TravelRequest.RequestDate},
                        {L("Camp"), travelRequest.TravelRequest.Camp},
                        {L("TransportBus"), travelRequest.TravelRequest.TransportBus},

                    });
            }

            return CreateExcelPackage("TravelRequestsList.xlsx", items);

        }
    }
}