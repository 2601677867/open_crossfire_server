using System.Runtime.InteropServices;

namespace Network.Protocol
{
    public class SV_PK
    {
        public const int PROTOCOL_GAME_SERVER_GAME_MANAGER = 250;
        public const int PROTOCOL_GAME_SERVER_GAME_LOG = 1;
        
        public const int PROTOCOL_LOGIN_SERVER_AUTO_EVENT_PULSE = 251;
        
        public const int PROTOCOL_LOGIN_SERVER_PUSH_PACKET = 252;
        public const int PROTOCOL_DBLOGIN = 1;
        
        public const int PROTOCOL_GAME_SERVER_PUSH_PACKET = 254;
        public const int PROTOCOL_ALIVE_CHECK_IN_ROOM = 1;
        public const int PROTOCOL_ANTI_ADD_INFO = 2;
        public const int PROTOCOL_ROOM_USER_CHECK = 4;
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_DBLOGIN
        {
            public int iClientKey;
            public long m_lUSN;
            public byte m_byReadPolicy;
        }
    }
}