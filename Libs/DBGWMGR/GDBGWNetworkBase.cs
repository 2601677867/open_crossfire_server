using System;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using Commons.Log;
using Network.Client;
using Network.Packet;
using Network.Protocol;
using Timer = System.Timers.Timer;

using static Network.Protocol.P_SZ;

/*
 * Author: Red_K
 */
namespace DBGWMGR
{
    public abstract class CGDBGWNetworkBase
    {
        #region Properties

        protected readonly CGameServerClient m_cClient;
        private readonly Timer m_pHBTimer;
        private volatile int m_nHeartbeatCount;
        private object m_lockObject = new object();

        #endregion

        #region Structs

        public struct PACKET_DATA
        {
            public readonly AutoResetEvent Signal;
            private readonly List<byte> NetworkBuffer;
            public int ReceivedPacketCount;

            public PACKET_DATA(bool initialState)
            {
                Signal = new AutoResetEvent(initialState);
                NetworkBuffer = new List<byte>();
                ReceivedPacketCount = 0;
            }
            
            public void Reset()
            {
                Signal.Reset();
                NetworkBuffer.Clear();
                ReceivedPacketCount = 0;
            }
            
            public void AddToBuffer(IEnumerable<byte> buffer)
            {
                foreach (var b in buffer)
                {
                    NetworkBuffer.Add(b);
                }
            }

            public void SetLastBuffer(IEnumerable<byte> buffer)
            {
                NetworkBuffer.Clear();
                foreach (var b in buffer)
                {
                    NetworkBuffer.Add(b);
                }
            }
        
            public byte[] GetLastBuffer()
            {
                return NetworkBuffer.ToArray();
            }
        }
        
        #endregion

        public CGDBGWNetworkBase(string szAddr, ushort usPort, int iRecvTimeout)
        {
            m_pHBTimer = new Timer {Interval = 10000};
            m_pHBTimer.Elapsed += T_HEARTBEAT;
            
            m_cClient = new CGameServerClient(szAddr, usPort, iRecvTimeout);
            m_cClient.ConnectionBrokenEvent += CloseEvent;
            m_cClient.ReceiveClientDataEvent += OnReceiveData;
        }

        public void StopHeartbeat()
        {
            m_pHBTimer.Stop();
        }
        
        private void OnReceiveData(byte[] buff, double recv_time)
        {
            //PublicLogger.GetLogger().info("__InternalProcessPacket");
           
            var nFirst = buff[3];
            var nSecond = buff[4];
            var nThird = buff[5];
            //var nFourth = buff[3];
            //var nFifth = buff[4];
            
            //CPublicLogger.GetLogger().info(nFirst + " " + nSecond + " " + nThird);

            // Copy data parts to a new buffer array
            //var bufData = new byte[buff.Length - 5];
            //Array.Copy(buff, 5, bufData, 0, buff.Length - 5);
            var bufData = new byte[buff.Length - PROTOCOL_NON_USER_AREA_SIZE];
            Array.Copy(buff, PROTOCOL_NON_USER_AREA_SIZE - 1,
                bufData, 0, buff.Length - PROTOCOL_NON_USER_AREA_SIZE);

            lock (m_lockObject)
            {
                InternalProcessPacket(nFirst, nSecond, nThird, bufData);
            }
        }

        private void CloseEvent()
        {
            OnClose();
            GDBGWUtils.PrintManagerInfo(
                "close event from server. server IP:{0}",
                m_cClient.GetAddress());
            
            CPublicLogger.GetLogger().info("[CGDBGWNetworkBase::CloseEvent()] GDBGW disconnected!!!");
        }

        #region Shared

        protected abstract void InternalProcessPacket(byte byFirst, byte bySecond, byte byThird, byte[] aBuff);

        protected abstract void OnClose();
        protected abstract bool Authenticate();

        private void AddHeartbeatCount()
        {
            Interlocked.Increment(ref m_nHeartbeatCount);
        }

        protected void ResetHeartbeatCount()
        {
            Interlocked.Exchange(ref m_nHeartbeatCount, 0);
        }

        protected void StartHeartbeat()
        {
            try
            {
                m_pHBTimer.Start();
            }
            catch
            {
                CPublicLogger.GetLogger().warn("CGDBGWNetworkBase - StartHeartbeat() unable to start HB thread");
            }
        }

        #endregion

        #region Heartbeat Thread (Keep Alive)
        
        private void T_HEARTBEAT(object source, ElapsedEventArgs e)
        {
            if (m_cClient.IsRunning())
            {
                if (m_nHeartbeatCount != 0)
                {
                    CPublicLogger.GetLogger().warn(
                        "[{0}] GDBGW Skipped HeartBeat #{1}",
                        "CGDBGWNetworkBase::T_HEARTBEAT",
                        m_nHeartbeatCount);
                }
                
                var cPacket = new CPacket();
                cPacket.SetFirstClass(GDBGW_PK.PROTOCOL_GDBGW);
                cPacket.SetSecondClass(GDBGW_PK.PROTOCOL_HEARTBEAT);
                cPacket.CopyToUserDataArea(null);
                if (m_cClient.SendPacket(cPacket))
                {
                    AddHeartbeatCount();
                }

                if (m_nHeartbeatCount >= 6)
                {
                    m_cClient.Disconnect();
                    CPublicLogger.GetLogger().warn(
                        "[{0}] m_cClient::Disconnect() (reason: {1})",
                        "CGDBGWNetworkBase::T_HEARTBEAT",
                        "Heartbeat Stopped");
                }
            }
            else
            {
                if (m_cClient.Connect() && Authenticate())
                {
                    GDBGWUtils.PrintManagerInfo(
                        "Reconnect Success. server IP:{0}",
                        m_cClient.GetAddress());
                    CPublicLogger.GetLogger().info("GDBGW reconnect success");
                }
                else
                {
                    GDBGWUtils.PrintManagerInfo(
                        "FD_CONNECT_BIT[FALSE] from:{0}::{1}",
                        m_cClient.GetAddress(),
                        m_cClient.GetPort());
                    CPublicLogger.GetLogger().warn("failed connecting to GDBGW");
                }
            }
        }
        
        #endregion
    }
}