using System.Diagnostics.CodeAnalysis;

namespace AdaroConnect.Wrapper.Enumeration
{
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    public enum RfcErrorGroups
    {
        OK,
        ABAP_APPLICATION_FAILURE,
        ABAP_RUNTIME_FAILURE,
        LOGON_FAILURE,
        COMMUNICATION_FAILURE,
        EXTERNAL_RUNTIME_FAILURE,
        EXTERNAL_APPLICATION_FAILURE,
        EXTERNAL_AUTHORIZATION_FAILURE,
    }
}
