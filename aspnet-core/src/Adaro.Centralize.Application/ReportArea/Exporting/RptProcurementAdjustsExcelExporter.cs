using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.ReportArea.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.ReportArea.Exporting
{
    public class RptProcurementAdjustsExcelExporter : MiniExcelExcelExporterBase, IRptProcurementAdjustsExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public RptProcurementAdjustsExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetRptProcurementAdjustForViewDto> rptProcurementAdjusts)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var rptProcurementAdjust in rptProcurementAdjusts)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("PurchasingDocument"), rptProcurementAdjust.RptProcurementAdjust.PurchasingDocument},
                        {L("IsContract"), rptProcurementAdjust.RptProcurementAdjust.IsContract},
                        {L("IsAdjust"), rptProcurementAdjust.RptProcurementAdjust.IsAdjust},
                        {L("DayAdjust"), rptProcurementAdjust.RptProcurementAdjust.DayAdjust},
                        {L("Remark"), rptProcurementAdjust.RptProcurementAdjust.Remark},

                    });
            }

            return CreateExcelPackage("RptProcurementAdjustsList.xlsx", items);

        }
    }
}