using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_GAME_USER_ITEM_INFO
    {
        public int nCount;
        public int nItemIndex;
        public long lInvenSRL;
    }
}