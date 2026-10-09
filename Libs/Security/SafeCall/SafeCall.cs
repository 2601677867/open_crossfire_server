using System;
using Commons.Log;
using Security.Dump;

namespace Security.SafeCall
{
    public class CSafeCall
    {
        private readonly bool m_bDump;

        public CSafeCall(bool dump)
        {
            m_bDump = dump;
        }

        public bool TryCall(Func<bool> sFunc)
        {
#if RELEASE
                try
                {
                    return sFunc();
                } catch (Exception e) {
                    CPublicLogger.GetLogger().fatal(e.Message + "\r\n" + e.StackTrace);
                    if (m_bDump)
                    {
                        CMiniDump.TryDump(
                            string.Format("{0}\\dump\\FATAL_{1:yyyy-MM-dd-HH-mm-ss}.dmp", 
                                CPublicLogger.GetLogger().GetFullLogPath(), DateTime.Now));
                    }
                    return false;
                }
#else
            return sFunc();
#endif
        }
        
        public static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            {
                CPublicLogger.GetLogger().fatal((e.ExceptionObject as Exception)?.Message);
            }
            catch
            {
                Console.WriteLine((e.ExceptionObject as Exception)?.Message);
            }

#if RELEASE
            CMiniDump.TryDump(
                string.Format("{0}\\dump\\EXCEPTION_{1:yyyy-MM-dd-HH-mm-ss}.dmp", 
                CPublicLogger.GetLogger().GetFullLogPath(), DateTime.Now));
#endif
        }
    }
}