using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Login
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class CONNECT_HASH_DATA
    {
        public int m_iKey;
        public int m_nResult;
        public long m_lUSN;
        public long m_lClock;
        public long m_lHash;
    }
}