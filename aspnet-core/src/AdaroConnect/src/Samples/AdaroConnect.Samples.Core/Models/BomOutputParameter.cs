using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;

namespace AdaroConnect.Application.Core.Models
{
    public sealed class BomOutputParameter : IRfcOutput
    {
        [RfcEntityProperty("STB")]
        public Stb[] StbData { get; set; }

        [RfcEntityProperty("TOPMAT")]
        public Topmat Topmat { get; set; }
    }
}
