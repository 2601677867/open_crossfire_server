using System.Runtime.InteropServices;
using Network.Protocol;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_USER_ATTACH_INFO
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = P_SZ.MAX_VVIP_ATTACHMENT_WEAPON_TYPE)]
        public int[] nItemIndex;
    }
}