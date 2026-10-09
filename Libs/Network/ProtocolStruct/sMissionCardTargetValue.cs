using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct sMissionCardTargetValue
    {
        private int m_nMissionTarget_Value;

        private long m_nMissionReward_Value;
    }
}