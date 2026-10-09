namespace cf_loginsrv.Socket
{
    internal static class CSocketController
    {
        private static CNetworkMgr sm_pLoginServer;
        private static CNetworkMgr sm_pMgmtServer;

        public static CNetworkMgr GetLoginServer()
        {
            return sm_pLoginServer;
        }

        public static CNetworkMgr GetMgmtServer()
        {
            return sm_pMgmtServer;
        }

        public static void SetLoginServer(CNetworkMgr pServer)
        {
            sm_pLoginServer = pServer;
            sm_pLoginServer.GetSocket().AcceptEvent += CMainServer.GetGameNetworkHandler().OnAccept;
            sm_pLoginServer.GetSocket().ReceiveEvent += CMainServer.GetGameNetworkHandler().OnNetworkMsg;
            sm_pLoginServer.GetSocket().ProcessCloseEvent += CMainServer.GetGameNetworkHandler().OnCloseClient;
        }

        public static void SetMgmtServer(CNetworkMgr pServer)
        {
            sm_pMgmtServer = pServer;
            sm_pMgmtServer.GetSocket().ReceiveEvent += CMainServer.GetMgmtNetworkHandler().OnNetworkMsg;
            sm_pMgmtServer.GetSocket().ProcessCloseEvent += CMainServer.GetGameNetworkHandler().OnCloseClient;
        }
    }
}