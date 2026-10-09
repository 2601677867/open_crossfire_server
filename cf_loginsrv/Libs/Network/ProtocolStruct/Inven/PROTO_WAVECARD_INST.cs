using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Inven
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct PROTO_WAVECARD_INST
    {
        public byte bFavorite;
        public byte nWaveCardEnchant;
        public fixed short aSlots[MAX_WAVE_SLOT_COUNT];
        public byte tWaveCardType;
    }
}