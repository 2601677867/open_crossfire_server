using System;
using System.Diagnostics;
using System.Net;
using cf_loginsrv.Config;
using cf_loginsrv.Log;
using cf_loginsrv.Socket;
using cf_loginsrv.Util;
using Commons.Log;
using Commons.Native;
using DBGWMGR;
using Network.HashMaker;
using Network.Packet;
using Network.Protocol;
using Network.ProtocolStruct;
using Network.ProtocolStruct.Login;
using Network.Server;
using Network.SharedFolder;

using static Network.Protocol.SV_PK;
using static Network.Protocol.MM_PK;
using static Network.Protocol.LG_PK;
using static Network.Protocol.P_SZ;
using static DBGWMGR.E_GDBGW_DEFS;
using static Network.Assert.ProtocolAssert;

namespace cf_loginsrv.Packet
{
    public partial class CGameNetworkHandler : CBaseNetworkHandler
    {
#if DEBUG
        private const string DEBUG_ADMIN_ID = "admin";
        private const string DEBUG_ADMIN_PASSWORD = "admin";
        private const long DEBUG_ADMIN_USN = -10001;
        private const int DEBUG_ADMIN_GAME_POINT = 90000;

        private bool TryProcessDebugLogin(CLGUserContext cContext, PROTO_REQUEST_CONNECT tReqConnect)
        {
            // DEBUG ONLY: accept ANY account/password without touching GDBGW.
            // The login packet has no dedicated username field, so the raw password
            // value is used as the displayed name.
            var szName = string.Empty;
            if (tReqConnect.tConnectInfo.m_szPassword != null)
                szName = NativeUtil.BArrToStr(tReqConnect.tConnectInfo.m_szPassword).Trim();
            if (string.IsNullOrEmpty(szName)) szName = DEBUG_ADMIN_ID;

            CServerLog.GetLogger().warn(
                "DEBUG MODE: login bypass accepted for ANY account/password (name: '{0}', IP: {1})",
                szName, cContext.IPAddress);

            cContext.tStatInfo.lUSN = DEBUG_ADMIN_USN;
            cContext.tStatInfo.szName = szName;
            cContext.tStatInfo.szNick = szName;
            cContext.tStatInfo.nGamePoint = DEBUG_ADMIN_GAME_POINT;

            var tConnectRet = new PROTO_REQUEST_CONNECT_RESULT();
            tConnectRet.m_eResult = CONNECTRESULT.CONNECT_SUCCESS;

            tConnectRet.tLoginInfo.m_lUSN = (int) DEBUG_ADMIN_USN;
            NativeUtil.strncpy(ref tConnectRet.tLoginInfo.m_szCallName, szName,
                SF_MAX_CHARACTER_NAME_LENGTH);
            tConnectRet.tLoginInfo.m_nLevel = 1;
            tConnectRet.tLoginInfo.m_dKillDeath = 1.0d;
            tConnectRet.tLoginInfo.m_byUseGlobalJoin = CServerConfig.GetUseGlobalJoin();

            tConnectRet.tLoginInfo.m_aServers = new PROTO_GAMESERVER[MAX_GAMESERVER_COUNT];
            tConnectRet.tLoginInfo.m_nServerCount = CopyServerList(
                ref tConnectRet.tLoginInfo.m_aServers, out _);

            if (tConnectRet.tLoginInfo.m_nServerCount == 0)
            {
                CServerLog.GetLogger().error(
                    "DEBUG MODE: WARNING - server list is EMPTY! Client will not show server selection.");
            }

            tConnectRet.tLoginInfo.m_lKey1 = NativeUtil.GetTickCount();
            tConnectRet.tLoginInfo.m_lKey2 = (int) CLoginHash.GetInstance().CalculateConnectHash(
                cContext.tStatInfo.lUSN,
                (uint)tConnectRet.tLoginInfo.m_lKey1,
                (uint)(tConnectRet.tLoginInfo.m_lKey1 >> 31));

            tConnectRet.tLoginInfo.m_iSSN = CServerConfig.GetSSN();
            tConnectRet.tLoginInfo.m_bySeason = 14;
            tConnectRet.tLoginInfo.m_byNewSeasonNty = 1;

            var aAddrBytes = cContext.IPAddress.GetAddressBytes();
            tConnectRet.tLoginInfo.m_iAddr =
                aAddrBytes[0] << 24 | aAddrBytes[1] << 16 | aAddrBytes[2] << 8 | aAddrBytes[3];

            CServerLog.GetLogger().warn(
                "DEBUG MODE: sending CONNECT_SUCCESS - serverCount={0}, USN={1}, SSN={2}, addr={3}",
                tConnectRet.tLoginInfo.m_nServerCount,
                tConnectRet.tLoginInfo.m_lUSN,
                tConnectRet.tLoginInfo.m_iSSN,
                cContext.IPAddress);

            Console.WriteLine($"[DIAG] TryProcessDebugLogin: serverCount={tConnectRet.tLoginInfo.m_nServerCount}, structSize={System.Runtime.InteropServices.Marshal.SizeOf(typeof(PROTO_REQUEST_CONNECT_RESULT))}");
            if (tConnectRet.tLoginInfo.m_nServerCount > 0)
            {
                var srv = tConnectRet.tLoginInfo.m_aServers[0];
                var nameStr = srv.m_szServerName != null ? System.Text.Encoding.ASCII.GetString(srv.m_szServerName).TrimEnd('\0') : "(null)";
                var nSrvIdx = srv.m_nServerID;
                var bConnected = nSrvIdx >= 0 && nSrvIdx < CMainServer.m_aGameServers.Length
                    && CMainServer.m_aGameServers[nSrvIdx].m_bGameServerConnected;
                Console.WriteLine($"[DIAG] Server[0]: ID={srv.m_nServerID}, Name='{nameStr}', IP={srv.m_dwServerAddr}, Port={srv.m_nServerPort}, HighProp={srv.m_nServerHighProperty}, Prop={srv.m_nProperty}, Connected={bConnected}");
            }

            var cPacket = new CPacket();
            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_CONNECT_RESULT);
            var bCopyOk = cPacket.CopyToUserDataArea(tConnectRet);
            Console.WriteLine($"[DIAG] CopyToUserDataArea: success={bCopyOk}, recordedSize={cPacket.GetRecordedSize()}, wSize={cPacket.GetReceivedSize()}");
            var bSendOk = CServer.SendMessage(cContext, cPacket);
            Console.WriteLine($"[DIAG] SendMessage: success={bSendOk}");

            return true;
        }
#endif

        #region REQ_CONNECT

        private double CalculateKillDeath(int iKill, int iDeath)
        {
            if (iDeath != 0)
                return (double) iKill / iDeath;
            return iKill / 0.9;
        }
        
        private void ProcessLogin(CLGUserContext cContext, PROTO_REQUEST_CONNECT tReqConnect)
        {
            cContext.tStatInfo.lUSN = tReqConnect.tConnectInfo.m_lUSN;

            var szRAS = NativeUtil.BArrToStr(tReqConnect.tConnectInfo.m_szRAS);
            if (szRAS.Length == 8)
            {
                cContext.tStatInfo.wAge = (short) (Convert.ToInt16(szRAS.Substring(0, 2)) - 1);
                cContext.tStatInfo.wRegion = Convert.ToInt16(szRAS.Substring(2, 1));
                cContext.tStatInfo.wSex = Convert.ToInt16(szRAS.Substring(3, 1));
                if (cContext.tStatInfo.wSex == 0)
                    cContext.tStatInfo.szRegID = $"{szRAS.Substring(4, 4)}M{cContext.tStatInfo.wSex:00}";
                else
                    cContext.tStatInfo.szRegID = $"{szRAS.Substring(4, 4)}F{cContext.tStatInfo.wSex:00}";
                cContext.tStatInfo.szPCBID = NativeUtil.BArrToStr(tReqConnect.tConnectInfo.m_szPCBID);
                
                cContext.tStatInfo.szLoginDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                cContext.tStatInfo.szDayPartKey = DateTime.Now.ToString("dd");
                //cContext.tStatInfo.szHGWKey = NativeUtil.BArrToStr(tReqConnect.tConnectInfo.m_szHGWKey);
                cContext.tStatInfo.tLoginInfo = tReqConnect;
                //cContext.tStatInfo.szName = NativeUtil.BArrToStr(tReqConnect.tConnectInfo.m_szName);
                cContext.tStatInfo.szName = tReqConnect.tConnectInfo.m_lUSN.ToString();
                var szPassword = NativeUtil.BArrToStr(tReqConnect.tConnectInfo.m_szPassword);

                if (!CServerConfig.GetUseLLSMgmt())
                {
                    CServerDataManager.AExecuteGameSP(
                        STORE_LOGIN_AUTH,
                        StoreAuthCallBackProc,
                        cContext,
                        cContext.tStatInfo.szName, 
                        szPassword);
                }
                else
                {
                    LLSAuthProc(cContext, NativeUtil.BArrToStr(tReqConnect.tConnectInfo.m_szPassword));
                }
            }
        }

        private void LLSAuthProc(CLGUserContext cContext, string szAuthKey)
        {
            var tDBLogin = new PROTO_DBLOGIN();
            tDBLogin.iClientKey = cContext.iClientKey;
            tDBLogin.m_lUSN = -1;
            
            if (CSharedVariable.LSKeys.TryGetValue(cContext.tStatInfo.lUSN, out var tKey) &&
                //tKey.szIPAddress == cContext.IPAddress.ToString() &&
                tKey.szKey == szAuthKey)
            {
                tKey.szIPAddress = cContext.IPAddress.ToString();
                tDBLogin.m_lUSN = cContext.tStatInfo.lUSN;
                tDBLogin.m_byReadPolicy = 1;
            }
            
            var cPacket = new CPacket();
            cPacket.SetFirstClass(PROTOCOL_LOGIN_SERVER_PUSH_PACKET);
            cPacket.SetSecondClass(PROTOCOL_DBLOGIN);
            cPacket.CopyToUserDataArea(tDBLogin);
            
            CSocketController.GetLoginServer().GetSocket().PushServerMessage(cPacket);
        }
        
