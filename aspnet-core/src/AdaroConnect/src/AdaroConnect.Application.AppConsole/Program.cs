using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AdaroConnect.Models;
using AdaroConnect.Application.Core.Abstracts;
using AdaroConnect.Application.Core.Extensions;
using AdaroConnect.Application.Core.Models;
using AdaroConnect.Application.AppConsole.Services;
using Microsoft.Extensions.DependencyInjection.Extensions;
using AdaroConnect.Application.AppConsole.Entities;
using Microsoft.EntityFrameworkCore;
using System.IO;
using Microsoft.Extensions.Options;

namespace AdaroConnect.Application.AppConsole
{
    internal static class Program
    {
        #region Preperation 

        #region Constants

        private const string AppSettingsFileName = "appSettings.json";
        private const string UserSecretId = "9D0ABE4E-8415-4738-8021-14000B491020";
        private static bool DisplayedWelcomeMessage;
        #endregion

        #region Variables

        private static IServiceCollection ServiceCollection;
        private static IConfigurationBuilder ConfigurationBuilder;
        private static string MENU_BY_PASS { get; set; }

        private static readonly Dictionary<char, Tuple<string, string>> Operations = new()
        {
            {'1', new Tuple<string, string>("Synchronize Report PO (ZMM021R)", "SAP_DownloadSynch_Procurement")},
            {'2', new Tuple<string, string>("Cost Center", "GetCostCenter")},



            //{'3', new Tuple<string, string>("Table Samples", "GetMaterial")},
            //{'4', new Tuple<string, string>("Table Samples", "GetMaterialsByPrefixWithSubTable")},
            //{'5', new Tuple<string, string>("Bapi Samples", "GetVendors")},
            //{'6', new Tuple<string, string>("RFC Samples", "GetSAPJobs")},
            //{'7', new Tuple<string, string>("MetaData Samples", "GetFunctionMetaData")},
            //{'8', new Tuple<string, string>("Transaction Samples", "CreateMaterialWithTransactionAsync") },
            //{'9', new Tuple<string, string>("Cost Center", "GetCostCenter") },
            //{'9', new Tuple<string, string>("Purchase Order", "GetPurchaseOrder") },
        };

        #endregion

        #region Properties

        private static IServiceProvider ServiceProvider { get; set; }
        public static IConfiguration ApplicationConfiguration { get; set; }

        #endregion

        #region Configuration

        private static void ConfigureServices()
        {
            ServiceCollection = new ServiceCollection();
            ServiceCollection.AddAdaroConnectSampleCore(ApplicationConfiguration);
            ServiceCollection.Configure<AdaroConfigurations>(ApplicationConfiguration.GetSection("AdaroConfiguration"));

            ServiceCollection.AddDbContext<AdaroConnectContext>(
                options => options.UseSqlServer(ApplicationConfiguration.GetConnectionString("Default"), sqlServerOptions => sqlServerOptions.CommandTimeout(1200))
            );


            ServiceCollection.TryAddSingleton<ICustomReportPurchaseOrderSynch, CustomReportPurchaseOrderSynch>();
            ServiceProvider = ServiceCollection.BuildServiceProvider();
        }

        #endregion

        #endregion

        public static async Task Main(string[] args)
        {
            //return;
            ConfigurationBuilder = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile(AppSettingsFileName, true, true)
                .AddUserSecrets(UserSecretId, true)
                .AddEnvironmentVariables();
            ApplicationConfiguration = ConfigurationBuilder.Build();

            ConfigureServices();

            

            try
            {
                MappingMenuByPassing(args);
                await Menu();
            }
            catch (Exception e)
            {
                System.Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(e);
                throw;
            }
        }

        #region Menu Operation

