using System;
using System.Linq;
using System.Net;
using cf_loginsrv.Config;
using cf_loginsrv.Log;
using Network.Packet;
using Network.Protocol;
using Network.Server;

using static Network.Protocol.LG_PK;

namespace cf_loginsrv.Socket
{
    internal class CNetworkMgr : CBaseNetworkMgr
    {
        private readonly int m_nBufferSize;

        private readonly int m_nConnectionCount;
        private readonly string m_szAddress;
        private readonly int m_nPort;
        private CServer m_cSocket;

        public CNetworkMgr(int connections, int bufferSize, string ip, int port) : base(connections)
        {
            m_nConnectionCount = connections;
            m_nBufferSize = bufferSize;
            m_szAddress = ip;
            m_nPort = port;
        }

        public CServer GetSocket()
        {
            return m_cSocket;
        }

        public bool Listen()
        {
            try
            {
                m_cSocket = new CServer(new CLGUserContext(), m_nConnectionCount, m_nBufferSize, CServerLog.LOG_SHORT_NAME);
                CServerLog.GetLogger().info("Socket Initiation Success");
                m_cSocket.IOCPInit();
                CServerLog.GetLogger().info("IOCP Initiation Success");
                m_cSocket.InitBufferPool();
                CServerLog.GetLogger().info("Socket Context Pool Creation Success");
                if (!m_cSocket.Start(new IPEndPoint(IPAddress.Parse(m_szAddress), m_nPort))) return false;
                
                //CServerLog.GetLogger().info($"Success CNetworkMgr::listen(), Port = {m_nPort}");
                return true;
            }
            catch (Exception e)
            {
                CServerLog.GetLogger().error($"Failed CNetworkMgr::listen(), Port = {m_nPort}, ErrCode = {e.HResult}");
                CServerLog.GetLogger().error(e);
                return false;
            }
        }

        public void TimeOutCheck()
        {
            var connections = m_cSocket.GetClients();

            int nExpireCount = 0, nKickCount = 0, nDeadSessionCount = 0;
            foreach (var cContext in connections.Cast<CLGUserContext>())
            {
                switch (cContext.tStatInfo.nStatus)
                {
                    case 10:
                        nExpireCount++;
                        break;
                    case 20:
                        nKickCount++;
                        break;
                    case 30:
                        nDeadSessionCount++;
                        break;
                }

                cContext.tStatInfo.nCreateCharLimitTime++;
                cContext.tStatInfo.nSelectServerLimitTime++;
                
                if (CServerConfig.GetUseAutoServerSelect() == 1 &&
                    !cContext.tStatInfo.bNoticed &&
                    cContext.tStatInfo.nSelectServerLimitTime >= 3 &&
                    cContext.tStatInfo.nStatus != 10)
                {
                    cContext.tStatInfo.bNoticed = true;
                    
                    var cPacket = new CPacket(); // auto login message
                    cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
                    cPacket.SetSecondClass(PROTOCOL_TIMEOUT_NTY);
                    cPacket.SetThirdClass(PROTOCOL_AUTOSELECT_SERVER_NTY);
                    cPacket.CopyToUserDataArea(null);
                    CServer.SendMessage(cContext, cPacket);
                }
                
                if (cContext.tStatInfo.nSelectServerLimitTime == 6)
                {
                    var cPacket = new CPacket();
                    cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
                    cPacket.SetSecondClass(PROTOCOL_TIMEOUT_NTY);
                    cPacket.CopyToUserDataArea(null);
                    CServer.SendMessage(cContext, cPacket); // cn&ph removed
                }
                else if (cContext.tStatInfo.nSelectServerLimitTime >= 6 + 1 ||
                         cContext.tStatInfo.nCreateCharLimitTime >= 21)
                {
                    CServerLog.GetLogger().info("Login - Time expired / IP: {0}", cContext.IPAddress);
                    CServerLog.GetLogger().info(
                        "[CNetworkMgr::TimeOutCheck()] TIME OUT ({0},{1})",
                        cContext.tStatInfo.nSelectServerLimitTime, cContext.tStatInfo.nCreateCharLimitTime);

                    CMainServer.GetGameNetworkHandler().ProcessFinishedClient(cContext);
                    CSocketController.GetLoginServer().GetSocket().CloseClient(cContext);
                }
            }

            if (connections.Count != 0)
                CServerLog.GetLogger().info($"[CNetworkMgr::TimeOutCheck()] T:{connections.Count} N:{nExpireCount} M:{nKickCount} D:{nDeadSessionCount}");
        }

        public void BroadcastPacket<TObject>(int iClientKey, byte byFirstClass, byte bySecondClass, TObject obj)
        {
            var cPacket = new CPacket();
            cPacket.SetFirstClass(byFirstClass);
            cPacket.SetSecondClass(bySecondClass);
            cPacket.CopyToUserDataArea(obj);
            
            BroadcastPacket(iClientKey, cPacket);
        }
        
        public void BroadcastPacket<TObject>(byte byFirstClass, byte bySecondClass, TObject obj)
        {
            BroadcastPacket(-1, byFirstClass, bySecondClass, obj);
        }
        
        public void BroadcastPacket(int iClientKey, byte byFirstClass, byte bySecondClass)
        {
            var cPacket = new CPacket();
            cPacket.SetFirstClass(byFirstClass);
            cPacket.SetSecondClass(bySecondClass);

            BroadcastPacket(iClientKey, cPacket);
        }
        
        public void BroadcastPacket(byte byFirstClass, byte bySecondClass)
        {
            BroadcastPacket(-1, byFirstClass, bySecondClass);
        }

        public void BroadcastPacket(int iClientKey, CPacket cPacket)
        {
            foreach (var cUserTokenBase in m_cSocket.GetClients())
            {
                var cContext = (CLGUserContext) cUserTokenBase;
                if (cContext.iClientKey != iClientKey)
                    CServer.SendMessage(cContext, cPacket);
            }
        }

        public void SendPacket<TObject>(CLGUserContext cContext, byte byFirstClass, byte bySecondClass, TObject obj)
        {
            var cPacket = new CPacket();
            cPacket.SetFirstClass(byFirstClass);
            cPacket.SetSecondClass(bySecondClass);
            cPacket.CopyToUserDataArea(obj);

            CServer.SendMessage(cContext, cPacket);
        }
        
        public void SendPacket(CLGUserContext cContext, byte byFirstClass, byte bySecondClass)
        {
            var cPacket = new CPacket();
            cPacket.SetFirstClass(byFirstClass);
            cPacket.SetSecondClass(bySecondClass);

            CServer.SendMessage(cContext, cPacket);
        }
        
        public void PushServerMessage(byte byFirst, byte bySecond, byte byThird = 0)
        {
            var cPacket = new CPacket();
            cPacket.SetFirstClass(byFirst);
            cPacket.SetSecondClass(bySecond);
            cPacket.SetThirdClass(byThird);
            cPacket.CopyToUserDataArea(null);
            m_cSocket.PushServerMessage(cPacket);
        }
    }
}