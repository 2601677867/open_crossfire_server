using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;

namespace Network.Server
{
    public interface IUserTokenBase
    {
        IPAddress IPAddress { get; set; }
        int RemotePort { get; set; }
        EndPoint Remote { get; set; }
        Socket Socket { get; set; }
        DateTime ConnectTime { get; set; }
        List<byte> Buffer { get; }
        
        IUserTokenBase CreateUserToken();
    }
}