using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_DEFAULT_FUNC_DATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PERIOD_CHAR_FUNC)]
        public long[] nDefaultFuncInvenSrl;
    }
}