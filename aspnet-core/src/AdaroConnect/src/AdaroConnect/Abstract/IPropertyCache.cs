using System;
using System.Collections.Generic;
using System.Reflection;

namespace AdaroConnect.Abstract
{
    public interface IPropertyCache
    {
        void AddToPropertyCache(Type type);
        PropertyInfo GetPropertyInfo(Type type, string key);
        Dictionary<string, PropertyInfo> GetPropertyInfos(Type type);
    }
}
