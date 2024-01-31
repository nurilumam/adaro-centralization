using AdaroConnect.Core.Models;

namespace AdaroConnect.Core.Abstract
{
    public interface IRfcNetWeaverLibrary
    {
        public RfcNetWeaverLibraryVersion LibraryVersion { get; }

        void EnsureLibraryPresent();
        RfcNetWeaverLibraryVersion GetVersion();
    }
}
