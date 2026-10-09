using System;
using System.Diagnostics;
using System.Timers;
using cf_loginsrv.Config;
using cf_loginsrv.Log;
using cf_loginsrv.Socket;
using Commons.Log;
using Microsoft.VisualBasic.Devices;

namespace cf_loginsrv.Util
{
    public static class CPerformanceChecker
    {
        private static string name;

        private static Timer pTimer;
        private static Timer pPacketCntTimer;

        private static string instanceName = "";

        private static readonly Process currentProcess = Process.GetCurrentProcess();
        private static readonly Computer myInfo = new Computer();

        private static PerformanceCounter[] cpuCounter;
        private static PerformanceCounter cpuTotalCounter;
        private static PerformanceCounter memCounter;
        private static PerformanceCounter memPCounter;

        private static PerformanceCounter networkCBCounter;
        private static PerformanceCounter networkBRCounter;
        private static PerformanceCounter networkBSCounter;
        private static PerformanceCounter networkPRCounter;
        private static PerformanceCounter networkPSCounter;

        private static void PrintCPUInfo(string context)
        {
            CServerLog.GetLogger().PrintToConsole("cpu {0}", "CPUINFO", context);
        }

        private static void PrintMemInfo(string context)
        {
            CServerLog.GetLogger().PrintToConsole("mem {0}", "MEMINFO", context);
        }

        private static void PrintNetworkInfo(string context)
        {
            CServerLog.GetLogger().PrintToConsole("net {0}", "NETWORKINFO", context);
        }
        
        private static void PrintPacketCntInfo(string context)
        {
            CServerLog.GetLogger().PrintToConsole("free/abandoned/received/buffer {0}", "PACKETCNT", context);
        }
        
        private static void PrintIOBufferInfo(string context)
        {
            CServerLog.GetLogger().PrintToConsole("iobuffer {0}", "IOBUFFER", context);
        }

        private static void TPacketCnt(object sender, ElapsedEventArgs e)
        {
            var socket = CSocketController.GetLoginServer().GetSocket();
            var context_packet_cnt = socket.GetFreePoolCount().ToString();
            context_packet_cnt += "\t" + socket.m_nAbandonedPacketCount;
            context_packet_cnt += "\t" + socket.m_nReceivedPacketCount;
            context_packet_cnt += "\t" + socket.m_nReceivedTotalBuffer;
            
            socket.ResetDebugInfo();
            
            PrintPacketCntInfo(context_packet_cnt);
        }
        
        private static void TPerformance(object sender, ElapsedEventArgs e)
        {
            var context_cpu = ((int) cpuTotalCounter.NextValue()).ToString();
            for (var i = 0; i < cpuCounter.Length; i++) context_cpu += "\t" + (int) cpuCounter[i].NextValue();
            PrintCPUInfo(context_cpu);

            var context_mem = ((int) (memCounter.NextValue() / 1024 / 1024)).ToString();
            context_mem += "\t" + (int) (memPCounter.NextValue() / 1024 / 1024);
            context_mem += "\t" + (myInfo.Info.TotalPhysicalMemory - myInfo.Info.AvailablePhysicalMemory) / 1024 / 1024;
            context_mem += "\t" + myInfo.Info.TotalPhysicalMemory / 1024 / 1024;
            context_mem += "\t" + (myInfo.Info.TotalVirtualMemory - myInfo.Info.AvailableVirtualMemory) / 1024 / 1024;
            context_mem += "\t" + myInfo.Info.TotalVirtualMemory / 1024 / 1024;
            PrintMemInfo(context_mem);

            var context_network = instanceName;
            context_network += "\t" + (int) networkCBCounter.NextValue();
            context_network += "\t" + (int) networkBSCounter.NextValue();
            context_network += "\t" + (int) networkBRCounter.NextValue();
            context_network += "\t" + (int) networkPSCounter.NextValue();
            context_network += "\t" + (int) networkPRCounter.NextValue();
            PrintNetworkInfo(context_network);

            var data = CSocketController.GetLoginServer().GetSocket().GetDebugInfo();
            var context_buffer = data.Item1.ToString();
            context_buffer += "\t" + data.Item2;
            context_buffer += "\t" + data.Item3;
            context_buffer += "\t" + data.Item4;
            context_buffer += "\t" + data.Item5;
            context_buffer += "\t" + data.Item6;
            context_buffer += "\t" + data.Item7;
            PrintIOBufferInfo(context_buffer);
            
            var context_buffer_log = data.Item1.ToString();
            context_buffer_log += ";\t" + data.Item2;
            context_buffer_log += ";\t" + data.Item3;
            context_buffer_log += ";\t" + data.Item4;
            context_buffer_log += ";\t" + data.Item5;
            context_buffer_log += ";\t" + data.Item6;
            context_buffer_log += ";\t" + data.Item7;
            CServerLog.PrintSvrIoState(context_buffer_log);
        }

