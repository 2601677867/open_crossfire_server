using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Threading;
using Commons.Log;
using Network.Packet;
using static Network.Protocol.P_SZ;

namespace Network.Server
{
    public sealed class CServer
    {
        private const int opsToAlloc = 2;
        private readonly CBufferManager m_bufferManager;

        private readonly int m_maxConnectNum;
        private readonly Semaphore m_maxNumberAcceptedClients;

        private readonly CSocketEventPool m_pool;
        private readonly int m_recvBufferSize;
         
        private Socket listenSocket;
        private int m_clientCount;
        
        private readonly IUserTokenBase m_tokenBase;

        public volatile int m_nReceivedPacketCount;
        public volatile int m_nReceivedTotalBuffer;
        public volatile int m_nAbandonedPacketCount;

        public (int, int, int, int, int, int, int) GetDebugInfo()
        {
            return (m_clientCount, m_maxConnectNum, m_pool.Count, m_bufferManager.GetFreeIndexPoolCount(), m_bufferManager.GetBufferCount(), m_recvBufferSize, m_bufferManager.GetCurrentIndex());
        }

        public void ResetDebugInfo()
        {
            m_nReceivedPacketCount = 0;
            m_nReceivedTotalBuffer = 0;
            m_nAbandonedPacketCount = 0;
        }

        public int GetFreePoolCount()
        {
            return m_bufferManager.GetFreeIndexPoolCount();
        }

        /// <summary>
        ///     构造函数
        /// </summary>
        /// <param name="tokenBase">UserToken (which inherits from IUserTokenBase)</param>
        /// <param name="numConnections">最大连接数</param>
        /// <param name="receiveBufferSize">缓存区大小</param>
        /// <param name="serverShortName">服务端名称缩写 (日志用)</param>
        public CServer(IUserTokenBase tokenBase, int numConnections, int receiveBufferSize, string serverShortName)
        {
            m_tokenBase = tokenBase;
            m_nReceivedTotalBuffer = 0;
            m_clientCount = 0;
            m_maxConnectNum = numConnections;
            m_recvBufferSize = receiveBufferSize;
            // allocate buffers such that the maximum number of sockets can have one outstanding read and   
            //write posted to the socket simultaneously    
            m_bufferManager = new CBufferManager(receiveBufferSize * numConnections * opsToAlloc, receiveBufferSize);

            m_pool = new CSocketEventPool(numConnections);
            m_maxNumberAcceptedClients = new Semaphore(numConnections, numConnections);

            m_sServerShortName = serverShortName;
        }

        #region 定义属性

        /// <summary>
        ///     获取客户端列表
        /// </summary>
        private List<IUserTokenBase> ClientList;

        #endregion

        public List<IUserTokenBase> GetClients()
        {
            //return new List<IUserTokenBase>(ClientList); // clone
            lock (ClientList)
            {
                return ClientList;
            }
        }

        public int GetClientCount()
        {
            lock (ClientList)
            {
                return ClientList.Count;
            }
        }

        public int GetBufferSize()
        {
            return m_recvBufferSize;
        }

        /// <summary>
        ///     初始化
        /// </summary>
        public void Init()
        {
            IOCPInit();
            InitBufferPool();
        }
        
        /// <summary>
        ///     初始化 - 需要手动调用 InitBufferPool()
        /// </summary>
        public void IOCPInit()
        {
            // Allocates one large byte buffer which all I/O operations use a piece of.  This gaurds   
            // against memory fragmentation  
            m_bufferManager.InitBuffer();
            ClientList = new List<IUserTokenBase>();
        }

        public void InitBufferPool()
        {
            // preallocate pool of SocketAsyncEventArgs objects 
            for (var i = 0; i < m_maxConnectNum; i++)
            {
                var readWriteEventArg = new SocketAsyncEventArgs();
                readWriteEventArg.Completed += IO_Completed;
                readWriteEventArg.UserToken = m_tokenBase.CreateUserToken();

                // assign a byte buffer from the buffer pool to the SocketAsyncEventArg object  
                m_bufferManager.SetBuffer(readWriteEventArg);
                // add SocketAsyncEventArg to the pool  
                m_pool.Push(readWriteEventArg);
            }
        }

        private string m_sRemoteIP;
        private string m_sServerShortName;

