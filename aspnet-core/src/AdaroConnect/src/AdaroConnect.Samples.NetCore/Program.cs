using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AdaroConnect.Extensions;
using AdaroConnect.Mm;
using AdaroConnect.Samples.NetCore.BapiExamples;
using AdaroConnect.Samples.NetCore.RfcExamples;
using AdaroConnect.Samples.NetCore.RfcExamplesJob;
using AdaroConnect.Samples.NetCore.RfcExamplesJob.Models;
using AdaroConnect.Samples.NetCore.TableExamples;
using AdaroConnect.Samples.NetCore.TableExamples.Models;

namespace AdaroConnect.Samples.NetCore
{
    internal static class Program
    {
        private static IServiceProvider ServiceProvider;
        private static bool DisplayedWelcomeMessage;
        private const string SapSectionName = "SapServerConnections:Sap";

        [HandleProcessCorruptedStateExceptions]
        private static void Main()
        {
            try
            {
                IConfiguration configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddUserSecrets("d43bb0e9-3a7e-4cb4-9ebb-3cf7f9d826bd")
                    .AddJsonFile("appsettings.json", optional: true)
                    .AddEnvironmentVariables()
                    .Build();

                var connectionString = configuration.GetSection(SapSectionName).Value;

                var serviceCollection = new ServiceCollection();
                serviceCollection.AddAdaroConnect(connectionString);
                ServiceProvider = serviceCollection.BuildServiceProvider();

                Menu();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }


        private static void WelcomeMessage()
        {
            if (!DisplayedWelcomeMessage)
                return;
            Console.WriteLine("".PadLeft(20, '='));
            Console.WriteLine("====== WELCOME TO AdaroConnect SAMPLES ===========");
            Console.WriteLine("".PadLeft(20, '='));
            Console.WriteLine("\n\n");
            DisplayedWelcomeMessage = true;
        }
        private static void Menu()
        {
            ConsoleKeyInfo keyCode;
            do
            {
                WelcomeMessage();
                Console.WriteLine("====== MENU ===========");
                Console.WriteLine("".PadLeft(20, '='));
                Console.WriteLine("1 - Rfc Samples - GetBillOfMaterial");
                Console.WriteLine("2 - Table Samples - GetMaterials");
                Console.WriteLine("3 - Bapi Samples - GetVendors");
                Console.WriteLine("4 - Table Samples - GetMaterial");
                Console.WriteLine("5 - Rfc Samples - GetSAPJobs");
                Console.WriteLine("--------------------------------------");
                Console.WriteLine("0 - Exit");
                Console.WriteLine("".PadLeft(20, '='));
                Console.Write("Please select an operation:");
                keyCode = Console.ReadKey();
            } while (keyCode.KeyChar != '0' && keyCode.KeyChar != '1' && keyCode.KeyChar != '2' && keyCode.KeyChar != '3' && keyCode.KeyChar != '4' && keyCode.KeyChar != '5');
            ShowMenu(keyCode.KeyChar);
            Menu();
        }

        private static void ShowMenu(char keyCode)
        {
            switch (keyCode)
            {
            case '0':
                Environment.Exit(0);
                break;
            case '1':
                GetRfcBillOfMaterial();
                break;
            case '2':
                GetMaterials();
                break;
            case '3':
                GetBapiVendors();
                break;
            case '4':
                GetMaterial();
                break;
            case '5':
                GetJobs();
                break;
            default:
                Console.WriteLine("Menu Key not found!");
                break;
            }
        }


        private static void GetRfcBillOfMaterial()
        {
            string materialCode = "200ARGS0203000000";
            string plantcode = "201";

            var manager = new BillOfMaterialManager(ServiceProvider);
            var bom = manager.GetBillOfMaterial(materialCode, plantcode);
            manager.Print(bom);
        }
        private static void GetBapiVendors()
        {
            string companyCode = "200";

            var manager = new VendorManager(ServiceProvider);
            var ressult = manager.GetVerdorsByCompanyCode(companyCode);
            manager.Print(ressult);
        }
        private static void GetMaterials()
        {
            string materialPrefix = "200";
            int rowCount = 5;

            var manager = new MaterialManager(ServiceProvider);
            //Material Category Table spesfic development table in ABAP
            //Not used Material Category change MaterialQueryOptions
            List<Material> result = manager.GetMaterialsByPrefixAsync(materialPrefix, new MaterialQueryOptions { IncludeAll = true }, true, rowCount).Result;
            manager.Print(result);
        }
        private static void GetMaterial()        
        {
            string material = "11AKPAKLNCA0300000";
            int rowCount = 1;

            var manager = new MaterialManager(ServiceProvider);
            Material result = manager.GetMaterial(material,  true, rowCount);
            manager.Print(result);
        }

        private static void GetJobs()
        {
            var manager = new JobManager(ServiceProvider);
            GetJobOutputParameter jobs = manager.GetJobs();
            manager.Print(jobs);
        }
    }
}
