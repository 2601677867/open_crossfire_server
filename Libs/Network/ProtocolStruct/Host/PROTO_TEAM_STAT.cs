using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_TEAM_STAT
    {
        public GAMEENDREASON eGameEndReason;
        public byte tWinTeamIndex;
        public int iWinTeamScore;
        public int iLoseTeamScore;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 42)]
        public string szTeamClanID;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] nTeamClanIndex;
    }
}