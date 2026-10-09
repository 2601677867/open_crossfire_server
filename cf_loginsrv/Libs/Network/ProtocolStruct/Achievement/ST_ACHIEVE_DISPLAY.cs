using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Achievement
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ST_ACHIEVE_DISPLAY
    {
        public E_ACHIEVE_CATEGORY m_category;
        public int m_nIndex;
        public int m_nSubIndex;
    }
}