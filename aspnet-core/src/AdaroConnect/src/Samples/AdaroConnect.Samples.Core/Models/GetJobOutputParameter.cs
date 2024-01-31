using AdaroConnect.Abstraction;
using AdaroConnect.Abstraction.Attributes;

namespace AdaroConnect.Application.Core.Models
{
    public class GetJobOutputParameter : IRfcOutput
    {
        [RfcEntityProperty("T_V_OP")]
        public JobStatus[] JobStatuses { get; set; }
    }
}
