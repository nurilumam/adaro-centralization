using System;
using AdaroConnect.Wrapper.Abstract;
using AdaroConnect.Wrapper.Enumeration;
using AdaroConnect.Wrapper.Extension;
using AdaroConnect.Wrapper.Struct;

namespace AdaroConnect.Wrapper.Fields
{
    internal sealed class LongField : Field<long>
    {
        public LongField(string name, long value)
            : base(name, value)
        {
        }

        public override void Apply(IRfcInterop interop, IntPtr dataHandle)
        {
            RfcResultCodes resultCode = interop.SetInt8(
                dataHandle: dataHandle,
                name: Name,
                value: Value,
                errorInfo: out RfcErrorInfo errorInfo);

            resultCode.ThrowOnError(errorInfo);
        }

        public static LongField Extract(IRfcInterop interop, IntPtr dataHandle, string name)
        {
            RfcResultCodes resultCode = interop.GetInt8(
                dataHandle: dataHandle,
                name: name,
                out long value,
                out RfcErrorInfo errorInfo);

            resultCode.ThrowOnError(errorInfo);

            return new LongField(name, value);
        }

        public override string ToString()
            => $"{Name} = {Value}L";
    }
}
