using System.Diagnostics;

namespace PMSConn
{
    public class NetworkUtil
    {
        internal static double GetNetworkUtilization(string networkCard)
        {
            const int numberOfIterations = 10;

            var bandwidthCounter = new PerformanceCounter("Network Interface", "Current Bandwidth", networkCard);
            var bandwidth = bandwidthCounter.NextValue(); //valor fixo 10Mb/100Mn/

            var dataSentCounter = new PerformanceCounter("Network Interface", "Bytes Sent/sec", networkCard);

            var dataReceivedCounter = new PerformanceCounter("Network Interface", "Bytes Received/sec", networkCard);

            float sendSum = 0;
            float receiveSum = 0;

            for (var index = 0; index < numberOfIterations; index++)
            {
                sendSum += dataSentCounter.NextValue();
                receiveSum += dataReceivedCounter.NextValue();
            }

            var dataSent = sendSum;
            var dataReceived = receiveSum;


            double utilization = 8 * (dataSent + dataReceived) / (bandwidth * numberOfIterations) * 100;
            return utilization;
        }
    }
}