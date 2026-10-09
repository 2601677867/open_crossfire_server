using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_RETURN_INFO
    {
        public int lTimeStamp;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        public string szPartKeyDay;
        public int lLoginTime;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string szLoginDate;
        public short nClanUser;
        public short nClanSuperVisor;
        public short nClanLLevel;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string szClanSrl;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string szClanUserSrl;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 21)]
        public string szClanID;
        public short nSuperVisor;
        public int nLev;
        public int nKill;
        public int nDeath;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 33)]
        public string szHashedValue;
        public long lEP;
        public int nGP;
        public int nTodayEPAmount;
        public int nTodayGP;
        public int nTodayPlayCnt;
        public int nTodayKillCnt;
        public int nTodayDeathCnt;
        public int nTodayHeadShotCnt;
        public int nTodayWin;
        public int nTodayLose;
        public int nTodayDraw;
        public double dConnectTime;
        public long ClanKey;
        public byte ClanGrade;
        public CLAN_UNIT_INFO ClanUnitInfo;
        public byte byWaveLevel;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
        public byte[] aDummy;
    }
}