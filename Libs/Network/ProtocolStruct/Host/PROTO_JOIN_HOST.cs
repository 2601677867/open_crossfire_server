using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_JOIN_HOST
    {
        public PROTO_GAME_USER_INFO sUserInfo;
    }
}