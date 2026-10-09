using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;
using static Network.ProtocolStruct.WEAPON_CATEGORY;

namespace Network.ProtocolStruct.Inven
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public unsafe struct PROTO_SACK_INST
    {
        public fixed byte szID[MAX_ITEM_ID];
        public fixed byte szCode[MAX_ITEM_CODE];
        public int nSackSRL;
        public fixed long aaWeaponItemInvenSRL[(int) MAX_WEAPON_CATEGORY * MAX_WEAPON_SLOT];
        public ITEM_TIME Time;
        public long lInvenSrl;
    }
}