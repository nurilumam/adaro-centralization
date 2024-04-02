using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using AdaroConnect.Application.AppConsole.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Options;


namespace AdaroConnect.Application.AppConsole.Services
{
    public class GeneralLedgerMap : ClassMap<GeneralLedgerAccount>
    {
        public GeneralLedgerMap()
        {
            Map(m => m.FundsCenter).Name("Funds Center/Commitment Item");
            Map(m => m.ConsumableBudget).Name("Consumable Budget");
            Map(m => m.ConsumedBudget).Name("Consumed Budget");
            Map(m => m.AvailableAmount).Name("Available Amount");
            Map(m => m.CurrentBudget).Name("Current Budget");
            Map(m => m.CommitmentActuals).Name("Commitment/Actuals");
        }
    }

    public class GLAccountSynch : IGLAccountSynch
    {
        private readonly AdaroConfigurations _configuration;
        private readonly AdaroConnectContext _connectContext;
        public GLAccountSynch(AdaroConnectContext adaroConnectContext, IOptions<AdaroConfigurations> configuration)
        {
            _configuration = configuration.Value;
            _connectContext = adaroConnectContext;
        }

        public void SynchronizeData()
        {
            System.Console.WriteLine("====== STARTING SYNCH ===========");

            string SAP_EXCEL_PATH = _configuration.SAP_EXCEL_PATH;
            DirectoryInfo d = new DirectoryInfo(SAP_EXCEL_PATH); //Assuming Test is your Folder
            FileInfo[] Files = d.GetFiles("FMAVCR01*.DAT"); //Getting Text files

            List<GeneralLedgerAccount> GLAccountDatas = new List<GeneralLedgerAccount>();
            List<GeneralLedgerAccount> GLAccountNew = new List<GeneralLedgerAccount>();
            List<GeneralLedgerAccount> GLAccountUpdates = new List<GeneralLedgerAccount>();
            List<CostCenter> CostCenterNew = new List<CostCenter>();


            if (Files.Length > 0)
            {
                var GeneralLedgerAccounts = Files.OrderBy(x => x.CreationTime).ToList();
                Console.WriteLine($"Total Files : {GeneralLedgerAccounts.Count}");

                CsvConfiguration  myConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
                myConfig.Delimiter = "\t";

                foreach (var GLAccountFile in GeneralLedgerAccounts)
                {
                    using (var reader = new StreamReader(GLAccountFile.FullName))           
                    using (var csv = new CsvReader(reader, myConfig))
                    {
                        csv.Context.RegisterClassMap<GeneralLedgerMap>();
                        var records = csv.GetRecords<GeneralLedgerAccount>().ToList();
                        Console.WriteLine($"Total Rows : {records.Count()}");
                        Console.WriteLine($"Synch File : {GLAccountFile.Name}");
                        GLAccountDatas.AddRange(records);
                    }
                }

                Console.WriteLine($"Total All Rows : {GLAccountDatas.Count}");

                #region Cleaning Data
                CostCenter costCenter = new CostCenter();

                GLAccountDatas.ForEach(x => {
                    x.Id = Guid.NewGuid();

                    var FundsCenter = x.FundsCenter;
                    x.FundsCenter = FundsCenter.Split(" ")[0].Trim();
                    x.FundsCenterDescription = FundsCenter.Replace(x.FundsCenter, "").Trim();
                    int FirstCode = 0;
                    int.TryParse(x.FundsCenter.Substring(0, 1), out FirstCode);


                    if (
                        costCenter.CostCenterCode != x.FundsCenter &&
                        FirstCode != 5
                    )
                    {
                        costCenter = CostCenterNew.FirstOrDefault(y => y.CostCenterCode == x.FundsCenter);
                        if(costCenter == null)
                        {
                            costCenter = _connectContext.CostCenters.FirstOrDefault(y => y.CostCenterCode == x.FundsCenter);
                            if (costCenter != null)
                            {
                                costCenter.Id = costCenter.Id;
                            }
                            else
                            {
                                costCenter = new CostCenter()
                                {
                                    Id = Guid.NewGuid(),
                                    CostCenterCode = x.FundsCenter,
                                    ControllingArea = "2000",
                                    CostCenterShort = x.FundsCenterDescription,
                                    CostCenterName = x.FundsCenterDescription,
                                    DepartmentName = string.Empty,
                                    Description = string.Empty,
                                    ActState = string.Empty,
                                    IsActive = true,
                                    Period = DateTime.Now.Year.ToString()
                                };

                                CostCenterNew.Add(costCenter);
                            }
                        }
                    }


                    if(FirstCode==5) x.CostCenterId = costCenter.Id;

                    x.CreatedDate = DateTime.Now;
                    x.UpdatedDate = DateTime.Now;
                });


                GLAccountDatas = GLAccountDatas
                    .Where(x => x.CostCenterId != Guid.Empty)
                    .ToList();

                #endregion



                Console.WriteLine($"Total Clean Rows : {GLAccountDatas.Count}");

                foreach (var GLAccountItem in GLAccountDatas)
                {
                    var GLAccountUpdate = _connectContext.GeneralLedgerAccounts.FirstOrDefault(x => x.CostCenterId == GLAccountItem.CostCenterId && x.FundsCenter == GLAccountItem.FundsCenter);
                    if (GLAccountUpdate != null)
                    {
                        #region UPDATE
                        GLAccountUpdate.FundsCenterDescription = GLAccountItem.FundsCenterDescription;
                        GLAccountUpdate.CostCenterId = GLAccountItem.CostCenterId;
                        GLAccountUpdate.ConsumableBudget = GLAccountItem.ConsumableBudget;
                        GLAccountUpdate.ConsumedBudget = GLAccountItem.ConsumedBudget;
                        GLAccountUpdate.AvailableAmount = GLAccountItem.AvailableAmount;
                        GLAccountUpdate.CurrentBudget = GLAccountItem.CurrentBudget;
                        GLAccountUpdate.CommitmentActuals = GLAccountItem.CommitmentActuals;
                        GLAccountUpdate.UpdatedDate = DateTime.Now;
                        #endregion

                        GLAccountUpdates.Add(GLAccountUpdate);
                    }
                    else
                    {
                        GLAccountNew.Add(GLAccountItem);
                    }

                }
                

                if (CostCenterNew.Count > 0)
                {
                    _connectContext.CostCenters.AddRange(CostCenterNew);
                    Console.WriteLine($"Total Insert Rows : {CostCenterNew.Count}");
                }

                if (GLAccountUpdates.Count > 0)
                {
                    _connectContext.GeneralLedgerAccounts.UpdateRange(GLAccountUpdates);
                    Console.WriteLine($"Total Update Rows : {GLAccountUpdates.Count}");
                }

                if (GLAccountNew.Count > 0)
                {
                    _connectContext.GeneralLedgerAccounts.AddRange(GLAccountNew);
                    Console.WriteLine($"Total Insert Rows : {GLAccountNew.Count}");
                }

                if ((CostCenterNew.Count + GLAccountUpdates.Count + GLAccountNew.Count) > 0)
                {
                    _connectContext.SaveChanges();
                    foreach (var GeneralLedgerAccount in GeneralLedgerAccounts)
                    {
                        if (!Directory.Exists(Path.Combine(SAP_EXCEL_PATH, "OLD_DATA")))
                        {
                            Directory.CreateDirectory(Path.Combine(SAP_EXCEL_PATH, "OLD_DATA"));
                        }

                        GeneralLedgerAccount.MoveTo(Path.Combine(SAP_EXCEL_PATH, "OLD_DATA", GeneralLedgerAccount.Name));
                    }
                }
            }
            else
            {
                System.Console.WriteLine("File not exists");
            }

            System.Console.WriteLine("====== END SYNCH ===========");
        }
    }
}
