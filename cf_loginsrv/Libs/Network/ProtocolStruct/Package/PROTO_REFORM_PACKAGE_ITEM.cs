using System.Runtime.InteropServices;
using Network.Protocol;

namespace Network.ProtocolStruct.Package
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_REFORM_PACKAGE_ITEM
    {
        public int nItemIndex;
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = P_SZ.MAX_ITEM_ID)]
        public string szItemId;
        
        public long lInvenSrl;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_DRESS_PART)]
        public long[] nDefaultDressInvenSrl;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_PERIOD_CHAR_FUNC)]
        public long[] nDefaultFuncInvenSrl;
    }
}