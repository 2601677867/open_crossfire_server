using System.Runtime.InteropServices;
using Network.Protocol;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct Team_Room_User
    {
        public P_ATNM.VALID_USER_INFO m_UserInfo;

        public uint m_dwTeamUserIndex;

        public int m_bCurrentPosition;

        public int m_bTeamMaster;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 13)]
        public string m_szCharacterName;

        public PROTO_MYCLANINFO m_tClanNameInfo;

        public int m_nLevel;

        public short m_nDummy;

        public int m_nNameCardIndex1;

        public int m_nNameCardIndex2;

        public int m_nMainBadgeKind;

        public int m_nMainBadgeLevel;

        public int m_nColorChattingIndex;

        public int m_nColorCallNameIndex;

        public int m_nCategory;

        public int m_nIndex;

        public int m_nSubIndex;

        public uint m_dwVVIPItemCount;
        
        public uint m_dwVVIPDummy;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public VVIP_ITEM[] m_arrVVIPItems;
    }
}