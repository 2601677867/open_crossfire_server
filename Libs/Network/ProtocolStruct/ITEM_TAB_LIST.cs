using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ITEM_TAB_LIST
    {
        public ushort nTabNo;
        public ushort nItemIndex;
    }
}