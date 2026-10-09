using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_MAIN_BADGE_INFO
    {
        public int nMainBadgeKind;
        public int nMainBadgeLevel;
    }
}