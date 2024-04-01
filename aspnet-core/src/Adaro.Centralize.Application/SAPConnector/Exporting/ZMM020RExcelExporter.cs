using System.Collections.Generic;
using Abp.Runtime.Session;
using Abp.Timing.Timezone;
using Adaro.Centralize.DataExporting.Excel.MiniExcel;
using Adaro.Centralize.SAPConnector.Dtos;
using Adaro.Centralize.Dto;
using Adaro.Centralize.Storage;

namespace Adaro.Centralize.SAPConnector.Exporting
{
    public class ZMM020RExcelExporter : MiniExcelExcelExporterBase, IZMM020RExcelExporter
    {

        private readonly ITimeZoneConverter _timeZoneConverter;
        private readonly IAbpSession _abpSession;

        public ZMM020RExcelExporter(
            ITimeZoneConverter timeZoneConverter,
            IAbpSession abpSession,
            ITempFileCacheManager tempFileCacheManager) :
    base(tempFileCacheManager)
        {
            _timeZoneConverter = timeZoneConverter;
            _abpSession = abpSession;
        }

        public FileDto ExportToFile(List<GetZMM020RForViewDto> zmM020Rs)
        {

            var items = new List<Dictionary<string, object>>();

            foreach (var zmM020R in zmM020Rs)
            {
                items.Add(new Dictionary<string, object>()
                    {
                        {L("PurchaseRequisition"), zmM020R.ZMM020R.PurchaseRequisition},
                        {L("DocumentType"), zmM020R.ZMM020R.DocumentType},
                        {L("DocumentTypeText"), zmM020R.ZMM020R.DocumentTypeText},
                        {L("ItemRequisition"), zmM020R.ZMM020R.ItemRequisition},
                        {L("ProcessingStatusCode"), zmM020R.ZMM020R.ProcessingStatusCode},
                        {L("ProcessingStatus"), zmM020R.ZMM020R.ProcessingStatus},
                        {L("DeletionIndicator"), zmM020R.ZMM020R.DeletionIndicator},
                        {L("ItemCategory"), zmM020R.ZMM020R.ItemCategory},
                        {L("AccountAssignment"), zmM020R.ZMM020R.AccountAssignment},
                        {L("Material"), zmM020R.ZMM020R.Material},
                        {L("ShortText"), zmM020R.ZMM020R.ShortText},
                        {L("QuantityRequested"), zmM020R.ZMM020R.QuantityRequested},
                        {L("UnitOfMeasure"), zmM020R.ZMM020R.UnitOfMeasure},
                        {L("ServiceItem"), zmM020R.ZMM020R.ServiceItem},
                        {L("Service"), zmM020R.ZMM020R.Service},
                        {L("ServiceShortText"), zmM020R.ZMM020R.ServiceShortText},
                        {L("QuantityService"), zmM020R.ZMM020R.QuantityService},
                        {L("UnitOfMeasureService"), zmM020R.ZMM020R.UnitOfMeasureService},
                        {L("DeliveryDate"), zmM020R.ZMM020R.DeliveryDate},
                        {L("MaterialGroup"), zmM020R.ZMM020R.MaterialGroup},
                        {L("Plant"), zmM020R.ZMM020R.Plant},
                        {L("StorageLocation"), zmM020R.ZMM020R.StorageLocation},
                        {L("PurchaseGroup"), zmM020R.ZMM020R.PurchaseGroup},
                        {L("Requisitioner"), zmM020R.ZMM020R.Requisitioner},
                        {L("RequisitionerName"), zmM020R.ZMM020R.RequisitionerName},
                        {L("PurchasingDocument"), zmM020R.ZMM020R.PurchasingDocument},
                        {L("PurchaseOrderDate"), zmM020R.ZMM020R.PurchaseOrderDate},
                        {L("OutlineAgreement"), zmM020R.ZMM020R.OutlineAgreement},
                        {L("PrincAgreementItem"), zmM020R.ZMM020R.PrincAgreementItem},
                        {L("PurchasingInfoRec"), zmM020R.ZMM020R.PurchasingInfoRec},
                        {L("Status"), zmM020R.ZMM020R.Status},
                        {L("CreatedBy"), zmM020R.ZMM020R.CreatedBy},
                        {L("Currency"), zmM020R.ZMM020R.Currency},
                        {L("EntrySheet"), zmM020R.ZMM020R.EntrySheet},
                        {L("GoodsReceipt"), zmM020R.ZMM020R.GoodsReceipt},
                        {L("SupplierCode"), zmM020R.ZMM020R.SupplierCode},
                        {L("SupplierName"), zmM020R.ZMM020R.SupplierName},
                        {L("ReleaseIndicator"), zmM020R.ZMM020R.ReleaseIndicator},
                        {L("UnitPrice"), zmM020R.ZMM020R.UnitPrice},
                        {L("ValuationPrice"), zmM020R.ZMM020R.ValuationPrice},
                        {L("ItemText"), zmM020R.ZMM020R.ItemText},
                        {L("LongText"), zmM020R.ZMM020R.LongText},
                        {L("FirstApprovalDate"), zmM020R.ZMM020R.FirstApprovalDate},
                        {L("FirstApprovalName"), zmM020R.ZMM020R.FirstApprovalName},
                        {L("LastApprovalDate"), zmM020R.ZMM020R.LastApprovalDate},
                        {L("LastApprovalName"), zmM020R.ZMM020R.LastApprovalName},
                        {L("CostCenter"), zmM020R.ZMM020R.CostCenter},
                        {L("CostCenterDescription"), zmM020R.ZMM020R.CostCenterDescription},
                        {L("WBSElement"), zmM020R.ZMM020R.WBSElement},
                        {L("Asset"), zmM020R.ZMM020R.Asset},
                        {L("FundsCenter"), zmM020R.ZMM020R.FundsCenter},
                        {L("RemainQuantity"), zmM020R.ZMM020R.RemainQuantity},
                        {L("CreatedDate"), zmM020R.ZMM020R.CreatedDate},
                        {L("UpdatedDate"), zmM020R.ZMM020R.UpdatedDate},

                    });
            }

            return CreateExcelPackage("ZMM020RList.xlsx", items);

        }
    }
}