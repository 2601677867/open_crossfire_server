using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Achievement
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ST_OBJECT
    {
        public int m_nIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public ST_SUB_OBJECT[] m_subObjects;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public int[] m_nPassiveLev;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
        public long[] m_nCurLimitValue;

        public long lDummy;
    }
}