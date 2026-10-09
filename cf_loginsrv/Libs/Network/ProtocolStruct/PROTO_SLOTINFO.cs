using System.Runtime.InteropServices;
using Network.ProtocolStruct.Achievement;

using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RING_BUFF_DATA
    {
        public int nBuffType;
        public int nBuffRarity;
        public int nBuffValue;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct RING_DATA
    {
        public int nnFirst;
        public long nnInvenSRL;
        public int nItemIndex;
        public byte bFlag;
        public int nNew;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_RING_BUFF_COUNT)]
        public RING_BUFF_DATA[] aBuff;
    }
    
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_SLOTINFO
    {
        [MarshalAs(UnmanagedType.I1)] public bool bIsBoss;
        [MarshalAs(UnmanagedType.I1)] public bool bIsBots;
        [MarshalAs(UnmanagedType.I1)] public bool bIsObserver;
        public short nTeamIndex;
        [MarshalAs(UnmanagedType.I1)] public bool bReady;
        [MarshalAs(UnmanagedType.I1)] public bool bPlay;
        public int nRankingLevel;
        public short wFameGrade;
        public int nNumKill;
        public int nNumDeath;
        public int nNumWin;
        public int nNumLose;
        public USERMANNERS eUserManners;
        public long nUserID;
        public PROTO_MYCLANINFO szClanName;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
        public byte[] szCharacterName;

        public int nNameCardIndex1;
        public int nNameCardIndex2;
        public PROTO_MAIN_BADGE_INFO stMainBadgeInfo;
        
#if USE_CFVIP_SYSTEM
        [MarshalAs(UnmanagedType.I1)] public bool bSpecialUser;
        public byte cSpecialUserLevel;
        public short sDummy;
        [MarshalAs(UnmanagedType.I1)] public bool bGreenCommunityUser;
#endif
        
        public byte nColorChattingIndex;
        public byte nColorCallNameIndex;
        public uint dwVVIPItemCount;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_VVIP_ITEM_NUM)]
        public VVIP_ITEM[] arrVVIPItemInfoIndex;

        public int nAchieveCallNameEffectValue;
        public int nAchieveNameCardEffectValue;
        public ST_ACHIEVE_DISPLAY achieveDisplayInfo;
        [MarshalAs(UnmanagedType.I1)] public bool is_globalroom_enter_;
        
        public int nMainCharacterIndex;
        public byte byRankLev;
        public byte byRankLev2;
        public byte bShowRankLev;
        public byte bShowRankLev2;
        public int nLoadingBGType;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_RING_COUNT)]
        public RING_DATA[] aRingData;
        
        public byte by111;
        public byte by222;
        public int i111;
        public int i222;
        
        public short nSlotIndex;
        
        public byte c1;
        
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_ITEM_CODE)]
        public string szMainCharacterItemCode;
        
        public int nMainWeaponIndex;
        public ushort wnew1;
        public ushort wnew2;
        public int nDisplayKill;
        public int nDisplayDeath;
        public byte ch_5;
        public int i_1;
        public int i_2;
        public int i_3;
        public short s_4;
        public byte c_5;
        public short s697;
        public byte by699;
    }
}