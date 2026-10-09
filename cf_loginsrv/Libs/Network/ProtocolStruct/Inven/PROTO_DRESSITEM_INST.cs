using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Inven
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct PROTO_DRESSITEM_INST
    {
        public PROTO_ITEM_INST tItemBase;
    }
}