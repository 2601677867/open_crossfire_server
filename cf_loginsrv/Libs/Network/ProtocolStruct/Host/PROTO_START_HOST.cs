using System.Runtime.InteropServices;
using Network.ProtocolStruct.Gacha;
using Network.SharedFolder;

using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_START_HOST_HEADER
    {
        public int nHostGhostPlayCnt;
        public uint nHostIP;
        public int nGameServerManagerKey;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_GAME_SERIAL + 1)]
        public string szGameLogSerial;

        public short nChannelNumber;
        public short nRoomNumber;
        public short nMapIndex;
        public short nRoomMaxUser;
        public byte nRoomMaxSlot;
        public short nRoomObserverMaxUser;
        public short nUserCount;

        public ROUNDTYPE eRoundType;
        public byte eWeaponType;
        public byte eItemDropType;
        [MarshalAs(UnmanagedType.I1)] public bool bEliteMode;
        public CLANGAMETYPE eClanGameType;
        public short nRoundsPerLevel;
        public DEATHMATCHTYPE eDeathMatchType;
        public int nDeathMatchGoal;
        public short nBotDifficulty;
        public short nBotWeapon;
        public byte byAIDifficulty;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string szTeamClanID1;
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string szTeamClanID2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] nTeamClanIndex;

        [MarshalAs(UnmanagedType.I1)] public bool bIsClanGame;
        [MarshalAs(UnmanagedType.I1)] public bool bIsTournament;
        public float fTimeToRespawn;
        public ushort wInitialTP;
        [MarshalAs(UnmanagedType.I1)] public bool bFriendlyFire;
        [MarshalAs(UnmanagedType.I1)] public bool bClanBattle;
        public byte tRankMatch;
        [MarshalAs(UnmanagedType.I1)] public bool bClanHalfTime;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] nFirstRound;

        public PROTO_HASHDATA sProtoHashData;
        public ST_AI_GACHA_GROUP_LIST m_stAIGachaGroupRateList;
        
        [MarshalAs(UnmanagedType.I1)] public bool bThrowingAxe;
        [MarshalAs(UnmanagedType.I1)] public bool bExtensionPack;
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3 * 5 * 12)]
        public string szUnknown;
        
        [MarshalAs(UnmanagedType.I1)] public bool bC4Wire;
        [MarshalAs(UnmanagedType.I1)] public bool bFairMatch;
        public int nFairMatchBotGrade;

        public byte byCurrentStartUserCount;
        public byte byStartUserCount;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_START_HOST
    {
        public PROTO_START_HOST_HEADER tStartHeader;
		
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_USER_IN_ROOM)]
        public PROTO_GAME_USER_INFO[] pProtoMgmtGameStartUserInfo;
    }
}