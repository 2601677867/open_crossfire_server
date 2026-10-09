using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct CLAN_UNIT_INFO
    {
        public byte byUnitLevel01;
        public byte byUnitLevel02;
        public byte byUnitLevel03;
        public byte byUnitLevel04;
    }
}