using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Commons.Log;
using Network.Packet;

using static Network.Protocol.P_SZ;

namespace Network.Client
{
    public class CGameServerClient
    {
        #region Proeprties
        
        public delegate void OnReceiveData(byte[] buff, double recv_time);
        public delegate void OnConnectionBroken();
        public delegate void OnConnected();
        
        public event OnReceiveData ReceiveClientDataEvent;
        public event OnConnectionBroken ConnectionBrokenEvent;
        public event OnConnected ConnectedEvent;
        
        private readonly int m_iRecvTimeout;

        private Socket m_pClient;
        private IPEndPoint m_pRemoteEP;

        private readonly string m_szAddress;
        private readonly ushort m_usPort;
        
        private volatile bool m_bRunning;

        private const int BUFFER_SIZE = 1024;
        
        private readonly byte[] m_gTempBuffer = new byte[BUFFER_SIZE];
        private readonly List<byte> m_gBuffer = new List<byte>();
        
        #endregion

        #region Constructor
        
        public CGameServerClient(string szAddr, ushort usPort, int iRecvTimeout = -1)
        {
            m_szAddress = szAddr;
            m_usPort = usPort;
            m_iRecvTimeout = iRecvTimeout;
            
            m_pClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = iRecvTimeout == -1 ? -1 : iRecvTimeout * 1000
            };
        }
        
        #endregion
        
        #region Shared Methods
        
        public bool Connect(bool bDoNotListen = false)
        {
            RecreateInstance();
            RecreateEndpoint();
            try
            {
                m_pClient.Connect(m_pRemoteEP);
            }
            catch
            {
                return false;
            }

            if (!m_pClient.Connected) return false;

            if (!bDoNotListen) StartListenPacket();
            
            ConnectedEvent?.Invoke();

            return true;
        }

        public void Disconnect()
        {
            m_pClient.Shutdown(SocketShutdown.Both);
            m_bRunning = false;
            ConnectionBrokenEvent?.Invoke();
        }
        
        public void StartListenPacket()
        {
            if (m_bRunning) return;
            m_bRunning = true;
            m_pClient.BeginReceive(m_gTempBuffer, 0, BUFFER_SIZE, 0, ReceiveCallback, m_pClient);
        }

        public bool SendPacket(CPacket cPacket)
        {
            if (!m_bRunning) return false;
            if (!cPacket.CheckValidPacket(out var wSize))
            {
                CPublicLogger.GetLogger().stackTrace("[CGameServerClient::SendPacket] CheckValidPacket fail");
                return false;
            }
                
            return m_pClient.Send(cPacket.GetPacketData(), wSize + PROTOCOL_NON_USER_AREA_SIZE, SocketFlags.None) == cPacket.GetRecordedSize();
        }
        
        public int SendPacket(CPacket cPacket, out bool IsSockSent)
        {
            if (!m_bRunning)
            {
                IsSockSent = false;
                return 0;
            }
                
            if (!cPacket.CheckValidPacket(out var wSize))
            {
                CPublicLogger.GetLogger().stackTrace("[CGameServerClient::SendPacket] CheckValidPacket fail");
                IsSockSent = false;
                return 0;
            }

            var aPacketData = cPacket.GetPacketData();
            var bytesTransfered = m_pClient.Send(aPacketData, aPacketData.Length, SocketFlags.None);
                
            if (bytesTransfered != aPacketData.Length)
            {
                CPublicLogger.GetLogger().warn(
                    $"[CGameServerClient::SendPacket] bytesTransfered {bytesTransfered} but wSize {aPacketData.Length}");
            }

            IsSockSent = bytesTransfered > 0;
            return bytesTransfered;
        }

        public IntPtr GetHandle()
        {
            return m_pClient.Handle;
        }

        public Socket GetSocket()
        {
            return m_pClient;
        }
        
        public ushort GetPort()
        {
            return m_usPort;
        }
        
        public string GetAddress()
        {
            return m_szAddress;
        }

        public void StopListenPacket()
        {
            m_bRunning = false;
        }
        
        public bool IsRunning()
        {
            return m_bRunning;
        }

        private void RecreateEndpoint()
        {
            //_remoteEndPoint = new IPEndPoint(Dns.GetHostEntry(_ip).AddressList[0], _port);
            if (m_szAddress.StartsWith("[", StringComparison.Ordinal) && m_szAddress.EndsWith("]", StringComparison.Ordinal))
                m_pRemoteEP = new IPEndPoint(Dns.GetHostEntry(m_szAddress.Substring(1, m_szAddress.Length - 2)).AddressList[0],
                    m_usPort);
            else
                m_pRemoteEP = new IPEndPoint(IPAddress.Parse(m_szAddress), m_usPort);
        }

