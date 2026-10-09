using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Commons.Native;

namespace Commons.Log
{
    public enum LOGLEVEL
    {
        LEVEL_OFF,
        LEVEL_FATAL,
        LEVEL_ERROR,
        LEVEL_WARN,
        LEVEL_INFO,
        LEVEL_DEBUG,
        LEVEL_TRACE,
        LEVEL_ALL
    }
    
    public class CBaseLogHelper
    {
        private readonly string m_sDirName = "";
        private readonly string m_sLogFileName = "";
        private readonly string m_sName = "";
        private readonly string m_sLogBaseDir = "";
        private readonly LOGLEVEL m_eLogLevel = LOGLEVEL.LEVEL_INFO;
        
        private int m_nCodePage;
        
        private string m_sRemoteAddr;
        private string m_sShortName;
        private string m_szHostName;
        
        private readonly AutoResetEvent m_pLogSignal = new AutoResetEvent(false);
        private readonly AutoResetEvent m_pConsoleSignal = new AutoResetEvent(false);

        private readonly Thread m_tLogThread;
        private readonly Thread m_tConsoleThread;

        private readonly CLogFormatter m_pFormatter = new CLogFormatter();
        
        public delegate void OnLogEvent(string context);
        public event OnLogEvent HelperLogEvent;

        public CLogFormatter GetFormatter()
        {
            return m_pFormatter;
        }

        private struct ELog
        {
            public string m_sType;
            public string m_sContext;
            public object[] m_gArgs;
            public DateTime m_pTime;
            public ELog(string sContext, string sType, object[] gArgs = null)
            {
                m_sContext = sContext;
                m_sType = sType;
                m_gArgs = gArgs;
                m_pTime = DateTime.Now;
            }
        }

        private struct EConsole
        {
            public string m_szType;
            public string m_szContext;
            public DateTime m_pTime;
            public EConsole(string szContext, string szType)
            {
                m_szContext = szContext;
                m_szType = szType;
                m_pTime = DateTime.Now;
            }
        }

        private readonly ConcurrentQueue<EConsole> m_pConsoleQueue = new ConcurrentQueue<EConsole>();
        private readonly ConcurrentQueue<ELog> m_pLogQueue = new ConcurrentQueue<ELog>();

        private void AddToLog(string context, string type = null, object[] args = null, params object[] objs)
        {
            if (bTerminating) return;
            
            m_pLogQueue.Enqueue(new ELog(FormatLog(context, type, args), type, objs));
            m_pLogSignal.Set();
        }

        [Conditional("DEBUG")]
        public void PrintToConsole(string context, string type = null, params object[] args)
        {
            if (bTerminating) return;
            
            m_pConsoleQueue.Enqueue(new EConsole(FormatConsole(context, args), type));
            m_pConsoleSignal.Set();
        }

        public string GetFullLogPath()
        {
            return $"{m_sLogBaseDir}\\{m_sDirName}\\{DateTime.Now:yyyyMMdd}";
        }

        public string GetBaseLogPath()
        {
            return m_sLogBaseDir;
        }

        public LOGLEVEL GetLogLevel()
        {
            return m_eLogLevel;
        }

        public string GetFileName()
        {
            return m_sLogFileName;
        }

        public string GetLogDir()
        {
            return m_sDirName;
        }

        public string GetName()
        {
            return m_sName;
        }
        
        public string GetShortName()
        {
            return m_sShortName;
        }

