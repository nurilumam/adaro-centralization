using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using AdaroConnect.Abstract;
using AdaroConnect.Core.Abstract;
using AdaroConnect.Application.Core.Abstracts;
using AdaroConnect.Application.Core.Models;

namespace AdaroConnect.Application.Core.Managers
{
    public class GoodsMovementManager : IGoodsMovementManager
    {
        private readonly IServiceProvider _serviceProvider;

        public GoodsMovementManager(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<GoodsMovementBapiOutputParameter> MaterialTransferAsync()
        {
            var inputParameter = new GoodsMovementBapiInputParameter
            {
                Header = new Header {DocumentDate = DateTime.Now, PostingDate = DateTime.Now},
                MovementType = new MovementType {MovementTypeCode = "04"},
                Items = new MovementItem[]
                {
                    new MovementItem
                    {
                        MaterialCode = "",
                        PlantCode = "",
                        WarehouseCode = "",
                        MoveType = "413",
                        ValueType = "",
                        Quantity = 1,
                        QuantityMeasurementUnitType = "ST",
                        MovementMaterialCode = "",
                        SpecialStockIndicator = 'E',
                        SalesOrderNumberOld = "",
                        SalesItemNumberOld = "",
                        OrderNumber = "",
                        OrderItemNumber = "",
                        MovementPlantCode = "",
                        MovementValueType = "",
                        MovementWarehouseCode = ""
                    }
                },
                SerialNumbers = new MovementSerialNumber[] { }
            };


            using IRfcClient client = _serviceProvider.GetRequiredService<IRfcClient>();
            using IRfcTransaction transaction = client.CreateTransaction();
            using IRfcTransactionFunction func = transaction.CreateFunction("BAPI_GOODSMVT_CREATE");
            await func.InvokeAsync(inputParameter);
            transaction.SubmitTransaction();
            transaction.ConfirmTransaction();
            GoodsMovementBapiOutputParameter result = func.ReadSubmitResult<GoodsMovementBapiOutputParameter>();
            return result;
        }

        public void Print(GoodsMovementBapiOutputParameter model)
        {
            throw new NotImplementedException();
        }
    }
}
