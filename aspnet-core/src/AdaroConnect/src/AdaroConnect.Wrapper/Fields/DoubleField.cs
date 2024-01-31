using System;
using AdaroConnect.Wrapper.Abstract;
using AdaroConnect.Wrapper.Enumeration;
using AdaroConnect.Wrapper.Extension;
using AdaroConnect.Wrapper.Struct;

namespace AdaroConnect.Wrapper.Fields
{
    internal sealed class DoubleField : Field<double>
    {
        public DoubleField(string name, double value)
            : base(name, value)
        {
        }

        public override void Apply(IRfcInterop interop, IntPtr dataHandle)
        {
            RfcResultCodes resultCode = interop.SetFloat(
                dataHandle: dataHandle,
                name: Name,
                value: Value,
                errorInfo: out RfcErrorInfo errorInfo);

            resultCode.ThrowOnError(errorInfo);
        }

        public static DoubleField Extract(IRfcInterop interop, IntPtr dataHandle, string name)
        {
            RfcResultCodes resultCode = interop.GetFloat(
                dataHandle: dataHandle,
                name: name,
                out double value,
                out RfcErrorInfo errorInfo);

            resultCode.ThrowOnError(errorInfo);

            return new DoubleField(name, value);
        }

        public override string ToString()
            => $"{Name} = {Value}";
    }
}