        public CBaseLogHelper(string sName, string sDirName, string sFileName, string sLogBaseDir, int nLogLevel)
        {
            try
            {
                m_sName = sName;
                m_sDirName = sDirName;
                m_sLogFileName = sFileName;
                m_sLogBaseDir = sLogBaseDir;
                m_eLogLevel = (LOGLEVEL)nLogLevel;

                m_nCodePage = NativeUtil.IsUnix() ? Encoding.UTF8.CodePage : Global.Encoding.CodePage;
                
#if DEBUG
                Console.OutputEncoding = Encoding.GetEncoding(m_nCodePage);
#endif

                m_pFormatter.AddFormatter("SettingCheck", "{0},{1},{2},warn,{4}", LOGLEVEL.LEVEL_FATAL, false, ConsoleColor.DarkYellow);

                m_pFormatter.AddFormatter("FATAL", "{0},{1},{2},{3},FATAL,{4}", LOGLEVEL.LEVEL_FATAL, false, ConsoleColor.DarkRed);
                m_pFormatter.AddFormatter("error", "{0},{1},{2},{3},error,{4}", LOGLEVEL.LEVEL_ERROR, false, ConsoleColor.Red);
                m_pFormatter.AddFormatter("warn", "{0},{1},{2},{3},warn,{4}", LOGLEVEL.LEVEL_WARN, false, ConsoleColor.Yellow);
                m_pFormatter.AddFormatter("info", "{0}> {1}", LOGLEVEL.LEVEL_INFO, false, ConsoleColor.Green);
                m_pFormatter.AddFormatter("etcinfo", "{0},{1},{2},info,{4}", LOGLEVEL.LEVEL_INFO, false, ConsoleColor.Green);
                m_pFormatter.AddFormatter("PACKET_PROFILE", "{0}\t{1}\t{2}\t{3}\t{4}\t{5}\t{6}\t{7}\t{8}\t{9}\t{10}\t{11}", LOGLEVEL.LEVEL_INFO, false, ConsoleColor.Green, false, "{0}/{1}/{2}/1_PACKET_PROFILE_{6}.log");

                m_pFormatter.AddFormatter("debug", "{0},{1},{2},{3},debug,{4}", LOGLEVEL.LEVEL_DEBUG, false, ConsoleColor.Blue);
                m_pFormatter.AddFormatter("trace", "{0},{1},{2},{3},trace,{4}", LOGLEVEL.LEVEL_TRACE, false, ConsoleColor.Cyan);
                m_pFormatter.AddFormatter("packet", "{2}\t{4}", LOGLEVEL.LEVEL_ALL, false, ConsoleColor.Cyan);
                m_pFormatter.AddFormatter("timeinfo", "{0},{1},{2},info,{3}", LOGLEVEL.LEVEL_FATAL, false, ConsoleColor.Yellow);

                m_pFormatter.AddFormatter("1_ERROR", "{0}\t{1}\t{2}\t{3}", LOGLEVEL.LEVEL_FATAL, false, ConsoleColor.Gray, false, "{0}/{1}/{2}/1_ERROR_{6}.log");
                
#if DEBUG
                m_tConsoleThread = new Thread(TConsole);
#endif
                m_tLogThread = new Thread(TLog);

                m_szHostName = Dns.GetHostName();

                Start();
            }
            catch (Exception e)
            {
                PrintToConsole("ERROR" + e.Message + "\r\n" + e.StackTrace);
            }
        }

        public void SetEncoding(int nNewCodePage)
        {
            m_nCodePage = nNewCodePage;
            Console.OutputEncoding = Encoding.GetEncoding(m_nCodePage);
        }
        
        public void SetPacketInfoSetting(string sAddr, string sShortName)
        {
            m_sRemoteAddr = sAddr;
            m_sShortName = sShortName;
        }

        public string GetHostName()
        {
            return m_szHostName;
        }

        private void Start()
        {
            m_tLogThread.Start();
#if DEBUG
            m_tConsoleThread.Start();
#endif
        }

        private void Stop()
        {
            // Set signal so that the loop comes back and check terminating flag
            m_pLogSignal.Set();
            m_pConsoleSignal.Set();
        }

        private static string csvFormatStr(string s)
        {
            if (s.IndexOf(',') == -1)
            {
                return s;
            }

            return '"' + s + '"';
        }

        private static string FormatConsole(string context, params object[] args)
        {
            if (args != null && args.Length > 0)
                return string.Format(context, args);
            return context;
        }

        private string FormatLog(string context, string type, params object[] args)
        {
            var csv = m_pFormatter.GetCSV(type);
            context = csv ? csvFormatStr(FormatConsole(context, args)) : FormatConsole(context, args);
            return context;
        }
        
        private void PrintAndLog(string szType, string szContext, params object[] aArgs)
        {
            if (bTerminating) return;

            PrintToConsole(szContext, szType, aArgs);
            AddToLog(szContext, szType, aArgs);
        }

        public void SettingCheck(string szContext, params object[] aArgs) { PrintAndLog("SettingCheck", szContext, aArgs); }
        
        [Conditional("RELEASE")]
        public void service(string szContext, params object[] aArgs) { PrintAndLog("error", szContext, aArgs); }

        public void fatal(string szContext, params object[] aArgs)
        {
            WaitUntilFlushed();
            PrintAndLog("FATAL", szContext, aArgs);
        }
        
        public void error(string szContext, params object[] aArgs) { PrintAndLog("error", szContext, aArgs); }

        public void error(Exception ex) { PrintAndLog("error", ex.Message + "\r\n" + ex.StackTrace); }
        
        public void warn(string szContext, params object[] aArgs) { PrintAndLog("warn", szContext, aArgs); }

        public void info(string szContext, params object[] aArgs)
        {
            if (bTerminating) return;
            PrintToConsole(szContext, "info", aArgs);
            print("info", szContext, aArgs, DateTime.Now.ToString("HH:mm:ss.fff"));
        }

        public void etcinfo(string szContext, params object[] aArgs) { PrintAndLog("etcinfo", szContext, aArgs); }

        public void debug(string szContext, params object[] aArgs) { PrintAndLog("debug", szContext, aArgs); }
        
        public void trace(string szContext, params object[] aArgs) { PrintAndLog("trace", szContext, aArgs); }

        public void stackTrace(string szErrorMsg) { PrintAndLog("error", "{0}\r\n{1}", szErrorMsg, new StackTrace().ToString()); }

