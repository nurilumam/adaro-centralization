using AdaroConnect.Abstraction.Attributes;

namespace AdaroConnect.Application.Core.Models
{
    public class Vendor
    {
        [RfcEntityProperty("VENDOR_NO")]
        public string VendorNo { get; set; }

        [RfcEntityProperty("NAME")]
        public string Name { get; set; }
    }
}
