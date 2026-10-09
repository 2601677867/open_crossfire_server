using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.CFVersion
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct CF_VERIFIER_VERSION_ACK_MSG
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 3)]
        private string byDummyBytes1;

        private int iClientReleaseVersion;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 1)]
        private string byDummyBytes2;
    }
}