using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_USER_ACHIEVE_DATA
    {
        public byte m_cMainCode;
        public byte m_cSubType;
        public byte m_cPassiveLev;
        public short m_nValue;
        public int m_nLimitCount;
    }
}