        private void ProcessReconnect(CLGUserContext cContext, PROTO_REQUEST_RECONNECT tReqReconnect, 
            PROTO_REQUEST_RECONNECT_RESULT tReconnectRet)
        {
            cContext.tStatInfo.lUSN = tReqReconnect.tConnectInfo.m_lUSN;
            cContext.tStatInfo.bySupervisor = (byte)tReqReconnect.tReturnInfo.nSuperVisor;

            var szRAS = NativeUtil.BArrToStr(tReqReconnect.tConnectInfo.m_szRAS);
            cContext.tStatInfo.wAge = (short) (Convert.ToInt16(szRAS[0] + szRAS[1]) - 1);
            cContext.tStatInfo.wRegion = Convert.ToInt16(szRAS[2]);
            cContext.tStatInfo.wSex = Convert.ToInt16(szRAS[3]);
            if (cContext.tStatInfo.wSex == 0)
                cContext.tStatInfo.szRegID = $"{szRAS[4] + szRAS[5] + szRAS[6] + szRAS[7]}M{cContext.tStatInfo.wSex:00}";
            else
                cContext.tStatInfo.szRegID = $"{szRAS[4] + szRAS[5] + szRAS[6] + szRAS[7]}F{cContext.tStatInfo.wSex:00}";
            cContext.tStatInfo.szPCBID = NativeUtil.BArrToStr(tReqReconnect.tConnectInfo.m_szPCBID);

            cContext.tStatInfo.szLoginDateTime = tReqReconnect.tReturnInfo.szLoginDate;
            cContext.tStatInfo.szDayPartKey = tReqReconnect.tReturnInfo.szPartKeyDay;
            //cContext.tStatInfo.szHGWKey = NativeUtil.BArrToStr(tReqReconnect.tConnectInfo.m_szHGWKey);
            //cContext.tStatInfo.szName = NativeUtil.BArrToStr(tReqReconnect.tConnectInfo.m_szName);
            cContext.tStatInfo.szName = tReqReconnect.tConnectInfo.m_lUSN.ToString();

            if (!CServerConfig.GetServerValidCheck()
            || CHashMaker.CalculateMMServerReturnHash(tReqReconnect.tReturnInfo) == tReqReconnect.tReturnInfo.szHashedValue)
            {
                if (CheckDuplicate(LG_SERVER_NO, cContext.tStatInfo.lUSN, cContext.tStatInfo.dConnectTime))
                {
                    tReconnectRet.m_eResult = CONNECTRESULT.CONNECT_DUPLICATE;
                    tReconnectRet.tLoginInfo.m_nServerCount = 0;
                }
                else
                {
                    cContext.tStatInfo.bySupervisor = (byte)tReqReconnect.tReturnInfo.nSuperVisor;
                    cContext.tStatInfo.szClanSrl = tReqReconnect.tReturnInfo.szClanSrl;
                    cContext.tStatInfo.szClanID = tReqReconnect.tReturnInfo.szClanID;
                    cContext.tStatInfo.szClanUserSrl = tReqReconnect.tReturnInfo.szClanUserSrl;
                    cContext.tStatInfo.nTodayGamePoint = tReqReconnect.tReturnInfo.nTodayGP;
                    cContext.tStatInfo.nTodayEXP = tReqReconnect.tReturnInfo.nTodayEPAmount;
                    cContext.tStatInfo.nLevel = (short)tReqReconnect.tReturnInfo.nLev;
                    cContext.tStatInfo.nEnemyKill = tReqReconnect.tReturnInfo.nKill;
                    cContext.tStatInfo.nDeathCount = tReqReconnect.tReturnInfo.nDeath;
                    cContext.tStatInfo.nTodayPlay = tReqReconnect.tReturnInfo.nTodayPlayCnt;
                    cContext.tStatInfo.nTodayWin = tReqReconnect.tReturnInfo.nTodayWin;
                    cContext.tStatInfo.nTodayLose = tReqReconnect.tReturnInfo.nTodayLose;
                    cContext.tStatInfo.nTodayDraw = tReqReconnect.tReturnInfo.nTodayDraw;
                    cContext.tStatInfo.nTodayKill = tReqReconnect.tReturnInfo.nTodayKillCnt;
                    cContext.tStatInfo.nTodayDeath = tReqReconnect.tReturnInfo.nTodayDeathCnt;
                    cContext.tStatInfo.nTodayHeadshot = tReqReconnect.tReturnInfo.nTodayHeadShotCnt;
                    cContext.tStatInfo.nGamePoint = tReqReconnect.tReturnInfo.nGP;
                    cContext.tStatInfo.lExp = tReqReconnect.tReturnInfo.lEP;
                    cContext.tStatInfo.lClanKey = tReqReconnect.tReturnInfo.ClanKey;
                    cContext.tStatInfo.byClanUserGrade = tReqReconnect.tReturnInfo.ClanGrade;
                    cContext.tStatInfo.byWaveLevel = tReqReconnect.tReturnInfo.byWaveLevel;
                    cContext.tStatInfo.dConnectTime = tReqReconnect.tReturnInfo.dConnectTime;
                    cContext.tStatInfo.sClanLLevel = tReqReconnect.tReturnInfo.nClanLLevel;
                    //cContext.tStatInfo.bySupervisor = tReqReconnect.tReturnInfo.nClanSuperVisor;

                    tReconnectRet.tLoginInfo.m_lUSN = (int) cContext.tStatInfo.lUSN;
                    tReconnectRet.tLoginInfo.m_byClanMember = cContext.tStatInfo.byClanMember;
                    tReconnectRet.tLoginInfo.m_bySupervisor = cContext.tStatInfo.bySupervisor;
                    tReconnectRet.tLoginInfo.m_nLevel = cContext.tStatInfo.nLevel;
                    tReconnectRet.tLoginInfo.m_nKill = cContext.tStatInfo.nEnemyKill;
                    tReconnectRet.tLoginInfo.m_nDeath = cContext.tStatInfo.nDeathCount;
                    tReconnectRet.tLoginInfo.m_byWaveLevel = cContext.tStatInfo.byWaveLevel;

                    if (tReconnectRet.tLoginInfo.m_nDeath > 0)
                    {
                        if (tReconnectRet.tLoginInfo.m_nKill + tReconnectRet.tLoginInfo.m_nDeath >= 100)
                            tReconnectRet.tLoginInfo.m_dKillDeath =
                                (double) tReconnectRet.tLoginInfo.m_nKill / tReconnectRet.tLoginInfo.m_nDeath;
                        else
                            tReconnectRet.tLoginInfo.m_dKillDeath = 1.0d;
                    }
                    else if (tReconnectRet.tLoginInfo.m_nKill + 0 >= 100)
                    {
                        tReconnectRet.tLoginInfo.m_dKillDeath = tReconnectRet.tLoginInfo.m_nKill / 0.9;
                    }
                    else
                    {
                        tReconnectRet.tLoginInfo.m_dKillDeath = 1.0d;
                    }

                    tReconnectRet.tLoginInfo.m_aServers = new PROTO_GAMESERVER[MAX_GAMESERVER_COUNT];
                    tReconnectRet.tLoginInfo.m_nServerCount = CopyServerList(
                        ref tReconnectRet.tLoginInfo.m_aServers, out var byAutoSelectID);
                    //tReconnectRet.tLoginInfo.m_byAutoServerSelectID = byAutoSelectID;
                }

                tReconnectRet.tLoginInfo.m_lKey1 = (int) tReqReconnect.tConnectInfo.m_lKey1;
                tReconnectRet.tLoginInfo.m_lKey2 = (int) tReqReconnect.tConnectInfo.m_lKey2;
                //tReconnectRet.tLoginInfo.tCryptedData = tReqReconnect.tCryptedData;
            }
            else
            {
                tReconnectRet.m_eResult = CONNECTRESULT.CONNECT_INVALID_USERNAME_OR_PASSWORD;
            }
            
            tReconnectRet.tLoginInfo.m_byUseGlobalJoin = CServerConfig.GetUseGlobalJoin();
            //tReconnectRet.tLoginInfo.m_byReadNewPolicy = 1;

            var cPacket = new CPacket();
            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_RECONNECT_RESULT);
            cPacket.CopyToUserDataArea(tReconnectRet);
            CServer.SendMessage(cContext, cPacket);
            
