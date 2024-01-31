using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AdaroConnect.Abstract;
using AdaroConnect.Core.Abstract;
using AdaroConnect.Application.Core.Abstracts;
using AdaroConnect.Application.Core.Models;

namespace AdaroConnect.Application.Core.Managers
{
    public class MaterialSaveDataManager:IMaterialSaveDataManager
    {
        private readonly IServiceProvider _serviceProvider;

        public MaterialSaveDataManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<MaterialSaveDataBapiOutputParameter> CreateMaterialAsync()
        {
            var inputParameter = new MaterialSaveDataBapiInputParameter
            {
                HeadData = new HeadData { MaterialCode = "HSN000000000000030", MaterialType = "7100", Sector = 'B', IsBasicView = 'X' },
                MaterialDescription = new MaterialDescription { Name = "Plastik Palet 100 'Lük", LanguageCode = "TR"},
                ClientData = new ClientData
                {
                    MaterialGroup = "71000010",
                    BaseMeasurementUnit = "ST",
                    BaseMeasurementUnitIso = "ST",
                    Divison = "15",
                    ProductionHierarchy = "ZZZ",
                    StorageCondition = "I3"
                }
            };

            
            using IRfcClient client = _serviceProvider.GetRequiredService<IRfcClient>();

            using IRfcTransaction transaction = client.CreateTransaction();
            using IRfcTransactionFunction func = transaction.CreateFunction("BAPI_MATERIAL_SAVEDATA");
            await func.InvokeAsync(inputParameter);
            transaction.SubmitTransaction();
            transaction.ConfirmTransaction();
            MaterialSaveDataBapiOutputParameter result = func.ReadSubmitResult<MaterialSaveDataBapiOutputParameter>();
            return result;
        }

        public void Print(MaterialSaveDataBapiOutputParameter model)
        {
            if (model == null)
            {
                Console.WriteLine($"Have not a result");
                return;
            }

            Console.WriteLine($"{model.BapiReturn.Code},{model.BapiReturn.Message},{model.BapiReturn.MessageV1}");
            Console.WriteLine($"{model.ExtendedReturnMessage.Id},{model.ExtendedReturnMessage.Message},{model.ExtendedReturnMessage.MessageV1}");
        }
    }
}
