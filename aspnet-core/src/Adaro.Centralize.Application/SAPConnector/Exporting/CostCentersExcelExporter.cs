using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.SAPConnector.Exporting
{
    public class CostCentersExcelExporter : MiniExcelExcelExporterBase, ICostCentersExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public CostCentersExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetCostCenterForViewDto> costCenters)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var costCenter in costCenters)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("ControllingArea"), costCenter.CostCenter.ControllingArea},
                        {L("CostCenterName"), costCenter.CostCenter.CostCenterName},
                        {L("Description"), costCenter.CostCenter.Description},
                        {L("IsActive"), costCenter.CostCenter.IsActive},
                        {L("CostCenterCode"), costCenter.CostCenter.CostCenterCode},
                        {L("DepartmentName"), costCenter.CostCenter.DepartmentName},
                        {L("Period"), costCenter.CostCenter.Period},

                    });
            }

            return CreateExcelPackage("CostCentersList.xlsx", items);

        }
    }
}