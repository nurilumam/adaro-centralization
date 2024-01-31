using System;
using System.Diagnostics.CodeAnalysis;
using AdaroConnect.Core.Abstract;
using AdaroConnect.Core.Models;
using AdaroConnect.Wrapper.Abstract;
using AdaroConnect.Wrapper.Exception;

namespace AdaroConnect.Core
{
    [ExcludeFromCodeCoverage]
    public class RfcNetWeaverLibrary : IRfcNetWeaverLibrary
    {
        private readonly IRfcInterop _interop;
        private bool _libraryChecked;
        private RfcNetWeaverLibraryVersion _libraryVersion;
        public RfcNetWeaverLibraryVersion LibraryVersion
        {
            get
            {
                EnsureLibraryPresent();
                return _libraryVersion;
            }
            private set => _libraryVersion = value;
        }

        public RfcNetWeaverLibrary(IRfcInterop interop)
        {
            _interop = interop;
        }
        

        #region IRfcLibrary Implementation

        public void EnsureLibraryPresent()
        {
            if (_libraryChecked) return;

            LibraryVersion = GetVersion();
            _libraryChecked = true;
        }

        public RfcNetWeaverLibraryVersion GetVersion()
        {
            try
            {
                _interop.GetVersion(out uint majorVersion, out uint minorVersion, out uint patchLevel);

                return new RfcNetWeaverLibraryVersion
                {
                    Major = majorVersion,
                    Minor = minorVersion,
                    Patch = patchLevel,
                };
            }
            catch (DllNotFoundException ex)
            {
                throw new RfcLibraryNotFoundException(ex);
            }
        }

        #endregion
    }
}
