using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.SAPConnector.Exporting
{
    public class DataProductionsExcelExporter : MiniExcelExcelExporterBase, IDataProductionsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public DataProductionsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetDataProductionForViewDto> dataProductions)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var dataProduction in dataProductions)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("MaterialDocument"), dataProduction.DataProduction.MaterialDocument},
                        {L("MaterialDocYear"), dataProduction.DataProduction.MaterialDocYear},
                        {L("MaterialDocItem"), dataProduction.DataProduction.MaterialDocItem},
                        {L("Order"), dataProduction.DataProduction.Order},
                        {L("Reservation"), dataProduction.DataProduction.Reservation},
                        {L("PurchaseOrder"), dataProduction.DataProduction.PurchaseOrder},
                        {L("MovementType"), dataProduction.DataProduction.MovementType},
                        {L("MovementTypeText"), dataProduction.DataProduction.MovementTypeText},
                        {L("Plant"), dataProduction.DataProduction.Plant},
                        {L("StorageLocation"), dataProduction.DataProduction.StorageLocation},
                        {L("Material"), dataProduction.DataProduction.Material},
                        {L("MaterialDescription"), dataProduction.DataProduction.MaterialDescription},
                        {L("Quantity"), dataProduction.DataProduction.Quantity},
                        {L("QtyInOrderUnit"), dataProduction.DataProduction.QtyInOrderUnit},
                        {L("PostingDate"), dataProduction.DataProduction.PostingDate},
                        {L("EntryDate"), dataProduction.DataProduction.EntryDate},
                        {L("DocumentDate"), dataProduction.DataProduction.DocumentDate},
                        {L("Batch"), dataProduction.DataProduction.Batch},

                    });
            }

            return CreateExcelPackage("DataProductionsList.xlsx", items);

        }
    }
}