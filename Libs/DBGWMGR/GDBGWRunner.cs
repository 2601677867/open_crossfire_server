using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Commons.Log;
using Commons.Native;
using Network.Packet;
using Network.Protocol;
using static Network.Protocol.P_SZ;

/*
 * Author: Red_K
 */
namespace DBGWMGR
{
    public class CGDBGWRunner : CGDBGWNetworkBase
    {
        #region Properties
        
        private volatile int m_nASync;
        private volatile int m_nSync;
        
        private volatile int m_nSeqNum;
        
        public PACKET_DATA m_pConnectData;
        public PACKET_DATA m_pInitData;

        private int m_nRecvTimeout;
        private string[] m_aQueryList;
        
        private const int MAX_RECV_COUNT = 10000;
        private readonly PACKET_DATA[] m_aResponses = new PACKET_DATA[MAX_RECV_COUNT];
        private readonly object[] m_aLocks = new object[MAX_RECV_COUNT];
        
        private Thread m_pAsyncRunnerThread;
        public readonly BlockingCollection<AsyncExecuteQueryRequest> m_queueQuery = new BlockingCollection<AsyncExecuteQueryRequest>();

        public delegate void AsyncExecuteQueryCallback(object objPassThruParam, CGDBGWParser cParser, int nErrCode);
        
        #endregion
        
        #region Constructor

        public CGDBGWRunner(string szClientIP, ushort usClientPort, int nRecvTimeout, string[] aQueryList) : base(szClientIP, usClientPort, nRecvTimeout)
        {
            Interlocked.Exchange(ref m_nSeqNum, 0);
            Interlocked.Exchange(ref m_nSync, 0);
            Interlocked.Exchange(ref m_nASync, 0);
            
            m_pConnectData = new PACKET_DATA(false);
            m_pInitData = new PACKET_DATA(false);

            // allocate memories for packets
            for (var i = 0; i < MAX_RECV_COUNT; i++)
            {
                m_aResponses[i] = new PACKET_DATA(false);
                m_aLocks[i] = new object();
            }

            m_nRecvTimeout = nRecvTimeout;
            m_aQueryList = aQueryList;
        }
        
        #endregion

        #region Shared

        protected override void OnClose()
        {
            Interlocked.Exchange(ref m_nSync, 0);
            Interlocked.Exchange(ref m_nASync, 0);
            
            m_pConnectData = new PACKET_DATA(false);
            m_pInitData = new PACKET_DATA(false);
        }

        public ushort GetPort()
        {
            return m_cClient.GetPort();
        }
        
        public bool ManagerInit()
        {
            var iStartTick = NativeUtil.GetTickCount();
            while (NativeUtil.GetTickCount() - iStartTick < 30000)
            {
                if (m_cClient.Connect() && Authenticate())
                {
                    CPublicLogger.GetLogger().info("Success gDBGW ManagerInit");
                    return true;
                }
            }

            return false;
        }

