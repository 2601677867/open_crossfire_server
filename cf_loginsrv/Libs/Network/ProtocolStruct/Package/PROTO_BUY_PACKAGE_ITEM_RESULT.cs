using System.Runtime.InteropServices;
using Network.Protocol;

namespace Network.ProtocolStruct.Package
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_BUY_PACKAGE_ITEM_RESULT
    {
        public BUYITEM eResult;
        [MarshalAs(UnmanagedType.I1)]
        public bool bFPItem;
        [MarshalAs(UnmanagedType.I1)]
        public bool bCashItem;
        public int nPrice;
        public int nAdditionalGP;
        //public int nAdditionalFP;
        public int nNowCash;
        public int nNowGamePoint;
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = P_SZ.MAX_ITEM_ID)]
        public string szItemId;
        
        public short nItemCount;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_PACKAGE_ITEM_COUNT)]
        public PROTO_REFORM_PACKAGE_ITEM[] aItems;
    }
}