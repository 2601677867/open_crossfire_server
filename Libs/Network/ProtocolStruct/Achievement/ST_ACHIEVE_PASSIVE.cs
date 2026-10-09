using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Achievement
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ST_ACHIEVE_PASSIVE
    {
        public int m_nMainCode;
        public E_ACHIEVE_PASSIVE_TYPE m_eType;
        public int m_nSubType;
        public int m_nRoundType;
        public int m_nMapIndex;
        public E_ACHIEVE_PASSIVE_REWARD_TYPE m_eRewardType;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        public long[] m_value_lev;

        public E_ACHIEVE_PASSIVE_LIMIT_TYPE m_eLimitType;
        public int m_nLimitValue;
    }
}