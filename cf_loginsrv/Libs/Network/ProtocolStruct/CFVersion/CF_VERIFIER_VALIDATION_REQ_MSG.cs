using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.CFVersion
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct CF_VERIFIER_VALIDATION_REQ_MSG
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
        private string byDummyBytes1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        private string byInstantKey;

        private int iReqItemNum;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        private string byDummyBytes2;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        private int[] iReqItemTypes;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
        private int[] iReqItemSubtypes;
    }
}