using AdaroConnect.Abstract;
using AdaroConnect.Parameters;
using AdaroConnect.Wrapper.Attributes;

namespace AdaroConnect.Samples.NetCore.BapiExamples.Models
{
    public class VendorBapiOutputParameter:IRfcBapiOutput
    {
        [RfcEntityProperty("RETURN")]
        public RfcBapiOutputParameter BapiReturn { get; set; }

        [RfcEntityProperty("VENDOR")]
        public VendorModel[] Vendors { get; set; }
    }
}
