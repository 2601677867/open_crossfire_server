using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Achievement
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct ST_SUB_OBJECT
    {
        public int m_nIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
        public int[] m_values;
        
        public E_ACHIEVE_STATUS m_status;
    }
}