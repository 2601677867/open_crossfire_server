using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Inven
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct PROTO_CHARITEM_INST
    {
        public PROTO_ITEM_INST tItemBase;
        public fixed long nDefaultDressInvenSrl[MAX_DRESS_PART * MAX_DRESS_LAYER];
        public fixed long nDefaultFuncInvenSrl[MAX_PERIOD_CHAR_FUNC];
        public PROTO_CHAROPTION tCharOption;
    }
}