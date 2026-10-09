using cf_loginsrv.Config;
using cf_loginsrv.Log;
using cf_loginsrv.Socket;
using Commons.Native;
using DBGWMGR;
using Network.Packet;
using Network.Protocol;
using Network.SharedFolder;
using static DBGWMGR.E_GDBGW_DEFS;

namespace cf_loginsrv.Util
{
    public enum GETUSERDATA_RESULT
    {
        FOREVER_BANNED = -8,
        NO_NICK = -7,
        BANNED = -6,
        NO_ACCESS = -5,
        NO_USERDATA = -3,
        UNKNOWNERROR = -1,
        SUCCESS
    }
    
    public class CServerDataManager
    {
        private static CDBGWManager m_databaseMgr;
        
        public static bool SetTutorialLog_ExecuteQuery(int location, 
            long lUSN, LG_PK.PROTO_REQUEST_TUTORIAL_POPUP_END tTutorialPopupEnd)
        {
            var bSuccess = AExecuteLogSP(STORE_SET_TUTORIAL_LOG,
                TutorialPopupProcCallback, STORE_SET_TUTORIAL_LOG,
                lUSN, location,
                tTutorialPopupEnd.m_aValues);
            if (!bSuccess)
            {
                CServerLog.GetLogger().error("TutorialPopup [AsyncExecuteQuery Failed][{0}][{1}]",
                    (int)STORE_SET_TUTORIAL_LOG,
                    GetQueryString(STORE_SET_TUTORIAL_LOG));
            }

            return bSuccess;
        }
        
        private static void TutorialPopupProcCallback(object objPassThruParam, CGDBGWParser cParser, int nErrCode)
        {
            var nQueryId = (int) objPassThruParam;
            if (nErrCode != 0 || cParser == null || cParser.Result != 0)
            {
                CServerLog.GetLogger().error($"TutorialPopup [ExecuteQuery Failed][{nQueryId}]");
            }
        }
        
        public static int GetUserBanTime(long lUSN)
        {
            var bSuccess = ExecuteGameQuery(QUERY_SELECT_USER_CONENCT_UDATE, out var cParser, lUSN);
            if (!bSuccess || cParser.RowCount != 1) return 0;
            if (cParser.GetString(2) == "H") return -1;
            if (NativeUtil.GetTimestamp() >= cParser.GetInt(1)) return 0;
            return cParser.GetInt(1);
        }
        
        public static bool CheckAuthKeyValid_ExecuteQuery(long lUSN, byte[] aszAuthKey, string szClientIP)
        {
            var szEncryptKey = CBase64.Encode(aszAuthKey);
            
            var bSuccess = ExecuteGameSP(STORE_AUTHKEY_CHECK, out var cParser,
                lUSN, szEncryptKey, szClientIP);
            if (!bSuccess) return false;
            if (cParser.IsResultError())
            {
                CServerLog.GetLogger().warn("[AUTH_HACK_USER!] USN : {0}, szEncryptKey : {1}", lUSN, szEncryptKey);
                return false;
            }
            
            return true;
        }

        private static bool GiveItemToOriginUser(long lUSN)
        {
            var bSuccess = ExecuteGameSP(STORE_GIVE_PROMOTION_ITEM, out var cParser, lUSN, 1);
            if (!bSuccess) return false;
            
            return true;
        }

        public static int DBCreateCharacter(CLGUserContext cContext, byte[] aszNickName)
        {
            var iResult = CSharedMethod.CheckNickName(aszNickName);
            if (iResult >= 0)
            {
                var szNickName = NativeUtil.BArrToStr(aszNickName);
                foreach (var szRealAbuse in CSharedVariable.aszRealAbuseNames)
                {
                    if (szNickName.Contains(szRealAbuse))
                    {
                        return -11;
                    }
                }
                
                var bSuccess = ExecuteGameSP(STORE_CREATE_USER, out var cParser,
                    cContext.tStatInfo.lUSN,
                    CBase64.Encode(szNickName), CBase64.Encode(szNickName.ToLower()), 1);
                if (!bSuccess) return -2;
                
                switch (cParser.Result)
                {
                    case 1:
                        if (GiveItemToOriginUser(cContext.tStatInfo.lUSN))
                        {
                            CServerLog.GetLogger().info($"GiveItemToOriginUser() usn = {cContext.tStatInfo.lUSN}");
                        }
                        else
                        {
                            CServerLog.GetLogger().info($"GiveItemToOriginUser() failed");
                        }
                        return 1;
                    case -1:
                        CServerLog.GetLogger().warn("Execute Query Error [Q34]");
                        return -1;
                    case -2:
                        return -2;
                    case -3:
                        return -3;
                    default:
                        return -2;
                }
            }
            else
            {
                return -10;
            }
        }

