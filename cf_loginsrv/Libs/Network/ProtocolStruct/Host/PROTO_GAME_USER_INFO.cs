using System.Runtime.InteropServices;
using Network.ProtocolStruct.Inven;

using static Network.Protocol.P_SZ;

namespace Network.ProtocolStruct.Host
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct PROTO_GAME_USER_INFO
    {
        public long iUserID;
        
        [MarshalAs(UnmanagedType.I1)]
        public bool bIsBoss;
        
        [MarshalAs(UnmanagedType.I1)]
        public bool bIsBots;
        
        [MarshalAs(UnmanagedType.I1)]
        public bool bIsObserver;
        
        [MarshalAs(UnmanagedType.I1)]
        public bool bIsSupervisor;
        public byte tTeamIndex;
        public int iSlotIndex;
        public int nClientKey;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_HOST_USERID)]
        public string szUserID;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = SF_MAX_CHARACTER_NAME_LENGTH)]
        public byte[] aszUserCharname;

        [MarshalAs(UnmanagedType.ByValTStr, SizeConst = MAX_HOST_STARTDATE)]
        public string szStartDate;

        public long nStartTime;
        public int tCharItemInfoIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_DRESS_PART * MAX_DRESS_LAYER)]
        public int[] aDressItemInfoIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FUNC_ITEM)]
        public PROTO_GAME_USER_ITEM_INFO[] aItemInfo;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_FUNC_ITEM)]
        public int[] aFuncItemInfoIndex;

        public PROTO_WAVECARD_INST tWaveCardInfo;

        public int nNumMissionAccomplished;
        public byte tCurRankType;
        public short wFameGrade;
        public long iEP;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SACK_PER_USER)]
        public PROTO_SACKINFO[] aSackInfo;

        public int nNameCardIndex1;
        public int nNameCardIndex2;
        public byte nColorCallNameIndex;
        public byte nColorChattingIndex;
#if USE_CFVIP_SYSTEM
        public byte bSpecialUser;
        public byte bGreenCommunityUser;
#endif
        public PROTO_MAIN_BADGE_INFO stMainBadgeInfo;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SEND_SPRAYINFO_TO_HOST)]
        public short[] nSprayItemIndex;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SEND_ACHIEVEINFO_TO_HOST)]
        public PROTO_USER_ACHIEVE_DATA[] stAchieveData;

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SACK_PER_USER)]
        public PROTO_USER_ATTACH_INFO[] tVVIPAttachmentDoll;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SACK_PER_USER)]
        public PROTO_USER_ATTACH_INFO[] tVVIPAttachmentVFX;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SACK_PER_USER)]
        public PROTO_USER_ATTACH_INFO[] tVVIPAttachmentSkin;
        
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = MAX_SACK_PER_USER)]
        public PROTO_USER_ATTACH_INFO[] tVVIPAttachmentSound;
    }
}