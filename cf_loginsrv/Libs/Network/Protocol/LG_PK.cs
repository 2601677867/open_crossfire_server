using System.Runtime.InteropServices;
using Network.ProtocolStruct;
using Network.ProtocolStruct.Login;
using Network.SharedFolder;
using static Network.Protocol.P_SZ;

// ReSharper disable FieldCanBeMadeReadOnly.Global
// ReSharper disable MemberCanBePrivate.Global

namespace Network.Protocol
{
    public static class LG_PK
    {
        public const int PROTOCOL_LOGIN_FIRST_CLASS = 0;
        
        public const int PROTOCOL_REQUEST_CONNECT = 0;
        public const int PROTOCOL_REQUEST_CONNECT_RESULT = 1;
        public const int PROTOCOL_REQUEST_RECONNECT = 2;
        public const int PROTOCOL_REQUEST_RECONNECT_RESULT = 3;
        public const int PROTOCOL_REQUEST_UPDATE = 4;
        public const int PROTOCOL_REQUEST_UPDATE_RESULT = 5;
        public const int PROTOCOL_DUMMY1 = 6;
        public const int PROTOCOL_TIMEOUT_NTY = 7;
        public const int PROTOCOL_REQUEST_CHARACTER_CREATE = 8;
        public const int PROTOCOL_REQUEST_CHARACTER_CREATE_RESULT = 9;
        public const int PROTOCOL_REQUEST_CHARACTER_NAMECHECK = 10;
        public const int PROTOCOL_REQUEST_CHARACTER_NAMECHECK_RESULT = 11;
        public const int PROTOCOL_REQUEST_SAMEIDCONNECT_FORCE_LEAVE = 12;
        public const int PROTOCOL_REQUEST_SAMEIDCONNECT_FORCE_LEAVE_RESULT = 13;
        public const int PROTOCOL_REQUEST_SAMEIDCONNECT_FORCE_LEAVE_NTY = 14;
        public const int PROTOCOL_REQUEST_SERVER_CONNECT_REQUEST = 15;
        public const int PROTOCOL_REQUEST_SERVER_CONNECT_RESULT = 16;
        public const int PROTOCOL_REQUEST_SERVER_DISCONNECT_REQUEST = 17;
        public const int PROTOCOL_REQUEST_SERVER_DISCONNECT_RESULT = 18;
        public const int PROTOCOL_REQUEST_RECONNECT_FOR_DIST = 19;
        public const int PROTOCOL_REQUEST_RECONNECT_FOR_DIST_RESULT = 20;
        public const int PROTOCOL_REQUEST_DAILY_SCORE = 21;
        public const int PROTOCOL_REQUEST_DAILY_SCORE_RESULT = 22;
        public const int PROTOCOL_REQUEST_CHANGE_NAME = 23;
        public const int PROTOCOL_REQUEST_CHANGE_NAME_RESULT = 24;
        public const int PROTOCOL_PROTECTED_KEY = 25;
        public const int PROTOCOL_REQUEST_TUTORIAL_POPUP = 26;
        public const int PROTOCOL_REQUEST_TUTORIAL_POPUP_END = 27;
        public const int PROTOCOL_NAME_CHANGE_NTY = 35;
        public const int PROTOCOL_CONFIRM_LATEST_POLICY = 37;

