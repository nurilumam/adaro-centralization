using System;
using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;
using AdaroConnect.Abstraction.Enumerations;
using AdaroConnect.Abstraction.Model;

namespace AdaroConnect.Application.Core.Models
{

    public class CostCenterGetListOutputParameter : IRfcOutput
    {
        [RfcEntityProperty("RETURN")]
        public ExtendedReturnMessage[] ExtendedReturnMessage { get; set; }

        //[RfcEntityProperty("RETURNMESSAGES")]
        //public BapiReturnParameter BapiReturn { get; set; }


        [RfcEntityProperty("COSTCENTERLIST")]
        public CostCenterItem[] CostCenter { get; set; }
    }

    
    public class CostCenterItem
    {
        [RfcEntityProperty("CO_AREA", "Controlling Area", RfcDataTypes.CHAR, 4)]
        public string ControllingArea { get; set; }

        [RfcEntityProperty("COSTCENTER", "Cost Center", RfcDataTypes.CHAR, 10)]
        public string CostCenter { get; set; }

        [RfcEntityProperty("NAME", "General Name", RfcDataTypes.CHAR, 20)]
        public string Name { get; set; }

        [RfcEntityProperty("DESCRIPT", "Description", RfcDataTypes.CHAR, 40)]
        public string Description { get; set; }
        [RfcEntityProperty("ACT_STATE", "Activation Status of Generic Master Record", RfcDataTypes.CHAR, 1)]
        public string ACT_STATE { get; set; }

    }
}
