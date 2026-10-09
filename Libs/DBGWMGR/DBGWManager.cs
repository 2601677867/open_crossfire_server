using System;
using System.IO;
using System.Threading;
using Commons.Config;
using Commons.Log;
using Commons.Native;
using Network.Client;
using Network.Packet;
using Network.Protocol;

/*
 * Author: Red_K
 */
namespace DBGWMGR
{
    public class CDBGWManager
    {
        #region Properties
        
        private static readonly string DBGWMGR_CONFIG_PATH = NativeUtil.IsUnix()
            ? "/Users/DBGWMGR.ini"
            : Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.System)) + "\\DBGWMGR.ini";
        
        private readonly string[] m_aQueryList;
        private readonly int m_nRecvTimeout;

        private readonly CGDBGWRunner m_cRunner;

        #endregion

        #region Constructor & Destroyer
        
        public static int FileValidCheck()
        {
            var nQueryNum = Convert.ToInt32(CIniFile.GetIniString("QUERY", "QUERY_NUM", DBGWMGR_CONFIG_PATH));
            for (var i = 0; i < nQueryNum; i++)
            {
                var szQueryRaw = CIniFile.GetIniString("QUERY", "Q" + (i + 1), DBGWMGR_CONFIG_PATH);
                
                //if (!string.IsNullOrEmpty(szQueryRaw) && (!szQueryRaw.Contains("(") || !szQueryRaw.Contains(")")))
                //    return i + 1;
            }

            return 0;
        }

        public CDBGWManager(string szServerName, string szRemoteIP)
        {
            var szClientIP = CIniFile.GetIniString("DBGW", "IP", DBGWMGR_CONFIG_PATH);
            var usClientPort = Convert.ToUInt16(CIniFile.GetIniString("DBGW", "PORT", DBGWMGR_CONFIG_PATH));
            var nRecvTimeout = Convert.ToInt32(CIniFile.GetIniString("TIME", "RECV_TIMEOUT", DBGWMGR_CONFIG_PATH));
            var nQueryNum = Convert.ToInt32(CIniFile.GetIniString("QUERY", "QUERY_NUM", DBGWMGR_CONFIG_PATH));
            
            m_aQueryList = new string[nQueryNum];
            for (var i = 0; i < nQueryNum; i++)
            {
                var szQuery = CIniFile.GetIniString("QUERY", "Q" + (i + 1), DBGWMGR_CONFIG_PATH);
                m_aQueryList[i] = "";
                
                if (!string.IsNullOrEmpty(szQuery)) m_aQueryList[i] = szQuery;
            }

            m_nRecvTimeout = nRecvTimeout == -1 ? 30 * 1000 : nRecvTimeout * 1000;

            GDBGWUtils.SetServerInfo(szRemoteIP, szServerName);
            m_cRunner = new CGDBGWRunner(szClientIP, usClientPort, m_nRecvTimeout, m_aQueryList);
            
            CPublicLogger.GetLogger().GetFormatter().AddChannel("DELAYQUERY", "DB", LOGLEVEL.LEVEL_WARN,
                ConsoleColor.DarkYellow);
            CPublicLogger.GetLogger().GetFormatter().AddChannel("DBGWM_LOG", "DB", LOGLEVEL.LEVEL_INFO,
                ConsoleColor.DarkYellow);
            
            GDBGWUtils.PrintManagerInfo("Server started");
        }

        public void Terminate()
        {
            m_cRunner.Stop();
        }
        
        #endregion

        #region Get Methods

        public string GetReqStr(E_GDBGW_DEFS eReq)
        {
            return m_aQueryList[(int)eReq - 1];
        }
        
        #endregion

        #region Init Methods

        public bool Init()
        {
            GDBGWUtils.PrintManagerInfo("Server started with DBGWMGR.ini");
            GDBGWUtils.PrintManagerInfo("Receive Timeout value is [{0}]seconds", m_nRecvTimeout / 1000);
            GDBGWUtils.PrintManagerInfo("GDBGW Port : {0}", m_cRunner.GetPort());

            if (!m_cRunner.ManagerInit())
            {
                CPublicLogger.GetLogger().error("Failure gDBGW ManagerInit");
                return false;
            }

            return true;
        }

        #endregion

        #region Async Methods

        public bool AExecuteQuery(
            E_GDBGW_DEFS eQueryId, 
            GDBGW_PK.PRIORITY ePriority,
            string szDBAlias,
            CGDBGWRunner.AsyncExecuteQueryCallback pCallback,
            object objPassThruParam,
            params object[] aParams)
        {
            if (!m_cRunner.IsAsyncRunnerThreadAlive())
            {
                CPublicLogger.GetLogger().warn("DBGWMGR_AExecuteQuery, create task failed!!!");
                return false;
            }
            
            m_cRunner.m_queueQuery.Add(new CGDBGWRunner.AsyncExecuteQueryRequest
            {
                eQueryId = eQueryId,
                szDBAlias = szDBAlias,
                ePriority = ePriority,
                objPassThruParam = objPassThruParam,
                eExecuteType = CGDBGWRunner.EAsyncExecuteType.Text,
                Callback = pCallback,
                aParams = aParams
            });

            return true;
        }
        
        public bool AExecuteSP(
            E_GDBGW_DEFS eQueryId, 
            GDBGW_PK.PRIORITY ePriority,
            string szDBAlias,
            CGDBGWRunner.AsyncExecuteQueryCallback pCallback,
            object objPassThruParam,
            params object[] aParams)
        {
            if (!m_cRunner.IsAsyncRunnerThreadAlive())
            {
                CPublicLogger.GetLogger().warn("DBGWMGR_AExecuteSP, create task failed!!!");
                return false;
            }
            
            m_cRunner.m_queueQuery.Add(new CGDBGWRunner.AsyncExecuteQueryRequest
            {
                eQueryId = eQueryId,
                szDBAlias = szDBAlias,
                ePriority = ePriority,
                objPassThruParam = objPassThruParam,
                eExecuteType = CGDBGWRunner.EAsyncExecuteType.StoredProcedure,
                Callback = pCallback,
                aParams = aParams
            });

            return true;
        }

        #endregion

        #region Sync Methods

        public bool ExecuteQuery(
            E_GDBGW_DEFS eQuery, 
            GDBGW_PK.PRIORITY ePriority, 
            string szDBAlias,
            out CGDBGWParser cParser,
            params object[] aParams)
        {
            try
            {
                return m_cRunner.InternalExecuteQuery((int)eQuery, szDBAlias, ePriority, out cParser, aParams);
            }
            catch (Exception)
            {
                cParser = null;
                return false;
            }
        }

        public bool ExecuteSP(
            E_GDBGW_DEFS eQuery,
            GDBGW_PK.PRIORITY ePriority, 
            string szDBAlias,
            out CGDBGWParser cParser,
            params object[] aParams)
        {
            try
            {
                return m_cRunner.InternalExecuteSP((int)eQuery, szDBAlias, ePriority, out cParser, aParams);
            }
            catch (Exception)
            {
                cParser = null;
                return false;
            }
        }

        #endregion
    }
}