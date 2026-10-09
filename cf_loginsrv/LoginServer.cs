using System;
using System.Diagnostics;
using System.Reflection;
using System.ServiceProcess;
using cf_loginsrv.Config;
using cf_loginsrv.Log;
using cf_loginsrv.ServerStruct;
using cf_loginsrv.Util;
using Commons.Log;
using Commons.Version;
using DBGWMGR;
using PMSConn;
using Security.SafeCall;

using static Network.Protocol.P_SZ;

namespace cf_loginsrv
{
    public class LoginServer : ServiceBase
    {
        private readonly string BUILD_TIME =
            CBuildTimeConverter.Convert(Assembly.GetExecutingAssembly().GetName().Version);

        private CSafeCall m_cSafeCall;
        private CPMSConn m_cPmsConn;

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            var assemblyName = new AssemblyName(args.Name);
            var ass = assemblyName.ToString().Substring(0, assemblyName.ToString().IndexOf(','));
            var dir = Environment.GetEnvironmentVariable("IPGRA_LIB_DIR");
            return Assembly.LoadFrom(dir + "\\" + ass + ".dll");
        }

        private bool Initial()
        {
            if (!CServerConfig.GetInstance().InitConfig()) return false;
            
            CServerLog.Init("cf_loginsrv", "crossfire/cf_loginsrv", "cf_loginsrv", 
                CServerConfig.GetBaseLogPath(), CServerConfig.GetLogLevel());
            CPublicLogger.SetLogger(CServerLog.GetLogger());

            Console.WriteLine("============================================================================");

            CServerConfig.GetInstance().FinishInit();
            CServerConfig.GetInstance().CheckSetting();

            CServerLog.GetLogger().etcinfo("STARTING LOGIN SERVER, BUILD DATE: {0}", BUILD_TIME);
            CServerLog.GetLogger().info("STARTING LOGIN SERVER, BUILD DATE: {0}", BUILD_TIME);

            CServerLog.GetLogger().info(
                "MGMT SERVER PORT : {0} / LOGIN SERVER PORT : {1}", 
                CServerConfig.GetMgmtRemotePort(), CServerConfig.GetServerRemotePort());

#if DEBUG
            try
            {
                var nValidCheckResult = CDBGWManager.FileValidCheck();
                if (nValidCheckResult != 0)
                    CServerLog.GetLogger().warn(
                        "DEBUG MODE: CDBGWManager::FileValidCheck() returned Q{0}. Continuing.", nValidCheckResult);
            }
            catch (Exception e)
            {
                CServerLog.GetLogger().warn(
                    "DEBUG MODE: CDBGWManager::FileValidCheck() exception - {0}. Continuing without DB.", e.Message);
            }
#else
            var nValidCheckResult = CDBGWManager.FileValidCheck();
            if (nValidCheckResult != 0)
            {
                CServerLog.GetLogger().error("CDBGWManager::FileValidCheck() Failed, Q{0}", nValidCheckResult);
                return false;
            }
#endif

#if DEBUG
            // DEBUG: allow the login server to start without GDBGW so it can be tested offline.
            var bDbReady = false;
            try
            {
                bDbReady = CServerDataManager.InitDatabaseManager(CServerLog.LOG_SHORT_NAME,
                    CServerConfig.GetServerRemoteAddr());
            }
            catch (Exception e)
            {
                CServerLog.GetLogger().warn("DEBUG MODE: GDBGW init exception - {0}", e.Message);
            }

            if (!bDbReady)
                CServerLog.GetLogger().warn(
                    "DEBUG MODE: Failed to connect GDBGW. Continuing WITHOUT database (admin/admin login only).");
#else
            if (!CServerDataManager.InitDatabaseManager(CServerLog.LOG_SHORT_NAME, CServerConfig.GetServerRemoteAddr()))
                return false;
#endif

            m_cPmsConn = new CPMSConn("cf_loginsrv", CServerConfig.GetBaseLogPath(), CServerConfig.GetLogLevel());
            if (!m_cPmsConn.Init())
                CServerLog.GetLogger().info("PMSConn - NO Connect Or NO Argument!!!");

            if (!CServerConfig.GetClientValidCheck())
                CServerLog.GetLogger().info("NO Valid Check Client's Login Hashed Value!!!!");
            if (!CServerConfig.GetServerValidCheck())
                CServerLog.GetLogger().info("NO Valid Check Client's Server Hashed Value!!!!");

            if (CServerConfig.GetUseLLSMgmt())
                CServerLog.GetLogger().info("Using Launcher Login Service");
            else
                CServerLog.GetLogger().warn("NOT USING Launcher Login Service!!");

            if (CSharedMethod.LoadDefaultCharacterData() != 1)
            {
                CServerLog.GetLogger().info("Failed Load Default CharacterData");
#if !DEBUG
                return true;
#endif
            }
            
            CMainServer.m_aGameServers = new GAMESERVER[MAX_GAMESERVER_COUNT];
            for (var i = 0; i < MAX_GAMESERVER_COUNT; i++)
            {
                CMainServer.m_aGameServers[i].m_nServerID = -1;
                CMainServer.m_aGameServers[i].m_nServerHighProperty = 1;
            }
            
            CServerLog.GetLogger().info("...Waiting for first update of server default data");
            //if (!CMainServer.RefreshServerList())
            //{
            //    CServerLog.GetLogger().info("FAIL_LoadServerDefaultData");
            //    return false;
            //}
            CServerLog.GetLogger().info("SUCCESS_LoadServerDefaultData");

            CServerLog.GetLogger().SetPacketInfoSetting(CServerConfig.GetServerRemoteAddr(), CServerLog.LOG_SHORT_NAME);

            if (!CMainServer.RunNetworkManager())
            {
                CServerLog.GetLogger().fatal("Failed to starting-up server");
                return false;
            }
            
            CServerLog.GetLogger().info("Succeed to starting-up server");
            CServerLog.GetLogger().info($"LoginServer started [{CServerConfig.GetServerRemoteAddr()}]");

#if DEBUG
            try
            {
                CPerformanceChecker.Init(CServerLog.LOG_SHORT_NAME);
                CPerformanceChecker.Start();
            }
            catch (Exception e)
            {
                CServerLog.GetLogger().warn("DEBUG MODE: CPerformanceChecker init failed - {0}. Continuing.", e.Message);
            }
#else
            CPerformanceChecker.Init(CServerLog.LOG_SHORT_NAME);
            CPerformanceChecker.Start();
#endif

            CServerLog.GetLogger().service("Service started");
            CServerLog.GetLogger().PrintToConsole("Program started");

            return true;
        }

