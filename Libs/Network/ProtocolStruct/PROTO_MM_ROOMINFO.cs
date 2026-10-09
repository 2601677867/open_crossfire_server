using System;
using System.Runtime.InteropServices;
using Network.Protocol;
using Network.SharedFolder;

using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct t_refine_match_data
    {
        public ushort w1;
        public ushort w2;
        public ushort w3;
        public ushort w4;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_MM_ROOMINFO
    {
        public short nRoomNumber;
        public short nRoomMaxUser;
        public short nRoomObserverMaxUser;
        public short nMapType;
        [MarshalAs(UnmanagedType.I1)] public bool bIsFreeCamera;
        public ROUNDTYPE eGameRule;
        public byte eWeaponType;
        public byte eItemDropType;
        public byte by1;
        public byte by2;
        public DEATHMATCHTYPE eWinCondition;
        public int nWinGoal;
        [MarshalAs(UnmanagedType.I1)] public bool bEliteMode;

        [MarshalAs(UnmanagedType.I1)] public bool bIsPlayingEnter;
        public float fTimeToRespawn;
        public ushort wInitialTP;
        [MarshalAs(UnmanagedType.I1)] public bool bFriendlyFire;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TEAM_INDEX_MAX_COUNT)]
        public ushort[] nFirstHalfResult;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TEAM_INDEX_MAX_COUNT)]
        public ushort[] nSecondHalfResult;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TEAM_INDEX_MAX_COUNT)]
        public ushort[] nExtraFirstHalfResult;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TEAM_INDEX_MAX_COUNT)]
        public ushort[] nExtraSecondHalfResult;

        public TOURROOM_STATE_R eTourRoomState;

        public byte cRound;
        public uint m_dwTeamEffectCount;
        public uint m_dwVVIPUserCount;
        public int m_bLeagueGameRoom;
        public P_ATNM.MatchType m_eMatchType;
        public SYSTEMTIME m_tCurrentTime;
        public SYSTEMTIME m_tStartingTime;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 66)]
        public string szTeamName;

        [MarshalAs(UnmanagedType.I1)] public bool bClanHalfTime;
        [MarshalAs(UnmanagedType.I1)] public bool bWaveBalance;
        
        public byte ch0;
        public byte ch1;
        public byte ch2; // -1
        public short s3; // 1
        public short s5; // -1
        public byte ch7;
        public byte ch8_dummy;
        public byte ch9;
        public byte ch10;
        public byte ch11;
        public byte ch12;
        public byte ch13;
        public byte ch14;
        public byte ch15;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MM_ROOM_TEAM_MAX_USER)]
        public byte[] buffer1;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MM_ROOM_TEAM_MAX_USER)]
        public byte[] buffer2;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = TEAM_INDEX_MAX_COUNT)]
        public t_refine_match_data[] aRefineMatchData;
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_UCC_MAP_PATH_LENGTH)]
        public string szUCCMapPath;
    }
}