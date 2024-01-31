using System.Collections.Generic;
using AdaroConnect.Models;

namespace AdaroConnect.Application.Core.Abstracts
{
    public interface IFunctionMetaDataManager:IPrintable<List<ParameterMetaData>>,IPrintable<List<FieldMetaData>>
    {
        List<ParameterMetaData> GetFunctionMetaData(string functionName);
    }
}
