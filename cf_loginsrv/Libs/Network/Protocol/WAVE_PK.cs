using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.Protocol
{
    public class WAVE_PK
    {
        public const int PROTOCOL_WAVE_FIRST = 14;

        public const int PROTOCOL_MAIN_WAVE_CARD = 3;
        public const int PROTOCOL_SET_MAIN_WAVE_CARD_RET = 1;
        public const int PROTOCOL_ROOM_CHANGE_MAIN_WAVE_CARD = 2;
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_SETMAINWAVECARD_RET
        {
            public byte byResult;
            public int nFuncItemIndex;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_CHANGE_WAVE_CARD_INFO
        {
            public short sWaveCardType;
            public byte byEnchant;
            
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_WAVE_SLOT_COUNT)]
            public short[] aSlotTypes;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_ROOM_CHANGE_MAIN_WAVE_CARD
        {
            public long lUSN;
            public PROTO_CHANGE_WAVE_CARD_INFO tChangeWaveCardInfo;
        }
    }
}