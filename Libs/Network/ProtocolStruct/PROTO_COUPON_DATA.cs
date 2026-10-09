using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_COUPON_DATA
    {
        public int nCouponID;
        public int nChangeNeedCnt;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
        public string szItemID;

        public int nGachaItemIndex;
    }
}