using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Login
{
    // 12-byte wire entry (stride 0xc, filler = FUN_0040d1f0), reply total 100 x 12 =
    // 1200. Only the 12-byte size is verified - the field meanings below are the port's
    // working guess. Rows are written compacted from row 0 like the login server array.
    //   @0 short  server id
    //   @2 short  online status (1 = reachable)
    //   @4 int    current players, -1 = offline
    //   @8 int    0 = normal, 1 = crowded / event
    // Sequential for consistency with PROTO_GAMESERVER; this one has no reference
    // fields, so Explicit would also load, but keeping one style avoids re-introducing
    // the object-field alignment trap when a byte[] is ever added here.
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public struct PROTO_GAMESERVER_UPDATE
    {
        public short m_nServerID;             // 0
        public short m_nServerStatus;         // 2
        public int m_nServerConnectCount;     // 4
        public int m_nEvent;                  // 8
    }
}
