using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;

namespace AdaroConnect.Application.Core.Models
{
    public class GetJobInputParameter : IRfcInput
    {
        [RfcEntityProperty("I_DATA_S")]
        public string StartDate { get; set; }

        [RfcEntityProperty("I_DATA_E")]
        public string EndDate { get; set; }

        [RfcEntityProperty("I_STATUS")]
        public string Status { get; set; }

        [RfcEntityProperty("I_PROGNAME")]
        public string ProgramName { get; set; }

        [RfcEntityProperty("I_AUTHCKMAN")]
        public string ClientCode { get; set; }
    }
}
