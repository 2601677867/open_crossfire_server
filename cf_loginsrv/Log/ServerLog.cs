using System;
using System.Diagnostics;
using System.IO;
using cf_loginsrv.Config;
using cf_loginsrv.Socket;
using Commons.Log;
using Network.Packet;

namespace cf_loginsrv.Log
{
    internal static class CServerLog
    {
        public const string LOG_SHORT_NAME = "LG";
        
        private static CBaseLogHelper sm_pLogger;

        public static void Init(string sName, string sDirName, string sFileName, string sLogBaseDir, int nLogLevel)
        {
            sm_pLogger = new CBaseLogHelper(sName, sDirName, sFileName, sLogBaseDir, nLogLevel);
            
            if (!Directory.Exists($"{sm_pLogger.GetBaseLogPath()}/crossfire/SvrState"))
            {
                Directory.CreateDirectory($"{sm_pLogger.GetBaseLogPath()}/crossfire/SvrState");
            }
            
            sm_pLogger.GetFormatter().AddFormatter("CONTENTS_FAME", 
                "{0} {1}", LOGLEVEL.LEVEL_FATAL, false, ConsoleColor.DarkYellow, false, "{0}/{1}/{2}/CONTENTS_FAME_{6}_{7}.log");
            
            sm_pLogger.GetFormatter().AddFormatter("SVRSTATE_TIMEINFO", 
                "TIME_{0}_{1};\t{2};\t{3};\t{4};\t{5};\t{6};\t{7};\t{8};\t{9};\t{10};\t{11}", 
                LOGLEVEL.LEVEL_FATAL, false, ConsoleColor.Green, false,
                "{0}/crossfire/SvrState/Profile_TIME_{5}_" + LOG_SHORT_NAME + "_{6}.log");
            
            sm_pLogger.GetFormatter().AddFormatter("SVRSTATE_IOINFO", 
                "IO_{0}_{1};\t{2};\t{3}", 
                LOGLEVEL.LEVEL_FATAL, false, ConsoleColor.Green, false,
                "{0}/crossfire/SvrState/Profile_IO_{5}_" + LOG_SHORT_NAME + "_{6}.log");
        }

        public static CBaseLogHelper GetLogger()
        {
            return sm_pLogger;
        }
        
        public static void LogFameSystem(string context, params object[] args)
        {
            sm_pLogger.PrintToConsole(context, "CONTENTS_FAME", args);
            sm_pLogger.print("CONTENTS_FAME", context, args, DateTime.Now.ToString("HH:mm:ss.fff"));
        }
        
        public static void PacketProfile(string szProtoName, CLGUserContext cContext,
            double dRecvTime, Stopwatch swProcessTime, CPacket cPacket)
        {
            sm_pLogger.packet_info(szProtoName, 
                cContext.IPAddress.ToString(),
                cContext.RemotePort, 
                (int) cContext.Socket.Handle, 
                dRecvTime, 
                swProcessTime?.Elapsed.TotalSeconds ?? 0, cPacket.GetFirstClass(), cPacket.GetSecondClass());
        }
        
        public static void PrintPrimaryError(string context, params object[] args)
        {
            sm_pLogger.PrintToConsole(context, "1_ERROR", args);
            sm_pLogger.print("1_ERROR", context, args, CServerConfig.GetServerRemoteAddr(), LOG_SHORT_NAME, 
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"));
        }
        
        public static void PrintSvrState(string name, string ip, int port, int handle, double recv_time, double process_time, byte First, byte Second, params object[] args)
        {
            sm_pLogger.print("SVRSTATE_TIMEINFO", process_time.ToString("F6"), args, 
                sm_pLogger.GetHostName(), LOG_SHORT_NAME, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"),
                ip, port.ToString(), handle.ToString(), First.ToString(), name, Second.ToString(),
                (recv_time + process_time).ToString("F6"), recv_time.ToString("F6"));
        }
        
        public static void PrintSvrIoState(string context)
        {
            sm_pLogger.print("SVRSTATE_IOINFO", context, new object[] {}, 
                sm_pLogger.GetHostName(), LOG_SHORT_NAME, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"));
        }
    }
}