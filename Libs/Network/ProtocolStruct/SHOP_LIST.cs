using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct SHOP_LIST
    {
        public ushort nItemIndex;
        public char cItemInfo;
        public char chPadding;
        public short nItemRank;

        public short i1;
        // sale type, 1 = D-6534 sale, 2 = D-6534, !3 = sale, 4 = limited, 6 = limited + D-6534,
        // 7 = limited + D-6534, 8 = limited, !9 = limited sale
        public int i2;
        public int i3;
        public short i4; // D-XXXX Number-1
        public short i5; // bDayPart
        public int i6;
        public int i7; // 0 = Sold out
        public int i8;
        public int i9; // bDiscount
        public int i10; // sale/discount price
        
        public int i11;
        public int i12;
        public int i13;
        public int i14;
        public int i15;
        

        //[MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
        //public byte[] aDummy;
    }
}