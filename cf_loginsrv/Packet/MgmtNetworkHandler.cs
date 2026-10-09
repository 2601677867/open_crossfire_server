using System;
using System.Diagnostics;
using System.Net;
using System.Text;
using cf_loginsrv.Config;
using cf_loginsrv.Log;
using cf_loginsrv.Socket;
using cf_loginsrv.Util;
using Commons.Native;
using Network.Packet;
using Network.Protocol;
using Network.Server;

using static Network.Protocol.LMS_PK;
using static Network.Protocol.P_SZ;
using static Network.Assert.ProtocolAssert;

namespace cf_loginsrv.Packet
{
    public class CMgmtNetworkHandler : CBaseNetworkHandler
    {
        public void CloseGameServer(short sServerIndex)
        {
            lock (CMainServer.m_aGameServers)
            {
                CMainServer.m_aGameServers[sServerIndex].m_bGameServerConnected = false;
            }
        }
        
        private short GetMgmtServerIndex(short sServerHighProperty, short sServerLowProperty,
            short sServerHighLimit, short sServerLowLimit, double dServerHighKillDeath, double dServerLowKilLDeath,
            int iServerAddr, int iServerPort, byte byPassword, out short sServerDatabaseNo)
        {
            sServerDatabaseNo = -1;
            short sServerIndex = -1;
            
            lock (CMainServer.m_aGameServers)
            {
                for (short i = 0; i < MAX_GAMESERVER_COUNT; i++)
                {
                    if ((CMainServer.m_aGameServers[i].m_dwInternalAddr == iServerAddr || 
                         CMainServer.m_aGameServers[i].m_dwServerAddr == iServerAddr) &&
                        CMainServer.m_aGameServers[i].m_nServerPort == iServerPort)
                    {
                        sServerIndex = i;
                        break;
                    }
                }

                if (sServerIndex == -1)
                {
                    CMainServer.RefreshServerList();
                    for (short i = 0; i < MAX_GAMESERVER_COUNT; i++)
                    {
                        if ((CMainServer.m_aGameServers[i].m_dwInternalAddr == iServerAddr || 
                             CMainServer.m_aGameServers[i].m_dwServerAddr == iServerAddr) &&
                            CMainServer.m_aGameServers[i].m_nServerPort == iServerPort)
                        {
                            sServerIndex = i;
                            break;
                        }
                    }
                }

                if (sServerIndex != -1)
                {
                    if (CMainServer.m_aGameServers[sServerIndex].m_bGameServerConnected)
                    {
                        sServerIndex = -2;
                    }
                    else
                    {
                        CMainServer.m_aGameServers[sServerIndex].m_bGameServerConnected = true;
                        CMainServer.m_aGameServers[sServerIndex].m_nServerHighProperty = sServerHighProperty;
                        CMainServer.m_aGameServers[sServerIndex].m_nServerLowProperty = sServerLowProperty;
                        CMainServer.m_aGameServers[sServerIndex].m_nServerHighLimit = sServerHighLimit;
                        CMainServer.m_aGameServers[sServerIndex].m_nServerLowLimit = sServerLowLimit;
                        CMainServer.m_aGameServers[sServerIndex].m_dServerHighKD = dServerHighKillDeath;
                        CMainServer.m_aGameServers[sServerIndex].m_dServerLowKD = dServerLowKilLDeath;
                        CMainServer.m_aGameServers[sServerIndex].m_nPassword = byPassword;
                    
                        sServerDatabaseNo = CMainServer.m_aGameServers[sServerIndex].m_nServerID;
                    }
                }
            }

            return sServerIndex;
        }

