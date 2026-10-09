using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct sMissionCardSrl
    {
        private int m_nMissionTarget_Srl;

        private int m_nMissionReward_Srl;
    }
}