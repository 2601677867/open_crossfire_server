using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_DEFAULT_DRESS_DATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DRESS_PART)]
        public long[] nDefaultDressInvenSrl;
    }
}