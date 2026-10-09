using System.Runtime.InteropServices;

namespace Network.Protocol
{
    public static class HA_PK
    {
        public const byte PROTOCOL_HA_FIRST_CLASS = 231;
        
        public const byte PROTOCOL_GS_INIT = 1;
        public const byte PROTOCOL_GS_INIT_FAULT = 2;
        public const byte PROTOCOL_GS_PERFORMANCE_INFO = 3;
        public const byte PROTOCOL_GS_HEARTBEAT = 4;
        public const byte PROTOCOL_GS_STAT_INFO = 5;
        public const byte PROTOCOL_GS_REGION_INFO = 6;
        public const byte PROTOCOL_GS_ANNOUNCE_CMD = 7;
        public const byte PROTOCOL_GS_ORDER_CMD = 8;
        public const byte PROTOCOL_GS_WARNING_NTF = 9;
        public const byte PROTOCOL_GS_STAT_INFO_PC = 10;
        public const byte PROTOCOL_GS_REGION_INFO_PC = 11;
        public const byte PROTOCOL_GS_ANNOUNCE_UNICODE_CMD = 12;
        public const byte PROTOCOL_GS_INIT_RESULT = 13;
        public const byte PROTOCOL_GSI_NTY = 14;
        public const byte PROTOCOL_GSI_PERFORMANCE_NTY = 15;
        public const byte PROTOCOL_GSI_STAT_INFO_NTY = 16;
        public const byte PROTOCOL_GSI_REGION_INFO_PC_NTY = 16;
        public const byte PROTOCOL_MC_CONNECT_REQ = 17;
        public const byte PROTOCOL_MC_CONNECT_RESULT = 18;
        public const byte PROTOCOL_MC_LOGIN_REQ = 19;
        public const byte PROTOCOL_MC_LOGIN_RESULT = 20;
        public const byte PROTOCOL_MC_GET_HOSTLIST_REQ = 21;
        public const byte PROTOCOL_MC_GET_HOSTLIST_RESULT = 22;
        public const byte PROTOCOL_LOG_INFO = 23;
        public const byte PROTOCOL_LOG_INFO_MC = 24;
        
        // Structures...
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgPMSInitNtf_Tag
        {
            public string m_szSvcName;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HA_SZ.PROTO_INIT_PASSWORD_MAX)]
            public string m_szCryptedPassword;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgPMSInitFaultNtf_Tag
        {
            public string m_szSvcName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GS_INIT_RESULT
        {
            public enum EResult
            {
                SUCCESS,
                WRONG_PW,
                UNKNOWN_ERROR
            }
            
            public EResult m_eResult;
            public int m_nGSID;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgPMSHeartBeatAns_Tag
        {
            public int m_nReqNum;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgPMSPerformAns_Tag
        {
            public float m_fPrivateWorkingSetCpu;
            public float m_fWorkingSetMem;
            public float m_fPrivateWorkingSetMem;
            public float m_fSystemCpu;
            public long m_lSystemMem;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgPMSPerformMCNty_Tag
        {
            public int m_nGSID;
            public msgPMSPerformAns_Tag m_tPerformInfo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgPMSStatInfoAns_Tag
        {
            public int m_nOnlines;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgPMSStatInfoMCNty_Tag
        {
            public int m_nGSID;
            public msgPMSStatInfoAns_Tag m_tStatInfo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgPMSRegionInfoPCAns_Tag
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HA_SZ.NETWORK_INTERFACE_NAME_MAX)]
            public string m_szNetworkInterfaceName;
            
            public double m_dNetworkPercentage;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgPMSRegionInfoPCMCNty_Tag
        {
            public int m_nGSID;
            public msgPMSRegionInfoPCAns_Tag m_tRegionInfoPC;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GSI_STATUS_NTY
        {
            public enum EGSIStatus : byte
            {
                STARTING,
                STARTED,
                STOPPED
            }
            
            public int m_nGSID;
            public EGSIStatus m_eStatus;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgMCConnectReq_Tag
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HA_SZ.HOST_NAME_MAX)]
            public string m_szHostName;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgMCConnectRes_Tag
        {
            public enum EResult : byte
            {
                CONNECTION_ALLOW,
                CONNECTION_DISALLOW
            }
            
            public EResult m_eResult;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgMCGetHostListReq_Tag
        {
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAME_SERVER_BASIC_INFO
        {
            public int m_nGSID;
                
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HA_SZ.HOST_GROUP_NAME_MAX)]
            public string m_szHostGroupName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HA_SZ.GSI_TYPE_NAME_MAX)]
            public string m_szGSIType;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HA_SZ.HOST_NAME_MAX)]
            public string m_szHostName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HA_SZ.IP_ADDRESS_MAX)]
            public string m_szIPAddress;
            
            public byte m_byHAStatus;
            public byte m_byGSIStatus;
            public int m_nPort;
            public byte m_byStatus;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_GAME_SERVER_INFO
        {
            public PROTO_GAME_SERVER_BASIC_INFO m_tBasicInfo;
            public msgPMSPerformAns_Tag m_tPerformInfo;
            public msgPMSStatInfoAns_Tag m_tStatInfo;
            public msgPMSRegionInfoPCAns_Tag m_tRegionInfoPC;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgMCGetHostListRes_Tag
        {
            public enum EResult
            {
                CONNECTION_DISALLOW = -1,
                RESULT_SUCCESS
            }
            
            public EResult m_eResult;
            public int m_nGSCount;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HA_SZ.GAME_SERVER_MAX)]
            public PROTO_GAME_SERVER_INFO[] m_aGameServers;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgMCLoginReq_Tag
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HA_SZ.USER_NAME_MAX)]
            public string m_szName;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HA_SZ.PASSWORD_MAX)]
            public string m_szPassword;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct msgMCLoginRes_Tag
        {
            public enum EResult
            {
                CONNECTION_DISALLOW = -1,
                RESULT_SUCCESS,
                RESULT_INVALID_USERNAME_OR_PASSWORD,
                RESULT_HOST_BIND_MISMATCH,
                RESULT_DB_UNKOWN_ERROR,
                RESULT_UNKNOWN_ERROR
            }
            
            public EResult m_eResult;
            public int m_nPriviledge;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HA_SZ.USER_NICKNAME_MAX)]
            public string m_szNickName;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HA_SZ.EMAIL_MAX)]
            public string m_szEmail;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_LOG_INFO
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HA_SZ.LOG_CONTENT_MAX)]
            public string m_szContent;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_LOG_INFO_MC
        {
            public int m_nGSID;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = HA_SZ.LOG_CONTENT_MAX)]
            public string m_szContent;
        }
    }
}