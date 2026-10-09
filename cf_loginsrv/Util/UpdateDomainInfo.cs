using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Timers;
using cf_loginsrv.Log;
using Commons;
using Network;
using Timer = System.Timers.Timer;

namespace cf_loginsrv.Util
{
    public class CUpdateDomainInfo : CSingleton<CUpdateDomainInfo>
    {
        private readonly Dictionary<string, string> m_dDomains = new Dictionary<string, string>();

        public void DoUpdate()
        {
            try
            {
                lock (m_dDomains)
                {
                    var dDomainsCopy = new Dictionary<string, string>(m_dDomains);
                
                    foreach (var domain in dDomainsCopy)
                    {
                        m_dDomains[domain.Key] = CIPNetworking.GetIPv4(domain.Key);
                    }
                }
            }
            catch (Exception e)
            {
                CServerLog.GetLogger().error("CUpdateDomainInfo::DoUpdate() Fail : {0}", e.Message);
            }
        }

        private bool Exists(string domain)
        {
	        return m_dDomains.ContainsKey(domain);
        }

        private void AddDomain(string domain)
        {
            try
            {
                m_dDomains[domain] = CIPNetworking.GetIPv4(domain);
            }
            catch (Exception e)
            {
                CServerLog.GetLogger().error("AddDomain() error, failed to add domain address!! : {0}", e.Message);
            }
        }

        public string GetIPAddress(string domain)
        {
            lock (m_dDomains)
            {
                if (!Exists(domain)) AddDomain(domain);
            
                if (!m_dDomains.TryGetValue(domain, out var addr))
                {
                    CServerLog.GetLogger().error("GetIPAddress() error, failed to get domain ip!!");
                    return "0.0.0.0";
                }
                
                return addr;
            }
        }
    }
}