using System;

namespace AdaroConnect.Wrapper.Abstract
{
    public interface IOutputMapper
    {
        TOutput Extract<TOutput>(IRfcInterop interop, IntPtr dataHandle);
    }
}
