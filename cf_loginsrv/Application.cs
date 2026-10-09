using System;
using System.Diagnostics;
using System.ServiceProcess;

namespace cf_loginsrv
{
    internal static class Application
    {
        /// <summary>
        ///     应用程序的主入口点。
        /// </summary>
        private static LoginServer svc;

        private static void Main()
        {
            svc = new LoginServer();
            StartProgram();
            StartService();
        }

        [Conditional("DEBUG")]
        private static void StartProgram()
        {
            var args = new[] {""};
            svc.Start(args);
            while (true)
            {
                var chKey = Console.ReadKey();
                
                if (chKey.Key == ConsoleKey.Escape || chKey.Key == ConsoleKey.Q)
                {
                    Console.WriteLine();
                    break;
                }
            }
            svc.Stop();
        }

        [Conditional("RELEASE")]
        private static void StartService()
        {
            var ServicesToRun = new ServiceBase[] {svc};
            ServiceBase.Run(ServicesToRun);
        }
    }
}