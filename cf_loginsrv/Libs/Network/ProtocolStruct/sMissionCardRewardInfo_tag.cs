using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct sMissionCardRewardInfo_tag
    {
        private int m_nSrl;

        private E_REWARD_TYPE m_nRewardType;

        private long m_nRewardValue;
    }
}