        public static void Init(string _name)
        {
            pTimer = new Timer(30 * 1000);
            pTimer.Elapsed += TPerformance;
            
            pPacketCntTimer = new Timer(10 * 1000);
            pPacketCntTimer.Elapsed += TPacketCnt;

            name = _name;

            cpuCounter = new PerformanceCounter[Environment.ProcessorCount];
            for (var i = 0; i < cpuCounter.Length; i++)
            {
                cpuCounter[i] = new PerformanceCounter
                {
                    CategoryName = "Processor", CounterName = "% Processor Time", InstanceName = i.ToString()
                };
                cpuCounter[i].NextValue(); // Skip Value 0
            }

            cpuTotalCounter = new PerformanceCounter
            {
                CategoryName = "Processor", CounterName = "% Processor Time", InstanceName = "_Total"
            };
            cpuTotalCounter.NextValue(); // Skip Value 0

            memCounter = new PerformanceCounter
            {
                CategoryName = "Process", CounterName = "Working Set", InstanceName = currentProcess.ProcessName
            };
            memCounter.NextValue(); // Skip Value 0

            memPCounter = new PerformanceCounter
            {
                CategoryName = "Process",
                CounterName = "Working Set - Private",
                InstanceName = currentProcess.ProcessName
            };
            memPCounter.NextValue(); // Skip Value 0

            var category = new PerformanceCounterCategory("Network Interface");

            foreach (var sInstanceName in category.GetInstanceNames())
            {
                if (sInstanceName.Contains("Loopback")) continue;
                if (sInstanceName.Contains("isatap")) continue;
                if (sInstanceName.Contains("环回")) continue;
                if (sInstanceName.Contains("Interface")) continue;
                if (sInstanceName.Contains("{") || sInstanceName.Contains("}")) continue;
                instanceName = sInstanceName;
                break;
            }

            networkBRCounter = new PerformanceCounter
            {
                CategoryName = "Network Interface", CounterName = "Bytes Received/sec", InstanceName = instanceName
            };
            networkBRCounter.NextValue(); // Skip Value 0

            networkBSCounter = new PerformanceCounter
            {
                CategoryName = "Network Interface", CounterName = "Bytes Sent/sec", InstanceName = instanceName
            };
            networkBSCounter.NextValue(); // Skip Value 0

            networkPRCounter = new PerformanceCounter
            {
                CategoryName = "Network Interface",
                CounterName = "Packets Received/sec",
                InstanceName = instanceName
            };
            networkPRCounter.NextValue(); // Skip Value 0

            networkPSCounter = new PerformanceCounter
            {
                CategoryName = "Network Interface", CounterName = "Packets Sent/sec", InstanceName = instanceName
            };
            networkPSCounter.NextValue(); // Skip Value 0

            networkCBCounter = new PerformanceCounter
            {
                CategoryName = "Network Interface", CounterName = "Current Bandwidth", InstanceName = instanceName
            };
            networkCBCounter.NextValue(); // Skip Value 0


            var pFormatter = CServerLog.GetLogger().GetFormatter();
            pFormatter.AddChannel("CPUINFO", "PERF", LOGLEVEL.LEVEL_ALL, ConsoleColor.DarkMagenta);
            pFormatter.AddChannel("MEMINFO", "PERF", LOGLEVEL.LEVEL_ALL, ConsoleColor.DarkMagenta);
            pFormatter.AddChannel("NETWORKINFO", "PERF", LOGLEVEL.LEVEL_ALL, ConsoleColor.DarkMagenta);
            pFormatter.AddChannel("PACKETCNT", "PERF", LOGLEVEL.LEVEL_ALL, ConsoleColor.DarkMagenta);
            pFormatter.AddChannel("IOBUFFER", "PERF", LOGLEVEL.LEVEL_ALL, ConsoleColor.DarkMagenta);
        }

        public static void Start()
        {
            pTimer.Start();
            pPacketCntTimer.Start();
        }

        public static void Stop()
        {
            pTimer?.Stop();
            pPacketCntTimer?.Stop();
        }
    }
}