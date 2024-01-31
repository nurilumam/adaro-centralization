using System;
using AdaroConnect.Wrapper.Abstract;

namespace AdaroConnect.Wrapper.Fields.Abstract
{
    internal interface IField
    {
        void Apply(IRfcInterop interop, IntPtr dataHandle);
    }
}
