using AdaroConnect.Wrapper.Attributes;

namespace AdaroConnect.Samples.NetCore.RfcExamples.Models
{
    public sealed class Topmat
    {
        [RfcEntityProperty("MATNR", Description = "Malzeme Tanımı")]
        public string Code { get; set; }

        [RfcEntityProperty("MAKTX", Description = "Tanım")]
        public string Definition { get; set; }
    }
}
