using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct VVIP_ITEM
    {
        public int m_nItemIndex;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VVIP_ITEM_SPECIAL_BUFFS)]
        public byte[] m_aBuffs;
        
        public short m_nSpecialBuff;
    }
}