using System.Runtime.InteropServices;
using Network.ProtocolStruct.Host;

// ReSharper disable FieldCanBeMadeReadOnly.Global

namespace Network.Protocol
{
    public static class GMS_PK
    {
        public const int PROTOCOL_MM = 2;

        public const int PROTOCOL_GMGMT_GAMESTART = 2;
        public const int PROTOCOL_GMGMT_GMSTART_RESULT = 3;
        public const int PROTOCOL_GMGMT_GAMEFINISHED = 4;
        public const int PROTOCOL_GMGMT_DESTROY_ROOM = 5;
        public const int PROTOCOL_GMGMT_UPDATE_CHARACTER_STAT = 6;
        public const int PROTOCOL_GMGMT_GAMEJOIN = 9;
        public const int PROTOCOL_GMGMT_GMJOIN_RESULT = 10;
        public const int PROTOCOL_GMGMT_USERDEAD = 12;
        public const int PROTOCOL_GMGMT_NOTIFY_IAMMMSERVER = 13;
        public const int PROTOCOL_GMGMT_HEARTBEAT = 14;
        public const int PROTOCOL_GMGMT_GSM_TERMINATED = 15;
        public const int PROTOCOL_GMGMT_BAN_USER = 16;
        public const int PROTOCOL_GMGMT_ROUND_CHANGE_NTY = 17;
        public const int PROTOCOL_GMGMT_MAPLOADING_COMPLETE_NTY = 18;
        public const int PROTOCOL_GMGMT_INGAME_AIGACHA_ITEM_INSERT = 19;
        public const int PROTOCOL_GMGMT_INGAME_AIGACHA_ITEM_INSERT_RESULT = 20;
        public const int PROTOCOL_GMGMT_KICK_USER = 21;
        public const int PROTOCOL_GMGMT_CODEHUNTER_MOVELOBBY = 22;
        public const int PROTOCOL_GMGMT_RUN_COMMAND = 100;
        public const int PROTOCOL_GMGMT_GAMEFINISHED_HEAD = 101;
        public const int PROTOCOL_GMGMT_GAMEFINISHED_BODY = 102;
        public const int PROTOCOL_MM_SECOND_255 = 255;
        
        public const int PROTOCOL_GAMEMGT = 3;
        
        public const int PROTOCOL_GAMEMGT_CONNECT = 0;
        public const int PROTOCOL_GAMEMGT_CONNECT_RESULT = 1;
        public const int PROTOCOL_STARTING_GAME = 4;
        public const int PROTOCOL_STARTING_GAME_RESULT = 5;
        public const int PROTOCOL_GAMEEND = 6;
        public const int PROTOCOL_DESTROY_ROOM = 7;
        public const int PROTOCOL_UPDATE_CHARACTER_STAT = 8;
        public const int PROTOCOL_SERVER_ABNORMALLY_TERMINATED = 9;
        public const int PROTOCOL_SERVER_NEWLY_CREATED = 10;
        public const int PROTOCOL_INSERT_CHARACTER = 11;
        public const int PROTOCOL_INSERT_CHARACTER_RESULT = 12;
        public const int PROTOCOL_MMSERVER_TERMINATED = 13;
        public const int PROTOCOL_USER_DISCONNECTED = 14;
        public const int PROTOCOL_GAMEMGT_HEARTBEAT = 15;
        public const int PROTOCOL_GAMEMGT_BAN_USER = 16;
        public const int PROTOCOL_ALLOCABLEROOMCOUNT_CHANGED = 17;
        public const int PROTOCOL_GAMEGT_ROUND_CHANGE_NTY = 18;
        public const int PROTOCOL_GAMEMGT_MAPLOADING_COMPLETE_NTY = 19;
        // ...
        public const int PROTOCOL_INGAME_AIGACHA_ITEM_INSERT = 22;
        public const int PROTOCOL_INGAME_AIGACHA_ITEM_INSERT_RESULT = 23;
        public const int PROTOCOL_GAMEMGT_KICK_USER = 24;
        public const int PROTOCOL_GAMEMGT_CODEHUNTER_MOVELOBBY = 25;
        public const int PROTOCOL_GAMEMGT_CODEHUNTER_MOVELOBBY_RESULT = 26;
        public const int PROTOCOL_SPY_CHANGE_NTY = 27;
        // 28 defined
        // 29 defined
        public const int PROTOCOL_AI3_BONUS_CLEARBOX_ITEM = 31;
        // 32 defined
        public const int PROTOCOL_SPECTATOR_RECOMMEND_ROOM_SELECT_NTY = 34;
        public const int PROTOCOL_START_GAME_NTY = 35;
        public const int PROTOCOL_AUTO_SIDE_CHANGE_TEAM_INFO_NTY = 36;
        public const int PROTOCOL_WORLD_CREATE_REQ = 41;
        public const int PROTOCOL_WORLD_CREATE_RES = 42;
        public const int PROTOCOL_GAMEMGT_RUN_COMMAND = 100;
        
