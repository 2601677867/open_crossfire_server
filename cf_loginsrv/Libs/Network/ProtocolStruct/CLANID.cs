using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct CLANID
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        private string sz;
    }
}