using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.Finance.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.Finance.Exporting
{
    public class TransferBudgetItemsExcelExporter : MiniExcelExcelExporterBase, ITransferBudgetItemsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public TransferBudgetItemsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetTransferBudgetItemForViewDto> transferBudgetItems)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var transferBudgetItem in transferBudgetItems)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("PeriodFrom"), transferBudgetItem.TransferBudgetItem.PeriodFrom},
                        {L("AmountFrom"), transferBudgetItem.TransferBudgetItem.AmountFrom},
                        {L("PeriodTo"), transferBudgetItem.TransferBudgetItem.PeriodTo},
                        {L("AmountTo"), transferBudgetItem.TransferBudgetItem.AmountTo},

                    });
            }

            return CreateExcelPackage("TransferBudgetItemsList.xlsx", items);

        }
    }
}