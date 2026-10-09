using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_ITEM_ID_DATA
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
        public string szItemID;
    }
}