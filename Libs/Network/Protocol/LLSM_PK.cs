using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.Protocol
{
    public static class LLSM_PK
    {
        public const int PROTOCOL_LLSM = 67;
        
        public const int PROTOCOL_LLSM_LS_CONNECT = 0;
        public const int PROTOCOL_LLSM_LS_CONNECT_RESULT = 1;
        public const int PROTOCOL_LLSM_HEARTBEAT = 2;
        public const int PROTOCOL_LLSM_LAUNCHER_KEYINFO = 3;
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_LLSM_LAUNCHER_KEYINFO
        {
            public long lUSN;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_RSA_KEY_LENGTH)]
            public string szRSAKey;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_IP_ADDRESS_LENGTH)]
            public string szIPAddress;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_LLSM_LS_CONNECT
        {
            public int iMgmtRemotePort;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_LLSM_LS_CONNECT_RESULT
        {
            public enum RESULT
            {
                SUCCESS,
                FAILED,
                ALREADY_CONNECTED
            }
            
            public RESULT eResult;
        }
    }
}