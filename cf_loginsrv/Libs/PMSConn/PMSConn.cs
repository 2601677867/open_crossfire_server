using System;
using System.Diagnostics;
using System.Globalization;
using System.Timers;
using Commons.Config;
using Commons.Log;
using Commons.Native;
using Network.Client;
using Network.Packet;
using Network.Protocol;
using Network.Server;
using Security.SafePassword;
using Timer = System.Timers.Timer;

namespace PMSConn
{
    public class CPMSConn
    {
        private const int FD_CLOSE = 10053;
        
        #region Properties
        
        private const int MB_DIV = 1024 * 1024;
        private static string CONNPATH = AppDomain.CurrentDomain.BaseDirectory + "/ServerInfo.ini";
        private static string SETTING_SECTION = "BridgeServerExist";
        private static string SETTING_KEY = "PMSConn";
        
        private readonly CBaseLogHelper m_pLogger;

        private readonly SystemInfo m_sysInfo;
        private static readonly Process sm_curProc = Process.GetCurrentProcess();
        
        private readonly PerformanceCounter m_pMemCounter = new PerformanceCounter("Process", "Working Set", sm_curProc.ProcessName);
        private readonly PerformanceCounter m_pPrivateMemCounter =
            new PerformanceCounter("Process", "Working Set - Private", sm_curProc.ProcessName);
        private readonly PerformanceCounter m_pProcessorCounter =
            new PerformanceCounter("Process", "% Processor Time", sm_curProc.ProcessName);
        
        private CGameServerClient m_pClient;
        private CServer m_pServer;
        
        private readonly string m_szAddr;
        private readonly string m_szName;
        
        private readonly Timer m_keepAliveTimer;
        private readonly Timer m_loopTimer;
        
        private readonly ushort m_usPort;

        private E_LAST_ERROR_EVENT m_eLastErrorEvent;

        internal int m_nReqNum;
        internal int m_nGsid;
        
        #endregion

        #region Constructor

        public CPMSConn(string name, string dir, int logLevel = 4, CServer server = null)
        {
            //CPMSConnNetworkHandler.SetThis(this);

            m_szName = name;
            m_szAddr = "127.0.0.1";
            m_usPort = 8990;
            
            m_pClient = new CGameServerClient(m_szAddr, m_usPort);
            m_pClient.ReceiveClientDataEvent += CPMSConnNetworkHandler.OnReceiveData;
            
            m_pServer = server;

            //m_keepAliveTimer = new Timer(5000);
            //m_keepAliveTimer.Elapsed += TKeepAlive;

            m_loopTimer = new Timer(1);
            m_loopTimer.Elapsed += TLoop;

            m_pLogger = new CBaseLogHelper(name, "PMSConn", name, dir, logLevel);
            m_pLogger.GetFormatter().AddFormatter("PMS_INFO", "PMSConn[{0}] = {1}",
                LOGLEVEL.LEVEL_FATAL, false, ConsoleColor.Gray, false);

            m_pLogger.HelperLogEvent += LogEvent;
            CPublicLogger.GetLogger().HelperLogEvent += LogEvent;
            
            if (!NativeUtil.IsUnix()) m_sysInfo = new SystemInfo();
        }

        #endregion
        
        #region Shared Methods
        
        public void SetServer(CServer server)
        {
            m_pServer = server;
        }

        private void LogEvent(string context)
        {
            SendLog(context);
        }

        internal void PrintInfo(string context, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(DateTime.Now.ToString("HH:mm:ss,fff") + "> " + "INFO" + " : " + context, args);
            Console.ResetColor();
            m_pLogger.print("PMS_INFO", context, args, DateTime.Now.ToString("dd_HH:mm:ss.fff"));
        }
        
        internal void SetLastErrorEvent(E_LAST_ERROR_EVENT _event)
        {
            m_eLastErrorEvent = _event;
        }

        private E_LAST_ERROR_EVENT GetLastErrorEvent()
        {
            return m_eLastErrorEvent;
        }

        #endregion
        
        #region Init & Connect

        public void SetIniSetting(string szFileName, string szSection, string szKey)
        {
            CONNPATH = AppDomain.CurrentDomain.BaseDirectory + "/" + szFileName;
            SETTING_SECTION = szSection;
            SETTING_KEY = szKey;
        }