        protected override bool Authenticate()
        {
            ResetHeartbeatCount();
            
            byte byListPacketCount = 1;

            // make sure all the properties we need are reset properly
            var aQueryListPackets = new CPacket[50];

            // Init gDBGW
            var cPacket = new CPacketWriter();
            cPacket.SetFirstClass(GDBGW_PK.PROTOCOL_GDBGW);
            cPacket.SetSecondClass(GDBGW_PK.PROTOCOL_GS_QUERY_LIST);

            for (var i = 0; i < m_aQueryList.Length; i ++)
            {
                cPacket.WriteInt(i + 1);
                cPacket.WriteString(m_aQueryList[i]);

                if (i != m_aQueryList.Length - 1 &&
                    cPacket.GetRecordedSize() + 4 + m_aQueryList[i + 1].Length + 1 >= 
                    LENGTH_SOCKET_BUFFER - PROTOCOL_NON_USER_AREA_SIZE)
                {
                    cPacket.WriteInt(-1); // end
                    aQueryListPackets[byListPacketCount - 1] = cPacket;
                    byListPacketCount++;
                    cPacket = new CPacketWriter();
                    cPacket.SetFirstClass(GDBGW_PK.PROTOCOL_GDBGW);
                    cPacket.SetSecondClass(GDBGW_PK.PROTOCOL_GS_QUERY_LIST);
                }
            }

            if (byListPacketCount == 1)
            {
                cPacket.WriteInt(-1);
                aQueryListPackets[byListPacketCount - 1] = cPacket;
            }
            else if (cPacket.GetRecordedSize() > 0)
            {
                cPacket.WriteInt(-1); // end
                aQueryListPackets[byListPacketCount - 1] = cPacket;
            }
            else
            {
                byListPacketCount = 1;
            }

            var tConnect = new GDBGW_PK.PROTO_REQUEST_GS_CONNECT {byPacketCount = byListPacketCount};
            var cConnectPacket = new CPacket();
            cConnectPacket.SetFirstClass(GDBGW_PK.PROTOCOL_GDBGW);
            cConnectPacket.SetSecondClass(GDBGW_PK.PROTOCOL_REQUEST_GS_CONNECT);
            cConnectPacket.CopyToUserDataArea(tConnect);
            m_cClient.SendPacket(cConnectPacket);

            if (m_pConnectData.Signal.WaitOne(m_nRecvTimeout, false))
            {
                var gBuffer = m_pConnectData.GetLastBuffer();
                m_pConnectData.Reset();
                if (gBuffer == null || gBuffer.Length != 4) return false;

                var pPackageReader = new CBufferReader(gBuffer);
                var eResult = (GDBGW_PK.PROTO_REQUEST_GS_CONNECT_RESULT.RESULT) pPackageReader.ReadInt();

                if (eResult != GDBGW_PK.PROTO_REQUEST_GS_CONNECT_RESULT.RESULT.SUCCESS)
                {
                    if (eResult == GDBGW_PK.PROTO_REQUEST_GS_CONNECT_RESULT.RESULT.FAIL)
                    {
                        CPublicLogger.GetLogger().error("[ INITIAL DBGWMGR ] Not allowed!!!");
                        GDBGWUtils.PrintManagerInfo($"CSockThread::Authenticate() Failed! serverIP:{m_cClient.GetAddress()}" +
                                                    $" ErrorCode:{(int) eResult}");
                    }
                    else if (eResult == GDBGW_PK.PROTO_REQUEST_GS_CONNECT_RESULT.RESULT.ERROR)
                    {
                        CPublicLogger.GetLogger().error("[ INITIAL DBGWMGR ] GDBGW EXCEPTION IN GS CONNECT");
                        GDBGWUtils.PrintManagerInfo($"CSockThread::Authenticate() Failed! serverIP:{m_cClient.GetAddress()}" +
                                                         $" ErrorCode:{(int) eResult}");
                    }

                    return false;
                }

            }
            else // timeout
            {
                return false;
            }

            foreach (var cNewPacket in aQueryListPackets)
            {
                if (cNewPacket != null && cNewPacket.GetRecordedSize() > 0)
                {
                    m_cClient.SendPacket(cNewPacket);
                }
            }
            
            if (m_pInitData.Signal.WaitOne(m_nRecvTimeout, false))
            {
                var gBuffer = m_pInitData.GetLastBuffer();
                m_pInitData.Reset();
                if (gBuffer == null || gBuffer.Length != 4) return false;

                var pPackageReader = new CBufferReader(gBuffer);
                var eResult = (GDBGW_PK.PROTO_GS_QUERY_LIST_RESULT.RESULT) pPackageReader.ReadInt();

                if (eResult != GDBGW_PK.PROTO_GS_QUERY_LIST_RESULT.RESULT.RECEIVED_ALL_ACK)
                {
                    if (eResult == GDBGW_PK.PROTO_GS_QUERY_LIST_RESULT.RESULT.ERROR)
                    {
                        GDBGWUtils.PrintManagerInfo(
                            "CSockThread::Authenticate() Failed! serverIP:{0} ErrorCode:{1}",
                            m_cClient.GetAddress(),
                            (int) eResult);
                        CPublicLogger.GetLogger().error("[ INITIAL DBGWMGR ] GDBGW EXCEPTION IN QUERY LIST");
                    }

                    return false;
                }
            }
            else // timeout
            {
                return false;
            }

            StartHeartbeat();
            StartAsyncRunnerThread();

            GDBGWUtils.PrintManagerInfo(
                "CSockThread::Authenticate() Success. serverIP:{0} ErrorCode:{1}",
                m_cClient.GetAddress(), 0);

            return true;
        }
        
