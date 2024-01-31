using System;
using System.Diagnostics.CodeAnalysis;
using AdaroConnect.Wrapper.Struct;

namespace AdaroConnect.Wrapper.Extension
{
    [ExcludeFromCodeCoverage]
    public static class RfcErrorInfoExtensions
    {
        public static void ThrowOnError(this RfcErrorInfo errorInfo, Action beforeThrow = null) =>
            errorInfo.Code.ThrowOnError(errorInfo, beforeThrow);
    }
}
