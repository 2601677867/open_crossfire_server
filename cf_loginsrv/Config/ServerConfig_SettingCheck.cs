using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using cf_loginsrv.Log;

namespace cf_loginsrv.Config
{
    internal partial class CServerConfig
    {
        public void CheckSetting()
        {
            CServerLog.GetLogger().SettingCheck(
                "================================================================================================");
            CServerLog.GetLogger().SettingCheck("[Server Start]");
            CServerLog.GetLogger().SettingCheck("");
            CServerLog.GetLogger().SettingCheck("[ServerInfo.ini]");
            CServerLog.GetLogger().SettingCheck("Server Max User : {0}", GetServerMaxUser());
            CServerLog.GetLogger().SettingCheck("ServerDefaultAllocSize : {0}", GetServerDefaultAllocSize());
            CServerLog.GetLogger().SettingCheck("ServerAddAllocSize : {0}", 1000);
            CServerLog.GetLogger().SettingCheck("ClientVersion : {0}", GetClientVersion());
            CServerLog.GetLogger().SettingCheck("ServerServiceForcePort : {0}", GetServerRemotePort());
            CServerLog.GetLogger().SettingCheck("SSN : {0}", GetSSN());

            var szFileName = $"{AppDomain.CurrentDomain.BaseDirectory}/cf_loginsrv.exe";
            
            var fileInfo = new FileInfo(szFileName);
            CServerLog.GetLogger().SettingCheck("Create : [{0:0000}-{1:00}-{2:00}][{3:00}:{4:00}:{5:00}]", 
                fileInfo.CreationTime.Year, fileInfo.CreationTime.Month, fileInfo.CreationTime.Day,
                fileInfo.CreationTime.Hour, fileInfo.CreationTime.Minute, fileInfo.CreationTime.Second);
            CServerLog.GetLogger().SettingCheck("Read : [{0:0000}-{1:00}-{2:00}][{3:00}:{4:00}:{5:00}]", 
                fileInfo.LastAccessTime.Year, fileInfo.LastAccessTime.Month, fileInfo.LastAccessTime.Day,
                fileInfo.LastAccessTime.Hour, fileInfo.LastAccessTime.Minute, fileInfo.LastAccessTime.Second);
            CServerLog.GetLogger().SettingCheck("Modify : [{0:0000}-{1:00}-{2:00}][{3:00}:{4:00}:{5:00}]", 
                fileInfo.LastWriteTime.Year, fileInfo.LastWriteTime.Month, fileInfo.LastWriteTime.Day,
                fileInfo.LastWriteTime.Hour, fileInfo.LastWriteTime.Minute, fileInfo.LastWriteTime.Second);

            if (File.Exists(szFileName))
            {
                if (fileInfo.Length > 0)
                {
                    CServerLog.GetLogger().SettingCheck("File Size : {0} Byte", fileInfo.Length);
                }
                else
                {
                    CServerLog.GetLogger().SettingCheck("Get File Size is Zero");
                }
            }
            else
            {
                CServerLog.GetLogger().SettingCheck("Get File Size Error");
            }

            var bFound = false;
            var NetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface NetworkIntf in NetworkInterfaces)
            {
                var IPInterfaceProperties = NetworkIntf.GetIPProperties();
                var UnicastIPAddressInformationCollection =
                    IPInterfaceProperties.UnicastAddresses;

                foreach (var UnicastIPAddressInformation in UnicastIPAddressInformationCollection)
                {
                    if (UnicastIPAddressInformation.Address.AddressFamily == AddressFamily.InterNetwork)
                    {
                        CServerLog.GetLogger().SettingCheck("Server IP: {0}",
                            UnicastIPAddressInformation.Address.ToString());
                        if (UnicastIPAddressInformation.Address.ToString().Equals(GetServerRemoteAddr()))
                        {
                            bFound = true;
                        }
                    }
                }
            }

            if ( !bFound )
                CServerLog.GetLogger().SettingCheck("Error, Server IP is not available!");
            
            CServerLog.GetLogger().SettingCheck(
                "================================================================================================");
            CServerLog.GetLogger().SettingCheck("\n\n\n");
        }
    }
}