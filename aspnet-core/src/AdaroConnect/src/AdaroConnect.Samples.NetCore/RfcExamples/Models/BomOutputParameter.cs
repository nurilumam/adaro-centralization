using AdaroConnect.Wrapper.Attributes;

namespace AdaroConnect.Samples.NetCore.RfcExamples.Models
{
    public sealed class BomOutputParameter
    {
        [RfcEntityProperty("STB")]
        public Stb[] StbData { get; set; }

        [RfcEntityProperty("TOPMAT")]
        public Topmat Topmat { get; set; }
    }
}