        public void packet(string szContext, params object[] aArgs)
        {
            PrintToConsole(szContext, "packet", aArgs);
        }

        public void packet_info(string name, string ip, int port, int handle, double recv_time, 
            double process_time, byte First, byte Second, params object[] args)
        {
            if (string.IsNullOrEmpty(name)) return;
            print("PACKET_PROFILE", 
                process_time.ToString("F6"), args, m_sRemoteAddr, m_sShortName,
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,fff"), 
                ip, port.ToString(), handle.ToString(), First.ToString(), name, Second.ToString(), 
                (recv_time + process_time).ToString("F6"), recv_time.ToString("F6"));
        }

        public void print(string type, string context, object[] args, params object[] objs)
        {
            if (bTerminating) return;
            
            //console(context, type, args);
            AddToLog(context, type, args, objs);
        }

        private bool bTerminating;

        private void WaitUntilFlushed()
        {
            while (m_pLogQueue.Count > 0)
            {
                Thread.Sleep(100);
            }
        }
        
        public void TryTerminate()
        {
            bTerminating = true;
            WaitUntilFlushed();
            Stop();
        }

        private void TConsole()
        {
            while (!bTerminating)
            {
                m_pConsoleSignal.WaitOne();
                while (m_pConsoleQueue.Count > 0 && m_pConsoleQueue.TryDequeue(out var tConsoleObj))
                {
                    if (tConsoleObj.m_szType != null)
                    {
                        var szFormattedMsg = tConsoleObj.m_pTime.ToString("HH:mm:ss,fff") + "> " + tConsoleObj.m_szType +
                                             " : " + tConsoleObj.m_szContext;

                        HelperLogEvent?.Invoke(szFormattedMsg);

                        var eLogLevel = m_pFormatter.GetLevel(tConsoleObj.m_szType);
                        if (m_eLogLevel < eLogLevel) continue;
                        var pColor = m_pFormatter.GetColor(tConsoleObj.m_szType);
                        Console.ForegroundColor = pColor;
                        Console.WriteLine(szFormattedMsg);
                        Console.ResetColor();
                    }
                    else
                    {
                        HelperLogEvent?.Invoke(tConsoleObj.m_szContext);

                        Console.WriteLine(tConsoleObj.m_szContext);
                        Console.ResetColor();
                    }
                }
            }
        }

        private void TLog()
        {
            while (!bTerminating)
            {
                m_pLogSignal.WaitOne();
                lock (m_pLogQueue)
                {
                    while (m_pLogQueue.Count > 0 && m_pLogQueue.TryDequeue(out var log))
                    {
                        if (!m_pFormatter.Exists(log.m_sType)) continue;
                        var eLogLevel = m_pFormatter.GetLevel(log.m_sType);
                        if (m_eLogLevel < eLogLevel) continue;
                        var szFormatter = m_pFormatter.GetFormatter(log.m_sType);
                        var nTimeDiv = m_pFormatter.GetTimeDiv(log.m_sType);

                        var pFileFormatter = m_pFormatter.GetFileFormatter(log.m_sType);
                        var szFileName = string.Format(pFileFormatter, m_sLogBaseDir, m_sDirName, 
                            DateTime.Now.ToString("yyyyMMdd"), log.m_sType, m_sLogFileName, 
                            m_szHostName, DateTime.Now.ToString("yyyyMMdd"), 
                            (DateTime.Now.Hour / nTimeDiv).ToString("00"));

                        string szToPrint;

                        try
                        {
                            if (log.m_gArgs.Length > 0)
                            {
                                var aToArgs = new object[log.m_gArgs.Length + 1];
                                for (var i = 0; i < log.m_gArgs.Length; i++) aToArgs[i] = (string) log.m_gArgs[i];
                                aToArgs[aToArgs.Length - 1] = log.m_sContext;
                                szToPrint = string.Format(szFormatter, aToArgs);
                            }
                            else
                                szToPrint = string.Format(szFormatter, m_szHostName, m_sName,
                                    log.m_pTime.ToString("yyyy-MM-dd HH:mm:ss,fff"),
                                    Thread.CurrentThread.ManagedThreadId, log.m_sContext);
                        }
                        catch (Exception)
                        {
                            szToPrint = "FORMAT ERROR";
                        }

                        if (!Directory.Exists($"{m_sLogBaseDir}/{m_sDirName}/{DateTime.Now:yyyyMMdd}"))
                        {
                            Directory.CreateDirectory($"{m_sLogBaseDir}/{m_sDirName}/{DateTime.Now:yyyyMMdd}");
                        }

                        var aDataToWrite = Encoding.GetEncoding(m_nCodePage).GetBytes(szToPrint + "\r\n");

                        try
                        {
                            var fsLogFile = new FileStream(szFileName, FileMode.Append, FileAccess.Write);
                            fsLogFile.Write(aDataToWrite, 0, aDataToWrite.Length);
                            fsLogFile.Flush();
                            fsLogFile.Close();
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                    }
                }
            }
        }
    }
}
