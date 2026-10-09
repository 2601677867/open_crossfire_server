using cf_loginsrv.Config;
using cf_loginsrv.Log;

namespace cf_loginsrv.Socket
{
    internal class CBaseNetworkMgr
    {
        protected CLGUserContext[] m_aContextPool;

        public CBaseNetworkMgr(int connections)
        {
            m_aContextPool = new CLGUserContext[connections];
        }

        private bool IsValidClientKey(int iClientKey)
        {
            return iClientKey >= 0 && iClientKey < CServerConfig.GetServerMaxUser();
        }
        
        public CLGUserContext GetSocketContext(int iClientKey)
        {
            return IsValidClientKey(iClientKey) ? m_aContextPool[iClientKey] : null;
        }
        
        public void SetSocketContext(int iClientKey, CLGUserContext cContext)
        {
            if (!IsValidClientKey(iClientKey)) return;

            cContext.iClientKey = iClientKey;
            m_aContextPool[iClientKey] = cContext;
            CServerLog.GetLogger().info("[CBaseNetworkMgr::SetSocketContext] socket client set to: {0}", iClientKey);
        }
        
        public void RemoveSocketContext(int iClientKey)
        {
            if (!IsValidClientKey(iClientKey)) return;
            
            m_aContextPool[iClientKey] = null;
        }

        public int GetNextClientKey()
        {
            for (var i = 0; i < CServerConfig.GetServerMaxUser(); i++)
            {
                if (m_aContextPool[i] == null)
                {
                    return i;
                }
            }

            CServerLog.GetLogger().info("Socket Context OverFlow!");
            return -1;
        }
    }
}