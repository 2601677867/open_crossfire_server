using System;
using System.Linq;
using System.ServiceProcess;

namespace Commons.Service
{
    public static class CServiceHelper
    {
        /// <summary>
        ///     判断是否安装了某个服务
        /// </summary>
        /// <param name="serviceName"></param>
        /// <returns></returns>
        public static bool ISWindowsServiceInstalled(string serviceName)
        {
            try
            {
                var services = ServiceController.GetServices();

                return services.Any(service => service.ServiceName == serviceName);
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        ///     启动某个服务
        /// </summary>
        /// <param name="serviceName"></param>
        public static void StartService(string serviceName)
        {
            try
            {
                var services = ServiceController.GetServices();


                foreach (var service in services)
                {
                    if (service.ServiceName != serviceName) continue;
                    service.Start();

                    service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                }
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     停止某个服务
        /// </summary>
        /// <param name="serviceName"></param>
        public static void StopService(string serviceName)
        {
            try
            {
                var services = ServiceController.GetServices();


                foreach (var service in services)
                {
                    if (service.ServiceName != serviceName) continue;
                    service.Stop();

                    service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                }
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        ///     判断某个服务是否启动
        /// </summary>
        /// <param name="serviceName"></param>
        public static bool ISStart(string serviceName)
        {
            var result = false;

            try
            {
                var services = ServiceController.GetServices();

                foreach (var service in services)
                {
                    if (service.ServiceName != serviceName) continue;
                    if (service.Status == ServiceControllerStatus.Running
                        || service.Status == ServiceControllerStatus.StartPending)
                        result = true;
                }
            }
            catch
            {
                // ignored
            }

            return result;
        }
    }
}