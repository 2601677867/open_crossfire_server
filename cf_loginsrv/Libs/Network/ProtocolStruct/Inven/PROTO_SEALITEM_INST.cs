using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Inven
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct PROTO_SEALITEM_INST
    {
        public fixed int aItemIndex[MAX_SEAL_ITEM_COUNT];
    }
}