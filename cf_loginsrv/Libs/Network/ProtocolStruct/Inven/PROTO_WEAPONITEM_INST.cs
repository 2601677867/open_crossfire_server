using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Inven
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct PROTO_WEAPONITEM_INST
    {
        public PROTO_ITEM_INST tItemBase;
        public bool bVScope;
        public int nVScopeMin;
        public int nVScopeMax;
        public bool bWeaponUpgrade;
    }
}