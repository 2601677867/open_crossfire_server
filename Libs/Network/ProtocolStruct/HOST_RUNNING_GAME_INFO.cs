using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct HOST_RUNNING_GAME_INFO
    {
        private byte iTeamNum;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        private ushort[] iTeamScore;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        private string iContWinNum;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        private ushort[] iTeamDeath;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        private ushort[] iNumKillPlayerThisRound;

        private byte bRoundStarted;

        private byte bValidRound;

        private short fRoundTimeLeft;

        private short fRoundTimePassed;

        private short fWaitTimeLeftBetweenRounds;

        private short fBuyStopTimeLeft;

        private byte bC4Planted;

        private short fC4TimeLeft;

        private uint iUserIDC4Planted;

        private byte bC4Dropped;

        private short fC4PosX;

        private short fC4PosY;

        private short fC4PosZ;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 7)]
        private string iGameEndTypeCounts;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 16)]
        private HOST_RUNNING_GAME_USER_INFO[] sHostRunningGameInfo;
    }
}