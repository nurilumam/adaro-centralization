using AdaroConnect.Wrapper.Attributes;

namespace AdaroConnect.Samples.NetCore.BapiExamples.Models
{
    public class VendorBapiInputParameter
    {
        [RfcEntityProperty("COMP_CODE")]
        public string CompanyCode { get; set; }
    }
}
