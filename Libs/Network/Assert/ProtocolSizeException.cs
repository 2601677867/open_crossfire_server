using System;

namespace Network.Assert
{
    public sealed class ProtocolSizeException : Exception
    {
        public ProtocolSizeException() { }

        public ProtocolSizeException(string message)
            : base(message) { }

        public ProtocolSizeException(string message, Exception inner)
            : base(message, inner) { }
    }
}