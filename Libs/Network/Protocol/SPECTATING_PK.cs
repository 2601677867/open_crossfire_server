using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.Protocol
{
    public class SPECTATING_PK
    {
        public const int PROTOCOL_SPECTATING = 18;
        public const int PROTOCOL_SPECTATING_SECOND = 1;
        
        public const int PROTOCOL_REQUEST_SPECTATING = 1;
        public const int PROTOCOL_REQUEST_SPECTATING_RESULT = 2;
        public const int PROTOCOL_REQUEST_SPECTATING_ERROR = 3;
        public const int PROTOCOL_REQUEST_SPECTATING_END = 4;
        public const int PROTOCOL_REQUEST_SPECTATING_PASSWORD = 5;
        public const int PROTOCOL_REQUEST_SPECTATING_PASSWORD_RESULT = 6;
        public const int PROTOCOL_REQUEST_SPECTATING_RECOMMENDATION_ROOM_LIST = 7;
        public const int PROTOCOL_SPECTATING_RECOMMENDATION_ROOM_LIST = 8;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_SPECTATING
        {
            public enum REASON
            {
                FROM_LOBBY = 1,
                FROM_BUDDY_LIST_NOT_INGAME = 2,
                FROM_BUDDY_LIST_INGAME = 3,
                FROM_SPECTATE_BUDDY_BUTTON = 4,
                FROM_WEB = 5
            }
            
            public long lTargetUSN;
            public int iTeamIndex;
            public int iSlotIndex;
            public REASON eReason;
            public int iWebReserved1;
            public int iWebReserved2;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_SPECTATING_PASSWORD
        {
            public PROTO_REQUEST_SPECTATING tRequest;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MM_ROOM_PW_MAXLENGTH)]
            public byte[] aszPassword;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_SPECTATING_RESULT
        {
            public enum RESULT
            {
                SUCCESS,
                PLAYER_NOT_EXIST,
                WRONG_ROOM_INFO,
                GAME_NOT_STARTED,
                GAME_ALREAEDY_ENDED,
                CANNOT_SPECTATE,
                PREPARING_TO_SPECTATE,
                REQUIRE_PASSWORD,
                WRONG_PASSWORD,
                ROOM_CANNOT_SPECTATE,
                PLEASE_RETRY,
                SERVER_IS_BUSY
            }

            public RESULT eResult;
        }
    }
}