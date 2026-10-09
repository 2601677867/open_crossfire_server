using System;
using System.Globalization;
using cf_loginsrv.Log;
using cf_loginsrv.Socket;
using Commons;
using DBGWMGR;
using Network.Protocol;
using static DBGWMGR.E_GDBGW_DEFS;

namespace cf_loginsrv.Util
{
    public class FameSystem : CSingleton<FameSystem>
    {
        private DateTime m_dtNextResetTime;
        
        public bool UpdateFameGrade()
        {
            if (DateTime.Now < m_dtNextResetTime) return true;

            var bSuccess = CServerDataManager.AExecuteGameSP(
                STORE_FAME_GRADE_UPDATE, 
                UpdateFameGradeQueryCallBack, STORE_FAME_GRADE_UPDATE, 
                "DUMMY");

            return bSuccess;
        }

        private void UpdateFameGradeQueryCallBack(object objPassThruParam, CGDBGWParser cParser, int nErrCode)
        {
            if (cParser == null) return;
            
            var eQueryId = (E_GDBGW_DEFS) objPassThruParam;
            if (eQueryId != STORE_FAME_GRADE_UPDATE) return;

            if (nErrCode != 0)
            {
                CServerLog.LogFameSystem("[QueryCallBack ExecuteQuery Failed 1][{0}]", nErrCode);
                return;
            }

            if (cParser.IsResultError())
            {
                if (cParser.Result == -1) 
                    CServerLog.PrintPrimaryError("Error... DBTABLE CF_FAME_GRADE_INFO Not exist information.");
                
                CServerLog.LogFameSystem("[QueryCallBack ExecuteQuery Failed 2]");
                return;
            }

            var szNextResetTime = cParser.GetString(1);
            DateTime.TryParseExact(
                szNextResetTime, 
                "yyyyMMddHHmmss", 
                CultureInfo.InvariantCulture,
                DateTimeStyles.None, 
                out m_dtNextResetTime);
            
            CSocketController.GetMgmtServer().BroadcastPacket(
                LMS_PK.PROTOCOL_LOGIN_MGMT, LMS_PK.PROTOCOL_MGMT_FAME_LIST_UPDATE_FINISHED);
            
            CServerLog.LogFameSystem(
                "Reset FameGradeList and send MGMT_FAME_LIST_UPDATE_FINISHED... next Reset time:{0}",
                szNextResetTime);
        }
    }
}