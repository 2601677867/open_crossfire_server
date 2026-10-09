using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Gacha
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ST_DISPLAY_DATA
    {
        public int nGachaGroup;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 11)]
        public string szItemID;

        public int nRareType;
    }
}