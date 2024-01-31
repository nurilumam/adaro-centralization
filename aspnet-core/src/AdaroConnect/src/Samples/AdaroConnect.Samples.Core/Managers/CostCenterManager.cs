using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AdaroConnect.Abstract;
using AdaroConnect.Core.Abstract;
using AdaroConnect.Application.Core.Abstracts;
using AdaroConnect.Application.Core.Models;

namespace AdaroConnect.Application.Core.Managers
{
    public class CostCenterManager : ICostCenterManager
    {
        private readonly IServiceProvider _serviceProvider;

        public CostCenterManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<CostCenterGetListOutputParameter> GetCostCenterAsync()
        {
            var inputParameter = new CostCenterGetListInputParameter
            {
                ControlingArea = "ADRO",
                CostCenterGroup = "2000",
                DateFrom = "26.01.2024",
                //DateTo = ""
                //CostCenter = new CostCenterGetListInputParameter.CostCenter[]
            };

            using IRfcClient client = _serviceProvider.GetRequiredService<IRfcClient>();
            CostCenterGetListOutputParameter bomResult = await client.ExecuteRfcAsync<CostCenterGetListInputParameter, CostCenterGetListOutputParameter>("BAPI_COSTCENTER_GETLIST1", inputParameter);
            return bomResult;


        }

        public void Print(CostCenterGetListOutputParameter model)
        {
            Console.WriteLine($"Cost Center : {model.CostCenter.Length}");



            Console.WriteLine(string.Format("|{0,5}|{1,5}|{2,5}|{3,5}|", "CO_AREA", "COSTCENTER", "NAME", "DESCRIPT"));
            foreach (var costCenter in model.CostCenter)
            {
                Console.WriteLine(string.Format("|{0,5}|{1,5}|{2,5}|{3,5}|", costCenter.ControllingArea, costCenter.CostCenter, costCenter.Name, costCenter.Description));
            }
        }
    }
}
