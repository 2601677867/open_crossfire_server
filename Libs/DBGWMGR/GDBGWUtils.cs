using System;
using System.Linq;
using Commons.Log;
using Network.Client;

/*
 * Author: Red_K
 */
namespace DBGWMGR
{
    public static class GDBGWUtils
    {
        private static string sm_szRemoteIP;
        private static string sm_szServerName;

        public static void SetServerInfo(string szRemoteIP, string szServerName)
        {
            sm_szRemoteIP = szRemoteIP;
            sm_szServerName = szServerName;
        }

        #region Internal Methods

        private static void __PrintCustomAsyncQueryError(string szType, int nQueryID, string szError, params object[] gDatas)
        {
            CPublicLogger.GetLogger().error($"Execute Query {szType}: Q{nQueryID}");
            CPublicLogger.GetLogger().error($"Error Msg: {szError}");
            var sDatas = gDatas.Aggregate("", (current, s) => current + "|" + s);
            PrintDBError($"[AExecuteQuery Failed] Error query number : {nQueryID}, Error reason : F|-205|0|{szError}{sDatas}");
        }
        
        private static void __PrintCustomSyncQueryError(string szType, int nQueryID, string szError, string sQuery, params object[] gDatas)
        {
            CPublicLogger.GetLogger().error($"Execute Query {szType}: Q{nQueryID}");
            CPublicLogger.GetLogger().error($"Error Msg: {szError}");
            var sDatas = gDatas.Aggregate("", (current, s) => current + "|" + s);
            PrintDBError($"[ExecuteQuery Failed-2][{sQuery}{sDatas}]");
        }

        #endregion

        #region Log Methods
        
        public static void PrintAsyncQueryError(int nQueryID, string szError, params object[] aDatas)
        {
            __PrintCustomAsyncQueryError("Error", nQueryID, szError, aDatas);
        }

        public static void PrintSyncQueryError(int nQueryID, string szError, string szQuery, params object[] aDatas)
        {
            __PrintCustomSyncQueryError("Error", nQueryID, szError, szQuery, aDatas);
        }

        public static void PrintTimeInfo(string context, params object[] args)
        {
            CPublicLogger.GetLogger().PrintToConsole(context, "timeinfo", args);
        }

        public static void PrintDelayInfo(string context, params object[] args)
        {
            CPublicLogger.GetLogger().PrintToConsole(context, "DELAYQUERY", args);
        }

        public static void PrintDBError(string context, params object[] args)
        {
            CPublicLogger.GetLogger().PrintToConsole(context, "1_ERROR", args);
        }
        
        public static void PrintManagerInfo(string context, params object[] args)
        {
            CPublicLogger.GetLogger().PrintToConsole(context, "DBGWM_LOG", args);
        }
        
        #endregion
    }
}