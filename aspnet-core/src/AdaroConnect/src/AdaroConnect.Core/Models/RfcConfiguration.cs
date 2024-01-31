using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace AdaroConnect.Core.Models
{
    [ExcludeFromCodeCoverage]
    public class RfcConfiguration
    {
        public string DefaultServer { get; set; }
        public List<RfcServer> RfcServers { get; set; }

        public RfcConfiguration()
        {
            DefaultServer = string.Empty;
            RfcServers = new List<RfcServer>();
        }
    }
}
