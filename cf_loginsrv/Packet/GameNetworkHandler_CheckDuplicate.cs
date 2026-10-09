using System.Collections.Generic;
using cf_loginsrv.Log;
using cf_loginsrv.Socket;
using Network.Packet;
using Network.Protocol;
using Network.Server;

using static Network.Protocol.P_SZ;

namespace cf_loginsrv.Packet
{
    public partial class CGameNetworkHandler
    {
        public CGameNetworkHandler()
        {
            // add 1 for login server index 100
            sm_lstUsers = new List<UserStat_tag>[MAX_GAMESERVER_COUNT + 1];
            for (var i = 0; i < MAX_GAMESERVER_COUNT + 1; i ++)
                sm_lstUsers[i] = new List<UserStat_tag>();
        }
        
        public void HandleDuplicateConnection(CLGUserContext cContext)
        {
            lock (sm_lstUsers)
            {
                foreach (var cUserTokenBase in CSocketController.GetLoginServer().GetSocket().GetClients())
                {
                    var cLoggedInContext = (CLGUserContext) cUserTokenBase;
                    if (cLoggedInContext.tStatInfo.dConnectTime != cContext.tStatInfo.dConnectTime
                        && cLoggedInContext.tStatInfo.lUSN == cContext.tStatInfo.lReqUSN)
                    {
                        cLoggedInContext.tStatInfo.nStatus = 30;
                            
                        var cPacket = new CPacket();
                        cPacket.SetFirstClass(LG_PK.PROTOCOL_LOGIN_FIRST_CLASS);
                        cPacket.SetSecondClass(LG_PK.PROTOCOL_REQUEST_SAMEIDCONNECT_FORCE_LEAVE_NTY);
                        CServer.SendMessage(cLoggedInContext, cPacket, true);
                        
                        CServerLog.GetLogger().info("[CGameNetworkHandler::HandleDuplicateConnection]");
                            
                        ProcessFinishedClient(cLoggedInContext);
                    }
                }
            }
        }

        public void SendForceLeaveMsgToMM(CLGUserContext cContext, int iMMIndex)
        {
            var tForceLeave = new LMS_PK.PROTO_MGMT_SAMEIDCONNECT_FORCE_LEAVE
            {
                m_lUSN = cContext.tStatInfo.lReqUSN,
                m_iLoginTime = cContext.tStatInfo.nLoginUDate,
                m_dConnectTime = cContext.tStatInfo.dConnectTime
            };

            var cPacket = new CPacket();
            cPacket.SetFirstClass(LMS_PK.PROTOCOL_LOGIN_MGMT);
            cPacket.SetSecondClass(LMS_PK.PROTOCOL_MGMT_SAMEIDCONNECT_FORCE_LEAVE);
            cPacket.CopyToUserDataArea(tForceLeave);

            foreach (var userTokenBase in CSocketController.GetMgmtServer().GetSocket().GetClients())
            {
                var cMMContext = (CLGUserContext) userTokenBase;
                if (cMMContext.sMgmtServerNo != -1 && cMMContext.sMgmtServerNo == iMMIndex) 
                    CServer.SendMessage(cMMContext, cPacket);
            }
        }

        public class UserStat_tag
        {
            public long m_lUSN;
            public double m_dConnectTime;
        }
        public List<UserStat_tag>[] sm_lstUsers;
       
        public void UpdateUserLocation(short sServerNo, long lUSN, double dConnectTime)
        {
            //CServerLog.GetLogger().debug("UpdateUserLocation {0} {1} {2}", sServerNo, lUSN, dConnectTime);
            lock (sm_lstUsers)
            {
                //if (s_mUserStatus[sServerNo].Contains(lUSN)) s_mUserStatus.Remove(lUSN);
            
                sm_lstUsers[sServerNo].Add(new UserStat_tag
                {
                    m_lUSN = lUSN,
                    m_dConnectTime = dConnectTime
                });
            }
        }
        
        public void LogoutUser(short sServerNo, long lUSN, double dConnectTime)
        {
            //CServerLog.GetLogger().debug("LogoutUser {0} {1} {2}", sServerNo, lUSN, dConnectTime);
            lock (sm_lstUsers)
            {
                var iIndex = sm_lstUsers[sServerNo].FindIndex(x => x.m_lUSN == lUSN && x.m_dConnectTime == dConnectTime);
                if (iIndex != -1)
                {
                    sm_lstUsers[sServerNo].RemoveAt(iIndex);
                }
                /*else
                {
                    CServerLog.GetLogger().error(
                        "LogoutUser : iIndex == -1!!! usn {0}, sServerNo {1} dConnectTime {2}",
                        lUSN, sServerNo, dConnectTime);
                }*/
            }
        }

        public bool CheckDuplicate(short sServerNo, long lUSN, double dConnectTime)
        {
            //CServerLog.GetLogger().debug("CheckDuplicate {0} {1} {2}", sServerNo, lUSN, dConnectTime);
            lock (sm_lstUsers)
            {
                foreach (var lstServerUsers in sm_lstUsers)
                {
                    if (lstServerUsers.FindIndex(x => x.m_lUSN == lUSN && x.m_dConnectTime != dConnectTime) != -1)
                    {
                        CServerLog.GetLogger().info("CheckDuplicate : {0}, {1:F6}", lUSN, dConnectTime);
                        return true;
                    }
                }
            }

            return false;
        }
        
        public void LogoutUserInMM(short sServerNo)
        {
            //CServerLog.GetLogger().debug("LogoutUserInMM {0}", sServerNo);
            lock (sm_lstUsers)
            {
                sm_lstUsers[sServerNo].Clear();
            }
        }
    }
}