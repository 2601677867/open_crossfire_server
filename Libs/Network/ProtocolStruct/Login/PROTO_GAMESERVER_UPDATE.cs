using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Login
{
    // 10-byte wire entry (Pack=1): short id @0, int count @2, int event @6.
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_GAMESERVER_UPDATE
    {
        public short m_nServerID;
        public int m_nServerConnectCount;
        public int m_nEvent;
    }
}
