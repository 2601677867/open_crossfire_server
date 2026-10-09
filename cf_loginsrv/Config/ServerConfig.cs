using Commons;

namespace cf_loginsrv.Config
{
    internal partial class CServerConfig : CSingleton<CServerConfig>
    {
        public static string GetBaseLogPath() { return This.m_szBaseLogPath; }
        public static int GetLogLevel() { return This.m_nLogLevel; }
        public static int GetServerDefaultAllocSize() { return This.m_nServerDefaultAllocSize; }
        public static int GetServerRemotePort() { return This.m_nRemotePort + 1; }
        public static int GetClientVersion() { return This.m_nClientVersion; }
        public static int GetMgmtRemotePort() { return This.m_nRemotePort; }
        public static int GetServerRemoteAddrInet() { return This.m_nRemoteAddrInet; }
        public static int GetServerMaxUser() { return This.m_nServerMaxUser; }
        public static int GetBufferSize() { return This.m_nBufferSize; }
        public static bool GetClientValidCheck() { return This.m_bClientValidCheck; }
        public static bool GetServerValidCheck() { return This.m_bServerValidCheck; }
        public static string GetServerRemoteAddr() { return This.m_szRemoteAddr; }
        public static byte GetUseGlobalJoin() { return This.m_byUseGlobalJoin; }
        public static bool GetUseLLSMgmt() { return This.m_bUseLLSMgmt; }
        public static string GetLLSMgmtAddr() { return This.m_szLLSMgmtAddr; }
        public static ushort GetLLSMgmtPort() { return This.m_usLLSMgmtPort; }
        public static int GetSSN() { return This.m_nSSN; }
        public static int GetUseAutoServerSelect() { return This.m_byAutoServerSelect; }
        
        public static string GetGameDBAlias() { return This.m_szGameDBAlias; }
        public static string GetGuildDBAlias() { return This.m_szGuildDBAlias; }
        public static string GetEventDBAlias() { return This.m_szEventDBAlias; }
        public static string GetLogDBAlias() { return This.m_szLogDBAlias; }
    }
}