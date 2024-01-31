using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;

namespace AdaroConnect.Application.Core.Models
{
    public class VendorBapiInputParameter : IBapiInput
    {
        [RfcEntityProperty("COMP_CODE")]
        public string CompanyCode { get; set; }
    }
}