        private static void WelcomeMessage()
        {
            if (!DisplayedWelcomeMessage)
                return;
            System.Console.WriteLine("".PadLeft(20, '='));
            System.Console.WriteLine("====== WELCOME TO ADARO CONNECT ===========");
            System.Console.WriteLine("".PadLeft(20, '='));
            System.Console.WriteLine("\n\n");
            DisplayedWelcomeMessage = true;
        }
        private static async Task Menu()
        {
            ConsoleKeyInfo keyCode;


            if (!string.IsNullOrEmpty(MENU_BY_PASS))
            {
                _ = ExecuteOperation(char.Parse(MENU_BY_PASS));
            } else
            {
                do
                {
                    WelcomeMessage();
                    System.Console.WriteLine("====== MENU ===========");
                    System.Console.WriteLine("".PadLeft(20, '='));

                    foreach (KeyValuePair<char, Tuple<string, string>> operation in Operations)
                        System.Console.WriteLine($"{operation.Key} - {operation.Value.Item1} - {operation.Value.Item2}");

                    System.Console.WriteLine("--------------------------------------");
                    System.Console.WriteLine("0 - Exit");
                    System.Console.WriteLine("".PadLeft(20, '='));
                    System.Console.Write("Please select an operation:");
                    keyCode = System.Console.ReadKey();
                } while (keyCode.KeyChar != '0' && !Operations.ContainsKey(keyCode.KeyChar));

                await ExecuteOperation(keyCode.KeyChar);
                await Menu();
            }
        }
        private static async Task ExecuteOperation(char keyCode)
        {
            switch (keyCode)
            {
                case '0':
                    Environment.Exit(0);
                    break;
                case '1':
                    await SAP_DownloadSynch_Procurement();
                    break;




                case '2':
                await GetCostCenterAsync();
                break;

                case '3':
                    await GetMaterialAsync();
                    break;
                case '4':
                    await GetMaterialsByPrefixWithSubTablesAsync();
                    break;
                case '5':
                    await GetVendorsUseBapi();
                    break;
                case '6':
                    await GetJobsAsync();
                    break;
                case '7':
                    GetFunctionMetaData();
                    break;
                case '8':
                    await CreateMaterialWithTransactionAsync();
                    break;
                //case '9':
                //    await GetCostCenterAsync();
                //    break;
                case '9':
                    await GetPurchaseOrder();
                    break;
                default:
                    System.Console.WriteLine("Menu Key not found!");
                    break;
            }
        }

        private static void MappingMenuByPassing(string[] args)
        {
            if(args != null && args.Length > 0)
            {
                foreach (var arg in args)
                {
                    var arg_param = arg.Split("=");

                    switch (arg_param[0])
                    {
                        case "MENU":
                            if(arg_param.Length > 1)
                                MENU_BY_PASS = arg_param[1];
                            break;

                        default:
                            break;
                    }
                }
            }
        }
        #endregion

        #region MAIN FUNCTION METHOD

        private static async Task SAP_DownloadSynch_Procurement()
        {
            ICustomReportPurchaseOrderSynch manager = ServiceProvider.GetRequiredService<ICustomReportPurchaseOrderSynch>();
            manager.SynchronizeData();
        }

        #endregion

        #region Example Execution Methods

        private static async Task GenerateTableSAP()
        {
            SAPMetaDataExtractor SAPExtract = new SAPMetaDataExtractor();
            SAPExtract.GetTableInfo("EKKO");
        }

