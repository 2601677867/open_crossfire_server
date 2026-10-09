using System;
using System.Threading;
using Commons.Log;
using Network.Packet;
using Network.Protocol;

namespace PMSConn
{
    public static class CPMSConnNetworkHandler
    {
        public static readonly AutoResetEvent sm_connectResultEvent = new AutoResetEvent(false);
        private static CPMSConn g_pApp;
        
        public static void SetThis(CPMSConn pClass)
        {
            g_pApp = pClass;
        }
        
        private static void OnNetworkMsg(byte nSecond, byte nThird, byte nFourth, byte nFifth, byte[] aData)
        {
            var cPacket = new CBufferReader(aData);
            
            switch (nSecond)
            {
                case HA_PK.PROTOCOL_GS_INIT_RESULT:
                    var tInitRes = cPacket.CopyDataFromUserArea<HA_PK.PROTO_GS_INIT_RESULT>();
                    
                    switch (tInitRes.m_eResult)
                    {
                        case HA_PK.PROTO_GS_INIT_RESULT.EResult.WRONG_PW:
                            g_pApp.PrintInfo("[PROTOCOL_PMS_CONNECT_RESULT] Failed to init");
                            g_pApp.SetLastErrorEvent(E_LAST_ERROR_EVENT.WRONG_CONNECT_PASSWORD);
                            break;
                        case HA_PK.PROTO_GS_INIT_RESULT.EResult.UNKNOWN_ERROR:
                            g_pApp.PrintInfo("[PROTOCOL_PMS_CONNECT_RESULT] Unknown Error");
                            g_pApp.SetLastErrorEvent(E_LAST_ERROR_EVENT.CONNECT_UNKNOWN_ERROR);
                            break;
                        case HA_PK.PROTO_GS_INIT_RESULT.EResult.SUCCESS:
                            g_pApp.m_nGsid = tInitRes.m_nGSID;
                            break;
                        default:
                            g_pApp.PrintInfo("[PROTOCOL_PMS_CONNECT_RESULT] Unknown PROTO_PMS_INIT_RESULT.m_eResult");
                            break;
                    }

                    sm_connectResultEvent.Set();

                    break;
                case HA_PK.PROTOCOL_GS_HEARTBEAT:
                    var tHeartBeat = cPacket.CopyDataFromUserArea<HA_PK.msgPMSHeartBeatAns_Tag>();
                    Interlocked.Exchange(ref g_pApp.m_nReqNum, tHeartBeat.m_nReqNum);
                    g_pApp.PrintInfo("[PMSConn] OnRecv HeartBeatReq Message : ReqNo. {0}", g_pApp.m_nReqNum);
                    break;
                default:
                    CPublicLogger.GetLogger().SettingCheck($"[CPMSConnNetworkHandler::OnNetworkMsg] Invalid Packet Second {nSecond} Third {nThird}");
                    break;
            }
        }
        
        #region BaseNetworkHandler
        
        public static void OnReceiveData(byte[] buff, double recv_time)
        {
            // Copy the header secion out
            var bufHeader = new byte[5];
            Buffer.BlockCopy(buff, 0, bufHeader, 0, 5);
            
            // Get headers
            var nFirst = bufHeader[0];
            var nSecond = bufHeader[1];
            var nThird = bufHeader[2];
            var nFourth = bufHeader[3];
            var nFifth = bufHeader[4];

            // Copy data parts to a new buffer array
            var bufData = new byte[buff.Length - 5];
            Array.Copy(buff, 5, bufData, 0, buff.Length - 5);
            
            if (nFifth == HA_PK.PROTOCOL_HA_FIRST_CLASS)
            {
                OnNetworkMsg(nSecond, nThird, nFourth, nFifth, bufData);
            }
            else
            {
                CPublicLogger.GetLogger().etcinfo($"PMSConn Invalid Packet First {nFirst}");
            }
        }
        
        #endregion
    }
}