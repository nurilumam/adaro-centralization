using System;
using AdaroConnect.Wrapper.Abstract;
using AdaroConnect.Wrapper.Enumeration;
using AdaroConnect.Wrapper.Extension;
using AdaroConnect.Wrapper.Struct;

namespace AdaroConnect.Wrapper.Fields
{
    internal sealed class IntField : Field<int>
    {
        public IntField(string name, int value)
            : base(name, value)
        {
        }

        public override void Apply(IRfcInterop interop, IntPtr dataHandle)
        {
            RfcResultCodes resultCode = interop.SetInt(
                dataHandle: dataHandle,
                name: Name,
                value: Value,
                errorInfo: out RfcErrorInfo errorInfo);

            resultCode.ThrowOnError(errorInfo);
        }

        public static IntField Extract(IRfcInterop interop, IntPtr dataHandle, string name)
        {
            RfcResultCodes resultCode = interop.GetInt(
                dataHandle: dataHandle,
                name: name,
                out int value,
                out RfcErrorInfo errorInfo);

            resultCode.ThrowOnError(errorInfo);

            return new IntField(name, value);
        }

        public override string ToString()
            => $"{Name} = {Value}";
    }
}
