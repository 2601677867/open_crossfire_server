using Network.Protocol;
using Network.Server;

namespace cf_loginsrv.Socket
{
    public class CLGUserContext : CUserTokenBase
    {
        public struct TStatInfo
        {
            public bool bReconnected { get; set; }
            public long lUSN { get; set; }
            public long lReqUSN { get; set; }
            public bool bCharacterCreated { get; set; }
            public byte bySupervisor { get; set; }
            public string szLoginDateTime { get; set; }
            public int nLoginUDate { get; set; }
            public byte byClanMember { get; set; }
            public string szClanID { get; set; }
            public string szClanSrl { get; set; }
            public string szClanUserSrl { get; set; }
            public short nLevel { get; set; }
            public int nEnemyKill { get; set; }
            public int nDeathCount { get; set; }
            public short wSex { get; set; }
            public short wRegion { get; set; }
            public short wAge { get; set; }
            public string szRegID { get; set; }
            public long lClanKey { get; set; }
            public short sClanLLevel { get; set; }
            public byte byClanUserGrade { get; set; }
            public int nSelectServerLimitTime { get; set; }
            public int nCreateCharLimitTime { get; set; }
            public int nStatus { get; set; }
            public string szDayPartKey { get; set; }
            public string szPCBID { get; set; }
            public string szName { get; set; }
            public string szNick { get; set; }
            public long lExp { get; set; }
            public int nGamePoint { get; set; }
            public int nTodayEXP { get; set; }
            public int nTodayGamePoint { get; set; }
            public int nTodayPlay { get; set; }
            public int nTodayKill { get; set; }
            public int nTodayDeath { get; set; }
            public int nTodayHeadshot { get; set; }
            public int nTodayWin { get; set; }
            public int nTodayLose { get; set; }
            public int nTodayDraw { get; set; }
            public LG_PK.PROTO_REQUEST_CONNECT tLoginInfo;
            public double dConnectTime { get; set; }
            public int nConnectDenyUDate { get; set; }
            public string szHGWKey { get; set; }
            public byte byEvent { get; set; }
            public byte byWaveLevel { get; set; }
            public bool bForceNickChange { get; set; }
            //public bool bLogin { get; set; }
            //public bool bExpired { get; set; }
            public bool bNoticed { get; set; }
        }
        
        public TStatInfo tStatInfo;

        public short sMgmtServerNo { get; set; }
        public short sMgmtServerIndex { get; set; }
        public int nMgmtRemotePort { get; set; }
        public int iClientKey { get; set; }
        
        public CLGUserContext()
        {
            sMgmtServerIndex = -1;
            sMgmtServerNo = -1;

            tStatInfo = new TStatInfo
            {
                lUSN = -1,
                bCharacterCreated = true,
                szNick = ""
            };
        }
        
        public override IUserTokenBase CreateUserToken()
        {
            return new CLGUserContext();
        }
    }
}