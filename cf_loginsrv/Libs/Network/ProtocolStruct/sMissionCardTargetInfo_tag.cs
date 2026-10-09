using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct sMissionCardTargetInfo_tag
    {
        private int m_nSrl;

        private E_MISSION_TYPE m_nMissionType;

        private E_MATCH_TYPE m_nModeType;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
        private int[] m_nLevValue;

        private enum eValue
        {
            MAX_LEV = 4
        }
    }
}