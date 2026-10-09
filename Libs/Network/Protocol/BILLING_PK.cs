using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.Protocol
{
    public static class BILLING_PK
    {
        public const int PROTOCOL_BILLING = 248;

        public const int PROTOCOL_PAYLETTER_CONNECT = 0;
        
        public const int PROTOCOL_PAYLETTER_GETMONEY = 10;
        
        public const int PROTOCOL_PAYLETTER_BUYITEM = 20;
        public const int PROTOCOL_PAYLETTER_ROLLBACK = 21;
        public const int PROTOCOL_PAYLETTER_DEDUCT_GIFT = 22;
        public const int PROTOCOL_PAYLETTER_BUYITEM_LEAGUEREWARD = 23;
        
        public const int PROTOCOL_PAYLETTER_HEALTH = 90;
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_BILLING_BASE
        {
            public int dwSeq;
            public int nReturn;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_BILLING_MSG_LENGTH)]
            public string szMsg;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_BILLING_HEALTH
        {
            public int result;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_BILLING_CONNECT
        {
            public int iServerKey;
            public uint iAddrInet;
            public int iPort;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_BILLING_GET_CASH
        {
            public PROTO_BILLING_BASE tBillingBase;
            public long lUSN;
            public int nClientKey;
            public int nUserCash;
            public int nUserBonus;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_USER_CHARNAME_LENGTH)]
            public string szUserID;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_BILLING_ROLLBACK_CASH
        {
            public PROTO_BILLING_BASE tBillingBase;
            public long lUSN;
            public int nClientKey;
            public int nUserCash;
            public int nUserBonus;
            public int nAmount;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_USER_CHARNAME_LENGTH)]
            public string szUserID;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_BILLING_CHARGE_NO_LENGTH)]
            public string szChargeNo;
        }
        
        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct PROTO_BILLING_BUYITEM
        {
            public PROTO_BILLING_BASE tBillingBase;
            public long lUSN;
            public int nClientKey;
            public uint dwClientAddr;
            public int nLevel;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = SF_USER_CHARNAME_LENGTH)]
            public string szUserID;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_NAME)]
            public string szItemName;

            public int nItemIndex;
            public int nPrice;
            
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_ID)]
            public string szItemId;

            public long nInvenSRL;
            public bool bItemPeriodContinue;
            public uint dwBuyTime;
            public short nLogType;

            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_BILLING_CHARGENO_LENGTH)]
            public string szChargeNo;
            
            public int nNowCash;
        }
    }
}