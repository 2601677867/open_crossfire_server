using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace cf_loginsrv.ServerStruct
{
    public struct GAMESERVER
    {
        public bool m_bGameServerConnected;
        public bool m_bEvent;
        public uint m_dwInternalAddr;
            
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
        public uint m_dwServerAddr;
        public int m_nServerLimitCount;
        public int m_nServerConnectCount;
        public int m_nPassword;
    }
}