        protected override void OnStart(string[] args)
        {
            m_cSafeCall = new CSafeCall(true);
            if (!m_cSafeCall.TryCall(Initial)) Terminate();
        }

        private void CleanUp()
        {
            m_cPmsConn?.Stop();
            CMainServer.CleanUp();
            CServerDataManager.Terminate();
            CPerformanceChecker.Stop();
            CServerLog.GetLogger()?.TryTerminate();
        }

        private void Terminate()
        {
            Console.WriteLine();
            Console.WriteLine("!!! cf_loginsrv Initial() FAILED - the server cannot start. !!!");
            Console.WriteLine("!!! Check the log folder configured by ServerLogPath for details. !!!");
            Console.Write("Press Enter to exit...");
            try { Console.ReadLine(); } catch { }
            CleanUp();
            StopService();
            StopProgram();
        }

        [Conditional("DEBUG")]
        private static void StopProgram()
        {
            Environment.Exit(0);
        }

        [Conditional("RELEASE")]
        private void StopService()
        {
            Stop();
        }

        protected override void OnStop()
        {
            CServerLog.GetLogger().info("SERVICE STOPPED");
            CServerLog.PrintPrimaryError("SERVICE STOP LOGIN SERVER");
            CleanUp();
        }

        public void Start(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += CSafeCall.UnhandledException;
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            OnStart(args);
        }
    }
}