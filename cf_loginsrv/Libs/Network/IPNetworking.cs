using System.Net;

namespace Network
{
    public static class CIPNetworking
    {
        /// <summary>
        ///     取得指定主机 IPv4 地址
        /// </summary>
        public static string GetIPv4(string hostNameOrAddress)
        {
            var ipv4 = string.Empty;

            var ipArray = Dns.GetHostAddresses(hostNameOrAddress);
            foreach (var ip in ipArray)
            {
                if (ip.AddressFamily.ToString() != "InterNetwork") continue;
                ipv4 = ip.ToString();
                break;
            }

            if (ipv4 != string.Empty)
                return ipv4;

            ipArray = Dns.GetHostEntry(hostNameOrAddress).AddressList;
            foreach (var ip in ipArray)
            {
                if (ip.AddressFamily.ToString() != "InterNetwork") continue;
                ipv4 = ip.ToString();
                break;
            }

            return ipv4;
        }
    }
}