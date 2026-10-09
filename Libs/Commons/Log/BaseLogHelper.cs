using System;
using System.Collections.Concurrent;
using System.Diagnostics;
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

    /// <summary>
    ///     Console logger. Entries are queued and written by one worker thread as
    ///     "HH:mm:ss.fff 【TAG】 message", colored per channel; nothing goes to disk.
    /// </summary>
    public class CBaseLogHelper
    {
        private readonly string m_sDirName = "";
        private readonly string m_sName = "";
        private readonly string m_sLogBaseDir = "";
        private readonly LOGLEVEL m_eLogLevel = LOGLEVEL.LEVEL_INFO;

        private int m_nCodePage;

        private string m_szHostName;

        private readonly AutoResetEvent m_pConsoleSignal = new AutoResetEvent(false);

        private readonly Thread m_tConsoleThread;

        private readonly CLogFormatter m_pFormatter = new CLogFormatter();

        public delegate void OnLogEvent(string context);
        public event OnLogEvent HelperLogEvent;

        public CLogFormatter GetFormatter()
        {
            return m_pFormatter;
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

        // Shared by every logger instance (PMSConn creates a second one): without it two worker
        // threads interleave their prefix writes and the lines come out garbled.
        private static readonly object sm_pConsoleLock = new object();

        /// <param name="sFileName">Ignored, kept for pre-built DLLs that still pass it.</param>
        public CBaseLogHelper(string sName, string sDirName, string sFileName, string sLogBaseDir, int nLogLevel)
            : this(sName, sDirName, sLogBaseDir, nLogLevel)
        {
        }

        public CBaseLogHelper(string sName, string sDirName, string sLogBaseDir, int nLogLevel)
        {
            try
            {
                m_sName = sName;
                m_sDirName = sDirName;
                m_sLogBaseDir = sLogBaseDir;
                m_eLogLevel = (LOGLEVEL) nLogLevel;

                m_nCodePage = NativeUtil.IsUnix() ? Encoding.UTF8.CodePage : Global.Encoding.CodePage;

                m_pFormatter.AddChannel("SettingCheck", "CONFIG", LOGLEVEL.LEVEL_FATAL, ConsoleColor.DarkYellow);

                m_pFormatter.AddChannel("FATAL", "FATAL", LOGLEVEL.LEVEL_FATAL, ConsoleColor.DarkRed);
                m_pFormatter.AddChannel("error", "ERROR", LOGLEVEL.LEVEL_ERROR, ConsoleColor.Red);
                m_pFormatter.AddChannel("1_ERROR", "ERROR", LOGLEVEL.LEVEL_ERROR, ConsoleColor.Red);
                m_pFormatter.AddChannel("warn", "WARN", LOGLEVEL.LEVEL_WARN, ConsoleColor.Yellow);
                m_pFormatter.AddChannel("info", "INFO", LOGLEVEL.LEVEL_INFO, ConsoleColor.Green);
                m_pFormatter.AddChannel("etcinfo", "SYSTEM", LOGLEVEL.LEVEL_INFO, ConsoleColor.DarkGreen);
                m_pFormatter.AddChannel("debug", "DEBUG", LOGLEVEL.LEVEL_DEBUG, ConsoleColor.Blue);
                m_pFormatter.AddChannel("trace", "TRACE", LOGLEVEL.LEVEL_TRACE, ConsoleColor.Cyan);

                // Verbose data channels: shown only when ServerLogLevel is raised to 6/7.
                m_pFormatter.AddChannel("packet", "PACKET", LOGLEVEL.LEVEL_ALL, ConsoleColor.Cyan);
                m_pFormatter.AddChannel("PACKET_PROFILE", "PROFILE", LOGLEVEL.LEVEL_ALL, ConsoleColor.DarkGray);
                m_pFormatter.AddChannel("timeinfo", "PROFILE", LOGLEVEL.LEVEL_ALL, ConsoleColor.DarkGray);

                m_tConsoleThread = new Thread(TConsole);

                m_szHostName = Dns.GetHostName();

                Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR" + e.Message + "\r\n" + e.StackTrace);
            }
        }

        public string GetHostName()
        {
            return m_szHostName;
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

        public string GetName()
        {
            return m_sName;
        }

        private void ApplyConsoleEncoding()
        {
            // The full-width 【】 brackets need a code page that carries them.
            try
            {
                Console.OutputEncoding = Encoding.GetEncoding(m_nCodePage);
            }
            catch (Exception)
            {
                // Redirected stdout or unsupported code page: keep the console default.
            }
        }

        private void Start()
        {
            ApplyConsoleEncoding();
            m_tConsoleThread.Start();
        }

        private void Stop()
        {
            // Set signal so that the loop comes back and check terminating flag
            m_pConsoleSignal.Set();
        }

        private static string FormatConsole(string context, params object[] args)
        {
            if (args != null && args.Length > 0)
                return string.Format(context, args);
            return context;
        }

        /// <summary>
        ///     Fill a channel's positional template with the caller's extra fields, message last.
        ///     Channels added by AddChannel have no template and print the message as-is.
        /// </summary>
        private string BuildLine(string szType, string szContext, object[] aObjs)
        {
            var szTemplate = m_pFormatter.GetFormatter(szType);
            if (string.IsNullOrEmpty(szTemplate) || aObjs == null || aObjs.Length == 0) return szContext;

            var aArgs = new object[aObjs.Length + 1];
            for (var i = 0; i < aObjs.Length; i++) aArgs[i] = aObjs[i];
            aArgs[aArgs.Length - 1] = szContext;

            try
            {
                return string.Format(szTemplate, aArgs);
            }
            catch (Exception)
            {
                return szContext;
            }
        }

        private void Enqueue(string szType, string szContext)
        {
            if (bTerminating) return;

            m_pConsoleQueue.Enqueue(new EConsole(szContext, szType));
            m_pConsoleSignal.Set();
        }

        private void Print(string szType, string szContext, params object[] aArgs)
        {
            Enqueue(szType, FormatConsole(szContext, aArgs));
        }

        public void SettingCheck(string szContext, params object[] aArgs) { Print("SettingCheck", szContext, aArgs); }

        [Conditional("RELEASE")]
        public void service(string szContext, params object[] aArgs) { Print("error", szContext, aArgs); }

        public void fatal(string szContext, params object[] aArgs)
        {
            WaitUntilFlushed();
            Print("FATAL", szContext, aArgs);
        }

        public void error(string szContext, params object[] aArgs) { Print("error", szContext, aArgs); }

        public void error(Exception ex) { Print("error", ex.Message + "\r\n" + ex.StackTrace); }

        public void warn(string szContext, params object[] aArgs) { Print("warn", szContext, aArgs); }

        public void info(string szContext, params object[] aArgs) { Print("info", szContext, aArgs); }

        public void etcinfo(string szContext, params object[] aArgs) { Print("etcinfo", szContext, aArgs); }

        public void debug(string szContext, params object[] aArgs) { Print("debug", szContext, aArgs); }

        public void trace(string szContext, params object[] aArgs) { Print("trace", szContext, aArgs); }

        public void stackTrace(string szErrorMsg)
        {
            Print("error", "{0}\r\n{1}", szErrorMsg, new StackTrace().ToString());
        }

        public void packet(string szContext, params object[] aArgs) { Print("packet", szContext, aArgs); }

        public void packet_info(string name, string ip, int port, int handle, double recv_time,
            double process_time, byte First, byte Second)
        {
            if (string.IsNullOrEmpty(name)) return;
            Print("PACKET_PROFILE", "{0} {1}:{2} h={3} cls={4}/{5} recv={6:F6}s proc={7:F6}s total={8:F6}s",
                name, ip, port, handle, First, Second, recv_time, process_time, recv_time + process_time);
        }

        /// <summary>
        ///     Legacy entry point used by pre-built DLLs: the message plus positional extra fields.
        /// </summary>
        public void print(string type, string context, object[] args, params object[] objs)
        {
            Enqueue(type, BuildLine(type, FormatConsole(context, args), objs));
        }

        public void PrintToConsole(string context, string type = null, params object[] args)
        {
            Enqueue(type, FormatConsole(context, args));
        }

        private bool bTerminating;

        private void WaitUntilFlushed()
        {
            while (m_pConsoleQueue.Count > 0)
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
                    var szType = tConsoleObj.m_szType;
                    var szContext = tConsoleObj.m_szContext;
                    var szTime = tConsoleObj.m_pTime.ToString("HH:mm:ss.fff");

                    if (szType == null)
                    {
                        HelperLogEvent?.Invoke(szContext);
                        lock (sm_pConsoleLock)
                        {
                            Console.WriteLine(szContext);
                        }
                        continue;
                    }

                    var eLogLevel = m_pFormatter.GetLevel(szType);
                    if (m_eLogLevel < eLogLevel) continue;

                    var szTag = m_pFormatter.GetTag(szType);
                    HelperLogEvent?.Invoke(szTime + " 【" + szTag + "】 " + szContext);

                    var pTagColor = m_pFormatter.GetColor(szType);
                    // Failures keep the whole line colored; everything else prints a neutral message.
                    var pMsgColor = eLogLevel <= LOGLEVEL.LEVEL_ERROR ? pTagColor : ConsoleColor.Gray;

                    lock (sm_pConsoleLock)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkGray;
                        Console.Write(szTime + " ");
                        Console.ForegroundColor = pTagColor;
                        Console.Write("【" + szTag + "】 ");
                        Console.ForegroundColor = pMsgColor;
                        Console.WriteLine(szContext);
                        Console.ResetColor();
                    }
                }
            }
        }
    }
}