        private void RecreateInstance()
        {
            if (m_pClient.Connected) m_pClient.Disconnect(true);

            m_pClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp)
            {
                ReceiveTimeout = m_iRecvTimeout == -1 ? -1 : m_iRecvTimeout * 1000
            };
        }

        #endregion

        #region Packet Handler

        private void __NEW_HANDLE_PACKET()
        {
            //PublicLogger.GetLogger().debug("__NEW_HANDLE_PACKET");
            if (m_gBuffer == null || m_gBuffer.Count == 0)
            {
                CPublicLogger.GetLogger().trace("[{0}:{1}] 0 size buffer received in __NEW_HANDLE_PACKET()!",
                    m_szAddress, m_usPort);
                return;
            }

            if (m_gBuffer.Count < 9)
            {
                CPublicLogger.GetLogger().trace("[{0}:{1}] not enough packet size in __NEW_HANDLE_PACKET()!",
                    m_szAddress, m_usPort);
                return;
            }

            //m_gBuffer.AddRange(gResponse);

            var sw = new Stopwatch();

            do
            {
                sw.Reset();
                sw.Start();
                
                var lenBytes = m_gBuffer.GetRange(1, 2).ToArray();
                int packageLen = BitConverter.ToUInt16(lenBytes, 0);
                if (packageLen > m_gBuffer.Count - 9)
                {
                    if (m_gBuffer[0] != 0xF1)
                    {
                        CPublicLogger.GetLogger()
                            .etcinfo(
                                "[CGameServerClient] Ignored long packet because of unknown structure, rbs: {0}B, ptl: {1}B",
                                m_gBuffer.Count, packageLen);
                        m_gBuffer.Clear();
                    }

                    // not enough length, exit, wait to fill up
                    break;
                }

                //var rev = m_gBuffer.GetRange(3, packageLen + 5).ToArray();
                var rev = m_gBuffer.GetRange(0, packageLen + 9).ToArray();

                if (m_gBuffer[0] != 0xF1 || m_gBuffer[packageLen + 8] != 0xF2)
                {
                    CPublicLogger.GetLogger().etcinfo("[CGameServerClient] Unknown packet structure, rbs: {0}B, ptl: {1}B",
                        m_gBuffer.Count, packageLen);
                    m_gBuffer.Clear();
                    break;
                }

                m_gBuffer.RemoveRange(0, packageLen + 9);

                sw.Stop();

                ReceiveClientDataEvent?.Invoke(rev, sw.Elapsed.TotalSeconds);
            } while (m_gBuffer.Count > 4);

        }
        
        #endregion

        #region Async Callback
        
        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var pClient = (Socket) ar.AsyncState;
                //var client = pClient.WorkSocket;

                if (pClient == null || !pClient.Connected)
                {
                    m_bRunning = false;
                    ConnectionBrokenEvent?.Invoke();
                    return;
                }

                var BytesTransferred = pClient.EndReceive(ar);

                if (BytesTransferred > 0)
                {
                    var data = new byte[BytesTransferred];
                    Array.Copy(m_gTempBuffer, 0, data, 0, BytesTransferred);
                    lock (m_gBuffer)
                    {
                        m_gBuffer.AddRange(data);
                    }

                    //foreach (var data in dataTaken) pClient.Sb.Add(data);
                
                    //PublicLogger.GetLogger().trace("[CAsyncClient] Received buffer size: {0}B",
                    //    data.Length);

                    lock (m_gBuffer)
                    {
                        __NEW_HANDLE_PACKET();
                    }
                    
                    if (m_bRunning)
                    {
                        pClient.BeginReceive(m_gTempBuffer, 0, BUFFER_SIZE, 0, ReceiveCallback, pClient);
                    }
                }
                else
                {
                    m_bRunning = false;
                    ConnectionBrokenEvent?.Invoke();
                }

                //m_pReceiveSignal.Set();
                
                //var dataTaken = pClient.Buffer.Take(bytesRead).ToArray();

                /*if (pClient.Available > 0)
                {
                    pClient.BeginReceive(m_gBufferSection, 0, BUFFER_SIZE, 0, ReceiveCallback, pClient);
                }
                else
                {
                    //if (state.Sb.Count > 2) m_gResponse = (byte[]) state.Sb.ToArray(typeof(byte));
                    //if (m_gBuffer.Count > 0) __HANDLE_PACKET();
                    m_pReceiveSignal.Set();
                }*/
            }
            catch (Exception e)
            {
                CPublicLogger.GetLogger().error(e);
                m_bRunning = false;
                ConnectionBrokenEvent?.Invoke();
				//m_gResponse = null;
				//m_pReceiveSignal.Set();
            }
        }
        
        #endregion
    }
}