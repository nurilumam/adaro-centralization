using System.Diagnostics.CodeAnalysis;
using AdaroConnect.Abstraction.Attributes;

namespace AdaroConnect.Models
{
    [ExcludeFromCodeCoverage]
    internal sealed class TableOutputParameter
    {
        #region Properties

        [RfcEntityProperty("DATA")]
        public TableData[] Data { get; set; }

        #endregion
    }
}
