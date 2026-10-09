using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace MC.Commons.Client
{
    public class AsyncClient
    {
        private readonly string ip = "";
        private readonly int port;
        private readonly ManualResetEvent receiveDone = new ManualResetEvent(false);
        private readonly ManualResetEvent sendDone = new ManualResetEvent(false);
        private Socket client;

        public bool clientUnlocked;

        public ManualResetEvent connectDone = new ManualResetEvent(false);

        public bool connected;

        private Thread keepAliveThread;
        private string name = "";
        private byte[] response;

        public AsyncClient(string ip, int port, string name)
        {
            this.ip = ip;
            this.port = port;
            this.name = name;
        }

        public bool IsSocketConnected(Socket s)
        {
            return !(s.Poll(1000, SelectMode.SelectRead) && s.Available == 0 || !s.Connected);
        }

        public Socket getSocket()
        {
            return client;
        }

        private void checkConnect(object remoteEP)
        {
            while (true)
            {
                if (!IsSocketConnected(client))
                {
                    //PublicLogger.getLogger().error("{0} Disconnected! Trying to reconnect...", name);
                    if (client.Connected) client.Disconnect(true);
                    connectDone.Reset();

                    client.BeginConnect((EndPoint) remoteEP, ConnectCallback, client);
                    connectDone.WaitOne();
                }

                Thread.Sleep(5000);
            }
        }

        public byte[] ReceivePacket()
        {
            lock (receiveDone)
            {
                receiveDone.Reset();
                Receive(client);
                receiveDone.WaitOne();
            }

            return response;
        }


        public void ConnectWithoutCatch(AsyncCallback _callback, int RECV_TIMEOUT = -1)
        {
            var ipAddress = IPAddress.Parse(ip);
            var remoteEP = new IPEndPoint(ipAddress, port);

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (RECV_TIMEOUT != -1) client.ReceiveTimeout = RECV_TIMEOUT;

            clientUnlocked = false;
            connectDone.Reset();

            client.BeginConnect(remoteEP, _callback, client);
            connectDone.WaitOne();

            //if (client.Connected)
            //{
            //keepAliveThread.IsBackground = true;
            //keepAliveThread.Start(remoteEP);
            //}
        }

        public void ConnectWithoutCatch(int RECV_TIMEOUT = -1)
        {
            var ipAddress = IPAddress.Parse(ip);
            var remoteEP = new IPEndPoint(ipAddress, port);

            client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            if (RECV_TIMEOUT != -1) client.ReceiveTimeout = RECV_TIMEOUT;

            clientUnlocked = false;
            connectDone.Reset();

            client.BeginConnect(remoteEP, ConnectCallback, client);
            connectDone.WaitOne();

            //if (client.Connected)
            //{
            //keepAliveThread.IsBackground = true;
            //keepAliveThread.Start(remoteEP);
            //}
        }

        public void Connect(int RECV_TIMEOUT = -1)
        {
            try
            {
                var ipAddress = IPAddress.Parse(ip);
                var remoteEP = new IPEndPoint(ipAddress, port);

                client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                if (RECV_TIMEOUT != -1) client.ReceiveTimeout = RECV_TIMEOUT;

                connectDone.Reset();
                clientUnlocked = false;

                client.BeginConnect(remoteEP, ConnectCallback, client);
                connectDone.WaitOne();

                if (client.Connected)
                {
                    keepAliveThread = new Thread(checkConnect);
                    keepAliveThread.IsBackground = true;
                    keepAliveThread.Start(remoteEP);
                }
            }
            catch (Exception e)
            {
                connected = false;
                //PublicLogger.getLogger().error(e.Message);
                //PublicLogger.getLogger().trace(e.StackTrace);
                Console.WriteLine(e.Message);
            }
        }

        public void Disconnect()
        {
            try
            {
                client.Shutdown(SocketShutdown.Both);
                client.Close();
                connected = false;
                if (keepAliveThread != null) keepAliveThread.Abort();
                //PublicLogger.getLogger().info("{0} Abort", name);
            }
            catch (Exception e)
            {
                //PublicLogger.getLogger().error(e.Message);
                //PublicLogger.getLogger().trace(e.StackTrace);
            }
        }

        public byte[] SendPacketOnly(uint head, byte[] message)
        {
            try
            {
                var buff = new byte[message.Length + 7];
                var len = BitConverter.GetBytes(message.Length);
                var header = BitConverter.GetBytes(head);

                buff[0] = 0xF1;
                Array.Copy(len, 0, buff, 1, 2);
                Array.Copy(header, 0, buff, 3, header.Length);
                Array.Copy(message, 0, buff, 6, message.Length);
                buff[message.Length + 6] = 0xF2;

                lock (sendDone)
                {
                    clientUnlocked = true;

                    sendDone.Reset();

                    Send(client, buff);
                    sendDone.WaitOne();
                }

                return response;
            }
            catch (Exception e)
            {
                //PublicLogger.getLogger().error(e.Message);
                //PublicLogger.getLogger().trace(e.StackTrace);
                return new byte[] {0xFF};
            }
        }

        public byte[] SendPacket(uint head, byte[] message)
        {
            try
            {
                var buff = new byte[message.Length + 7];
                var len = BitConverter.GetBytes(message.Length);
                var header = BitConverter.GetBytes(head);

                buff[0] = 0xF1;
                Array.Copy(len, 0, buff, 1, 2);
                Array.Copy(header, 0, buff, 3, header.Length);
                Array.Copy(message, 0, buff, 6, message.Length);
                buff[message.Length + 6] = 0xF2;

                lock (sendDone)
                lock (receiveDone)
                {
                    sendDone.Reset();
                    receiveDone.Reset();

                    Send(client, buff);
                    sendDone.WaitOne();

                    Receive(client);
                    receiveDone.WaitOne();
                }

                return response;
            }
            catch (Exception e)
            {
                //PublicLogger.getLogger().error(e.Message);
                //PublicLogger.getLogger().trace(e.StackTrace);
                return new byte[] {0xFF};
            }
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                var client = (Socket) ar.AsyncState;
                client.EndConnect(ar);
                connected = true;
                connectDone.Set();

                //PublicLogger.getLogger().info("{0} Connected", name);
            }
            catch (Exception e)
            {
                //PublicLogger.getLogger().error(e.Message);
                //PublicLogger.getLogger().trace(e.StackTrace);
                connected = false;
                connectDone.Set();
            }
        }

        private void Receive(Socket client)
        {
            try
            {
                var state = new StateObject();
                state.workSocket = client;
                client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, ReceiveCallback, state);
            }
            catch (Exception e)
            {
                //PublicLogger.getLogger().error(e.Message);
                //PublicLogger.getLogger().trace(e.StackTrace);
            }
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            try
            {
                var state = (StateObject) ar.AsyncState;
                var client = state.workSocket;

                foreach (var b in state.buffer) state.sb.Add(b);
                if (client == null || !client.Connected) return;
                if (client.Available > 0)
                {
                    client.BeginReceive(state.buffer, 0,
                        client.Available < StateObject.BufferSize ? client.Available : StateObject.BufferSize, 0,
                        ReceiveCallback, state);
                }
                else
                {
                    if (state.sb.Count > 1) response = (byte[]) state.sb.ToArray(typeof(byte));
                    receiveDone.Set();
                }
            }
            catch (Exception e)
            {
                //PublicLogger.getLogger().error(e.Message);
                //PublicLogger.getLogger().trace(e.StackTrace);
            }
        }

        private void Send(Socket client, byte[] byteData)
        {
            client.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, client);
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                var client = (Socket) ar.AsyncState;

                var bytesSent = client.EndSend(ar);

                sendDone.Set();
            }
            catch (Exception e)
            {
                //PublicLogger.getLogger().error(e.Message);
                //PublicLogger.getLogger().trace(e.StackTrace);
            }
        }
    }
}