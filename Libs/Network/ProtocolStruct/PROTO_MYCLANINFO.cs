using System.Runtime.InteropServices;
using Network.Protocol;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_MYCLANINFO
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CLAN_NAME_LENGTH)]
        public byte[] aszClanName;
        
        public P_IGC.CLAN_MARK_INFO tClanMarkInfo;
    }
}