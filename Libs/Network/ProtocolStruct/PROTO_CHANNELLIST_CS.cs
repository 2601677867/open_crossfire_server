using System.Runtime.InteropServices;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 4)]
    public class PROTO_CHANNELLIST_CS
    {
        public PROTO_CHANNELLIST_CS()
        {
            for (int i = 0; i < this.oLogin.Length; i++)
            {
                this.oLogin[i] = new LC_Login();
            }
        }

        public short nChannelID;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szChannelName;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public byte[] szChannelShortName;

        public short nMaxUser;

        public short nCurrentUser;

        public int iGMSIndex;

        public int nStep;

        public bool bPassWord;

        public int nChannelRestrictMinLevel;

        public int nChannelRestrictMaxLevel;

        public float fChannelRestrictMinKD;

        public float fChannelRestrictMaxKD;

        public LC_Login[] oLogin = new LC_Login[10];
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public class LC_Login
    {
        public byte btLoginState;

        public uint uiMainUID;
    }
}