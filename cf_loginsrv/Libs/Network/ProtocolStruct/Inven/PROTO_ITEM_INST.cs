using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Inven
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct PROTO_ITEM_INST
    {
        public fixed byte szItemId[MAX_ITEM_ID];
        public fixed byte szItemCode[MAX_ITEM_CODE];
        public long lInvenSrl;
        public ITEM_TIME tItemTime;
        public int nGaugeBar;
        public int nRepairCost;
        public int nCurrentGauge;
        
        [MarshalAs(UnmanagedType.I1)]
        public bool bWeeklyWeapon;
    }
}