        #endregion
        
        #region Structs

        public struct AsyncExecuteQueryRequest
        {
            public E_GDBGW_DEFS eQueryId;
            public string szDBAlias;
            public GDBGW_PK.PRIORITY ePriority;
            public object objPassThruParam;
            public EAsyncExecuteType eExecuteType;
            public AsyncExecuteQueryCallback Callback;
            public object[] aParams;
        }
        
        #endregion
        
        #region Enums

        public enum EAsyncExecuteType : byte
        {
            Text,
            StoredProcedure,
            INTERNAL_TERMINATE
        }

        #endregion
        
        #region Process Packet

        private void OnExecuteQueryResult(byte byThird, byte[] aBuff)
        {
            switch (byThird)
            {
                case GDBGW_PK.PROTOCOL_EXECUTE_QUERY_RESULT_HEADER:
                {
                    if (aBuff.Length < 4) return;

                    var pPackageReader = new CBufferReader(aBuff);
                    var nQuerySeq = pPackageReader.ReadInt();

                    //PublicLogger.GetLogger().debug($"DBGWMGR_OnReceiveData, EXECUTE_QUERY_HEADER nQuerySeq {nQuerySeq}");

                    // remove query seq from the data buffer
                    var newBufData = new byte[aBuff.Length - 4];
                    Array.Copy(aBuff, 4, newBufData, 0, aBuff.Length - 4);
                
                    m_aResponses[nQuerySeq].AddToBuffer(newBufData);
                    break;
                }
                case GDBGW_PK.PROTOCOL_EXECUTE_QUERY_RESULT_DATA:
                {
                    if (aBuff.Length < 4) return;

                    var pPackageReader = new CBufferReader(aBuff);
                    var nQuerySeq = pPackageReader.ReadInt();

                    //PublicLogger.GetLogger().debug($"DBGWMGR_OnReceiveData, EXECUTE_QUERY_DATA nQuerySeq {nQuerySeq}");

                    // remove query seq from the data buffer
                    var newBufData = new byte[aBuff.Length - 4];
                    Array.Copy(aBuff, 4, newBufData, 0, aBuff.Length - 4);

                    m_aResponses[nQuerySeq].AddToBuffer(newBufData);
                    Interlocked.Increment(ref m_aResponses[nQuerySeq].ReceivedPacketCount);
                    break;
                }
                case GDBGW_PK.PROTOCOL_EXECUTE_QUERY_RESULT_DATA_EXT:
                {
                    if (aBuff.Length < 4) return;

                    var pPackageReader = new CBufferReader(aBuff);
                    var nQuerySeq = pPackageReader.ReadInt();

                    //PublicLogger.GetLogger().debug($"DBGWMGR_OnReceiveData, EXECUTE_QUERY_DATA_EXT nQuerySeq {nQuerySeq}");

                    // remove query seq from the data buffer
                    var newBufData = new byte[aBuff.Length - 4];
                    Array.Copy(aBuff, 4, newBufData, 0, aBuff.Length - 4);

                    m_aResponses[nQuerySeq].AddToBuffer(newBufData);
                    Interlocked.Increment(ref m_aResponses[nQuerySeq].ReceivedPacketCount);
                    break;
                }
                case GDBGW_PK.PROTOCOL_EXECUTE_QUERY_RESULT_DATA_END:
                {
                    if (aBuff.Length < 4) return;

                    var pPackageReader = new CBufferReader(aBuff);
                    var nQuerySeq = pPackageReader.ReadInt();
                    var nPacketCount = pPackageReader.ReadInt();
                
                    //PublicLogger.GetLogger().debug($"DBGWMGR_OnReceiveData, EXECUTE_QUERY_DATA_END nQuerySeq {nQuerySeq}");
                
                    if (m_aResponses[nQuerySeq].ReceivedPacketCount < nPacketCount + 1)
                    {
                        GDBGWUtils.PrintManagerInfo($"*** CMsgTable::AddSyncRcvMsg failed index:{nQuerySeq}");
                        CPublicLogger.GetLogger()
                            .error(
                                $"[DBGWMGR::OnReceiveData()] nQuerySeq {nQuerySeq} not enough data packets in EXECUTE_QUERY_DATA_END " +
                                $"(recv:{m_aResponses[nQuerySeq].ReceivedPacketCount}, supposed:{nPacketCount + 1})");
                    }

                    m_aResponses[nQuerySeq].Signal.Set();
                    break;
                }
            }
        }

