using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.CFVersion
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct CF_VERIFIER_VERSION_REQ_MSG
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
        private string byDummyBytes1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        private string byPrivateKey;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 4)]
        private string byDummyBytes2;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
        private string byInstantKey;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
        private string byDummyBytes3;
    }
}