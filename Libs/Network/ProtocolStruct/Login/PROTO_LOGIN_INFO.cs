using System.Runtime.InteropServices;
using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Login
{
    // 8084-byte login body - verified against the binaries (MSVC Pack = 4):
    //   prefix 52 + 100 x PROTO_GAMESERVER(80) + trailer 32.
    // Sequential + Pack = 4, never Explicit: m_szCallName (0) and m_aServers (52) are
    // reference fields, and LayoutKind.Explicit throws TypeLoadException for an object
    // field it considers misaligned, which kills the type instead of the layout.
    // Two offsets here are Pack = 4 padding artifacts a Pack = 1 port silently drops:
    // the hole after the 13-byte name (USN sits at 16, not 13) and the int at 48 that
    // pushes the array to 52.
    // C++ `long` is 4 bytes on Win32, so USN and the connect keys are int, not Int64.
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PROTO_LOGIN_INFO
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
        public byte[] m_szCallName;           // 0, hole 13..15

        public int m_lUSN;                    // 16
        public byte m_bySpecialUser;          // 20, semantics not verified
        public byte m_byClanMember;           // 21
        public short m_nLevel;                // 22
        public int m_nKill;                   // 24
        public int m_nDeath;                  // 28
        public int m_nDummy32;                // 32, semantics not verified
        public double m_dKillDeath;           // 36

        public short m_nServerCount;          // 44
        public int m_nDummy48;                // 48, semantics not verified

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_GAMESERVER_COUNT)]
        public PROTO_GAMESERVER[] m_aServers; // 52

        // Trailer: only its 32-byte size is verified, the order inside it is not.
        public byte m_bySupervisor;           // 8052
        public int m_lKey1;                   // 8056
        public int m_nBannedTime;             // 8060
        public byte m_byUseGlobalJoin;        // 8064
        public byte m_byWaveLevel;            // 8065
        public byte m_byUseAutoServerSelect;  // 8066
        public int m_lKey2;                   // 8068
        public int m_iSSN;                    // 8072
        public byte m_bySeason;               // 8076
        public byte m_byNewSeasonNty;         // 8077
        public int m_iAddr;                   // 8080
    }
}
