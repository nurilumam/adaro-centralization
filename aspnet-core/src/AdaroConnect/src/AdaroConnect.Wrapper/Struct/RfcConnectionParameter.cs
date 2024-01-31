using System.Runtime.InteropServices;

namespace AdaroConnect.Wrapper.Struct
{
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
    public struct RfcConnectionParameter
    {
        [MarshalAs(UnmanagedType.LPTStr)]
        public string Name;

        [MarshalAs(UnmanagedType.LPTStr)]
        public string Value;
    }
}
