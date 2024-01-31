using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;
using AdaroConnect.Abstraction.Model;

namespace AdaroConnect.Application.Core.Models
{
    public class VendorBapiOutputParameter : IBapiOutput
    {
        [RfcEntityProperty("RETURN")]
        public BapiReturnParameter BapiReturn { get; set; }

        [RfcEntityProperty("VENDOR")]
        public Vendor[] Vendors { get; set; }
    }
}
