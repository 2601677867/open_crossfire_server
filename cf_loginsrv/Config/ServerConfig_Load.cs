using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using cf_loginsrv.Util;

namespace cf_loginsrv.Config
{
    internal partial class CServerConfig
    {
        private readonly string m_szSrvInfoPath = AppDomain.CurrentDomain.BaseDirectory + "/ServerInfo.ini";
        private readonly string m_szSecureCodePath = AppDomain.CurrentDomain.BaseDirectory + "/SecureCode.ini";

        private string m_szBaseLogPath;
        private string m_szRemoteAddr;
        private int m_nRemoteAddrInet;
        private int m_nRemotePort;
        private int m_nServerMaxUser;
        private int m_nLogLevel;
        private int m_nBufferSize;
        private int m_nServerDefaultAllocSize;

        private string m_szGameDBAlias;
        private string m_szGuildDBAlias;
        private string m_szEventDBAlias;
        private string m_szLogDBAlias;

        private int m_nSSN;
        
        private bool m_bUseLLSMgmt;
        private string m_szLLSMgmtAddr;
        private ushort m_usLLSMgmtPort;

        private int m_nClientVersion;
        private int m_nLimitTime;
        private int m_nReLimitTime;

        private bool m_bClientValidCheck;
        private bool m_bServerValidCheck;
        
        private bool m_bTestServer;
        private byte m_byUseGlobalJoin;
        private byte m_byAutoServerSelect;

        public bool InitConfig()
        {
            try
            {
                if (!File.Exists(m_szSrvInfoPath)) return false;
                
                for (var i = 1; i <= CProtectedKey.MAX_CLIENT_SECURE_CODE_COUNT; i++)
                {
                    var iKey = GetIniValueInt(m_szSecureCodePath, "Client", $"Key{i}", int.MaxValue, false);
                    if (iKey == int.MaxValue) break;
                    CProtectedKey.GetInstance().AddProtectedKey(iKey, CProtectedKey.ESecureCodeType.TYPE_CLIENT);
                }

                for (var i = 1; i <= CProtectedKey.MAX_SERVER_SECURE_CODE_COUNT; i++)
                {
                    var iKey = GetIniValueInt(m_szSecureCodePath, "Server", $"Key{i}", int.MaxValue, false);
                    if (iKey == int.MaxValue) break;
                    CProtectedKey.GetInstance().AddProtectedKey(iKey, CProtectedKey.ESecureCodeType.TYPE_SERVER);
                }

                m_nLogLevel = GetIniValueInt(m_szSrvInfoPath, "LogInfo", "ServerLogLevel", 4);
                m_szBaseLogPath = GetIniValueString(m_szSrvInfoPath, "LogInfo", "ServerLogPath", "C:\\Log");
                
                m_szGameDBAlias = GetIniValueString(m_szSrvInfoPath, "ServerInfo", "GameDB", "");
                m_szLogDBAlias = GetIniValueString(m_szSrvInfoPath, "ServerInfo", "LogDB", "");
                m_szGuildDBAlias = GetIniValueString(m_szSrvInfoPath, "ServerInfo", "GuildDB", "CF_GAMEDB");
                m_szEventDBAlias = GetIniValueString(m_szSrvInfoPath, "ServerInfo", "EventDB", "CF_EVENT");

                m_szRemoteAddr = GetIniValueString(m_szSrvInfoPath, "ServerInfo", "ServerServiceForceIP", "0.0.0.0");
                if (m_szRemoteAddr == "0.0.0.0")
                {
                    var szPrivateIPRule = GetIniValueString(m_szSrvInfoPath, "ServerInfo", "PrivateIPRule", "");
                    if (string.IsNullOrEmpty(szPrivateIPRule))
                    {
                        Console.WriteLine("Empty szPrivateRule!!!!");
                        return false;
                    }

                    var NetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

                    foreach (NetworkInterface NetworkIntf in NetworkInterfaces)
                    {
                        var IPInterfaceProperties = NetworkIntf.GetIPProperties();
                        var UnicastIPAddressInformationCollection =
                            IPInterfaceProperties.UnicastAddresses;

                        foreach (var UnicastIPAddressInformation in UnicastIPAddressInformationCollection)
                        {
                            if (UnicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                            {
                                if (UnicastIPAddressInformation.Address.ToString().StartsWith(szPrivateIPRule))
                                {
                                    m_szRemoteAddr = UnicastIPAddressInformation.Address.ToString();
                                    break;
                                }
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(m_szRemoteAddr) || m_szRemoteAddr == "0.0.0.0") return false;
                }

                var szSplitAddr = m_szRemoteAddr.Split('.');
                m_nRemoteAddrInet =
                    Convert.ToByte(szSplitAddr[3]) << 24 | Convert.ToByte(szSplitAddr[2]) << 16 |
                    Convert.ToByte(szSplitAddr[1]) << 8 | Convert.ToByte(szSplitAddr[0]);

                m_nRemotePort = GetIniValueInt(m_szSrvInfoPath, "ServerInfo", "ServerServiceForcePort", 13007);
                m_nServerMaxUser = GetIniValueInt(m_szSrvInfoPath, "ServerInfo", "ServerMaxUser", 10000);
                m_nBufferSize = GetIniValueInt(m_szSrvInfoPath, "ServerInfo", "ServerMaxBufferSize", 1024);
                m_nServerDefaultAllocSize = GetIniValueInt(m_szSrvInfoPath, "ServerInfo", "ServerDefaultAllocSize", 50000, false);

                m_nClientVersion = GetIniValueInt(m_szSrvInfoPath, "ServerInfo", "ClientVersion", 1);
                m_nLimitTime = GetIniValueInt(m_szSrvInfoPath, "ServerInfo", "LimitTime", 30);
                m_nReLimitTime = GetIniValueInt(m_szSrvInfoPath, "ServerInfo", "ReLimitTime", 10);

                m_nSSN = GetIniValueInt(m_szSrvInfoPath, "ServerInfo", "SSN", 318, false);

                m_bClientValidCheck = GetIniValueString(m_szSrvInfoPath, "BridgeServerExist", "ValidCheck", "NO")
                    .ToUpper() == "YES";
                m_bServerValidCheck = GetIniValueString(m_szSrvInfoPath, "BridgeServerExist", "MMSValidCheck", "NO")
                    .ToUpper() == "YES";

                m_bTestServer = GetIniValueString(m_szSrvInfoPath, "ServerInfo", "TestServer", "0").ToUpper() == "1";

                m_bUseLLSMgmt = GetIniValueString(m_szSrvInfoPath, "LauncherLoginServiceMgmt", "UseLLSMgmt", "NO")
                    .ToUpper() == "YES";
                m_szLLSMgmtAddr = GetIniValueString(m_szSrvInfoPath, "LauncherLoginServiceMgmt", "LLSMgmtIP", "127.0.0.1");
                m_usLLSMgmtPort = GetIniValueUShort(m_szSrvInfoPath, "LauncherLoginServiceMgmt", "LLSMgmtPort", 7998);

                m_byUseGlobalJoin = GetIniValueByte(m_szSrvInfoPath, "MM_SERVER_FUNCTION", "Enable", 0);
                m_byAutoServerSelect = GetIniValueByte(m_szSrvInfoPath, "MM_SERVER_FUNCTION", "AutoServerSelect", 0);

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine("InitConfig - ERROR: " + e.Message + "\r\n" + e.StackTrace);
            }

            return false;
        }
    }
}