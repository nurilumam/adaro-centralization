using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.Finance.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.Finance.Exporting
{
    public class TransferBudgetsExcelExporter : MiniExcelExcelExporterBase, ITransferBudgetsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TransferBudgetsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTransferBudgetForViewDto> transferBudgets)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var transferBudget in transferBudgets)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("DocumentNo"), transferBudget.TransferBudget.DocumentNo},
                        {L("Department"), transferBudget.TransferBudget.Department},
                        {L("Division"), transferBudget.TransferBudget.Division},
                        {L("Reason"), transferBudget.TransferBudget.Reason},
                        {L("Location"), transferBudget.TransferBudget.Location},

                    });
            }

            return CreateExcelPackage("TransferBudgetsList.xlsx", items);

        }
    }
}