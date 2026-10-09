using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Inven
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct PROTO_FUNCITEM_INST
    {
        public PROTO_ITEM_INST tItemBase;
        [MarshalAs(UnmanagedType.I1)]
        public bool bFuncUse;
        public fixed byte szUCCSprayText1[MAX_UCC_SPRAY_TEXT];
        public fixed byte szUCCSprayText2[MAX_UCC_SPRAY_TEXT];
        public byte nUCCTextColorIndex;
        public byte nUCCBackGroudIndex;
        public byte nColorChattingColor;
        public byte nColorNameColor;
        public PROTO_WAVECARD_INST tWaveCardData;
    }
}