        protected override void InternalProcessPacket(byte byFirst, byte bySecond, byte byThird, byte[] aBuff)
        {
            if (byFirst != GDBGW_PK.PROTOCOL_GDBGW) return;

            switch (bySecond)
            {
                case GDBGW_PK.PROTOCOL_REQUEST_GS_CONNECT_RESULT:
                    //PublicLogger.GetLogger().debug("DBGWMGR_OnReceiveData, GS_CONNECT_RESULT");
                    m_pConnectData.SetLastBuffer(aBuff);
                    m_pConnectData.Signal.Set();
                    break;
                case GDBGW_PK.PROTOCOL_HEARTBEAT:
                    //PublicLogger.GetLogger().debug("DBGWMGR_OnReceiveData, GS_HEARTBEAT");
                    ResetHeartbeatCount();
                    break;
                case GDBGW_PK.PROTOCOL_GS_QUERY_LIST_RESULT:
                    //PublicLogger.GetLogger().debug("DBGWMGR_OnReceiveData, GS_QUERY_RECIEVED_ALL");
                    m_pInitData.SetLastBuffer(aBuff);
                    m_pInitData.Signal.Set();
                    break;
                case GDBGW_PK.PROTOCOL_EXECUTE_QUERY_RESULT:
                    OnExecuteQueryResult(byThird, aBuff);
                    break;
                default:
                    GDBGWUtils.PrintManagerInfo($"recv packet not in database {byFirst} {bySecond} {byThird}");
                    CPublicLogger.GetLogger().warn($"GDBGW___InternalProcessPacket, recv packet not in database {byFirst} {bySecond} {byThird}");
                    break;
                
            }
        }
        
        #endregion
        
        #region AsyncRunner

        public bool IsAsyncRunnerThreadAlive()
        {
            return m_pAsyncRunnerThread != null && m_pAsyncRunnerThread.IsAlive;
        }

        public void StartAsyncRunnerThread()
        {
            try
            {
                m_pAsyncRunnerThread ??= new Thread(T_ASYNCRUNNER);

                if (!m_pAsyncRunnerThread.IsAlive)
                {
                    m_pAsyncRunnerThread.Start();
                }
            }
            catch
            {
                CPublicLogger.GetLogger().warn(
                    "{0} - unable to start AsyncRunner thread",
                    "CGDBGWNetworkBase::StartAsyncRunnerThread");
            }
        }