        private static async Task GetJobsAsync()
        {
            IJobManager manager = ServiceProvider.GetRequiredService<IJobManager>();
            GetJobOutputParameter jobs = await manager.GetJobsAsync();
            manager.Print(jobs);
        }
        private static async Task GetVendorsUseBapi()
        {
            const string COMPANY_CODE = "200";

            IVendorManager manager = ServiceProvider.GetRequiredService<IVendorManager>();
            VendorBapiOutputParameter result = await manager.GetVendorsByCompanyCodeAsync(COMPANY_CODE);
            manager.Print(result);
        }
        private static async Task GetMaterialAsync()
        {
            const string MATERIAL_CODE = "15101505-0001";

            IMaterialManager manager = ServiceProvider.GetRequiredService<IMaterialManager>();
            Material result = await manager.GetMaterialWithSubTableAsync(MATERIAL_CODE);
            manager.Print(result);
        }
        private static async Task GetMaterialsByPrefixWithSubTablesAsync()
        {
            //const string MATERIAL_PREFIX = "11AKPAK";
            const string MATERIAL_PREFIX = "1D";
            const int RECORD_COUNT = 0;

            IMaterialManager manager = ServiceProvider.GetRequiredService<IMaterialManager>();
            List<Material> result = await manager.GetMaterialsByPrefixWithSubTablesAsync(MATERIAL_PREFIX, RECORD_COUNT);
            manager.Print(result);
        }
        private static async Task GetBillOfMaterialsAsync()
        {
            var billOfMaterialList = new List<Tuple<string, string, string>>()
            {
                new("200ALFS0304000018", "LIVE", "201"),
                new("200DYNA0102000000", "LIVE", "201"),
                new("200ELAC0303000738", "LIVE", "201"),
                new("200RUBA0305000861", "LIVE", "201"),
                new("201STBK1603000092", "LIVE", "201"),
                new("203BARB2306000021", "LIVE", "201"),
                new("203ELAC2D06000000", "LIVE", "201"),
                new("204LMAC3K04000260", "LIVE", "201"),
                new("204STBKS903000089", "LIVE", "201"),
                new("207ALBZ1002000400", "LIVE", "201"),
                new("207SLPT0000000000", "LIVE", "201"),
                new("207SMCF1602000900", "LIVE", "201"),
                new("200ALFS0304000018", "TEST", "201"),
                new("200DYNA0102000000", "TEST", "201"),
                new("200ELAC0303000738", "TEST", "201"),
                new("200RUBA0305000861", "TEST", "201"),
                new("201STBK1603000092", "TEST", "201"),
                new("203BARB2306000021", "TEST", "201"),
                new("203ELAC2D06000000", "TEST", "201"),
                new("204LMAC3K04000260", "TEST", "201"),
                new("204STBKS903000089", "TEST", "201"),
            };

            IBillOfMaterialManager manager = ServiceProvider.GetRequiredService<IBillOfMaterialManager>();

            await Task.WhenAll(billOfMaterialList.Select(material => Task.Run(async () =>
            {
                BomOutputParameter result = await manager.GetBillOfMaterialAsync(material.Item1, material.Item3, material.Item2);
                manager.Print(result?.Topmat);
            })).ToArray()).ConfigureAwait(false);
        }
        private static async Task GetBillOfMaterialAsync()
        {
            const string MATERIAL_CODE = "200DYNA0102000000";
            const string PLANT_CODE = "201";

            IBillOfMaterialManager manager = ServiceProvider.GetRequiredService<IBillOfMaterialManager>();
            BomOutputParameter result = await manager.GetBillOfMaterialAsync(MATERIAL_CODE, PLANT_CODE);
            manager.Print(result);
        }
        private static void GetFunctionMetaData()
        {
            const string FUNCTION_NAME = "BAPI_PO_GETITEMS";

            IFunctionMetaDataManager manager = ServiceProvider.GetRequiredService<IFunctionMetaDataManager>();
            List<ParameterMetaData> metaData = manager.GetFunctionMetaData(FUNCTION_NAME);
            manager.Print(metaData);
        }
        private static async Task CreateMaterialWithTransactionAsync()
        {
            IMaterialSaveDataManager manager = ServiceProvider.GetRequiredService<IMaterialSaveDataManager>();
            MaterialSaveDataBapiOutputParameter result = await manager.CreateMaterialAsync();
            manager.Print(result);
        }

        private static async Task GetCostCenterAsync()
        {
            ICostCenterManager manager = ServiceProvider.GetRequiredService<ICostCenterManager>();
            CostCenterGetListOutputParameter result = await manager.GetCostCenterAsync();
            manager.Print(result);
        }

        private static async Task GetPurchaseOrder()
        {
            IPurchaseOrderManager manager = ServiceProvider.GetRequiredService<IPurchaseOrderManager>();
            var result = await manager.GetPurchaseOrder(new SAPGeneralParameterModel() { CompanyCode = "2000", DateFrom = DateTime.Now.AddMonths(-3) });
            var strObj = Newtonsoft.Json.JsonConvert.SerializeObject(result);
            System.Console.WriteLine($"Total Item {result.Count}");
        }

        private static async Task GetPurchaseOrderItems()
        {
            IPurchaseOrderManager manager = ServiceProvider.GetRequiredService<IPurchaseOrderManager>();
            List<PurchasingDocumentItem> result = await manager.GetPurchaseOrderItem(new SAPGeneralParameterModel());
            System.Console.WriteLine($"Total Item {result.Count}");
        }

        private static async Task GetGetAccountAssignmentPurchasing()
        {
            IPurchaseOrderManager manager = ServiceProvider.GetRequiredService<IPurchaseOrderManager>();
            var result = await manager.GetAccountAssignmentPurchasing();
            System.Console.WriteLine($"Total Item {result.Count}");
        }

        private static async Task GetServicePackage()
        {
            IPurchaseOrderManager manager = ServiceProvider.GetRequiredService<IPurchaseOrderManager>();
            var result = await manager.GetServicePackage();
            System.Console.WriteLine($"Total Item {result.Count}");
        }

        #endregion

    }
}
