using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_START_HOST_RESULT
    {
        public uint gamePort;
        public uint gameAddr;
        public START_HOST_RESULT eResult;
    }
}