        private void OnMgmtConnect(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_CONNECT tConnect = default;
            
            if (!cPacket.CopyFromUserDataArea(ref tConnect))
            {
                CServerLog.GetLogger().info("IP: {0}", cContext.IPAddress);
                return;
            }

            var sServerIndex = GetMgmtServerIndex(tConnect.m_sServerHighProperty,
                tConnect.m_sServerLowProperty, tConnect.m_sServerHighLimit,
                tConnect.m_sServerLowLimit, tConnect.m_dHighLimitKillDeath,
                tConnect.m_dLowLimitKillDeath, tConnect.m_iRemoteAddrInet,
                tConnect.m_iRemotePort, tConnect.m_byPassword, out var sServerNo);

            CServerLog.GetLogger().info(
                "MGMT_CONNECT {0} : {1} : {2:F6} : {3} ",
                tConnect.m_iRemoteAddrInet, tConnect.m_iRemotePort, 
                tConnect.m_dLowLimitKillDeath, sServerIndex);
           
            CServerLog.GetLogger().SettingCheck(
                "MM_CONNECTED IP : {0}, PORT : {1}, Server INDEX : {2} ",
                new IPAddress(tConnect.m_iRemoteAddrInet), tConnect.m_iRemotePort, sServerIndex);
            
            CServerLog.GetLogger().info(
                "MGMT_CONNECT {0} : {1} : {2:F6} : {3} ",
                tConnect.m_iRemoteAddrInet, tConnect.m_iRemotePort, 
                tConnect.m_dHighLimitKillDeath, sServerIndex);

            var tConnectRet = new PROTO_MGMT_CONNECT_RESULT();
            
            if (sServerIndex == -1)
            {
                tConnectRet.m_eResult = PROTO_MGMT_CONNECT_RESULT.CONNECTRESULT.FAILED;
            }
            else if (sServerIndex == -2)
            {
                tConnectRet.m_eResult = PROTO_MGMT_CONNECT_RESULT.CONNECTRESULT.ALREADY_CONNECTED;
            }
            else
            {
                tConnectRet.m_eResult = PROTO_MGMT_CONNECT_RESULT.CONNECTRESULT.SUCCESS;
                cContext.sMgmtServerNo = sServerNo;
                cContext.sMgmtServerIndex = sServerIndex;
            }

            tConnectRet.m_sServerIndex = sServerIndex;
            tConnectRet.m_byGlobalJoin = CServerConfig.GetUseGlobalJoin();
            
            cPacket.SetFirstClass(PROTOCOL_LOGIN_MGMT);
            cPacket.SetSecondClass(PROTOCOL_MGMT_CONNECT_RESULT);
            cPacket.CopyToUserDataArea(tConnectRet);
            CServer.SendMessage(cContext, cPacket);

            if (sServerIndex < 0)
            {
                CSocketController.GetMgmtServer().GetSocket().CloseClient(cContext);
                CServerLog.GetLogger().SettingCheck(
                    "Invalid MM Server Connected => IP : {0}, Port : {1}, Index : {2}",
                    new IPAddress(tConnect.m_iRemoteAddrInet), tConnect.m_iRemotePort, sServerIndex);
            }
        }

        private void OnMgmtUserInfoUpdate(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_USERINFO_UPDATE tUserInfoUpdate = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tUserInfoUpdate) );
            
