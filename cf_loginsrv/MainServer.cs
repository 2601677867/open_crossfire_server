using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using cf_loginsrv.Config;
using cf_loginsrv.Log;
using cf_loginsrv.Packet;
using cf_loginsrv.ServerStruct;
using cf_loginsrv.Socket;
using cf_loginsrv.Util;
using Commons;
using Commons.Native;
using DBGWMGR;
using Network.Packet;
using Network.Protocol;
using Timer = System.Timers.Timer;

using static Network.Protocol.P_SZ;

namespace cf_loginsrv
{
    public static class CMainServer
    {
        public static GAMESERVER[] m_aGameServers;
        public static readonly Dictionary<int, LMS_PK.PROTO_AUTO_EVENT_START> m_dicEventInfos = 
            new Dictionary<int, LMS_PK.PROTO_AUTO_EVENT_START>();

        private static Timer m_aTimer;
        private static int m_dwTimerCount;

        private static readonly CGameNetworkHandler m_pGameNetworkHandler = new CGameNetworkHandler();
        private static readonly CMgmtNetworkHandler m_pMgmtNetworkHandler = new CMgmtNetworkHandler();
        private static readonly CLLSMgmtNetworkHandler m_pLLSMgmtNetworkHandler = new CLLSMgmtNetworkHandler();
        
        private static CLLSMgmtClient m_pLLSMgmtClient;

#if DEBUG
        // DEBUG-only fake game server, so the client sees a server list even without
        // GDBGW/SQL. Name is fixed to "TEST"; there is exactly one such entry.
        public const string DEBUG_TEST_SERVER_NAME = "测试服务器";
        // ID 0 is treated as "no server" by some client paths - use 1 instead.
        private const short DEBUG_TEST_SERVER_ID = 1;
        private const int DEBUG_TEST_SERVER_PORT = 10009; // cf_gamesrv default game port
        private const string DEBUG_TEST_SERVER_IP = "127.0.0.1";
#endif

        public static CGameNetworkHandler GetGameNetworkHandler()
        {
            return m_pGameNetworkHandler;
        }

        public static CMgmtNetworkHandler GetMgmtNetworkHandler()
        {
            return m_pMgmtNetworkHandler;
        }
        
        public static CLLSMgmtClient GetLLSMgmtClient()
        {
            return m_pLLSMgmtClient;
        }

        private static void InitTimers()
        {
            m_aTimer = new Timer {Interval = 1000};
            m_aTimer.Elapsed += OnTimer;
        }
        
        public static void CleanUp()
        {
            m_aTimer?.Stop();
        }

        private static void StartTimers()
        {
            m_dwTimerCount = 0;
            m_aTimer.Enabled = true;
        }

        private static void OnTimer(object source, ElapsedEventArgs e)
        {
            var dwTimerCount = m_dwTimerCount;
            Interlocked.Increment(ref m_dwTimerCount);
            
            if (dwTimerCount % 10 == 0)
            {
                DoLMSRefresh();
                CSocketController.GetLoginServer().TimeOutCheck();
                CSocketController.GetLoginServer().PushServerMessage(
                    SV_PK.PROTOCOL_LOGIN_SERVER_AUTO_EVENT_PULSE, 0);

                if (CServerConfig.GetUseLLSMgmt())
                {
                    if (!m_pLLSMgmtClient.IsConnected())
                    {
                        var bConnected = m_pLLSMgmtClient.Connect();
                        if (bConnected)
                        {
                            CServerLog.GetLogger().info("Connected to LLSMgmt");
                        }
                    }
                    else
                    {
                        m_pLLSMgmtClient.Heartbeat();
                    }
                }
            }

            if (dwTimerCount % 60 == 0)
            {
                FameSystem.GetInstance().UpdateFameGrade();
                CUpdateDomainInfo.GetInstance().DoUpdate();
            }
        }
        
        private static void DoLMSRefresh()
        {
            lock ( m_aGameServers )
            {
                try
                {
                    RefreshServerList();
                } 
                catch (Exception e)
                {
                    CServerLog.GetLogger().error($"RefreshServerList Failed : {e.Message}\r\n{e.StackTrace}");
                }
            }
        }
        
