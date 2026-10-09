#define USE_CONNECT_LOG

using System;
using cf_loginsrv.Log;
using cf_loginsrv.Socket;
using cf_loginsrv.Util;
using Commons.Native;
using DBGWMGR;
using Network.Server;

using static Network.Protocol.P_SZ;

namespace cf_loginsrv.Packet
{
	public partial class CGameNetworkHandler
    {
	    private bool AddLoginLog_ExecuteQuery(CLGUserContext cContext)
	    {
		    if (cContext.tStatInfo.lUSN == -1 || !cContext.tStatInfo.bCharacterCreated) return true;

		    return CServerDataManager.AExecuteLogQuery(E_GDBGW_DEFS.QUERY_ADD_CONNECT_LOG,
			    null, null, cContext.tStatInfo.szDayPartKey, cContext.tStatInfo.lUSN,
			    cContext.tStatInfo.szLoginDateTime, DateTime.Now.ToString("yyyyMMddHHmmss"),
			    cContext.IPAddress.ToString(), NativeUtil.GetTimestamp() - cContext.tStatInfo.nLoginUDate,
			    cContext.tStatInfo.szPCBID == "_" ? "NULL" : cContext.tStatInfo.szPCBID,
				cContext.tStatInfo.nStatus == 30 ? "D" : cContext.tStatInfo.nStatus == 20 ? "N" : "X");
	    }
	    
        public void OnCloseClient(IUserTokenBase tokenBase)
        {
	        var cContext = (CLGUserContext) tokenBase;

	        if (cContext.sMgmtServerIndex != -1)
	        {
		        CServerLog.GetLogger().info("CGameNetworkHandler::OnCloseClient {0}", cContext.sMgmtServerIndex);
	        }

	        ProcessFinishedClient(cContext);
        }

        public void ProcessFinishedClient(CLGUserContext cContext)
        {
	        if (cContext.tStatInfo.lUSN != -1)
	        {
		        // SharedVariable.PrimaryKeys.Remove(token.USN);
		        CSocketController.GetLoginServer().RemoveSocketContext(cContext.iClientKey);
		        cContext.iClientKey = -1;
		        
		        LogoutUser(LG_SERVER_NO, cContext.tStatInfo.lUSN, cContext.tStatInfo.dConnectTime);

		        // PMSLogout(age, region, sex, szPCBID)
#if USE_CONNECT_LOG
				if (!AddLoginLog_ExecuteQuery(cContext))
				{
					CServerLog.GetLogger().error("{0}의 로그를 남기지 못했습니다.", cContext.tStatInfo.lUSN);
				}
#endif
				
				cContext.tStatInfo.lUSN = -1;
	        }
			
	        if (cContext.sMgmtServerIndex != -1)
	        {
		        if (cContext.sMgmtServerNo < 0) return;
		        
		        CMainServer.GetMgmtNetworkHandler().CloseGameServer(cContext.sMgmtServerIndex);
		        LogoutUserInMM(cContext.sMgmtServerNo);
		        
		        CServerLog.GetLogger().info(
			        "Close Game Server!!!! => Server Index : {0}, Server IP : {1}",
			        cContext.sMgmtServerIndex, cContext.IPAddress);
		        cContext.sMgmtServerIndex = -1;
		        cContext.sMgmtServerNo = -1;
	        }
        }
    }
}