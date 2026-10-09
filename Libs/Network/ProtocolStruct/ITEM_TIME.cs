using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct ITEM_TIME
    {
        public ushort wYear;
        public ushort wMonth;
        public int wDay;
        public ushort wHour;
        public ushort wMinute;
    }
}