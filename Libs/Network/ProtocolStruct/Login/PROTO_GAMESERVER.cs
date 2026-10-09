using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Login
{
    // 90-byte wire entry (Pack=1), verified byte-for-byte against the captured
    // real login-result packet (see cf_gamesrv\Tests\ServerListParserTest.cs):
    //   7 ints end at offset 85, then byte bReturnUserServer @85 and
    //   int m_nProperty @86 -> stride 90 between server entries.
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_GAMESERVER
    {
        public short m_nServerHighProperty;
        public short m_nServerLowProperty;
        public short m_nServerLowLimit;
        public short m_nServerHighLimit;
        public double m_dServerLowKD;
        public double m_dServerHighKD;
        public short m_nServerID;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SERVER_DISPLAY_NAME)]
        public byte[] m_szServerName;

        public int m_nServerPort;
        public int m_nDummy12008;   // real server sends the literal 12008
        public int m_nDummy28004;   // real server sends the literal 28004
        public uint m_dwServerAddr;
        public int m_nServerLimitCount;
        public int m_nServerConnectCount;
        public int m_nEvent;

        public byte bReturnUserServer;
        public int m_nProperty; // 3 = return, 4 = match, 5 = prepare
    }
}
