using System;
using System.Diagnostics;
using cf_loginsrv.Config;
using cf_loginsrv.Log;
using cf_loginsrv.Util;
using Network.Packet;
using static Network.Protocol.P_SZ;
using static Network.Assert.ProtocolAssert;
using static Network.Protocol.LLSM_PK;

namespace cf_loginsrv.Packet
{
    public class CLLSMgmtNetworkHandler : CBaseNetworkHandler
    {
        private void OnLSConnectResult(CPacket cPacket)
        {
            PROTO_LLSM_LS_CONNECT_RESULT tConnectRet = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tConnectRet) );
            
            if (tConnectRet.eResult == PROTO_LLSM_LS_CONNECT_RESULT.RESULT.FAILED)
            {
                CServerLog.GetLogger().error("LSConenctResult - failed!!!");
            }
            else if (tConnectRet.eResult == PROTO_LLSM_LS_CONNECT_RESULT.RESULT.ALREADY_CONNECTED)
            {
                CServerLog.GetLogger().error("LSConenctResult - already connected!!!");
            }
        }

        private void OnLauncherKeyInfo(CPacket cPacket)
        {
            PROTO_LLSM_LAUNCHER_KEYINFO tLauncherKeyInfo = default;
            PROTOCOL_ASSERT( cPacket.CopyFromUserDataArea(ref tLauncherKeyInfo) );
            
            var tKey = new CSharedVariable.LauncherAuthKey(
                tLauncherKeyInfo.szRSAKey.Substring(22, 172), 
                tLauncherKeyInfo.lUSN,
                tLauncherKeyInfo.szIPAddress);
            
            lock (CSharedVariable.LSKeys)
            {
                CSharedVariable.LSKeys[tLauncherKeyInfo.lUSN] = tKey;
            }
            
            CServerLog.GetLogger().debug(
                "OnLauncherKeyInfo Received : {0} {1}",
                tKey.lUSN, tKey.szKey);
        }
        
        private string OnRcvLLSMgmtMsg(CPacket cPacket)
        {
            switch (cPacket.GetSecondClass())
            {
                case PROTOCOL_LLSM_LS_CONNECT_RESULT:
                    OnLSConnectResult(cPacket);
                    return "LLSM_LS_CONNECT_RESULT";
                case PROTOCOL_LLSM_HEARTBEAT:
                    return "LLSM_HEARTBEAT";
                case PROTOCOL_LLSM_LAUNCHER_KEYINFO:
                    OnLauncherKeyInfo(cPacket);
                    return "LLSM_LAUNCHER_KEYINFO";
            }

            return null;
        }
        
        #region Base Methods

        public void OnNetworkMsg(byte[] buff, double recv_time)
        {
            var cPacket = new CPacket(buff);
            
            // Create our debug info
            if (cPacket.GetReceivedSize() > PROTOCOL_NON_USER_AREA_SIZE)
            {
                //CServerLog.GetLogger().packet(CServer.ByteArr2Hex(buff));
            }
            CServerLog.GetLogger().packet(
                "[ LLSM RECV ][ PACKET First 0x{0:X2} Second 0x{1:X2} Third 0x{2:X2} ]",
                cPacket.GetFirstClass(), cPacket.GetSecondClass(), cPacket.GetThirdClass());
            
            if (cPacket.GetFirstClass() == PROTOCOL_LLSM)
            {
                try
                {
                    var swProcessTime = new Stopwatch();

                    swProcessTime.Reset();
                    swProcessTime.Start();
                    
                    var szProtoName = OnRcvLLSMgmtMsg(cPacket);
                    
                    swProcessTime.Stop();

                    if (!string.IsNullOrEmpty(szProtoName))
                    {
                        CServerLog.GetLogger().packet_info(szProtoName, 
                            CServerConfig.GetLLSMgmtAddr(),
                            CServerConfig.GetMgmtRemotePort(),
                            (int)CMainServer.GetLLSMgmtClient().GetHandle(), 
                            recv_time, 
                            swProcessTime.Elapsed.TotalSeconds, 
                            cPacket.GetFirstClass(), 
                            cPacket.GetSecondClass());
                    }
                }
                catch (Exception e)
                {
                    CServerLog.GetLogger().error(
                        "[CLLSMgmtNetworkHandler::OnNetworkMsg()] OnRcvLLSMgmtMsg Exception - {0}\r\n{1}",
                        e.Message, e.StackTrace);
                }
            }
            else
            {
                CServerLog.GetLogger().etcinfo(
                    "[CLLSMgmtNetworkHandler::OnNetworkMsg()] Invalid Packet First {0}",
                    cPacket.GetFirstClass());
            }
        }
        
        #endregion
    }
}