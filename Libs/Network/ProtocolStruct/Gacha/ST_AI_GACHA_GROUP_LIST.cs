using System.Runtime.InteropServices;
using Network.Protocol;

namespace Network.ProtocolStruct.Gacha
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ST_AI_GACHA_GROUP_LIST
    {
        public uint m_dwCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_AI_GACHA_LIST_NUM)]
        public ST_AI_GACHA_GROUP[] m_stAIGachaRate;
    }
}