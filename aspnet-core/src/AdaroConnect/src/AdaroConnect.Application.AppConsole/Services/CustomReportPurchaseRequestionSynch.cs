using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
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
using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using OfficeOpenXml;


namespace AdaroConnect.Application.AppConsole.Services
{
    public class CustomReportPurchaseRequestionSynch : ICustomReportPurchaseRequestionSynch
    {
        private readonly AdaroConfigurations _configuration;
        private readonly AdaroConnectContext _connectContext;
        public CustomReportPurchaseRequestionSynch(AdaroConnectContext adaroConnectContext, IOptions<AdaroConfigurations> configuration)
        {
            _configuration = configuration.Value;
            _connectContext = adaroConnectContext;
        }

        public void SynchronizeData()
        {
            //GenerateScript();
            ReadExcel();
        }

        private void ReadExcel()
        {
            System.Console.WriteLine("====== STARTING SYNCH ===========");

            string SAP_EXCEL_PATH = _configuration.SAP_EXCEL_PATH;
            DirectoryInfo d = new DirectoryInfo(SAP_EXCEL_PATH); //Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("ZMM020R*.XLSX"); //Getting Text files

            List<Zmm020r> Zmm020r_DATAS = new List<Zmm020r>();

            if (Files.Length > 0)
            {
                var Zmm020rs = Files.OrderBy(x => x.CreationTime).ToList();
                Console.WriteLine($"Total Files : {Zmm020rs.Count}");

                foreach (var Zmm020r in Zmm020rs)
                {
                    Console.WriteLine($"Synch File : {Zmm020r.Name}");

                    var excel = new ExcelMapper(Zmm020r.FullName);
                    #region MAPPING EXCEL
                    excel.AddMapping<Zmm020r>("Purchase Requisition", p => p.PurchaseRequisition);
                    excel.AddMapping<Zmm020r>("Document Type", p => p.DocumentType);
                    excel.AddMapping<Zmm020r>("Item of requisition", p => p.ItemRequisition);
                    excel.AddMapping<Zmm020r>("Processing Status", p => p.ProcessingStatus);
                    excel.AddMapping<Zmm020r>("Deletion indicator", p => p.DeletionIndicator);
                    excel.AddMapping<Zmm020r>("Item category", p => p.ItemCategory);
                    excel.AddMapping<Zmm020r>("Acct Assignment Cat#", p => p.AccountAssignment);
                    excel.AddMapping<Zmm020r>("Material", p => p.Material);
                    excel.AddMapping<Zmm020r>("Short Text", p => p.ShortText);
                    excel.AddMapping<Zmm020r>("Quantity requested", p => p.QuantityRequested);
                    excel.AddMapping<Zmm020r>("Unit of Measure", p => p.UnitOfMeasure);
                    excel.AddMapping<Zmm020r>("Service Item", p => p.ServiceItem);
                    excel.AddMapping<Zmm020r>("Service", p => p.Service);
                    excel.AddMapping<Zmm020r>("Service Short Text", p => p.ServiceShortText);
                    excel.AddMapping<Zmm020r>("Quantity Service", p => p.QuantityService);
                    excel.AddMapping<Zmm020r>("UoM Service", p => p.UnitOfMeasureService);
                    excel.AddMapping<Zmm020r>("Deliv# date(From/to)", p => p.DeliveryDate);
                    excel.AddMapping<Zmm020r>("Material Group", p => p.MaterialGroup);
                    excel.AddMapping<Zmm020r>("Plant", p => p.Plant);
                    excel.AddMapping<Zmm020r>("Storage location", p => p.StorageLocation);
                    excel.AddMapping<Zmm020r>("Purchase Group", p => p.PurchaseGroup);
                    excel.AddMapping<Zmm020r>("Requisitioner", p => p.Requisitioner);
                    excel.AddMapping<Zmm020r>("Purchasing Document", p => p.PurchasingDocument);
                    excel.AddMapping<Zmm020r>("Purchase Order Date", p => p.PurchaseOrderDate);
                    excel.AddMapping<Zmm020r>("Outline agreement", p => p.OutlineAgreement);
                    excel.AddMapping<Zmm020r>("Princ#agreement item", p => p.PrincAgreementItem);
                    excel.AddMapping<Zmm020r>("Purchasing info rec#", p => p.PurchasingInfoRec);
                    excel.AddMapping<Zmm020r>("Status", p => p.Status);
                    excel.AddMapping<Zmm020r>("Created By", p => p.CreatedBy);
                    excel.AddMapping<Zmm020r>("Currency", p => p.Currency);
                    excel.AddMapping<Zmm020r>("Entry Sheet", p => p.EntrySheet);
                    excel.AddMapping<Zmm020r>("Goods Receipt", p => p.GoodsReceipt);
                    excel.AddMapping<Zmm020r>("Supplier Name", p => p.SupplierName);
                    excel.AddMapping<Zmm020r>("Release Indicator", p => p.ReleaseIndicator);
                    excel.AddMapping<Zmm020r>("Unit Price", p => p.UnitPrice);
                    excel.AddMapping<Zmm020r>("Valuation Price", p => p.ValuationPrice);
                    excel.AddMapping<Zmm020r>("Item Text", p => p.ItemText);
                    excel.AddMapping<Zmm020r>("Long Text", p => p.LongText);
                    excel.AddMapping<Zmm020r>("First Approval Date", p => p.FirstApprovalDate);
                    excel.AddMapping<Zmm020r>("First Approval Name", p => p.FirstApprovalName);
                    excel.AddMapping<Zmm020r>("Last Approval Date", p => p.LastApprovalDate);
                    excel.AddMapping<Zmm020r>("Last Approval Name", p => p.LastApprovalName);
                    excel.AddMapping<Zmm020r>("Cost Center", p => p.CostCenter);
                    excel.AddMapping<Zmm020r>("WBS Element", p => p.Wbselement);
                    excel.AddMapping<Zmm020r>("Asset", p => p.Asset);
                    excel.AddMapping<Zmm020r>("Funds center", p => p.FundsCenter);
                    excel.AddMapping<Zmm020r>("Remain Quantity", p => p.RemainQuantity);


                    #endregion

                    var datas = excel.Fetch<Zmm020r>().ToList();
                    Console.WriteLine($"Total Rows : {datas.Count}");


                    #region Cleaning Data
                    datas.ForEach(x => {
                        x.Id = Guid.NewGuid();
                        x.DocumentId = $"{x.PurchaseRequisition}{x.ItemRequisition}";
                        if (!string.IsNullOrEmpty(x.DocumentType))
                        {
                            var documentType = x.DocumentType.Split("-");
                            x.DocumentType = documentType[0].Trim();
                            x.DocumentTypeText = documentType[1].Trim();
                        }

                        if (!string.IsNullOrEmpty(x.ProcessingStatus))
                        {
                            var processingStatus = x.ProcessingStatus.Split("-");
                            x.ProcessingStatusCode = processingStatus[0].Trim();
                            x.ProcessingStatus = processingStatus[1].Trim();
                        }

                        if (!string.IsNullOrEmpty(x.SupplierName))
                        {
                            var supplier = x.SupplierName.Split("-");
                            x.SupplierCode = supplier[0].Trim();
                            x.SupplierName = supplier[1].Trim();                            
                        }

                        if (!string.IsNullOrEmpty(x.Requisitioner))
                        {
                            var requisitioner = x.Requisitioner.Split("-");
                            x.Requisitioner = requisitioner[0].Trim();
                            x.RequisitionerName = requisitioner[1].Trim();
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

                    Zmm020r_DATAS.AddRange(datas);
                }

                Console.WriteLine($"Total All Rows : {Zmm020r_DATAS.Count}");


                Zmm020r_DATAS = Zmm020r_DATAS.GroupBy(l => l.DocumentId)
                    .Select(g => g.OrderByDescending(c => c.CreatedDate).FirstOrDefault())
                    .ToList();

                Console.WriteLine($"Total Clean Rows : {Zmm020r_DATAS.Count}");

                List<Zmm020r> Zmm020r_NEW = new List<Zmm020r>();
                List<Zmm020r> Zmm020r_UPDATES = new List<Zmm020r>();

                foreach (var Zmm020r_ITEM in Zmm020r_DATAS)
                {
                    var Zmm020r_UPDATE = _connectContext.Zmm020rs.FirstOrDefault(x => x.DocumentId == Zmm020r_ITEM.DocumentId);
                    if (Zmm020r_UPDATE != null)
                    {
                        #region UPDATE
                        Zmm020r_UPDATE.PurchaseRequisition = Zmm020r_ITEM.PurchaseRequisition;
                        Zmm020r_UPDATE.DocumentType = Zmm020r_ITEM.DocumentType;
                        Zmm020r_UPDATE.DocumentTypeText = Zmm020r_ITEM.DocumentTypeText;
                        Zmm020r_UPDATE.ItemRequisition = Zmm020r_ITEM.ItemRequisition;
                        Zmm020r_UPDATE.ProcessingStatusCode = Zmm020r_ITEM.ProcessingStatusCode;
                        Zmm020r_UPDATE.ProcessingStatus = Zmm020r_ITEM.ProcessingStatus;
                        Zmm020r_UPDATE.DeletionIndicator = Zmm020r_ITEM.DeletionIndicator;
                        Zmm020r_UPDATE.ItemCategory = Zmm020r_ITEM.ItemCategory;
                        Zmm020r_UPDATE.AccountAssignment = Zmm020r_ITEM.AccountAssignment;
                        Zmm020r_UPDATE.Material = Zmm020r_ITEM.Material;
                        Zmm020r_UPDATE.ShortText = Zmm020r_ITEM.ShortText;
                        Zmm020r_UPDATE.QuantityRequested = Zmm020r_ITEM.QuantityRequested;
                        Zmm020r_UPDATE.UnitOfMeasure = Zmm020r_ITEM.UnitOfMeasure;
                        Zmm020r_UPDATE.ServiceItem = Zmm020r_ITEM.ServiceItem;
                        Zmm020r_UPDATE.Service = Zmm020r_ITEM.Service;
                        Zmm020r_UPDATE.ServiceShortText = Zmm020r_ITEM.ServiceShortText;
                        Zmm020r_UPDATE.QuantityService = Zmm020r_ITEM.QuantityService;
                        Zmm020r_UPDATE.UnitOfMeasureService = Zmm020r_ITEM.UnitOfMeasureService;
                        Zmm020r_UPDATE.DeliveryDate = Zmm020r_ITEM.DeliveryDate;
                        Zmm020r_UPDATE.MaterialGroup = Zmm020r_ITEM.MaterialGroup;
                        Zmm020r_UPDATE.Plant = Zmm020r_ITEM.Plant;
                        Zmm020r_UPDATE.StorageLocation = Zmm020r_ITEM.StorageLocation;
                        Zmm020r_UPDATE.PurchaseGroup = Zmm020r_ITEM.PurchaseGroup;
                        Zmm020r_UPDATE.Requisitioner = Zmm020r_ITEM.Requisitioner;
                        Zmm020r_UPDATE.RequisitionerName = Zmm020r_ITEM.RequisitionerName;
                        Zmm020r_UPDATE.PurchasingDocument = Zmm020r_ITEM.PurchasingDocument;
                        Zmm020r_UPDATE.PurchaseOrderDate = Zmm020r_ITEM.PurchaseOrderDate;
                        Zmm020r_UPDATE.OutlineAgreement = Zmm020r_ITEM.OutlineAgreement;
                        Zmm020r_UPDATE.PrincAgreementItem = Zmm020r_ITEM.PrincAgreementItem;
                        Zmm020r_UPDATE.PurchasingInfoRec = Zmm020r_ITEM.PurchasingInfoRec;
                        Zmm020r_UPDATE.Status = Zmm020r_ITEM.Status;
                        Zmm020r_UPDATE.CreatedBy = Zmm020r_ITEM.CreatedBy;
                        Zmm020r_UPDATE.Currency = Zmm020r_ITEM.Currency;
                        Zmm020r_UPDATE.EntrySheet = Zmm020r_ITEM.EntrySheet;
                        Zmm020r_UPDATE.GoodsReceipt = Zmm020r_ITEM.GoodsReceipt;
                        Zmm020r_UPDATE.SupplierCode = Zmm020r_ITEM.SupplierCode;
                        Zmm020r_UPDATE.SupplierName = Zmm020r_ITEM.SupplierName;
                        Zmm020r_UPDATE.ReleaseIndicator = Zmm020r_ITEM.ReleaseIndicator;
                        Zmm020r_UPDATE.UnitPrice = Zmm020r_ITEM.UnitPrice;
                        Zmm020r_UPDATE.ValuationPrice = Zmm020r_ITEM.ValuationPrice;
                        Zmm020r_UPDATE.ItemText = Zmm020r_ITEM.ItemText;
                        Zmm020r_UPDATE.LongText = Zmm020r_ITEM.LongText;
                        Zmm020r_UPDATE.FirstApprovalDate = Zmm020r_ITEM.FirstApprovalDate;
                        Zmm020r_UPDATE.FirstApprovalName = Zmm020r_ITEM.FirstApprovalName;
                        Zmm020r_UPDATE.LastApprovalDate = Zmm020r_ITEM.LastApprovalDate;
                        Zmm020r_UPDATE.LastApprovalName = Zmm020r_ITEM.LastApprovalName;
                        Zmm020r_UPDATE.CostCenter = Zmm020r_ITEM.CostCenter;
                        Zmm020r_UPDATE.CostCenterDescription = Zmm020r_ITEM.CostCenterDescription;
                        Zmm020r_UPDATE.Wbselement = Zmm020r_ITEM.Wbselement;
                        Zmm020r_UPDATE.Asset = Zmm020r_ITEM.Asset;
                        Zmm020r_UPDATE.FundsCenter = Zmm020r_ITEM.FundsCenter;
                        Zmm020r_UPDATE.RemainQuantity = Zmm020r_ITEM.RemainQuantity;
                        Zmm020r_UPDATE.UpdatedDate = Zmm020r_ITEM.UpdatedDate;
                        #endregion

                        Zmm020r_UPDATES.Add(Zmm020r_UPDATE);
                    }
                    else
                    {
                        Zmm020r_NEW.Add(Zmm020r_ITEM);
                    }
                }

                if (Zmm020r_UPDATES.Count > 0)
                {
                    _connectContext.Zmm020rs.UpdateRange(Zmm020r_UPDATES);
                    Console.WriteLine($"Total Update Rows : {Zmm020r_UPDATES.Count}");
                }

                if (Zmm020r_NEW.Count > 0)
                {
                    _connectContext.Zmm020rs.AddRange(Zmm020r_NEW);
                    Console.WriteLine($"Total Insert Rows : {Zmm020r_NEW.Count}");
                }

                if ((Zmm020r_UPDATES.Count + Zmm020r_NEW.Count) > 0)
                {
                    _connectContext.SaveChanges();
                    foreach (var Zmm020r in Zmm020rs)
                    {
                        if (!Directory.Exists(Path.Combine(SAP_EXCEL_PATH, "OLD_DATA")))
                        {
                            Directory.CreateDirectory(Path.Combine(SAP_EXCEL_PATH, "OLD_DATA"));
                        }

                        Zmm020r.MoveTo(Path.Combine(SAP_EXCEL_PATH, "OLD_DATA", Zmm020r.Name));
                    }
                }
            }
            else
            {
                System.Console.WriteLine("File not exists");
            }

            System.Console.WriteLine("====== END SYNCH ===========");
        }
        private void GenerateScript()
        {
            var PathCurrent = System.IO.Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
            var SCRIPT_TEMPLATE = File.ReadAllText(Path.Combine(PathCurrent, "SCRIPT_TEMPLATE", "Zmm020r.txt"));

            var DateCurrent = DateTime.Now;
            var DateFrom = DateCurrent.AddYears(-1);
            DateFrom = new DateTime(DateFrom.Year, DateFrom.Month, 1);

            SCRIPT_TEMPLATE = SCRIPT_TEMPLATE
                .Replace("[DATE_FROM]", DateFrom.ToString("dd.MM.yyyy"))
                .Replace("[DATE_TO]", DateCurrent.ToString("dd.MM.yyyy"))
                .Replace("[PATH_FILE]", _configuration.SAP_EXCEL_PATH)
                .Replace("[FILE_NAME]", $"Zmm020r_{DateFrom.ToString("ddMMyyyy")}_{DateCurrent.ToString("ddMMyyyy")}_{DateCurrent.ToString("HHmmss")}.XLSX");


            string FILE_NAME = Path.Combine(_configuration.SAP_EXCEL_PATH, "SAP_SCRIPT", "Zmm020r.vbs");

            try
            {
                // Check if file already exists. If yes, delete it.
                if (File.Exists(FILE_NAME))
                {
                    File.Delete(FILE_NAME);
                }

                // Create a new file
                using (StreamWriter sw = File.CreateText(FILE_NAME))
                {
                    sw.WriteLine(SCRIPT_TEMPLATE);
                }


            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.ToString());
            }


        }

        private void RunningSAP()
        {
            Process p = new Process();
            p.StartInfo.FileName = "C:\\PrintingArgs.exe";
            p.StartInfo.Arguments = "1 2 3 4 5 6 7 8 9 10 11 12 13 14 15 16 17 18";
            p.Start();
        }
    }
}
