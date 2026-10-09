using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_JOIN_HOST_RESULT
    {
        public long iUserID;
        public uint gamePort;
        public uint gameAddr;
        public JOIN_HOST_RESULT eResult;
    }
}