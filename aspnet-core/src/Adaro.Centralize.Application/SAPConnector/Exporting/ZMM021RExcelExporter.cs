using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.SAPConnector.Exporting
{
    public class ZMM021RExcelExporter : MiniExcelExcelExporterBase, IZMM021RExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ZMM021RExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetZMM021RForViewDto> zmM021Rs)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var zmM021R in zmM021Rs)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("PurchasingDocument"), zmM021R.ZMM021R.PurchasingDocument},
                        {L("PurchasingDocType"), zmM021R.ZMM021R.PurchasingDocType},
                        {L("PurchasingDocTypeDescription"), zmM021R.ZMM021R.PurchasingDocTypeDescription},
                        {L("Item"), zmM021R.ZMM021R.Item},
                        {L("LineNumber"), zmM021R.ZMM021R.LineNumber},
                        {L("DeletionIndicator"), zmM021R.ZMM021R.DeletionIndicator},
                        {L("DocumentDate"), zmM021R.ZMM021R.DocumentDate},
                        {L("CreatedOn"), zmM021R.ZMM021R.CreatedOn},
                        {L("PurchaseRequisition"), zmM021R.ZMM021R.PurchaseRequisition},
                        {L("ItemPR"), zmM021R.ZMM021R.ItemPR},
                        {L("SupplierCode"), zmM021R.ZMM021R.SupplierCode},
                        {L("SupplierName"), zmM021R.ZMM021R.SupplierName},
                        {L("Address"), zmM021R.ZMM021R.Address},
                        {L("ItemNo"), zmM021R.ZMM021R.ItemNo},
                        {L("MaterialGroup"), zmM021R.ZMM021R.MaterialGroup},
                        {L("ShortText"), zmM021R.ZMM021R.ShortText},
                        {L("OrderQuantity"), zmM021R.ZMM021R.OrderQuantity},
                        {L("OrderUnit"), zmM021R.ZMM021R.OrderUnit},
                        {L("Currency"), zmM021R.ZMM021R.Currency},
                        {L("DeliveryDate"), zmM021R.ZMM021R.DeliveryDate},
                        {L("NetPrice"), zmM021R.ZMM021R.NetPrice},
                        {L("NetOrderValue"), zmM021R.ZMM021R.NetOrderValue},
                        {L("Demurrage"), zmM021R.ZMM021R.Demurrage},
                        {L("GrossPrice"), zmM021R.ZMM021R.GrossPrice},
                        {L("TotalDiscount"), zmM021R.ZMM021R.TotalDiscount},
                        {L("FreightCost"), zmM021R.ZMM021R.FreightCost},
                        {L("ReleaseIndicator"), zmM021R.ZMM021R.ReleaseIndicator},
                        {L("Plant"), zmM021R.ZMM021R.Plant},
                        {L("PurchasingGroup"), zmM021R.ZMM021R.PurchasingGroup},
                        {L("TaxCode"), zmM021R.ZMM021R.TaxCode},
                        {L("CollectiveNumber"), zmM021R.ZMM021R.CollectiveNumber},
                        {L("ItemCategory"), zmM021R.ZMM021R.ItemCategory},
                        {L("AccountAssignment"), zmM021R.ZMM021R.AccountAssignment},
                        {L("OutlineAgreement"), zmM021R.ZMM021R.OutlineAgreement},
                        {L("RFQNo"), zmM021R.ZMM021R.RFQNo},
                        {L("QtyPending"), zmM021R.ZMM021R.QtyPending},
                        {L("MaterialService"), zmM021R.ZMM021R.MaterialService},
                        {L("ApprovalStatus"), zmM021R.ZMM021R.ApprovalStatus},
                        {L("POStatus"), zmM021R.ZMM021R.POStatus},
                        {L("Period"), zmM021R.ZMM021R.Period},
                        {L("CommentVendor"), zmM021R.ZMM021R.CommentVendor},
                        {L("ItemText"), zmM021R.ZMM021R.ItemText},
                        {L("LongText"), zmM021R.ZMM021R.LongText},
                        {L("OurReference"), zmM021R.ZMM021R.OurReference},
                        {L("PRFinalFirstApprovalDate"), zmM021R.ZMM021R.PRFinalFirstApprovalDate},
                        {L("PRFinalLastApprovalDate"), zmM021R.ZMM021R.PRFinalLastApprovalDate},
                        {L("POFirstApprovalDate"), zmM021R.ZMM021R.POFirstApprovalDate},
                        {L("POLastApprovalDate"), zmM021R.ZMM021R.POLastApprovalDate},
                        {L("POApprovalName"), zmM021R.ZMM021R.POApprovalName},
                        {L("BuyerCode"), zmM021R.ZMM021R.BuyerCode},
                        {L("BuyerName"), zmM021R.ZMM021R.BuyerName},
                        {L("PICDept"), zmM021R.ZMM021R.PICDept},
                        {L("PICSect"), zmM021R.ZMM021R.PICSect},
                        {L("FuelAllocation"), zmM021R.ZMM021R.FuelAllocation},
                        {L("CostCenter"), zmM021R.ZMM021R.CostCenter},
                        {L("CostCenterDescription"), zmM021R.ZMM021R.CostCenterDescription},
                        {L("WBSElement"), zmM021R.ZMM021R.WBSElement},
                        {L("AssetNo"), zmM021R.ZMM021R.AssetNo},
                        {L("FundCenter"), zmM021R.ZMM021R.FundCenter},
                        {L("CreatedDate"), zmM021R.ZMM021R.CreatedDate},
                        {L("UpdatedDate"), zmM021R.ZMM021R.UpdatedDate},

                    });
            }

            return CreateExcelPackage("ZMM021RList.xlsx", items);

        }
    }
}