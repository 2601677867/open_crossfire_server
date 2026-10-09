using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct HOST_CASHITEM_DATA
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 16)]
        private string funcItemCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 480)]
        private int[] funcItemIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 480)]
        private ushort[] propertyVal;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 480)]
        private uint[] extraVal;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 480)]
        private ushort[] useCount;
    }
}