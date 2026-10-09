using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct sMissionCard_UserInfo
    {
        private E_MATCH_TYPE m_modeType;

        private E_MISSION_TYPE m_missionType;

        private int m_nTargetValue;

        private E_REWARD_TYPE m_reward_type;

        private long m_nRewardValue;

        private bool m_bActive;
    }
}