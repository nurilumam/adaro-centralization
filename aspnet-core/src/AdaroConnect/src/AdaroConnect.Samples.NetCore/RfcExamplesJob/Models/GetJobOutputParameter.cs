using AdaroConnect.Wrapper.Attributes;

namespace AdaroConnect.Samples.NetCore.RfcExamplesJob.Models
{
    public sealed class GetJobOutputParameter
    {
        [RfcEntityProperty("T_V_OP")]
        public JobStatus[] JobStatuses { get; set; }
    }
}
