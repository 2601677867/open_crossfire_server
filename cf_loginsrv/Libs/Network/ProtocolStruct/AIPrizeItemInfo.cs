using System.Runtime.InteropServices;

using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct AIPrizeItemInfo
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
        private string m_szItemID;

        private long m_InvenSRL;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DRESS_PART)]
        private long[] m_arrDefaultDressInvenSrl;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_PERIOD_CHAR_FUNC)]
        private long[] m_arrDefaultFuncInvenSrl;

        private int m_bStorage;
    }
}