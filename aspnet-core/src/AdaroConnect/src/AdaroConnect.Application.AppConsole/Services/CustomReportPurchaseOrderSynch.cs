using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using AdaroConnect.Application.AppConsole.Entities;
using Azure;
using ExcelObjectMapper.Helpers;
using ExcelObjectMapper.Readers;
using Ganss.Excel;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;


namespace AdaroConnect.Application.AppConsole.Services
{
    public class CustomReportPurchaseOrderSynch :ICustomReportPurchaseOrderSynch
    {
        private readonly AdaroConfigurations _configuration;
        private readonly AdaroConnectContext _connectContext;
        public CustomReportPurchaseOrderSynch(AdaroConnectContext adaroConnectContext, IOptions<AdaroConfigurations> configuration)
        {
            _configuration = configuration.Value;
            _connectContext = adaroConnectContext;
        }

        public void DownloadExcel()
        {
            string SAP_EXCEL_PATH = _configuration.SAP_EXCEL_PATH;
            DirectoryInfo d = new DirectoryInfo(SAP_EXCEL_PATH); //Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("ZMM021R*.XLSX"); //Getting Text files

            List<Zmm021r> ZMM021R_DATAS = new List<Zmm021r>();

            if (Files.Length > 0)
            {
                var ZMM021Rs = Files.OrderBy(x => x.CreationTime).ToList();

                foreach (var zmm021r in ZMM021Rs)
                {
                    var excel = new ExcelMapper(zmm021r.FullName);
                    #region MAPPING EXCEL
                    excel.AddMapping<Zmm021r>("Purchasing Document", p => p.PurchasingDocument);
                    excel.AddMapping<Zmm021r>("Purchasing Document", p => p.PurchasingDocument);
                    excel.AddMapping<Zmm021r>("Purchasing Doc Type", p => p.PurchasingDocType);
                    excel.AddMapping<Zmm021r>("Purchasing Doc Type Desc", p => p.PurchasingDocTypeDescription);
                    excel.AddMapping<Zmm021r>("Item", p => p.Item);
                    excel.AddMapping<Zmm021r>("Line Number", p => p.LineNumber);
                    excel.AddMapping<Zmm021r>("Deletion Indicator", p => p.DeletionIndicator);
                    excel.AddMapping<Zmm021r>("Document Date", p => p.DocumentDate);
                    excel.AddMapping<Zmm021r>("Created On", p => p.CreatedOn);
                    excel.AddMapping<Zmm021r>("Purchase Requisition", p => p.PurchaseRequisition);
                    excel.AddMapping<Zmm021r>("Item PR", p => p.ItemPr);
                    excel.AddMapping<Zmm021r>("Name of Supplier", p => p.SupplierName);
                    excel.AddMapping<Zmm021r>("Address", p => p.Address);
                    excel.AddMapping<Zmm021r>("Material/Service No#", p => p.ItemNo);
                    excel.AddMapping<Zmm021r>("Material Group", p => p.MaterialGroup);
                    excel.AddMapping<Zmm021r>("Short Text", p => p.ShortText);
                    excel.AddMapping<Zmm021r>("Order Quantity", p => p.OrderQuantity);
                    excel.AddMapping<Zmm021r>("Order Unit", p => p.OrderUnit);
                    excel.AddMapping<Zmm021r>("Currency", p => p.Currency);
                    excel.AddMapping<Zmm021r>("Delivery Date", p => p.DeliveryDate);
                    excel.AddMapping<Zmm021r>("Net price", p => p.NetPrice);
                    excel.AddMapping<Zmm021r>("Net Order Value", p => p.NetOrderValue);
                    excel.AddMapping<Zmm021r>("Demurrage", p => p.Demurrage);
                    excel.AddMapping<Zmm021r>("Gross Price", p => p.GrossPrice);
                    excel.AddMapping<Zmm021r>("Total Discount", p => p.TotalDiscount);
                    excel.AddMapping<Zmm021r>("Freight Cost", p => p.FreightCost);
                    excel.AddMapping<Zmm021r>("Release Indicator", p => p.ReleaseIndicator);
                    excel.AddMapping<Zmm021r>("Plant", p => p.Plant);
                    excel.AddMapping<Zmm021r>("Purchasing Group", p => p.PurchasingGroup);
                    excel.AddMapping<Zmm021r>("Tax Code", p => p.TaxCode);
                    excel.AddMapping<Zmm021r>("Collective Number", p => p.CollectiveNumber);
                    excel.AddMapping<Zmm021r>("Item Category", p => p.ItemCategory);
                    excel.AddMapping<Zmm021r>("Acct# Assignment", p => p.AccountAssignment);
                    excel.AddMapping<Zmm021r>("Outline Agreement", p => p.OutlineAgreement);
                    excel.AddMapping<Zmm021r>("RFQ No#", p => p.Rfqno);
                    excel.AddMapping<Zmm021r>("Still to be Delivered (Qty)", p => p.QtyPending);
                    excel.AddMapping<Zmm021r>("Material/Service", p => p.MaterialService);
                    excel.AddMapping<Zmm021r>("Approval Status", p => p.ApprovalStatus);
                    excel.AddMapping<Zmm021r>("PO Status", p => p.Postatus);
                    excel.AddMapping<Zmm021r>("Period", p => p.Period);
                    excel.AddMapping<Zmm021r>("Comment for Vendor", p => p.CommentVendor);
                    excel.AddMapping<Zmm021r>("Item Text", p => p.ItemText);
                    excel.AddMapping<Zmm021r>("Long Text", p => p.LongText);
                    excel.AddMapping<Zmm021r>("Our Reference", p => p.OurReference);
                    excel.AddMapping<Zmm021r>("PR Final First Approval Date", p => p.PrfinalFirstApprovalDate);
                    excel.AddMapping<Zmm021r>("PR Final Last Approval Date", p => p.PrfinalLastApprovalDate);
                    excel.AddMapping<Zmm021r>("PO First Approval Date", p => p.PofirstApprovalDate);
                    excel.AddMapping<Zmm021r>("PO Last Approval Date", p => p.PolastApprovalDate);
                    excel.AddMapping<Zmm021r>("PO Approval Name", p => p.PoapprovalName);
                    excel.AddMapping<Zmm021r>("Buyer", p => p.BuyerName);
                    excel.AddMapping<Zmm021r>("PIC(Dept)", p => p.Picdept);
                    excel.AddMapping<Zmm021r>("PIC(Sect)", p => p.Picsect);
                    excel.AddMapping<Zmm021r>("Fuel Allocation", p => p.FuelAllocation);
                    excel.AddMapping<Zmm021r>("Cost Center", p => p.CostCenter);
                    excel.AddMapping<Zmm021r>("WBS Element", p => p.Wbselement);
                    excel.AddMapping<Zmm021r>("Asset No#", p => p.AssetNo);
                    excel.AddMapping<Zmm021r>("Fund Center", p => p.FundCenter);
                    #endregion

                    var datas = excel.Fetch<Zmm021r>().ToList();

                    #region Cleaning Data
                    datas.ForEach(x => {
                        x.Id = Guid.NewGuid();
                        x.DocumentId = $"{x.PurchasingDocument}{x.Item}{x.LineNumber}";
                        if (!string.IsNullOrEmpty(x.SupplierName))
                        {
                            var supplier = x.SupplierName.Split("-");
                            x.SupplierCode = supplier[0].Trim();
                            x.SupplierName = supplier[1].Trim();
                        }

                        if (!string.IsNullOrEmpty(x.BuyerName))
                        {
                            var Buyer = x.BuyerName.Split("-");
                            x.BuyerCode = Buyer[0].Trim();
                            x.BuyerName = Buyer[1].Trim();
                        }

                        if (!string.IsNullOrEmpty(x.CostCenter))
                        {
                            var CostCenter = x.CostCenter.Split("-");
                            x.CostCenter = CostCenter[0].Trim();
                            x.CostCenterDescription = CostCenter[1].Trim();
                        }

                        x.CreatedDate = DateTime.Now;
                        x.UpdatedDate = DateTime.Now;

                    });
                    #endregion

                    ZMM021R_DATAS.AddRange(datas);

                    
                    if (!Directory.Exists(Path.Combine(SAP_EXCEL_PATH, "OLD_DATA")))
                    {
                        Directory.CreateDirectory(Path.Combine(SAP_EXCEL_PATH, "OLD_DATA"));
                    }

                    zmm021r.MoveTo(Path.Combine(SAP_EXCEL_PATH, "OLD_DATA", zmm021r.Name));

                    //zmm021r.Delete();
                }


                ZMM021R_DATAS = ZMM021R_DATAS.GroupBy(l => l.DocumentId)
                    .Select(g => g.OrderByDescending(c => c.CreatedDate).FirstOrDefault())
                    .ToList();

                List<Zmm021r> ZMM021R_NEW = new List<Zmm021r>();
                List<Zmm021r> ZMM021R_UPDATES = new List<Zmm021r>();

                foreach (var ZMM021R_ITEM in ZMM021R_DATAS)
                {
                    var ZMM021R_UPDATE = _connectContext.Zmm021rs.FirstOrDefault(x => x.DocumentId == ZMM021R_ITEM.DocumentId);
                    if(ZMM021R_UPDATE != null)
                    {
                        #region UPDATE
                        ZMM021R_UPDATE.PurchasingDocument = ZMM021R_ITEM.PurchasingDocument;
                        ZMM021R_UPDATE.PurchasingDocType = ZMM021R_ITEM.PurchasingDocType;
                        ZMM021R_UPDATE.PurchasingDocTypeDescription = ZMM021R_ITEM.PurchasingDocTypeDescription;
                        ZMM021R_UPDATE.Item = ZMM021R_ITEM.Item;
                        ZMM021R_UPDATE.LineNumber = ZMM021R_ITEM.LineNumber;
                        ZMM021R_UPDATE.DeletionIndicator = ZMM021R_ITEM.DeletionIndicator;
                        ZMM021R_UPDATE.DocumentDate = ZMM021R_ITEM.DocumentDate;
                        ZMM021R_UPDATE.CreatedOn = ZMM021R_ITEM.CreatedOn;
                        ZMM021R_UPDATE.PurchaseRequisition = ZMM021R_ITEM.PurchaseRequisition;
                        ZMM021R_UPDATE.ItemPr = ZMM021R_ITEM.ItemPr;
                        ZMM021R_UPDATE.SupplierCode = ZMM021R_ITEM.SupplierCode;
                        ZMM021R_UPDATE.SupplierName = ZMM021R_ITEM.SupplierName;
                        ZMM021R_UPDATE.Address = ZMM021R_ITEM.Address;
                        ZMM021R_UPDATE.ItemNo = ZMM021R_ITEM.ItemNo;
                        ZMM021R_UPDATE.MaterialGroup = ZMM021R_ITEM.MaterialGroup;
                        ZMM021R_UPDATE.ShortText = ZMM021R_ITEM.ShortText;
                        ZMM021R_UPDATE.OrderQuantity = ZMM021R_ITEM.OrderQuantity;
                        ZMM021R_UPDATE.OrderUnit = ZMM021R_ITEM.OrderUnit;
                        ZMM021R_UPDATE.Currency = ZMM021R_ITEM.Currency;
                        ZMM021R_UPDATE.DeliveryDate = ZMM021R_ITEM.DeliveryDate;
                        ZMM021R_UPDATE.NetPrice = ZMM021R_ITEM.NetPrice;
                        ZMM021R_UPDATE.NetOrderValue = ZMM021R_ITEM.NetOrderValue;
                        ZMM021R_UPDATE.Demurrage = ZMM021R_ITEM.Demurrage;
                        ZMM021R_UPDATE.GrossPrice = ZMM021R_ITEM.GrossPrice;
                        ZMM021R_UPDATE.TotalDiscount = ZMM021R_ITEM.TotalDiscount;
                        ZMM021R_UPDATE.FreightCost = ZMM021R_ITEM.FreightCost;
                        ZMM021R_UPDATE.ReleaseIndicator = ZMM021R_ITEM.ReleaseIndicator;
                        ZMM021R_UPDATE.Plant = ZMM021R_ITEM.Plant;
                        ZMM021R_UPDATE.PurchasingGroup = ZMM021R_ITEM.PurchasingGroup;
                        ZMM021R_UPDATE.TaxCode = ZMM021R_ITEM.TaxCode;
                        ZMM021R_UPDATE.CollectiveNumber = ZMM021R_ITEM.CollectiveNumber;
                        ZMM021R_UPDATE.ItemCategory = ZMM021R_ITEM.ItemCategory;
                        ZMM021R_UPDATE.AccountAssignment = ZMM021R_ITEM.AccountAssignment;
                        ZMM021R_UPDATE.OutlineAgreement = ZMM021R_ITEM.OutlineAgreement;
                        ZMM021R_UPDATE.Rfqno = ZMM021R_ITEM.Rfqno;
                        ZMM021R_UPDATE.QtyPending = ZMM021R_ITEM.QtyPending;
                        ZMM021R_UPDATE.MaterialService = ZMM021R_ITEM.MaterialService;
                        ZMM021R_UPDATE.ApprovalStatus = ZMM021R_ITEM.ApprovalStatus;
                        ZMM021R_UPDATE.Postatus = ZMM021R_ITEM.Postatus;
                        ZMM021R_UPDATE.Period = ZMM021R_ITEM.Period;
                        ZMM021R_UPDATE.CommentVendor = ZMM021R_ITEM.CommentVendor;
                        ZMM021R_UPDATE.ItemText = ZMM021R_ITEM.ItemText;
                        ZMM021R_UPDATE.LongText = ZMM021R_ITEM.LongText;
                        ZMM021R_UPDATE.OurReference = ZMM021R_ITEM.OurReference;
                        ZMM021R_UPDATE.PrfinalFirstApprovalDate = ZMM021R_ITEM.PrfinalFirstApprovalDate;
                        ZMM021R_UPDATE.PrfinalLastApprovalDate = ZMM021R_ITEM.PrfinalLastApprovalDate;
                        ZMM021R_UPDATE.PofirstApprovalDate = ZMM021R_ITEM.PofirstApprovalDate;
                        ZMM021R_UPDATE.PolastApprovalDate = ZMM021R_ITEM.PolastApprovalDate;
                        ZMM021R_UPDATE.PoapprovalName = ZMM021R_ITEM.PoapprovalName;
                        ZMM021R_UPDATE.BuyerCode = ZMM021R_ITEM.BuyerCode;
                        ZMM021R_UPDATE.BuyerName = ZMM021R_ITEM.BuyerName;
                        ZMM021R_UPDATE.Picdept = ZMM021R_ITEM.Picdept;
                        ZMM021R_UPDATE.Picsect = ZMM021R_ITEM.Picsect;
                        ZMM021R_UPDATE.FuelAllocation = ZMM021R_ITEM.FuelAllocation;
                        ZMM021R_UPDATE.CostCenter = ZMM021R_ITEM.CostCenter;
                        ZMM021R_UPDATE.CostCenterDescription = ZMM021R_ITEM.CostCenterDescription;
                        ZMM021R_UPDATE.Wbselement = ZMM021R_ITEM.Wbselement;
                        ZMM021R_UPDATE.AssetNo = ZMM021R_ITEM.AssetNo;
                        ZMM021R_UPDATE.FundCenter = ZMM021R_ITEM.FundCenter;
                        ZMM021R_UPDATE.UpdatedDate = DateTime.Now;
                        #endregion

                        ZMM021R_UPDATES.Add(ZMM021R_UPDATE);
                    } else
                    {
                        ZMM021R_NEW.Add(ZMM021R_ITEM);
                    }
                }

                if (ZMM021R_UPDATES.Count > 0)
                    _connectContext.Zmm021rs.UpdateRange(ZMM021R_UPDATES);

                if (ZMM021R_NEW.Count > 0)
                    _connectContext.Zmm021rs.AddRange(ZMM021R_NEW);

                _connectContext.SaveChanges();

            }

        }

        //public void SynchDatabase()
        //{


            
        //}
    }
}
