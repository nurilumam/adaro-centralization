using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.SAPConnector.Exporting
{
    public class GeneralLedgerAccountsExcelExporter : MiniExcelExcelExporterBase, IGeneralLedgerAccountsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public GeneralLedgerAccountsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetGeneralLedgerAccountForViewDto> generalLedgerAccounts)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var generalLedgerAccount in generalLedgerAccounts)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("FundsCenter"), generalLedgerAccount.GeneralLedgerAccount.FundsCenter},
                        {L("ConsumableBudget"), generalLedgerAccount.GeneralLedgerAccount.ConsumableBudget},
                        {L("ConsumedBudget"), generalLedgerAccount.GeneralLedgerAccount.ConsumedBudget},
                        {L("AvailableAmount"), generalLedgerAccount.GeneralLedgerAccount.AvailableAmount},
                        {L("CurrentBudget"), generalLedgerAccount.GeneralLedgerAccount.CurrentBudget},
                        {L("CommitmentActuals"), generalLedgerAccount.GeneralLedgerAccount.CommitmentActuals},
                        {L("FundsCenterDescription"), generalLedgerAccount.GeneralLedgerAccount.FundsCenterDescription},

                    });
            }

            return CreateExcelPackage("GeneralLedgerAccountsList.xlsx", items);

        }
    }
}