        public bool Init()
        {
            E_USE_ERROR eError;
            
            try
            {
                eError = CIniFile.GetIniString(SETTING_SECTION, SETTING_KEY, CONNPATH).ToUpper() == "YES" ? 
                    E_USE_ERROR.PMS_USE_CONNECT : E_USE_ERROR.PMS_NO_CONNECT_OR_ARGUMENT;
            }
            catch
            {
                eError = E_USE_ERROR.PMS_FAIL;
            }

            switch (eError)
            {
                case E_USE_ERROR.PMS_FAIL:
                    PrintInfo("-----> PMS : Init() : FAILED  ///  ErrorCode : {0}", -1);
                    return false;
                case E_USE_ERROR.PMS_NO_CONNECT_OR_ARGUMENT:
                    PrintInfo("-----> PMS : Init() : NO Connect Or NO Argument!!!");
                    return false;
                case E_USE_ERROR.PMS_USE_ARGUMENT:
                case E_USE_ERROR.PMS_USE_CONNECT:
                    goto Connect;
                default:
                    PrintInfo("-----> PMS : Init() : FAILED");
                    return false;
            }

            Connect:
            {
                if (!TryConnect())
                {
                    m_keepAliveTimer.Start();
                    PrintInfo("-----> PMS : Init() : FAILED, ErrCode = {0}", (int)GetLastErrorEvent());
                    return false;
                }
                
                m_keepAliveTimer.Start();
                PrintInfo("-----> PMS : Init() : SUCCESS");
                return true;
            }
        }
        
        private bool TryConnect()
        {
            if (m_loopTimer.Enabled) m_loopTimer.Stop();
            
            if (!m_pClient.Connect())
            {
                SetLastErrorEvent(E_LAST_ERROR_EVENT.CONNECT_FAILED);
                PrintInfo("PMS Connect Failed ({0}, {1}), ErrCode = {2}", m_szAddr, m_usPort, (int)GetLastErrorEvent());
                return false;
            }
            
            m_nReqNum = 0;

            if (!SendConnectMsg()) return false;
            
            SetLastErrorEvent(E_LAST_ERROR_EVENT.NO_ERROR);
            
            if (!CPMSConnNetworkHandler.sm_connectResultEvent.WaitOne(5000))
            {
                CPublicLogger.GetLogger().warn("CPMSConnNetworkHandler.sm_connectResultEvent.WaitOne() timed out [5] seconds");
                m_pClient.Disconnect();
                return false;
            }
            
            PrintInfo("PMS Connected, ErrCode = {0}", GetLastErrorEvent());
            if (GetLastErrorEvent() != E_LAST_ERROR_EVENT.NO_ERROR) return false;

            if (!m_loopTimer.Enabled) m_loopTimer.Start();

            return true;
        }

        public void Stop()
        {
            m_keepAliveTimer?.Stop();
            m_loopTimer?.Stop();
            m_pClient?.StopListenPacket();
            m_pLogger?.TryTerminate();
        }
        
        #endregion
        
        #region Threads

        private void TKeepAlive(object sender, ElapsedEventArgs e)
        {
            if (!m_pClient.IsRunning())
            {
                if (GetLastErrorEvent() != E_LAST_ERROR_EVENT.CONNECT_FAILED)
                {
                    PrintInfo("PMS Disconnected, ErrCode = {0}, Event = {1}", FD_CLOSE, GetLastErrorEvent());
                }
                
                TryConnect();
            }
        }

        private void TLoop(object sender, ElapsedEventArgs e)
        {
            if ((int) m_loopTimer.Interval == 1)
            {
                m_loopTimer.Interval = 10000;
                SendLog("============================================================================");
                return;
            }

            if (!m_pClient.IsRunning())
                m_loopTimer.Stop();

            PerformPMSScheduleTasks();
        }
        
        private void PerformPMSScheduleTasks()
        {
            SendHeartbeat();
            SendPerformance();
            SendStat();
            SendRegion();
        }
        
        #endregion
        
        #region BaseNetworkSendMsg
        