        public static bool RefreshServerList()
        {
            var bSuccess = CServerDataManager.ExecuteGameQuery(
                E_GDBGW_DEFS.QUERY_SELECT_SERVER_LIST, out var cParser);
            if (!bSuccess)
            {
                CServerLog.GetLogger().error(
                    "[ExecuteQuery Failed][{0}]",
                    CServerDataManager.GetQueryString(E_GDBGW_DEFS.QUERY_SELECT_SERVER_LIST));
#if DEBUG
                EnsureDebugServerList();
#endif
                return false;
            }

            if (cParser.RowCount == 0)
            {
#if DEBUG
                CServerLog.GetLogger().warn("DEBUG MODE: Server list query returned 0 rows, injecting debug server");
                EnsureDebugServerList();
#endif
                return true;
            }

            while (cParser.Peek())
            {
                var nServerID = cParser.GetShort(1);
                m_aGameServers[nServerID].m_nServerID = nServerID;
                NativeUtil.strncpy(ref m_aGameServers[nServerID].m_szServerName,
                    cParser.GetString(2), MAX_SERVER_DISPLAY_NAME);
                m_aGameServers[nServerID].m_nServerConnectCount = cParser.GetInt(4);
                m_aGameServers[nServerID].m_nServerLimitCount = cParser.GetInt(5);
                if ( m_aGameServers[nServerID].m_nServerConnectCount >
                     m_aGameServers[nServerID].m_nServerLimitCount)
                {
                    m_aGameServers[nServerID].m_nServerConnectCount = 
                        m_aGameServers[nServerID].m_nServerLimitCount;
                }
                    
                var szAddress = cParser.GetString(6);
                    
                if (szAddress[szAddress.Length - 1] >= '0' && szAddress[szAddress.Length - 1] <= '9')
                {
                    m_aGameServers[nServerID].m_dwServerAddr = NativeUtil.inet_addr(szAddress);
                }
                else
                {
                    m_aGameServers[nServerID].m_dwServerAddr = 
                        NativeUtil.inet_addr(CUpdateDomainInfo.GetInstance().GetIPAddress(szAddress));
                }

                m_aGameServers[nServerID].m_nServerPort = cParser.GetInt(7);
                m_aGameServers[nServerID].m_bEvent = cParser.GetString(8) == "1";
                m_aGameServers[nServerID].m_dwInternalAddr = NativeUtil.inet_addr(cParser.GetString(9));
            }

            return true;
        }

#if DEBUG
        // Injects the DEBUG fake game server into the server list (name/addr/port).
        // Only used in DEBUG builds when no real list could be loaded from GDBGW.
        public static void EnsureDebugServerList()
        {
            if (m_aGameServers == null) return;

            lock (m_aGameServers)
            {
                for (var i = 0; i < MAX_GAMESERVER_COUNT; i++)
                {
                    m_aGameServers[i].m_nServerID = -1;
                    m_aGameServers[i].m_nServerHighProperty = 1;
                }

                var nID = DEBUG_TEST_SERVER_ID;
                m_aGameServers[nID].m_nServerID = nID;
                m_aGameServers[nID].m_nServerHighProperty = (short)Network.SharedFolder.SERVERHIGHPROPERTY.NORMAL_SERVER;
                m_aGameServers[nID].m_nServerLowProperty = 0;
                m_aGameServers[nID].m_nServerHighLimit = 100;
                m_aGameServers[nID].m_nServerLowLimit = 0;
                m_aGameServers[nID].m_dServerHighKD = 0.0;
                m_aGameServers[nID].m_dServerLowKD = 0.0;
                m_aGameServers[nID].m_szServerName = new byte[MAX_SERVER_DISPLAY_NAME];
                NativeUtil.strncpy(ref m_aGameServers[nID].m_szServerName,
                    DEBUG_TEST_SERVER_NAME, MAX_SERVER_DISPLAY_NAME);
                m_aGameServers[nID].m_nServerPort = DEBUG_TEST_SERVER_PORT;
                m_aGameServers[nID].m_dwServerAddr = NativeUtil.inet_addr(DEBUG_TEST_SERVER_IP);
                m_aGameServers[nID].m_dwInternalAddr = NativeUtil.inet_addr(DEBUG_TEST_SERVER_IP);
                m_aGameServers[nID].m_nServerLimitCount = 100;
                m_aGameServers[nID].m_nServerConnectCount = 0;
                m_aGameServers[nID].m_nPassword = 0;
                m_aGameServers[nID].m_bGameServerConnected = true;
                m_aGameServers[nID].m_bEvent = false;
            }
        }
#endif
        
        public static bool RefreshAutoEventList()
        {
            var bSuccess = CServerDataManager.ExecuteGameQuery(
                E_GDBGW_DEFS.QUERY_SELECT_SERVER_EVENT_INFO, out var cParser);
            
            if (!bSuccess) return false;
            if (cParser.RowCount == 0) return false;
            
            lock (m_dicEventInfos)
            {
                m_dicEventInfos.Clear();
                while (cParser.Peek())
                {
                    var tEventInfo = new LMS_PK.PROTO_AUTO_EVENT_START();
                    tEventInfo.iExpPercentage = cParser.GetInt(1);
                    tEventInfo.iGPPercentage = cParser.GetInt(2);
                    tEventInfo.bDeathReset = cParser.GetInt(3) == 1;
                    NativeUtil.strncpy(ref tEventInfo.aszMemo, cParser.GetString(4), MAX_AUTO_EVENT_MEMO_LENGTH);
                    tEventInfo.iServerNo = cParser.GetInt(5);
                    tEventInfo.iMapID = cParser.GetInt(6);
                    tEventInfo.iSubMapID = cParser.GetInt(7);
                    
                    m_dicEventInfos[tEventInfo.iServerNo] = tEventInfo;
                }
            }

            return true;
        }

        public static bool RunNetworkManager()
        {
            m_pLLSMgmtClient = new CLLSMgmtClient(
                CServerConfig.GetLLSMgmtAddr(),
                CServerConfig.GetLLSMgmtPort(), 
                m_pLLSMgmtNetworkHandler.OnNetworkMsg);
            
            var cMgmtSocketMgr = new CNetworkMgr(MAX_GAMESERVER_COUNT + 1, 
                CServerConfig.GetBufferSize(), CServerConfig.GetServerRemoteAddr(), CServerConfig.GetMgmtRemotePort());
            var cLoginSocketMgr = new CNetworkMgr(CServerConfig.GetServerMaxUser(),
                CServerConfig.GetBufferSize(), CServerConfig.GetServerRemoteAddr(), CServerConfig.GetServerRemotePort());
            if (!cMgmtSocketMgr.Listen() || !cLoginSocketMgr.Listen()) return false;
            
            CSocketController.SetMgmtServer(cMgmtSocketMgr);
            CSocketController.SetLoginServer(cLoginSocketMgr);
#if DEBUG
            EnsureDebugServerList();
#endif
            InitTimers();
            StartTimers();
            CServerLog.GetLogger().info("[CMainServer::RunNetworkManager] RunNetworkManager OK");
                
            return true;
        }
    }
}