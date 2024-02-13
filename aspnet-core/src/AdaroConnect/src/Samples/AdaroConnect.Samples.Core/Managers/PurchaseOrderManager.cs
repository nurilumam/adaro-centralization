using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AdaroConnect.Abstract;
using AdaroConnect.Core.Abstract;
using AdaroConnect.Application.Core.Abstracts;
using AdaroConnect.Application.Core.Models;
using System.Linq;
using System.Collections.Generic;
using AdaroConnect.Query;

namespace AdaroConnect.Application.Core.Managers
{
    public class PurchaseOrderManager : IPurchaseOrderManager
    {
        private readonly IServiceProvider _serviceProvider;

        public PurchaseOrderManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<POGetItemsOutputParameter> GetPurchaseOrderItems()
        {
            var inputParameter = new POGetItemsInputParameter
            {
                PURCH_ORG = "2000",
                WITH_PO_HEADERS = "x"
            };

            using IRfcClient client = _serviceProvider.GetRequiredService<IRfcClient>();
            POGetItemsOutputParameter bomResult = await client.ExecuteRfcAsync<POGetItemsInputParameter, POGetItemsOutputParameter>("BAPI_PO_GETITEMS", inputParameter);
            return bomResult;


        }

        public async Task<List<PurchasingDocumentHeader>> GetPurchaseOrder()
        {
            int recordCount = 1000;

            List<string> whereClause = new AbapQuery().Set(QueryOperator.Equal("BUKRS", "2000"))
                .And(QueryOperator.GreaterThan("AEDAT", "01.01.2022")).GetQuery();

            using IRfcClient sapClient = _serviceProvider.GetRequiredService<IRfcClient>();
            return await sapClient.GetTableDataAsync<PurchasingDocumentHeader>(whereClause, rowCount: recordCount, includeUnsafeFields: true);
        }

        public async Task<List<PurchasingDocumentItem>> GetPurchaseOrderItem()
        {
            int recordCount = 300;

            List<string> whereClause = new AbapQuery().Set(QueryOperator.Equal("BUKRS", "2000")).GetQuery();

            using IRfcClient sapClient = _serviceProvider.GetRequiredService<IRfcClient>();
            return await sapClient.GetTableDataAsync<PurchasingDocumentItem>(whereClause, rowCount: recordCount, includeUnsafeFields: true);
        }

        public async Task<List<AccountAssignmentPurchasing>> GetAccountAssignmentPurchasing()
        {
            int recordCount = 100;

            List<string> whereClause = new AbapQuery().Set(QueryOperator.Equal("KOKRS", "ADRO")).GetQuery();

            using IRfcClient sapClient = _serviceProvider.GetRequiredService<IRfcClient>();
            return await sapClient.GetTableDataAsync<AccountAssignmentPurchasing>(whereClause, rowCount: recordCount, includeUnsafeFields: true);
        }

        public async Task<List<ServicePackage>> GetServicePackage()
        {
            int recordCount = 100;

            List<string> whereClause = new AbapQuery().Set(QueryOperator.Equal("SRVPOS", "70111601-0001")).GetQuery();

            using IRfcClient sapClient = _serviceProvider.GetRequiredService<IRfcClient>();
            return await sapClient.GetTableDataAsync<ServicePackage>(whereClause, rowCount: recordCount, includeUnsafeFields: true);
        }

        public void Print(POGetItemsOutputParameter model)
        {
            Console.WriteLine($"Cost Center : {model.PurchaseOrderHeaders.Count()}");



            //Console.WriteLine(string.Format("|{0,5}|{1,5}|{2,5}|{3,5}|", "CO_AREA", "COSTCENTER", "NAME", "DESCRIPT"));
            //foreach (var costCenter in model.CostCenter)
            //{
            //    Console.WriteLine(string.Format("|{0,5}|{1,5}|{2,5}|{3,5}|", costCenter.ControllingArea, costCenter.CostCenter, costCenter.Name, costCenter.Description));
            //}
        }
    }
}
