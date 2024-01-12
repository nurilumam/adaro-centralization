using System;
using System.Collections.Generic;
using System.Text;

namespace Adaro.Centralize.MaterialRequest
{
    public enum MaterialRequestStatus
    {
        New = 0,
        Submitted = 1,
        Rejected = 3,
        VerifyCataloger = 3,
        VerifyBudget = 4,
        SubmittedHolding = 5,
        Registred = 6,
    }
}