        public const int PROTOCOL_STARTING_GAME_HEAD_THIRD = 1;
        public const int PROTOCOL_STARTING_GAME_BODY_THIRD = 2;
        
        public const int PROTOCOL_FINISHED_GAME_HEAD_THIRD = 1;
        public const int PROTOCOL_FINISHED_GAME_BODY_THIRD = 2;
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_FINISHED_GAME_HEAD
        {
            public PROTO_FINISHED_GAME_HEAD tProtoFinishedGameHead;
        }
    
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_FINISHED_GAME_BODY
        {
            public int nMatchMakingKey;
            public short nChannelNumber;
            public short nRoomNumber;

            public byte byUserCount;
            public PROTO_USER_STAT tUserStat;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_FINISHED_GAME_HEAD
        {
            public PROTO_FINISHED_GAME_HEAD tProtoFinishedGameHead;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_FINISHED_GAME_BODY
        {
            public short nChannelNumber;
            public short nRoomNumber;
            public byte byUserCount;
            public PROTO_USER_STAT tUserStat;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_GAMESTART_USER
        {
            public short nChannelNumber;
            public short nRoomNumber;
            public byte byUserCount;
            public PROTO_GAME_USER_INFO tProtoMgmtGameStartUserInfo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_GAMESTART
        {
            public int nMatchMakingKey;
            public PROTO_START_HOST_HEADER tStartHostHeader;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_GAMESTART_USER
        {
            public int nMatchMakingKey;
            public short nChannelNumber;
            public short nRoomNumber;
            public byte byUserCount;
            public PROTO_GAME_USER_INFO tProtoMgmtGameStartUserInfo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_SPECTATOR_RECOMMEND_ROOM_SELECT_NTY
        {
            public int nMatchMakingKey;
            public short nChannelNumber;
            public short nRoomNumber;
            public byte tPosition;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_GAMEJOIN
        {
            public uint dwGameServerManagerKeyIP;
            public int nGameServerManagerKey;
            public short nChannelNumber;
            public short nRoomNumber;
            public PROTO_JOIN_HOST tProtoJoinHost;
            public byte tRound;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_GAMEJOIN
        {
            public int nMMKey;
            public short nChannelNumber;
            public short nRoomNumber;
            public PROTO_GAME_USER_INFO tProtoMgmtGameStartUserInfo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_USERDEAD
        {
            public int nGameServerManagerKey;
            public short nServerNumber;
            public short nChannelNumber;
            public short nRoomNumber;
            public long lUSN;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_USERDEAD
        {
            public int nMMKey;
            public short nChannelNumber;
            public short nRoomNumber;
            public long lUSN;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_INGAME_AIGACHA_ITEM_INSERT
        {
            
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_INGAME_AIGACHA_ITEM_INSERT
        {
            public int m_nMMKey;
            public PROTO_INGAME_AIGACHA_ITEM_INSERT tInsert;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_INGAME_AIGACHA_ITEM_INSERT
        {
            public PROTO_INGAME_AIGACHA_ITEM_INSERT tInsert;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_INGAME_AIGACHA_ITEM_INSERT_RESULT
        {
            
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_INGAME_AIGACHA_ITEM_INSERT_RESULT
        {
            public int nMatchMakingKey;
            public short nChannelNumber;
            public short nRoomNumber;
            public PROTO_INGAME_AIGACHA_ITEM_INSERT_RESULT tInsert;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_INGAME_AIGACHA_ITEM_INSERT_RESULT
        {
            public int nGameServerManagerKey;
            public short nServerNumber;
            public short nChannelNumber;
            public short nRoomNumber;
            public PROTO_INGAME_AIGACHA_ITEM_INSERT_RESULT tInsert;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_CODEHUNTER_MOVE_LOBBY
        {
            public int m_nGSMKey;
            public short m_nMMKey;
            public short m_nChannelId;
            public short m_nRoomId;
            public long m_lUSN;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_CODEHUNTER_MOVE_LOBBY
        {
            public int m_nMMKey;
            public short m_nChannelId;
            public short m_nRoomId;
            public long m_lUSN;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_KICK_USER
        {
            public short m_nChannelId;
            public short m_nRoomId;
            public long m_lUSN;
            public byte m_byTeamIndex;
            public byte m_bySlotIndex;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_KICK_USER
        {
            public int m_nMMKey;
            public short m_nChannelId;
            public short m_nRoomId;
            public long m_lUSN;
            public byte m_byTeamIndex;
            public byte m_bySlotIndex;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_MAPLOADING_COMPLETE_NTY
        {
            public int m_nMMKey;
            public short m_nChannelId;
            public short m_nRoomId;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_MAPLOADING_COMPLETE_NTY
        {
            public short m_nChannelId;
            public short m_nRoomId;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEGT_ROUND_CHANGE_NTY
        {
            public int m_nMMKey;
            public short m_nChannelId;
            public short m_nRoomId;
            public byte m_cRound;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_ROUND_CHANGE_NTY
        {
            public short m_nChannelId;
            public short m_nRoomId;
            public byte m_cRound;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_BAN_USER
        {
            public int nMatchMakingKey;
            public short nChannelNumber;
            public short nRoomNumber;
            public long lUSN;
            public byte tIndex;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_BAN_USER
        {
            public short nChannelNumber;
            public short nRoomNumber;
            public long lUSN;
            public byte tIndex;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_UPDATE_CHARACTER_STAT
        {
            public int nMatchMakingKey;
            public short nChannelNumber;
            public short nRoomNumber;
            public PROTO_USER_STAT tUserStat;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_UPDATE_CHARACTER_STAT
        {
            public short nChannelNumber;
            public short nRoomNumber;
            public PROTO_USER_STAT tUserStat;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SERVER_NEWLY_CREATED
        {
            public int nAddCount;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_INSERT_CHARACTER_RESULT
        {
            public int m_nMMKey;
            public short m_nChannelId;
            public short m_nRoomId;
            public PROTO_JOIN_HOST_RESULT tProtoJoinHostResult;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_INSERT_CHARACTER_RESULT
        {
            public uint m_dwGMSAddrInet;
            public uint m_dwGSMAddrInet;
            public int m_nMMKey;
            public short m_nChannelId;
            public short m_nRoomId;
            public PROTO_JOIN_HOST_RESULT tProtoJoinHostResult;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_STARTING_GAME_RESULT
        {
            public int m_nMMKey;
            public short m_nChannelId;
            public short m_nRoomId;
            public PROTO_START_HOST_RESULT tStartHostResult;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_STARTING_GAME_RESULT
        {
            public uint m_dwGMSAddrInet;
            public uint m_dwGSMAddrInet;
            public int m_nGameServerManagerKey;
            public short m_nChannelId;
            public short m_nRoomId;
            public PROTO_START_HOST_RESULT tStartHostResult;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_ALLOCABLEROOMCOUNT_CHANGED
        {
            public int m_nRoomCount;
            public int m_nPlayingNormalRoomCount;
            public int m_nPlayingAIRoomCount;
            public int m_nPlayingWaveRoomCount;
            public int m_nPlayingAI2RoomCount;
            public int m_nPlayingAIBotRoomCount;
            public int m_nPlayingAI3RoomCount;
            public int m_nPlayingBattleRoyalRoomCount;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_CONNECT
        {
            public short m_nDummy;
            public short m_nServerID;
            public int m_nRoomCount;
            public int m_nSettingRoomCount;
            public int m_nPlayingNormalRoomCount;
            public int m_nPlayingAIRoomCount;
            public int m_nPlayingWaveRoomCount;
            public int m_nPlayingAI2RoomCount;
            public int m_nPlayingAIBotRoomCount;
            public int m_nPlayingAI3RoomCount;
            public int m_nPlayingBattleRoyalRoomCount;
        }

        public enum GAMEMGT_CONNECT_RESULT
        {
            SUCCESS,
            FAIL
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_CONNECT_RESULT
        {
            public GAMEMGT_CONNECT_RESULT m_eResult;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_NOTIFY_IAMMMSERVER
        {
            public short m_nServerNumber;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_MM_SERVER_TERMINATED
        {
            public int m_iMatchMakingKey;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_DESTROY_ROOM
        {
            public int m_iMatchMakingKey;
            public short m_nChannelId;
            public short m_nRoomId;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_DESTROY_ROOM
        {
            public short m_nChannelId;
            public short m_nRoomId;
            public uint m_dwGSMKeyIP;
            public int m_nGSMKey;
        }
        
        public enum GAME_COMMAND_ID
        {
            GAME_COMMAND_CLOSE_SERVER,
            GAME_COMMAND_LIMIT_SERVER_FPS,
            GAME_COMMAND_UNLIMIT_SERVER_FPS,
            GAME_COMMAND_SET_UPDATE_RATE,
            GAME_COMMAND_RESET_UPDATE_RATE,
            GAME_COMMAND_SET_MASSGAME,
            GAME_COMMAND_UNSET_MASSGAME
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GMGMT_RUN_COMMAND
        {
            public int nGSMKey;
            public short nMMKey;
            public short nChannelId;
            public short nRoomId;
            public long lUSN;
            public GAME_COMMAND_ID eCommandId;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string szParam;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAMEMGT_RUN_COMMAND
        {
            public int nMMKey;
            public short nChannelId;
            public short nRoomId;
            public long lUSN;
            public GAME_COMMAND_ID eCommandId;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 20)]
            public string szParam;
        }
    }
}