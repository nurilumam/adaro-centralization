using System.Diagnostics.CodeAnalysis;
using AdaroConnect.Abstraction.Attributes;

namespace AdaroConnect.Models
{
    [ExcludeFromCodeCoverage]
    internal sealed class TableField
    {
        #region Properties

        [RfcEntityProperty("FIELDNAME")]
        public string FieldName { get; set; }

        #endregion
    }
}