            if (tReconnectRet.m_eResult == CONNECTRESULT.CONNECT_SUCCESS)
            {
                cContext.tStatInfo.bReconnected = true;
                UpdateUserLocation(LG_SERVER_NO, cContext.tStatInfo.lUSN, cContext.tStatInfo.dConnectTime);
                // call(age, region, sex, szPCBID)
            }
            else
            {
                if (tReconnectRet.m_eResult == CONNECTRESULT.CONNECT_DUPLICATE)
                    cContext.tStatInfo.lReqUSN = cContext.tStatInfo.lUSN;
                cContext.tStatInfo.lUSN = -1;
            }
        }

        private void ProcessReconnectForDist(CLGUserContext cContext, 
            PROTO_REQUEST_RECONNECT_FOR_DIST tReqReconnectForDist)
        {
            cContext.tStatInfo.lUSN = tReqReconnectForDist.tConnectInfo.m_lUSN;

            var szRAS = NativeUtil.BArrToStr(tReqReconnectForDist.tConnectInfo.m_szRAS);
            if (szRAS.Length == 8)
            {
                cContext.tStatInfo.wAge = (short) (Convert.ToInt16(szRAS[0] + szRAS[1]) - 1);
                cContext.tStatInfo.wRegion = Convert.ToInt16(szRAS[2]);
                cContext.tStatInfo.wSex = Convert.ToInt16(szRAS[3]);
                if (cContext.tStatInfo.wSex == 0)
                    cContext.tStatInfo.szRegID =
                        $"{szRAS[4] + szRAS[5] + szRAS[6] + szRAS[7]}M{cContext.tStatInfo.wSex:00}";
                else
                    cContext.tStatInfo.szRegID =
                        $"{szRAS[4] + szRAS[5] + szRAS[6] + szRAS[7]}F{cContext.tStatInfo.wSex:00}";
                cContext.tStatInfo.szPCBID = NativeUtil.BArrToStr(tReqReconnectForDist.tConnectInfo.m_szPCBID);

                cContext.tStatInfo.szLoginDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                cContext.tStatInfo.szDayPartKey = DateTime.Now.ToString("dd");
                cContext.tStatInfo.szName = tReqReconnectForDist.tConnectInfo.m_lUSN.ToString();

                var tReconnectForDistRet = new PROTO_REQUEST_RECONNECT_FOR_DIST_RESULT();
                if (CheckDuplicate(LG_SERVER_NO, cContext.tStatInfo.lUSN, cContext.tStatInfo.dConnectTime))
                {
                    tReconnectForDistRet.m_eResult = CONNECTRESULT.CONNECT_DUPLICATE;
                    tReconnectForDistRet.tLoginInfo.m_nServerCount = 0;
                }
                else
                {
                    switch (CServerDataManager.GetUserDataFromDBServer_ExecuteQuery(cContext, cContext.tStatInfo.lUSN))
                    {
                        case GETUSERDATA_RESULT.UNKNOWNERROR:
                            tReconnectForDistRet.m_eResult = CONNECTRESULT.CONNECT_UNKNOWNERROR;
                            break;
                        case GETUSERDATA_RESULT.NO_ACCESS:
                            tReconnectForDistRet.m_eResult = CONNECTRESULT.CONNECT_NO_ACCESS;
                            break;
                        case GETUSERDATA_RESULT.NO_USERDATA:
                        {
                            tReconnectForDistRet.m_eResult = CONNECTRESULT.CONNECT_UNKNOWNERROR;
                            tReconnectForDistRet.tLoginInfo.m_dKillDeath = 1.0d;
                            CSocketController.GetLoginServer().GetSocket().CloseClient(cContext);

                            break;
                        }
                        case GETUSERDATA_RESULT.BANNED:
                            tReconnectForDistRet.m_eResult = CONNECTRESULT.CONNECT_BANNED;
                            tReconnectForDistRet.tLoginInfo.m_nBannedTime =
                                cContext.tStatInfo.nConnectDenyUDate - cContext.tStatInfo.nLoginUDate;
                            break;
                        case GETUSERDATA_RESULT.FOREVER_BANNED:
                            tReconnectForDistRet.m_eResult = CONNECTRESULT.CONNECT_FOREVER_BANNED;
                            break;
                        default:
                        {
                            GetClanDataFromDBServerExecuteQuery(cContext);

                            tReconnectForDistRet.tLoginInfo.m_byClanMember = cContext.tStatInfo.byClanMember;
                            tReconnectForDistRet.tLoginInfo.m_bySupervisor = cContext.tStatInfo.bySupervisor;
                            tReconnectForDistRet.tLoginInfo.m_nLevel = cContext.tStatInfo.nLevel;
                            tReconnectForDistRet.tLoginInfo.m_nKill = cContext.tStatInfo.nEnemyKill;
                            tReconnectForDistRet.tLoginInfo.m_nDeath = cContext.tStatInfo.nDeathCount;
                            tReconnectForDistRet.tLoginInfo.m_byWaveLevel = cContext.tStatInfo.byWaveLevel;

                            if (tReconnectForDistRet.tLoginInfo.m_nKill + tReconnectForDistRet.tLoginInfo.m_nDeath >=
                                100)
                            {
                                tReconnectForDistRet.tLoginInfo.m_dKillDeath = CalculateKillDeath(
                                    tReconnectForDistRet.tLoginInfo.m_nKill, tReconnectForDistRet.tLoginInfo.m_nDeath);
                            }
                            else
                            {
                                tReconnectForDistRet.tLoginInfo.m_dKillDeath = 1.0d;
                            }

                            tReconnectForDistRet.tLoginInfo.m_aServers = new PROTO_GAMESERVER[MAX_GAMESERVER_COUNT];
                            tReconnectForDistRet.tLoginInfo.m_nServerCount =
                                CopyServerList(ref tReconnectForDistRet.tLoginInfo.m_aServers, out var byAutoSelectID);
                            //tReconnectForDistRet.tLoginInfo.m_byAutoServerSelectID = byAutoSelectID;

                            break;
                        }
                    }

                    tReconnectForDistRet.tLoginInfo.m_lUSN = (int) tReqReconnectForDist.tConnectInfo.m_lUSN;
                    tReconnectForDistRet.tLoginInfo.m_lKey1 = (int) tReqReconnectForDist.tConnectInfo.m_lKey1;
                    tReconnectForDistRet.tLoginInfo.m_lKey2 = (int) tReqReconnectForDist.tConnectInfo.m_lKey2;
                    
                    tReconnectForDistRet.tLoginInfo.m_byUseGlobalJoin = CServerConfig.GetUseGlobalJoin();
                    //tReconnectForDistRet.tLoginInfo.m_byReadNewPolicy = 1;

                    var cPacket = new CPacket();
                    cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
                    cPacket.SetSecondClass(PROTOCOL_REQUEST_RECONNECT_FOR_DIST_RESULT);
                    cPacket.CopyToUserDataArea(tReconnectForDistRet);
                    CServer.SendMessage(cContext, cPacket);

                    if (tReconnectForDistRet.m_eResult == CONNECTRESULT.CONNECT_SUCCESS)
                    {
                        cContext.tStatInfo.bReconnected = true;
                        UpdateUserLocation(LG_SERVER_NO, cContext.tStatInfo.lUSN, cContext.tStatInfo.dConnectTime);
                        // call(age, region, sex, szPCBID)
                    }
                    else
                    {
                        if (tReconnectForDistRet.m_eResult == CONNECTRESULT.CONNECT_DUPLICATE)
                            cContext.tStatInfo.lReqUSN = cContext.tStatInfo.lUSN;
                        cContext.tStatInfo.lUSN = -1;
                    }
                }
            }
        }

        private void StoreAuthCallBackProc(object objPassThruParam, CGDBGWParser cParser, int nErrCode)
        {
            var cContext = (CLGUserContext) objPassThruParam;
            
            if (nErrCode != 0)
            {
                CServerLog.GetLogger().error(
                    "StoreAuthCallBackProc RESULT ERROR : {0}",
                    "DBRESULTSTRING");
                return;
            }

            var tDBLogin = new PROTO_DBLOGIN();
            tDBLogin.iClientKey = cContext.iClientKey;

            if (cParser.Result == 1)
            {
                tDBLogin.m_lUSN = cParser.GetLong(1);
                tDBLogin.m_byReadPolicy  = cParser.GetByte(2);
                if (tDBLogin.m_lUSN < 0) tDBLogin.m_lUSN = -1;
            }
            else
            {
                tDBLogin.m_lUSN = -1;
            }

            var cPacket = new CPacket();
            cPacket.SetFirstClass(PROTOCOL_LOGIN_SERVER_PUSH_PACKET);
            cPacket.SetSecondClass(PROTOCOL_DBLOGIN);
            cPacket.CopyToUserDataArea(tDBLogin);
            
            CSocketController.GetLoginServer().GetSocket().PushServerMessage(cPacket);
        }
        
        #endregion
        
        public int GetClanDataFromDBServerExecuteQuery(CLGUserContext cContext)
        {
            var bSuccess = CServerDataManager.ExecuteGuildSP(
                STORE_GET_CLAN_BASEINFO, out var cParser,
                cContext.tStatInfo.lUSN);
            
            if (!bSuccess)
            {
                CServerLog.GetLogger().error(
                    "CGameNetworkHandler::GetClanDataFromDBServer : ExecuteQuery Failed : usn={0}, query={1}",
                    cContext.tStatInfo.lUSN, 
                    CServerDataManager.GetQueryString(STORE_GET_CLAN_BASEINFO));
                return -1;
            }

            if (cParser.Result != 1)
            {
                CServerLog.GetLogger().error(
                    "CGameNetworkHandler::GetClanDataFromDBServerExecuteQuery succeed but query failed : usn={0}, query={1}",
                    cContext.tStatInfo.lUSN, 
                    CServerDataManager.GetQueryString(STORE_GET_CLAN_BASEINFO));
                return -3;
            }

            cContext.tStatInfo.lClanKey = cParser.GetLong(1);
            cContext.tStatInfo.byClanUserGrade = cParser.GetByte(2);
            cContext.tStatInfo.sClanLLevel = cContext.tStatInfo.byClanUserGrade;

            //cContext.tStatInfo.tReturnInfo.nClanLLevel = cContext.tStatInfo.byClanUserGrade;
            //cContext.tStatInfo.nClanLLevel = cContext.tStatInfo.byClanUserGrade;
            if (cContext.tStatInfo.byClanUserGrade != 1)
            {
                cContext.tStatInfo.byClanMember = 1;
                cContext.tStatInfo.szClanID = cContext.tStatInfo.lClanKey.ToString();
                cContext.tStatInfo.szClanSrl = cContext.tStatInfo.lClanKey.ToString();
                cContext.tStatInfo.szClanUserSrl = cContext.tStatInfo.lClanKey.ToString();
            }
            
            return 0;
        }

        #region LocalMsgHandler
        
        private int CheckIPBlock(CLGUserContext cContext, IPAddress ipAddr, long lUSN)
        {
            var aAddrBytes = ipAddr.GetAddressBytes();
            var iAddr = aAddrBytes[0] << 24 | aAddrBytes[1] << 16 | aAddrBytes[2] << 8 | aAddrBytes[3];
            var bSuccess = CServerDataManager.ExecuteGameSP(
                STORE_CHECK_IP_BLOCK, out var cParser,
                iAddr, lUSN);
            
            if (bSuccess)
            {
                if (cParser.Result == 1)
                {
                    SendLoginIPBlockResult(cContext);
                    CServerLog.GetLogger().info("Block User(USN : {0}) Connect : IP ({1}), ({2})", 
                        lUSN, ipAddr.ToString(), cParser.Result);

                    return 1;
                }
            }
            else
            {
                CServerLog.GetLogger().error("[CheckIPBlock - DBQUERYFAILED() Error]");
            }
            return 0;
        }
        
        private void OnLocalDBLogin(CPacket cPacket)
        {
            PROTO_DBLOGIN tDBLogin = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tDBLogin) );
            
            var cContext = CSocketController.GetLoginServer().GetSocketContext(tDBLogin.iClientKey);
            if (cContext == null)
            {
                CServerLog.GetLogger().error("OnLocalDBLogin - cContext is NULL!");
                return;
            }

            if (NativeUtil.strcmp(cContext.tStatInfo.tLoginInfo.tConnectInfo.m_szPassword, NativeUtil.NULL) == 0)
                return;
            
            var tConnectRet = new PROTO_REQUEST_CONNECT_RESULT();
            
            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_CONNECT_RESULT);
            
            if (tDBLogin.m_lUSN == -1)
            {
                CServerLog.GetLogger().info("[DB Login failed][ID: {0}][Result: {1}]", 
                    cContext.tStatInfo.szName, tDBLogin.m_lUSN);
                tConnectRet.m_eResult = CONNECTRESULT.CONNECT_INVALID_USERNAME_OR_PASSWORD;
            }
            else
            {
                if (CheckIPBlock(cContext, cContext.IPAddress, tDBLogin.m_lUSN) == 1) return;
                
                tConnectRet.tLoginInfo.m_lUSN = (int) tDBLogin.m_lUSN;
                cContext.tStatInfo.lUSN = tDBLogin.m_lUSN;
                cContext.tStatInfo.tLoginInfo.tConnectInfo.m_lUSN = tDBLogin.m_lUSN;
                cContext.tStatInfo.wRegion = 30;

                if (CheckDuplicate(LG_SERVER_NO, cContext.tStatInfo.lUSN, cContext.tStatInfo.dConnectTime))
                {
                    tConnectRet.m_eResult = CONNECTRESULT.CONNECT_DUPLICATE;
                    tConnectRet.tLoginInfo.m_nServerCount = 0;
                }
                else
                {
                    switch (CServerDataManager.GetUserDataFromDBServer_ExecuteQuery(cContext, cContext.tStatInfo.lUSN))
                    {
                        case GETUSERDATA_RESULT.UNKNOWNERROR:
                            tConnectRet.m_eResult = CONNECTRESULT.CONNECT_UNKNOWNERROR;
                            break;
                        case GETUSERDATA_RESULT.NO_ACCESS:
                            tConnectRet.m_eResult = CONNECTRESULT.CONNECT_NO_ACCESS;
                            break;
                        case GETUSERDATA_RESULT.NO_USERDATA:
                        {
                            tConnectRet.m_eResult = CONNECTRESULT.CONNECT_CREATE_CHARACTER;
                            tConnectRet.tLoginInfo.m_dKillDeath = 1.0d;
                            cContext.tStatInfo.nGamePoint = 90000;
                            
                            tConnectRet.tLoginInfo.m_aServers = new PROTO_GAMESERVER[MAX_GAMESERVER_COUNT];
                            tConnectRet.tLoginInfo.m_nServerCount = 
                                CopyServerList(ref tConnectRet.tLoginInfo.m_aServers, out var byAutoSelectID);
                            //tConnectRet.tLoginInfo.m_byAutoServerSelectID = byAutoSelectID;
                            
                            tConnectRet.tLoginInfo.m_lKey1 = NativeUtil.GetTickCount();
                            tConnectRet.tLoginInfo.m_lKey2 = (int) CLoginHash.GetInstance().CalculateConnectHash(
                                cContext.tStatInfo.lUSN,
                                (uint) tConnectRet.tLoginInfo.m_lKey1,
                                (uint) (tConnectRet.tLoginInfo.m_lKey1 >> 31));
                            // MMCONNECT included first
                            
                            //tConnectRet.tLoginInfo.m_nRankMatchFlag = 12;
                            
                            tConnectRet.tLoginInfo.m_iSSN = 318;
                            tConnectRet.tLoginInfo.m_sInfinityAIEvent = 0;
                            tConnectRet.tLoginInfo.m_bySeason = 14;
                            tConnectRet.tLoginInfo.m_byNewSeasonNty = 1;
                            
                            var aAddrBytes = cContext.IPAddress.GetAddressBytes();
                            tConnectRet.tLoginInfo.m_iAddr = 
                                aAddrBytes[0] << 24 | aAddrBytes[1] << 16 | aAddrBytes[2] << 8 | aAddrBytes[3];
                            
                            CServerDataManager.RegisterAuthKey_ExecuteQuery(cContext.tStatInfo.lUSN, 
                                CBase64.Encode(tConnectRet.tLoginInfo.m_lKey1.ToString() + tConnectRet.tLoginInfo.m_lKey2),
                                cContext.IPAddress.ToString());
                            
                            /*var tHashData = new CONNECT_HASH_DATA
                            {
                                m_iKey = GetLoginCryptedDataKey(),
                                m_nResult = 0,
                                m_lUSN = cContext.tStatInfo.lUSN,
                                m_lClock = Environment.TickCount
                            };
                            tHashData.m_lHash = CalculateConnectHash(tHashData.m_lUSN,
                                (uint) tHashData.m_lClock, (uint) (tHashData.m_lClock >> 31));
                            tConnectRet.tLoginInfo.tCryptedData.m_aCryptedData = StructureToByteArr(tHashData);
                            EncryptConnectHashData(ref tConnectRet.tLoginInfo.tCryptedData.m_aCryptedData,
                                GetKeyByteArr((ulong)tHashData.m_iKey));
                            
                            RegisterAuthKey_ExecuteQuery(cContext.tStatInfo.lUSN, 
                                CBase64.Encode(tConnectRet.tLoginInfo.tCryptedData.m_aCryptedData),
                                cContext.IPAddress.ToString());*/
                            
                            //SendProtectedKeyPacket(cContext);

                            break;
                        }
                        case GETUSERDATA_RESULT.BANNED:
                            tConnectRet.m_eResult = CONNECTRESULT.CONNECT_BANNED;
                            tConnectRet.tLoginInfo.m_nBannedTime = 
                                cContext.tStatInfo.nConnectDenyUDate - cContext.tStatInfo.nLoginUDate;
                            break;
                        case GETUSERDATA_RESULT.FOREVER_BANNED:
                            tConnectRet.m_eResult = CONNECTRESULT.CONNECT_FOREVER_BANNED;
                            break;
                        default:
                        {
                            GetClanDataFromDBServerExecuteQuery(cContext);

                            tConnectRet.tLoginInfo.m_byClanMember = cContext.tStatInfo.byClanMember;
                            tConnectRet.tLoginInfo.m_bySupervisor = cContext.tStatInfo.bySupervisor;
                            tConnectRet.tLoginInfo.m_nLevel = cContext.tStatInfo.nLevel;
                            tConnectRet.tLoginInfo.m_nKill = cContext.tStatInfo.nEnemyKill;
                            tConnectRet.tLoginInfo.m_nDeath = cContext.tStatInfo.nDeathCount;
                            tConnectRet.tLoginInfo.m_byWaveLevel = cContext.tStatInfo.byWaveLevel;
                            NativeUtil.strncpy(ref tConnectRet.tLoginInfo.m_szCallName,
                                cContext.tStatInfo.szNick, SF_MAX_CHARACTER_NAME_LENGTH);

                            if (tConnectRet.tLoginInfo.m_nDeath > 0)
                            {
                                if (tConnectRet.tLoginInfo.m_nKill + tConnectRet.tLoginInfo.m_nDeath >= 100)
                                    tConnectRet.tLoginInfo.m_dKillDeath =
                                        (double) tConnectRet.tLoginInfo.m_nKill / tConnectRet.tLoginInfo.m_nDeath;
                                else
                                    tConnectRet.tLoginInfo.m_dKillDeath = 1.0d;
                            }
                            else if (tConnectRet.tLoginInfo.m_nKill + 0 >= 100)
                            {
                                tConnectRet.tLoginInfo.m_dKillDeath = tConnectRet.tLoginInfo.m_nKill / 0.9;
                            }
                            else
                            {
                                tConnectRet.tLoginInfo.m_dKillDeath = 1.0d;
                            }

                            tConnectRet.tLoginInfo.m_aServers = new PROTO_GAMESERVER[MAX_GAMESERVER_COUNT];
                            tConnectRet.tLoginInfo.m_nServerCount = 
                                CopyServerList(ref tConnectRet.tLoginInfo.m_aServers, out var byAutoSelectID);
                            //tConnectRet.tLoginInfo.m_byAutoServerSelectID = byAutoSelectID;
                            //tConnectRet.tLoginInfo.m_byUseAutoServerSelect = 1;
                            
                            //tConnectRet.tLoginInfo.tCryptedData.iKey = GetLoginCryptedDataKey();
                            /*var tHashData = new CONNECT_HASH_DATA
                            {
                                m_iKey = GetLoginCryptedDataKey(),
                                m_nResult = 0,
                                m_lUSN = cContext.tStatInfo.lUSN,
                                m_lClock = Environment.TickCount
                            };
                            tHashData.m_lHash = CalculateConnectHash(tHashData.m_lUSN,
                                (uint) tHashData.m_lClock, (uint) (tHashData.m_lClock >> 31));
                            tConnectRet.tLoginInfo.tCryptedData.m_aCryptedData = StructureToByteArr(tHashData);
                            EncryptConnectHashData(ref tConnectRet.tLoginInfo.tCryptedData.m_aCryptedData,
                                GetKeyByteArr((ulong)tHashData.m_iKey));
                            
                            RegisterAuthKey_ExecuteQuery(cContext.tStatInfo.lUSN, 
                                CBase64.Encode(tConnectRet.tLoginInfo.tCryptedData.m_aCryptedData),
                                cContext.IPAddress.ToString());*/

                            //SendProtectedKeyPacket(cContext);
                            tConnectRet.tLoginInfo.m_lKey1 = NativeUtil.GetTickCount();
                            // MMCONNECT included first
                            tConnectRet.tLoginInfo.m_lKey2 = (int) CLoginHash.GetInstance().CalculateConnectHash(
                                cContext.tStatInfo.lUSN,
                                (uint) tConnectRet.tLoginInfo.m_lKey1,
                                (uint) (tConnectRet.tLoginInfo.m_lKey1 >> 31));
                            
                            //tConnectRet.tLoginInfo.m_nRankMatchFlag = 12;
                            
                            tConnectRet.tLoginInfo.m_iSSN = 318;
                            tConnectRet.tLoginInfo.m_sInfinityAIEvent = 0;
                            tConnectRet.tLoginInfo.m_bySeason = 14;
                            tConnectRet.tLoginInfo.m_byNewSeasonNty = 1;
                            
                            var aAddrBytes = cContext.IPAddress.GetAddressBytes();
                            tConnectRet.tLoginInfo.m_iAddr = 
                                aAddrBytes[0] << 24 | aAddrBytes[1] << 16 | aAddrBytes[2] << 8 | aAddrBytes[3];
                            
                            CServerDataManager.RegisterAuthKey_ExecuteQuery(cContext.tStatInfo.lUSN, 
                                CBase64.Encode(tConnectRet.tLoginInfo.m_lKey1.ToString() + tConnectRet.tLoginInfo.m_lKey2),
                                cContext.IPAddress.ToString());
                            
                            break;
                        }
                    }
                }
                
                //tConnectRet.tLoginInfo.m_byReadNewPolicy = tDBLogin.m_byReadPolicy;
            }
            
            tConnectRet.tLoginInfo.m_byUseGlobalJoin = CServerConfig.GetUseGlobalJoin();
            
            cPacket.CopyToUserDataArea(tConnectRet);
            CServer.SendMessage(cContext, cPacket);

            if (tConnectRet.m_eResult == CONNECTRESULT.CONNECT_CREATE_CHARACTER)
            {
                cContext.tStatInfo.nSelectServerLimitTime = -200; // limit 6
                cContext.tStatInfo.nCreateCharLimitTime = 0; // limit 21
                cContext.tStatInfo.bCharacterCreated = false;
                // call(age, region, sex, szPCBID)
            }
            else if (tConnectRet.m_eResult == CONNECTRESULT.CONNECT_SUCCESS)
            {
                UpdateUserLocation(LG_SERVER_NO, cContext.tStatInfo.lUSN, cContext.tStatInfo.dConnectTime);
                CServerDataManager.UpdateUserLoginDB_ExecuteQuery(cContext);
                // call(age, region, sex, szPCBID)
            }
            else
            {
                if (tConnectRet.m_eResult == CONNECTRESULT.CONNECT_DUPLICATE)
                    cContext.tStatInfo.lReqUSN = cContext.tStatInfo.lUSN;
                cContext.tStatInfo.lUSN = -1;
            }
        }
        
        #endregion

        #region ServerPushMsgHandler

        private string OnRcvServerPushMsg(CPacket cPacket)
        {
            switch (cPacket.GetSecondClass())
            {
                case PROTOCOL_DBLOGIN:
                    OnLocalDBLogin(cPacket);
                    return "DBLOGIN";
            }

            return null;
        }
        
        #endregion
        
        #region Handler
        
        private void SendProtectedKeyPacket(CLGUserContext token)
        {
            try
            {
                var cPacket = new CPacket();
                cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
                cPacket.SetSecondClass(PROTOCOL_PROTECTED_KEY);

                var tProtoProtectedKey = new PROTO_PROTECTED_KEY();
                tProtoProtectedKey.m_aProtectedKeys = new int[MAX_PROTECTED_KEYS];
                tProtoProtectedKey.m_aProtectedKeyTypes = new int[MAX_PROTECTED_KEYS];
                
                for (var i = 0; i < CProtectedKey.GetInstance().GetProtectedKeyCount(); i++)
                {
                    var tSecureCode = CProtectedKey.GetInstance().GetProtectedKey(i);
                    tProtoProtectedKey.m_aProtectedKeys[i] = tSecureCode.m_iKey;
                    tProtoProtectedKey.m_aProtectedKeyTypes[i] = (int) tSecureCode.m_eType;
                }

                cPacket.CopyToUserDataArea(tProtoProtectedKey);

                CServer.SendMessage(token, cPacket);
            }
            catch (Exception e)
            {
                CServerLog.GetLogger().error(e);
            }
        }
        
        private void SendLoginIPBlockResult(CLGUserContext cContext)
        {
            var tConnectRet = new PROTO_REQUEST_CONNECT_RESULT();
            
            var cPacket = new CPacket();
            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_CONNECT_RESULT);
            
            tConnectRet.m_eResult = CONNECTRESULT.CONNECT_IP_BLOCKED;
            
            cPacket.CopyToUserDataArea(tConnectRet);
            
            CServer.SendMessage(cContext, cPacket);
        }
        
        private void OnRequestLogin(CLGUserContext cContext, CPacket cPacket)
        {
            if (cContext.tStatInfo.bReconnected) return;

            /*var aNetworkData = cPacket.GetNetworkData();
            var aCryptedData = new byte[aNetworkData.Length - 5];
            Array.Copy(aNetworkData, 5, aCryptedData,
                0, aNetworkData.Length - 5);
            var aDecryptedData = KISA_SEED_ECB.PH_Login_Decrypt(aCryptedData);*/

            //var aDecryptedData = cPacket.GetNetworkData();
            
            //CServerLog.GetLogger().packet_trace(CServer.ByteArr2Hex(aDecryptedData));

            //var tReqConnect = ByteArrayToClass<LG_PK.PROTO_REQUEST_CONNECT>(aDecryptedData);
            
            PROTO_REQUEST_CONNECT tReqConnect = default;
            //PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tReqConnect) );
            
            cPacket.CopyFromUserDataArea(ref tReqConnect);
            var tConnectRet = new PROTO_REQUEST_CONNECT_RESULT();
            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_CONNECT_RESULT);