        private void SendLog(string context)
        {
            var cPacket = new CPacket();
            cPacket.SetFirstClass(HA_PK.PROTOCOL_HA_FIRST_CLASS);
            cPacket.SetSecondClass(HA_PK.PROTOCOL_LOG_INFO);

            var tLogInfo = new HA_PK.PROTO_LOG_INFO
            {
                m_szContent = context
            };
            
            cPacket.CopyToUserDataArea(tLogInfo);
            
            m_pClient.SendPacket(cPacket);
        }

        private bool SendConnectMsg()
        {
            var cPacket = new CPacket();
            cPacket.SetFirstClass(HA_PK.PROTOCOL_HA_FIRST_CLASS);
            cPacket.SetSecondClass(HA_PK.PROTOCOL_GS_INIT);

            var tInit = new HA_PK.msgPMSInitNtf_Tag()
            {
                m_szSvcName = m_szName,
                m_szCryptedPassword = CSafePassword.Encrypt("GladIPGra???@719#(&@")
            };
            
            cPacket.CopyToUserDataArea(tInit);
            
            return m_pClient.SendPacket(cPacket);
        }

        private bool SendHeartbeat()
        {
            var cPacket = new CPacket();
            cPacket.SetFirstClass(HA_PK.PROTOCOL_HA_FIRST_CLASS);
            cPacket.SetSecondClass(HA_PK.PROTOCOL_GS_HEARTBEAT);
            cPacket.CopyToUserDataArea(null);

            var tHeartBeat = new HA_PK.msgPMSHeartBeatAns_Tag
            {
                m_nReqNum = m_nReqNum
            };

            cPacket.CopyToUserDataArea(tHeartBeat);
            return m_pClient.SendPacket(cPacket);
        }

        private bool SendPerformance()
        {
            var cPacket = new CPacket();
            cPacket.SetFirstClass(HA_PK.PROTOCOL_HA_FIRST_CLASS);
            cPacket.SetSecondClass(HA_PK.PROTOCOL_GS_PERFORMANCE_INFO);

            var tPerfInfo = new HA_PK.msgPMSPerformAns_Tag
            {
                m_fPrivateWorkingSetCpu = m_pProcessorCounter.NextValue() / Environment.ProcessorCount,
                m_fWorkingSetMem = m_pMemCounter.NextValue() / 1024,
                m_fPrivateWorkingSetMem = m_pPrivateMemCounter.NextValue() / 1024,
                m_fSystemCpu = m_sysInfo.CpuLoad,
                m_lSystemMem = (m_sysInfo.PhysicalMemory - m_sysInfo.MemoryAvailable) / MB_DIV
            };


            cPacket.CopyToUserDataArea(tPerfInfo);

            return m_pClient.SendPacket(cPacket);
        }

        private bool SendStat()
        {
            var cPacket = new CPacket();
            cPacket.SetFirstClass(HA_PK.PROTOCOL_HA_FIRST_CLASS);
            cPacket.SetSecondClass(HA_PK.PROTOCOL_GS_STAT_INFO);

            var tStatInfo = new HA_PK.msgPMSStatInfoAns_Tag()
            {
                m_nOnlines = m_pServer?.GetClients().Count ?? -1
            };

            cPacket.CopyToUserDataArea(tStatInfo);

            return m_pClient.SendPacket(cPacket);
        }

        private bool SendRegion()
        {
            var perfCategory = new PerformanceCounterCategory("Network Interface");
            var aInstanceNames = perfCategory.GetInstanceNames();

            foreach (var name in aInstanceNames)
            {
                var value = NetworkUtil.GetNetworkUtilization(name);
                if (value.ToString(CultureInfo.InvariantCulture) != "NaN")
                {
                    var cPacket = new CPacket();
                    cPacket.SetFirstClass(HA_PK.PROTOCOL_HA_FIRST_CLASS);
                    cPacket.SetSecondClass(HA_PK.PROTOCOL_GS_REGION_INFO_PC);

                    var tRegionInfo = new HA_PK.msgPMSRegionInfoPCAns_Tag
                    {
                        m_szNetworkInterfaceName = name, 
                        m_dNetworkPercentage = value
                    };


                    cPacket.CopyToUserDataArea(tRegionInfo);
                    
                    return m_pClient.SendPacket(cPacket);
                }
            }

            return false;
        }
        
        #endregion
    }
}