        /// <summary>
        ///     启动服务
        /// </summary>
        /// <param name="localEndPoint"></param>
        public bool Start(IPEndPoint localEndPoint)
        {
            try
            {
                m_sRemoteIP = localEndPoint.Address.ToString();
                
                ClientList.Clear();
                listenSocket = new Socket(localEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                listenSocket.Bind(localEndPoint);
                // start the server with a listen backlog of 100 connections  
                listenSocket.Listen(m_maxConnectNum);
                // post accepts on the listening socket  
                StartAccept(null);
                return true;
            }
            catch (Exception e)
            {
                CPublicLogger.GetLogger().error(e);
                //PublicLogger.getLogger().trace(e);
                return false;
            }
        }

        /// <summary>
        ///     停止服务
        /// </summary>
        public void Stop()
        {
            foreach (var token in ClientList)
                try
                {
                    token.Socket.Shutdown(SocketShutdown.Both);
                }
                catch (Exception e)
                {
                    CPublicLogger.GetLogger().error(e);
                }

            try
            {
                listenSocket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e)
            {
                CPublicLogger.GetLogger().error(e);
            }

            listenSocket.Close();
            var c_count = ClientList.Count;
            lock (ClientList)
            {
                ClientList.Clear();
            }
        }


        public void CloseClient(IUserTokenBase token)
        {
            try
            {
                token.Socket.Shutdown(SocketShutdown.Both);
            }
            catch (Exception e)
            {
                CPublicLogger.GetLogger().error(e);
            }
        }


        // Begins an operation to accept a connection request from the client   
        //  
        // <param name="acceptEventArg">The context object to use when issuing   
        // the accept operation on the server's listening socket</param>  
        private void StartAccept(SocketAsyncEventArgs acceptEventArg)
        {
            if (acceptEventArg == null)
            {
                acceptEventArg = new SocketAsyncEventArgs();
                acceptEventArg.Completed += AcceptEventArg_Completed;
            }
            else
            {
                // socket must be cleared since the context object is being reused  
                acceptEventArg.AcceptSocket = null;
            }

            m_maxNumberAcceptedClients.WaitOne();
            if (!listenSocket.AcceptAsync(acceptEventArg)) ProcessAccept(acceptEventArg);
        }

        // This method is the callback method associated with Socket.AcceptAsync   
        // operations and is invoked when an accept operation is complete  
        //  
        private void AcceptEventArg_Completed(object sender, SocketAsyncEventArgs e)
        {
            ProcessAccept(e);
        }

        private void ProcessAccept(SocketAsyncEventArgs e)
        {
            try
            {
                Interlocked.Increment(ref m_clientCount);
                // Get the socket for the accepted client connection and put it into the   
                //ReadEventArg object user token  
                var readEventArgs = m_pool.Pop();
                var userToken = (IUserTokenBase) readEventArgs.UserToken;

                userToken.Socket = e.AcceptSocket;
                userToken.ConnectTime = DateTime.Now;
                userToken.Remote = e.AcceptSocket.RemoteEndPoint;
                userToken.IPAddress = ((IPEndPoint) e.AcceptSocket.RemoteEndPoint).Address;
                userToken.RemotePort = ((IPEndPoint) e.AcceptSocket.RemoteEndPoint).Port;

                lock (ClientList)
                {
                    ClientList.Add(userToken);
                }

                var nLocalPort = ((IPEndPoint) e.AcceptSocket.LocalEndPoint).Port;
                if (AcceptEvent != null)
                {
                    AcceptEvent(userToken);
                }
                else
                {
                    CPublicLogger.GetLogger().info(
                        $"Accepted ({userToken.IPAddress}, {userToken.RemotePort}) -> local port {nLocalPort}");
                }

                CPublicLogger.GetLogger().debug(
                    "Posting initial receive for client ({0}, {1}) on local port {2}",
                    userToken.IPAddress, userToken.RemotePort, nLocalPort);
                if (!e.AcceptSocket.ReceiveAsync(readEventArgs))
                {
                    CPublicLogger.GetLogger().debug("ReceiveAsync completed synchronously");
                    ProcessReceive(readEventArgs);
                }
            }
            catch (Exception me)
            {
                CPublicLogger.GetLogger().error(me);
                //PublicLogger.getLogger().trace(me);
            }

            // Accept the next connection request  
            if (e.SocketError == SocketError.OperationAborted) return;
            StartAccept(e);
        }


        private void IO_Completed(object sender, SocketAsyncEventArgs e)
        {
            // determine which type of operation just completed and call the associated handler  
            switch (e.LastOperation)
            {
                case SocketAsyncOperation.Receive:
                    ProcessReceive(e);
                    break;
                case SocketAsyncOperation.Send:
                    ProcessSend(e);
                    break;
                default:
                    throw new ArgumentException("The last operation completed on the socket was not a receive or send");
            }
        }


        // This method is invoked when an asynchronous receive operation completes.   
        // If the remote host closed the connection, then the socket is closed.    
        // If data was received then the data is echoed back to the client.  
        //  
        private void ProcessReceive(SocketAsyncEventArgs e)
        {
            try
            {
                // check if the remote host closed the connection  
                //Console.WriteLine("Connected");

                var token = (IUserTokenBase) e.UserToken;
                CPublicLogger.GetLogger().trace(
                    "ProcessReceive {0} bytes, SocketError {1}, client {2}:{3}",
                    e.BytesTransferred, e.SocketError, token.IPAddress, token.RemotePort);
                
                if (e.BytesTransferred > 0 && e.SocketError == SocketError.Success)
                {
                    var sw = new Stopwatch();
                    sw.Reset();
                    sw.Start();

                    var data = new byte[e.BytesTransferred];
                    Array.Copy(e.Buffer, e.Offset, data, 0, e.BytesTransferred);
                    CPublicLogger.GetLogger().trace("[{2}:{3}] recv {0}B: {1}",
                        data.Length, BitConverter.ToString(data, 0, Math.Min(data.Length, 32)),
                        token.IPAddress, token.RemotePort);
                    
                    lock (token.Buffer)
                    {
                        token.Buffer.AddRange(data);
                    }

                    sw.Stop();

                    var BaseTime = sw.Elapsed.TotalSeconds;

                    Interlocked.Exchange(ref m_nReceivedTotalBuffer, m_nReceivedTotalBuffer + data.Length);
                    
                    //CPublicLogger.GetLogger().trace("[CIOCPServer] ({2}) (HANDLE {3}) Received buffer size: {0}B, ElapsedTime: {1}s",
                    //    data.Length, BaseTime.ToString("F6"), ((IPEndPoint) listenSocket.LocalEndPoint).Port, listenSocket.Handle);

                    do
                    {
                        sw.Reset();
                        sw.Start();
                        // 00    01  02     03  04  05  06  07    08 09 0A 0B     0C
                        // F1    04  00     00  00  00  00  00    01 00 00 00     F2
                        //        Size        Header         Data
                        var lenBytes = token.Buffer.GetRange(1, 2).ToArray();
                        int packageLen = BitConverter.ToUInt16(lenBytes, 0);
                        CPublicLogger.GetLogger().trace(
                            "frame head 0x{0:X2} len={1}B tail 0x{2:X2} 0x{3:X2} buffered={4}B",
                            token.Buffer[0], packageLen,
                            token.Buffer.Count >= 2 ? token.Buffer[token.Buffer.Count - 2] : (byte)0,
                            token.Buffer.Count >= 1 ? token.Buffer[token.Buffer.Count - 1] : (byte)0,
                            token.Buffer.Count);
                        
                        // Wire framing (client protocol): F1 + Size(2) + C1 C2 C3 + Data + F2 = Size + 7.
                        // Note: the encrypted login packet has no trailing 0xF2, so the end byte
                        // must not be used to decide the frame size.
                        const int nOverhead = 7;

                        if (packageLen > token.Buffer.Count - nOverhead)
                        {
                            if (token.Buffer[0] != PROTOCOL_START_BYTE)
                            {
                                CPublicLogger.GetLogger()
                                    .etcinfo(
                                        "[CIOCPServer] ({2}) (HANDLE {3}) Ignored long packet because of unknown structure, rbs: {0}B, ptl: {1}B",
                                        data.Length, packageLen, ((IPEndPoint) listenSocket.LocalEndPoint).Port, listenSocket.Handle);
                                lock (token.Buffer)
                                {
                                    token.Buffer.Clear();
                                }
                                Interlocked.Increment(ref m_nAbandonedPacketCount);
                            }

                            break;
                        }

                        var rev = token.Buffer.GetRange(0, packageLen + nOverhead).ToArray();

                        lock (token.Buffer)
                        {
                            token.Buffer.RemoveRange(0, packageLen + nOverhead);
                        }

                        sw.Stop();
                        
                        Interlocked.Increment(ref m_nReceivedPacketCount);
                        ReceiveEvent?.Invoke(token, rev, BaseTime + sw.Elapsed.TotalSeconds);
                    } while (token.Buffer.Count > 4);

                    sw.Stop();
                    if (!token.Socket.ReceiveAsync(e))
                        ProcessReceive(e);
                    
                    //GC.Collect();
                }
                else
                {
                    CPublicLogger.GetLogger().debug(
                        "Client disconnected - transferred {0}, SocketError {1}, client {2}:{3}",
                        e.BytesTransferred, e.SocketError, token.IPAddress, token.RemotePort);
                    CloseClientSocket(e);
                }
            }
            catch (Exception xe)
            {
                CPublicLogger.GetLogger().error(xe);
                //PublicLogger.getLogger().trace(xe);
            }
        }

        // This method is invoked when an asynchronous send operation completes.    
        // The method issues another receive on the socket to read any additional   
        // data sent from the client  
        //  
        // <param name="e"></param>  
        private void ProcessSend(SocketAsyncEventArgs e)
        {
            if (e.SocketError == SocketError.Success)
            {
                // done echoing data back to the client  
                var token = (IUserTokenBase) e.UserToken;
                // read the next block of data send from the client  
                var willRaiseEvent = token.Socket.ReceiveAsync(e);
                if (!willRaiseEvent) ProcessReceive(e);
            }
            else
            {
                CloseClientSocket(e);
            }
        }

        //关闭客户端  
        private void CloseClientSocket(SocketAsyncEventArgs e)
        {
            var token = e.UserToken as IUserTokenBase;

            if (token != null)
            {
                ProcessCloseEvent?.Invoke(token);
                
                if (DisconnectEvent != null)
                {
                    DisconnectEvent(token);
                }
                else
                {
                    CPublicLogger.GetLogger().info($"Closed ({token.IPAddress}, {token.RemotePort})");
                }
            }

            lock (ClientList)
            {
                ClientList.Remove(token);
            }
            
            // close the socket associated with the client  
            try
            {
                if (token != null && token.Socket.Connected)
                    token.Socket.Shutdown(SocketShutdown.Send);
            }
            catch (Exception ex)
            {
                CPublicLogger.GetLogger().error(ex);
            }

            if (token != null)
            {
                token.Socket.Close();
                // decrement the counter keeping track of the total number of clients connected to the server  
                Interlocked.Decrement(ref m_clientCount);
                m_maxNumberAcceptedClients.Release();
                // Free the SocketAsyncEventArg so they can be reused by another client  
                e.UserToken = m_tokenBase.CreateUserToken();

                m_pool.Push(e);
            }
        }

        public static string ByteToHexStr(byte[] bytes)
        {
            var returnStr = "";
            if (bytes != null)
            {
                var length = bytes.Length;
                if (length > 10000) length = 10000;
                for (var i = 0; i < length; i++) returnStr += bytes[i].ToString("x2");
            }

            return returnStr;
        }

        private void PrintDelayPacketLog(string context, params object[] args)
        {
            CPublicLogger.GetLogger().print("GMGMT_DESTROY", context, args, m_sRemoteIP, m_sServerShortName,
                DateTime.Now.ToString("HH:mm:ss.fff"));
        }
        
        public static string ByteArr2Hex(byte[] aBytes, int iStartIndex = 0)
        {
            if (aBytes.Length == 0) return "";
            var szHexView = "\r\n";
            szHexView += "         +-------------------------------------------------+                 \r\n";
            szHexView += "         |  0  1  2  3  4  5  6  7  8  9  a  b  c  d  e  f |                 \r\n";
            szHexView += "+--------+-------------------------------------------------+----------------+\r\n";
            
            for (var i = 0; i < aBytes.Length - iStartIndex; i++)
            {
                var row = i / 16;
                var col = i % 16;
                
                if (col == 0) 
                    szHexView += "|" + row.ToString("x7") + "0" + "| ";
                
                szHexView += aBytes[i + iStartIndex].ToString("x2") + " ";

                if (col == 15 || i == aBytes.Length - iStartIndex - 1)
                {
                    int off = row * 16;
                    int len = i % 16 + 1;
                    
                    for (var j = 0; j < 16 - len; j++) szHexView += "   ";
                    szHexView += "|";
                    
                    for (var j = off; j < off + len; j++)
                        if (aBytes[j + iStartIndex] >= 32 && aBytes[j + iStartIndex] <= 126)
                        {
                            szHexView += (char) aBytes[j + iStartIndex];
                        }
                        else
                        {
                            szHexView += '.';
                        }
                    
                    for (var j = 0; j < 16 - len; j++) szHexView += " ";
                    szHexView += "|\r\n";
                }
                //szHexView += getLineTail(aBytes, row * 16, i % 16 + 1) + "\r\n";
            }

            szHexView += "+--------+-------------------------------------------------+----------------+";
            return szHexView;
        }

        public bool PushServerMessage(CPacket cPacket)
        {
            if (!cPacket.CheckValidPacket())
            {
                CPublicLogger.GetLogger().stackTrace("CServer::PushServerMessage CheckValidPacket error!!!");
                return false;
            }

            var aPacketData = cPacket.GetPacketData();

            ReceiveEvent?.Invoke(null, aPacketData, 0);
            return true;
        }

        /// <summary>
        ///     对数据进行打包,然后再发送
        /// </summary>
        /// <param name="token">IUserTokenBase</param>
        /// <param name="cPacket">Packet to be sent</param>
        /// <param name="bSyncSend">Use synchronized send method</param>
        /// <returns></returns>
        public static bool SendMessage(IUserTokenBase token, CPacket cPacket, bool bSyncSend = false)
        {
            if (token?.Socket == null || !token.Socket.Connected)
                return false;
            
            bool bResult;
            
            if (!cPacket.CheckValidPacket(out var wSize))
            {
                CPublicLogger.GetLogger().stackTrace("CServer::SendMessage CheckValidPacket error!!!");
                return false;
            }

            var aPacketData = cPacket.GetPacketData();

            if (!bSyncSend)
            {
                var sendArg = new SocketAsyncEventArgs {UserToken = token};
                sendArg.SetBuffer(aPacketData, 0, aPacketData.Length);
                bResult = token.Socket.SendAsync(sendArg);
                sendArg.Dispose();
            }
            else
            {
                bResult = token.Socket.Send(aPacketData, aPacketData.Length, SocketFlags.None) != 0;
            }

            return bResult;
        }
        
        private static byte[] HexStr2Bytes(string src)
        {
            var l = src.Length / 2;
            var ret = new byte[l];
            for (var i = 0; i < l; i++) ret[i] = Convert.ToByte(src.Substring(i * 2, 2), 16);
            return ret;
        }

        #region 定义委托  

        /// <summary>
        ///     客户端连接数量变化时触发
        /// </summary>
        /// <param name="num">当前增加客户的个数(用户退出时为负数,增加时为正数,一般为1)</param>
        /// <param name="token">增加用户的信息</param>
        public delegate void OnClientNumberChange(int num, IUserTokenBase token);

        /// <summary>
        ///     接收到客户端的数据
        /// </summary>
        /// <param name="token">客户端</param>
        /// <param name="buff">客户端数据</param>
        public delegate void OnReceiveData(IUserTokenBase tokenBase, byte[] buff, double rcv_time);

        /// <summary>
        ///     关闭客户端时触发
        /// </summary>
        /// <param name="token">客户端</param>
        public delegate void OnCloseSocket(IUserTokenBase tokenBase);
        
        public delegate void OnAccepted(IUserTokenBase tokenBase);
        
        public delegate void OnDisconnected(IUserTokenBase tokenBase);

        #endregion

        #region 定义事件

        /// <summary>
        ///     接收到客户端的数据事件
        /// </summary>
        public event OnReceiveData ReceiveEvent;


        /// <summary>
        ///     关闭客户端触发事件
        /// </summary>
        public event OnCloseSocket ProcessCloseEvent;

        public event OnAccepted AcceptEvent;
        
        public event OnDisconnected DisconnectEvent;

        #endregion
    }
}