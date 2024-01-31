using System.Diagnostics.CodeAnalysis;
using AdaroConnect.Abstraction.Attributes;

namespace AdaroConnect.Models
{
    [ExcludeFromCodeCoverage]
    internal sealed class TableOption
    {
        #region Properties

        [RfcEntityProperty("TEXT")]
        public string Text { get; set; }

        #endregion
    }
}
