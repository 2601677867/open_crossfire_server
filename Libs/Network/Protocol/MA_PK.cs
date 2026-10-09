using System.Runtime.InteropServices;

namespace Network.Protocol
{
    public static class MA_PK
    {
        // Size Defines...
        private const int HOST_NAME_MAX = 255;
        private const int MAIL_TITLE_MAX = 255;
        private const int MAIL_CONTENT_MAX = 2000;
        
        // Protocol Defines...
        
        public const byte PROTOCOL_MA_FIRST_CLASS = 230;
        
        public const byte PROTOCOL_HA_INIT = 0;
        public const byte PROTOCOL_HA_INIT_RESULT = 1;
        public const byte PROTOCOL_MAIL_REQ = 2;
        
        // Structures...
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_HA_INIT
        {
            public int m_nHAID;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HOST_NAME_MAX)]
            public string m_szHostName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_HA_INIT_RESULT
        {
            public RESULT m_eResult;

            public enum RESULT
            {
                SUCCESS,
                FAIL,
                UNKNOWN_ERROR
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MAIL_REQ
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAIL_TITLE_MAX)]
            public string m_szTitle;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAIL_CONTENT_MAX)]
            public string m_szContent;
        }
    }
}