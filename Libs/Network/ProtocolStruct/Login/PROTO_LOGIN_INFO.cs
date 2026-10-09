using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Login
{
    // 9107-byte login-result body (Pack=1). Together with the 4-byte m_eResult
    // prefix of PROTO_REQUEST_CONNECT_RESULT the packet is exactly 9111 bytes -
    // the size and layout of the captured real login-result packet
    // (see cf_gamesrv\Tests\ServerListParserTest.cs):
    //   prefix 43 + server array 100*90=9000 + tail 64 = 9107 (+4 = 9111).
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_LOGIN_INFO
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
        public byte[] m_szCallName;

        public long m_lUSN;                 // 8 bytes @13 (capture: data+17)
        public byte m_bySpecialUser;
        public byte m_byClanMember;
        public short m_nLevel;
        public int m_nKill;
        public int m_nDeath;
        public double m_dKillDeath;

        public short m_nServerCount;        // ends prefix @43
            
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GAMESERVER_COUNT)]
        public PROTO_GAMESERVER[] m_aServers;

        // 64-byte tail, order verified against the capture:
        // m_nBannedTime = 500 @tail+17, season=17 @36, nty=1 @37, byNew=1 @44,
        // szRegisterDate = "20150710195938" @49..63.
        public byte m_bySupervisor;
        public int m_bClearedPoint;
        public int m_nDummy;
        public long m_lKey1;
        public int m_nBannedTime;           // 500 in the capture
        public byte m_byUseGlobalJoin;
        public byte m_byWaveLevel;
        public byte m_byUseAutoServerSelect;
        public long m_lKey2;
        public int m_iSSN;
        public byte m_bySeason;             // latest 17
        public byte m_byNewSeasonNty;        // 1
        public int m_iAddr;
        public short m_sInfinityAIEvent;
        public byte byNew;                  // 1
        public int m_dummy5;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string szRegisterDate;
    }

    // Historical alias of the same capture-verified layout (m_byFlag name).
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_LOGIN_INFO_
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
        public byte[] m_szCallName;

        public long m_lUSN;
        public byte m_byFlag; // 1
        public byte m_byClanMember;
        public short m_nLevel;
        public int m_nKill;
        public int m_nDeath;
        public double m_dKillDeath;

        public short m_nServerCount;
            
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GAMESERVER_COUNT)]
        public PROTO_GAMESERVER[] m_aServers;

        public byte m_bySupervisor;
        public int m_bClearedPoint;
        public int m_nDummy;
        public long m_lKey1;
        public int m_nBannedTime; // 500
        public byte m_byUseGlobalJoin;
        public byte m_byWaveLevel;
        public byte m_byUseAutoServerSelect;
        public long m_lKey2;
        public int m_iSSN;
        public byte m_bySeason; // latest 17
        public byte m_byNewSeasonNty; // 1
        public int m_iAddr;
        public short m_sInfinityAIEvent;
        public byte byNew; // 1
        public int m_dummy5;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 15)]
        public string szRegisterDate;
    }
}
