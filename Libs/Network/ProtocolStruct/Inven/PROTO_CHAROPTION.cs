using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Inven
{
    public unsafe struct PROTO_CHAROPTION
    {
        public fixed int options[MAX_CHAR_OPTION];
        public fixed int levels[MAX_CHAR_OPTION];
    }
}