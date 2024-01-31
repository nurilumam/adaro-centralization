using System.Diagnostics.CodeAnalysis;
using AdaroConnect.Abstraction.Attributes;

namespace AdaroConnect.Models
{
    [ExcludeFromCodeCoverage]
    internal sealed class TableData
    {
        #region Properties

        [RfcEntityProperty("WA")]
        public string Wa { get; set; }

        #endregion
    }
}
