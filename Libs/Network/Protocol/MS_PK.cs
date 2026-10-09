using System.Runtime.InteropServices;
using Network.SharedFolder;

namespace Network.Protocol
{
    public static class MS_PK
    {
        public const int PROTOCOL_MM_ROOM = 247;
        public const int PROTOCOL_MM_ROOM_SECOND = 4;
        
        public const int PROTOCOL_LOCAL_ROOM_CLOSED = 0;
        public const int PROTOCOL_MM_ROOM_SERVER_KEY_CMD = 1;
        public const int PROTOCOL_MM_ROOM_SERVER_KEY_REQ = 2;
        public const int PROTOCOL_MM_ROOM_ROMM_INFO_CMD = 3;
        public const int PROTOCOL_MM_ROOM_SEARCH_REQUEST = 4;
        public const int PROTOCOL_MM_ROOM_SEARCH_RESULT = 5;
        public const int PROTOCOL_LOCAL_ENTER_ROOM_KEY_CMD = 6;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MM_ROOM_SERVER_KEY_CMD
        {
            public short sServerKey;
            public SERVERLOWPROPERTY eServerLowProperty;
            public short nServerHighLimit;
            public short nServerLowLimit;
            public bool bKillDeathLimit;
            public float fHighKillDeathLimit;
            public float fLowKillDeathLimit;
            public short sServerUserCount;
            public short sServerMaxRoomCount;
        }
    }
}