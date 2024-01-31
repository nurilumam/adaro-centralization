using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;
using AdaroConnect.Abstraction.Enumerations;

namespace AdaroConnect.Application.Core.Models
{
    [RfcEntity("MAKT", Description = "Material Definition Table")]
    public class MaterialDefinition : ISapTable
    {
        [RfcEntityProperty("MATNR", Description = "Material Code", SapDataType = RfcDataTypes.CHAR, Length = 18)]
        public string Code { get; set; }

        [RfcEntityProperty("MAKTX", Description = "Material Short Definition", SapDataType = RfcDataTypes.CHAR, Length = 40)]
        public string Definition { get; set; }
    }
}
