using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.Travel.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.Travel.Exporting
{
    public class AirportsExcelExporter : MiniExcelExcelExporterBase, IAirportsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public AirportsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetAirportForViewDto> airports)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var airport in airports)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("AirportName"), airport.Airport.AirportName},
                        {L("IATA"), airport.Airport.IATA},
                        {L("City"), airport.Airport.City},
                        {L("Category"), airport.Airport.Category},

                    });
            }

            return CreateExcelPackage("AirportsList.xlsx", items);

        }
    }
}