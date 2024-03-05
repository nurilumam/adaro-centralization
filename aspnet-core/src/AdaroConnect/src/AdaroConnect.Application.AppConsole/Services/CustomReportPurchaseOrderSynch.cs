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

                    });
                    #endregion

                    Console.WriteLine(datas.Count);

                    _connectContext.Zmm021rs.AddRange(datas);
                    _connectContext.SaveChanges();
                }



            }

        }

        //public void SynchDatabase()
        //{


            
        //}
    }
}
