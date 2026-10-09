using System;
using System.Diagnostics;
using cf_loginsrv.Socket;
using Commons.Log;
using Network.Packet;

namespace cf_loginsrv.Log
{
    internal static class CServerLog
    {
        public const string LOG_SHORT_NAME = "LG";
        
        private static CBaseLogHelper sm_pLogger;

        public static void Init(string sName, string sDirName, string sLogBaseDir, int nLogLevel)
        {
            sm_pLogger = new CBaseLogHelper(sName, sDirName, sLogBaseDir, nLogLevel);

            var pFormatter = sm_pLogger.GetFormatter();
            pFormatter.AddChannel("CONTENTS_FAME", "SYSTEM", LOGLEVEL.LEVEL_INFO, ConsoleColor.DarkGreen);
            pFormatter.AddChannel("SVRSTATE_TIMEINFO", "PROFILE", LOGLEVEL.LEVEL_ALL, ConsoleColor.DarkGray);
            pFormatter.AddChannel("SVRSTATE_IOINFO", "PROFILE", LOGLEVEL.LEVEL_ALL, ConsoleColor.DarkGray);
        }

        public static CBaseLogHelper GetLogger()
        {
            return sm_pLogger;
        }
        
        public static void LogFameSystem(string context, params object[] args)
        {
            sm_pLogger.PrintToConsole(context, "CONTENTS_FAME", args);
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
        }
        
        public static void PrintSvrState(string name, string ip, int port, int handle, double recv_time, double process_time, byte First, byte Second, params object[] args)
        {
            sm_pLogger.PrintToConsole("{0} {1}:{2} h={3} cls={4}/{5} recv={6:F6}s proc={7:F6}s total={8:F6}s",
                "SVRSTATE_TIMEINFO", name, ip, port, handle, First, Second, recv_time, process_time,
                recv_time + process_time);
        }
        
        public static void PrintSvrIoState(string context)
        {
            sm_pLogger.PrintToConsole(context, "SVRSTATE_IOINFO");
        }
    }
}
