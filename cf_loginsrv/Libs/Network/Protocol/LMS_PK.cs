using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.Protocol
{
    public static class LMS_PK
    {
        public const int PROTOCOL_LOGIN_MGMT = 1;
        
        public const int PROTOCOL_MGMT_CONNECT = 0;
        public const int PROTOCOL_MGMT_CONNECT_RESULT = 1;
        public const int PROTOCOL_MGMT_UPDATE = 2;
        public const int PROTOCOL_MGMT_SERVEROFF = 3;
        public const int PROTOCOL_MGMT_USERINFO_UPDATE = 4;
        public const int PROTOCOL_MGMT_USERINFO_UPDATE_RESULT = 5;
        public const int PROTOCOL_MGMT_SAMEIDCONNECT_FORCE_LEAVE = 6;
        public const int PROTOCOL_MGMT_SAMEIDCONNECT_FORCE_LEAVE_RESULT = 7;
        public const int PROTOCOL_MGMT_SUPERVISOR_NOTIFY = 8;
        public const int PROTOCOL_MGMT_SUPERVISOR_ROLLING = 9;
        public const int PROTOCOL_MGMT_SUPERVISOR_ROLLSTOP = 10;
        public const int PROTOCOL_MGMT_SUPERVISOR_KICK = 11;
        public const int PROTOCOL_MGMT_SUPERVISOR_KICK_RESULT = 12;
        public const int PROTOCOL_MGMT_SUPERVISOR_QKICK = 13;
        public const int PROTOCOL_MGMT_SUPERVISOR_QKICK_RESULT = 14;
        public const int PROTOCOL_MGMT_SUPERVISOR_CHATOFF = 15;
        public const int PROTOCOL_MGMT_SUPERVISOR_CHATOFF_RESULT = 16;
        public const int PROTOCOL_MGMT_SUPERVISOR_CHATON = 17;
        public const int PROTOCOL_MGMT_SUPERVISOR_CHATON_RESULT = 18;
        public const int PROTOCOL_MGMT_SUPERVISOR_WHISPER = 19;
        public const int PROTOCOL_MGMT_SUPERVISOR_WHISPER_RESULT = 20;
        public const int PROTOCOL_MGMT_EXCEPTION_DATA = 21;
        public const int PROTOCOL_MGMT_AUTO_EVENT_START = 22;
        public const int PROTOCOL_MGMT_AUTO_EVENT_END = 23;
        public const int PROTOCOL_MGMT_CROSSFIRE_EVENT_SELECT = 24;
        public const int PROTOCOL_MGMT_CROSSFIRE_EVENT_SELECT_RESULT = 25;
        public const int PROTOCOL_MGMT_GACHA_WINNER_NTY = 26;
        public const int PROTOCOL_MGMT_HEARTBEAT = 27;
        public const int PROTOCOL_MGMT_SOCKET_CONTEXT_OVERFLOW = 28;
        public const int PROTOCOL_MGMT_FAME_LIST_UPDATE = 29;
        public const int PROTOCOL_MGMT_FAME_LIST_UPDATE_FINISHED = 30;
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_AUTO_EVENT_START
        {
            public int iExpPercentage;
            public int iGPPercentage;
            
            [MarshalAs(UnmanagedType.Bool)]
            public bool bDeathReset;

            [MarshalAs(UnmanagedType.Bool)]
            public bool bAutoEventContinue;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AUTO_EVENT_MEMO_LENGTH)]
            public byte[] aszMemo;
            
            public int iServerNo;
            public int iMapID;
            public int iSubMapID;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_CONNECT
        {
            public short m_sServerHighProperty;
            public short m_sServerLowProperty;
            public short m_sServerLowLimit;
            public short m_sServerHighLimit;
            public double m_dLowLimitKillDeath;
            public double m_dHighLimitKillDeath;
            public byte m_byPassword;
            public short m_sDummy;
            public int m_iRemoteAddrInet;
            public int m_iRemotePort;
            public int m_iServerMaxUser;
            public int m_iServerCurrentUser;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_CONNECT_RESULT
        {
            public enum CONNECTRESULT
            {
                SUCCESS,
                ALREADY_CONNECTED,
                FAILED
            }

            public CONNECTRESULT m_eResult;
            public short m_sServerIndex;
            public byte m_byGlobalJoin;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_USERINFO_UPDATE
        {
            public enum ACTION
            {
                CHECKLOGIN,
                LOGOUTUSER
            }

            public ACTION m_eAction;
            public long m_lUSN;
            public int m_iLoginTime;
            public int m_iClientKey;
            public double m_dConnectTime;
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CRYPTED_DATA_SIZE)]
            //public byte[] m_aAuthKey;
            public long m_lKey1;
            public long m_lKey2;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_IP_ADDRESS_LENGTH)]
            public string m_szClientIP;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_USERINFO_UPDATE_RESULT
        {
            public enum RESULT
            {
                SUCCESS,
                UNKNOWNERROR,
                INVALID_KEY_VALUE,
                DUPLICATE
            }
            
            public RESULT m_eResult;
            public long m_lUSN;
            public int m_iClientKey;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SAMEIDCONNECT_FORCE_LEAVE
        {
            public long m_lUSN;
            public int m_iLoginTime;
            public double m_dConnectTime;
        }
        
        public struct EXCEPTIONDATA
        {
            public long m_lUSN;
            //public int m_iLoginTime;
            public double m_dConnectTime;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_EXCEPTION_DATA
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_EXCEPTION_DATA_PER_PACKET)]
            public EXCEPTIONDATA[] aExceptionData;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_CROSSFIRE_EVENT_SELECT_RESULT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCallName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_HEARTBEAT
        {
            public uint m_iRemoteAddrInet;
            public int m_iRemotePort;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_ROLLING
        {
            public SP_PK.PROTO_ROLLING tProtoRolling;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_ROLLSTOP
        {
            public short nServerNo;
            public short nChannelNo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_KICK
        {
            public long nTargetUSN;
            public long nRequesterUSN;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_KICK_RESULT
        {
            public long nUSN;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_QKICK
        {
            public long nTargetUSN;
            public long nRequesterUSN;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_QKICK_RESULT
        {
            public long nUSN;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_NOTIFY
        {
            public short nServerNo;
            public short nChannelNo;
            public short nNotifyLength;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_ROLLING_MEMO_LENGTH)]
            public byte[] aszContent;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_WHISPER
        {
            public long nRequesterUSN;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszRequesterCharacterName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszTargetCharacterName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHATTING_LENGTH)]
            public byte[] aszContent;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_WHISPER_RESULT
        {
            public long nRequesterUSN;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszRequesterCharacterName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszTargetCharacterName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CHATTING_LENGTH)]
            public byte[] aszContent;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_CHATOFF
        {
            public long nTargetUSN;
            public long nRequesterUSN;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_CHATOFF_RESULT
        {
            public long nUSN;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_CHATON
        {
            public long nTargetUSN;
            public long nRequesterUSN;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_MGMT_SUPERVISOR_CHATON_RESULT
        {
            public long nUSN;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] aszCharacterName;
        }
    }
}