using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Login
{
    // 80-byte wire entry, stride 0x50 - verified against cf_loginsrv_C++.exe
    // (CopyServerList = FUN_00408f90). The binaries are MSVC Pack = 4: name sits at
    // 26..56, the port at 60, leaving a 3-byte hole at 57..59.
    // Must stay Sequential + Pack = 4, never Explicit: m_szServerName is a reference
    // field at offset 26, and LayoutKind.Explicit rejects an object field that is not
    // pointer-aligned, which throws TypeLoadException the moment the type is touched
    // (Marshal.SizeOf / CopyToUserDataArea) instead of producing a wrong layout.
    // Pack = 4 reproduces the offsets above by construction.
    // This revision has no dummy, password, return-user or property field.
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PROTO_GAMESERVER
    {
        public short m_nServerHighProperty;   // 0
        public short m_nServerLowProperty;    // 2
        public short m_nServerLowLimit;       // 4
        public short m_nServerHighLimit;      // 6
        public double m_dServerLowKD;         // 8
        public double m_dServerHighKD;        // 16
        public short m_nServerID;             // 24

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SERVER_DISPLAY_NAME)]
        public byte[] m_szServerName;         // 26, hole 57..59

        public int m_nServerPort;             // 60
        public uint m_dwServerAddr;           // 64
        public int m_nServerLimitCount;       // 68
        public int m_nServerConnectCount;     // 72
        public int m_nEvent;                  // 76
    }
}
