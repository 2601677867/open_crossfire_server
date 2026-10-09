using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Login
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_CRYPTED_DATA
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_CRYPTED_DATA_SIZE)]
        public byte[] m_aCryptedData;
    }
}