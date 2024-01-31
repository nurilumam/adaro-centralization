using System;

namespace AdaroConnect.Wrapper.Abstract
{
    public interface IInputMapper
    {
        void Apply(IRfcInterop interop, IntPtr dataHandle, object input);
    }
}
