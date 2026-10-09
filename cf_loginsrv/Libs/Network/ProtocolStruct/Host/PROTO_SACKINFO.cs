using System.Runtime.InteropServices;
using Network.Protocol;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_SACKINFO
    {
        public int iNumber;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.ITEM_PER_SACK)]
        public int[] aWeaponItemInfoIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.ITEM_PER_SACK)]
        public byte[] bIsWeaponZeroState;
    }
}