        public void Stop()
        {
            StopHeartbeat();
            StopAsyncRunnerThread();
        }
        
        public void StopAsyncRunnerThread()
        {
            m_queueQuery.Add(new AsyncExecuteQueryRequest { eExecuteType = EAsyncExecuteType.INTERNAL_TERMINATE });
        }
        
        private void T_ASYNCRUNNER()
        {
            while (true)
            {
                foreach (var tExRequest in m_queueQuery.GetConsumingEnumerable())
                {
                    try
                    {
                        switch (tExRequest.eExecuteType)
                        {
                            case EAsyncExecuteType.Text:
                            {
                                var nResult = InternalAExecuteQuery(
                                    (int) tExRequest.eQueryId, 
                                    tExRequest.szDBAlias, 
                                    tExRequest.ePriority, 
                                    out var cParser,
                                    tExRequest.aParams);
                                
                                tExRequest.Callback?.Invoke(tExRequest.objPassThruParam, cParser, nResult);
                                break;
                            }
                            case EAsyncExecuteType.StoredProcedure:
                            {
                                var nResult = InternalAExecuteSP(
                                    (int) tExRequest.eQueryId, 
                                    tExRequest.szDBAlias, 
                                    tExRequest.ePriority,
                                    out var cParser,
                                    tExRequest.aParams);
                                
                                tExRequest.Callback?.Invoke(tExRequest.objPassThruParam, cParser, nResult);
                                break;
                            }
                            case EAsyncExecuteType.INTERNAL_TERMINATE:
                            {
                                return;
                            }
                            default:
                                CPublicLogger.GetLogger().warn(
                                    "[CGDBGWRunner::T_ASYNCRUNNER()] tExRequest.eExecuteType NOT DEFINED");
                                break;
                        }
                    }
                    catch (Exception e)
                    {
                        CPublicLogger.GetLogger().error(
                            "[CGDBGWRunner::T_ASYNCRUNNER()] tExRequest Q{0} ERROR {1}\r\n{2}",
                            (int) tExRequest.eQueryId, e.Message, e.StackTrace);
                    }
                }
            }
        }
        
        #endregion
        
        #region Packet Methods

        private void CreateExecuteQueryPacket(out CPacketWriter cPacket,
            GDBGW_PK.QUERYTYPE eQueryType,
            GDBGW_PK.PRIORITY ePriority,
            int nNewSeqNum, int nQueryID, string szDBAlias, params object[] aParams)
        {
            cPacket = new CPacketWriter();
            cPacket.SetFirstClass(GDBGW_PK.PROTOCOL_GDBGW);
            cPacket.SetSecondClass(GDBGW_PK.PROTOCOL_EXECUTE_QUERY);
            
            cPacket.WriteInt(nNewSeqNum);
            cPacket.WriteByte((byte)ePriority);
            cPacket.WriteInt(nQueryID);
            cPacket.WriteString(szDBAlias);
            cPacket.WriteByte((byte)eQueryType);
            cPacket.WriteInt(aParams.Length);
            foreach (var obj in aParams)
            {
                var szParam = obj == null ? "" : obj.ToString();
                /*if (obj is string)
                {
                    // avoid sql injecting... @ 31 Dec 2019
                    // ----------------------------------------
                    // removed @ 27 Sept 2020
                    // - now using bind parameter instead of direct formatting (which prevents sql injecting)
                    // updated @ 4 Sept 2020
                    // performance improve - moved from GDBGW to Library Call
                    szParam = szParam.Replace("'", "''");
                }*/
                
                cPacket.WriteString(szParam);
            }
        }
        
        #endregion

        #region Async Methods