        public static int DBCheckNameAvailable(CLGUserContext cContext, byte[] aszNickName)
        {
            if (m_databaseMgr == null) return -2;

            var iResult = CSharedMethod.CheckNickName(aszNickName);
            switch (iResult)
            {
                case -1:
                    return -20;
                case -2:
                    return -21;
                case -3:
                    return -22;
                case -4:
                    return -23;
                case -5:
                    return -24;
                default:
                    var szNickName = NativeUtil.BArrToStr(aszNickName);
                    foreach (var szRealAbuse in CSharedVariable.aszRealAbuseNames)
                    {
                        if (szNickName.Contains(szRealAbuse))
                        {
                            CServerLog.GetLogger().error(
                                "[CharacterNameCheck] : CHARACTERNAMECHECK_DENY_WORD, USN : {0}, Name : {1}, abuseName : {2}",
                                cContext.tStatInfo.lUSN, szNickName, szRealAbuse);
                            return -11;
                        }
                    }
                    
                    var bSuccess = ExecuteGameSP(STORE_CHECK_USER_NICK, out var cParser, CBase64.Encode(szNickName));
                    if (!bSuccess) return -2;
                    
                    switch (cParser.Result)
                    {
                        case 1:
                            return 1;
                        case -1:
                            CServerLog.GetLogger().warn("Execute Query Error [Q8]");
                            return -1;
                        case -2:
                            return -2;
                        case -3:
                            return -3;
                        default:
                            return -2;
                    }
            }
        }

        public static void RegisterAuthKey_ExecuteQuery(long lUSN, string szAuthKeyEncoded, string szClientIP)
        {
            var bSuccess = ExecuteGameSP(STORE_AUTHKEY_REGISTER, out var cParser,
                lUSN, szAuthKeyEncoded, szClientIP);
            if (!bSuccess || cParser == null || cParser.Result < 0)
            {
                CServerLog.GetLogger().error("[ExecuteQuery Failed][{0}], Result : {1}", 
                    GetQueryString(STORE_AUTHKEY_REGISTER), 
                    cParser.Result);
            }
        }
        
        public static void UpdateUserLoginDB_ExecuteQuery(CLGUserContext cContext)
        {
            var bSuccess = ExecuteGameSP(STORE_UPDATE_USER_LOGIN, out var cParser, cContext.tStatInfo.lUSN);
            if (!bSuccess || cParser == null || cParser.Result < 0)
            {
                CServerLog.GetLogger().error("[ExecuteQuery Failed][{0}], Result : {1}", 
                    GetQueryString(STORE_UPDATE_USER_LOGIN), 
                    cParser.Result);
            }
        }

        public static GETUSERDATA_RESULT GetUserDataFromDBServer_ExecuteQuery(CLGUserContext cContext, long lUSN)
        {
            var bSuccess = ExecuteGameQuery(QUERY_SELECT_USER_LOGIN_DATA, out var cParser, lUSN, lUSN);

            if (!bSuccess) return GETUSERDATA_RESULT.UNKNOWNERROR;
            if (cParser.RowCount == 0) return GETUSERDATA_RESULT.NO_USERDATA;
            
            // USN,LEV,ENEMY_KILL_CNT,DEATH_CNT,HOLD_TYPE,AUTHORITY,dbo.Base64_Encode(NICK) AS NICK,
            // CONNECT_DENY_UDATE,TODAY_EXP,TODAY_GAME_POINT,TODAY_PLAY,TODAY_KILL,TODAY_DEATH,
            // TODAY_HEADSHOT,TODAY_WIN,TODAY_LOSE,TODAY_DRAW,EXP,GAME_POINT,ISNULL(WAVE_LEVEL, 0)
            cContext.tStatInfo.nLevel = (short)cParser.GetInt(2);
            cContext.tStatInfo.nEnemyKill = cParser.GetInt(3);
            cContext.tStatInfo.nDeathCount = cParser.GetInt(4);
            var szHoldType = cParser.GetString(5);
            var szAuthority = cParser.GetString(6);
            cContext.tStatInfo.szNick = CBase64.Decode(cParser.GetString(7));
            cContext.tStatInfo.nConnectDenyUDate = cParser.GetInt(8);
            cContext.tStatInfo.nTodayEXP = cParser.GetInt(9);
            cContext.tStatInfo.nTodayGamePoint = cParser.GetInt(10);
            cContext.tStatInfo.nTodayPlay = cParser.GetInt(11);
            cContext.tStatInfo.nTodayKill = cParser.GetInt(12);
            cContext.tStatInfo.nTodayDeath = cParser.GetInt(13);
            cContext.tStatInfo.nTodayHeadshot = cParser.GetInt(14);
            cContext.tStatInfo.nTodayWin = cParser.GetInt(15);
            cContext.tStatInfo.nTodayLose = cParser.GetInt(16);
            cContext.tStatInfo.nTodayDraw = cParser.GetInt(17);
            cContext.tStatInfo.lExp = cParser.GetInt(18);
            cContext.tStatInfo.nGamePoint = cParser.GetInt(19);
            cContext.tStatInfo.byWaveLevel = cParser.GetByte(20);

            if (szAuthority == "G" || szAuthority == "A") cContext.tStatInfo.bySupervisor = 1;
            if (szHoldType == "A")
            {
                if (NativeUtil.GetTimestamp() > cContext.tStatInfo.nConnectDenyUDate)
                {
                    if (cContext.tStatInfo.szNick == "#") return GETUSERDATA_RESULT.NO_NICK;
                }
                else
                {
                    return GETUSERDATA_RESULT.BANNED;
                }
            }
            else if (szHoldType == "H")
            {
                return GETUSERDATA_RESULT.FOREVER_BANNED;
            }
            else if (szHoldType == "E")
            {
                return GETUSERDATA_RESULT.FOREVER_BANNED;
            }
            else
            {
                return GETUSERDATA_RESULT.NO_ACCESS;
            }
            
            return GETUSERDATA_RESULT.SUCCESS;
        }