        public const int PROTOCOL_AUTOSELECT_SERVER_NTY = 2;

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_PROTECTED_KEY
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PROTECTED_KEYS)]
            public int[] m_aProtectedKeys;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PROTECTED_KEYS)]
            public int[] m_aProtectedKeyTypes;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_CONNECT_INFO
        {
            public long m_lUSN;
            
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CONNECT_USERNAME_LENGTH)]
            //public byte[] m_szName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CONNECT_PASSWORD_LENGTH)]
            public byte[] m_szPassword;

            public int m_nClientKey;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PCBID_LENGTH)]
            public byte[] m_szPCBID;

            public short m_nVersion;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GUID_LENGTH)]
            public byte[] m_szGuid;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_RAS_LENGTH)]
            public byte[] m_szRAS;
            
            //public PROTO_CRYPTED_DATA m_tCryptedData;
            public int m_iKeyLength2;
            public int m_iKeyLength;
            
            public int m_iUSN;
            public int m_iLocalAddr;
            public int m_iGameID; // 2104833
            public int m_iDummy1; // 0
            
            //[MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_HGW_SECRETKEY_LENGTH)]
            //public byte[] m_szHGWKey;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_MAC_ADDRESS_LENGTH)]
            public byte[] m_szMacAddr;
            public int m_iFromNCLS;
            
            public long m_lKey1;
            public long m_lKey2;
            public int m_iSSN;
            public int m_iIsClientFirstLogin;
            public int m_iDummy9; // 0x419
            public int m_iDummy10; // 1
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_CONNECT
        {
            public PROTO_CONNECT_INFO tConnectInfo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_CONNECT_RESULT
        {
            public CONNECTRESULT m_eResult;
            public PROTO_LOGIN_INFO tLoginInfo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_UPDATE_RESULT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GAMESERVER_COUNT)]
            public PROTO_GAMESERVER_UPDATE[] aServers;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_RECONNECT
        {
            public PROTO_CONNECT_INFO tConnectInfo;
            public PROTO_RETURN_INFO tReturnInfo;
            //public PROTO_CRYPTED_DATA tCryptedData;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_RECONNECT_RESULT
        {
            public CONNECTRESULT m_eResult;
            public PROTO_LOGIN_INFO tLoginInfo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_RECONNECT_FOR_DIST
        {
            public PROTO_CONNECT_INFO tConnectInfo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_RECONNECT_FOR_DIST_RESULT
        {
            public CONNECTRESULT m_eResult;
            public PROTO_LOGIN_INFO tLoginInfo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_CHARACTER_CREATE
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] m_szCallName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_CHARACTER_CREATE_RESULT
        {
            public enum CHARACTERCREATE_RESULT
            {
                CHARACTERCREATE_SUCCESS,
                CHARACTERCREATE_FAIL,
                CHARACTERCREATE_UNKNOWNERROR,
                CHARACTERCREATE_WRONGNAME,
                CHARACTERCREATE_NEED_RECONNECT
            }
            
            public CHARACTERCREATE_RESULT m_eResult;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_CHARACTER_NAMECHECK
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] m_szCallName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_CHARACTER_NAMECHECK_RESULT
        {
            public enum NAMECHECK_RESULT
            {
                CHARACTERNAMECHECK_POSSIBLE,
                CHARACTERNAMECHECK_DENY_WORD,
                CHARACTERNAMECHECK_IMPOSSIBLE,
                CHARACTERNAMECHECK_NULL,
                CHARACTERNAMECHECK_BLANK,
                CHARACTERNAMECHECK_SPECIAL_CHAR,
                CHARACTERNAMECHECK_LONG_CHAR,
                CHARACTERNAMECHECK_SHORT_CHAR
            }
            
            public NAMECHECK_RESULT m_eResult;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_SERVER_CONNECT_REQUEST
        {
            public enum AUTOJOINID
            {
                AUTOJOIN_SERVER,
                AUTOJOIN_BATTLEROYAL,
                AUTOJOIN_RANKEDMATCH,
                AUTOJOIN_GLOBALROOM
            }
            
            public short m_sServerID;
            public AUTOJOINID m_nAutoFuncID;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_SERVER_CONNECT_RESULT
        {
            public enum CONNECTRESULT
            {
                SUCCESS,
                FAIL,
                SERVER_FULL
            }
            
            public CONNECTRESULT m_eResult;
            public short m_nSelectIndex;
            public PROTO_RETURN_INFO tProtoReturnInfo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_SERVER_DISCONNECT_REQUEST
        {
            public DISCONNECTREASON m_eReason;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_SERVER_DISCONNECT_RESULT
        {
            public DISCONNECTREASON m_eReason;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_DAILY_SCORE_RESULT
        {
            public int m_iTodayExp;
            public int m_iTodayGamePoint;
            public int m_iTodayKill;
            public int m_iTodayDeath;
            public int m_iTodayHeadshot;
            public int m_iTodayWin;
            public int m_iTodayLose;
            public long m_lExp;
            public int m_iGamePoint;
            public int m_iMorePlayCountToNextLevel;
            public short m_nNextLev;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_TUTORIAL_POPUP_END
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = (int) ROUNDTYPE.ROUNDTYPE_MAX)]
            public short[] m_aValues;
        }
         
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_CONFIRM_LATEST_POLICY
        {
            public byte m_byAgree;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_CHANGE_NAME
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
            public byte[] m_szCallName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_REQUEST_CHANGE_NAME_RESULT
        {
            public enum NAMECHANGE_RESULT // case 1/2/3/6, case 4
            {
                CHARACTERNAMECHECK_POSSIBLE,
                CHARACTERNAMECHECK_DENY_WORD,
                CHARACTERNAMECHECK_IMPOSSIBLE,
                CHARACTERNAMECHECK_NULL,
                CHARACTERNAMECHECK_BLANK,
                CHARACTERNAMECHECK_SPECIAL_CHAR,
                CHARACTERNAMECHECK_LONG_CHAR,
                CHARACTERNAMECHECK_SHORT_CHAR
            }
            
            public NAMECHANGE_RESULT m_eResult;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NAME_CHANGE_NTY
        {
            public enum NAMECHANGE_NTY
            {
                NONE,
                CHANGED_TO_DEFAULT_NAME,
                DB_ERROR_1,
                DB_ERROR_2
            }
            
            public NAMECHANGE_NTY m_eResult;
        }
    }
}