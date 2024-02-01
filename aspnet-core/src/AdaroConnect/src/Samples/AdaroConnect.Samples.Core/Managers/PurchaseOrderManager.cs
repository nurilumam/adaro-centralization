using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AdaroConnect.Abstract;
using AdaroConnect.Core.Abstract;
using AdaroConnect.Application.Core.Abstracts;
using AdaroConnect.Application.Core.Models;
using System.Linq;

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