#if DEBUG
            // DEBUG ONLY: any account/password succeeds immediately without touching GDBGW.
            if (TryProcessDebugLogin(cContext, tReqConnect)) return;
#endif
            
            var iBanTime = CServerDataManager.GetUserBanTime(tReqConnect.tConnectInfo.m_lUSN);
            if (iBanTime != 0)
            {
                if (iBanTime == -1) tConnectRet.m_eResult = CONNECTRESULT.CONNECT_FOREVER_BANNED;
                else
                {
                    tConnectRet.m_eResult = CONNECTRESULT.CONNECT_BANNED;
                    tConnectRet.tLoginInfo.m_nBannedTime = iBanTime - NativeUtil.GetTimestamp();
                }
                cPacket.CopyToUserDataArea(tConnectRet);
                cContext.tStatInfo.lUSN = -1;
                CServer.SendMessage(cContext, cPacket);
                return;
            }

            if (tReqConnect.tConnectInfo.m_lUSN != -1)
            {
                cContext.tStatInfo.nLoginUDate = NativeUtil.GetTimestamp();
                cContext.tStatInfo.dConnectTime = NativeUtil.GetTickCount();
                ProcessLogin(cContext, tReqConnect);
                return;
            }

            tConnectRet.m_eResult = CONNECTRESULT.CONNECT_INVALID_USERNAME_OR_PASSWORD;
            
            cPacket.CopyToUserDataArea(tConnectRet);
            CServer.SendMessage(cContext, cPacket);
        }

        private void OnRequestReconnect(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_REQUEST_RECONNECT tReconnectReq = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tReconnectReq) );
            
            var tReconnectRet = new PROTO_REQUEST_RECONNECT_RESULT();
            
            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_RECONNECT_RESULT);
            
            //CServerLog.GetLogger().info("Reconnect Auth {0} {1} | Calculated {2}",
            //    tReconnectReq.tConnectInfo.m_lKey1, tReconnectReq.tConnectInfo.m_lKey2,
            //    CalculateConnectHash(tReconnectReq.tConnectInfo.m_lUSN,
            //        (uint) tReconnectReq.tConnectInfo.m_lKey1, (uint) (tReconnectReq.tConnectInfo.m_lKey1 >> 31)));

            /*var aCryptedData = tReconnectReq.tCryptedData.m_aCryptedData.ToArray();
            DecryptConnectHashData(ref aCryptedData);
            CONNECT_HASH_DATA tConnectHashData;

            unsafe
            {
                fixed (byte* ptr = &aCryptedData[0])
                {
                    tConnectHashData = (CONNECT_HASH_DATA)Marshal.PtrToStructure((IntPtr)ptr, typeof(CONNECT_HASH_DATA));
                }
            }

            if (tConnectHashData.m_lUSN != tReconnectReq.tConnectInfo.m_lUSN
                || tConnectHashData.m_lHash != CalculateConnectHash(tConnectHashData.m_lUSN,
                    (uint) tConnectHashData.m_lClock, (uint) (tConnectHashData.m_lClock >> 31)))
            {
                tReconnectRet.m_eResult = CONNECTRESULT.CONNECT_INVALID_USERNAME_OR_PASSWORD;
                cPacket.CopyDataToUserArea(tReconnectRet);
                CServer.SendMessage(cContext, cPacket);
                break;
            }*/

            if (tReconnectReq.tConnectInfo.m_lUSN == -1)
            {
                tReconnectRet.m_eResult = CONNECTRESULT.CONNECT_INVALID_USERNAME_OR_PASSWORD;
                cPacket.CopyToUserDataArea(tReconnectRet);
                CServer.SendMessage(cContext, cPacket);
                return;
            }
            
            var bValidLogin = false;
            if (CServerConfig.GetUseLLSMgmt())
            {
                if (CSharedVariable.LSKeys.TryGetValue(tReconnectReq.tConnectInfo.m_lUSN, out var tKey) &&
                    tKey.szIPAddress == cContext.IPAddress.ToString() &&
                    tKey.szKey == NativeUtil.BArrToStr(tReconnectReq.tConnectInfo.m_szPassword))
                {
                    bValidLogin = true;
                }
            }
            else
            {
                if (tReconnectReq.tConnectInfo.m_lKey2 == CLoginHash.GetInstance().CalculateConnectHash(
                    tReconnectReq.tConnectInfo.m_lUSN,
                    (uint) tReconnectReq.tConnectInfo.m_lKey1, 
                    (uint) (tReconnectReq.tConnectInfo.m_lKey1 >> 31)))
                {
                    bValidLogin = true;
                }
            }

            if (!bValidLogin)
            {
                tReconnectRet.m_eResult = CONNECTRESULT.CONNECT_INVALID_USERNAME_OR_PASSWORD;
                cPacket.CopyToUserDataArea(tReconnectRet);
                CServer.SendMessage(cContext, cPacket);
                return;
            }

            CServerLog.GetLogger().etcinfo($"[REQUEST_RECONNECT] USN : {tReconnectReq.tConnectInfo.m_lUSN},");
            cContext.tStatInfo.nLoginUDate = NativeUtil.GetTimestamp();
            cContext.tStatInfo.dConnectTime = NativeUtil.GetTickCount();
                    
            var iBanTime = CServerDataManager.GetUserBanTime(tReconnectReq.tConnectInfo.m_lUSN);
            if (iBanTime != 0)
            {
                if (iBanTime == -1) tReconnectRet.m_eResult = CONNECTRESULT.CONNECT_FOREVER_BANNED;
                else
                {
                    tReconnectRet.m_eResult = CONNECTRESULT.CONNECT_BANNED;
                    tReconnectRet.tLoginInfo.m_nBannedTime = iBanTime - NativeUtil.GetTimestamp();
                }
                cPacket.CopyToUserDataArea(tReconnectRet);
                CServer.SendMessage(cContext, cPacket);
                return;
            }
                    
            ProcessReconnect(cContext, tReconnectReq, tReconnectRet);
        }

        private void OnRequestUpdate(CLGUserContext cContext, CPacket cPacket)
        {
            var tUpdateRet = new PROTO_REQUEST_UPDATE_RESULT();
            tUpdateRet.aServers = new PROTO_GAMESERVER_UPDATE[MAX_GAMESERVER_COUNT];
            
            CopyUpdateServerList(ref tUpdateRet.aServers);
                    
            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_UPDATE_RESULT);
            cPacket.CopyToUserDataArea(tUpdateRet);
            var bSent = CServer.SendMessage(cContext, cPacket);
            Console.WriteLine($"[DIAG] OnRequestUpdate: replied UPDATE_RESULT structSize={System.Runtime.InteropServices.Marshal.SizeOf(typeof(PROTO_REQUEST_UPDATE_RESULT))}, srv0.ID={tUpdateRet.aServers[0].m_nServerID}, srv0.Count={tUpdateRet.aServers[0].m_nServerConnectCount}, sent={bSent}");
        }

        private void OnRequestCharacterCreate(CLGUserContext cContext, CPacket cPacket)
        {
            if (cContext.tStatInfo.bReconnected) return;

            cContext.tStatInfo.nSelectServerLimitTime = -200; // limit 6
            cContext.tStatInfo.nCreateCharLimitTime = 0; // limit 21
            
            PROTO_REQUEST_CHARACTER_CREATE tCharCreateReq = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tCharCreateReq) );
            
            var tCharCreateRet = new PROTO_CHARACTER_CREATE_RESULT();

            if (tCharCreateReq.m_szCallName[0] == 0)
                tCharCreateRet.m_eResult =
                    PROTO_CHARACTER_CREATE_RESULT.CHARACTERCREATE_RESULT.CHARACTERCREATE_WRONGNAME;

            var nDBReadResult = CServerDataManager.DBCreateCharacter(cContext, tCharCreateReq.m_szCallName);
            if (nDBReadResult == -1)
            {
                tCharCreateRet.m_eResult = 
                    PROTO_CHARACTER_CREATE_RESULT.CHARACTERCREATE_RESULT.CHARACTERCREATE_FAIL;
            }
            else if (nDBReadResult == -2)
            {
                tCharCreateRet.m_eResult = 
                    PROTO_CHARACTER_CREATE_RESULT.CHARACTERCREATE_RESULT.CHARACTERCREATE_UNKNOWNERROR;
            }
            else if (nDBReadResult != -3 && nDBReadResult >= 0 )
            {
                if (nDBReadResult > 0)
                {
                    cContext.tStatInfo.nSelectServerLimitTime = 0;
                    cContext.tStatInfo.nCreateCharLimitTime = -200;
                    tCharCreateRet.m_eResult = 
                        PROTO_CHARACTER_CREATE_RESULT.CHARACTERCREATE_RESULT.CHARACTERCREATE_SUCCESS;
                    cContext.tStatInfo.bCharacterCreated = true;
                }
            }
            else
            {
                tCharCreateRet.m_eResult = 
                    PROTO_CHARACTER_CREATE_RESULT.CHARACTERCREATE_RESULT.CHARACTERCREATE_WRONGNAME;
            }

            if (tCharCreateRet.m_eResult ==
                PROTO_CHARACTER_CREATE_RESULT.CHARACTERCREATE_RESULT.CHARACTERCREATE_SUCCESS)
            {
                CServerDataManager.UpdateUserLoginDB_ExecuteQuery(cContext);
                CServerLog.GetLogger().info(
                    "[PROTO_CHARACTER_CREATE_RESULT] : CHARACTERCREATE_SUCCESS, USN : {0}, Name : {1}, nDBReadResult : {2}",
                    cContext.tStatInfo.lUSN,
                    NativeUtil.BArrToStr(tCharCreateReq.m_szCallName), nDBReadResult);
            }
            else
            {
                switch (tCharCreateRet.m_eResult)
                {
                    case PROTO_CHARACTER_CREATE_RESULT.CHARACTERCREATE_RESULT.CHARACTERCREATE_FAIL:
                        CServerLog.GetLogger().error(
                            "[PROTO_CHARACTER_CREATE_RESULT] : CHARACTERCREATE_FAIL, USN : {0}, Name : {1}, nDBReadResult : {2}",
                            cContext.tStatInfo.lUSN,
                            NativeUtil.BArrToStr(tCharCreateReq.m_szCallName), nDBReadResult);
                        break;
                    case PROTO_CHARACTER_CREATE_RESULT.CHARACTERCREATE_RESULT
                        .CHARACTERCREATE_UNKNOWNERROR:
                        CServerLog.GetLogger().error(
                            "[PROTO_CHARACTER_CREATE_RESULT] : CHARACTERCREATE_UNKNOWNERROR, USN : {0}, Name : {1}, nDBReadResult : {2}",
                            cContext.tStatInfo.lUSN,
                            NativeUtil.BArrToStr(tCharCreateReq.m_szCallName), nDBReadResult);
                        break;
                    case PROTO_CHARACTER_CREATE_RESULT.CHARACTERCREATE_RESULT.CHARACTERCREATE_WRONGNAME:
                        CServerLog.GetLogger().error(
                            "[PROTO_CHARACTER_CREATE_RESULT] : CHARACTERCREATE_WRONGNAME, USN : {0}, Name : {1}, nDBReadResult : {2}",
                            cContext.tStatInfo.lUSN,
                            NativeUtil.BArrToStr(tCharCreateReq.m_szCallName), nDBReadResult);
                        break;
                    case PROTO_CHARACTER_CREATE_RESULT.CHARACTERCREATE_RESULT
                        .CHARACTERCREATE_NEED_RECONNECT:
                        CServerLog.GetLogger().error(
                            "[PROTO_CHARACTER_CREATE_RESULT] : CHARACTERCREATE_NEED_RECONNECT, USN : {0}, Name : {1}, nDBReadResult : {2}",
                            cContext.tStatInfo.lUSN,
                            NativeUtil.BArrToStr(tCharCreateReq.m_szCallName), nDBReadResult);
                        break;
                }
            }

            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_CHARACTER_CREATE_RESULT);
            cPacket.CopyToUserDataArea(tCharCreateRet);
            CServer.SendMessage(cContext, cPacket);
        }

        private void OnRequestCharacterNameCheck(CLGUserContext cContext, CPacket cPacket)
        {
            if (cContext.tStatInfo.bReconnected) return;

            cContext.tStatInfo.nSelectServerLimitTime = -200; // limit 6
            cContext.tStatInfo.nCreateCharLimitTime = 0; // limit 21
            
            PROTO_REQUEST_CHARACTER_NAMECHECK tNameCheck = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tNameCheck) );
            
            var tNameCheckRet = new PROTO_CHARACTER_NAMECHECK_RESULT();

            var nDBReadResult = CServerDataManager.DBCheckNameAvailable(cContext, tNameCheck.m_szCallName);
            if (nDBReadResult == -11)
            {
                tNameCheckRet.m_eResult =
                    PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_DENY_WORD;
            }
            else if (nDBReadResult == -21)
            {
                tNameCheckRet.m_eResult =
                    PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_NULL;
            }
            else if (nDBReadResult == -22)
            {
                tNameCheckRet.m_eResult =
                    PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_BLANK;
            }
            else if (nDBReadResult == -23)
            {
                tNameCheckRet.m_eResult =
                    PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_SPECIAL_CHAR;
            }
            else if (nDBReadResult == -24)
            {
                tNameCheckRet.m_eResult =
                    PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_SHORT_CHAR;
            }
            else if (nDBReadResult == -20)
            {
                tNameCheckRet.m_eResult =
                    PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_LONG_CHAR;
            }
            else if (nDBReadResult != -2 && nDBReadResult != -1)
            {
                if (nDBReadResult > 0)
                    tNameCheckRet.m_eResult =
                        PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_POSSIBLE;
            }
            else
            {
                tNameCheckRet.m_eResult =
                    PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_IMPOSSIBLE;
            }

            if (tNameCheckRet.m_eResult ==
                PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_POSSIBLE)
            {
                CServerLog.GetLogger().info(
                    "[PROTO_CHARACTER_NAMECHECK_RESULT] : CHARACTERNAMECHECK_POSSIBLE, USN : {0}, Name : {1}, nDBReadResult : {2}",
                    cContext.tStatInfo.lUSN,
                    NativeUtil.BArrToStr(tNameCheck.m_szCallName), nDBReadResult);
            }
            else
            {
                switch (tNameCheckRet.m_eResult)
                {
                    case PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_DENY_WORD:
                        CServerLog.GetLogger().error(
                            "[PROTO_CHARACTER_NAMECHECK_RESULT] : CHARACTERNAMECHECK_DENY_WORD, USN : {0}, Name : {1}, nDBReadResult : {2}",
                            cContext.tStatInfo.lUSN,
                            NativeUtil.BArrToStr(tNameCheck.m_szCallName), nDBReadResult);
                        break;
                    case PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_IMPOSSIBLE:
                        CServerLog.GetLogger().error(
                            "[PROTO_CHARACTER_NAMECHECK_RESULT] : CHARACTERNAMECHECK_IMPOSSIBLE, USN : {0}, Name : {1}, nDBReadResult : {2}",
                            cContext.tStatInfo.lUSN,
                            NativeUtil.BArrToStr(tNameCheck.m_szCallName), nDBReadResult);
                        break;
                    case PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_NULL:
                        CServerLog.GetLogger().error(
                            "[PROTO_CHARACTER_NAMECHECK_RESULT] : CHARACTERNAMECHECK_NULL, USN : {0}, Name : {1}, nDBReadResult : {2}",
                            cContext.tStatInfo.lUSN,
                            NativeUtil.BArrToStr(tNameCheck.m_szCallName), nDBReadResult);
                        break;
                    case PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_BLANK:
                        CServerLog.GetLogger().error(
                            "[PROTO_CHARACTER_NAMECHECK_RESULT] : CHARACTERNAMECHECK_BLANK, USN : {0}, Name : {1}, nDBReadResult : {2}",
                            cContext.tStatInfo.lUSN,
                            NativeUtil.BArrToStr(tNameCheck.m_szCallName), nDBReadResult);
                        break;
                    case PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT
                        .CHARACTERNAMECHECK_SPECIAL_CHAR:
                        CServerLog.GetLogger().error(
                            "[PROTO_CHARACTER_NAMECHECK_RESULT] : CHARACTERNAMECHECK_SPECIAL_CHAR, USN : {0}, Name : {1}, nDBReadResult : {2}",
                            cContext.tStatInfo.lUSN,
                            NativeUtil.BArrToStr(tNameCheck.m_szCallName), nDBReadResult);
                        break;
                    case PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_LONG_CHAR:
                        CServerLog.GetLogger().error(
                            "[PROTO_CHARACTER_NAMECHECK_RESULT] : CHARACTERNAMECHECK_LONG_CHAR, USN : {0}, Name : {1}, nDBReadResult : {2}",
                            cContext.tStatInfo.lUSN,
                            NativeUtil.BArrToStr(tNameCheck.m_szCallName), nDBReadResult);
                        break;
                    case PROTO_CHARACTER_NAMECHECK_RESULT.NAMECHECK_RESULT.CHARACTERNAMECHECK_SHORT_CHAR:
                        CServerLog.GetLogger().error(
                            "[PROTO_CHARACTER_NAMECHECK_RESULT] : CHARACTERNAMECHECK_SHORT_CHAR, USN : {0}, Name : {1}, nDBReadResult : {2}",
                            cContext.tStatInfo.lUSN,
                            NativeUtil.BArrToStr(tNameCheck.m_szCallName), nDBReadResult);
                        break;
                }
            }

            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_CHARACTER_NAMECHECK_RESULT);
            cPacket.CopyToUserDataArea(tNameCheckRet);
            CServer.SendMessage(cContext, cPacket);
        }

        private void OnRequestSameIDConnectForceLeave(CLGUserContext cContext, CPacket cPacket)
        {
            lock (sm_lstUsers)
            {
                for (var i = 0; i < MAX_GAMESERVER_COUNT + 1; i++)
                {
                    if (sm_lstUsers[i].FindIndex(x =>
                        x.m_lUSN == cContext.tStatInfo.lReqUSN &&
                        x.m_dConnectTime != cContext.tStatInfo.dConnectTime) != -1)
                    {
                        if (i == LG_SERVER_NO)
                            HandleDuplicateConnection(cContext);
                        else
                            SendForceLeaveMsgToMM(cContext, i);
                    }
                }
            }

            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_SAMEIDCONNECT_FORCE_LEAVE_RESULT);
            CServer.SendMessage(cContext, cPacket);
        }

        private void OnServerConnectRequest(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_REQUEST_SERVER_CONNECT_REQUEST tServerConnect = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tServerConnect) );
            
            var tServerConnectRet = new PROTO_REQUEST_SERVER_CONNECT_RESULT();

            GetServerUserCountLimit(tServerConnect.m_sServerID, 
                out var iConnectCount, out var iLimitCount);

            if (iConnectCount < iLimitCount)
            {
                cContext.tStatInfo.nStatus = 20;
                tServerConnectRet.m_eResult = PROTO_REQUEST_SERVER_CONNECT_RESULT.CONNECTRESULT.SUCCESS;
                tServerConnectRet.m_nSelectIndex = -1;
                
                tServerConnectRet.tProtoReturnInfo.lTimeStamp = NativeUtil.GetTimestamp();
                tServerConnectRet.tProtoReturnInfo.szPartKeyDay = cContext.tStatInfo.szDayPartKey;
                tServerConnectRet.tProtoReturnInfo.lLoginTime = cContext.tStatInfo.nLoginUDate;
                tServerConnectRet.tProtoReturnInfo.dConnectTime = cContext.tStatInfo.dConnectTime;
                tServerConnectRet.tProtoReturnInfo.szLoginDate = cContext.tStatInfo.szLoginDateTime;
                tServerConnectRet.tProtoReturnInfo.nLev = cContext.tStatInfo.nLevel;
                tServerConnectRet.tProtoReturnInfo.nKill = cContext.tStatInfo.nEnemyKill;
                tServerConnectRet.tProtoReturnInfo.nDeath = cContext.tStatInfo.nDeathCount;
                tServerConnectRet.tProtoReturnInfo.nSuperVisor = cContext.tStatInfo.bySupervisor;

                GetClanDataFromDBServerExecuteQuery(cContext);

                tServerConnectRet.tProtoReturnInfo.ClanKey = cContext.tStatInfo.lClanKey;
                tServerConnectRet.tProtoReturnInfo.ClanGrade = cContext.tStatInfo.byClanUserGrade;
                tServerConnectRet.tProtoReturnInfo.ClanUnitInfo = new CLAN_UNIT_INFO();
                tServerConnectRet.tProtoReturnInfo.nClanUser = cContext.tStatInfo.byClanMember;
                tServerConnectRet.tProtoReturnInfo.szClanID = cContext.tStatInfo.szClanID;
                tServerConnectRet.tProtoReturnInfo.nClanSuperVisor = 0;
                tServerConnectRet.tProtoReturnInfo.nClanLLevel = cContext.tStatInfo.sClanLLevel;
                tServerConnectRet.tProtoReturnInfo.szClanSrl = cContext.tStatInfo.szClanSrl;
                tServerConnectRet.tProtoReturnInfo.szClanUserSrl = cContext.tStatInfo.szClanUserSrl;
                tServerConnectRet.tProtoReturnInfo.byWaveLevel = cContext.tStatInfo.byWaveLevel;

                tServerConnectRet.tProtoReturnInfo.szHashedValue =
                    CHashMaker.CalculateServerConnectHash(tServerConnectRet.tProtoReturnInfo);
            }
            else
            {
                tServerConnectRet.m_eResult =
                    PROTO_REQUEST_SERVER_CONNECT_RESULT.CONNECTRESULT.SERVER_FULL;
            }
            
            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_SERVER_CONNECT_RESULT);
            cPacket.CopyToUserDataArea(tServerConnectRet);
            CServer.SendMessage(cContext, cPacket);
        }

        private void OnRequestDailyScore(CLGUserContext cContext, CPacket cPacket)
        {
            var tDailyScoreRet = new PROTO_REQUEST_DAILY_SCORE_RESULT();
            tDailyScoreRet.m_lExp = cContext.tStatInfo.lExp;
            tDailyScoreRet.m_iGamePoint = cContext.tStatInfo.nGamePoint;
            
            tDailyScoreRet.m_iTodayWin = cContext.tStatInfo.nTodayWin;
            tDailyScoreRet.m_iTodayLose = cContext.tStatInfo.nTodayLose;
            
            tDailyScoreRet.m_iTodayExp = cContext.tStatInfo.nTodayEXP;
            tDailyScoreRet.m_iTodayGamePoint = cContext.tStatInfo.nTodayGamePoint;
            tDailyScoreRet.m_iTodayKill = cContext.tStatInfo.nTodayKill;
            tDailyScoreRet.m_iTodayDeath = cContext.tStatInfo.nTodayDeath;
            tDailyScoreRet.m_iTodayHeadshot = cContext.tStatInfo.nTodayHeadshot;

            var nExtep = CSharedVariable.aLevelLimits[cContext.tStatInfo.nLevel] - cContext.tStatInfo.lExp;
            var nAverageExpPerPlay = 0;
            if (cContext.tStatInfo.nTodayPlay > 0)
                nAverageExpPerPlay = cContext.tStatInfo.nTodayEXP / cContext.tStatInfo.nTodayPlay;
            if (nAverageExpPerPlay <= 0)
            {
                tDailyScoreRet.m_iMorePlayCountToNextLevel = 0;
            }
            else
            {
                tDailyScoreRet.m_iMorePlayCountToNextLevel = (int) nExtep / nAverageExpPerPlay;
                if (nExtep % nAverageExpPerPlay != 0) tDailyScoreRet.m_iMorePlayCountToNextLevel++;
            }

            tDailyScoreRet.m_nNextLev = (short) (cContext.tStatInfo.nLevel + 1);
            
            CServerLog.GetLogger().info("PROTOCOL_DAILY_SCORE | " +
                                        "USN: {0}, levelep: {1}, myep: {2}, extep: {3}, nTodayEPAmount: {4}, " +
                                        "nTodayPlayCnt: {5}, nAveEP: {6}, nMorePlayCountToNextLevel: {7}",
                cContext.tStatInfo.lUSN, 
                CSharedVariable.aLevelLimits[cContext.tStatInfo.nLevel], cContext.tStatInfo.lExp,
                nExtep, cContext.tStatInfo.nTodayEXP, cContext.tStatInfo.nTodayPlay, nAverageExpPerPlay,
                tDailyScoreRet.m_iMorePlayCountToNextLevel);
            
            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_DAILY_SCORE_RESULT);
            cPacket.CopyToUserDataArea(tDailyScoreRet);
            CServer.SendMessage(cContext, cPacket);
        }

        private void OnServerDisconnectRequest(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_REQUEST_SERVER_DISCONNECT_REQUEST tDisconnectReq = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tDisconnectReq) );
            
            var tDisconnectRet = new PROTO_REQUEST_SERVER_DISCONNECT_RESULT();
            if (tDisconnectReq.m_eReason == DISCONNECTREASON.DISCONNECTREASON_CONNECT_MM_SERVER)
            {
                cContext.tStatInfo.nStatus = 10; // 10 -> 20
            }
            else if (tDisconnectReq.m_eReason == DISCONNECTREASON.DISCONNECTREASON_EXIT_GAME)
            {
                cContext.tStatInfo.nStatus = 20;
            }

            tDisconnectRet.m_eReason = tDisconnectReq.m_eReason;
                    
            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_SERVER_DISCONNECT_RESULT);
            cPacket.CopyToUserDataArea(tDisconnectRet);
            CServer.SendMessage(cContext, cPacket);
        }

        private void OnRequestReconnectForDist(CLGUserContext cContext, CPacket cPacket)
        {
            if (cContext.tStatInfo.bReconnected) return;
            
            PROTO_REQUEST_RECONNECT_FOR_DIST tReconnectForDist = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tReconnectForDist) );
            
            cPacket.SetFirstClass(PROTOCOL_LOGIN_FIRST_CLASS);
            cPacket.SetSecondClass(PROTOCOL_REQUEST_RECONNECT_FOR_DIST_RESULT);
            var tReconnectForDistRet = new PROTO_REQUEST_RECONNECT_FOR_DIST_RESULT();

            if (tReconnectForDist.tConnectInfo.m_lKey2 != CLoginHash.GetInstance().CalculateConnectHash(
                tReconnectForDist.tConnectInfo.m_lUSN,
                (uint) tReconnectForDist.tConnectInfo.m_lKey1,
                (uint) (tReconnectForDist.tConnectInfo.m_lKey1 >> 31)))
            {
                tReconnectForDistRet.m_eResult = CONNECTRESULT.CONNECT_INVALID_USERNAME_OR_PASSWORD;
                cPacket.CopyToUserDataArea(tReconnectForDistRet);
                CServer.SendMessage(cContext, cPacket);
                return;
            }
            
            /*var aCryptedData = tReconnectForDist.tConnectInfo.m_tCryptedData.m_aCryptedData.ToArray();
            DecryptConnectHashData(ref aCryptedData);
            CONNECT_HASH_DATA tConnectHashData;

            unsafe
            {
                fixed (byte* ptr = &aCryptedData[0])
                {
                    tConnectHashData = (CONNECT_HASH_DATA)Marshal.PtrToStructure((IntPtr)ptr, typeof(CONNECT_HASH_DATA));
                }
            }

            if (tConnectHashData.m_lUSN != tReconnectForDist.tConnectInfo.m_lUSN
                || tConnectHashData.m_lHash != CalculateConnectHash(tConnectHashData.m_lUSN,
                    (uint) tConnectHashData.m_lClock, (uint) (tConnectHashData.m_lClock >> 31)))
            {
                tReconnectForDistRet.m_eResult = CONNECTRESULT.CONNECT_INVALID_USERNAME_OR_PASSWORD;
                cPacket.CopyDataToUserArea(tReconnectForDistRet);
                CServer.SendMessage(cContext, cPacket);
                break;
            }*/
            
            if (tReconnectForDist.tConnectInfo.m_lUSN == -1)
            {
                tReconnectForDistRet.m_eResult = CONNECTRESULT.CONNECT_INVALID_USERNAME_OR_PASSWORD;
                cPacket.CopyToUserDataArea(tReconnectForDistRet);
                CServer.SendMessage(cContext, cPacket);
                cContext.tStatInfo.lUSN = -1;
                return;
            }
            
            CServerLog.GetLogger().etcinfo("[REQUEST_RECONNECT_FOR_DIST] USN : {0}, ", 
                tReconnectForDist.tConnectInfo.m_lUSN);
            
            cContext.tStatInfo.nLoginUDate = NativeUtil.GetTimestamp();
            cContext.tStatInfo.dConnectTime = NativeUtil.GetTickCount();
            var iBanTime = CServerDataManager.GetUserBanTime(tReconnectForDist.tConnectInfo.m_lUSN);
            if (iBanTime != 0)
            {
                if (iBanTime == -1) tReconnectForDistRet.m_eResult = CONNECTRESULT.CONNECT_FOREVER_BANNED;
                else
                {
                    tReconnectForDistRet.m_eResult = CONNECTRESULT.CONNECT_BANNED;
                    tReconnectForDistRet.tLoginInfo.m_nBannedTime = iBanTime - NativeUtil.GetTimestamp();
                }
                cPacket.CopyToUserDataArea(tReconnectForDistRet);
                CServer.SendMessage(cContext, cPacket);
                return;
            }
            
            ProcessReconnectForDist(cContext, tReconnectForDist);
        }

        private bool CheckTutorialPopupValid(int location, PROTO_REQUEST_TUTORIAL_POPUP_END tTutorialPopupEnd)
        {
            if (location != 2) return true;
            for (var i = 0; i < (int) ROUNDTYPE.ROUNDTYPE_MAX; i++)
            {
                if (tTutorialPopupEnd.m_aValues[i] > 0)
                    return true;
            }
            return false;
        }
        
        private void OnRequestTutorialPopupEnd(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_REQUEST_TUTORIAL_POPUP_END tTutorialEnd = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tTutorialEnd) );
            
            if (!CheckTutorialPopupValid(1, tTutorialEnd)) return;
            CServerDataManager.SetTutorialLog_ExecuteQuery(1, cContext.tStatInfo.lUSN, tTutorialEnd);
        }

        private void OnConfirmLatestPolicy(CLGUserContext cContext, CPacket cPacket)
        {
            PROTO_CONFIRM_LATEST_POLICY tConfirmLatestPolicy = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tConfirmLatestPolicy) );
            
            if (tConfirmLatestPolicy.m_byAgree == 1)
            {
                CServerLog.GetLogger().info("PROTOCOL_CONFIRM_LATEST_POLICY | USN {0}", cContext.tStatInfo.lUSN);
                CServerDataManager.ExecuteGameQuery(
                    QUERY_UPDATE_USER_READ_POLICY_STATE,
                    out _,
                    cContext.tStatInfo.lUSN);

                //CServerDataManager.GetDatabaseMgr().ExecuteQuery();
                //CServerDataManager.GetUserInfoFromDB_ExecuteQuery();
            }
        }
        
        #endregion

        #region OnLoginMsg

        private string OnLoginMsg(CLGUserContext cContext, CPacket cPacket)
        {
            switch (cPacket.GetSecondClass())
            {
                case PROTOCOL_REQUEST_CONNECT:
                    OnRequestLogin(cContext, cPacket);
                    return "REQUEST_CONNECT";
                case PROTOCOL_REQUEST_RECONNECT:
                    OnRequestReconnect(cContext, cPacket);
                    return "REQUEST_RECONNECT";
                case PROTOCOL_REQUEST_UPDATE:
                    OnRequestUpdate(cContext, cPacket);
                    return "REQUEST_UPDATE";
                case PROTOCOL_REQUEST_CHARACTER_CREATE: 
                    OnRequestCharacterCreate(cContext, cPacket);
                    return "REQUEST_CHARACTER_CREATE";
                case PROTOCOL_REQUEST_CHARACTER_NAMECHECK: 
                    OnRequestCharacterNameCheck(cContext, cPacket);
                    return "REQUEST_CHARACTER_NAMECHECK";
                case PROTOCOL_REQUEST_SAMEIDCONNECT_FORCE_LEAVE:
                    OnRequestSameIDConnectForceLeave(cContext, cPacket);
                    return "REQUEST_SAMEIDCONNECT_FORCE_LEAVE";
                case PROTOCOL_REQUEST_SERVER_CONNECT_REQUEST: 
                    OnServerConnectRequest(cContext, cPacket);
                    return "REQUEST_SERVER_CONNECT_REQUEST";
                case PROTOCOL_REQUEST_SERVER_DISCONNECT_REQUEST: 
                    OnServerDisconnectRequest(cContext, cPacket);
                    return "REQUEST_SERVER_DISCONNECT_REQUEST";
                case PROTOCOL_REQUEST_RECONNECT_FOR_DIST: 
                    OnRequestReconnectForDist(cContext, cPacket);
                    return "REQUEST_RECONNECT_FOR_DIST";
                case PROTOCOL_REQUEST_DAILY_SCORE:
                    OnRequestDailyScore(cContext, cPacket);
                    return "REQUEST_DAILY_SCORE";
                case PROTOCOL_REQUEST_TUTORIAL_POPUP:
                    return "REQUEST_TUTORIAL_POPUP";
                case PROTOCOL_REQUEST_TUTORIAL_POPUP_END: 
                    OnRequestTutorialPopupEnd(cContext, cPacket);
                    return "REQUEST_TUTORIAL_POPUP_END";
                case PROTOCOL_CONFIRM_LATEST_POLICY: 
                    OnConfirmLatestPolicy(cContext, cPacket);
                    return "CONFIRM_LATEST_POLICY";
            }

            return null;
        }

        #endregion
        
        #region OnRcvLoginMsg
        
        private string OnRcvLoginMsg(CLGUserContext cContext, CPacket cPacket)
        {
            switch (cPacket.GetSecondClass())
            {
                case PROTOCOL_REQUEST_CONNECT:
                case PROTOCOL_REQUEST_RECONNECT:
                case PROTOCOL_REQUEST_SAMEIDCONNECT_FORCE_LEAVE:
                case PROTOCOL_REQUEST_RECONNECT_FOR_DIST:
                    return OnLoginMsg(cContext, cPacket);
                default:
                    if (cContext.tStatInfo.lUSN != -1)
                    {
                        return OnLoginMsg(cContext, cPacket);
                    }
                    else
                    {
                        CServerLog.GetLogger().etcinfo(
                            "[CGameNetworkHandler::OnRcvLoginMsg] invalid packet second {0} third {1}",
                            cPacket.GetSecondClass(), cPacket.GetThirdClass());
                        CSocketController.GetLoginServer().GetSocket().CloseClient(cContext);

                        return null;
                    }
            }
        }
        
        #endregion

        #region NetworkHandler

        private bool m_bEventStarted = false;

        private void SendAutoEventList()
        {
            lock (CMainServer.m_dicEventInfos)
            {
                if (CMainServer.m_dicEventInfos.ContainsKey(-1))
                {
                    CSocketController.GetMgmtServer().BroadcastPacket(
                        LMS_PK.PROTOCOL_LOGIN_MGMT, LMS_PK.PROTOCOL_MGMT_AUTO_EVENT_START,
                        CMainServer.m_dicEventInfos[-1]);
                    return;
                }
                
                foreach (var cUserTokenBase in CSocketController.GetMgmtServer().GetSocket().GetClients())
                {
                    var cContext = (CLGUserContext) cUserTokenBase;
                    if (cContext.sMgmtServerNo > 0 && cContext.sMgmtServerNo <= 100)
                    {
                        if (CMainServer.m_dicEventInfos.ContainsKey(cContext.sMgmtServerNo))
                            CSocketController.GetMgmtServer().SendPacket(cContext,
                                LMS_PK.PROTOCOL_LOGIN_MGMT, LMS_PK.PROTOCOL_MGMT_AUTO_EVENT_START,
                                CMainServer.m_dicEventInfos[cContext.sMgmtServerNo]);
                        else
                            CSocketController.GetMgmtServer().SendPacket(cContext,
                                LMS_PK.PROTOCOL_LOGIN_MGMT, LMS_PK.PROTOCOL_MGMT_AUTO_EVENT_END);
                    }
                }
            }
        }

        private void OnInternalNetworkMsg(CPacket cPacket, double recv_time)
        {
            if (cPacket.GetFirstClass() == PROTOCOL_LOGIN_SERVER_AUTO_EVENT_PULSE)
            {
                var bHasAutoEvent = CMainServer.RefreshAutoEventList();

                if (!bHasAutoEvent)
                {
                    if (m_bEventStarted)
                    {
                        CSocketController.GetMgmtServer().BroadcastPacket(
                            LMS_PK.PROTOCOL_LOGIN_MGMT, LMS_PK.PROTOCOL_MGMT_AUTO_EVENT_END);
                        m_bEventStarted = false;
                    }
                }
                else
                {
                    SendAutoEventList();
                    m_bEventStarted = true;
                }
            }
            else if (cPacket.GetFirstClass() == PROTOCOL_LOGIN_SERVER_PUSH_PACKET)
            {
                try
                {
                    var swProcessTime = new Stopwatch();

                    swProcessTime.Reset();
                    swProcessTime.Start();
                
                    var szProtoName = OnRcvServerPushMsg(cPacket);
                
                    swProcessTime.Stop();

                    if (!string.IsNullOrEmpty(szProtoName))
                    {
                        CServerLog.GetLogger().packet_info(
                            szProtoName, 
                            CServerConfig.GetServerRemoteAddr(),
                            CServerConfig.GetServerRemotePort(), 
                            0,
                            recv_time, 
                            swProcessTime.Elapsed.TotalSeconds,
                            cPacket.GetFirstClass(), 
                            cPacket.GetSecondClass());
                    }
                }
                catch (Exception e)
                {
                    CServerLog.GetLogger().error(
                        "[CGameNetworkHandler::OnNetworkMsg()] OnRcvServerPushMsg Exception - {0}\r\n{1}",
                        e.Message, e.StackTrace);
                }
            }
        }

        public void OnAccept(IUserTokenBase tokenBase)
        {
            var cContext = (CLGUserContext) tokenBase;
            
            Console.WriteLine($"[DIAG] OnAccept called - IP: {cContext.IPAddress}, Port: {cContext.RemotePort}");
            CServerLog.GetLogger().info("Accepted ({0}, {1})", cContext.IPAddress.ToString(), cContext.RemotePort);
            var nNewKey = CSocketController.GetLoginServer().GetNextClientKey();
            if (nNewKey == -1) // overflows!!!
            {
                Console.WriteLine($"[DIAG] GetNextClientKey returned -1 (overflow)!");
                return;
            }
            
            Console.WriteLine($"[DIAG] GetNextClientKey returned: {nNewKey}");
            CSocketController.GetLoginServer().SetSocketContext(nNewKey, cContext);
        }

        public void OnNetworkMsg(IUserTokenBase tokenBase, byte[] buff, double recv_time)
        {
            var cPacket = new CPacket(buff);
            
            Console.WriteLine($"[DIAG] OnNetworkMsg - Packet: First=0x{cPacket.GetFirstClass():X2}, Second=0x{cPacket.GetSecondClass():X2}, Third=0x{cPacket.GetThirdClass():X2}, Size={buff.Length}");
            
            if (tokenBase == null)
            {
                OnInternalNetworkMsg(cPacket, recv_time);
                return;
            }
            
            var cContext = (CLGUserContext) tokenBase;

            // Create our debug info
            if (cPacket.GetReceivedSize() > PROTOCOL_NON_USER_AREA_SIZE)
            {
                if (CServerLog.GetLogger().GetLogLevel() == LOGLEVEL.LEVEL_ALL)
                    CServerLog.GetLogger().packet(CServer.ByteArr2Hex(buff));
            }
            
            CServerLog.GetLogger().packet(
                "[ RECV ][ PACKET First 0x{0:X2} Second 0x{1:X2} Third 0x{2:X2} ]",
                cPacket.GetFirstClass(), cPacket.GetSecondClass(), cPacket.GetThirdClass());
            
            if (cPacket.GetFirstClass() == PROTOCOL_LOGIN_FIRST_CLASS)
            {
                try
                {
                    var swProcessTime = new Stopwatch();

                    swProcessTime.Reset();
                    swProcessTime.Start();
                    
                    var szProtoName = OnRcvLoginMsg(cContext, cPacket);
                    
                    swProcessTime.Stop();

                    Console.WriteLine($"[DIAG] OnRcvLoginMsg handled Second=0x{cPacket.GetSecondClass():X2} -> {szProtoName ?? "(no handler)"}");

                    if (!string.IsNullOrEmpty(szProtoName))
                    {
                        CServerLog.PacketProfile(szProtoName, cContext, recv_time, swProcessTime, cPacket);
                        
                        CServerLog.PrintSvrState(szProtoName, cContext.IPAddress.ToString(),
                            cContext.RemotePort, (int) cContext.Socket.Handle, 
                            recv_time, swProcessTime.Elapsed.TotalSeconds, 
                            cPacket.GetFirstClass(), cPacket.GetSecondClass());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"[DIAG] OnRcvLoginMsg EXCEPTION: {e.Message}\r\n{e.StackTrace}");
                    CServerLog.GetLogger().error(
                        "[CGameNetworkHandler::OnNetworkMsg()] OnRcvLoginMsg Exception - {0}\r\n{1}",
                        e.Message, e.StackTrace);
                }
            }
            else if (cPacket.GetFirstClass() == PROTOCOL_MATCHMAKING)
            {
                if (cPacket.GetSecondClass() == PROTOCOL_CONFESS_HACK)
                {
                    CServerLog.PacketProfile("PROTOCOL_CONFESS_HACK", 
                        cContext, recv_time, null, cPacket);

                    //var tConfessHack = cPacket.CopyDataFromUserArea<LG_MM_PK.PROTO_CONFESS_HACK>();
                    
                    // TODO HackLog
                }
            }
            else
            {
                CServerLog.GetLogger().etcinfo(
                    "[CGameNetworkHandler::OnNetworkMsg()] Invalid Packet First {0}",
                    cPacket.GetFirstClass());
            }
        }
        
        #endregion
    }
}