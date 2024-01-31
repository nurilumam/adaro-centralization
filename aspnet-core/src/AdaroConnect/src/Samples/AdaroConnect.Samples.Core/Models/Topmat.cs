using AdaroConnect.Abstraction.Attributes;

namespace AdaroConnect.Application.Core.Models
{
    public sealed class Topmat
    {
        [RfcEntityProperty("MATNR", Description = "Malzeme Tanımı")]
        public string Code { get; set; }

        [RfcEntityProperty("MAKTX", Description = "Tanım")]
        public string Definition { get; set; }
    }
}