        #region Base Methods
        
        // Queries ...
        
        public static bool IsDatabaseReady()
        {
            return m_databaseMgr != null;
        }

        public static bool ExecuteGameQuery(E_GDBGW_DEFS eQuery, out CGDBGWParser cParser, params object[] aParams)
        {
            if (m_databaseMgr == null)
            {
                cParser = null;
                return false;
            }

            return m_databaseMgr.ExecuteQuery(
                eQuery,
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetGameDBAlias(),
                out cParser,
                aParams);
        }
        
        public static bool ExecuteGuildQuery(E_GDBGW_DEFS eQuery, out CGDBGWParser cParser, params object[] aParams)
        {
            if (m_databaseMgr == null)
            {
                cParser = null;
                return false;
            }

            return m_databaseMgr.ExecuteQuery(
                eQuery,
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetGuildDBAlias(),
                out cParser,
                aParams);
        }
        
        public static bool ExecuteEventQuery(E_GDBGW_DEFS eQuery, out CGDBGWParser cParser, params object[] aParams)
        {
            if (m_databaseMgr == null)
            {
                cParser = null;
                return false;
            }

            return m_databaseMgr.ExecuteQuery(
                eQuery,
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetEventDBAlias(),
                out cParser,
                aParams);
        }
        
        public static bool ExecuteLogQuery(E_GDBGW_DEFS eQuery, out CGDBGWParser cParser, params object[] aParams)
        {
            if (m_databaseMgr == null)
            {
                cParser = null;
                return false;
            }

            return m_databaseMgr.ExecuteQuery(
                eQuery,
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetLogDBAlias(),
                out cParser,
                aParams);
        }
        
        public static bool AExecuteGameQuery(
            E_GDBGW_DEFS eQueryId,
            CGDBGWRunner.AsyncExecuteQueryCallback pCallback,
            object objPassThruParam,
            params object[] aParams)
        {
            if (m_databaseMgr == null) return false;

            return m_databaseMgr.AExecuteQuery(
                eQueryId, 
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetGameDBAlias(),
                pCallback, 
                objPassThruParam, 
                aParams);
        }
        
        public static bool AExecuteGuildQuery(
            E_GDBGW_DEFS eQueryId,
            CGDBGWRunner.AsyncExecuteQueryCallback pCallback,
            object objPassThruParam,
            params object[] aParams)
        {
            if (m_databaseMgr == null) return false;

            return m_databaseMgr.AExecuteQuery(
                eQueryId, 
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetGuildDBAlias(),
                pCallback, 
                objPassThruParam, 
                aParams);
        }
        
        public static bool AExecuteEventQuery(
            E_GDBGW_DEFS eQueryId,
            CGDBGWRunner.AsyncExecuteQueryCallback pCallback,
            object objPassThruParam,
            params object[] aParams)
        {
            if (m_databaseMgr == null) return false;

            return m_databaseMgr.AExecuteQuery(
                eQueryId, 
                GDBGW_PK.PRIORITY.Normal,
                CServerConfig.GetEventDBAlias(),
                pCallback, 
                objPassThruParam, 
                aParams);
        }
        
