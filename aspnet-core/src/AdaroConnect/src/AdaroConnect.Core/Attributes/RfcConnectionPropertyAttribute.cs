using System;

namespace AdaroConnect.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RfcConnectionPropertyAttribute: Attribute
    {
        internal string Name { get; set; }
        
        public RfcConnectionPropertyAttribute(string name)
        {
            Name = name;
        }
    }

}