            if (tUserInfoUpdate.m_eAction == PROTO_MGMT_USERINFO_UPDATE.ACTION.LOGOUTUSER)
            {
                CMainServer.GetGameNetworkHandler().LogoutUser(
                    cContext.sMgmtServerNo, tUserInfoUpdate.m_lUSN, tUserInfoUpdate.m_dConnectTime);
            }
            else if (tUserInfoUpdate.m_eAction == PROTO_MGMT_USERINFO_UPDATE.ACTION.CHECKLOGIN)
            {
                var tUserInfoUpdateRet = new PROTO_MGMT_USERINFO_UPDATE_RESULT();

                if (CMainServer.GetGameNetworkHandler().CheckDuplicate(
                    cContext.sMgmtServerNo, tUserInfoUpdate.m_lUSN, tUserInfoUpdate.m_dConnectTime))
                {
                    tUserInfoUpdateRet.m_eResult = PROTO_MGMT_USERINFO_UPDATE_RESULT.RESULT.DUPLICATE;
                }
                else if (!CServerDataManager.CheckAuthKeyValid_ExecuteQuery(
                    tUserInfoUpdate.m_lUSN,
                    Encoding.ASCII.GetBytes(tUserInfoUpdate.m_lKey1 + tUserInfoUpdate.m_lKey2.ToString()), 
                    tUserInfoUpdate.m_szClientIP))
                {
                    tUserInfoUpdateRet.m_eResult = PROTO_MGMT_USERINFO_UPDATE_RESULT.RESULT.INVALID_KEY_VALUE;
                }
                else
                {
                    tUserInfoUpdateRet.m_eResult = PROTO_MGMT_USERINFO_UPDATE_RESULT.RESULT.SUCCESS;
                    CMainServer.GetGameNetworkHandler().UpdateUserLocation(
                        cContext.sMgmtServerNo, tUserInfoUpdate.m_lUSN, tUserInfoUpdate.m_dConnectTime);
                }

                tUserInfoUpdateRet.m_lUSN = tUserInfoUpdate.m_lUSN;
                tUserInfoUpdateRet.m_iClientKey = tUserInfoUpdate.m_iClientKey;
                
                cPacket.SetFirstClass(PROTOCOL_LOGIN_MGMT);
                cPacket.SetSecondClass(PROTOCOL_MGMT_USERINFO_UPDATE_RESULT);
                cPacket.CopyToUserDataArea(tUserInfoUpdateRet);
                CServer.SendMessage(cContext, cPacket);
            }
        }

        private void OnMgmtExceptionData(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_EXCEPTION_DATA tExceptionData = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tExceptionData) );

            var iCount = 0;
            for (var i = 0; i < MAX_EXCEPTION_DATA_PER_PACKET; i++)
            {
                if (tExceptionData.aExceptionData[i].m_lUSN == -1) continue;
                CMainServer.GetGameNetworkHandler().UpdateUserLocation(
                    cContext.sMgmtServerNo, 
                    tExceptionData.aExceptionData[i].m_lUSN, 
                    tExceptionData.aExceptionData[i].m_dConnectTime);
                iCount++;
            }
            
            CServerLog.GetLogger().info("[MGMT_EXCEPTION_DATA] {0}번의 서버에서 {1}명의 정보가 들어왔습니다.",
                cContext.sMgmtServerNo, iCount);
        }

        private void OnCrossFireEventSelectResult(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_CROSSFIRE_EVENT_SELECT_RESULT tEvent = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tEvent) );
            
            CServerLog.GetLogger().info("[운영자] {0}시 {1}분 집중공격 이벤트 당첨자는 {2}(이용자콜네임)님 입니다.",
                DateTime.Now.Hour, DateTime.Now.Minute,
                NativeUtil.BArrToStr(tEvent.aszCallName));
        }

        private void OnGachaWinnerNty(CLGUserContext cContext, CPacket cPacket)
        {
            MM_PK.PROTO_GACHA_WINNER_NTY tGachaWinnerNty = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tGachaWinnerNty) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_GACHA_WINNER_NTY, tGachaWinnerNty);
        }

        private void OnMgmtHeartBeat(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_HEARTBEAT tHeartbeat = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tHeartbeat) );

            if (cContext.sMgmtServerIndex == -1)
            {
                CServerLog.GetLogger().info(
                    "[MGMT_HEARTBEAT]Server not connected => IP : {0}, Port : {1}, Index : {2}",
                    NativeUtil.inet_ntoa(tHeartbeat.m_iRemoteAddrInet),
                    tHeartbeat.m_iRemotePort,
                    cContext.sMgmtServerIndex);
                return;
            }
            
            cPacket.SetFirstClass(PROTOCOL_LOGIN_MGMT);
            cPacket.SetSecondClass(PROTOCOL_MGMT_HEARTBEAT);
            cPacket.CopyToUserDataArea(tHeartbeat);
            CServer.SendMessage(cContext, cPacket);
        }

        private void OnSameConnectForceLeave(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SAMEIDCONNECT_FORCE_LEAVE tForceLeave = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tForceLeave) );
            
            if (cContext.sMgmtServerNo == -1) return;
            
            lock (CMainServer.GetGameNetworkHandler().sm_lstUsers)
            {
                for (var i = 0; i < MAX_GAMESERVER_COUNT + 1; i++)
                {
                    if (CMainServer.GetGameNetworkHandler().sm_lstUsers[i].FindIndex(x =>
                        x.m_lUSN == cContext.tStatInfo.lReqUSN &&
                        x.m_dConnectTime != cContext.tStatInfo.dConnectTime) != -1)
                    {
                        if (i == LG_SERVER_NO)
                            CMainServer.GetGameNetworkHandler().HandleDuplicateConnection(cContext);
                        else
                            CMainServer.GetGameNetworkHandler().SendForceLeaveMsgToMM(cContext, i);
                    }
                }
            }
        }

        private void OnSPNotify(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_NOTIFY tProtoNotify = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoNotify) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_NOTIFY, tProtoNotify);
        }
        
        private void OnSPRolling(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_ROLLING tProtoRolling = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoRolling) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_ROLLING, tProtoRolling);
        }
        
        private void OnSPRollStop(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_ROLLSTOP tProtoRollStop = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoRollStop) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_ROLLSTOP, tProtoRollStop);
        }
        
        private void OnSPKick(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_KICK tProtoKick = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoKick) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_KICK, tProtoKick);
        }
        
        private void OnSPKickResult(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_KICK_RESULT tProtoKickResult = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoKickResult) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_KICK_RESULT, tProtoKickResult);
        }
        
        private void OnSPQKick(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_QKICK tProtoQKick = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoQKick) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_QKICK, tProtoQKick);
        }
        
        private void OnSPQKickResult(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_QKICK_RESULT tProtoQKickResult = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoQKickResult) );
           
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_QKICK_RESULT, tProtoQKickResult);
        }
        
        private void OnSPChatOff(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_CHATOFF tProtoChatOff = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoChatOff) );
           
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_CHATOFF, tProtoChatOff);
        }
        
        private void OnSPChatOffResult(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_CHATOFF_RESULT tProtoChatOffResult = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoChatOffResult) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_CHATOFF_RESULT, tProtoChatOffResult);
        }
        
        private void OnSPChatOn(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_CHATON tProtoChatOn = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoChatOn) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_CHATON, tProtoChatOn);
        }
        
        private void OnSPChatOnResult(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_CHATON_RESULT tProtoChatOnResult = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoChatOnResult) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_CHATON_RESULT, tProtoChatOnResult);
        }
        
        private void OnSPWhisper(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_WHISPER tProtoWhisper = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoWhisper) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_WHISPER, tProtoWhisper);
        }
        
        private void OnSPWhisperResult(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_MGMT_SUPERVISOR_WHISPER_RESULT tProtoWhisperResult = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tProtoWhisperResult) );
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                PROTOCOL_LOGIN_MGMT, PROTOCOL_MGMT_SUPERVISOR_WHISPER_RESULT, tProtoWhisperResult);
        }
        
        private string OnRcvMgmtMsg(CLGUserContext cContext, CPacket cPacket)
        {
            switch (cPacket.GetSecondClass())
            {
                case PROTOCOL_MGMT_CONNECT:
                    OnMgmtConnect(cContext, cPacket);
                    return "MGMT_CONNECT";
                case PROTOCOL_MGMT_UPDATE:
                    return "MGMT_UPDATE";
                case PROTOCOL_MGMT_SERVEROFF:
                    return "MGMT_SERVEROFF";
                case PROTOCOL_MGMT_USERINFO_UPDATE:
                    OnMgmtUserInfoUpdate(cContext, cPacket);
                    return "MGMT_USERINFO_UPDATE";
                case PROTOCOL_MGMT_SAMEIDCONNECT_FORCE_LEAVE:
                    OnSameConnectForceLeave(cContext, cPacket);
                    return "MGMT_SAMEIDCONNECT_FORCE_LEAVE";
                case PROTOCOL_MGMT_SUPERVISOR_NOTIFY:
                    OnSPNotify(cContext, cPacket);
                    return "MGMT_SUPERVISOR_NOTIFY";
                case PROTOCOL_MGMT_SUPERVISOR_ROLLING:
                    OnSPRolling(cContext, cPacket);
                    return "MGMT_SUPERVISOR_ROLLING";
                case PROTOCOL_MGMT_SUPERVISOR_ROLLSTOP:
                    OnSPRollStop(cContext, cPacket);
                    return "MGMT_SUPERVISOR_ROLLSTOP";
                case PROTOCOL_MGMT_SUPERVISOR_KICK:
                    OnSPKick(cContext, cPacket);
                    return "MGMT_SUPERVISOR_KICK";
                case PROTOCOL_MGMT_SUPERVISOR_KICK_RESULT:
                    OnSPKickResult(cContext, cPacket);
                    return "MGMT_SUPERVISOR_KICK_RESULT";
                case PROTOCOL_MGMT_SUPERVISOR_QKICK:
                    OnSPQKick(cContext, cPacket);
                    return "MGMT_SUPERVISOR_QKICK";
                case PROTOCOL_MGMT_SUPERVISOR_QKICK_RESULT:
                    OnSPQKickResult(cContext, cPacket);
                    return "MGMT_SUPERVISOR_QKICK_RESULT";
                case PROTOCOL_MGMT_SUPERVISOR_CHATOFF:
                    OnSPChatOff(cContext, cPacket);
                    return "MGMT_SUPERVISOR_CHATOFF";
                case PROTOCOL_MGMT_SUPERVISOR_CHATOFF_RESULT:
                    OnSPChatOffResult(cContext, cPacket);
                    return "MGMT_SUPERVISOR_CHATOFF_RESULT";
                case PROTOCOL_MGMT_SUPERVISOR_CHATON:
                    OnSPChatOn(cContext, cPacket);
                    return "MGMT_SUPERVISOR_CHATON";
                case PROTOCOL_MGMT_SUPERVISOR_CHATON_RESULT:
                    OnSPChatOnResult(cContext, cPacket);
                    return "MGMT_SUPERVISOR_CHATON_RESULT";
                case PROTOCOL_MGMT_SUPERVISOR_WHISPER:
                    OnSPWhisper(cContext, cPacket);
                    return "MGMT_SUPERVISOR_WHISPER";
                case PROTOCOL_MGMT_SUPERVISOR_WHISPER_RESULT:
                    OnSPWhisperResult(cContext, cPacket);
                    return "MGMT_SUPERVISOR_WHISPER_RESULT";
                case PROTOCOL_MGMT_EXCEPTION_DATA:
                    OnMgmtExceptionData(cContext, cPacket);
                    return "MGMT_EXCEPTION_DATA";
                case PROTOCOL_MGMT_CROSSFIRE_EVENT_SELECT_RESULT:
                    OnCrossFireEventSelectResult(cContext, cPacket);
                    return "MGMT_CROSSFIRE_EVENT_SELECT_RESULT";
                case PROTOCOL_MGMT_GACHA_WINNER_NTY:
                    OnGachaWinnerNty(cContext, cPacket);
                    return "MGMT_GACHA_WINNER_NTY";
                case PROTOCOL_MGMT_HEARTBEAT:
                    OnMgmtHeartBeat(cContext, cPacket);
                    return "MGMT_HEARTBEAT";
                default:
                    CServerLog.GetLogger().etcinfo(
                        "[CGameNetworkHandler::OnRcvMgmtMsg] invalid packet second {0} third {1}",
                        cPacket.GetSecondClass(), cPacket.GetThirdClass());
                    break;
            }

            return null;
        }
        
        #region Base Methods

        public void OnNetworkMsg(IUserTokenBase tokenBase, byte[] buff, double recv_time)
        {
            var cContext = (CLGUserContext) tokenBase;
            
            var cPacket = new CPacket(buff);
            
            // Create our debug info
            if (cPacket.GetReceivedSize() > PROTOCOL_NON_USER_AREA_SIZE)
            {
                //CreateDebugInfo(buff);
            }
            
            //CServerLog.GetLogger().packet_trace(
            //    "[ MGMT RECV ][ PACKET First 0x{0:X2} Second 0x{1:X2} Third 0x{2:X2} ]",
            //    cPacket.GetFirstClass(), cPacket.GetSecondClass(), cPacket.GetThirdClass());
            
            if (cPacket.GetFirstClass() == PROTOCOL_LOGIN_MGMT)
            {
                try
                {
                    var swProcessTime = new Stopwatch();

                    swProcessTime.Reset();
                    swProcessTime.Start();
                    
                    var szProtoName = OnRcvMgmtMsg(cContext, cPacket);
                    
                    swProcessTime.Stop();

                    if (!string.IsNullOrEmpty(szProtoName))
                    {
                        CServerLog.PacketProfile(szProtoName, cContext, recv_time, swProcessTime, cPacket);
                    }
                }
                catch (Exception e)
                {
                    CServerLog.GetLogger().error(
                        "[CMgmtNetworkHandler::OnNetworkMsg()] OnRcvMgmtMsg Exception - {0}\r\n{1}",
                        e.Message, e.StackTrace);
                }
            }
            else
            {
                CServerLog.GetLogger().etcinfo(
                    "[CMgmtNetworkHandler::OnNetworkMsg()] Invalid Packet First {0}",
                    cPacket.GetFirstClass());
            }
        }
        
        #endregion
    }
}