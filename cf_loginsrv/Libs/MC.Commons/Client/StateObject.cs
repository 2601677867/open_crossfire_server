using System.Collections;
using System.Net.Sockets;

namespace MC.Commons.Client
{
    public class StateObject
    {
        // Size of receive buffer.     
        public const int BufferSize = 1;

        // Receive buffer.     
        public byte[] buffer = new byte[BufferSize];

        // Received data string.     
        public ArrayList sb = new ArrayList();

        // Client socket.     
        public Socket workSocket = null;
    }
}