        private int InternalAExecuteSP(
            int nQueryID,
            string szDBAlias,
            GDBGW_PK.PRIORITY ePriority,
            out CGDBGWParser cParser,
            params object[] aParams)
        {
            cParser = null;
            if (!m_cClient.IsRunning())
            {
                CPublicLogger.GetLogger().error("[CGDBGWParser::InitDBResultString] Error : Connection failure");
                return -10000;
            }
            
            var nNewSeqNum = Interlocked.Exchange(ref m_nSeqNum, (m_nSeqNum + 1) % MAX_RECV_COUNT);
            var szQuery = m_aQueryList[nQueryID - 1];

            byte[] buff;
            lock (m_aLocks[nNewSeqNum])
            {
                m_aResponses[nNewSeqNum].Reset();

                CreateExecuteQueryPacket(out var cPacket, 
                    GDBGW_PK.QUERYTYPE.StoredProcedure,
                    ePriority,
                    nNewSeqNum, nQueryID, szDBAlias, aParams);

                m_cClient.SendPacket(cPacket, out var bIsSockSent);

                if (!m_aResponses[nNewSeqNum].Signal.WaitOne(m_nRecvTimeout, false))
                {
                    GDBGWUtils.PrintManagerInfo($"*** CASyncThread::StartEx(). Async Msg timeout index:{nNewSeqNum}, IsSockSent:{bIsSockSent.ToString().ToUpper()}, Query String: {szQuery}");
                    GDBGWUtils.PrintAsyncQueryError(nQueryID, "Async execute query timeout", szQuery, aParams);
                    return -10001;
                }

                buff = m_aResponses[nNewSeqNum].GetLastBuffer();
                m_aResponses[nNewSeqNum].Reset();
            }

            var cBufferReader = new CBufferReader(buff);
            var nResult = cBufferReader.ReadShort();
            var szError = cBufferReader.ReadString();
            if (nResult != 0)
            {
                GDBGWUtils.PrintAsyncQueryError(nQueryID, szError, szQuery, aParams);
                return nResult;
            }
            
            var nTime = cBufferReader.ReadUInt();

            cParser = new CGDBGWParser();
            if (!cParser.DBReadData(cBufferReader)) return -10002;
            
            Interlocked.Increment(ref m_nASync);
            GDBGWUtils.PrintTimeInfo(
                "Run InternalAExecuteSP() Time => {0}ms, SYNC : {1}, ASYNC : {2}, Priority : {3}",
                nTime, m_nSync, m_nASync, (int)ePriority);

            if (nTime >= GDBGW_TIMEOUT_LIMIT)
            {
                GDBGWUtils.PrintTimeInfo(
                    "[InternalAExecuteSP::CheckTimeInfo()] => Dalay {0}ms, Query : {1}",
                    nTime,
                    szQuery);
                GDBGWUtils.PrintDelayInfo("{0}\t{1}\t{2}", "ASYNC", nTime, szQuery);
            }
                            
            return 0;
        }

