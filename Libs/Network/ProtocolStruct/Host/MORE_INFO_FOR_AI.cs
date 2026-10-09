using System.Runtime.InteropServices;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct MORE_INFO_FOR_AI
    {
        public int nTotalUser;
        public int nResurrect;
        public int nLastRound;
        public int nMedicKit;
        public byte byReviveCnt;
    }
}