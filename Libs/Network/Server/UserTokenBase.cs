using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Network.Server
{
    public abstract class CUserTokenBase : IUserTokenBase
    {
        protected CUserTokenBase()
        {
            Buffer = new List<byte>();
        }
        
        public IPAddress IPAddress { get; set; }
        public int RemotePort { get; set; }
        public EndPoint Remote { get; set; }
        public Socket Socket { get; set; }
        public DateTime ConnectTime { get; set; }
        public List<byte> Buffer { get; protected set; }

        public abstract IUserTokenBase CreateUserToken();
    }
}