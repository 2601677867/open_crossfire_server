using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.Protocol
{
    public static class NCLS_PK
    {
        public const int PROTOCOL_NCLS = 66;
        
        public const int PROTOCOL_NCLS_REQUEST_LOGIN = 0;
        public const int PROTOCOL_NCLS_REQUEST_LOGIN_RESULT = 1;
        public const int PROTOCOL_NCLS_REQUEST_AREA_LIST = 2;
        public const int PROTOCOL_NCLS_REQUEST_AREA_LIST_RESULT = 3;
        public const int PROTOCOL_NCLS_REQUEST_NEWS_URL = 4;
        public const int PROTOCOL_NCLS_REQUEST_NEWS_URL_RESULT = 5;
        public const int PROTOCOL_NCLS_USERDATA = 6;
        public const int PROTOCOL_NCLS_USERDATA_UPDATE_NTY = 7;
        public const int PROTOCOL_NCLS_REQUEST_CHECK_NICKNAME = 8;
        public const int PROTOCOL_NCLS_REQUEST_CHECK_NICKNAME_RESULT = 9;
        public const int PROTOCOL_NCLS_REQUEST_CONFIRM_NICKNAME = 10;
        public const int PROTOCOL_NCLS_REQUEST_CONFIRM_NICKNAME_RESULT = 11;
        public const int PROTOCOL_NCLS_REQUEST_START_GAME = 12;
        public const int PROTOCOL_NCLS_REQUEST_START_GAME_RESULT = 13;
        public const int PROTOCOL_NCLS_REQUEST_RECENTLY_PLAYED_SERVERS = 14;
        public const int PROTOCOL_NCLS_REQUEST_RECENTLY_PLAYED_SERVERS_RESULT = 15;
        public const int PROTOCOL_NCLS_UPDATE_RECENTLY_PLAYED_SERVERS = 16;
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_RECENTLY_PLAYED_SERVERS_RESULT
        {
            public enum RESULT
            {
                SUCCESS,
                FAILED
            }
            
            public RESULT eResult;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_RECENT_AREA_COUNT)]
            public int[] aServerIDs;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_UPDATE_RECENTLY_PLAYED_SERVERS
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_RECENT_AREA_COUNT)]
            public int[] aServerIDs;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_LOGIN
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_USERNAME_LENGTH)]
            public byte[] aszUserName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PASSWORD_LENGTH)]
            public byte[] aszPassword;

            public int iVersion;
            
            [MarshalAs(UnmanagedType.I1)]
            public bool bGenerateToken;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TOKEN_LENGTH)]
            public byte[] aszToken;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_LOGIN_RESULT
        {
            public enum RESULT
            {
                SUCCESS,
                UNKNOWN_ERROR,
                INVALID_USERNAME_OR_PASSWORD,
                NOT_ACTIVATED,
                UPDATE_AVAILABLE,
                TOKEN_EXPIRED
            }

            public RESULT eResult;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_TOKEN_LENGTH)]
            public byte[] aszToken;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_AREA_LIST { }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_AREA_LIST_RESULT
        {
            public enum RESULT
            {
                SUCCESS,
                FAILED
            }

            public RESULT eResult;
            public int iAreaCount;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AREA_COUNT)]
            public PROTO_NCLS_AREA[] aAreas;

            public ushort GetSize()
            {
                return (ushort) (sizeof(RESULT) + sizeof(int) + iAreaCount * Marshal.SizeOf<PROTO_NCLS_AREA>());
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_AREA
        {
            public uint uiAreaID;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AREA_NAME_LENGTH)]
            public byte[] aszAreaName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AREA_VERSION_LENGTH)]
            public byte[] aszAreaVersion;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_ADDRESS_LENGTH)]
            public byte[] aszServerAddr;
            
            public int iLimitCount;
            public int iConnectCount;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_NEWS_URL { }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_NEWS_URL
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_NEWS_URL_POSITION_LENGTH)]
            public byte[] aszPosition;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_NEWS_URL_LENGTH)]
            public byte[] aszUrl;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_NEWS_URL_RESULT
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_NEWS_URL_BLOCK_COUNT)]
            public PROTO_NCLS_NEWS_URL[] aNewsUrls;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_USERDATA
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_USERNAME_LENGTH)]
            public byte[] aszName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_NICKNAME_LENGTH)]
            public byte[] aszNick;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_CHECK_NICKNAME
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_NICKNAME_LENGTH)]
            public byte[] aszName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_CHECK_NICKNAME_RESULT
        {
            public enum RESULT
            {
                AVAILABLE,
                TOO_SHORT,
                BAD_SYMBOL,
                ABUSE_NAME,
                NAME_USED
            }

            public RESULT eResult;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_CONFIRM_NICKNAME
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_NICKNAME_LENGTH)]
            public byte[] aszName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_CONFIRM_NICKNAME_RESULT
        {
            public enum RESULT
            {
                SUCCESS,
                FAILED,
                NICK_DENIED
            }

            public RESULT eResult;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_START_GAME
        {
            public int iServerID;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_NCLS_REQUEST_START_GAME_RESULT
        {
            public enum RESULT
            {
                SUCCESS,
                FAILED,
                FAILED_GET_LOGIN_SERVER_LIST
            }

            public RESULT eResult;

            public long lUSN;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_IP_ADDRESS_LENGTH)]
            public byte[] aszAddress;
            public ushort usPort;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_AREA_NAME_LENGTH)]
            public byte[] aszAreaName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_RSA_KEY_LENGTH)]
            public byte[] aszRSAKey;
        }
    }
}