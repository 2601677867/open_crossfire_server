using System.Runtime.InteropServices;
using Network.Protocol;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_FINISHED_GAME_CHANNEL_ROOM_ID
    {
        public int nMatchMakingKey;
        public short nChannelNumber;
        public short nRoomNumber;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_FINISHED_GAME_HEAD
    {
        public int nMatchMakingKey;
        public short nChannelNumber;
        public short nRoomNumber;
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        public string szGameLogSerial;

        public PROTO_TEAM_STAT tTeamStat;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
        public int[] iGameEndTypeCounts;

        public long iMVPUserID;
        public byte bIs1stPlaceMVP;
        public long iFirstKillUserID;
        public long iLastKillUserID;
        public long iAceUserID;
        public long lTopEscapeUserID;
        public byte bLastRoundClearFlag;

        public MORE_INFO_FOR_AI AIMoreInfo;
        public PROTO_HASHDATA sProtoHashData;
        
        public byte byCurrentUserCount;
        public byte byEndUserCount;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_FINISHED_GAME
    {
        public PROTO_FINISHED_GAME_HEAD tProtoFinishedGameHead;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.SF_MAX_USER_IN_ROOM)]
        public PROTO_USER_STAT[] aUserStat;
    }
}