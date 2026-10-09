using System.Runtime.InteropServices;
using Network.Protocol;

namespace Network.ProtocolStruct.Gacha
{
    public enum eGACHA_IDENTIFY_RESULT
    {
        GACHA_IDENTIFY_SUCCESS,
        GACHA_IDENTIFY_BLANK,
        GACHA_IDENTIFY_NOT_ENOUGH,
        GACHA_IDENTIFY_NOITEM,
        GACHA_IDENTIFY_FAIL,
        GACHA_MY_INVEN_LIMIT_FAIL,
        GACHA_IDENTIFY_MISMATCH,
        GACHA_LEVEL_MISMATCH,
        GACHA_INCORRECT_OPEN_TIME
        // 0xC
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_GACHA_IDENTIFY_RESULT
    {
        public eGACHA_IDENTIFY_RESULT eResult;

        public int nCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_GACHA_GROUP_NUM)]
        public int[] aItemIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_GACHA_GROUP_NUM)]
        public PROTO_ITEM_ID_DATA[] aItemID;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_GACHA_GROUP_NUM)]
        public int[] aCurrentGauge;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_GACHA_GROUP_NUM)]
        public long[] aInvenSrl;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_GACHA_GROUP_NUM)]
        public PROTO_DEFAULT_DRESS_DATA[] aDefaultDressInvenSrl;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_GACHA_GROUP_NUM)]
        public PROTO_DEFAULT_FUNC_DATA[] aDefaultFuncInvenSrl;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_GACHA_GROUP_NUM)]
        public int[] aItemCnt;
        
        public PROTO_MYCLANINFO tClan;
        
        public int nCashType;
        public int nTotalStorageCount;

        [MarshalAs(UnmanagedType.I1)]
        public bool bExtraReward;
        
        public int nLuckyGauge;
        public short nTotalTempStorageCount;
        
        public long lExtraItemSrl;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = P_SZ.MAX_ITEM_ID)]
        public string szExtraItemId;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = P_SZ.MAX_GACHA_STORAGE_SRL_STR_LENGTH)]
        public string szStorageSrlStr;
    }

}