using System;

namespace Network.ProtocolStruct
{
    [Flags]
    public enum ROOMSETTINGCHANGEFLAG
    {
        ROOMSETTINGCHANGEFLAG_NONE = 0,
        ROOMSETTINGCHANGEFLAG_ROUND = 1 << 0,
        ROOMSETTINGCHANGEFLAG_WINCONDITION = 1 << 1,
        ROOMSETTINGCHANGEFLAG_ROOMNAME = 1 << 2
    }
}