using System;
using System.Collections.Generic;
using AdaroConnect.Wrapper.Struct;

namespace AdaroConnect.Core.Abstract
{
    public interface IRfcFunctionMetaData:IDisposable
    {
        List<RfcParameterDescription> GetParameterDescriptions();
        List<RfcFieldDescription> GetFieldDescriptions(IntPtr typeDescriptionHandler);
    }
}
