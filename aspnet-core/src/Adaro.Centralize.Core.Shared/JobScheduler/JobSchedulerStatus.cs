using System;
using System.Collections.Generic;
using System.Text;

namespace Adaro.Centralize.JobScheduler
{
    public enum JobSchedulerStatus
    {
        Progress = 0,
        Success = 1,
        Error = 2,
        Cancel = 3,
    }
}