        private int InternalAExecuteQuery(
            int nQueryID,
            string szDBAlias,
            GDBGW_PK.PRIORITY ePriority,
            out CGDBGWParser cParser,
            params object[] aParams)
        {
            cParser = null;
            if (!m_cClient.IsRunning())
            {
                CPublicLogger.GetLogger().error("[CGDBGWParser::InitDBResultString] Error : Connection failure");
                return -10000;
            }
            
            var nNewSeqNum = Interlocked.Exchange(ref m_nSeqNum, (m_nSeqNum + 1) % MAX_RECV_COUNT);
            var szQuery = m_aQueryList[nQueryID - 1];

            byte[] buff;
            lock (m_aLocks[nNewSeqNum])
            {
                m_aResponses[nNewSeqNum].Reset();
                
                CreateExecuteQueryPacket(out var cPacket, 
                    GDBGW_PK.QUERYTYPE.Text,
                    ePriority,
                    nNewSeqNum, nQueryID, szDBAlias, aParams);
                
                m_cClient.SendPacket(cPacket, out var bIsSockSent);

                if (!m_aResponses[nNewSeqNum].Signal.WaitOne(m_nRecvTimeout, false))
                {
                    GDBGWUtils.PrintManagerInfo($"*** CASyncThread::StartEx(). Async Msg timeout index:{nNewSeqNum}, IsSockSent:{bIsSockSent.ToString().ToUpper()}, Query String: {szQuery}");
                    GDBGWUtils.PrintAsyncQueryError(nQueryID, "Async execute query timeout", szQuery, aParams);
                    return -10001;
                }

                buff = m_aResponses[nNewSeqNum].GetLastBuffer();
                m_aResponses[nNewSeqNum].Reset();
            }

            var cBufferReader = new CBufferReader(buff);
            var nResult = cBufferReader.ReadShort();
            var szError = cBufferReader.ReadString();
            if (nResult != 0)
            {
                GDBGWUtils.PrintAsyncQueryError(nQueryID, szError, szQuery, aParams);
                return nResult;
            }
            
            var nTime = cBufferReader.ReadUInt();
            cParser = new CGDBGWParser();
            
            if (!cParser.DBReadData(cBufferReader)) return -10002;
                            
            Interlocked.Increment(ref m_nASync);
            GDBGWUtils.PrintTimeInfo(
                "Run InternalAExecuteQuery() Time => {0}ms, SYNC : {1}, ASYNC : {2}, Priority : {3}",
                nTime, m_nSync, m_nASync, (int)ePriority);

            if (nTime >= GDBGW_TIMEOUT_LIMIT)
            {
                GDBGWUtils.PrintTimeInfo(
                    "[InternalAExecuteQuery::CheckTimeInfo()] => Dalay {0}ms, Query : {1}",
                    nTime,
                    szQuery);
                GDBGWUtils.PrintDelayInfo("{0}\t{1}\t{2}", "ASYNC", nTime, szQuery);
            }
                            
            return 0;
        }
        
        #endregion

        #region Sync Methods

        public bool InternalExecuteSP(
            int nQueryID,
            string szDBAlias,
            GDBGW_PK.PRIORITY ePriority, 
            out CGDBGWParser cParser,
            params object[] aParams)
        {
            cParser = null;
            
            if (!m_cClient.IsRunning())
            {
                CPublicLogger.GetLogger().error("[CGDBGWParser::InitDBResultString] Error : Connection failure");
                return false;
            }
            
            var nNewSeqNum = Interlocked.Exchange(ref m_nSeqNum, (m_nSeqNum + 1) % MAX_RECV_COUNT);
            var szQuery = m_aQueryList[nQueryID - 1];

            byte[] buff;
            lock (m_aLocks[nNewSeqNum])
            {
                m_aResponses[nNewSeqNum].Reset();
                
                CreateExecuteQueryPacket(out var cPacket, 
                    GDBGW_PK.QUERYTYPE.StoredProcedure,
                    ePriority,
                    nNewSeqNum, nQueryID, szDBAlias, aParams);

                m_cClient.SendPacket(cPacket, out var bIsSockSent);

                if (!m_aResponses[nNewSeqNum].Signal.WaitOne(m_nRecvTimeout, false))
                {
                    GDBGWUtils.PrintManagerInfo($"*** CControl::Execute TIMEOUT failed index:{nNewSeqNum}, IsSockSent:{bIsSockSent.ToString().ToUpper()}, Query String: {szQuery}");
                    GDBGWUtils.PrintSyncQueryError(nQueryID, "Sync execute query timeout", szQuery, aParams);
                    return false;
                }

                buff = m_aResponses[nNewSeqNum].GetLastBuffer();
                m_aResponses[nNewSeqNum].Reset();
            }

            var cBufferReader = new CBufferReader(buff);
            var nResult = cBufferReader.ReadShort();
            var szError = cBufferReader.ReadString();
            if (nResult != 0)
            {
                GDBGWUtils.PrintSyncQueryError(nQueryID, szError, szQuery, aParams);
                return false;
            }
            
            var nTime = cBufferReader.ReadUInt();
            cParser = new CGDBGWParser();

            if (!cParser.DBReadData(cBufferReader)) return false;
                            
            Interlocked.Increment(ref m_nSync);
            GDBGWUtils.PrintTimeInfo(
                "Run InternalExecuteSP() Time => {0}ms, SYNC : {1}, ASYNC : {2}, Priority : {3}",
                nTime, m_nSync, m_nASync, (int)ePriority);

            if (nTime >= GDBGW_TIMEOUT_LIMIT)
            {
                GDBGWUtils.PrintTimeInfo(
                    "[InternalExecuteSP::CheckTimeInfo()] => Dalay {0}ms, Query : {1}",
                    nTime,
                    szQuery);
                GDBGWUtils.PrintDelayInfo("{0}\t{1}\t{2}", "SYNC", nTime, szQuery);
            }
                            
            return true;
        }
        
