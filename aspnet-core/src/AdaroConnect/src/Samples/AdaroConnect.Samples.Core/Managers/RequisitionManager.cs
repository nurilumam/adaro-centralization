using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AdaroConnect.Abstract;
using AdaroConnect.Core.Abstract;
using AdaroConnect.Samples.Core.Abstracts;
using AdaroConnect.Samples.Core.Models;

namespace AdaroConnect.Samples.Core.Managers
{
    public class RequisitionManager : IRequisitionManager
    {
        private readonly IServiceProvider _serviceProvider;

        public RequisitionManager(IServiceProvider serviceProvider)
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
        }
    }
}
