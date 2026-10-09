using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.CFVersion
{
    [StructLayout(LayoutKind.Sequential, Pack = 8)]
    public struct CF_VERIFIER_VALIDATION_ACK_MSG
    {
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
        private string byDummyBytes1;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 640)]
        private string byHashedValues;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 2)]
        private string byDummyBytes2;
    }
}