        public bool InternalExecuteQuery(
            int nQueryID, 
            string szDBAlias,
            GDBGW_PK.PRIORITY ePriority,
            out CGDBGWParser cParser, 
            params object[] aParams)
        {
            cParser = null;
            
            if (!m_cClient.IsRunning())
            {
                CPublicLogger.GetLogger().error("[CGDBGWParser::InitDBResultString] Error : Connection failure");
                return false;
            }
            
            var nNewSeqNum = Interlocked.Exchange(ref m_nSeqNum, (m_nSeqNum + 1) % MAX_RECV_COUNT);
            var szQuery = m_aQueryList[nQueryID - 1];

            byte[] buff;
            lock (m_aLocks[nNewSeqNum])
            {
                m_aResponses[nNewSeqNum].Reset();
                
                CreateExecuteQueryPacket(out var cPacket, 
                    GDBGW_PK.QUERYTYPE.Text,
                    ePriority,
                    nNewSeqNum, nQueryID, szDBAlias, aParams);

                m_cClient.SendPacket(cPacket, out var bIsSockSent);

                if (!m_aResponses[nNewSeqNum].Signal.WaitOne(m_nRecvTimeout, false))
                {
                    GDBGWUtils.PrintManagerInfo($"*** CControl::Execute TIMEOUT failed index:{nNewSeqNum}, IsSockSent:{bIsSockSent.ToString().ToUpper()}, Query String: {szQuery}");
                    GDBGWUtils.PrintSyncQueryError(nQueryID, "Sync execute query timeout", szQuery, aParams);
                    return false;
                }

                buff = m_aResponses[nNewSeqNum].GetLastBuffer();
                m_aResponses[nNewSeqNum].Reset();
            }

            var cBufferReader = new CBufferReader(buff);
            var nResult = cBufferReader.ReadShort();
            var szError = cBufferReader.ReadString();
            if (nResult != 0)
            {
                GDBGWUtils.PrintSyncQueryError(nQueryID, szError, szQuery, aParams);
                return false;
            }
            
            var nTime = cBufferReader.ReadUInt();
            cParser = new CGDBGWParser();
            
            if (!cParser.DBReadData(cBufferReader)) return false;

            Interlocked.Increment(ref m_nSync);
            GDBGWUtils.PrintTimeInfo(
                "Run InternalExecuteQuery() Time => {0}ms, SYNC : {1}, ASYNC : {2}, Priority : {3}",
                nTime, m_nSync, m_nASync, (int)ePriority);

            if (nTime >= GDBGW_TIMEOUT_LIMIT)
            {
                GDBGWUtils.PrintTimeInfo(
                    "[InternalExecuteQuery::CheckTimeInfo()] => Dalay {0}ms, Query : {1}",
                    nTime,
                    szQuery);
                GDBGWUtils.PrintDelayInfo("{0}\t{1}\t{2}", "SYNC", nTime, szQuery);
            }
                            
            return true;
        }
        
        #endregion
    }
}