        public static bool AExecuteLogQuery(
            E_GDBGW_DEFS eQueryId,
            CGDBGWRunner.AsyncExecuteQueryCallback pCallback,
            object objPassThruParam,
            params object[] aParams)
        {
            if (m_databaseMgr == null) return false;

            return m_databaseMgr.AExecuteQuery(
                eQueryId, 
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetLogDBAlias(),
                pCallback, 
                objPassThruParam, 
                aParams);
        }
        
        // Stored Procedure ...
        
        public static bool ExecuteGameSP(E_GDBGW_DEFS eQuery, out CGDBGWParser cParser, params object[] aParams)
        {
            if (m_databaseMgr == null)
            {
                cParser = null;
                return false;
            }

            return m_databaseMgr.ExecuteSP(
                eQuery, 
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetGameDBAlias(),
                out cParser,
                aParams);
        }
        
        public static bool ExecuteGuildSP(E_GDBGW_DEFS eQuery, out CGDBGWParser cParser, params object[] aParams)
        {
            if (m_databaseMgr == null)
            {
                cParser = null;
                return false;
            }

            return m_databaseMgr.ExecuteSP(
                eQuery,
                GDBGW_PK.PRIORITY.Normal,
                CServerConfig.GetGuildDBAlias(),
                out cParser,
                aParams);
        }
        
        public static bool ExecuteEventSP(E_GDBGW_DEFS eQuery, out CGDBGWParser cParser, params object[] aParams)
        {
            if (m_databaseMgr == null)
            {
                cParser = null;
                return false;
            }

            return m_databaseMgr.ExecuteSP(
                eQuery,
                GDBGW_PK.PRIORITY.Normal,
                CServerConfig.GetEventDBAlias(),
                out cParser,
                aParams);
        }
        
        public static bool ExecuteLogSP(E_GDBGW_DEFS eQuery, out CGDBGWParser cParser, params object[] aParams)
        {
            if (m_databaseMgr == null)
            {
                cParser = null;
                return false;
            }

            return m_databaseMgr.ExecuteSP(
                eQuery,
                GDBGW_PK.PRIORITY.Normal,
                CServerConfig.GetLogDBAlias(),
                out cParser,
                aParams);
        }
        
        public static bool AExecuteGameSP(
            E_GDBGW_DEFS eQueryId,
            CGDBGWRunner.AsyncExecuteQueryCallback pCallback,
            object objPassThruParam,
            params object[] aParams)
        {
            if (m_databaseMgr == null) return false;

            return m_databaseMgr.AExecuteSP(
                eQueryId, 
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetGameDBAlias(), 
                pCallback,
                objPassThruParam, 
                aParams);
        }
        
        public static bool AExecuteGuildSP(
            E_GDBGW_DEFS eQueryId,
            CGDBGWRunner.AsyncExecuteQueryCallback pCallback,
            object objPassThruParam,
            params object[] aParams)
        {
            if (m_databaseMgr == null) return false;

            return m_databaseMgr.AExecuteSP(
                eQueryId, 
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetGuildDBAlias(), 
                pCallback,
                objPassThruParam, 
                aParams);
        }
        
        public static bool AExecuteEventSP(
            E_GDBGW_DEFS eQueryId,
            CGDBGWRunner.AsyncExecuteQueryCallback pCallback,
            object objPassThruParam,
            params object[] aParams)
        {
            if (m_databaseMgr == null) return false;

            return m_databaseMgr.AExecuteSP(
                eQueryId, 
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetEventDBAlias(), 
                pCallback,
                objPassThruParam, 
                aParams);
        }
        
        public static bool AExecuteLogSP(
            E_GDBGW_DEFS eQueryId,
            CGDBGWRunner.AsyncExecuteQueryCallback pCallback,
            object objPassThruParam,
            params object[] aParams)
        {
            if (m_databaseMgr == null) return false;

            return m_databaseMgr.AExecuteSP(
                eQueryId, 
                GDBGW_PK.PRIORITY.Normal, 
                CServerConfig.GetLogDBAlias(), 
                pCallback,
                objPassThruParam, 
                aParams);
        }
        
        // End
        
        public static bool InitDatabaseManager(string szServerName, string szRemoteIP)
        {
            if (m_databaseMgr != null)
            {
                CServerLog.GetLogger().error("InitDatabaseManager() - m_databaseMgr already!!!");
                return false;
            }
            
            m_databaseMgr = new CDBGWManager(szServerName, szRemoteIP);
            return m_databaseMgr.Init();
        }

        public static string GetQueryString(E_GDBGW_DEFS eQuery)
        {
            return m_databaseMgr?.GetReqStr(eQuery);
        }
        
        public static void Terminate()
        {
            m_databaseMgr?.Terminate();
        }
        
        #endregion
    }
}