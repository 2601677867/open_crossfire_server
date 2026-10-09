using System;
using cf_loginsrv.Config;
using cf_loginsrv.Log;
using Network.Client;
using Network.Packet;
using Network.Protocol;

namespace cf_loginsrv.Util
{
    public class CLLSMgmtClient
    {
        private readonly CGameServerClient m_pClient;

        public CLLSMgmtClient(string szAddr, ushort usPort,
            CGameServerClient.OnReceiveData eventRecv)
        {
            m_pClient = new CGameServerClient(szAddr, usPort);

            m_pClient.ReceiveClientDataEvent += eventRecv;
        }

        public IntPtr GetHandle()
        {
            return m_pClient.GetHandle();
        }

        public bool IsConnected()
        {
            return m_pClient.IsRunning();
        }

        public bool SendToMgmt(CPacket cPacket)
        {
            return m_pClient.SendPacket(cPacket);
        }
        
        public bool Connect()
        {
            if (!m_pClient.Connect())
            {
                CServerLog.GetLogger().warn("[CLLSMgmtClient::Connect] Failed to connect");
                return false;
            }
            
            var tConnect = new LLSM_PK.PROTO_LLSM_LS_CONNECT();
            tConnect.iMgmtRemotePort = CServerConfig.GetServerRemotePort();

            var cPacket = new CPacket();
            cPacket.SetFirstClass(LLSM_PK.PROTOCOL_LLSM);
            cPacket.SetSecondClass(LLSM_PK.PROTOCOL_LLSM_LS_CONNECT);
            cPacket.CopyToUserDataArea(tConnect);

            SendToMgmt(cPacket);

            return true;
        }

        public void Disconnect()
        {
            m_pClient.Disconnect();
        }
        
        public bool Heartbeat()
        {
            var cPacket = new CPacket();
            cPacket.SetFirstClass(LLSM_PK.PROTOCOL_LLSM);
            cPacket.SetSecondClass(LLSM_PK.PROTOCOL_LLSM_HEARTBEAT);
            cPacket.CopyToUserDataArea(null);
            
            if (!SendToMgmt(cPacket)) return false;

            return true;
        }
    }
}